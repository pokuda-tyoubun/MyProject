using C1.Win.C1Input;
using FxCommonLib.Consts;
using FxCommonLib.Utils;
using Microsoft.WindowsAPICodePack.Controls;
using Microsoft.WindowsAPICodePack.Controls.WindowsForms;
using Microsoft.WindowsAPICodePack.Shell;
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
    public partial class InteractionPathForm : Form {
        
        public string RemotePath;
        public string LocalPath;

        public InteractionPathForm() {
            InitializeComponent();

            RemotePath = "";
            LocalPath = "";
        }
        public InteractionPathForm(string remotePath, string localPath) {
            InitializeComponent();

            RemotePath = remotePath;
            LocalPath = localPath;
            this.RemotePathText.Text = remotePath;
            this.LocalPathText.Text = localPath;
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, EventArgs e) {
            string remotePath = this.RemotePathText.Text;
            string localPath = this.LocalPathText.Text;
            //末尾の\を除去
            if (remotePath.EndsWith(@"\")) {
                remotePath = remotePath.Remove(remotePath.Length - 1);
            }
            if (localPath.EndsWith(@"\")) {
                localPath = localPath.Remove(localPath.Length - 1);
            }

            RemotePath = remotePath;
            LocalPath = localPath;

            DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton1_Click(object sender, EventArgs e) {
            RemotePath = "";
            LocalPath = "";

            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// ローカルパス選択ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefButton_Click(object sender, EventArgs e) {
            string selectedPath = FileUtil.GetSelectedDirectory("ローカルパス選択", this.LocalPathText.Text);
            if (!String.IsNullOrEmpty(selectedPath)) {
                this.LocalPathText.Text = selectedPath;
            }
        }

    }
}
