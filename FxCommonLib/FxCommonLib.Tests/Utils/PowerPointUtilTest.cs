using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using FxCommonLib.Utils;
using System.Security.AccessControl;
using System.Diagnostics;

namespace FxCommonLib.Tests.Utils {
    [TestClass]
    public class PowerPointUtilTest {

        [TestMethod]
        public void SaveAsXPSTest() {
            COMUtil com = new COMUtil();
            object ppApp = null;
            PowerPointUtil pp = new PowerPointUtil();
            
            string inputPath = Environment.CurrentDirectory + @".\TestData\Sample.pptx";
            string outputPath = Environment.CurrentDirectory + @".\TestData\Sample.xps";

            File.Delete(outputPath);

            ppApp = com.CreateObject("Powerpoint.Application");
            //HACK msoFalseにするとエラーになる。
            //pp.SetVisible(ppApp, PowerPointUtil.MsoTriState.msoTrue);

            object presentations = pp.GetPresentations(ppApp);
            object presentation = pp.Open2007(presentations, inputPath, PowerPointUtil.MsoTriState.msoFalse);

            try {
                //XPS形式で保存
                pp.SaveAs(presentation, outputPath, PowerPointUtil.PpFileFormat.PpSaveAsXPS);
            } finally {
                pp.Close(presentation);
                pp.Quit(ppApp);
                com.MReleaseComObject(presentation);
                com.MReleaseComObject(presentations);
                com.MReleaseComObject(ppApp);
            }

            Assert.AreEqual(File.Exists(outputPath),true);
        }
    }
}
