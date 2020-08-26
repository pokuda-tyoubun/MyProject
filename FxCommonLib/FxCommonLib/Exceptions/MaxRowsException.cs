using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.Exceptions {
    /// <summary>
    /// Select文実行時に閾値より多くのレコードが返ってきた場合
    /// </summary>
    public class MaxRowsException : Exception {
        public MaxRowsException(string msg) : base(msg) {}
    }
}
