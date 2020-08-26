using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.Controls {
    interface ICodeTextBoxEx {

        /// <summary>
        /// コードチェックメソッド
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        bool CheckCode(out DataTable dt);
    }
}
