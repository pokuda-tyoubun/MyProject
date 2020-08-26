using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using FxCommonLib.Utils;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Diagnostics;

namespace FxCommonLib.Tests.Utils {

    [TestClass]
    public class ExcelUtilTest {

        [TestMethod]
        public void SelectedSheetsPrintOutTest() {
            //想定結果：全シートが印刷される
            //備考：
            //確認者：橋本, 確認日：2019/3/26

            COMUtil comUtil = new COMUtil();
            ExcelUtil xls = new ExcelUtil();
            object app = null;
            object books = null;
            object book = null;
            object sheets = null;

            try {
                //読取用Excelをオープン
                app = comUtil.CreateObject("Excel.Application");
                xls.SetVisible(app, true);
                //xls.SetDisplayAlerts(app, false);
                //xls.SetScreenUpdating(app, true);

                books = xls.GetWorkbooks(app);
                string path = Environment.CurrentDirectory + @"\TestData\Sample.xlsx";
                book = xls.Open(books, path);
                
                sheets = xls.GetWorksheets(book);
                //全シート選択
                xls.WorksheetsSelect(sheets);
                //印刷
                if (IsExistsMESPrinter()) {
                    //MESPrinter
                    xls.SelectedSheetsPrintOut(app, "MESPrinter");
                } else {
                    //デフォルトプリンタ
                    xls.SelectedSheetsPrintOut(app);
                }

                xls.Close(book);

            } finally {
                xls.Quit(app);
                comUtil.MReleaseComObject(sheets);
                comUtil.MReleaseComObject(book);
                comUtil.MReleaseComObject(books);
                comUtil.MReleaseComObject(app);
                GC.Collect();
            }
        }
        [TestMethod]
        public void Test() {
            string path = Environment.CurrentDirectory + @"\TestData\Sample.xlsx";
            //Process.Start(path);

            foreach (string s in System.Drawing.Printing.PrinterSettings.InstalledPrinters) {
                Debug.WriteLine(s);
            }
        }

        /// <summary>
        /// 専用プリンタの存在判定
        /// </summary>
        /// <returns></returns>
        private bool IsExistsMESPrinter() {
            bool ret = false;
            foreach (string s in System.Drawing.Printing.PrinterSettings.InstalledPrinters) {
                Debug.WriteLine(s);
                if (s == "MESPrinter") {
                    ret = true;
                    break;
                }
            }
            return ret;
        }
    }
}
