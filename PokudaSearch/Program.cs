using FlexLucene.Analysis.Ja;
using PokudaSearch.SandBox;
using System;
using System.Collections.Generic;
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
            Application.Run(new MainFrameForm());
        }

        /// <summary>
        /// 初期処理
        /// </summary>
        private static void Initialize() {
            AppObject.RootDirPath = Directory.GetParent(Application.ExecutablePath).FullName;
            AppObject.RootDirPath += Consts.StoreDirName;

            //Analyzer
            //java.io.File f = new java.io.File(AppObject.RootDirPath + @"\..\UserDictionary.txt");
            //java.io.Reader treader = new java.io.FileReader(f);
            //UserDictionary userDic = null;
            //try { 
            //    //ユーザ辞書
            //    userDic = UserDictionary.Open(treader); //out of index exceptionがでる。
            //} finally {
            //    treader.close();    
            //}

            //Analyzer analyzer = new JapaneseAnalyzer(userDic, 
            //    JapaneseTokenizerMode.SEARCH,
            //    JapaneseAnalyzer.GetDefaultStopSet(), 
            //    JapaneseAnalyzer.GetDefaultStopTags());

            AppObject.AppAnalyzer = new JapaneseAnalyzer();
        }
    }
}
