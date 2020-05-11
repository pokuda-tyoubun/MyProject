using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokudaSearch.Test.Driver {
    public static class AppDriver {
        public static WindowsAppFriend App;
        public static MainFrameFormDriver MainFrameForm { get; set; }
        public static Killer Killer { get; private set; }
        private static Process _process = null;

        /// <summary>
        /// プロセスを起動し、アタッチする。
        /// </summary>
        public static void Attach() {
            if (_process != null && !_process.Responding) {
                //NOTE:この場合、一旦終了しないと後続の処理で例外が発生する
                _process.Kill();
                _process = null;
            }
            if (_process == null) {
                _process = Process.Start(@"..\..\..\PokudaSearch\bin\Debug\PokudaSearch.exe");
                App = new WindowsAppFriend(_process, clrVersion:"4.0");
                MainFrameForm = new MainFrameFormDriver(WindowControl.FromZTop(App));
            }
            App = new WindowsAppFriend(_process);

            //アプリを初期状態に戻す
            InitApp();

            //HACK:何秒が適当か検討する
            //10秒固まったらキル
            Killer = new Killer(10000, _process.Id);
        }

        /// <summary>
        /// アタッチを解放
        /// </summary>
        /// <param name="isContinue"></param>
        public static void Release(bool isContinue) {
            if (isContinue) {
                Killer.Finish();
                App.Dispose();
            } else {
                EndProcess();
            }
        }

        private static void InitApp() {
            System.Console.WriteLine(WindowControl.FromZTop(App).TypeFullName);
            if (WindowControl.FromZTop(App).GetWindowText().Contains("Pokuda")) {
                AppDriver.MainFrameForm = new MainFrameFormDriver(WindowControl.FromZTop(AppDriver.App));
            }
        }

        public static void EndProcess() {
            try {
                Killer.Finish();
            } catch { }
            try {
                _process.Kill();
            } catch { }
            _process = null;
        }

        /// <summary>
        /// モーダルウィンドウを取得する
        /// </summary>
        /// <returns></returns>
        public static WindowControl GetMordalWindow() {
            return MainFrameForm.Window.WaitForNextModal();
        }
    }
}
