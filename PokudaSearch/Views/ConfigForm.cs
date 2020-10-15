using C1.Win.C1Input;
using FxCommonLib.Consts;
using FxCommonLib.Utils;
using FxCommonLib.Win32API;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokudaSearch.Views {
    public partial class ConfigForm : Form {

        /// <summary>検索結果数の有効範囲</summary>
        private ValueInterval _searchResltRange = new ValueInterval(10, 2000, true, true);
        /// <summary>類似結果数の有効範囲</summary>
        private ValueInterval _moreLikeThisResultRange = new ValueInterval(10, 100, true, true);
        /// <summary>最大ファイルサイズの有効範囲</summary>
        private ValueInterval _fileSizeLimitRange = new ValueInterval(100, 2000, true, true);
        /// <summary>最大使用メモリサイズの有効範囲</summary>
        private ValueInterval _bufferSizeLimitRange = new ValueInterval(128, 2048, true, true);

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConfigForm() {
            InitializeComponent();
        }
        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e) {
        }

        private void ConfigForm_Load(object sender, EventArgs e) {
            //最大検索結果数
            this.MaxSearchResultNum.PostValidation.Intervals.Add(_searchResltRange);
            this.MaxSearchResultLabel.Text = "検索結果最大件数（" + 
                _searchResltRange.MinValue.ToString() + "～" + _searchResltRange.MaxValue.ToString() + "）";
            this.MaxSearchResultNum.Value = Properties.Settings.Default.MaxSearchResultNum;

            //最大類似検索結果数
            this.MaxMoreLikeThisResultNum.PostValidation.Intervals.Add(_moreLikeThisResultRange);
            this.MaxMoreLikeThisResultLabel.Text = "類似検索結果最大件数（" + 
                _moreLikeThisResultRange.MinValue.ToString() + "～" + _moreLikeThisResultRange.MaxValue.ToString() + "）";
            this.MaxMoreLikeThisResultNum.Value = Properties.Settings.Default.MaxMoreLikeThisResultNum;

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

            //デフォルト対象インデックス
            this.LocalIndexCheck.Checked = Properties.Settings.Default.LocalIndexChecked;
            this.OuterIndexCheck.Checked = Properties.Settings.Default.OuterIndexChecked;

            //もしかしてキーワード
            this.ShowSuggestionCheck.Checked = Properties.Settings.Default.ShowSuggestion;
        }

        /// <summary>
        /// 適用ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyButton_Click(object sender, EventArgs e) {
            string diffToolPath = this.DiffToolText.Text;
            //差分ツールのパスチェック
            if (diffToolPath != "" && !File.Exists(diffToolPath)) {
                MessageBox.Show(string.Format(AppObject.GetMsg(AppObject.Msg.ERR_X_FILE_NOT_FOUND), "差分ツール"),
                    AppObject.GetMsg(AppObject.Msg.TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            //最大検索結果数
            MainFrameForm.SearchForm.MaxSearchResultNum = int.Parse(this.MaxSearchResultNum.Text);
            Properties.Settings.Default.MaxSearchResultNum = int.Parse(this.MaxSearchResultNum.Text);

            //最大類似検索結果数
            MainFrameForm.SearchForm.MaxMoreLikeThisResultNum = int.Parse(this.MaxMoreLikeThisResultNum.Text);
            Properties.Settings.Default.MaxMoreLikeThisResultNum = int.Parse(this.MaxMoreLikeThisResultNum.Text);

            //最大ファイルサイズ
            Properties.Settings.Default.FileSizeLimit = int.Parse(this.FileSizeLimitNum.Text);
            //最大Bufferサイズ
            Properties.Settings.Default.BufferSizeLimit = int.Parse(this.BufferSizeLimitNum.Text);

            //差分ツール
            Properties.Settings.Default.DiffExe = diffToolPath;

            //ローカルインデックス
            Properties.Settings.Default.LocalIndexChecked = this.LocalIndexCheck.Checked;
            //外部インデックス
            Properties.Settings.Default.OuterIndexChecked = this.OuterIndexCheck.Checked;

            //「もしかしてキーワード」表示
            Properties.Settings.Default.ShowSuggestion = this.ShowSuggestionCheck.Checked;

            Properties.Settings.Default.Save();

            if (MainFrameForm.SearchForm != null) {
                //差分ツールの有効／無効を切替え
                if (StringUtil.NullToBlank(Properties.Settings.Default.DiffExe) == "") {
                    MainFrameForm.SearchForm.DiffMenu.Enabled = false;
                } else {
                    MainFrameForm.SearchForm.DiffMenu.Enabled = true;
                }

                //ローカルインデックス／外部インデックスのチェック
                MainFrameForm.SearchForm.CheckedLocalIndex(this.LocalIndexCheck.Checked);
                MainFrameForm.SearchForm.CheckedOuterIndex(this.OuterIndexCheck.Checked);
            }

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

        //TODO デバッグ検証用　後で削除
        protected override void WndProc(ref Message m) {
            base.WndProc(ref m);

            Debug.Print("Msg=" + m.Msg);
            Debug.Print("WParam=" + m.WParam.ToInt32());
            Debug.Print("LParam=" + m.LParam.ToInt32());
            Debug.Print("--GET_XBUTTON_WPARAM=" + Macros.GET_XBUTTON_WPARAM((uint)m.WParam.ToInt32()));
        }
    }
}
