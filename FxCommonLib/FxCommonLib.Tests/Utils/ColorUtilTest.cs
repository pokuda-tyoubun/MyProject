using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FxCommonLib.Tests.Utils.Demo;
using FxCommonLib.Utils;
using System.Drawing;

namespace FxCommonLib.Tests.Utils {
    [TestClass]
    public class ColorUtilTest {
        [TestMethod]
        public void GetColorTest() {
            ColorUtil cu = new ColorUtil();
            Color result = cu.GetColor("#FF0000");

            Assert.AreEqual(cu.GetColorString(Color.Red), cu.GetColorString(result));
        }

        [TestMethod]
        public void ColorUtilDemoTest() {
            ColorUtilDemo cud = new ColorUtilDemo();
            cud.ShowDialog();
        }
    }
}
