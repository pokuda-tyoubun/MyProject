using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokudaPriceInspector.Views {
    public partial class PriceCompareForm : Form {


        public PriceCompareForm() {
            InitializeComponent();


            //var browser = new CefSharp.WinForms.ChromiumWebBrowser("https://google.co.jp");
            //this.LeftPanel.Controls.Add(_webBrowser);
            //_webBrowser.Dock = DockStyle.Fill;
        }

        private void PriceCompareForm_Load(object sender, EventArgs e) {

            this.LeftBrowser.ScriptErrorsSuppressed = true;
            this.LeftBrowser.Navigate("https://www.amazon.co.jp/");

            this.RightBrowser.Navigate("");

            //CefSettings settings = new CefSettings();
            //settings.BrowserSubprocessPath = Path.Combine(
            //    AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
            //    (Environment.Is64BitProcess ? "x64" : "x86"),
            //    "CefSharp.BrowserSubprocess.exe");


            //// 日本語に設定する
            //settings.Locale = Globalization.CultureInfo.CurrentCulture.Parent.ToString();
            //settings.AcceptLanguageList = Globalization.CultureInfo.CurrentCulture.Name;
            //// ユーザーデータの保存先を設定する
            //settings.UserDataPath = appPath;
            //CefSharp.Cef.Initialize(settings, false, null);

            //// サイト読み込みと表示
            //_webBrowser = new ChromiumWebBrowser("https://www.google.co.jp/");
            //this.LeftPanel.Controls.Add(_webBrowser);
            //_webBrowser.Dock = DockStyle.Fill;

        }

        private void RunButton_Click(object sender, EventArgs e) {
            HtmlElementCollection all = this.LeftBrowser.Document.All;
            HtmlElementCollection forms = all.GetElementsByName("field-keywords");
            forms[0].InnerText = "c#";

            //TODOここから
            //Submitの方法が不明
            //forms[0].In
        }


    }
}
