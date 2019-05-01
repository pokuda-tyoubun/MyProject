using FlexLucene.Analysis;
using FlexLucene.Analysis.Ja;
using FlexLucene.Analysis.Ja.Dict;
using PokudaSearch.IndexBuilder;
using PokudaSearch.SandBox;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokudaSearch {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //起動時に初期処理
            Initialize();
            AppObject.Frame = new MainFrameForm();

            Application.Run(AppObject.Frame);
        }

        /// <summary>
        /// 初期処理
        /// </summary>
        private static void Initialize() {

            //SQLite接続文字列作成
            var builder = new System.Data.SQLite.SQLiteConnectionStringBuilder {
                DataSource= Properties.Settings.Default.SQLITE_DATA_SOURCE,
                Version = 3,
                LegacyFormat = false,
                //PageSize = 8192,
                //CacheSize = 81920,
                SyncMode = SynchronizationModes.Full, //途中で強制的に電源をOFFにすることも考えられるため。
                JournalMode = SQLiteJournalModeEnum.Default
            };
            AppObject.ConnectString = builder.ToString();

            //ユーザ辞書の設定
            AppObject.RootDirPath = Directory.GetParent(Application.ExecutablePath).FullName;
            AppObject.RootDirPath += LuceneIndexBuilder.StoreDirName;

            //Analyzer
            java.io.Reader treader = new java.io.FileReader(AppObject.RootDirPath + @".\..\UserDictionary.txt");
            UserDictionary userDic = null;
            try {
                //ユーザ辞書
                userDic = UserDictionary.Open(treader);
            } finally {
                treader.close();    
            }

            AppObject.AppAnalyzer = new JapaneseAnalyzer(userDic, 
                JapaneseTokenizerMode.SEARCH,
                JapaneseAnalyzer.GetDefaultStopSet(), 
                JapaneseAnalyzer.GetDefaultStopTags());

            //AppObject.AppAnalyzer = new JapaneseAnalyzer();
        }
    }
}
