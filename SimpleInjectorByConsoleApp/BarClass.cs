using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInjectorInConsoleApp {
    /// <summary>
    /// 注入クラスBar
    /// ※ IProxyを実装していない
    /// </summary>
    public class BarClass {
        private string val = "";
        public BarClass(string str) {
            val = str; 
        }

        public void Write() {
            Console.Write(val);
        }
    }
}
