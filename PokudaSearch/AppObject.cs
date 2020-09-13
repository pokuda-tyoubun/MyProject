using FlexLucene.Analysis;
using FlexLucene.Analysis.Ja;
using FxCommonLib.Models;
using FxCommonLib.Utils;
using log4net;
using PokudaSearch.Controls;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TikaOnDotNet.TextExtraction;

namespace PokudaSearch {

    public static class AppObject {

        #region Constants
        public const int TrialPeriod = 30;
        public const string LicenseKey = "POKUDA-879B4C51-8B48-4EB6-AB0D-4A89AD1DA5D2";
        #endregion Constants

        #region Properties
        /// <summary>ロガー</summary>
        private static ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static ILog Logger {
            get { return _logger; }
        }

        public static bool AlreadyLoadedLogviewer = false;
        public static bool IsTrial = true;
        public static int RemainingDays = 0;

        public static string DefaultPath = "";
        public static BootModes BootMode = BootModes.Filer;

        /// <summary>PokudaSearch.dbへの接続文字列</summary>
        public static string ConnectString { get; set; }

        /// <summary>SQLiteユーティリティ</summary>
        private static SQLiteDBUtil _db = new SQLiteDBUtil(AppObject.Logger);
        public static SQLiteDBUtil DbUtil {
            get { return _db; }
        }

        /// <summary>メインフレーム画面のインスタンス</summary>
        public static MainFrameForm Frame { get; set; }

        /// <summary>Luceneインデックス群配置パス</summary>
        public static string RootDirPath = @"";

        public static Analyzer AppAnalyzer = null;

        /// <summary>メッセージリソース</summary>
        private static MultiLangUtil _mlu = new MultiLangUtil();
        public static MultiLangUtil MLUtil {
            get { return _mlu; }
        }
        /// <summary>フィルターヘルパ</summary>
        private static FilterHelper _filterHelper = new FilterHelper();
        public static FilterHelper FilterHelper {
            get { return _filterHelper; }
        }
        private static CefSharpPanel _cefSharpPanel = new CefSharpPanel();
        public static CefSharpPanel CefSharpPanel {
            get { return _cefSharpPanel; }
        }
        #endregion Properties

        /// <summary>
        /// 起動モード
        /// </summary>
        public enum BootModes : int {
            //ファイラを起動
            Filer = 0,
            //ファイラを起動（多重起動しない）
            SingletonFiler,
            //インデックス更新モード
            UpdateIndex,
        }

        #region Message
        public static string GetMsg(Msg msgId) {
            string key = EnumUtil.GetName(msgId);
            return _mlu.GetMsg(key);
        }
        public enum Msg : int {
            [EnumLabel("抽出中...")]
            ACT_EXTRACT = 0,
            [EnumLabel("検索中…")]
            ACT_SEARCH,
            [EnumLabel("処理中 ...")]
            ACT_PROCESSING,
            [EnumLabel("絞込中 ...")]
            ACT_FILTER,
            [EnumLabel("絞込を解除中 ...")]
            ACT_RESET_FILTER,
            [EnumLabel("")]
            ACT_END,
            [EnumLabel("質問")]
            TITLE_QUESTION,
            [EnumLabel("情報")]
            TITLE_INFO,
            [EnumLabel("警告")]
            TITLE_WARN,
            [EnumLabel("エラー")]
            TITLE_ERROR,
            [EnumLabel("抽出対象データがありません。")]
            MSG_EXTRACT_ZERO,
            [EnumLabel("Excelのバージョンが2003以前なので、257列以降は切り捨てます。")]
            MSG_EXCEL2003_DATA_TRUNCATE,
            [EnumLabel("背景色設定中…")]
            MSG_COLORING_BACKCOLOR,
            [EnumLabel("処理を中断しますか？")]
            MSG_DO_STOP,
            [EnumLabel("インデックスが作成されていません。今すぐインデックスを作成しますか？")]
            MSG_DO_CREATE_INDEX,
            [EnumLabel("インデックス対象が0件でした。")]
            MSG_INDEXED_COUNT_ZERO,
            [EnumLabel("キーワードを入力して下さい。")]
            MSG_INPUT_KEYWORD,
            [EnumLabel("検索対象インデックスを選択して下さい。")]
            MSG_CHECKON_TARGET_INDEX,
            [EnumLabel("指定されたフォルダは存在しません。")]
            ERR_DIR_NOT_FOUND,
            [EnumLabel("ライセンスキーが認証されました。ご購入ありがとうございました。")]
            MSG_LICENSE_VERIFIED,
            [EnumLabel("ファイルが存在しません。")]
            ERR_FILE_NOT_FOUND,
            [EnumLabel("{0}ファイルが存在しません。")]
            ERR_X_FILE_NOT_FOUND,
            [EnumLabel("インデックス数が上限に達しました。これ以上インデックスを追加することはできません。")]
            MSG_INDEX_COUNT_MAX,
            [EnumLabel("指定されたローカルフォルダは存在しません。")]
            ERR_LOCALDIR_NOT_FOUND,
            [EnumLabel("「PokudaSearch.db」ファイルが存在しません。")]
            ERR_DBFILE_NOT_FOUND,
            [EnumLabel("無効なパスです。")]
            ERR_INVALID_PATH,
            [EnumLabel("Microsoft Wordがインストールされていません。")]
            ERR_UNINSTALLED_MS_WORD,
            [EnumLabel("Microsoft Excelがインストールされていません。")]
            ERR_UNINSTALLED_MS_XLS,
            [EnumLabel("Microsoft PowerPointがインストールされていません。")]
            ERR_UNINSTALLED_MS_PPT,
            [EnumLabel("情報を取得できませんでした。")]
            ERR_CANNOT_GET_INFO,
            [EnumLabel("[{0}]へのインデックスが参照できないため検索を中断します。")]
            ERR_UNLINKED_INDEX,
            [EnumLabel("ライセンスキーを認証できませんでした。\nライセンスキーをご確認のうえ、再度入力してください。")]
            ERR_LICENSE_CANNOT_VERIFIED,
            [EnumLabel("バージョン 1.1.0.0 以上で作成された外部インデックスを指定して下さい。")]
            ERR_DIFFERENT_VERSION,
        }
        #endregion Message


        public static string GetVersion() {
            Assembly asm = Assembly.GetEntryAssembly();
            string path = asm.Location;
            FileVersionInfo vi = FileVersionInfo.GetVersionInfo(path);

            return vi.FileVersion; 
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static AppObject() {
            foreach (Msg value in Enum.GetValues(typeof(Msg))) {
                var key = value.ToString();
                var label = EnumUtil.GetLabel(value);
                _mlu.MessageDictionary.Add(key, label);
            }
        }

        public static string GetLabel(Msg msgId) {
            return EnumUtil.GetLabel(msgId);
        }

        //TODO 配置場所を要検討
        /// <summary>
        /// ファイルの中身をテキスト抽出して内容が同じか判定する
        /// </summary>
        /// <param name="filePath1"></param>
        /// <param name="filePath2"></param>
        /// <returns></returns>
        public static bool IsSameContents(string filePath1, string filePath2) {
            bool ret = false;

            //拡張子が同じか確認
            string ext1 = Path.GetExtension(filePath1);
            string ext2 = Path.GetExtension(filePath2);
            if (ext1 != ext2) {
                return false;
            }

            string txt1 = "";
            string txt2 = "";
            if (ext1.ToLower() == ".txt") {
                using (var reader = new StreamReader(filePath1)) {
                    txt1 = reader.ReadToEnd();
                }
                using (var reader = new StreamReader(filePath2)) {
                    txt2 = reader.ReadToEnd();
                }
                
            } else {
                var txtExtractor = new TextExtractor();

                var content1 = txtExtractor.Extract(filePath1);
                txt1 = content1.Text;
                var content2 = txtExtractor.Extract(filePath2);
                txt2 = content2.Text;
            }

            if (txt1 == txt2) {
                ret = true;
            }

            return ret;
        }

        public static string GetConnectString(string sqliteDataSource) {

            var builder = new System.Data.SQLite.SQLiteConnectionStringBuilder {
                DataSource = sqliteDataSource,
                Version = 3,
                LegacyFormat = false,
                //PageSize = 8192,
                //CacheSize = 81920,
                SyncMode = SynchronizationModes.Full, //途中で強制的に電源をOFFにすることも考えられるため。
                JournalMode = SQLiteJournalModeEnum.Default
            };
            return builder.ToString();
        }
        //[TestMethod]
        //public void IsSameContentsTest() {
        //    bool ret = false;
        //    var txtExtractor = new TextExtractor();

        //    //テキストファイルでの比較
        //    //NOTE テキストファイルの中身がに日本語が入ると抽出できていない
        //    // ⇒ReadToEndで文字列を取得するようにした
        //    ret = FileUtil.IsSameContents(@".\TestData\Sample.txt", @".\TestData\SampleCopy.txt");
        //    Assert.AreEqual(ret, true);

        //    ret = FileUtil.IsSameContents(@".\TestData\Sample.txt", @".\TestData\SampleDiff.txt");
        //    Assert.AreEqual(ret, false);

        //    // エクセルでの比較
        //    ret = FileUtil.IsSameContents(@".\TestData\Sample.xlsx", @".\TestData\SampleCopy.xlsx");
        //    Assert.AreEqual(ret, true);

        //    ret = FileUtil.IsSameContents(@".\TestData\Sample.xlsx", @".\TestData\SampleDiff.xlsx");
        //    Assert.AreEqual(ret, false);

        //    //TODO テキスト抽出できない場合

        //}
    }
}
