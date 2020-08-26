using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FxCommonLib.Utils;

namespace FxCommonLib.Tests.Utils {
    [TestClass]
    public class StringUtilTest {
        [TestMethod]
        public void NullToBlankTest() {
            //想定結果：上記メールを受信
            //備考：エビデンス不要
            //確認者：橋本, 確認日：2018/8/13
            Assert.AreEqual("test", StringUtil.NullToBlank("test  "));
            Assert.AreEqual("test", StringUtil.NullToBlank("test\r\n"));
            Assert.AreEqual("\r\ntest", StringUtil.NullToBlank("\r\ntest"));

        }
        [TestMethod]
        public void RemoveZeroPaddingTest() {
            Assert.AreEqual("test", StringUtil.RemoveZeroPadding("test"));
            Assert.AreEqual("39", StringUtil.RemoveZeroPadding("00039"));
            Assert.AreEqual("ab", StringUtil.RemoveZeroPadding("000ab"));
            Assert.AreEqual("0", StringUtil.RemoveZeroPadding("000"));
            Assert.AreEqual("0.00", StringUtil.RemoveZeroPadding("0.00"));
            Assert.AreEqual("0.00", StringUtil.RemoveZeroPadding("000.00"));
            Assert.AreEqual("", StringUtil.RemoveZeroPadding(""));

        }
        [TestMethod]
        public void LeftTest() {
            Assert.AreEqual("1234", StringUtil.Left("12345", 4));
            Assert.AreEqual("12345", StringUtil.Left("12345", 5));
            Assert.AreEqual("12345", StringUtil.Left("12345", 6));
            Assert.AreEqual("", StringUtil.Left("", 6));
            Assert.AreEqual("", StringUtil.Left(null, 6));

        }
        [TestMethod]
        public void MultiValToDic() {
            //1件
            var dic = StringUtil.MultiValToDic("2019/7/12/登録日");
            Assert.AreEqual("2019/7/12", dic["登録日"]);

            //2件
            dic = StringUtil.MultiValToDic("2019/7/12/登録日;99/P番号");
            Assert.AreEqual("2019/7/12", dic["登録日"]);
            Assert.AreEqual("99", dic["P番号"]);

        }
        [TestMethod]
        public void MultiValToTable() {
            //HACK select句でKeyをselectしてテスト
            //1件
            var tbl = StringUtil.MultiValToTable("2019/7/12/登録日");
            Assert.AreEqual("2019/7/12", tbl.Rows[0]["value"]);

            //2件
            tbl = StringUtil.MultiValToTable("2019/7/12/登録日;99/P番号");
            Assert.AreEqual("2019/7/12", tbl.Rows[0]["value"]);
            Assert.AreEqual("99", tbl.Rows[1]["value"]);

        }
        //FIXME 他のメソッドのテストコードも作成
    }
}
