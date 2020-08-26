using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FxCommonLib.Tests.Utils.Demo;
using FxCommonLib.Utils;
using System.Drawing;
using FxCommonLib.Tests.Controls.Demo;

namespace FxCommonLib.Tests.Controls {
    [TestClass]
    public class TextBoxExTest {
        [TestMethod]
        public void TextBoxExDemoTest() {
            TextBoxExDemo tbed = new TextBoxExDemo();
            tbed.ShowDialog();
        }
    }
}
