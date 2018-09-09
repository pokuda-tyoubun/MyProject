using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInjectorInConsoleApp {
    class Program {

        private static Container container;

        static void Main(string[] args) {
            
            container = new Container();

            //デフォルトのLifestyleを確認
            Console.WriteLine("デフォルトのLifestyle : " + container.Options.DefaultLifestyle.ToString());

            container.Register<IProxy, HogeProxy>(Lifestyle.Singleton);
            //登録をFooProxyに切替えると、利用側のクラスを変更せずに処理を切り替えれます。
            //container.Register<IProxy, FooProxy>(Lifestyle.Singleton);

            //コンストラクタにRegister()していないTypeの引数を渡す方法
            container.Register<BarClass>(() => new BarClass("bar"), Lifestyle.Singleton);

            //Waning抑制の書き方
            //Registration registration = _container.GetRegistration(typeof(IReportWriter)).Registration;
            //registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "reason : see comment");
            //registration = _container.GetRegistration(typeof(OrderForm)).Registration;
            //registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "reason : see comment");

            container.Verify();

            // Useコンテナに登録していないが、コンストラクタインジェクションを行って必要な
            // オブジェクトを設定してインスタンスを作成してくれる。 (auto-wiring)
            var a = container.GetInstance<InjectionTargetClass>();
            a.Write();

            //var b = new BarClass("dummy");
            //b.Write();

            //これはException
            //var c = container.GetInstance<DummyClass>();
            //c.Write();

            //自動でコンソールが閉じるの防ぐ
            Console.ReadLine();
        }
    }
}
