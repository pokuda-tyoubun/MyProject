using FxCommonLib.Models;
using FxCommonLib.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagWriter {
    public static class AppObject {
        #region Constants
        #endregion Constants

        #region Properties

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
        #endregion Properties

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

        /// <summary>
        /// アプリのバージョン情報取得
        /// </summary>
        /// <returns></returns>
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


        public static void ReportProgress(BackgroundWorker bw, int molecule, int denominator, string msg) {
            //プログレスバー更新
            int p = (int)(((double)molecule / (double)denominator) * 100);
            bw.ReportProgress(p, molecule.ToString() + "/" + denominator.ToString() + " " + msg);
        }
    }
}
