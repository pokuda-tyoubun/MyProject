using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrySimpleInjector {
    class Program {
        /// <summary>
        /// 参考：https://devlights.hatenablog.com/entry/2015/03/17/130624
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            var diContainer = new Container();

            //一番シンプルな形
            //.Register<インターフェース, 具象クラス>();
            //Lifestyleを指定しない場合は、Transientとなる。
            //diContainer.Register<IHasName, Data1>(Lifestyle.Transient);
            diContainer.Register<IHasName, Data1>();
            //初期処理の登録
            //IHasNameの初期処理を定義
            diContainer.RegisterInitializer<IHasName>(x => { x.Name = "hello world " + x.GetType().Name; });

            // 初期化処理 (instanceCreator) を付与して登録
            diContainer.Register<Data2>(() => {
                //InstanceCreator
                return new Data2 {
                    //Valueプロパティを初期化
                    Value = "hello world"
                };
            });

            //シングルトンとして登録
            diContainer.RegisterSingleton<Data4>(() => {
                // シングルトンインスタンスをどのように構築するかを
                // 指定したい場合は、このようにFuncを作って中で構築する。
                // 当然中で、コンテナからインスタンスを取得して、それを
                // 利用することも可能。
                //
                // コンテナはData1を作成してくれる
                var a = diContainer.GetInstance<IHasName>();
                // コンテナData2を作成する際に初期化処理を行った上でインスタンスを返してくれる
                var b = diContainer.GetInstance<Data2>();
                // コンテナに登録していないが、コンストラクタインジェクションを行って必要な
                // オブジェクトを設定してインスタンスを作成してくれる。 (auto-wiring)
                var c = diContainer.GetInstance<Data3>();

                return new Data4(a, b, c, "prepreprefix:");
            });

            //既存クラスの登録もできる。
            diContainer.Register<IList<string>>(() => new List<string>());
            diContainer.Register<IDictionary<string, object>>(() => new Dictionary<string, object>());

            //プロパティインジェクション
            diContainer.RegisterInitializer<Data5>(instance => {
                instance.CreationDateTime = DateTime.Now;
            });

            //最新版ではRegisterAllが存在しないようだ。
            //diContainer.Register<IData>(typeof(Data1), typeof(Data2), typeof(Data3));

            diContainer.RegisterSingleton<IDataFactory>(() => {
                return new DataFactory {
                    {"data1", () => diContainer.GetInstance<Data1>()},
                    {"data2", () => diContainer.GetInstance<Data2>()},
                    {"data3", () => diContainer.GetInstance<Data3>()}
                };
            });

            //必須ではないが、登録内容を検証してくれる。
            diContainer.Verify();

            Console.WriteLine("<IHasName>");
            var obj = diContainer.GetInstance<IHasName>();
            Console.WriteLine("インスタンス名:" + obj.GetType().Name);
            Console.WriteLine("インスタンスのNameプロパティ:" + obj.Name);
            Console.WriteLine("");

            Console.WriteLine("<Data2>");
            var obj2 = diContainer.GetInstance<Data2>();
            Console.WriteLine("インスタンス名:" + obj2.GetType().Name);
            Console.WriteLine("インスタンスのValueプロパティ:" + obj2.Value);
            Console.WriteLine("");

            //登録していないが、コンストラクタインジェクションを行ってインスタンス作成してくれる。
            Console.WriteLine("<Data3>");
            var obj3 = diContainer.GetInstance<Data3>();
            Console.WriteLine(obj3.Print());
            Console.WriteLine("");

            Console.WriteLine("<Data4>");
            var obj4 = diContainer.GetInstance<Data4>();
            Console.WriteLine(obj4.Print());
            Console.WriteLine("");

            Console.WriteLine("<Data5>");
            Console.WriteLine(diContainer.GetInstance<Data5>().CreationDateTime);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.WriteLine(diContainer.GetInstance<Data5>().CreationDateTime);
            Console.WriteLine("");

            //var dataInstanceCollection = diContainer.GetAllInstances<IData>();
            //foreach (var item in dataInstanceCollection) {
            //    Console.WriteLine(item.GetType().Name);
            //}

            //キー文字列を使ってインスタンスを取得
            Console.WriteLine("<IDataFactory>");
            var factory = diContainer.GetInstance<IDataFactory>();
            var idata = factory.Create("data3");
            Console.WriteLine(idata.GetType().Name);

            Console.ReadLine();
        }
    }

    internal interface IData {
        // this is marker-interface.
    }

    internal interface IHasName {
        string Name { get; set; }
    }

    internal class Data1 : IHasName, IData {
        public string Name { get; set; }
    }

    internal class Data2 : IData {
        public string Value { get; set; }
    }

    internal class Data3 : IData {
        private readonly IHasName _a;
        private readonly Data2 _b;

        public Data3(IHasName a, Data2 b) {
            _a = a;
            _b = b;
        }

        public string Print() {
            return string.Format("{0} {1}", _a.Name, _b.Value);
        }
    }

    internal class Data4 : IData {
        private readonly IHasName _a;
        private readonly Data2 _b;
        private readonly Data3 _c;
        private readonly string _prefix;

        public Data4(IHasName a, Data2 b, Data3 c, string prefix) {
            _a = a;
            _b = b;
            _c = c;
            _prefix = prefix;
        }

        public string Print() {
            _c.Print();
            return string.Format("{0}{1} {2} ", _prefix, _a.Name, _b.Value);
        }
    }

    internal interface IHasCreationDateTime {
        DateTime CreationDateTime { get; }
    }

    internal class Data5 : IHasCreationDateTime, IData {
        public Data5() {
        }

        public DateTime CreationDateTime { get; internal set; }
    }


    internal interface IDataFactory {
        IData Create(string name);
    }

    internal class DataFactory : Dictionary<string, Func<IData>>, IDataFactory {
        public IData Create(string name) {
            return this[name]();
        }
    }
}
