using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FxCommonLib.Utils;
using System.Data;

namespace FxCommonLib.Tests.Utils {
    [TestClass]
    public class DateTimeUtilTest {
        [TestMethod]
        public void TruncateTest() {
            DateTime dt = new DateTime(2020, 04, 08, 16, 48, 55, 123);

            //精度を秒までに
            var sec = DateTimeUtil.Truncate(dt, TimeSpan.FromSeconds(1));
            Assert.AreEqual(DateTime.Parse(dt.ToString("yyyy/MM/dd HH:mm:ss")), sec);

            //精度を分までに
            var min = DateTimeUtil.Truncate(dt, TimeSpan.FromMinutes(1));
            Assert.AreEqual(DateTime.Parse(dt.ToString("yyyy/MM/dd HH:mm")), min);
        }

        [TestMethod]
        public void TimeSpanToIso8601Test() {
            TimeSpan ts = new TimeSpan(1, 1, 1);
            string val = DateTimeUtil.TimeSpanToIso8601(ts);
            Assert.AreEqual("P1H1M1S", val);

            ts = TimeSpan.FromMinutes(1.5);
            val = DateTimeUtil.TimeSpanToIso8601(ts);
            Assert.AreEqual("P1M30S", val);

            //切り捨ての確認(P8M16.2S -> P8m16S)
            ts = TimeSpan.FromMinutes(8.27);
            val = DateTimeUtil.TimeSpanToIso8601(ts);
            Assert.AreEqual("P8M16S", val);

            //切り捨ての確認(P8M16.8S -> P8m16S)
            ts = TimeSpan.FromMinutes(8.28);
            val = DateTimeUtil.TimeSpanToIso8601(ts);
            Assert.AreEqual("P8M16S", val);
        }
    }
}
