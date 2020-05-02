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
        /// Rows.Countプロパティ
        /// </summary>
        /// <returns></returns>
        public int RowsCount {
            get { return (int)_core["Rows"]()["Count"]().Core; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="src"></param>
        public FwFlexGridEx(AppVar src) {
            _core = src;
        }

        /// <summary>
        /// Selectメソッド
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void Select(int row, int col) {
            _core["Select"](row, col);
        }

        /// <summary>
        /// インデクサ
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