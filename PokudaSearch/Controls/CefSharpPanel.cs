using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using System.Windows.Threading;
using FxCommonLib.Utils;
using System.Threading;

namespace PokudaSearch.Controls {
    public partial class CefSharpPanel : UserControl {


        #region MemberVariables
        #endregion MemberVariables

        private bool _loadComplete = false;
        public bool LoadComplete {
            get {
                return  _loadComplete;
            }
        }
        private string _currentPageSource = "";
        public string CurrentPageSource {
            get {
                return _currentPageSource;
            }
        }
        private ChromiumWebBrowser _chromeBrowser;
        public ChromiumWebBrowser Browser {
            get {
                return _chromeBrowser;
            }
        }

        #region Constractors
        public CefSharpPanel() {
            InitializeComponent();
            if (!DesignModeUtil.IsDesignMode()) {
                InitializeChromium();
            }
        }
        #endregion Constractors


        private void InitializeChromium() {

            CefSettings settings = new CefSettings();
            settings.Locale = "ja";
            //ログ出力無効
            settings.LogSeverity = LogSeverity.Disable;
            settings.BrowserSubprocessPath = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                   Environment.Is64BitProcess ? "x64" : "x86",
                                                   "CefSharp.BrowserSubprocess.exe");

            Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);

            _chromeBrowser = new ChromiumWebBrowser("about:blank");
            this.BrowserPanel.Controls.Add(_chromeBrowser);
            //_chromeBrowser.Dock = DockStyle.Fill;
            //_chromeBrowser.Size = new Size(412, 470);
            //_chromeBrowser.Location = new Point(3, 3);
            //_chromeBrowser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            //_chromeBrowser.BringToFront();

            _chromeBrowser.LoadingStateChanged += ChromeBrowser_LoadingStateChanged;
            _chromeBrowser.LoadError += ChromeBrowser_LoadError;
            _chromeBrowser.FrameLoadStart += ChromeBrowser_FrameLoadStart;
            _chromeBrowser.FrameLoadEnd += ChromeBrowser_FrameLoadEnd;
        }
        #region EventHandlers
        /// <summary>
        /// Webブラウザーのプログレス変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChromeBrowser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e) {
            delegate_UpdateProgressBar callback = new delegate_UpdateProgressBar(UpdateProgressBar);
            this.ProgressBar.Invoke(callback, e.IsLoading);
        }
        /// <summary>
        /// Webブラウザーのロードエラー時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChromeBrowser_LoadError(object sender, CefSharp.LoadErrorEventArgs e) {
            AppObject.Logger.Warn("ChromeBrowser LoadError occured. : " + e.FailedUrl);
            _chromeBrowser.Load("about:blank");
        }

        private void ChromeBrowser_FrameLoadStart(object sender, CefSharp.FrameLoadStartEventArgs e) {
            _loadComplete = false;
        }
        private void ChromeBrowser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e) {
            _loadComplete = true;
            Dispatcher.CurrentDispatcher.BeginInvoke((Action)(async () => {
                _currentPageSource = await _chromeBrowser.GetSourceAsync();
            }));
        }
        #endregion EventHandlers


        /// <summary>プログレスバー更新イベント</summary>
        /// <param name="isLoading"></param>
        delegate void delegate_UpdateProgressBar(bool isLoading);
        /// <summary>
        /// プレビューブラウザのプログレスバー更新
        /// </summary>
        /// <param name="isLoading"></param>
        private void UpdateProgressBar(bool isLoading) {
            if (isLoading) {
                this.ProgressBar.Style = ProgressBarStyle.Marquee;
                this.ProgressBar.MarqueeAnimationSpeed = 100;
            } else {
                this.ProgressBar.Style = ProgressBarStyle.Blocks;
                this.ProgressBar.Value = 0;
            }
        }

        public void LoadAsync(string url) {
            _loadComplete = false;
            _chromeBrowser.Load(url);
            for (int i = 0; i < 100; i++) {
                if (_loadComplete) {
                    break;
                }
                Thread.Sleep(100);
            }

        }

        //public static Task LoadPageAsync(IWebBrowser browser, string address = null) {
        //    var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        //    EventHandler<LoadingStateChangedEventArgs> handler = null;
        //    handler = (sender, args) => {
        //        //Wait for while page to finish loading not just the first frame
        //        if (!args.IsLoading) {
        //            browser.LoadingStateChanged -= handler;
        //            //Important that the continuation runs async using TaskCreationOptions.RunContinuationsAsynchronously
        //            tcs.TrySetResult(true);
        //        }
        //    };

        //    browser.LoadingStateChanged += handler;

        //    if (!string.IsNullOrEmpty(address)) {
        //        browser.Load(address);
        //    }
        //    return tcs.Task;
        //}
    }
}
