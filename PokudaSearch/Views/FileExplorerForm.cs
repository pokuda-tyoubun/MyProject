using CefSharp.WinForms.Internals;
using com.sun.corba.se.pept.transport;
using com.sun.org.apache.xerces.@internal.util;
using FlexLucene.Store;
using FxCommonLib.Consts;
using Microsoft.WindowsAPICodePack.Controls;
using Microsoft.WindowsAPICodePack.Controls.WindowsForms;
using Microsoft.WindowsAPICodePack.Shell;
using PokudaSearch.IndexUtil;
using PokudaSearch.Win32API;
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

        //HACK*以下を参考い一通り実装する。
        //https://github.com/aybe/Windows-API-Code-Pack-1.1/blob/master/source/Samples/ExplorerBrowser/CS/WinForms/ExplorerBrowserTestForm.cs

        #region Constants
        #endregion Constants

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
                this.SubExplorer.Activate();
                //this.SubExplorer.SelectedItems[0]..Add(this.SubExplorer.Items[0]);
                //this.SubExplorer.Items[0].Properties.System.
                //this.SubExplorer.Focus();
            }
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.E) {
                ShowSearchForm();
            }
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.F) {
                ShowSearchFormWithTargetDir();
            }
        }
        private void SubExplorer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
            if ((e.Modifiers & Keys.Alt) == Keys.Alt && e.KeyCode == Keys.Left) {
                this.SubExplorer.NavigateLogLocation(NavigationLogDirection.Backward);
                return;
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
                ShowSearchForm();
            }
        }
        private void FileExplorerForm_KeyDown(object sender, KeyEventArgs e) {
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.W) {
                this.SubExplorer.Focus();
            }
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.E) {
                ShowSearchForm();
            }
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.F) {
                ShowSearchFormWithTargetDir();
            }
        }

        private void MainExplorer_NavigationComplete(object sender, NavigationCompleteEventArgs e) {
            var so = e.NewLocation;
            if (so.IsFileSystemObject) {
                this.MainExplorerCombo.Text = e.NewLocation.ParsingName;
                this.MainExplorerCombo.Items.Insert(this.MainExplorerCombo.Items.Count, so.ParsingName);
            } else {
                this.MainExplorerCombo.Text = e.NewLocation.Name;
                this.MainExplorerCombo.Items.Insert(this.MainExplorerCombo.Items.Count, so.Name);
            }
            this.Text = e.NewLocation.Name;
        }
        private void SubExplorer_NavigationComplete(object sender, NavigationCompleteEventArgs e) {
            var so = e.NewLocation;
            if (so.IsFileSystemObject) {
                this.SubExplorerCombo.Text = e.NewLocation.ParsingName;
                this.SubExplorerCombo.Items.Insert(this.SubExplorerCombo.Items.Count, so.ParsingName);
            } else {
                this.SubExplorerCombo.Text = e.NewLocation.Name;
                this.SubExplorerCombo.Items.Insert(this.SubExplorerCombo.Items.Count, so.Name);
            }
        }
        public void LoadMainExplorer(string path) {
            string prePath = this.MainExplorerCombo.Text;
            LoadPath(this.MainExplorer, path);
            LoadPath(this.SubExplorer, prePath);
        }

        public void ReLoadFileBlowser() {
            this.MainExplorer.Navigate(this.MainExplorer.NavigationLog.Locations.Last());
            this.SubExplorer.Navigate(this.SubExplorer.NavigationLog.Locations.Last());
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

        private void FileExplorerForm_FormClosed(object sender, FormClosedEventArgs e) {
            MainFrameForm.FileExplorerForm = null;
        }

        /// <summary>
        /// MainToolStripサイズ変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainToolStrip_SizeChanged(object sender, EventArgs e) {
            //this.MainPathCombo.Width = this.MainToolStrip.Width - 80;
        }
        /// <summary>
        /// SubToolStripサイズ変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubToolStrip_SizeChanged(object sender, EventArgs e) {
        }
        private void RightTopPanel_SizeChanged(object sender, EventArgs e) {
        }
        private void SubToolStrip_Resize(object sender, EventArgs e) {
            //this.SubPathCombo.Width = this.SubToolStrip.Width - 80;
        }

        private void MainExplorer_HelpRequested(object sender, HelpEventArgs hlpevent) {
            Process.Start(Properties.Settings.Default.HelpUrl);
        }

        private void SubExplorer_HelpRequested(object sender, HelpEventArgs hlpevent) {
            Process.Start(Properties.Settings.Default.HelpUrl);
        }



        private void ShowSearchForm() {
            AppObject.Frame.SearchFormButtonPerformClick();
        }
        private void ShowSearchFormWithTargetDir() {
            string targetDir = this.MainExplorerCombo.Text;
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
            AppObject.Frame.SearchFormButtonPerformClick();
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

        private void FileExplorerForm_Layout(object sender, LayoutEventArgs e) {
        }

        private void FileExplorerForm_Resize(object sender, EventArgs e) {
        }

        private void FileExplorerForm_SizeChanged(object sender, EventArgs e) {
        }

        private void UpwardMainExplorerButton_Click(object sender, EventArgs e) {
            UpwardNavigation(this.MainExplorer);
        }

        private void UpwardSubExplorerButton_Click(object sender, EventArgs e) {
            UpwardNavigation(this.SubExplorer);
        }

        private void UpwardNavigation(ExplorerBrowser eb) {
            string path = eb.NavigationLog.CurrentLocation.ParsingName;
            var parent = System.IO.Directory.GetParent(path);
            if (parent != null) {
                eb.Navigate(ShellObject.FromParsingName(parent.FullName));
            }
        }

        private void OpenExploereButton_Click(object sender, EventArgs e) {
            //ShellObject so = ShellObject.FromParsingName("::{031E4825-7B94-4DC3-B131-E946B44C8DD5}\\Git.library-ms");
            //this.MainExplorer.Navigate(so);

            SetHistory();
        }

        private void SetHistory() {
            this.MainExplorerCombo.Items.Clear();
            foreach (var shellObject in this.MainExplorer.NavigationLog.Locations) {
                if (shellObject.IsFileSystemObject) {
                    this.MainExplorerCombo.Items.Insert(this.MainExplorerCombo.Items.Count, shellObject.ParsingName);
                } else {
                    this.MainExplorerCombo.Items.Insert(this.MainExplorerCombo.Items.Count, shellObject.Name);
                }
            }
        }

        private void MainExplorer_LocationChanged(object sender, EventArgs e) {
            var so = this.MainExplorer.NavigationLog.Locations.Last();
            if (so.IsFileSystemObject) {
                this.MainExplorerCombo.Items.Insert(this.MainExplorerCombo.Items.Count, so.ParsingName);
            } else {
                this.MainExplorerCombo.Items.Insert(this.MainExplorerCombo.Items.Count, so.Name);
            }
        }

        private void FileExplorerForm_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Middle) {
                Debug.Print("MouseButtons.Middle");
            }
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

        private void MainExplorer_Click(object sender, EventArgs e) {
            Debug.Print("MouseButtons.Middle");
        }

        private void FileExplorerForm_Load(object sender, EventArgs e) {
        }

        private void MainExplorerCombo_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                LoadPath(this.MainExplorer, this.MainExplorerCombo.Text);
            }
        }

        private void MainExplorerCombo_SelectedItemChanged(object sender, EventArgs e) {
            int idx = this.MainExplorerCombo.SelectedIndex;
            var so = this.MainExplorer.NavigationLog.Locations.ToArray()[idx];
            this.MainExplorer.Navigate(so);
        }

        private void SubExplorerCombo_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                LoadPath(this.SubExplorer, this.SubExplorerCombo.Text);
            }
        }
        private void SubExplorerCombo_SelectedItemChanged(object sender, EventArgs e) {
            int idx = this.SubExplorerCombo.SelectedIndex;
            var so = this.SubExplorer.NavigationLog.Locations.ToArray()[idx];
            this.SubExplorer.Navigate(so);
        }
    }
}
