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
    public class FwFlexGridEx {
        AppVar _core;
 
        /// <summary>
        /// Rows.Count�v���p�e�B
        /// </summary>
        /// <returns></returns>
        public int RowsCount {
            get { return (int)_core["Rows"]()["Count"]().Core; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="src"></param>
        public FwFlexGridEx(AppVar src) {
            _core = src;
        }

        /// <summary>
        /// Select���\�b�h
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void Select(int row, int col) {
            _core["Select"](row, col);
        }

        /// <summary>
        /// �C���f�N�T
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public object this[int row, int col] {
            set { _core.Dynamic()[row, col] = value; }
            get { return _core.Dynamic()[row, col]; }
        }
    }
}