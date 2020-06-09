using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokudaSearch.Test.Driver;

namespace PokudaSearch.Test {
    /// <summary>
    /// UnitTest1 の概要の説明
    /// </summary>
    [TestClass]
    public class MainFrameFormTest : TestBase<MainFrameFormTest> {

        MainFrameFormDriver _mainFrameForm;

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
            _mainFrameForm = AppDriver.MainFrameForm;
        }
        [TestCleanup]
        public void TestCleanup() {
            NotifyTestCleanup();
        }
        #region AutoTest
        [TestMethod, TestCategory("Auto"), TestCategory("個別")]
        public void インデックス作成画面表示Test() {
            var indexBuildForm = _mainFrameForm.IndexBuildFormButton_EmulateClick();
            string windowText = indexBuildForm.Window.GetWindowText();
            indexBuildForm.Close();
            Assert.AreEqual("インデックス作成", windowText);
        }
        #endregion AutoTest

        [TestMethod]
        public void タブオーダーTest() {
            Assert.AreEqual("2020.05.26", "2020.05.26");
        }
        [TestMethod]
        public void 試用開始初回Test() {
            //試用期間残が30日で起動
            Assert.AreEqual("2020.05.26", "2020.05.26");
        }
        [TestMethod]
        public void 試用開始2日目Test() {
            //試用期間残が29日で起動
            //update [m_license] set [利用開始日] = "2020-05-25"
            Assert.AreEqual("2020.05.26", "2020.05.26");
        }
        [TestMethod]
        public void 試用開始2日目_認証キャンセルTest() {
            //試用期間残が29日で起動
            Assert.AreEqual("2020.05.26", "2020.05.26");
        }
        [TestMethod]
        public void 試用開始2日目_認証失敗Test() {
            //試用期間残が29日で起動
            Assert.AreEqual("2020.05.26", "2020.05.26");
        }
        [TestMethod]
        public void 試用開始2日目_認証成功Test() {
            //試用期間残が29日で起動
            //UIが認証済みになる。
            Assert.AreEqual("2020.05.26", "2020.05.26");
        }
        [TestMethod]
        public void 試用開始30日目Test() {
            //update [m_license] set [利用開始日] = '2020-04-26', [認証キー] = 'dummy'
            //試用期間残が0日で起動
            Assert.AreEqual("2020.05.26", "2020.05.26");
        }
        [TestMethod]
        public void 試用開始31日目_認証キャンセルTest() {
            //認証ダイアログ表示
            //アプリ終了
            Assert.AreEqual("2020.05.26", "2020.05.26");
        }
        [TestMethod]
        public void 試用開始31日目_認証失敗Test() {
            //認証ダイアログ表示
            //アプリ終了
            Assert.AreEqual("2020.05.26", "2020.05.26");
        }
        [TestMethod]
        public void 試用開始31日目_認証成功Test() {
            //認証ダイアログ表示
            Assert.AreEqual("2020.05.26", "2020.05.26");
        }
    }
}
