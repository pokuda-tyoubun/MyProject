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
        public void AutoTestインデックス作成() {
            var indexBuildForm = _mainFrameForm.IndexBuildFormButton_EmulateClick();
            string windowText = indexBuildForm.Window.GetWindowText();
            indexBuildForm.Close();
            Assert.AreEqual("インデックス作成", windowText);
        }
        #endregion AutoTest
    }
}
