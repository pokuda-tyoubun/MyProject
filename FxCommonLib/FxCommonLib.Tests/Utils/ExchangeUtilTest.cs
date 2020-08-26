using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using FxCommonLib.Utils;
using System.Security.AccessControl;
using System.Diagnostics;
using System.Data;

namespace FxCommonLib.Tests.Utils {
    [TestClass]
    public class ExchangeUtilTest {

        [TestMethod]
        public void GetExchangeRateTest() {
            ExchangeUtil eu = new ExchangeUtil();

            DataTable dt = eu.GetExchangeRateTable();
            Assert.AreEqual(EnumUtil.GetCount(typeof(ExchangeUtil.CurrencyPairCode)), dt.Rows.Count);
        }
        [TestMethod]
        public void GetOpenPriceTest() {
            ExchangeUtil eu = new ExchangeUtil();
            Decimal d = eu.GetOpenPrice(ExchangeUtil.CurrencyPairCode.USDJPY);

            Assert.AreEqual(true, d > 0);
        }
    }
}
