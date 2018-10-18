using IndexBuilder.Demo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace IndexBuilder {
    class Program {
        /// <summary>
        /// メイン
        /// ※.NetFramework4.0でビルドする必要あり。（4.5だとPDFファイルのみプロセス終了まで解放されない現象が発生するため。）
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(string[] args) {
            LuceneIndexBuilder lib = new LuceneIndexBuilder();

            if (args.Length == 1 && args[0] == "/DEMO") {
                DemoForm df = new DemoForm();
                Application.Run(df);

                return;
            }
#if DEBUG
            string rootDir = @"C:\Temp";
            string subDir = @"\Sub";
            string buildDir = @"\Build";
            string targetDir = @"\テストデータ";
#else
            if (args.Length != 4) {
                string msg = "ArgCount:" + args.Length.ToString() + " Useage:<RootPath><SubDir><BuildDir><TargetDir>";
                throw new ArgumentException(msg);
            }
            string rootDir = args[0];
            string subDir = args[1];
            string buildDir = args[2];
            string targetDir = args[3];
#endif
            lib.CreateIndex(rootDir, subDir, buildDir, targetDir);
#if DEBUG
            //コマンドプロンプトが閉じるのを防ぐため
            Console.ReadLine();
#endif
        }
    }
}
