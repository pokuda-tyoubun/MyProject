using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JGFSControls;
using System.Diagnostics;

namespace JGFileManagerTests {
    [TestClass]
    public class JCommonTests {
        [TestMethod]
        public void AppSettingsPathTest() {
            Trace.WriteLine(JCommon.AppSettingsPath());
        }
    }
}
