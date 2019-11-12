using Gecko;
using Gecko.DOM;
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

        private GeckoWebBrowser _leftBrowser = null;
        private GeckoWebBrowser _rightBrowser = null;

        public PriceCompareForm() {
            InitializeComponent();


            //var browser = new CefSharp.WinForms.ChromiumWebBrowser("https://google.co.jp");
            //this.LeftPanel.Controls.Add(_webBrowser);
            //_webBrowser.Dock = DockStyle.Fill;
        }

        private void PriceCompareForm_Load(object sender, EventArgs e) {
            _leftBrowser = new GeckoWebBrowser();
            this.LeftPanel.Controls.Add(_leftBrowser);
            _leftBrowser.Dock = DockStyle.Fill;
            _leftBrowser.Navigate("https://www.amazon.co.jp/");

            _rightBrowser = new GeckoWebBrowser();
            this.RightPanel.Controls.Add(_rightBrowser);
            _rightBrowser.Dock = DockStyle.Fill;
            _rightBrowser.Navigate("https://www.mercari.com/jp/");;

            //this.LeftBrowser.ScriptErrorsSuppressed = true;
            //this.LeftBrowser.Navigate("https://www.amazon.co.jp/");

            //this.RightBrowser.ScriptErrorsSuppressed = true;
            //this.RightBrowser.Navigate("https://www.mercari.com/jp/");

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
            SearchAmazon(_leftBrowser, this.KeywordText.Text);
            SearchMercari(_rightBrowser, this.KeywordText.Text);
        }

        private void SearchAmazon(IGeckoWebBrowser browser, string keyword) {

            GeckoElement element = browser.Document.GetElementById("twotabsearchtextbox");
            GeckoInputElement inputter = (GeckoInputElement)element;
            inputter.SetAttribute("value", keyword);


            GeckoElementCollection forms = browser.Document.GetElementsByName("site-search");
            ((GeckoFormElement)forms[0]).submit();


            //HtmlElementCollection all = browser.Document.All;
            //HtmlElementCollection forms = all.GetElementsByName("field-keywords");
            //forms[0].InnerText = keyword;

            //forms = all.GetElementsByName("site-search");
            //forms[0].InvokeMember("submit");
        }

        private void SearchMercari(IGeckoWebBrowser browser, string keyword) {

            GeckoElementCollection elements = browser.Document.GetElementsByName("keyword");
            GeckoInputElement inputter = (GeckoInputElement)elements[0];
            inputter.SetAttribute("value", keyword);

            var form = (GeckoFormElement)inputter.Parent;
            form.submit();


            //HtmlElementCollection all = browser.Document.All;
            //HtmlElementCollection forms = all.GetElementsByName("keyword");
            //forms[0].InnerText = keyword;

            //forms[0].InvokeMember("submit");
        }

    }
}
