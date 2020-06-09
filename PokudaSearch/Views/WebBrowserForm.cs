using C1.Win.C1Input;
using FxCommonLib.Consts;
using FxCommonLib.Utils;
using Microsoft.WindowsAPICodePack.Controls;
using Microsoft.WindowsAPICodePack.Controls.WindowsForms;
using Microsoft.WindowsAPICodePack.Shell;
using PokudaSearch.Controls;
using PokudaSearch.WebDriver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokudaSearch.Views {
    public partial class WebBrowserForm : Form {

        private string _rootUrl = "";

        private CefSharpPanel _browserPanel = null;
        public CefSharpPanel BrowserPanel {
            get {
                return _browserPanel;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WebBrowserForm(string rootUrl, CefSharpPanel browserPanel) {
            InitializeComponent();

            _rootUrl = rootUrl;
            _browserPanel = browserPanel;
            //_browserPanel.LoadPageAsync("about:blank");
            //this.Controls.Add(_browserPanel);
        }

        private void StopButton_Click(object sender, EventArgs e) {

        }

        private async void WebBrowserForm_Shown(object sender, EventArgs e) {
            WebClawringDriver wcd = new WebClawringDriver(_browserPanel);
            var dic = await wcd.Clawring(_rootUrl);

            foreach (var kvp in dic) {
                AppObject.Logger.Info(kvp.Key);
                AppObject.Logger.Info(kvp.Value.Title);
            }
        }

        private void WebBrowserForm_FormClosed(object sender, FormClosedEventArgs e) {
            _browserPanel.LoadPageAsync("about:blank");
            //this.Controls.Remove(_browserPanel);
        }
    }
}
