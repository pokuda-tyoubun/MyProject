using C1.Win.C1Input;
using FxCommonLib.Consts;
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
    public partial class ConfigForm : Form {

        private ValueInterval _searchResltRange = new ValueInterval(10, 2000, true, true);
        private ValueInterval _fileSizeLimitRange = new ValueInterval(100, 2000, true, true);
        private ValueInterval _bufferSizeLimitRange = new ValueInterval(128, 2048, true, true);

        public ConfigForm() {
            InitializeComponent();
        }
        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e) {
        }

        private void MainPanel_Paint(object sender, PaintEventArgs e) {

        }

        private void ConfigForm_Load(object sender, EventArgs e) {
            //最大検索結果数
            this.MaxSeachResultNum.PostValidation.Intervals.Add(_searchResltRange);
            this.MaxSearchResultLabel.Text = "検索結果最大件数（" + 
                _searchResltRange.MinValue.ToString() + "～" + _searchResltRange.MaxValue.ToString() + "）";
            this.MaxSeachResultNum.Value = Properties.Settings.Default.MaxSeachResultNum;

            //最大ファイルサイズ
            this.FileSizeLimitNum.PostValidation.Intervals.Add(_fileSizeLimitRange);
            this.FileSizeLimitLabel.Text = "最大ファイルサイズ（" + 
                _fileSizeLimitRange.MinValue.ToString() + "～" + _fileSizeLimitRange.MaxValue.ToString() + "MB）";
            this.FileSizeLimitNum.Value = Properties.Settings.Default.FileSizeLimit;

            //最大Bufferサイズ
            this.BufferSizeLimitNum.PostValidation.Intervals.Add(_bufferSizeLimitRange);
            this.BufferSizeLimitLabel.Text = "メモリ使用量（" + 
                _bufferSizeLimitRange.MinValue.ToString() + "～" + _bufferSizeLimitRange.MaxValue.ToString() + "MB）";
            this.BufferSizeLimitNum.Value = Properties.Settings.Default.BufferSizeLimit;

            //差分ツール
            this.DiffToolText.Text = Properties.Settings.Default.DiffExe;
        }

        private void ApplyButton_Click(object sender, EventArgs e) {
            //最大検索結果数
            MainFrameForm.SearchForm.MaxSeachResultNum = int.Parse(this.MaxSeachResultNum.Text);
            Properties.Settings.Default.MaxSeachResultNum = int.Parse(this.MaxSeachResultNum.Text);

            //最大ファイルサイズ
            Properties.Settings.Default.FileSizeLimit = int.Parse(this.FileSizeLimitNum.Text);
            //最大Bufferサイズ
            Properties.Settings.Default.BufferSizeLimit = int.Parse(this.BufferSizeLimitNum.Text);

            //差分ツール
            Properties.Settings.Default.DiffExe = this.DiffToolText.Text;

            Properties.Settings.Default.Save();

            this.Close();
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton1_Click(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// 差分ツール選択ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefButton_Click(object sender, EventArgs e) {
            OpenFileDialog ofDialog = new OpenFileDialog();
            //ダイアログのタイトルを指定する
            ofDialog.Title = "差分ツール選択";
 
            //ダイアログを表示する
            if (ofDialog.ShowDialog() == DialogResult.OK) {
                this.DiffToolText.Text = ofDialog.FileName;
            }
 
            // オブジェクトを破棄する
            ofDialog.Dispose();
        }
    }
}
