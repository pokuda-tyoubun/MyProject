using FxCommonLib.Utils;
using Gecko;
using Gecko.DOM;
using Gecko.Events;
using PokudaPriceInspector.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokudaPriceInspector.Views {
    public partial class PriceCompareForm : Form {

        private GeckoWebBrowser _rateBrowser = null;
        private GeckoWebBrowser _leftBrowser = null;
        private GeckoWebBrowser _rightBrowser = null;

        public PriceCompareForm() {
            InitializeComponent();
        }

        private void PriceCompareForm_Load(object sender, EventArgs e) {
            //TODO
            //GetOpenPriceCNHJPY();

            _rateBrowser = new GeckoWebBrowser();
            this.RatePanel.Controls.Add(_rateBrowser);
            _rateBrowser.Dock = DockStyle.Fill;
            _rateBrowser.Navigate("https://stocks.finance.yahoo.co.jp/stocks/detail/?code=CNHJPY=FX");

            _leftBrowser = new GeckoWebBrowser();
            this.LeftPanel.Controls.Add(_leftBrowser);
            _leftBrowser.Dock = DockStyle.Fill;
            _leftBrowser.Navigate("https://www.amazon.co.jp/");
            //TODO 何も表示されない(WebBrowserだと表示される)
            //_leftBrowser.Navigate("https://www.saiyasune.com/");

            _rightBrowser = new GeckoWebBrowser();
            this.RightPanel.Controls.Add(_rightBrowser);
            _rightBrowser.Dock = DockStyle.Fill;
            _rightBrowser.Navigate("https://www.mercari.com/jp/");;

            //this.LeftBrowser.ScriptErrorsSuppressed = true;
            //this.LeftBrowser.Navigate("https://www.saiyasune.com/");

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
            //SearchAmazon(_leftBrowser, this.KeywordText.Text);
            //SearchSaiyasune(_leftBrowser, this.KeywordText.Text);
            //SearchMercari(_rightBrowser, this.KeywordText.Text);

            GetOpenPriceCNHJPY();
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
        private void SearchSaiyasune(IGeckoWebBrowser browser, string keyword) {

            GeckoElementCollection forms = browser.Document.GetElementsByName("keywords");
            GeckoInputElement inputter = (GeckoInputElement)forms[0];
            inputter.SetAttribute("value", keyword);

            ((GeckoFormElement)forms[0].Parent).submit();


            //HtmlElementCollection all = browser.Document.All;
            //HtmlElementCollection forms = all.GetElementsByName("field-keywords");
            //forms[0].InnerText = keyword;

            //forms = all.GetElementsByName("site-search");
            //forms[0].InvokeMember("submit");
        }

        private Decimal GetPriceAmazon(IGeckoWebBrowser browser) {
            Decimal ret = 0;

            return ret;
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

        //private async void GetOpenPriceCNHJPY() {
        private Decimal GetOpenPriceCNHJPY() {
            var gbu = new GeckoBrowserUtil();

            GeckoElement detail = _rateBrowser.Document.GetElementById("detail");
            var ymuiEditLinkList = gbu.GetElementByClassNameRecv(detail, "ymuiEditLink mar0");
            GeckoElement target = ymuiEditLinkList[1];
            target = target.QuerySelector("strong");
            return Decimal.Parse(target.TextContent);
        }

        private void GetUsdJpyRateButton_Click(object sender, EventArgs e) {
            var eu = new ExchangeUtil();
            this.UsdJpyRateNum.Value = eu.GetOpenPrice(ExchangeUtil.CurrencyPairCode.USDJPY);

        }

        private void GetCnhJpyRateButton_Click(object sender, EventArgs e) {
            this.CnhJpyRateNum.Value = GetOpenPriceCNHJPY();
        }

    }
}
