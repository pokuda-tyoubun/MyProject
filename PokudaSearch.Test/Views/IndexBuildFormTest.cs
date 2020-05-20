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
    public class IndexBuildFormTest : TestBase<IndexBuildFormTest> {

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
        public void 存在しないフォルダを指定Test() {
            _indexBuildForm.TargetDirText.SetWindowText(@"C:\Temp2");

            Async async = new Async();
            _indexBuildForm.UpdateIndexButton.EmulateClick(async);
            string retMsg = FriendlyUtil.GetMsgBoxMessage(_indexBuildForm.Window, async);

            Assert.AreEqual(retMsg, AppDriver.GetMsg("ERR_DIR_NOT_FOUND"));
        }
        [TestMethod]
        public void インデックス新規作成_0件Test() {
            const string testPath = @"C:\Workspace\Repo\Git\MyProject\PokudaSearch.Test\TestData\Zero";
            _indexBuildForm.TargetDirText.SetWindowText(testPath);
            //参照ボタン
            Async async = new Async();
            _indexBuildForm.ReferenceButton.EmulateClick(async);
            var dirDlg = _indexBuildForm.Window.WaitForNextModal();
            string title = dirDlg.GetWindowText();
            Assert.AreEqual(title, "フォルダーの参照");

            //OKボタンクリック
            var okButton = dirDlg.IdentifyFromWindowText("OK");
            okButton.SendMessage(FriendlyUtil.BM_CLICK, IntPtr.Zero, IntPtr.Zero);
            string dirPath = _indexBuildForm.TargetDirText.GetWindowText();
            Assert.AreEqual(dirPath, testPath);

            //インデックス作成
            Async async2 = new Async();
            _indexBuildForm.UpdateIndexButton.EmulateClick(async2);

            //0件メッセージ
            string retMsg = FriendlyUtil.GetMsgBoxMessage(_indexBuildForm.Window, async2);
            Assert.AreEqual(retMsg, AppDriver.GetMsg("MSG_INDEXED_COUNT_ZERO"));
        }
        [TestMethod]
        public void インデックス新規作成Test() {
            const string testPath = @"C:\Workspace\Repo\Git\MyProject\PokudaSearch.Test\TestData\Normal";
            _indexBuildForm.TargetDirText.SetWindowText(testPath);

            //インデックス作成
            Async async = new Async();
            //完了するまで待機
            try {
                _indexBuildForm.UpdateIndexButton.EmulateClick(async);

                while (true) {
                    Thread.Sleep(1000);
                    string val = _indexBuildForm.ProgressBar.AppVar["Value"]().Core.ToString();
                    //進捗100%
                    if (val == "100") {
                        Assert.AreEqual(true, true);
                        break;
                    }
                }
            } finally {
                async.WaitForCompletion();
            }
        }
        [TestMethod]
        public void 外部インデックス追加Test() {
            const string testPath = @"S:\PokudaSearch v1.0.0.3\bin\DB";
            _indexBuildForm.TargetDirText.SetWindowText(testPath);

            //インデックス作成
            Async async = new Async();
            //完了するまで待機
            try {
                _indexBuildForm.AddOuterIndexButton.EmulateClick(async);

                var dirDlg = _indexBuildForm.Window.WaitForNextModal();

            } finally {
                async.WaitForCompletion();
            }
        }
        [TestMethod]
        public void 外部インデックスに繋がらない場合Test() {
        }
        [TestMethod]
        public void サーバ側で外部インデックスが削除された場合Test() {
        }
        [TestMethod]
        public void インデックスMax20Test() {
            //TODO
        }

        [TestMethod]
        public void インデックス作成に失敗した場合Test() {
            //FIXME
        }
        [TestMethod]
        public void タブオーダーTest() {
            Assert.AreEqual(true, true);
        }
    }
}
