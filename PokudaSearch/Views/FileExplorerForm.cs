using C1.Win.C1Input;
using CefSharp.WinForms.Internals;
using com.sun.corba.se.pept.transport;
using com.sun.org.apache.xerces.@internal.util;
using FlexLucene.Store;
using FxCommonLib.Consts;
using FxCommonLib.Utils;
using FxCommonLib.Win32API;
using Microsoft.WindowsAPICodePack.Controls;
using Microsoft.WindowsAPICodePack.Controls.WindowsForms;
using Microsoft.WindowsAPICodePack.Shell;
using PokudaSearch.IndexUtil;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokudaSearch.Views {
    public partial class FileExplorerForm : Form {

        #region Constants
        private const int MaxHistoryCount = 20;
        #endregion Constants

        #region Properties
        /// <summary>有効インデックス</summary>
        private static DataTable _activeIndex = null;
        public DataTable ActiveIndex {
            get { return _activeIndex; }
        }
        #endregion Properties

        #region MemberVariables
        //NOTE:「コントロールパネル」など、CLSID表示になるページがあり、そのページの再表示方法が不明なので、
        //      Back->Forwardで暫定対応する。そのためのForwardフラグ
        /// <summary>MainExplorerにFoward処理を行うかどうか</summary>
        //private bool _doForwardMain = false;
        /// <summary>SubExplorerにFoward処理を行うかどうか</summary>
        //private bool _doForwardSub = false;
        #endregion MemberVariables

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FileExplorerForm() {
            InitializeComponent();
        }
        #endregion Constractors

        #region EventHandlers
        private void FileExplorerForm_Shown(object sender, EventArgs e) {
            this.MainExplorer.Navigate(ShellObject.FromParsingName(AppObject.DefaultPath));

            //左部のTreeViewを非表示に
            this.SubExplorer.NavigationOptions.PaneVisibility.Navigation = PaneVisibilityState.Hide;
            this.SubExplorer.Navigate((ShellObject)KnownFolders.Desktop);
        }
        #endregion EventHandlers

        /// <summary>
        /// NOTE:イベント発火せず。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainExplorer_KeyDown(object sender, KeyEventArgs e) {
        }
        /// <summary>
        /// NOTE:イベント発火せず。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubExplorer_KeyDown(object sender, KeyEventArgs e) {

        }
        /// <summary>
        /// カレントフォルダ検索(Main)ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e) {
            ShowSearchForm(this.MainExplorerCombo.Text);
        }
        /// <summary>
        /// MainExplorerのリフレッシュ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshButton_Click(object sender, EventArgs e) {
            this.MainExplorer.Refresh();
        }
        /// <summary>
        /// カレントフォルダ検索(Sub)ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchSubButton_Click(object sender, EventArgs e) {
            ShowSearchForm(this.SubExplorerCombo.Text);
        }
        /// <summary>
        /// SubExplorerのリフレッシュ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshSubButton_Click(object sender, EventArgs e) {
            this.SubExplorer.Refresh();
        }
        /// <summary>
        /// MainExplorerのPreviewKeyDown
        /// NOTE:KeyDownイベントはハンドリングできない。(イベントが発火しない)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainExplorer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
            //Viライクな操作
            //if (e.KeyCode == Keys.J) {
            //    SendKeys.Send("{DOWN}");
            //    return;
            //}
            //if (e.KeyCode == Keys.K) {
            //    SendKeys.Send("{UP}");
            //    return;
            //}
            //if (e.KeyCode == Keys.L) {
            //    SendKeys.Send("{RIGHT}");
            //    return;
            //}
            //if (e.KeyCode == Keys.H) {
            //    SendKeys.Send("{LEFT}");
            //    return;
            //}
            //if (e.KeyCode == Keys.Oem1) {
            //    _command = ":";
            //    return;
            //}
            //if (e.KeyCode == Keys.W && _command == ":") {
            //    this.SubExplorer.PerformAutoScale();
            //    _command = "";
            //    return;
            //}
            if ((e.Modifiers & Keys.Alt) == Keys.Alt && e.KeyCode == Keys.Left) {
                this.MainExplorer.NavigateLogLocation(NavigationLogDirection.Backward);
                return;
            }
            if ((e.Modifiers & Keys.Alt) == Keys.Alt && e.KeyCode == Keys.D) {
                //アドレスバーへ
                this.MainExplorerCombo.Focus();
                this.MainExplorerCombo.SelectAll();
            }
            if (e.KeyCode == Keys.BrowserBack) {
                this.MainExplorer.NavigateLogLocation(NavigationLogDirection.Backward);
                return;
            }
            if (e.KeyCode == Keys.BrowserForward) {
                this.MainExplorer.NavigateLogLocation(NavigationLogDirection.Forward);
                return;
            }
            if (e.KeyCode == Keys.Back) {
                this.MainExplorer.NavigateLogLocation(NavigationLogDirection.Backward);
                return;
            }
            if ((e.Modifiers & Keys.Alt) == Keys.Alt && e.KeyCode == Keys.Right) {
                this.MainExplorer.NavigateLogLocation(NavigationLogDirection.Forward);
                return;
            }
            if ((e.Modifiers & Keys.Alt) == Keys.Alt && e.KeyCode == Keys.Up) {
                UpwardNavigation(this.MainExplorer);

                return;
            }

            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.W) {
                this.SubExplorer.Select();
                //this.SubExplorer.SelectedItems[0]..Add(this.SubExplorer.Items[0]);
                //this.SubExplorer.Items[0].Properties.System.
                //this.SubExplorer.Focus();
            }
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.F) {
                ShowSearchForm(this.MainExplorerCombo.Text);
            }
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.E) {
                OpenExplorerButton.PerformClick();
            }
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.S) {
                ShowSearchForm();
            }
        }
        /// <summary>
        /// SubExplorerのPreviewKeyDown
        /// NOTE:KeyDownイベントはハンドリングできない。(イベントが発火しない)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubExplorer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
            if ((e.Modifiers & Keys.Alt) == Keys.Alt && e.KeyCode == Keys.Left) {
                this.SubExplorer.NavigateLogLocation(NavigationLogDirection.Backward);
                return;
            }
            if ((e.Modifiers & Keys.Alt) == Keys.Alt && e.KeyCode == Keys.D) {
                //アドレスバーへ
                this.SubExplorerCombo.Focus();
                this.SubExplorerCombo.SelectAll();
            }
            if (e.KeyCode == Keys.BrowserBack) {
                this.SubExplorer.NavigateLogLocation(NavigationLogDirection.Backward);
                return;
            }
            if (e.KeyCode == Keys.BrowserForward) {
                this.SubExplorer.NavigateLogLocation(NavigationLogDirection.Forward);
                return;
            }
            if (e.KeyCode == Keys.Back) {
                this.SubExplorer.NavigateLogLocation(NavigationLogDirection.Backward);
                return;
            }
            if ((e.Modifiers & Keys.Alt) == Keys.Alt && e.KeyCode == Keys.Right) {
                this.SubExplorer.NavigateLogLocation(NavigationLogDirection.Forward);
                return;
            }
            if ((e.Modifiers & Keys.Alt) == Keys.Alt && e.KeyCode == Keys.Up) {
                UpwardNavigation(this.SubExplorer);

                return;
            }
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.W) {
                this.MainExplorer.Select();
                //this.MainExplorer.Focus();
            }
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.F) {
                ShowSearchForm(this.SubExplorerCombo.Text);
            }
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.E) {
                OpenExplorerSubButton.PerformClick();
            }
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.S) {
                ShowSearchForm();
            }
        }
        /// <summary>
        /// FileExplorerFormのKeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileExplorerForm_KeyDown(object sender, KeyEventArgs e) {
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.W) {
                this.SubExplorer.Select();
            }
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.S) {
                ShowSearchForm();
            }
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.F) {
                ShowSearchForm(this.MainExplorerCombo.Text);
            }
        }

        /// <summary>
        /// インデックス済みかどうか判定
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool IsContainActiveIndex(string path) {
            bool ret = false;
            foreach (DataRow dr in _activeIndex.Rows) {
                string activePath = StringUtil.NullToBlank(dr[EnumUtil.GetLabel(IndexBuildForm.ActiveIndexColIdx.IndexedPath)]);
                if (path.Contains(activePath)) {
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Main側をインデックス済みとしてステータス表示する。
        /// </summary>
        private void SetIndexedStatus() {
            this.SearchButton.BackColor = SystemColors.Control;
            AppObject.Frame.SetIndexedLabel("インデックス済み"); 
        }
        /// <summary>
        /// Main側をインデックス未作成としてステータス表示する。
        /// </summary>
        private void SetUnindexedStatus() {
            //this.SearchButton.BackColor = Color.LightCoral;
            this.SearchButton.BackColor = Color.LightSteelBlue;
            AppObject.Frame.SetIndexedLabel("インデックス未作成"); 
        }
        /// <summary>
        /// Sub側をインデックス済みとしてステータス表示する。
        /// </summary>
        private void SetSubIndexedStatus() {
            this.SearchSubButton.BackColor = SystemColors.Control;
            AppObject.Frame.SetSubIndexedLabel("インデックス済み"); 
        }
        /// <summary>
        /// Sub側をインデックス未作成としてステータス表示する。
        /// </summary>
        private void SetSubUnindexedStatus() {
            //this.SearchSubButton.BackColor = Color.LightCoral;
            this.SearchSubButton.BackColor = Color.LightSteelBlue;
            AppObject.Frame.SetSubIndexedLabel("インデックス未作成"); 
        }
        private void MainExplorer_NavigationComplete(object sender, NavigationCompleteEventArgs e) {
            SetUniqueHistoryCombo(this.MainExplorerCombo, e.NewLocation);

            var so = e.NewLocation;
            if (so.IsFileSystemObject) {
                this.MainExplorerCombo.Text = e.NewLocation.ParsingName;
                this.Text = e.NewLocation.ParsingName;
            } else {
                this.MainExplorerCombo.Text = e.NewLocation.Name;
                this.Text = e.NewLocation.Name;
            }

            if (IsContainActiveIndex(e.NewLocation.ParsingName)) {
                SetIndexedStatus();
            } else {
                SetUnindexedStatus();
            }
        }
        private void SubExplorer_NavigationComplete(object sender, NavigationCompleteEventArgs e) {
            SetUniqueHistoryCombo(this.SubExplorerCombo, e.NewLocation);

            var so = e.NewLocation;
            if (so.IsFileSystemObject) {
                this.SubExplorerCombo.Text = e.NewLocation.ParsingName;
                this.SubExplorerCombo.Items.Insert(this.SubExplorerCombo.Items.Count, so.ParsingName);
            } else {
                this.SubExplorerCombo.Text = e.NewLocation.Name;
                this.SubExplorerCombo.Items.Insert(this.SubExplorerCombo.Items.Count, so.Name);
            }
            if (IsContainActiveIndex(e.NewLocation.ParsingName)) {
                SetSubIndexedStatus();
            } else {
                SetSubUnindexedStatus();
            }
        }
        public void LoadMainExplorer(string path) {
            LoadPath(this.MainExplorer, path);
        }
        public void LoadMainToSubExplorer(string path) {
            string prePath = this.MainExplorerCombo.Text;
            LoadPath(this.MainExplorer, path);
            LoadPath(this.SubExplorer, prePath);
        }
        public void LoadSubExplorer(string path) {
            LoadPath(this.SubExplorer, path);
        }

        private void LoadPath(ExplorerBrowser eb, string path) {
            try {
                if (System.IO.Directory.Exists(path)) {
                    eb.Navigate(ShellFileSystemFolder.FromFolderPath(path));
                } else {
                    //スルー
                }
            } catch (COMException) {
                MessageBox.Show(AppObject.GetMsg(AppObject.Msg.ERR_INVALID_PATH), 
                    AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadPath(ExplorerBrowser eb, ShellObject so) {
            eb.Navigate(so);
        }
        public void ReLoadFileBlowser() {
            this.MainExplorer.Navigate(this.MainExplorer.NavigationLog.Locations.Last());
            this.SubExplorer.Navigate(this.SubExplorer.NavigationLog.Locations.Last());
        }

        private void FileExplorerForm_FormClosed(object sender, FormClosedEventArgs e) {
            MainFrameForm.FileExplorerForm = null;
        }

        private void MainExplorer_HelpRequested(object sender, HelpEventArgs hlpevent) {
            Process.Start(Properties.Settings.Default.HelpUrl);
        }

        private void SubExplorer_HelpRequested(object sender, HelpEventArgs hlpevent) {
            Process.Start(Properties.Settings.Default.HelpUrl);
        }

        private void ShowSearchForm() {
            AppObject.Frame.ShowSearchForm();
        }
        private void ShowSearchForm(string targetDir) {
            AppObject.Frame.ShowSearchForm();
            //既にインデックス化されているかどうか
            if (!MainFrameForm.SearchForm.IsContainTargetIndex(targetDir)) {
                var result = MessageBox.Show(AppObject.GetMsg(AppObject.Msg.MSG_DO_CREATE_INDEX), 
                    AppObject.GetMsg(AppObject.Msg.TITLE_QUESTION), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) {
                    //インデックス作成
                    var ibf = new IndexBuildForm(targetDir);
                    ibf.ShowDialog();
                    if (ibf.DoStop) {
                        //検索中断
                        return;
                    }
                } else {
                    //検索中断
                    return;
                }
            }

            //検索
            MainFrameForm.SearchForm.SetPathFilterText(targetDir);
        }

        private void BackwardSubExplorerButton_Click(object sender, EventArgs e) {
            this.SubExplorer.NavigateLogLocation(NavigationLogDirection.Backward);
        }

        private void ForwardSubExplorerButton_Click(object sender, EventArgs e) {
            this.SubExplorer.NavigateLogLocation(NavigationLogDirection.Forward);
        }

        private void BackwardMainExplorerButton_Click(object sender, EventArgs e) {
            this.MainExplorer.NavigateLogLocation(NavigationLogDirection.Backward);
        }

        private void ForwardMainExplorerButton_Click(object sender, EventArgs e) {
            this.MainExplorer.NavigateLogLocation(NavigationLogDirection.Forward);
        }

        private void UpwardMainExplorerButton_Click(object sender, EventArgs e) {
            UpwardNavigation(this.MainExplorer);
        }

        private void UpwardSubExplorerButton_Click(object sender, EventArgs e) {
            UpwardNavigation(this.SubExplorer);
        }

        private void UpwardNavigation(ExplorerBrowser eb) {
            string path = eb.NavigationLog.CurrentLocation.ParsingName;
            if (System.IO.Directory.Exists(path)) {
                var parent = System.IO.Directory.GetParent(path);
                if (parent != null) {
                    eb.Navigate(ShellObject.FromParsingName(parent.FullName));
                }
            } else {
                eb.NavigateLogLocation(NavigationLogDirection.Backward);
            }
        }

        private void SetUniqueHistoryCombo(C1ComboBox combo, ShellObject newSo) {
            string newPath = "";
            if (newSo.IsFileSystemObject) {
                newPath = newSo.ParsingName;
            } else {
                newPath = newSo.Name;
            }
            foreach (string path in combo.Items) {
                if (newPath == path) {
                    return;
                }
            }
            combo.Items.Insert(0, newPath);
            if (combo.Items.Count > MaxHistoryCount) {
                combo.Items.RemoveAt(combo.Items.Count - 1);
            }
        }

        private void MainExplorer_LocationChanged(object sender, EventArgs e) {
            //var so = this.MainExplorer.NavigationLog.Locations.Last();
            //if (so.IsFileSystemObject) {
            //    this.MainExplorerCombo.Items.Insert(this.MainExplorerCombo.Items.Count, so.ParsingName);
            //} else {
            //    this.MainExplorerCombo.Items.Insert(this.MainExplorerCombo.Items.Count, so.Name);
            //}
        }

        /// <summary>
        /// NOTE:発火しない。
        /// </summary>
        private void MainExplorer_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Middle) {
                Debug.Print("MouseButtons.Middle");
            }
        }

        /// <summary>
        /// NOTE:発火しない。
        /// </summary>
        private void MainExplorer_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Middle) {
                Debug.Print("MouseButtons.Middle");
            }
        }
        /// <summary>
        /// NOTE:発火しない。
        /// </summary>
        private void MainExplorer_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Middle) {
                Debug.Print("MouseButtons.Middle");
            }
        }

        private void FileExplorerForm_Load(object sender, EventArgs e) {
            LoadActiveIndex();
        }

        public void LoadActiveIndex() {
            _activeIndex = IndexBuildForm.SelectActiveIndex(AppObject.ConnectString);
        }

        private void MainExplorerCombo_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                string path = this.MainExplorerCombo.Text;
                var so = GetShellObject(this.MainExplorer, path);
                if (so == null) {
                    LoadPath(this.MainExplorer, path);
                } else {
                    this.MainExplorer.Navigate(so);
                }
            }
        }

        private ShellObject GetShellObject(ExplorerBrowser explorer, string path) {
            ShellObject ret = null;
            foreach (var so in explorer.NavigationLog.Locations) {
                string name = "";
                if (so.IsFileSystemObject) {
                    name = so.ParsingName;
                } else {
                    name = so.Name;
                }

                if (name == path) {
                    ret = so;
                    break;
                }
            }
            return ret;
        } 

        private void MainExplorerCombo_SelectedItemChanged(object sender, EventArgs e) {
            //NOTE 何故かインデックスが常に-1で返ってくるので、パス文字列で取得
            //int idx = this.MainExplorerCombo.SelectedIndex;
            string path = this.MainExplorerCombo.SelectedItem.ToString();
            var so = GetShellObject(this.MainExplorer, path);
            this.MainExplorer.Navigate(so);
        }

        private void SubExplorerCombo_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                string path = this.SubExplorerCombo.Text;
                var so = GetShellObject(this.SubExplorer, path);
                if (so == null) {
                    LoadPath(this.SubExplorer, path);
                } else {
                    this.SubExplorer.Navigate(so);
                }
            }
        }
        private void SubExplorerCombo_SelectedItemChanged(object sender, EventArgs e) {
            string path = this.SubExplorerCombo.Text;
            var so = GetShellObject(this.SubExplorer, path);
            this.SubExplorer.Navigate(so);
        }

        /// <summary>
        /// フォーカスEnter
        /// NOTE:初回しか発火しない。(他のコントロールに移して再度戻しても発火しない。）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainExplorerCombo_Enter(object sender, EventArgs e) {
            //this.MainExplorerCombo.SelectAll();
        }

        private void OpenExplorerButton_Click(object sender, EventArgs e) {
            string path = this.MainExplorerCombo.Text;
            var so = GetShellObject(this.MainExplorer, path);
            Process.Start(so.ParsingName);
        }
        private void OpenExplorerSubButton_Click(object sender, EventArgs e) {
            string path = this.SubExplorerCombo.Text;
            var so = GetShellObject(this.SubExplorer, path);
            Process.Start(so.ParsingName);
        }


        //HACK ドキュメントの定義と違うのは何故？
        //ドキュメント上
        //public const int WM_XBUTTONDOWN = 0x020B;
        //実際(ドキュメント上だとWM_APPCOMMAND)
        //public const int WM_XBUTTONDOWN = 0x0319;

        protected override void WndProc(ref Message m) {

            Debug.Print("Msg=" + m.Msg);
            Debug.Print("WParam=" + m.WParam.ToInt32());
            Debug.Print("LParam=" + m.LParam.ToInt32());
            Debug.Print("GET_XBUTTON_WPARAM=" + Macros.GET_XBUTTON_WPARAM((uint)m.WParam.ToInt32()));

            base.WndProc(ref m);

            //NOTE:WM_XBUTTONDOWNが何故かキャッチできず、WM_PARENTNOTIFYだと上手くいった。
            if (m.Msg == Macros.WM_PARENTNOTIFY) {
                if (Macros.GET_XBUTTON_WPARAM((uint)m.WParam.ToInt32()) == Macros.XBUTTONS.XBUTTON1) {
                    if (MainExplorer.ClientRectangle.Contains(MainExplorer.PointToClient(Cursor.Position))) {
                        this.MainExplorer.NavigateLogLocation(NavigationLogDirection.Backward);
                    }
                    if (SubExplorer.ClientRectangle.Contains(SubExplorer.PointToClient(Cursor.Position))) {
                        this.SubExplorer.NavigateLogLocation(NavigationLogDirection.Backward);
                    }
                }
                if (Macros.GET_XBUTTON_WPARAM((uint)m.WParam.ToInt32()) == Macros.XBUTTONS.XBUTTON2) {
                    if (MainExplorer.ClientRectangle.Contains(MainExplorer.PointToClient(Cursor.Position))) {
                        this.MainExplorer.NavigateLogLocation(NavigationLogDirection.Forward);
                    }
                    if (SubExplorer.ClientRectangle.Contains(SubExplorer.PointToClient(Cursor.Position))) {
                        this.SubExplorer.NavigateLogLocation(NavigationLogDirection.Forward);
                    }
                }
            }
        }

        private void MainExplorerCombo_DoubleClick(object sender, EventArgs e) {
            this.MainExplorerCombo.SelectAll();
        }

        private void SubExplorerCombo_DoubleClick(object sender, EventArgs e) {
            this.SubExplorerCombo.SelectAll();
        }
    }
}
