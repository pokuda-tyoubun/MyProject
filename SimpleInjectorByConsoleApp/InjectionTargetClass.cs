using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInjectorInConsoleApp {
    /// <summary>
    /// オブジェクト注入対象クラス
    /// </summary>
    public class InjectionTargetClass {
        private readonly IProxy _proxy;
        private readonly Container _container;
        private readonly BarClass _dummy;

        public InjectionTargetClass(IProxy proxy, Container container, BarClass dummy) {
            _proxy = proxy;
            _container = container;
            _dummy = dummy;
        }

        public void Write() {
            _proxy.Write();
            Console.WriteLine("注入コンテナのDefaultLifestyle : " + 
                _container.Options.DefaultLifestyle);
            _dummy.Write();
        }
    }
}
