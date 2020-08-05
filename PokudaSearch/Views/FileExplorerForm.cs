using FlexLucene.Store;
using FxCommonLib.Consts;
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

        //HACK*以下を参考い一通り実装する。
        //https://github.com/aybe/Windows-API-Code-Pack-1.1/blob/master/source/Samples/ExplorerBrowser/CS/WinForms/ExplorerBrowserTestForm.cs

        public FileExplorerForm() {
            InitializeComponent();
        }

        private void FileExplorerForm_Shown(object sender, EventArgs e) {
            this.MainExplorer.Navigate((ShellObject)KnownFolders.Desktop);

            this.SubExplorer.NavigationOptions.PaneVisibility.Navigation = PaneVisibilityState.Hide;
            this.SubExplorer.Navigate((ShellObject)KnownFolders.Desktop);
        }

        /// <summary>
        /// NOTE:イベント発火せず。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainExplorer_KeyDown(object sender, KeyEventArgs e) {
        }

        //HACK 左側のペインの十字キーが反応しない。

        private string _command = "";
        /// <summary>
        /// NOTE:KeyDownイベントはハンドリングできない。(イベントが発火しない)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainExplorer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
            //Viライクな操作
            if (e.KeyCode == Keys.J) {
                SendKeys.Send("{DOWN}");
                return;
            }
            if (e.KeyCode == Keys.K) {
                SendKeys.Send("{UP}");
                return;
            }
            if (e.KeyCode == Keys.L) {
                SendKeys.Send("{RIGHT}");
                return;
            }
            if (e.KeyCode == Keys.H) {
                SendKeys.Send("{LEFT}");
                return;
            }
            if ((e.Modifiers & Keys.Alt) == Keys.Alt && e.KeyCode == Keys.Left) {
                this.MainExplorer.NavigateLogLocation(NavigationLogDirection.Backward);
                return;
            }
            if ((e.Modifiers & Keys.Alt) == Keys.Alt && e.KeyCode == Keys.Right) {
                this.MainExplorer.NavigateLogLocation(NavigationLogDirection.Forward);
                return;
            }
            if ((e.Modifiers & Keys.Alt) == Keys.Alt && e.KeyCode == Keys.Up) {
                string path = this.MainExplorer.NavigationLog.CurrentLocation.ParsingName;
                var parent = System.IO.Directory.GetParent(path);
                this.MainExplorer.Navigate(ShellObject.FromParsingName(parent.FullName));
                return;
            }

            if (e.KeyCode == Keys.Oem1) {
                _command = ":";
                return;
            }
            if (e.KeyCode == Keys.W && _command == ":") {
                this.SubExplorer.PerformAutoScale();
                _command = "";
                return;
            }
        }

        private void MainExplorer_NavigationComplete(object sender, NavigationCompleteEventArgs e) {
            //this.mainPathCombo.Text = this.MainExplorer.NavigateLogLocation
            this.MainPathCombo.Text = e.NewLocation.ParsingName;
            this.Text = e.NewLocation.Name;
        }

        private void BackwardButton_Click(object sender, EventArgs e) {
            this.MainExplorer.NavigateLogLocation(NavigationLogDirection.Backward);
        }

        private void SubExplorer_NavigationComplete(object sender, NavigationCompleteEventArgs e) {
            this.SubPathCombo.Text = e.NewLocation.ParsingName;
        }

        private void MainPathCombo_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                ChangePath(this.MainExplorer, this.MainPathCombo);
            }
        }
        private void ChangePath(ExplorerBrowser eb, ToolStripComboBox tcb) {
            try {
                eb.Navigate(ShellFileSystemFolder.FromFolderPath(tcb.Text));
            } catch (DirectoryNotFoundException) {
                //スルーする
            } catch (COMException) {
                MessageBox.Show(AppObject.GetMsg(AppObject.Msg.ERR_INVALID_PATH), 
                    AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SubPathCombo_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                ChangePath(this.SubExplorer, this.SubPathCombo);
            }
        }

        private void FileExplorerForm_FormClosed(object sender, FormClosedEventArgs e) {
            MainFrameForm.FileExplorerForm = null;
        }

        private void ForwardButton_Click(object sender, EventArgs e) {
            this.MainExplorer.NavigateLogLocation(NavigationLogDirection.Forward);
        }

        private void BackwardSubButton_Click(object sender, EventArgs e) {
            this.SubExplorer.NavigateLogLocation(NavigationLogDirection.Backward);
        }

        private void ForwardSubButton_Click(object sender, EventArgs e) {
            this.SubExplorer.NavigateLogLocation(NavigationLogDirection.Forward);
        }

        private void MainToolStrip_SizeChanged(object sender, EventArgs e) {
            this.MainPathCombo.Width = (int)Math.Floor(this.MainToolStrip.Width * 0.9);
        }

        private void MainExplorer_HelpRequested(object sender, HelpEventArgs hlpevent) {
            Process.Start(Properties.Settings.Default.HelpUrl);
        }

        private void SubExplorer_HelpRequested(object sender, HelpEventArgs hlpevent) {
            Process.Start(Properties.Settings.Default.HelpUrl);
        }

        private void OnDemandSearchButton_Click(object sender, EventArgs e) {
            if (this.MainExplorer.SelectedItems.Count > 0) {
                string path = this.MainExplorer.SelectedItems[0].GetDisplayName(DisplayNameType.FileSystemPath);
                if (System.IO.Directory.Exists(path)) {
                    var ram = new RAMDirectory();
                    //オンデマンドインデックス作成
                    var ibf = new IndexBuildForm(path, ram);
                    ibf.ShowDialog();

                    //オンデマンド検索

                    ram = null;
                    GC.Collect();
                }
            }
        }

        private void CreateIndexButton_Click(object sender, EventArgs e) {

        }
    }
}
