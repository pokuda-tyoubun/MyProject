using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokudaSearch.Test.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokudaSearch.Test {
    [TestClass]
    public class TestBase<T> {

        //FIXME 要調査
        private static Dictionary<string, bool> _tests;

        public TestContext TestContext { get; set; }

        /// <summary>
        /// 初期化
        /// </summary>
        public static void NotifyClassInitialize() {
            _tests = typeof(T).GetMethods()
                              .Where(e => 0 < e.GetCustomAttributes(typeof(TestMethodAttribute), true).Length)
                              .ToDictionary(e => e.Name, e => true);
        }
        /// <summary>
        /// プロセス終了
        /// </summary>
        public static void NotifyClassCleanup() {
            AppDriver.EndProcess();
        }
        /// <summary>
        /// プロセス起動
        /// </summary>
        public void NotifyTestInitialize() {
            AppDriver.Attach();
        }
        /// <summary>
        /// テストCleanup
        /// </summary>
        public void NotifyTestCleanup() {
            if (TestContext.DataRow == null ||
                ReferenceEquals(TestContext.DataRow, TestContext.DataRow.Table.Rows[TestContext.DataRow.Table.Rows.Count - 1])) {
                _tests.Remove(TestContext.TestName);
            }
            AppDriver.Release(TestContext.CurrentTestOutcome == UnitTestOutcome.Passed && 0 < _tests.Count);
        }

        //FIXME 要調査
        public Data GetParam<Data>() where Data : new() {
            Data data = new Data();
            foreach (var e in typeof(Data).GetProperties()) {
                e.GetSetMethod().Invoke(data, new object[] { Convert(e.PropertyType, TestContext.DataRow[e.Name]) });
            }
            return data;
        }
        //FIXME 要調査
        static object Convert(Type type, object obj) {
            string value = obj == null ? string.Empty : obj.ToString();
            if (type == typeof(int)) {
                return int.Parse(value);
            } else if (type == typeof(bool)) {
                return string.Compare(value, true.ToString(), true) == 0;
            } else if (type == typeof(string)) {
                return value;
            }
            throw new NotSupportedException();
        }
    }
}
