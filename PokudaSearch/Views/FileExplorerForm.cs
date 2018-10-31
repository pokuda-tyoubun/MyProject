using Microsoft.WindowsAPICodePack.Controls;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        }

        private void MainExplorer_NavigationComplete(object sender, NavigationCompleteEventArgs e) {
            //this.MainPathCombo.Text = this.MainExplorer.NavigateLogLocation
        }

        private void BackwardButton_Click(object sender, EventArgs e) {
            this.MainExplorer.NavigateLogLocation(NavigationLogDirection.Backward);
        }
    }
}
