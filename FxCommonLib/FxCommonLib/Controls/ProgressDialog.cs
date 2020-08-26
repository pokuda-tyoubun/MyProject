using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FxCommonLib.Controls {
    /// <summary>
    /// 進捗ダイアログ
    /// </summary>
    public partial class ProgressDialog : Form {

        /// <summary>BackGroundWorkerに渡す引数</summary>
        private object _workerArgument = null;

        #region コンストラクタ
        /// <summary>
        /// ProgressDialogクラスのコンストラクタ
        /// </summary>
        public ProgressDialog(string caption, DoWorkEventHandler doWork, object argument, bool enabledBreak) {
            InitializeComponent();

            this.StopButton.Visible = enabledBreak;
            this.Text = caption;
            _workerArgument = argument;

            //イベント
            this.Worker.DoWork += doWork;

        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="doWork"></param>
        public ProgressDialog(string caption, DoWorkEventHandler doWork, object argument) : this(caption, doWork, argument, true) {
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="doWork"></param>
        public ProgressDialog(string caption, DoWorkEventHandler doWork) : this(caption, doWork, null, true) {
        }
        #endregion

        /// <summary>
        /// DoWorkイベントハンドラで設定された結果
        /// </summary>
        private object _result = null;
        public object Result {
            get { return this._result; }
        }

        private Exception _error = null;
        /// <summary>
        /// バックグラウンド処理中に発生したエラー
        /// </summary>
        public Exception Error {
            get { return this._error; }
        }

        /// <summary>
        /// 進行状況ダイアログで使用しているBackgroundWorkerクラス
        /// </summary>
        public BackgroundWorker BackgroundWorker {
            get { return this.Worker; }
        }


        /// <summary>
        /// StyleをMarqueeに設定
        /// </summary>
        /// <remarks></remarks>
        public void SetMarquee() {
            this.ProgressBar.Style = ProgressBarStyle.Marquee;
            this.ProgressBar.MarqueeAnimationSpeed = 100;
        }

        /// <summary>
        /// フォームが表示されたときにバックグラウンド処理を開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void ProgressDialog_Shown(object sender, EventArgs e) {
            this.Worker.RunWorkerAsync(this._workerArgument);
        }

        /// <summary>
        /// 中断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void StopButton_Click(object sender, EventArgs e) {
            this.StopButton.Enabled = false;
            this.Worker.CancelAsync();
        }

        /// <summary>
        /// プログレス変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            //プログレスバーの値を変更する
            if (e.ProgressPercentage < this.ProgressBar.Minimum) {
                this.ProgressBar.Value = this.ProgressBar.Minimum;
            } else if (this.ProgressBar.Maximum < e.ProgressPercentage) {
                this.ProgressBar.Value = this.ProgressBar.Maximum;
            } else {
                this.ProgressBar.Value = e.ProgressPercentage;
            }
            //メッセージのテキストを変更する
            this.MessageLabel.Text = (string)e.UserState;
        }

        /// <summary>
        /// バックグラウンド処理が終了したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Error != null) {
                MessageBox.Show("エラーが発生しました。" + "\r\n" + e.Error.Message + "\r\n" + e.Error.StackTrace, "エラー", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this._error = e.Error;
                this.DialogResult = DialogResult.Abort;
            } else if (e.Cancelled) {
                this.DialogResult = DialogResult.Cancel;
            } else {
                this._result = e.Result;
                this.DialogResult = DialogResult.OK;
            }

            this.Close();
        }

        /// <summary>
        /// プログレスバーのVisibleプロパティ
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ProgressVisible {
            get { return this.ProgressBar.Visible; }
            set { this.ProgressBar.Visible = value; }
        }

        /// <summary>
        /// プログレスバーのEnabledプロパティ
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool StopButtonEnabled {
            get { return this.StopButton.Enabled; }
            set { this.StopButton.Enabled = value; }
        }

        private void ProgressDialog_Load(object sender, EventArgs e) {

        }
    }
}
