using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Ong.Friendly.FormsStandardControls;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.CCC.Util {

    public class FwC1RibbonButton {

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        AppVar _core;
 
        /// <summary>
        /// Textプロパティ
        /// </summary>
        /// <returns></returns>
        public string Text {
            get { return (string)_core["Text"]().Core; }
            set { _core["Text"](value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="src"></param>
        public FwC1RibbonButton(AppVar src) {
            _core = src;
        }

        public IntPtr Handle {
            get { return (IntPtr)_core["Handle"]().Core;}
        }

        public void EmulateClick() {
            _core["OnClick"](EventArgs.Empty);
        }

        public void EmulateClick(Async async) {
            _core["OnClick", async](EventArgs.Empty);
        }
    }
}
