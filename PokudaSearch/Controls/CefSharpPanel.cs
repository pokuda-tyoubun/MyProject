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


        #region Properties
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
        private ChromiumWebBrowser _chromeBrowser = null;
        public ChromiumWebBrowser Browser {
            get {
                return _chromeBrowser;
            }
        }
        public Control FocusBackControl = null;
        #endregion Properties

        #region MemberVariables
        private TaskCompletionSource<bool> _completionSource = null;
        #endregion MemberVariables

        #region Constractors
        public CefSharpPanel() {
            InitializeComponent();
            if (!DesignModeUtil.IsDesignMode()) {
                InitializeChromium();
                InitializeInstance();
            }
        }
        #endregion Constractors

        public void FindNext() {
            _chromeBrowser.Find(identifier: 1, searchText: "稟議", forward: true, matchCase: false, findNext: true);
        }
        public void FindPrevious() {
            _chromeBrowser.Find(identifier: 1, searchText: "稟議", forward: false, matchCase: false, findNext: true);
        }
        public void StopFinding() {
            _chromeBrowser.StopFinding(true);
        }

        public void ResetCompletionSource() {
            _completionSource = null;
        }

        private void InitializeChromium() {

            CefSettings settings = new CefSettings();
            settings.Locale = "ja";
            //ログ出力無効
            settings.LogSeverity = LogSeverity.Disable;
            settings.BrowserSubprocessPath = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                   Environment.Is64BitProcess ? "x64" : "x86",
                                                   "CefSharp.BrowserSubprocess.exe");

            Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);
        }

        public void InitializeInstance() {
            if (_chromeBrowser != null) {
                this.BrowserPanel.Controls.Remove(_chromeBrowser);
                _chromeBrowser = null;
            }

            _chromeBrowser = new ChromiumWebBrowser("about:blank");
            this.BrowserPanel.Controls.Add(_chromeBrowser);

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

                if (_completionSource != null) {
                    if (!_completionSource.Task.IsCompleted) {
                        _completionSource.SetResult(true);
                    }
                }
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
                if (this.FocusBackControl == null) {
                    this.Parent.Focus();
                } else {
                    this.FocusBackControl.Focus();
                }
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


        public Task LoadPageAsync(string url = null) {
            _completionSource = new TaskCompletionSource<bool>();

            ThreadPool.QueueUserWorkItem(_ => {
                _chromeBrowser.Load(url);
            });

            var task = _completionSource.Task;
            return task;
        }

        public async Task<int> GetLinkNum() {
            var list = await GetLinks();
            return list.Count;
        }

        public Task<bool> SetInputValueById(string id, string value) {
            var completionSource = new TaskCompletionSource<bool>();
            string script = "document.getElementById('" + id + "').value=" + '\'' + value + '\'';
            Dispatcher.CurrentDispatcher.BeginInvoke((Action)(async () => {
                await _chromeBrowser.EvaluateScriptAsync(script).ContinueWith(x => {
                    var response = x.Result;
                    if (response.Success && response.Result != null) {
                        completionSource.SetResult(true);
                    } else {
                        completionSource.SetResult(false);
                    }

                });
            }));
            Task<bool> task = completionSource.Task;
            return task;
        }
        public Task<bool> ClickById(string id) {
            var completionSource = new TaskCompletionSource<bool>();
            string script = "document.getElementById('" + id + "').click();";
            Dispatcher.CurrentDispatcher.BeginInvoke((Action)(async () => {
                await _chromeBrowser.EvaluateScriptAsync(script).ContinueWith(x => {
                    var response = x.Result;
                    if (response.Success && response.Result != null) {
                        completionSource.SetResult(true);
                    } else {
                        completionSource.SetResult(false);
                    }

                });
            }));
            Task<bool> task = completionSource.Task;
            return task;
        }
        public Task<string> GetTextContentByXPath(string xpath) {
            var completionSource = new TaskCompletionSource<string>();
            string script = @"document.evaluate(""" + xpath + @""", document, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null).snapshotItem(0).textContent;";
            Dispatcher.CurrentDispatcher.BeginInvoke((Action)(async () => {
                await _chromeBrowser.EvaluateScriptAsync(script).ContinueWith(x => {
                    var response = x.Result;
                    if (response.Success && response.Result != null) {
                        var text = (string)response.Result;
                        completionSource.SetResult(text);
                    } else {
                        completionSource.SetResult("");
                    }

                });
            }));
            Task<string> task = completionSource.Task;
            return task;
        }
        public Task<string> GetTextContentByClassName(string className) {
            var completionSource = new TaskCompletionSource<string>();
            string script = "document.getElementsByClassName('" + className + "')[0].textContent;";
            Dispatcher.CurrentDispatcher.BeginInvoke((Action)(async () => {
                await _chromeBrowser.EvaluateScriptAsync(script).ContinueWith(x => {
                    var response = x.Result;
                    if (response.Success && response.Result != null) {
                        var text = (string)response.Result;
                        completionSource.SetResult(text);
                    } else {
                        completionSource.SetResult("");
                    }

                });
            }));
            Task<string> task = completionSource.Task;
            return task;
        }
        public Task<string> GetTextContentById(string id) {
            var completionSource = new TaskCompletionSource<string>();
            string script = "document.getElementById('" + id + "').textContent;";
            Dispatcher.CurrentDispatcher.BeginInvoke((Action)(async () => {
                await _chromeBrowser.EvaluateScriptAsync(script).ContinueWith(x => {
                    var response = x.Result;
                    if (response.Success && response.Result != null) {
                        var text = (string)response.Result;
                        completionSource.SetResult(text);
                    } else {
                        completionSource.SetResult("");
                    }

                });
            }));
            Task<string> task = completionSource.Task;
            return task;
        }
        public Task<bool> AnchorClickByClassName(string className) {
            var completionSource = new TaskCompletionSource<bool>();
            string script =
            //@"function triggerEvent(element, event) {
            //   if (document.createEvent) {
            //       // IE以外
            //       var evt = document.createEvent('HTMLEvents');
            //       evt.initEvent(event, true, true ); // event type, bubbling, cancelable
            //       return element.dispatchEvent(evt);
            //   }
            //}
            //var a = document.getElementsByClassName('ageCheck__link ageCheck__link--r18')[0];
            //triggerEvent(a,  
            //@"var a = document.getElementsByClassName('" + className + @"')[0];
            @"var a = document.getElementsByClassName('ageCheck__link ageCheck__link--r18')[0];
              a.click();";

            Dispatcher.CurrentDispatcher.BeginInvoke((Action)(async () => {
                await _chromeBrowser.EvaluateScriptAsync(script).ContinueWith(x => {
                    Thread.Sleep(3000);
                    var response = x.Result;
                    if (response.Success && response.Result != null) {
                        completionSource.SetResult(true);
                    } else {
                        completionSource.SetResult(false);
                    }

                });
            }));
            Task<bool> task = completionSource.Task;
            return task;
        }
        public Task<bool> ClickByXPath(string xpath) {
            var completionSource = new TaskCompletionSource<bool>();
            string script = "document.evaluate(('" + xpath + "', document, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null).snapshotItem(0).click();";
            Dispatcher.CurrentDispatcher.BeginInvoke((Action)(async () => {
                await _chromeBrowser.EvaluateScriptAsync(script).ContinueWith(x => {
                    var response = x.Result;
                    if (response.Success && response.Result != null) {
                        completionSource.SetResult(true);
                    } else {
                        completionSource.SetResult(false);
                    }

                });
            }));
            Task<bool> task = completionSource.Task;
            return task;
        }
        public Task<List<string>> GetTextContentsByXPath(string xpath) {
            var ret = new List<string>();
            var completionSource = new TaskCompletionSource<List<string>>();
            string script = @"(function() {
           	    				   var txtArray = new Array();
                                   var elms = document.evaluate(""//td[text()='ジャンル：']/../td[2]/a"", document, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
           	    				   for (var i = 0; i < elms.snapshotLength; i++) {
           	    						txtArray[i] = String(elms.snapshotItem(i).textContent);
           	    				   }
           	    				   return txtArray;
            				})();";
            Dispatcher.CurrentDispatcher.BeginInvoke((Action)(async () => {
                await _chromeBrowser.EvaluateScriptAsync(script).ContinueWith(x => {
                    var response = x.Result;
                    if (response.Success && response.Result != null) {
                        var list = (List<object>)response.Result;
                        foreach (string tmp in list) {
                            ret.Add(tmp);
                        }
                    }
                });
            }));
            Task<List<string>> task = completionSource.Task;
            return task;
        }

        public Task<List<string>> GetLinks() {
            var ret = new List<string>();
            var completionSource = new TaskCompletionSource<List<string>>();
            //if (_chromeBrowser.CanExecuteJavascriptInMainFrame) {
                const string script = @"(function()
            					{
        	    					var linksArray = new Array();
        	    					for (var i = 0; i < document.links.length; i++) {
        	    						linksArray[i] = String(document.links[i].href);
        	    					}
        	    					return linksArray;
            					})();";

                Dispatcher.CurrentDispatcher.BeginInvoke((Action)(async () => {
                    await _chromeBrowser.EvaluateScriptAsync(script).ContinueWith(x => {
                        var response = x.Result;
                        if (response.Success && response.Result != null) {
                            var list = (List<object>)response.Result;
                            foreach (string url in list) {
                                ret.Add(url);
                            }
                        }
                        completionSource.SetResult(ret);
                    });
                }));
            //} else {
            //    completionSource.SetResult(ret);
            //}
            Task<List<string>> task = completionSource.Task;
            return task;
        }

        public Task<string> GetTitle() {
            var completionSource = new TaskCompletionSource<string>();
            //if (_chromeBrowser.CanExecuteJavascriptInMainFrame) {
        		const string script = @"document.title;";

                Dispatcher.CurrentDispatcher.BeginInvoke((Action)(async () => {
                    //_chromeBrowser.ExecuteScriptAsync(script);
                    await _chromeBrowser.EvaluateScriptAsync(script).ContinueWith(x => {
                        var response = x.Result;
                        if (response.Success && response.Result != null) {
                            var title = response.Result;
                            completionSource.SetResult(title.ToString());
                        } else {
                            completionSource.SetResult("");
                        }
                    });
                }));
            //} else {
            //    completionSource.SetResult("");
            //}

            Task<string> task = completionSource.Task;
            return task;
        }

        public void CefShutdown() {
            CefSharp.Cef.Shutdown();
        }
    }
}
