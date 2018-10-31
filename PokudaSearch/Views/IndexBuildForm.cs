using FxCommonLib.Consts;
using FxCommonLib.Utils;
using PokudaSearch.IndexBuilder;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PokudaSearch.Views {
    public partial class IndexBuildForm : Form {


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IndexBuildForm() {
            InitializeComponent();
        }

        private void SetProgressValue(ProgressReport report) {
            this.ProgressBar.Value = report.Percent;
            this.LogViewerText.Text = report.ProgressCount.ToString() + "/" + report.TotalCount.ToString();
        }

        private void CreateIndexButton_Click(object sender, EventArgs e) {
            LuceneIndexWorker.TextExtractMode mode = LuceneIndexWorker.TextExtractMode.Tika;
            if (this.IFilterRadio.Checked) {
                mode = LuceneIndexWorker.TextExtractMode.IFilter;
            }

            Stopwatch sw = new Stopwatch();
            AppObject.Frame.SetStatusMsg(AppObject.MLUtil.GetMsg(CommonConsts.ACT_PROCESSING), true, sw);
            try {
                //LuceneIndexBuilder lib = new LuceneIndexBuilder(this.LogViewerText, LuceneIndexBuilder.TextExtractMode.IFilter);
                //lib.CreateIndex(AppObject.AppAnalyzer, 
                //                AppObject.RootDirPath, 
                //                LuceneIndexBuilder.IndexDirName, LuceneIndexBuilder.BuildDirName, this.TargetDirText.Text);
                //lib.CreateMultiFSIndex(AppObject.AppAnalyzer, 
                //                AppObject.RootDirPath, 
                //                Consts.IndexDirName, Consts.BuildDirName, this.TargetDirText.Text);

                var progress = new Progress<ProgressReport>(SetProgressValue);
                LuceneIndexWorker.CreateIndexBySingleThread(
                    AppObject.AppAnalyzer, 
                    AppObject.RootDirPath, 
                    this.TargetDirText.Text,
                    progress,
                    mode);
                //Multri RAMDirectoryで構築
                //LuceneIndexWorker.CreateIndexByMultiRAM(
                //    AppObject.AppAnalyzer, 
                //    AppObject.RootDirPath, 
                //    this.TargetDirText.Text,
                //    progress,
                //    mode);
            } finally {
                AppObject.Frame.SetStatusMsg(AppObject.MLUtil.GetMsg(CommonConsts.ACT_END), false, sw);
            }
        }

        /// <summary>
        /// 参照ボタンをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferenceButton_Click(object sender, EventArgs e) {
            string selectedPath = FileUtil.GetSelectedDirectory("検索対象フォルダ選択", Properties.Settings.Default.InitIndexPath);
            if (!String.IsNullOrEmpty(selectedPath)) {
                this.TargetDirText.Text = selectedPath;
            }
        }

        private void IndexBuildForm_FormClosed(object sender, FormClosedEventArgs e) {
            MainFrameForm.IndexBuildForm = null;
        }

        private void IndexBuildForm_Load(object sender, EventArgs e) {
            this.TargetDirText.Text = Properties.Settings.Default.InitIndexPath;
        }

        private void MergeIndexButton_Click(object sender, EventArgs e) {
            Stopwatch sw = new Stopwatch();
            AppObject.Frame.SetStatusMsg(AppObject.MLUtil.GetMsg(CommonConsts.ACT_SEARCH), true, sw);
            try {
                LuceneIndexWorker.MergeAndMove(
                    AppObject.AppAnalyzer, 
                    AppObject.RootDirPath, 
                    this.TargetDirText.Text);
            } finally {
                AppObject.Frame.SetStatusMsg(AppObject.MLUtil.GetMsg(CommonConsts.ACT_END), false, sw);
            }
        }
    }
}
