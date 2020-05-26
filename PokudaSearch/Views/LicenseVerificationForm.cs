using C1.Win.C1Input;
using FxCommonLib.Consts;
using Microsoft.WindowsAPICodePack.Controls;
using Microsoft.WindowsAPICodePack.Controls.WindowsForms;
using Microsoft.WindowsAPICodePack.Shell;
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

namespace PokudaSearch.Views {
    public partial class LicenseVerificationForm : Form {

        public string LicenseKey = "";

        public LicenseVerificationForm(bool isExpired = false) {
            InitializeComponent();

            if (isExpired) {
                this.ExpiredLabel.Visible = true;
            } else {
                this.ExpiredLabel.Visible = false;
            }
        }

        private void ApplyButton_Click(object sender, EventArgs e) {
            //ライセンス認証

            this.Cursor = Cursors.WaitCursor;
            try {
                this.LicenseKey = this.LicenseKeyText.Text.Trim();
                //ダミー送信
                Thread.Sleep(1000);
            } finally {
                this.Cursor = Cursors.Default;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton1_Click(object sender, EventArgs e) {
            this.LicenseKey = "";

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        /// <summary>
        /// ライセンス取得リンクをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetLicenseLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start(Properties.Settings.Default.HelpUrl);
        }
    }
}
