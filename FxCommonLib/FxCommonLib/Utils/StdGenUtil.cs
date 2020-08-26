using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.Utils {
    /// <summary>
    /// C++のSTLで扱っているテンプレートメソッドなどの機能を提供
    /// </summary>
    public class StdGenUtil {
        /// <summary>
        /// オブジェクトを入れ替えます。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static void Swap<T>(ref T lhs, ref T rhs) {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
    //参考:https://docs.microsoft.com/ja-jp/dotnet/csharp/programming-guide/generics/generic-methods
    //http://kaworu.jpn.input/cpp/C%2B%2B%E3%83%A9%E3%82%A4%E3%83%96%E3%83%A9%E3%83%AA
}
