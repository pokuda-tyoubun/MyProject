using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInjectorInConsoleApp {
    /// <summary>
    /// 注入クラスFoo
    /// </summary>
    class FooProxy : IProxy {
        public void Write() {
            Console.WriteLine("foo");
        }
    }
}
