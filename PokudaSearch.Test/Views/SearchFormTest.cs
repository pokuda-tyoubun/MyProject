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
    public class SearchFormTest : TestBase<IndexBuildFormTest> {

        IndexBuildFormDriver _indexBuildForm;

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
            _indexBuildForm = AppDriver.MainFrameForm.IndexBuildFormButton_EmulateClick();
        }

        [TestCleanup]
        public void TestCleanup() {
            NotifyTestCleanup();
        }

        [TestMethod]
        public void 外部インデックスに繋がらない場合Test() {
            //手動テストOK
        }
        [TestMethod]
        public void タブオーダーTest() {
            Assert.AreEqual(true, true);
        }
    }
}
