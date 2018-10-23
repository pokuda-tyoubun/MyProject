using FlexLucene.Analysis;
using FlexLucene.Analysis.Ja;
using FlexLucene.Analysis.Ja.Dict;
using FlexLucene.Document;
using FlexLucene.Index;
using FlexLucene.Store;
using FxCommonLib.FTSIndexer;
using FxCommonLib.Utils;
using java.nio.file;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TikaOnDotNet.TextExtraction;

namespace PokudaSearch.IndexBuilder {
    public class LuceneIndexWorker {


        #region Constants
        /// <summary>テキスト抽出器モード</summary>
        public enum TextExtractMode : int {
            Tika = 0,
            IFilter
        }
        /// <summary>Luceneインデックスディレクトリ</summary>
        public const string StoreDirName = @"\IndexStore";
        /// <summary>検索用Luceneインデックスディレクトリ名</summary>
        public const string IndexDirName = @"\Index";
        /// <summary>Luceneインデックス構築用ディレクトリ名</summary>
        public const string BuildDirName = @"\Build";
        /// <summary>マルチスレッド化にするファイル数の閾値</summary>
        private const int SplitBorder = 1000000;
        #endregion Constants

        #region MemberVariables
        /// <summary>インデックスを作成したファイルの総バイト数</summary>
		private static long _bytesTotal = 0;
        /// <summary>インデックスを作成したファイル数</summary>
		private static int _countTotal = 0;
        /// <summary>IFIlterエラーなどでインデックスに含まれなかったファイル数</summary>
		private static int _countSkipped = 0;
        /// <summary>インデックス作成対象ファイル数</summary>
		private static int _targetTotal = 0;
        /// <summary>2分割されたインデックス対象ファイルリスト</summary>
        private static List<FileInfo>[] _targetFileList = null;
        /// <summary>テキスト抽出モード</summary>
        private static TextExtractMode _txtExtractMode = TextExtractMode.Tika;

        /// <summary>Apache Tikaのテキスト抽出器</summary>
        private TextExtractor _txtExtractor = new TextExtractor();
        /// <summary>ハイライトフィールドタイプ</summary>
        private FieldType _hilightFieldType = new FieldType();
        #endregion MemberVariables

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LuceneIndexWorker() {
            SetHighlightFieldContentType(_hilightFieldType);
        }
        #endregion Constractors

        /// <summary>
        /// ハイライト対象フィールド用のフィールド定義
        /// </summary>
        private void SetHighlightFieldContentType(FieldType fieldType) {
            // IndexOptions.DOCS : ドキュメントのみインデックスする
            // IndexOptions.DOCS_AND_FREQS : ドキュメントと語の出現頻度をインデックスする
            // IndexOptions.DOCS_AND_FREQS_AND_POSITIONS : ドキュメントと語の出現頻度と出現位置をインデックスする。
            //                                              full scoring, フレーズ検索等が可能になる。
            // IndexOptions.DOCS_AND_FREQS_AND_POSITIONS_AND_OFFSETS;ドキュメントと語の出現頻度と出現位置と文字オフセットをインデックスする。

            //PostingsHighlighter用
            //fieldType.SetIndexOptions(IndexOptions.DOCS_AND_FREQS_AND_POSITIONS_AND_OFFSETS);

            //FastVectorHighlighterの時は以下を指定
            fieldType.SetIndexOptions(IndexOptions.DOCS_AND_FREQS_AND_POSITIONS);
            fieldType.SetStored(true);
            fieldType.SetTokenized(true);
            // FastvectorHighlighterを使うための設定
            fieldType.SetStoreTermVectors(true);
            fieldType.SetStoreTermVectorPositions(true);
            fieldType.SetStoreTermVectorOffsets(true);
        }

        /// <summary>
        /// インデックス作成
        /// </summary>
        /// <param name="analyzer"></param>
        /// <param name="rootPath"></param>
        /// <param name="targetDir"></param>
        /// <param name="progress"></param>
        /// <param name="txtExtractMode"></param>
        public async static void CreateIndex(
            Analyzer analyzer, 
            string rootPath, 
            string targetDir, 
            IProgress<ProgressReport> progress, 
            TextExtractMode txtExtractMode) {

            try {
                _txtExtractMode = txtExtractMode;

                //先に対象ファイルを分割
                _targetFileList = SplitTargetFiles(targetDir);

                var worker = await CreateIndexAsync(analyzer, rootPath, targetDir, progress);

                //FIXME FlexLucene.Index.IndexNotFoundException: no segments* file found in SimpleFSDirectory
                //      →Commit()を明示的に入れてみた。
                //      →ダメだったら、10000ファイル毎にマージする？
                //効果なし－＞以下のExceptionがでるのでファイルフラッシュ時間を空けてみる
                //Thread.Sleep(60000);
                if (_targetFileList.Length > 1) {
                    //2つのインデックスをマージ
                    MergeAndMove(analyzer, rootPath, targetDir);
                } else {
                    //インデックスファイルを一時構築用から検索用に移動
                    CopyIndexDir(rootPath + BuildDirName + "1", rootPath + IndexDirName);
                }

                AppObject.Logger.Info("インデックス構築処理完了");
            } catch (Exception e) {
                AppObject.Logger.Error(e.Message);   
            } finally {
            }
        }

        /// <summary>
        /// インデックスをマージして本番ディレクトリへ移動
        /// </summary>
        /// <param name="analyzer"></param>
        /// <param name="rootPath"></param>
        /// <param name="targetDir"></param>
        public static void MergeAndMove(
            Analyzer analyzer, 
            string rootPath, 
            string targetDir) {

            System.IO.Directory.CreateDirectory(rootPath + BuildDirName);
            FileUtil.DeleteDirectory(new DirectoryInfo(rootPath + BuildDirName));

            //2つのインデックスをマージ
            var fsIndexDir1 = FSDirectory.Open(FileSystems.getDefault().getPath(rootPath + BuildDirName + "1"));
            var fsIndexDir2 = FSDirectory.Open(FileSystems.getDefault().getPath(rootPath + BuildDirName + "2"));
            var tmpConf = new IndexWriterConfig(analyzer);
            var dirs = new FlexLucene.Store.Directory[] { fsIndexDir1, fsIndexDir2 };
            var ramIndexDir = MergeIndexAsRAMDir(dirs, tmpConf);
            //インデックスファイルを一時構築用に保存
            SaveFSIndexFromRAMIndex(ramIndexDir, rootPath + BuildDirName, analyzer);

            //インデックスファイルを一時構築用から検索用に移動
            CopyIndexDir(rootPath + BuildDirName, rootPath + IndexDirName);
        }

        /// <summary>
        /// インデックス作成
        /// </summary>
        /// <param name="analyzer"></param>
        /// <param name="rootPath"></param>
        /// <param name="targetDir"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public static Task<LuceneIndexWorker> CreateIndexAsync(
            Analyzer analyzer, 
            string rootPath, 
            string targetDir, 
            IProgress<ProgressReport> progress) {
            return Task.Run<LuceneIndexWorker>(() => DoWork(analyzer, rootPath, targetDir, progress));
        }


        /// <summary>
        /// 並列でインデックス作成
        /// </summary>
        /// <param name="analyzer"></param>
        /// <param name="rootPath"></param>
        /// <param name="targetDir"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        private static LuceneIndexWorker DoWork(
            Analyzer analyzer, 
            string rootPath, 
            string targetDir, 
            IProgress<ProgressReport> progress) {

            DeleteOldIndexes(rootPath);


            //第1スレッド引数
            var args1 = new List<object>();
            //invoke()で呼ばれるメソッドを設定
            args1.Add(progress);
            args1.Add(analyzer);
            args1.Add(rootPath + BuildDirName + "1");
            args1.Add(_targetFileList[0]);
            args1.Add("Thread1");

            var instance = new LuceneIndexWorker();
            if (_targetFileList.Length == 1) {
                //1スレッド実行
                instance.CreateFSIndex(args1);
            } else {
                //第2スレッド
                var args2 = new List<object>();
                //invoke()で呼ばれるメソッドを設定
                args2.Add(progress);
                args2.Add(analyzer);
                args2.Add(rootPath + BuildDirName + "2");
                args2.Add(_targetFileList[1]);
                args2.Add("Thread2");

                var options = new ParallelOptions() { MaxDegreeOfParallelism = 2 };
                Parallel.Invoke(options,
                    () => instance.CreateFSIndex(args1), 
                    () => instance.CreateFSIndex(args2));
            }

            return instance;
        }
        /// <summary>
        /// 引数のStore.Directory配列を全てマージして１つのRAMDirectoryにする
        /// </summary>
        /// <param name="dir1"></param>
        /// <param name="dir2"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private static FlexLucene.Store.Directory MergeIndexAsRAMDir(
            FlexLucene.Store.Directory[] dirs,
            IndexWriterConfig config) {

            var ret = new RAMDirectory();
            var writer = new IndexWriter(ret, config);

            try {
                writer.AddIndexes(dirs);
            } finally {
                writer.Close();
            }

            return ret;
        }
        /// <summary>
        /// RAMDirectoryのインデックスをFSIndexとして保存する
        /// </summary>
        /// <param name="ramDir"></param>
        /// <param name="savePath"></param>
        /// <param name="analyzer"></param>
        private static void SaveFSIndexFromRAMIndex(FlexLucene.Store.Directory ramDir, string savePath, Analyzer analyzer) {

            var fsIndexDir = FSDirectory.Open(FileSystems.getDefault().getPath(savePath));
            var fsConfig = new IndexWriterConfig(analyzer);
            IndexWriter fsIndexWriter = new IndexWriter(fsIndexDir, fsConfig);
            try {
                fsIndexWriter.AddIndexes(new FlexLucene.Store.Directory[]{ramDir});
                fsIndexWriter.Commit();
            } finally {
                fsIndexWriter.Close();
            }
        }
        /// <summary>
        /// １つのスレッドでFSIndexを作成する処理
        /// HACK 引数をばらせる？
        /// HACK internalとprivateの違い？
        /// </summary>
        /// <param name="args"></param>
        internal void CreateFSIndex(object args) {
            //キャスト
            var argList = (List<object>)args;
            //引数分解
            var progress = (IProgress<ProgressReport>)argList[0];
            var analyzer = (Analyzer)argList[1];
            var buildDirPath = (string)argList[2];
            var targetFileList = (List<FileInfo>)argList[3];
            var threadName = (string)argList[4];

            IndexWriterConfig config = new IndexWriterConfig(analyzer);
    		IndexWriter indexWriter = null;

            try {
                var indexBuildDir = FSDirectory.Open(FileSystems.getDefault().getPath(buildDirPath));
                indexWriter = new IndexWriter(indexBuildDir, config);

    			foreach (FileInfo fi in targetFileList) {
    				//Officeのテンポラリファイルは無視。
                    if (fi.Name.StartsWith("~")) {
    					continue;
                    }

    				try {
    					AddDocument(fi.FullName, indexWriter);

    					//インデックス作成ファイル表示
    					Interlocked.Increment(ref _countTotal);
    					Interlocked.Exchange(ref _bytesTotal, _bytesTotal + fi.Length);

                        AppObject.Logger.Info(threadName + ":" + fi.FullName);
    				} catch (Exception e) {
    					//インデックスが作成できなかったファイルを表示
                        AppObject.Logger.Error(e.Message);
    					Interlocked.Increment(ref _countSkipped);
                        AppObject.Logger.Info(threadName + ":" + "Skipped: " + fi.FullName);
    				}

                    //進捗度更新を呼び出し。
                    progress.Report(new ProgressReport() {
                        Percent = (int)((double)(_countTotal + _countSkipped) / _targetTotal * 100),
                        ProgressCount = _countTotal + _countSkipped,
                        TotalCount = _targetTotal
                    });
    			}
                if (_countTotal % 1000 == 0) {
                    //1000ファイル処理毎にコミット
                    indexWriter.Commit();
                }
                progress.Report(new ProgressReport() {
                    Percent = (int)((double)(_countTotal + _countSkipped) / _targetTotal * 100),
                    ProgressCount = _countTotal + _countSkipped,
                    TotalCount = _targetTotal
                });
                AppObject.Logger.Info(threadName + "：完了");
            } finally {
                if (indexWriter != null) {
                    //クローズ時にIndexファイルがフラッシュされる
                    indexWriter.Commit();
                    indexWriter.Close();
                }
            }
        }
        /// <summary>
        /// インデックスを本番フォルダへコピー
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        private static void CopyIndexDir(string sourcePath, string destPath) {
            //HACK 検索時にぶつからないようにセマフォが必要

            //対象フォルダ内をクリーンナップ
            DirectoryInfo dir = new DirectoryInfo(destPath);
            foreach (FileInfo fi in dir.GetFiles()) {
                fi.Delete();
            }

            //コピー
            dir = new DirectoryInfo(sourcePath);
            foreach (FileInfo fi in dir.GetFiles()) {
                File.Copy(fi.FullName, destPath + "\\" + fi.Name, true);
            }
        }
        /// <summary>
		/// 指定したテキスト抽出器でテキスト化したものをインデックス化
		/// テキスト抽出器の種類は以下のとおり
		/// 　・Apache Tika
		/// 　・IFilter
        /// </summary>
        /// <param name="path"></param>
        /// <param name="indexWriter"></param>
        private void AddDocument(string path, IndexWriter indexWriter) {
			Document doc = new Document();
			string filename = System.IO.Path.GetFileName(path);
			string extention = System.IO.Path.GetExtension(path);

            if (_txtExtractMode == TextExtractMode.Tika) {
                var content = _txtExtractor.Extract(path);
    			//doc.Add(new TextField("content", content.Text, FieldStore.NO));
    			doc.Add(new Field("content", content.Text, _hilightFieldType));
            } else {
    			//doc.Add(new TextField("content", IFilterParser.Parse(path), FieldStore.NO));
    			doc.Add(new Field("content", IFilterParser.Parse(path), _hilightFieldType));
            }

			doc.Add(new StringField("path", path, FieldStore.YES));
			doc.Add(new StringField("title", filename, FieldStore.YES));
			indexWriter.AddDocument(doc);
        }

        /// <summary>
        /// 構築用ディレクトリにある古いインデックスファイルを削除
        /// </summary>
        /// <param name="rootPath"></param>
        private static void DeleteOldIndexes(string rootPath) {
            System.IO.Directory.CreateDirectory(rootPath);
            System.IO.Directory.CreateDirectory(rootPath + IndexDirName);
            System.IO.Directory.CreateDirectory(rootPath + BuildDirName);
            System.IO.Directory.CreateDirectory(rootPath + BuildDirName + "1");
            System.IO.Directory.CreateDirectory(rootPath + BuildDirName + "2");

            //インデックスを削除
            FileUtil.DeleteDirectoryExceptOwn(rootPath + BuildDirName);
            FileUtil.DeleteDirectoryExceptOwn(rootPath + BuildDirName + "1");
            FileUtil.DeleteDirectoryExceptOwn(rootPath + BuildDirName + "2");
        }

        /// <summary>
        /// 処理対象ファイルを２分割する
        /// </summary>
        /// <param name="targetDir"></param>
        /// <returns></returns>
        private static List<FileInfo>[] SplitTargetFiles(string targetDir) {
            var ret = new List<List<FileInfo>>();

            var allFiles = FileUtil.GetAllFileInfo(targetDir);
            _targetTotal = allFiles.Count;

            if (allFiles.Count > SplitBorder && 
                Environment.ProcessorCount > 1) {
                //２つのリストで処理
                int border = allFiles.Count / 2;
                var list1 = new List<FileInfo>();
                for (int i = 0; i < border; i++) {
                    list1.Add(allFiles[i]);
                }
                ret.Add(list1);

                var list2 = new List<FileInfo>();
                for (int i = border; i < allFiles.Count; i++) {
                    list2.Add(allFiles[i]);
                }
                ret.Add(list2);
            } else {
                //１つのリストで処理
                var list1 = new List<FileInfo>();
                for (int i = 0; i < allFiles.Count; i++) {
                    list1.Add(allFiles[i]);
                }
                ret.Add(list1);
            }

            return ret.ToArray();
        }

        //----------------------------------------------
        //NOTE:RAMDirectoryのマルチスレッド版を作成してみたが、逆に遅い。
        #region Multi RAMDirectory
        public async static void CreateIndexByMultiRAM(
            Analyzer analyzer, 
            string rootPath, 
            string targetDir, 
            IProgress<ProgressReport> progress, 
            TextExtractMode txtExtractMode) {

            try {
                _txtExtractMode = txtExtractMode;

                //先に対象ファイルを分割
                _targetFileList = SplitTargetFiles(targetDir);

                var worker = await CreateRAMIndexAsync(analyzer, rootPath, targetDir, progress);

                //インデックスファイルを一時構築用から検索用に移動
                CopyIndexDir(rootPath + BuildDirName, rootPath + IndexDirName);

                AppObject.Logger.Info("インデックス構築処理完了");
            } catch (Exception e) {
                AppObject.Logger.Error(e.Message);   
            } finally {
            }
        }

        public static Task<LuceneIndexWorker> CreateRAMIndexAsync(
            Analyzer analyzer, 
            string rootPath, 
            string targetDir, 
            IProgress<ProgressReport> progress) {
            return Task.Run<LuceneIndexWorker>(() => DoWorkMulti(analyzer, rootPath, targetDir, progress));
        }

        private BlockingCollection<RAMDirectory> _que = new BlockingCollection<RAMDirectory>(10000);
        private static LuceneIndexWorker DoWorkMulti(
            Analyzer analyzer, 
            string rootPath, 
            string targetDir, 
            IProgress<ProgressReport> progress) {

            DeleteOldIndexes(rootPath);

            //
            var instance = new LuceneIndexWorker();
            var options = new ParallelOptions() { MaxDegreeOfParallelism = 4 };
            //Parallel.For(0, _targetFileList.Length, options, i => {
            //    instance.PushRAMDir(progress, analyzer, _targetFileList[i], "Thread" + i.ToString());
            //});
            Parallel.Invoke(options,
                () => instance.WriteFromQueue(progress, rootPath, analyzer),
                () => instance.PushRAMDir(progress, analyzer, _targetFileList[0], "Thread1"),
                () => instance.PushRAMDir(progress, analyzer, _targetFileList[1], "Thread2"));

            return instance;
        }
        internal void WriteFromQueue(IProgress<ProgressReport> progress, string rootPath, Analyzer analyzer) {
            while (true) {
                Thread.Sleep(3000);
                //終了条件判定
                if (_que.Count == 0 && (_countTotal + _countSkipped) == _targetTotal) {
                    break;
                }

                if (_que.Count > 0) {
                    RAMDirectory ram = null;
                    _que.TryTake(out ram, 10000);
                    SaveFSIndexFromRAMIndex(ram, rootPath + BuildDirName, analyzer);
                }
            }
        }
        internal void PushRAMDir(
            IProgress<ProgressReport> progress, 
            Analyzer analyzer, 
            List<FileInfo> targetFileList, 
            string threadName) {

            //nファイル毎に分割
            int count = 0;
            var tmpList = new List<List<FileInfo>>();
            List<FileInfo> tmp = null;
            foreach (FileInfo fi in targetFileList) {
                if (count % 1000 == 0) {
                    tmp = new List<FileInfo>();
                    tmpList.Add(tmp);
                }
                tmp.Add(fi);

                count++;
            }

            //nファイル毎にRAMDirectoryを作成
            foreach (List<FileInfo> list in tmpList) {
                IndexWriterConfig config = new IndexWriterConfig(analyzer);
        		IndexWriter indexWriter = null;
                var ram = new RAMDirectory();
                indexWriter = new IndexWriter(ram, config);

    			foreach (FileInfo fi in list) {
    				//Officeのテンポラリファイルは無視。
                    if (fi.Name.StartsWith("~")) {
    					continue;
                    }
    				try {
    					AddDocument(fi.FullName, indexWriter);

    					//インデックス作成ファイル表示
    					Interlocked.Increment(ref _countTotal);
    					Interlocked.Exchange(ref _bytesTotal, _bytesTotal + fi.Length);

                        AppObject.Logger.Info(threadName + ":" + fi.FullName);
    				} catch (Exception e) {
    					//インデックスが作成できなかったファイルを表示
                        AppObject.Logger.Error(e.Message);
    					Interlocked.Increment(ref _countSkipped);
                        AppObject.Logger.Info(threadName + ":" + "Skipped: " + fi.FullName);
    				} finally {
                    }

                    //進捗度更新を呼び出し。
                    progress.Report(new ProgressReport() {
                        Percent = (int)((double)(_countTotal + _countSkipped) / _targetTotal * 100),
                        ProgressCount = _countTotal + _countSkipped,
                        TotalCount = _targetTotal
                    });
    			}
                //クローズ時にIndexファイルがフラッシュされる
                indexWriter.Commit();
                indexWriter.Close();
                //キューに追加
                _que.TryAdd(ram, 10000);
            }
            AppObject.Logger.Info(threadName + "：完了");
        }
        #endregion Multi RAMDirectory
    }
}