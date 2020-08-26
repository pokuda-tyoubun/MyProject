using System;
using FxCommonLib.Tests.Controls.Demo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FxCommonLib.Tests.Controls {
    [TestClass]
    public class FlexGridExTest {
        [TestMethod]
        public void FlexGridExDemoTest() {
            FlexGridExDemo tged = new FlexGridExDemo();
            tged.ShowDialog();
        }
    }
}
