using CefSharp;
using CefSharp.WinForms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PokudaSearch.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokudaSearch.WebDriver {
    public class WebClawringDriver {

        private string _rootUrl = "http://078134995:Ais5vs2004@192.168.13.67/docs/html/kitei/index.htm";

        public WebClawringDriver() {
        }

        public struct WebContents {
            string Url;
            string Title;
            string Extention;
            DateTime updateDate;
            string contents;
        }

        public List<WebContents> Clawring(CefSharpPanel cefSharpPanel) {
            var ret = new List<WebContents>();

            cefSharpPanel.LoadAsync(_rootUrl);

            MessageBox.Show(cefSharpPanel.CurrentPageSource);

            //chromeBrowser.GetMainFrame().ViewSource();
            /*
            Task<string> t = chromeBrowser.GetMainFrame().GetSourceAsync();

            //リンクの一覧を取得
            AppObject.Logger.Info(chromeBrowser.Address);
            AppObject.Logger.Info(t.Result);
            */

            return ret;
        }
    }
}
