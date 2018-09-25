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
        }
    }
}
