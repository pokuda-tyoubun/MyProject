using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FxCommonLib.Utils;

namespace FxCommonLib.Tests.Utils {
    [TestClass]
    public class MailUtilTest {
        [TestMethod]
        public void SendMailTest() {
            MailUtil mu = new MailUtil("192.168.10.76", 25);
            mu.SendMail("fxcommonlib@chuo-computer.co.jp", "hashimoto-kouji@chuo-computer.co.jp", "件名", "メール本文");

            //想定結果：上記メールを受信
            //備考：エビデンス不要
            //確認者：橋本, 確認日：2018/3/19
            bool result = true;
            Assert.AreEqual(true, result);
        }
    }
}
