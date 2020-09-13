using System;
using System.Diagnostics;
using FxCommonLib.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MovieTagWriter.Test {
    [TestClass]
    public class SandBoxTest {
        [TestMethod]
        public void Test() {
            string test = "HUB825T-4@A0_933_04$000001";
            Debug.Print(test.Substring(0, test.IndexOf('@')));
        }
    }
}
