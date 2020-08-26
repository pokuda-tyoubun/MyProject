using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using FxCommonLib.Utils;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using log4net;

namespace FxCommonLib.Tests.Log4NetAppender {

    [TestClass]
    public class MSTeamsAppenderTest {
        //HACK log4net.dllはNuGetのものを使う方が良い？
        [TestMethod]
        public void LoggerTest() {
            //想定結果：上記メールを受信
            //備考：
            //確認者：橋本, 確認日：2018/8/14

            ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Fatal("hoge hoge Fatal.");
            logger.Error("hoge hoge Error.");
            logger.Warn("hoge hoge Warn.");
            logger.Info("hoge hoge Info.");
            logger.Debug("hoge hoge Debug.");
        }
    }
}
