using com.healthmarketscience.jackcess;
using FlexLucene.Analysis;
using FlexLucene.Analysis.Ja;
using FlexLucene.Analysis.Ja.Dict;
using FlexLucene.Document;
using FlexLucene.Index;
using FlexLucene.Store;
using FxCommonLib.FTSIndexer;
using FxCommonLib.Utils;
using java.nio.file;
using java.text;
using org.apache.tika.metadata;
using org.apache.tika.parser;
using org.apache.tika.sax;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TikaOnDotNet.TextExtraction;

namespace PokudaSearch.IndexUtil {
    public class LuceneIndexBuilder {
        public struct WebContents {
            public string Url;
            public string Title;
            public string Extention;
            public DateTime UpdateDate;
            public string Contents;
            public long Bytes;
        }

        #region Constants


        /// <summary>テキスト抽出器モード</summary>
        public enum TextExtractModes : int {
            Tika = 0,
            IFilter
        }
        public enum CreateModes : int {
            [EnumLabel("作成")]
            Create = 0,
            [EnumLabel("更新")]
            Update,
            [EnumLabel("外部参照")]
            OuterReference,
            [EnumLabel("即時検索")]
            OnDemand
        }
        #region IndexField
        public const string Path = "Path";
        public const string Title = "Title";
        public const string Extension = "Extension";
        public const string Content = "Content";
        public const string UpdateDate = "UpdateDate";
        #endregion IndexField

        /// <summary>インデックス化対象ファイルサイズ制限(Byte)</summary>
        public int FileSizeLimit = 0;
        /// <summary>Luceneインデックスディレクトリ</summary>
        public const string StoreDirName = @"\IndexStore";
        /// <summary>検索用Luceneインデックスディレクトリ名</summary>
        public const string IndexDirName = @"\Index";
        /// <summary>Luceneインデックス構築用ディレクトリ名</summary>
        public const string BuildDirName = @"\Build";
        /// <summary>マルチスレッド化にするファイル数の閾値</summary>
        private const int SplitBorder = 1;
        /// <summary>オンデマンド検索のファイル数上限</summary>
        private const int OnDemandMaxFileCount = 1000;
        #endregion Constants

        #region Properties
        public static bool DoStop = false;
        /// <summary>インデックス作成開始日時</summary>
        private static Dictionary<string, string> _targetExtensionDic = null;
        public static Dictionary<string, string> TargetExtensionDic {
            set { _targetExtensionDic = value; }
            get { return _targetExtensionDic; }
        }
        /// <summary>インデックス作成開始日時</summary>
        private static DateTime _startTime = DateTime.Now;
        public static DateTime StartTime {
            get { return _startTime; }
        }
        /// <summary>インデックス作成完了日時</summary>
        private static DateTime _endTime = DateTime.Now;
        public static DateTime EndTime {
            get { return _endTime; }
        }
        /// <summary>インデックス作成時間</summary>
        public static TimeSpan CreateTime {
            get { return EndTime - StartTime; }
        }
        /// <summary>インデックス対象パス</summary>
        private static string _indexedPath = "";
        public static string IndexedPath {
            get { return _indexedPath; }
        }
        /// <summary>インデックスファイルパス</summary>
        private static string _indexStorePath = "";
        public static string IndexStorePath {
            get { return _indexStorePath; }
        }
        /// <summary>インデックス作成対象ファイル数</summary>
		private static int _targetCount = 0;
        public static int TargetCount {
            get { return _targetCount; }
        }
        /// <summary>インデックスを作成したファイル数</summary>
		private static int _indexedCount = 0;
        public static int IndexedCount {
            get { return _indexedCount; }
        }
        /// <summary>IFIlterエラーなどでインデックスに含まれなかったファイル数</summary>
		private static int _skippedCount = 0;
        public static int SkippedCount {
            get { return _skippedCount; }
        }
        /// <summary>処理済みファイル数</summary>
        public static int FinishedCount {
            get { return _indexedCount + _skippedCount; }
        }
        /// <summary>テキスト抽出モード</summary>
        private static TextExtractModes _txtExtractMode = TextExtractModes.Tika;
        public static TextExtractModes TextExtractMode {
            get { return _txtExtractMode; }
        }
        /// <summary>インデックス作成モード</summary>
        private static CreateModes _createMode = CreateModes.Create;
        public static CreateModes CreateMode {
            get { return _createMode; }
        }
        /// <summary>インデックスを作成したファイルの総バイト数</summary>
		private static long _totalBytes = 0;
        public static long TotalBytes {
            get { return _totalBytes; }
        }
        /// <summary>予約No</summary>
		private static int _reservedNo = 0;
        public static int ReservedNo {
            set { _reservedNo = value; }
            get { return _reservedNo; }
        }

        #endregion Properties

        #region MemberVariables
        /// <summary>2分割されたインデックス対象ファイルリスト</summary>
        private static List<FileInfo>[] _targetFileList = null;

        /// <summary>Apache Tikaのテキスト抽出器</summary>
        private TextExtractor _txtExtractor = new TextExtractor();
        /// <summary>ハイライトフィールドタイプ</summary>
        private FieldType _hilightFieldType = new FieldType();
        private SimpleDateFormat _sdf = new SimpleDateFormat("yyyy/MM/dd");
        #endregion MemberVariables

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LuceneIndexBuilder() {
            _targetExtensionDic = CreateTargetExtensionsDic();
            SetHighlightFieldContentType(_hilightFieldType);

            this.FileSizeLimit = Properties.Settings.Default.FileSizeLimit * 1024 * 1024;
        }
        #endregion Constractors


        #region PublicMethods
        /// <summary>
        /// インデックス作成
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="targetDir"></param>
        /// <param name="progress"></param>
        /// <param name="txtExtractMode"></param>
        public async static void CreateIndexBySingleThread(
            string rootPath,
            string targetDir,
            IProgress<ProgressReport> progress,
            TextExtractModes txtExtractMode,
            string orgIndexStorePath = "") {

            DoStop = false;
            try {
                _txtExtractMode = txtExtractMode;

                var worker = await CreateIndexAsync(rootPath, targetDir, progress, orgIndexStorePath);

                AppObject.Logger.Info("インデックス構築完了");
            } catch (Exception e) {
                AppObject.Logger.Error(e.StackTrace);
            } finally {
            }
        }
        public async static void CreateRAMIndexBySingleThread(
            string targetDir,
            IProgress<ProgressReport> progress,
            TextExtractModes txtExtractMode,
            RAMDirectory ram) {

            DoStop = false;
            try {
                _txtExtractMode = txtExtractMode;

                var worker = await CreateRAMIndexAsync(targetDir, progress, ram);

                AppObject.Logger.Info("インデックス構築完了");
            } catch (Exception e) {
                AppObject.Logger.Error(e.StackTrace);
            } finally {
            }
        }
        public async static void CreateWebIndexBySingleThread(
            string rootPath,
            string targetUrl,
            Dictionary<string, WebContents> targetDic,
            IProgress<ProgressReport> progress,
            TextExtractModes txtExtractMode) {

            DoStop = false;
            try {
                _txtExtractMode = txtExtractMode;

                var worker = await CreateWebIndexAsync(rootPath, targetUrl, targetDic, progress);

                AppObject.Logger.Info("インデックス構築完了");
            } catch (Exception e) {
                AppObject.Logger.Error(e.StackTrace);
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
        /// <param name="rootPath"></param>
        /// <param name="targetDir"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        private static Task<LuceneIndexBuilder> CreateIndexAsync(
            string rootPath,
            string targetDir,
            IProgress<ProgressReport> progress,
            string orgIndexStorePath) {
            return Task.Run<LuceneIndexBuilder>(() => CreateSingle(rootPath, targetDir, progress, orgIndexStorePath));
        }
        private static Task<LuceneIndexBuilder> CreateRAMIndexAsync(
            string targetDir,
            IProgress<ProgressReport> progress,
            RAMDirectory ram) {
            return Task.Run<LuceneIndexBuilder>(() => CreateOnDemandIndex(targetDir, progress, ram));
        }
        /// <summary>
        /// Webインデックス作成
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="targetUrl"></param>
        /// <param name="targetDic"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        private static Task<LuceneIndexBuilder> CreateWebIndexAsync(
            string rootPath,
            string targetUrl,
            Dictionary<string, WebContents> targetDic,
            IProgress<ProgressReport> progress) {
            return Task.Run<LuceneIndexBuilder>(() => CreateWebIndex(rootPath, targetUrl, targetDic, progress));
        }
        /// <summary>
        /// CSVファイルの内容を文字列として取得
        /// </summary>
        /// <param name="csvPath"></param>
        /// <returns></returns>
        public static string ReadToString(string path) {
            string ret = "";
            using (var sr = new StreamReader(path, Encoding.GetEncoding(932))) {
                ret = sr.ReadToEnd();
            }
            return ret;
        }
        #endregion PublicMethods

        #region PrivateMethods
        /// <summary>
        /// 検索対象拡張子の辞書を作成
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> CreateTargetExtensionsDic() {
            var dic = new Dictionary<string, string>();

            AppObject.DbUtil.Open(AppObject.ConnectString);
            try {
                DataSet ds = AppObject.DbUtil.ExecSelect(SQLSrc.m_extensions.SELECT_ALL);
                foreach (DataRow dr in ds.Tables[0].Rows) {
                    string ext = StringUtil.NullToBlank(dr["extension"]);
                    if (!dic.ContainsKey(ext)) {
                        dic.Add(ext, "");
                    }
                }
            } finally {
                AppObject.DbUtil.Close();
            }
            return dic;
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

        private static LuceneIndexBuilder CreateOnDemandIndex(
            string targetDir,
            IProgress<ProgressReport> progress,
            RAMDirectory ram) {

            //カウントを初期化
            _targetCount = 0;
            _indexedCount = 0;
            _skippedCount = 0;
            _totalBytes = 0;

            var targetFileList = FileUtil.GetAllFileInfo(targetDir);
            _targetCount = targetFileList.Count;
            _indexedPath = targetDir;
            _indexStorePath = "NONE";
            if (_targetCount > OnDemandMaxFileCount) {
                return null;
            }
            _createMode = CreateModes.OnDemand;

            var instance = new LuceneIndexBuilder();
            var options = new ParallelOptions() { MaxDegreeOfParallelism = 1 };
            Parallel.Invoke(options,
                () => instance.CreateRAMIndex(
                    targetFileList,
                    progress,
                    "IndexingThread1",
                    ram));

            return instance;
        }

        /// <summary>
        /// インデックス作成
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="targetDir"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        private static LuceneIndexBuilder CreateSingle(
            string rootPath,
            string targetDir,
            IProgress<ProgressReport> progress,
            string orgIndexStorePath) {

            //カウントを初期化
            _targetCount = 0;
            _indexedCount = 0;
            _skippedCount = 0;
            _totalBytes = 0;

            var targetFileList = FileUtil.GetAllFileInfo(targetDir);
            _targetCount = targetFileList.Count;
            _indexedPath = targetDir;
            _indexStorePath = rootPath + IndexDirName + _reservedNo.ToString();
            if (StringUtil.NullToBlank(orgIndexStorePath) != "") {
                //既存のIndexディレクトリのパスを取得
                _indexStorePath = orgIndexStorePath;
                _createMode = CreateModes.Update;

            } else {
                //インデックス保存場所フォルダ作成
                System.IO.Directory.CreateDirectory(rootPath);
                System.IO.Directory.CreateDirectory(_indexStorePath);
                _createMode = CreateModes.Create;
            }

            var instance = new LuceneIndexBuilder();
            var options = new ParallelOptions() { MaxDegreeOfParallelism = 1 };
            Parallel.Invoke(options,
                () => instance.CreateFSIndex(
                    //rootPath + BuildDirName + "1",
                    _indexStorePath,
                    targetFileList,
                    progress,
                    "IndexingThread1"));

            return instance;
        }
        /// <summary>
        /// Webインデックス作成
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="targetUrl"></param>
        /// <param name="targetDic"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        private static LuceneIndexBuilder CreateWebIndex(
            string rootPath,
            string targetUrl,
            Dictionary<string, WebContents> targetDic,
            IProgress<ProgressReport> progress) {

            //カウントを初期化
            _targetCount = 0;
            _indexedCount = 0;
            _skippedCount = 0;
            _totalBytes = 0;

            _targetCount = targetDic.Count;
            _indexedPath = targetUrl;
            _indexStorePath = rootPath + IndexDirName + _reservedNo.ToString();
            //インデックス保存場所フォルダ作成
            System.IO.Directory.CreateDirectory(rootPath);
            System.IO.Directory.CreateDirectory(_indexStorePath);
            _createMode = CreateModes.Create;

            var instance = new LuceneIndexBuilder();
            var options = new ParallelOptions() { MaxDegreeOfParallelism = 1 };
            Parallel.Invoke(options,
                () => instance.CreateWebFSIndex(
                    _indexStorePath,
                    targetDic,
                    progress,
                    "IndexingThread1"));

            return instance;
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
                fsIndexWriter.AddIndexes(new FlexLucene.Store.Directory[] { ramDir });
                fsIndexWriter.Commit();
            } finally {
                fsIndexWriter.Close();
            }
        }
        internal void CreateWebFSIndex(
            string buildDirPath,
            Dictionary<string, WebContents> targetDic,
            IProgress<ProgressReport> progress,
            string threadName) {

            IndexWriterConfig config = new IndexWriterConfig(AppObject.AppAnalyzer);
            IndexWriter indexWriter = null;
            FSDirectory indexBuildDir = null;
            Dictionary<string, DocInfo> docDic = null;

            try {
                indexBuildDir = FSDirectory.Open(FileSystems.getDefault().getPath(buildDirPath));
                if (_createMode == CreateModes.Update) {
                    //既存ドキュメントを辞書化
                    var liru = new LuceneIndexReaderUtil();
                    docDic = liru.CreateDocumentDic(indexBuildDir);
                }

                int bufferSize = Properties.Settings.Default.BufferSizeLimit;
                config.SetRAMBufferSizeMB(bufferSize); //デフォルト16MB
                indexWriter = new IndexWriter(indexBuildDir, config);

                //開始時刻を記録
                _startTime = DateTime.Now;
                _indexedCount = 0;
                _skippedCount = 0;
                progress.Report(new ProgressReport() {
                    Percent = 0,
                    ProgressCount = FinishedCount,
                    TargetCount = _targetCount,
                    Status = ProgressReport.ProgressStatus.Start
                });

                //NOTE すぐにログ出力すると、フリーズ現象が発生する。
                Thread.Sleep(1000);
                AppObject.Logger.Info(threadName + "Indexing start.");
                foreach (var kvp in targetDic) {
                    var wc = kvp.Value;

                    Thread.Sleep(10);
                    if (DoStop) {
                        //中断
                        AppObject.Logger.Info("Stop command executed.");
                        indexWriter.Rollback();
                        return;
                    }

                    try {
                        if (AddWebDocument(wc, indexWriter, threadName)) {
                            //インデックス作成ファイル表示
                            AppObject.Logger.Info(threadName + ":" + wc.Url);
                            Interlocked.Increment(ref _indexedCount);
                            Interlocked.Exchange(ref _totalBytes, _totalBytes + wc.Bytes);
                        } 
                    } catch (IOException ioe) {
                        AppObject.Logger.Error(ioe.StackTrace);
                        Interlocked.Increment(ref _skippedCount);
                        AppObject.Logger.Info(threadName + ":" + "Skipped: " + wc.Url);
                        GC.Collect();
                    } catch (Exception e) {
                        //インデックスが作成できなかったファイルを表示
                        AppObject.Logger.Warn(e.Message);
                        Interlocked.Increment(ref _skippedCount);
                        AppObject.Logger.Info(threadName + ":" + "Skipped: " + wc.Url);
                        GC.Collect();
                        if (e.Message.IndexOf("IndexWriter is closed") >= 0) {
                            AppObject.Logger.Warn(e.GetBaseException().ToString());
                            throw new AlreadyClosedException("Index file capacity over. Please divide index directory.");
                        }
                    }
                    //進捗度更新を呼び出し。
                    progress.Report(new ProgressReport() {
                        Percent = GetPercentage(),
                        ProgressCount = FinishedCount,
                        TargetCount = _targetCount,
                        Status = ProgressReport.ProgressStatus.Processing
                    });
                }
                if (docDic != null) {
                    //削除されたファイルをインデックスから除去
                    foreach(var kvp in docDic) {
                        if (kvp.Value.Exists == false) {
                            //削除
                            Term t = new Term(LuceneIndexBuilder.Path, kvp.Value.Path);
                            indexWriter.DeleteDocuments(t);
                        }
                    }

                }
                indexWriter.Commit();
                AppObject.Logger.Info(threadName + "：完了");
            } catch (Exception e) {
                AppObject.Logger.Error(e.Message);
                if (indexWriter != null) {
                    indexWriter.Rollback();
                }
                throw e;
            } finally {
                if (indexWriter != null && indexWriter.IsOpen()) {
                    //クローズ時にIndexファイルがフラッシュされる
                    indexWriter.Close();
                }
                //完了時刻を記録
                _endTime = DateTime.Now;
                progress.Report(new ProgressReport() {
                    Percent = GetPercentage(),
                    ProgressCount = FinishedCount,
                    TargetCount = _targetCount,
                    Status = ProgressReport.ProgressStatus.Finished
                });
                indexBuildDir.Close();
            }
        }

        /// <summary>
        /// １つのスレッドでFSIndexを作成する処理
        /// Delete-Insert
        /// </summary>
        /// <param name="args"></param>
        internal void CreateFSIndex(
            string buildDirPath,
            List<FileInfo> targetFileList,
            IProgress<ProgressReport> progress,
            string threadName) {

            IndexWriterConfig config = new IndexWriterConfig(AppObject.AppAnalyzer);
            IndexWriter indexWriter = null;
            FSDirectory indexBuildDir = null;
            Dictionary<string, DocInfo> docDic = null;

            try {
                indexBuildDir = FSDirectory.Open(FileSystems.getDefault().getPath(buildDirPath));
                if (_createMode == CreateModes.Update) {
                    //既存ドキュメントを辞書化
                    var liru = new LuceneIndexReaderUtil();
                    docDic = liru.CreateDocumentDic(indexBuildDir);
                }

                int bufferSize = Properties.Settings.Default.BufferSizeLimit;
                config.SetRAMBufferSizeMB(bufferSize); //デフォルト16MB
                indexWriter = new IndexWriter(indexBuildDir, config);

                //開始時刻を記録
                _startTime = DateTime.Now;
                _indexedCount = 0;
                _skippedCount = 0;
                progress.Report(new ProgressReport() {
                    Percent = 0,
                    ProgressCount = FinishedCount,
                    TargetCount = _targetCount,
                    Status = ProgressReport.ProgressStatus.Start
                });

                //NOTE すぐにログ出力すると、フリーズ現象が発生する。
                Thread.Sleep(1000);
                AppObject.Logger.Info(threadName + "Indexing start.");
                foreach (FileInfo fi in targetFileList) {
                    Thread.Sleep(10);
                    if (DoStop) {
                        //中断
                        AppObject.Logger.Info("Stop command executed.");
                        indexWriter.Rollback();
                        return;
                    }
                    //Officeのテンポラリファイルは無視。
                    if (fi.Name.StartsWith("~")) {
                        continue;
                    }

                    try {
                        if (AddDocument(fi.FullName, indexWriter, threadName, docDic)) {
                            //インデックス作成ファイル表示
                            AppObject.Logger.Info(threadName + ":" + fi.FullName);
                            Interlocked.Increment(ref _indexedCount);
                            Interlocked.Exchange(ref _totalBytes, _totalBytes + fi.Length);
                        } 
                    } catch (IOException ioe) {
                        AppObject.Logger.Error(ioe.StackTrace);
                        Interlocked.Increment(ref _skippedCount);
                        AppObject.Logger.Info(threadName + ":" + "Skipped: " + fi.FullName);
                        GC.Collect();
                    } catch (Exception e) {
                        //インデックスが作成できなかったファイルを表示
                        AppObject.Logger.Warn(e.Message);
                        Interlocked.Increment(ref _skippedCount);
                        AppObject.Logger.Info(threadName + ":" + "Skipped: " + fi.FullName);
                        GC.Collect();
                        if (e.Message.IndexOf("IndexWriter is closed") >= 0) {
                            AppObject.Logger.Warn(e.GetBaseException().ToString());
                            throw new AlreadyClosedException("Index file capacity over. Please divide index directory.");
                        }
                    }
                    //進捗度更新を呼び出し。
                    progress.Report(new ProgressReport() {
                        Percent = GetPercentage(),
                        ProgressCount = FinishedCount,
                        TargetCount = _targetCount,
                        Status = ProgressReport.ProgressStatus.Processing
                    });
                }
                if (docDic != null) {
                    //削除されたファイルをインデックスから除去
                    foreach(var kvp in docDic) {
                        if (kvp.Value.Exists == false) {
                            //削除
                            Term t = new Term(LuceneIndexBuilder.Path, kvp.Value.Path);
                            indexWriter.DeleteDocuments(t);
                        }
                    }

                }
                indexWriter.Commit();
                AppObject.Logger.Info(threadName + "：完了");
            } catch (Exception e) {
                AppObject.Logger.Error(e.Message);
                if (indexWriter != null) {
                    indexWriter.Rollback();
                }
                throw e;
            } finally {
                if (indexWriter != null && indexWriter.IsOpen()) {
                    //クローズ時にIndexファイルがフラッシュされる
                    indexWriter.Close();
                }
                //完了時刻を記録
                _endTime = DateTime.Now;
                progress.Report(new ProgressReport() {
                    Percent = GetPercentage(),
                    ProgressCount = FinishedCount,
                    TargetCount = _targetCount,
                    Status = ProgressReport.ProgressStatus.Finished
                });
                indexBuildDir.Close();
            }
        }
        internal void CreateRAMIndex(
            List<FileInfo> targetFileList,
            IProgress<ProgressReport> progress,
            string threadName,
            RAMDirectory ram) {

            IndexWriterConfig config = new IndexWriterConfig(AppObject.AppAnalyzer);
            IndexWriter indexWriter = null;

            try {
                int bufferSize = Properties.Settings.Default.BufferSizeLimit;
                config.SetRAMBufferSizeMB(bufferSize); //オンデマンド時は、512MB
                indexWriter = new IndexWriter(ram, config);

                //開始時刻を記録
                _startTime = DateTime.Now;
                _indexedCount = 0;
                _skippedCount = 0;
                progress.Report(new ProgressReport() {
                    Percent = 0,
                    ProgressCount = FinishedCount,
                    TargetCount = _targetCount,
                    Status = ProgressReport.ProgressStatus.Start
                });
                //NOTE すぐにログ出力すると、フリーズ現象が発生する。
                Thread.Sleep(1000);
                AppObject.Logger.Info(threadName + "Indexing start.");
                foreach (FileInfo fi in targetFileList) {
                    Thread.Sleep(10);
                    if (DoStop) {
                        //中断
                        AppObject.Logger.Info("Stop command executed.");
                        indexWriter.Rollback();
                        return;
                    }
                    //Officeのテンポラリファイルは無視。
                    if (fi.Name.StartsWith("~")) {
                        continue;
                    }

                    try {
                        if (AddDocument(fi.FullName, indexWriter, threadName, docDic:null)) {
                            //インデックス作成ファイル表示
                            AppObject.Logger.Info(threadName + ":" + fi.FullName);
                            Interlocked.Increment(ref _indexedCount);
                            Interlocked.Exchange(ref _totalBytes, _totalBytes + fi.Length);
                        } 
                    } catch (IOException ioe) {
                        AppObject.Logger.Error(ioe.StackTrace);
                        Interlocked.Increment(ref _skippedCount);
                        AppObject.Logger.Info(threadName + ":" + "Skipped: " + fi.FullName);
                        GC.Collect();
                    } catch (Exception e) {
                        //インデックスが作成できなかったファイルを表示
                        AppObject.Logger.Warn(e.Message);
                        Interlocked.Increment(ref _skippedCount);
                        AppObject.Logger.Info(threadName + ":" + "Skipped: " + fi.FullName);
                        GC.Collect();
                        if (e.Message.IndexOf("IndexWriter is closed") >= 0) {
                            AppObject.Logger.Warn(e.GetBaseException().ToString());
                            throw new AlreadyClosedException("Index file capacity over. Please divide index directory.");
                        }
                    }
                    //進捗度更新を呼び出し。
                    progress.Report(new ProgressReport() {
                        Percent = GetPercentage(),
                        ProgressCount = FinishedCount,
                        TargetCount = _targetCount,
                        Status = ProgressReport.ProgressStatus.Processing
                    });
                }
                indexWriter.Commit();
                AppObject.Logger.Info(threadName + "：完了");
            } catch (Exception e) {
                AppObject.Logger.Error(e.Message);
                if (indexWriter != null) {
                    indexWriter.Rollback();
                }
                throw e;
            } finally {
                if (indexWriter != null && indexWriter.IsOpen()) {
                    //クローズ時にIndexファイルがフラッシュされる
                    indexWriter.Close();
                }
                //完了時刻を記録
                _endTime = DateTime.Now;
                progress.Report(new ProgressReport() {
                    Percent = GetPercentage(),
                    ProgressCount = FinishedCount,
                    TargetCount = _targetCount,
                    Status = ProgressReport.ProgressStatus.Finished
                });
            }
        }
        /// <summary>
        /// 進捗率(パーセンテージ)を取得
        /// </summary>
        /// <returns></returns>
        private int GetPercentage() {
            int val = 0;
            if (_targetCount > 0) {
                val = (int)((double)(FinishedCount) / _targetCount * 100);
                if (val > 100) {
                    val = 100;
                }
            }
            return val;
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

        private BodyContentHandler _handler = new BodyContentHandler();
        private Metadata _metadata = new Metadata();
        private Parser _parser = new AutoDetectParser();
        /// <summary>
        /// Webページをインデックス化
        /// </summary>
        /// <param name="wc"></param>
        /// <param name="indexWriter"></param>
        /// <param name="threadName"></param>
        /// <returns></returns>
        private bool AddWebDocument(WebContents wc, IndexWriter indexWriter, string threadName) {
            string extension = wc.Extention;

            //ドキュメント追加
            Document doc = new Document();
            if (extension.ToLower() == ".html" || 
                extension.ToLower() == ".htm" ||
                extension.ToLower() == "") {
                if (wc.Contents.Length > 0) {
                    if (_txtExtractMode == TextExtractModes.Tika) {
                        byte[] data = System.Text.Encoding.Unicode.GetBytes(wc.Contents);
                        _parser.parse(new java.io.ByteArrayInputStream(data), _handler, _metadata, new ParseContext());
                        string content = _handler.toString();
                        doc.Add(new Field(Content, content, _hilightFieldType));
                    } else {
                        doc.Add(new Field(Content, IFilterParser.Parse(wc.Contents), _hilightFieldType));
                    }
                }
            } else {
                //FIXME ダウンロードしてインデックス化
                //バイト数セット
                //インデックス後削除
            }

			doc.Add(new StringField(Path, wc.Url, FieldStore.YES));
			doc.Add(new StringField(Title, wc.Title, FieldStore.YES));
			doc.Add(new StringField(Extension, extension.ToLower(), FieldStore.YES));
            long l = long.Parse(wc.UpdateDate.ToString("yyyyMMddHHmmss"));
			doc.Add(new LongPoint(UpdateDate, l));
			doc.Add(new StoredField(UpdateDate, l));
			//doc.Add(new StringField(UpdateDate, 
            //    DateTools.DateToString(_sdf.parse(wc.UpdateDate.ToString("yyyy/MM/dd")), DateToolsResolution.DAY), 
            //    FieldStore.YES));
			indexWriter.AddDocument(doc);

            return true;
        }

        /// <summary>
		/// 指定したテキスト抽出器でテキスト化したものをインデックス化
		/// テキスト抽出器の種類は以下のとおり
		/// 　・Apache Tika
		/// 　・IFilter
        /// </summary>
        /// <param name="path"></param>
        /// <param name="indexWriter"></param>
        private bool AddDocument(string path, IndexWriter indexWriter, string threadName, Dictionary<string, DocInfo> docDic) {
            string filename = System.IO.Path.GetFileName(path);
            string extension = System.IO.Path.GetExtension(path);
            FileInfo fi = new FileInfo(path);

            if (extension == "" ||
                !_targetExtensionDic.ContainsKey(extension.ToLower())) {
                //拡張子なし or 対象拡張子外
                AppObject.Logger.Info(threadName + ":" + "Out of target extension. Skipped: " + path);
                Interlocked.Increment(ref _skippedCount);

                return false;
            }
            if (extension.ToLower() != ".mp4" && fi.Length > this.FileSizeLimit) {
                //サイズオーバー(mp4は対象外)
                AppObject.Logger.Info(threadName + ":" + "File size over. Skipped: " + path);
                Interlocked.Increment(ref _skippedCount);

                return false;
            }
            //存在するドキュメントか？
            if (docDic != null && docDic.ContainsKey(path)) {
                DocInfo di = docDic[path];
                di.Exists = true;
                docDic[path] = di;
                //更新日時チェック(秒単位で比較)
                if (di.UpdateDate < DateTimeUtil.Truncate(fi.LastWriteTime, TimeSpan.FromSeconds(1))) {
                    //更新されている場合Delete+Insert
                    Term t = new Term(LuceneIndexBuilder.Path, di.Path);
                    indexWriter.DeleteDocuments(t);

                } else {
                    //更新されていない。
                    AppObject.Logger.Info(threadName + ":" + "No updated. Skipped: " + path);
                    Interlocked.Increment(ref _skippedCount);

                    return false;
                }
            }

            //ドキュメント追加
            Document doc = new Document();
            if (extension.ToLower() == ".md") {
                //Markdown形式
                string content = ReadToString(path);
                doc.Add(new Field(Content, content, _hilightFieldType));
            } else if (extension.ToLower() == ".txt") {
                //TXTファイル
                var sjis = Encoding.GetEncoding("Shift_JIS");
                if (FileUtil.GetTextEncoding(path) == sjis) {
                    string content = "";
                    using (var reader = new StreamReader(path, sjis)) {
                        content = reader.ReadToEnd();
                    }
                    doc.Add(new Field(Content, content, _hilightFieldType));
                } else {
                    if (_txtExtractMode == TextExtractModes.Tika) {
                        var content = _txtExtractor.Extract(path);
                        doc.Add(new Field(Content, content.Text, _hilightFieldType));
                    } else {
                        doc.Add(new Field(Content, IFilterParser.Parse(path), _hilightFieldType));
                    }
                }
            } else {
                if (_txtExtractMode == TextExtractModes.Tika) {
                    var content = _txtExtractor.Extract(path);
                    doc.Add(new Field(Content, content.Text, _hilightFieldType));
                } else {
                    doc.Add(new Field(Content, IFilterParser.Parse(path), _hilightFieldType));
                }
            }

			doc.Add(new StringField(Path, path, FieldStore.YES));
			doc.Add(new StringField(Title, filename.ToLower(), FieldStore.YES));
			doc.Add(new StringField(Extension, extension.ToLower(), FieldStore.YES));
            //NOTE:Date型のFieldは存在しないのでlongで保持
            long l = long.Parse(fi.LastWriteTime.ToString("yyyyMMddHHmmss"));
			doc.Add(new LongPoint(UpdateDate, l));
			doc.Add(new StoredField(UpdateDate, l));
			//doc.Add(new StringField(UpdateDate, 
            //    DateTools.DateToString(_sdf.parse(fi.LastWriteTime.ToString("yyyy/MM/dd")), DateToolsResolution.DAY), 
            //    FieldStore.YES));
			indexWriter.AddDocument(doc);

            return true;
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
            _targetCount = allFiles.Count;

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
        #endregion PrivateMethods

        //----------------------------------------------
        //NOTE:RAMDirectoryのマルチスレッド版を作成してみたが、逆に遅い。
        //     大量ファイルだと失敗する場合がある。
        #region Multi RAMDirectory
        [Obsolete]
        public async static void CreateIndexByMultiRAM(
            Analyzer analyzer, 
            string rootPath, 
            string targetDir, 
            IProgress<ProgressReport> progress, 
            TextExtractModes txtExtractMode) {

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

        public static Task<LuceneIndexBuilder> CreateRAMIndexAsync(
            Analyzer analyzer, 
            string rootPath, 
            string targetDir, 
            IProgress<ProgressReport> progress) {
            return Task.Run<LuceneIndexBuilder>(() => DoWorkMulti(analyzer, rootPath, targetDir, progress));
        }
        

        private BlockingCollection<RAMDirectory> _que = new BlockingCollection<RAMDirectory>(10000);
        private static LuceneIndexBuilder DoWorkMulti(
            Analyzer analyzer, 
            string rootPath, 
            string targetDir, 
            IProgress<ProgressReport> progress) {

            DeleteOldIndexes(rootPath);

            //
            var instance = new LuceneIndexBuilder();
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="rootPath"></param>
        /// <param name="analyzer"></param>
        internal void WriteFromQueue(IProgress<ProgressReport> progress, string rootPath, Analyzer analyzer) {
            while (true) {
                if (_que.Count > 0) {
                    AppObject.Logger.Info(_que + "Before Queue Count:" + _que.Count);
                    //キューにあるRAMDirectoryをFSDirectoryとして保存
                    RAMDirectory ram = null;
                    _que.TryTake(out ram, 10000);
                    SaveFSIndexFromRAMIndex(ram, rootPath + BuildDirName, analyzer);
                    AppObject.Logger.Info(_que + "After Queue Count:" + _que.Count);
                } else {
                    Thread.Sleep(1000);
                }
                //終了条件判定
                if (_que.Count == 0 && (FinishedCount) == _targetCount) {
                    AppObject.Logger.Info("FSDirectory completed.");
                    break;
                }
            }
        }
        /// <summary>
        /// RAMDirectoryとしてインデックスを構築してQueueに追加
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="analyzer"></param>
        /// <param name="targetFileList"></param>
        /// <param name="threadName"></param>
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
                if (count % 300 == 0) {
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

                //Queueが10件以上になる場合は待ち
                while (true) {
                    if (_que.Count <= 10) {
                        break;
                    }
                    AppObject.Logger.Info("Queue is full. Queue Count:" + _que.Count);
                    Thread.Sleep(3000);
                }

                var ram = new RAMDirectory();
                indexWriter = new IndexWriter(ram, config);

    			foreach (FileInfo fi in list) {
    				//Officeのテンポラリファイルは無視。
                    if (fi.Name.StartsWith("~")) {
    					continue;
                    }
    				try {
    					AddDocument(fi.FullName, indexWriter, threadName, null);

    					//インデックス作成ファイル表示
    					Interlocked.Increment(ref _indexedCount);
    					Interlocked.Exchange(ref _totalBytes, _totalBytes + fi.Length);

                        AppObject.Logger.Info(threadName + ":" + fi.FullName);
    				} catch (Exception e) {
    					//インデックスが作成できなかったファイルを表示
                        AppObject.Logger.Error(e.Message);
    					Interlocked.Increment(ref _skippedCount);
                        AppObject.Logger.Info(threadName + ":" + "Skipped: " + fi.FullName);
    				} finally {
                    }

                    //進捗度更新を呼び出し。
                    progress.Report(new ProgressReport() {
                        Percent = GetPercentage(),
                        ProgressCount = FinishedCount,
                        TargetCount = _targetCount,
                        Status = ProgressReport.ProgressStatus.Processing
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
        #endregion Multi RAMDirectory
    }
}