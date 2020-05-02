using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Ong.Friendly.FormsStandardControls;
using System.Diagnostics;
using System.Threading;

namespace Codeer.Friendly.CCC.Util {
    public class FwC1NumericEdit {
        AppVar _core;
 
        /// <summary>
        /// Text�v���p�e�B
        /// </summary>
        /// <returns></returns>
        public string Text {
            get { return (string)_core["Text"]().Core; }
            set { _core["Text"](value); }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="src"></param>
        public FwC1NumericEdit(AppVar src) {
            _core = src;
        }
    }
}
