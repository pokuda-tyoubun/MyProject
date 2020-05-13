using FlexLucene.Analysis;
using FlexLucene.Analysis.Ja;
using FlexLucene.Analysis.Ja.Dict;
using PokudaSearch.IndexUtil;
using PokudaSearch.SandBox;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
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

            //x86,x64 dllの切替処理
            AppDomain.CurrentDomain.AssemblyResolve += Resolver;

            //起動時に初期処理
            Initialize();
            AppObject.Frame = new MainFrameForm();

            Application.Run(AppObject.Frame);
        }

        /// <summary>
        /// 初期処理
        /// </summary>
        private static void Initialize() {

            string sqliteDataSource = Directory.GetParent(Application.ExecutablePath).FullName + 
                                            Properties.Settings.Default.SQLITE_DATA_SOURCE;
            AppObject.ConnectString = AppObject.GetConnectString(sqliteDataSource);

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

            AppObject.AppAnalyzer = new JapaneseAnalyzer(userDic, //ユーザ定義辞書
                JapaneseTokenizerMode.SEARCH,
                JapaneseAnalyzer.GetDefaultStopSet(), 
                JapaneseAnalyzer.GetDefaultStopTags());
            //JapaneseTokenizerMode.EXTENDED;
            // ->拡張モードは、未知の単語のユニグラムを出力します。
            //JapaneseTokenizerMode.NORMAL;
            // ->通常のセグメンテーション：化合物の分解なし
            //JapaneseTokenizerMode.SEARCH;
            // ->検索を対象としたセグメンテーション：
            //   これには、長い名詞の複合化プロセスが含まれ、同義語としての完全な複合トークンも含まれます。

            //AppObject.AppAnalyzer = new JapaneseAnalyzer();
        }
        
        private static Assembly Resolver(object sender, ResolveEventArgs args) {
            if (args.Name.StartsWith("CefSharp")) {
                string assemblyName = args.Name.Split(new[] { ',' }, 2)[0] + ".dll";
                string path = Directory.GetParent(Application.ExecutablePath).FullName;
                string archSpecificPath = Path.Combine(path,
                                                       Environment.Is64BitProcess ? "x64" : "x86",
                                                       assemblyName);
                AppObject.Logger.Info("CefSharp path:" + archSpecificPath);
                //string archSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                //                                       Environment.Is64BitProcess ? "x64" : "x86",
                //                                       assemblyName);

                return File.Exists(archSpecificPath) ? Assembly.LoadFile(archSpecificPath) : null;
            }
            return null;
        }
    }
}
