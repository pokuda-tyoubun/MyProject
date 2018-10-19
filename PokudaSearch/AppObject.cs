using FlexLucene.Analysis;
using FlexLucene.Analysis.Ja;
using FxCommonLib.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokudaSearch {
    public static class AppObject {
        //HACK ログの出力先をexeの側に変更(ファイルの設定は、デフォルトに？)

        /// <summary>ロガー</summary>
        private static ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static ILog Logger {
            get { return _logger; }
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

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static AppObject() {
            //HACK Settingsファイルに移行
            _mlu.MessageDictionary.Add("ACT_EXTRACT", "抽出中...");
            _mlu.MessageDictionary.Add("ACT_SEARCH", "検索中…");
            _mlu.MessageDictionary.Add("ACT_PROCESSING", "処理中 ...");
            _mlu.MessageDictionary.Add("ACT_END", "");
            _mlu.MessageDictionary.Add("MSG_EXTRACT_ZERO", "抽出対象データがありません。");
            _mlu.MessageDictionary.Add("TITLE_WARN", "警告");
            _mlu.MessageDictionary.Add("MSG_EXCEL2003_DATA_TRUNCATE", "Excelのバージョンが2003以前なので、257列以降は切り捨てます。");
            _mlu.MessageDictionary.Add("MSG_COLORING_BACKCOLOR", "背景色設定中…");
        }
    }
}
