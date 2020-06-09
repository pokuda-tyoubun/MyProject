using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlexLucene.Queryparser.Surround.Parser;
using FlexLucene.Queryparser.Classic;
using FlexLucene.Analysis.Ja;
using System.IO;
using PokudaSearch.IndexUtil;
using FlexLucene.Analysis.Ja.Dict;
using FlexLucene.Search;
using java.nio.file;
using FlexLucene.Store;
using FlexLucene.Index;
using System.Diagnostics;
using FlexLucene.Document;
using PokudaSearch.Test.Views.Driver;
using PokudaSearch.Test.Driver;
using Codeer.Friendly;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.CCC.Util;
using FxCommonLib.Utils;
using System.Threading;

namespace PokudaSearch.Test.Views {
    [TestClass]
    public class ConfigFormTest : TestBase<IndexBuildFormTest> {


        [ClassInitialize]
        public static void ClassInitialize(TestContext c) {
            NotifyClassInitialize();
        }

        [ClassCleanup]
        public static void ClassCleanup() {
            NotifyClassCleanup();
        }

        [TestInitialize]
        public void TestInitialize() {
            NotifyTestInitialize();
        }

        [TestCleanup]
        public void TestCleanup() {
            NotifyTestCleanup();
        }

        #region ManualTest
        [TestMethod, TestCategory("Manual")]
        public void 関係のない差分ツールのパスを指定した場合Test() {
        }
        [TestMethod, TestCategory("Manual")]
        public void 差分ツールのパスを保持Test() {
            Assert.AreEqual("2020.05.27", "2020.05.27");
        }
        [TestMethod, TestCategory("Manual")]
        public void 検索結果最大件数保持Test() {
            Assert.AreEqual("2020.05.27", "2020.05.27");
        }
        [TestMethod, TestCategory("Manual")]
        public void デフォルト対象ローカルインデックスTest() {
            //値の保持
            //初回起動時の反映
        }
        [TestMethod, TestCategory("Manual")]
        public void デフォルト対象外部インデックスTest() {
            //値の保持
            //初回起動時の反映
        }
        //------------------------------------------------------------------------------
        [TestMethod, TestCategory("Manual"), TestCategory("共通")]
        public void 全てのスプリッタをドラッグTest() {
            Assert.AreEqual("2020.05.27", "2020.05.27");
        }
        [TestMethod, TestCategory("Manual"), TestCategory("共通")]
        public void 全てのスプリッタを折り畳むTest() {
            Assert.AreEqual("2020.05.27", "2020.05.27");
        }
        [TestMethod, TestCategory("Manual"), TestCategory("共通")]
        public void ウィンドウのサイズ変更Test() {
            //変更不可
            Assert.AreEqual("2020.05.27", "2020.05.27");
        }
        [TestMethod, TestCategory("Manual"), TestCategory("共通")]
        public void タブオーダーTest() {
            Assert.AreEqual(true, true);
        }
        #endregion ManualTest

    }
}
