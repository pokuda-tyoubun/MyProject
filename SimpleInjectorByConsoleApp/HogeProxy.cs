using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInjectorInConsoleApp {
    /// <summary>
    /// 注入クラスHoge
    /// </summary>
    class HogeProxy : IProxy {
        public void Write() {
            Console.WriteLine("hoge");
        }
    }
}
