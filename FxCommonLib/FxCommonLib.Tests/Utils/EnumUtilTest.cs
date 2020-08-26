using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using FxCommonLib.Utils;
using System.Security.AccessControl;
using System.Diagnostics;

namespace FxCommonLib.Tests.Utils {
    [TestClass]
    public class EnumUtilTest {
        private enum Months : int {
            [EnumLabel("1月")]
            January = 1,
            [EnumLabel("2月")]
            February,
            [EnumLabel("3月")]
            March,
            [EnumLabel("4月")]
            April,
            [EnumLabel("5月")]
            May,
            [EnumLabel("6月")]
            June,
            [EnumLabel("7月")]
            July,
            [EnumLabel("8月")]
            August,
            [EnumLabel("9月")]
            September,
            [EnumLabel("10月")]
            October,
            [EnumLabel("11月")]
            November,
            [EnumLabel("12月")]
            December,
        }


        [TestMethod]
        public void GetCountTest() {
            int count = EnumUtil.GetCount(typeof(Months));

            Assert.AreEqual(12, count);
        }
        [TestMethod]
        public void GetLabelTest() {
            string label = EnumUtil.GetLabel(Months.June);

            Assert.AreEqual("6月", label);
        }
        [TestMethod]
        public void GetNameTest() {
            string name = EnumUtil.GetName(Months.June);

            Assert.AreEqual("June", name);
        }
    }
}
