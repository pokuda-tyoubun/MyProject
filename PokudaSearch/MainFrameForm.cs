using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Win.C1Ribbon;
using PokudaSearch.Views;
using System.Diagnostics;
using PokudaSearch.SandBox;

namespace PokudaSearch {
    public partial class MainFrameForm : Form {

        //HACK Helpが必要
        //HACK OSSのライセンス明記が必要
        //HACK log4netのワーニングを調査
        //HACK スコア順にソートされているか確認


        //-----------------------------------------------------------------------------
        //DONE 右上のChildFormの閉じるボタンが複数でる問題を調査（FxClientでも発生したことがある）

        /// <summary>検索画面</summary>
        public static SimpleSearchForm SimpleSearchForm;
        /// <summary>インデックス作成画面</summary>
        public static IndexBuildForm IndexBuildForm;

        /// <summary>SandBox用</summary>
        public static TestForm TestForm;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainFrameForm() {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrameForm_Load(object sender, EventArgs e) {

        }

        private void MainFrameForm_Shown(object sender, EventArgs e) {
            //NOTE MainFrameFormの表示後に子フォームを表示しないと、閉じるボタンが複数表示されてしまう。
            LoadForms();
        }

        /// <summary>
        /// ステータスバーのメッセージ表示
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isStart"></param>
        /// <param name="sw"></param>
        public void SetStatusMsg(string msg, bool isStart, Stopwatch sw) {
            if (isStart) {
                Cursor.Current = Cursors.WaitCursor;
                sw.Start();
            } else {
                Cursor.Current = Cursors.Default;
                sw.Stop();
                msg = " Time:" + sw.Elapsed.ToString().Substring(0, 12);
            }

            AppObject.Logger.Info(msg);
            StatusLabel.Text = msg;
            if (isStart) {
                this.ProgressBar.Style = ProgressBarStyle.Marquee;
            } else {
                try {
                    this.ProgressBar.Style = ProgressBarStyle.Blocks;
                    this.ProgressBar.Value = 0;
                } catch (NullReferenceException ne) {
                    //強制終了時にNullRefferenceになるので無視する。
                    AppObject.Logger.Info("Ignore->" + ne.Message);
                }
            }
        }

        /// <summary>
        /// ファイル検索画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchFormButton_Click(object sender, EventArgs e) {
            if (SimpleSearchForm == null) {
                SimpleSearchForm = new SimpleSearchForm();
                LoadForm(SimpleSearchForm);
            } else {
                SimpleSearchForm.Activate();
            }
        }

        /// <summary>
        /// インデックス作成画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IndexBuildFormButton_Click(object sender, EventArgs e) {
            if (IndexBuildForm == null) {
                IndexBuildForm = new IndexBuildForm();
                LoadForm(IndexBuildForm);
            } else {
                IndexBuildForm.Activate();
            }
        }

        /// <summary>
        /// 前もって起動する処理
        /// </summary>
        public void LoadForms() {
            try {
                //ファイル検索画面
                SimpleSearchForm = new SimpleSearchForm();
                LoadForm(SimpleSearchForm);
            } finally {
            }
        }

        private void LoadForm(Form frm) {
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void SandBoxGroup_DialogLauncherClick(object sender, EventArgs e) {

        }

        private void TestFormButton_Click(object sender, EventArgs e) {
        }


    }
}
