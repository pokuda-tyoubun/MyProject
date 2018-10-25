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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TikaOnDotNet.TextExtraction;

namespace PokudaSearch.IndexBuilder {
    public class LuceneIndexBuilder {
        //TODO
        private const int SingleThreadBorder = 1;


        //HACK*RAMDirectoryとマルチスレッドでインデックスを作成を高速化する。
        //     →マルチコアの本のPart2-10を参考にしてみる。
        //　　→C\Temp(920件)で実装して、計測してみた。
        //        ・FSDirectory 3:50秒
        //        ・RAMDirectory 3:50秒
        //        ※マルチスレッドで分担すれば早くなる？
        //        （どちらにしろ、バックグラウンドでインデックス作成させたいので、RAMDirectoryへ）
        //HACK c:\Workspaceで試みるとハング(0xc0000005 メモリアクセス違反)した
        //     →定期的にファイルに書き出す or FSDirectoryをマルチにして統合する
        //     →実装してみたが 3:54秒(Thread1に大きいファイルが固まっていた)
        //HACK* →データをばらして試す必要あり。
        //HACK* 　→シングルスレッド 1:53秒
        //HACK* 　→2スレッド 1:08秒
        //HACK* 　→3スレッド 1:13秒

        //HACK*FastVectorHilighterに対応させる。(以下のURLを参考に実装)
        //        参考：https://gist.github.com/mocobeta/57a8f61250468180607d
        //HACK フィージビリティを確認できたらFxCommonLibにこのクラスを移行し、IndexBuilderも再構成すること
        //HACK アプリ終了後もインデックス作成プロセスが残っている。
        //HACK ユーザ(流行語)辞書登録機能を実装
        //        参考：https://ichigo.hopto.org/2017/11/29/userdictionary_flexlucene_lucene_net/

        //HACK 最大フィールド長1000らしいフィールド長を確認してみる
        //HACK SaveFSIndexFromRAMIndexに集約
        //HACK CopyIndexDirに集約

        //-----------------------------------------------------------------------------
        //DONE インデックスが追記モードになっているっぽい
        //DONE C\Tempでインデックスを作成してもキーワードが引っ掛からないのは何故か
        //     →hilightFieldType指定が誤っているようだ

        public enum TextExtractMode : int {
            Tika = 0,
            IFilter
        }

        private TextExtractor _txtExtractor = new TextExtractor();

		private long _bytesTotal = 0;
		private int _countTotal = 0;
		private int _countSkipped = 0;

        private TextBox _logViewer = null;

        private TextExtractMode _txtExtractMode = TextExtractMode.Tika;

        private StringBuilder _extractLog = new StringBuilder(""); 

        private FieldType _hilightFieldType = new FieldType();

        private delegate void SetProgressValueDelegate(int percent);

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logViewer"></param>
        /// <param name="txtExtractMode"></param>
        public LuceneIndexBuilder(TextBox logViewer, TextExtractMode txtExtractMode) {
            _logViewer = logViewer;
            _txtExtractMode = txtExtractMode;

            SetHighlightFieldContentType(_hilightFieldType);
        }

        #region PublicMethods
        // シングルスレッド版---------------------------------------------------------------------------
        /// <summary>
        /// シングルスレッドでの単純なインデックス作成
        /// </summary>
        /// <param name="rootPath">インデックスファイル用のルートフォルダ</param>
        /// <param name="indexDir">完成したインデックスファイルの置き場所</param>
        /// <param name="buildDir">ビルド時の中間ファイルの置き場所</param>
        /// <param name="targetDir">検索対象のフォルダ</param>
        public void CreateIndex(Analyzer analyzer, string rootPath, string indexDir, string buildDir, string targetDir) {

            //インデックスを削除
            FileUtil.DeleteDirectory(new DirectoryInfo(rootPath + buildDir));

            IndexWriterConfig config = new IndexWriterConfig(analyzer);
    		IndexWriter indexWriter = null;

            try {
                var indexBuildDir = FSDirectory.Open(FileSystems.getDefault().getPath(rootPath + buildDir));
                indexWriter = new IndexWriter(indexBuildDir, config);

                var di = new DirectoryInfo(targetDir);
                AddDirRecursively(di, indexWriter);
            } finally {
                if (indexWriter != null) {
                    //クローズ時にIndexファイルがフラッシュされる
                    indexWriter.Close();
                }
            }

            //作成したIndexファイルを上書き
            DirectoryInfo dir = new DirectoryInfo(rootPath + indexDir);
            foreach (FileInfo fi in dir.GetFiles()) {
                fi.Delete();
            }
            dir = new DirectoryInfo(rootPath + buildDir);
            foreach (FileInfo fi in dir.GetFiles()) {
                File.Copy(fi.FullName, rootPath + indexDir + "\\" + fi.Name, true);
            }

            AppendLogViewer("インデックス構築処理完了");
        }
        // ThreadPool版---------------------------------------------------------------------------
        /// <summary>
        /// ThreadPoolを使ったインデックス作成処理
        /// </summary>
        /// <param name="analyzer"></param>
        /// <param name="rootPath"></param>
        /// <param name="indexDir"></param>
        /// <param name="buildDir"></param>
        /// <param name="targetDir"></param>
        public void CreateMultiFSIndex(Analyzer analyzer, string rootPath, string indexDir, string buildDir, string targetDir) {
            //インデックスを削除
            System.IO.Directory.CreateDirectory(rootPath + buildDir + "1");
            FileUtil.DeleteDirectory(new DirectoryInfo(rootPath + buildDir + "1"));
            System.IO.Directory.CreateDirectory(rootPath + buildDir + "2");
            FileUtil.DeleteDirectory(new DirectoryInfo(rootPath + buildDir + "2"));

            //先に対象ファイルを分割
            var targetFileList = CreateTargetList(targetDir);

            //第1スレッド
            var args1 = new List<object>();
            //invoke()で呼ばれるメソッドを設定
            args1.Add(new SetProgressValueDelegate(SetProgressValue));
            args1.Add(analyzer);
            args1.Add(rootPath + buildDir + "1");
            args1.Add(targetFileList[0]);
            args1.Add("Thread1");
            ThreadPool.QueueUserWorkItem(new WaitCallback(CreateFSIndex), args1);

            if (targetFileList.Length > 1) {
                //第2スレッド
                var args2 = new List<object>();
                //invoke()で呼ばれるメソッドを設定
                args2.Add(new SetProgressValueDelegate(SetProgressValue));
                args2.Add(analyzer);
                args2.Add(rootPath + buildDir + "2");
                args2.Add(targetFileList[1]);
                args2.Add("Thread2");
                ThreadPool.QueueUserWorkItem(new WaitCallback(CreateFSIndex), args2);
            }

            //完了待ち
            //FIXME 良くないTPLにすべき
            while (true) {
                Thread.Sleep(100);
                int total = _countTotal + _countSkipped;
                if (total > 0 && total >= targetFileList[0].Count + targetFileList[1].Count) {
                    //ロック解放待ち
                    Thread.Sleep(5000);
                    break;
                }
            } 

            //HACK 完了待ちがサポートされているTPLに書き換える
            //2つのインデックスをマージ
            //FIXME シングルスレッドの場合を記述
            var fsIndexDir1 = FSDirectory.Open(FileSystems.getDefault().getPath(rootPath + buildDir + "1"));
            var fsIndexDir2 = FSDirectory.Open(FileSystems.getDefault().getPath(rootPath + buildDir + "2"));
            var tmpConf = new IndexWriterConfig(analyzer);
            var dirs = new FlexLucene.Store.Directory[] { fsIndexDir1, fsIndexDir2 };
            var ramIndexDir = MergeIndexAsRAMDir(dirs, tmpConf);

            //ファイルにインデックスを保存
            SaveFSIndexFromRAMIndex(ramIndexDir, rootPath + buildDir, analyzer);

            //作成したIndexファイルを移動
            CopyIndexDir(rootPath + buildDir, rootPath + indexDir);

            AppendLogViewer("インデックス構築処理完了");
        }
        #endregion PublicMethods

        #region PrivateMethods
        /// <summary>
        /// １つのスレッドでFSIndexを作成する処理
        /// </summary>
        /// <param name="args"></param>
        private void CreateFSIndex(object args) {
            //キャスト
            var argList = (List<object>)args;
            //引数分解
            var progress = (SetProgressValueDelegate)argList[0];
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
                        // Hack Interlocked.Incrementを使うべき
    					_countTotal++;
    					_bytesTotal += fi.Length;
                        AppendLogViewer(threadName + ":" + fi.FullName);
    				} catch (Exception e) {
    					//インデックスが作成できなかったファイルを表示
                        AppObject.Logger.Error(e.Message);
    					_countSkipped++;
                        AppendLogViewer(threadName + ":" + "Skipped: " + fi.FullName);
    				}

                    //進捗度更新を呼び出し。
                    if (((_countTotal + _countSkipped) % 10) == 0) {
                        progress.Invoke(_countTotal + _countSkipped);
                    }
    			}
                progress.Invoke(_countTotal + _countSkipped);
                AppObject.Logger.Info(threadName + "：完了");
            } finally {
                if (indexWriter != null) {
                    //クローズ時にIndexファイルがフラッシュされる
                    indexWriter.Close();
                }
            }
        }
        /// <summary>
        /// 進捗を表示
        /// </summary>
        /// <param name="totalCount"></param>
        private void SetProgressValue(int totalCount) {
            //HACK マルチスレッド下ではコントロールにアクセスできない。
            //_logViewer.Text = (totalCount).ToString() + "件 処理済み";
            Application.DoEvents();
        }
        /// <summary>
        /// インデックスを本番フォルダへコピー
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        private void CopyIndexDir(string sourcePath, string destPath) {
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
        /// RAMDirectoryのインデックスをFSIndexとして保存する
        /// </summary>
        /// <param name="ramDir"></param>
        /// <param name="savePath"></param>
        /// <param name="analyzer"></param>
        private void SaveFSIndexFromRAMIndex(FlexLucene.Store.Directory ramDir, string savePath, Analyzer analyzer) {

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
        /// 引数のStore.Directory配列を全てマージして１つのRAMDirectoryにする
        /// </summary>
        /// <param name="dir1"></param>
        /// <param name="dir2"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private FlexLucene.Store.Directory MergeIndexAsRAMDir(
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
        /// 処理対象ファイルを分割する
        /// </summary>
        /// <param name="targetDir"></param>
        /// <returns></returns>
        private List<FileInfo>[] CreateTargetList(string targetDir) {
            var ret = new List<List<FileInfo>>();

            var allFiles = FileUtil.GetAllFileInfo(targetDir);

            if (allFiles.Count > SingleThreadBorder && 
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

        /// <summary>
		/// 再帰的にフォルダを探索してインデックスに追加
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="indexWriter"></param>
        private void AddDirRecursively(DirectoryInfo directory, IndexWriter indexWriter) {

			foreach (FileInfo fi in directory.GetFiles()) {
				//Officeのテンポラリファイルは無視。
                if (fi.Name.StartsWith("~")) {
					continue;
                }

				try {
					AddDocument(fi.FullName, indexWriter);

					//インデックス作成ファイル表示
					_countTotal++;
					_bytesTotal += fi.Length;
                    AppendLogViewer(fi.FullName);
				} catch (Exception e) {
					//インデックスが作成できなかったファイルを表示
                    AppObject.Logger.Error(e.Message);
					_countSkipped++;
                    AppendLogViewer("Skipped: " + fi.FullName);
				}

                if (((_countTotal + _countSkipped) % 10) == 0) {
                    _logViewer.Text = (_countTotal + _countSkipped).ToString() + "件 処理済み";
                    Application.DoEvents();
                }
			}
            _logViewer.Text = (_countTotal + _countSkipped).ToString() + "件 処理済み";

			//再帰的にサブフォルダも追加
			foreach (DirectoryInfo di in directory.GetDirectories()) {

				AddDirRecursively(di, indexWriter);
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
    			doc.Add(new Field("content", IFilterParser.Parse(path), _hilightFieldType));
            } else {
    			//doc.Add(new TextField("content", IFilterParser.Parse(path), FieldStore.NO));
    			doc.Add(new Field("content", IFilterParser.Parse(path), _hilightFieldType));
            }

			doc.Add(new StringField("path", path, FieldStore.YES));
			doc.Add(new StringField("title", filename, FieldStore.YES));
			indexWriter.AddDocument(doc);
        }

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
        /// ログビューワーに追記
        /// </summary>
        /// <param name="msg"></param>
        private void AppendLogViewer(string msg) {
            AppObject.Logger.Info(msg);

            if (_logViewer != null) {
                _extractLog.Insert(0, msg + "\r\n");
            }
        }
        #endregion PrivateMethods


        #region Obsolete
        /// <summary>
        /// RAMDirectoryで作成して、最後にFSDirectoryへ
        /// ※シングルスレッドでは、FSDirectoryとパフォーマンスは変わらない。
        /// ※ヒープ領域を調整するように組まないとメモリエラーでハングする。
        /// </summary>
        /// <param name="analyzer"></param>
        /// <param name="rootPath"></param>
        /// <param name="indexDir"></param>
        /// <param name="buildDir"></param>
        /// <param name="targetDir"></param>
        [Obsolete]
        public void CreateRAM2FSIndex(Analyzer analyzer, string rootPath, string indexDir, string buildDir, string targetDir) {

            //インデックスを削除
            FileUtil.DeleteDirectory(new DirectoryInfo(rootPath + buildDir));

            IndexWriterConfig ramConfig = new IndexWriterConfig(analyzer);
    		IndexWriter ramIndexWriter = null;
            var fsIndexDir = FSDirectory.Open(FileSystems.getDefault().getPath(rootPath + buildDir));
            var ramIndexDir = new RAMDirectory();
            try {
                ramIndexWriter = new IndexWriter(ramIndexDir, ramConfig);

                var di = new DirectoryInfo(targetDir);
                AddDirRecursively(di, ramIndexWriter);
            } finally {
                if (ramIndexWriter != null) {
                    ramIndexWriter.Close();
                }
            }
            //ファイルにインデックスを保存
            var fsConfig = new IndexWriterConfig(analyzer);
            IndexWriter fsIndexWriter = new IndexWriter(fsIndexDir, fsConfig);
            try {
                fsIndexWriter.AddIndexes(new FlexLucene.Store.Directory[]{ramIndexDir});
                fsIndexWriter.Commit();
            } finally {
                fsIndexWriter.Close();
            }

            //作成したIndexファイルを上書き
            DirectoryInfo dir = new DirectoryInfo(rootPath + indexDir);
            foreach (FileInfo fi in dir.GetFiles()) {
                fi.Delete();
            }
            dir = new DirectoryInfo(rootPath + buildDir);
            foreach (FileInfo fi in dir.GetFiles()) {
                File.Copy(fi.FullName, rootPath + indexDir + "\\" + fi.Name, true);
            }

            AppendLogViewer("インデックス構築処理完了");
        }
        #endregion Obsolete
    }
}