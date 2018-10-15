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

        private void CreateIndexButton_Click(object sender, EventArgs e) {
            LuceneIndexBuilder.TextExtractMode mode = LuceneIndexBuilder.TextExtractMode.Tika;
            if (this.IFilterRadio.Checked) {
                mode = LuceneIndexBuilder.TextExtractMode.IFilter;
            }
            LuceneIndexBuilder lib = new LuceneIndexBuilder(this.LogViewerText, mode);

            Directory.CreateDirectory(AppObject.RootDirPath);
            Directory.CreateDirectory(AppObject.RootDirPath + Consts.IndexDirName);
            Directory.CreateDirectory(AppObject.RootDirPath + Consts.BuildDirName);

            Stopwatch sw = new Stopwatch();
            AppObject.Frame.SetStatusMsg(AppObject.MLUtil.GetMsg(CommonConsts.ACT_SEARCH), true, sw);
            try {
                //lib.CreateIndex(AppObject.AppAnalyzer, 
                //                AppObject.RootDirPath, 
                //                Consts.IndexDirName, Consts.BuildDirName, this.TargetDirText.Text);
                lib.CreateMultiFSIndex(AppObject.AppAnalyzer, 
                                AppObject.RootDirPath, 
                                Consts.IndexDirName, Consts.BuildDirName, this.TargetDirText.Text);
            } finally {
                this.Activate();
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
    }
}
