using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Ong.Friendly.FormsStandardControls;
using System.Diagnostics;
using System.Threading;
using System.Drawing;

namespace Codeer.Friendly.CCC.Util {
    public class FwC1Ribbon {
        AppVar _core;
 
        /// <summary>
        /// Controlsプロパティ
        /// </summary>
        /// <returns></returns>
        public dynamic Controls {
            get { return (dynamic)_core["Controls"]().Core; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="src"></param>
        public FwC1Ribbon(AppVar src) {
            _core = src;
        }

        public void ControlMouseClick(string ctlName) {
            _core["Controls"]()[ctlName]()["MouseClick"]();
        }

        public int ControlsCount() {
            return (int)_core["Controls"]()["Count"]().Core;
        }

        public string ControlName(int index) {
            return (string)_core["Controls"]()[index.ToString()]()["Name"]().Core;
        }

        public void ShowControls() {
            for (int i = 0; i < ControlsCount(); i++) {
                Debug.Print(ControlName(i));
            }
        }
    }
}
