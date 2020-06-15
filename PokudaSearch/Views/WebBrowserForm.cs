using C1.Win.C1Input;
using com.healthmarketscience.jackcess;
using FxCommonLib.Consts;
using FxCommonLib.Utils;
using Microsoft.WindowsAPICodePack.Controls;
using Microsoft.WindowsAPICodePack.Controls.WindowsForms;
using Microsoft.WindowsAPICodePack.Shell;
using PokudaSearch.Controls;
using PokudaSearch.IndexUtil;
using PokudaSearch.WebDriver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PokudaSearch.IndexUtil.LuceneIndexBuilder;

namespace PokudaSearch.Views {
    public partial class WebBrowserForm : Form {

        private string _rootUrl = "";

        private IndexBuildForm _indexBuildForm = null;

        private Dictionary<string, WebContents> _targetDic = new Dictionary<string, WebContents>();
        public Dictionary<string, WebContents> TargetDic {
            get {
                return _targetDic;
            }
        }

        private CefSharpPanel _browserPanel = null;
        public CefSharpPanel BrowserPanel {
            get {
                return _browserPanel;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WebBrowserForm(string rootUrl, CefSharpPanel browserPanel, IndexBuildForm ibf) {
            InitializeComponent();

            _rootUrl = rootUrl;
            _browserPanel = browserPanel;
            _indexBuildForm = ibf;

            this.Controls.Add(_browserPanel);
            _browserPanel.InitializeInstance();
        }

        private void StopButton_Click(object sender, EventArgs e) {

        }

        private async void WebBrowserForm_Shown(object sender, EventArgs e) {
            WebClawringDriver wcd = new WebClawringDriver(_browserPanel);
            _targetDic = await wcd.Clawring(_rootUrl);
            this.Close();
        }

        private void WebBrowserForm_FormClosed(object sender, FormClosedEventArgs e) {
            _browserPanel.Browser.Load("about:blank");
            this.Controls.Remove(_browserPanel);
        }
    }
}
