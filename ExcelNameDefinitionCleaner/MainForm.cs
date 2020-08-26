using ExcelNameDefinitionCleaner.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = NetOffice.ExcelApi;

namespace ExcelNameDefinitionCleaner {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void RunButton_Click(object sender, EventArgs e) {
            Clear();
        }

        private void Clear() {
            string root = @"C:\Workspace\Repo\Git\ecoLLaboMES\doc";
                //Excelファイルを探す
                var fileList = FileUtil.GetAllFileInfo(root);
                var application = new Excel.Application { Visible = true };
                foreach (FileInfo fi in fileList) {
                    //if (fi.Extension.ToLower() == ".xls" || fi.Extension.ToLower() == ".xlsx") {
                    if (fi.Extension.ToLower() == ".xls") {
                        Excel.Workbook book = null;
                        try {
                            Debug.Print(fi.FullName);
                            book = application.Workbooks.Open(fi.FullName,
                                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                            //Debug.Print(books.Name + ":" + books.Names.Count().ToString() + ":" + books.Names.ToString());
                            if (book.Names.Count() > 0) {
                                Debug.Print("Find!!" + book.FullName);
                            }
                        } catch {
                            //スルー
                        } finally {
                            try {
                                if (book != null) {
                                    book.Close(false, Type.Missing, Type.Missing);
                                }
                            } catch {
                                //更にスルー
                            }
                        }
                    }
                }
                application.Quit();
        }
    }
}
