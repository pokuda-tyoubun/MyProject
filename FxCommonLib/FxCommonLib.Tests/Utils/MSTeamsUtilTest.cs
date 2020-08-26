using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using FxCommonLib.Utils;
using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace FxCommonLib.Tests.Utils {

    [TestClass]
    public class MSTeamsUtilTest {
        private string _webhookURL = "https://outlook.office.com/webhook/6560db08-2c70-4993-8626-4085187897ab@1edf949e-9f89-4fdd-a567-e83f41c45ea6/IncomingWebhook/58bb608a006949a2898433c097e8630d/44e25d42-47f2-4373-a6a5-e333a7fb8d38";

        [TestMethod]
        public void PostPlainMessageTest() {
            //想定結果：上記メールを受信
            //備考：
            //確認者：橋本, 確認日：2018/8/13

            var mst = new MSTeamsUtil();
            mst.PostPlainMessage(_webhookURL, "hoge hoge");
        }
    }
}
