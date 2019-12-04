using FxCommonLib.Consts;
using Microsoft.WindowsAPICodePack.Controls;
using Microsoft.WindowsAPICodePack.Controls.WindowsForms;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            //this.MainPathCombo.Text = this.MainExplorer.NavigateLogLocation
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
            } catch (COMException) {
                //HACK メッセージ
                MessageBox.Show("存在しないパスです。", AppObject.MLUtil.GetMsg(CommonConsts.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SubPathCombo_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                ChangePath(this.SubExplorer, this.SubPathCombo);
            }
        }
    }
}
