namespace PokudaSearch {
    partial class MainFrameForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrameForm));
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MorphemeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainRibbon = new C1.Win.C1Ribbon.C1Ribbon();
            this.ribbonApplicationMenu1 = new C1.Win.C1Ribbon.RibbonApplicationMenu();
            this.ribbonBottomToolBar1 = new C1.Win.C1Ribbon.RibbonBottomToolBar();
            this.ribbonConfigToolBar1 = new C1.Win.C1Ribbon.RibbonConfigToolBar();
            this.ribbonQat1 = new C1.Win.C1Ribbon.RibbonQat();
            this.MainTab = new C1.Win.C1Ribbon.RibbonTab();
            this.FullTextSearchGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.FileExplorerFormButton = new C1.Win.C1Ribbon.RibbonButton();
            this.SearchFormButton = new C1.Win.C1Ribbon.RibbonButton();
            this.IndexBuildFormButton = new C1.Win.C1Ribbon.RibbonButton();
            this.TagGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.TagEditFormButton = new C1.Win.C1Ribbon.RibbonButton();
            this.AnalyzeGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.ribbonButton1 = new C1.Win.C1Ribbon.RibbonButton();
            this.SandBoxGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.TestFormButton = new C1.Win.C1Ribbon.RibbonButton();
            this.HelpGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.ConfigButton = new C1.Win.C1Ribbon.RibbonButton();
            this.VerifyLicenseButton = new C1.Win.C1Ribbon.RibbonButton();
            this.HelpButton1 = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonTopToolBar1 = new C1.Win.C1Ribbon.RibbonTopToolBar();
            this.MainStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainRibbon)).BeginInit();
            this.SuspendLayout();
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgressBar,
            this.StatusLabel,
            this.MorphemeLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 834);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(1284, 27);
            this.MainStatusStrip.TabIndex = 4;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(100, 21);
            // 
            // StatusLabel
            // 
            this.StatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.StatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(66, 22);
            this.StatusLabel.Text = "コマンド待ち";
            // 
            // MorphemeLabel
            // 
            this.MorphemeLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.MorphemeLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.MorphemeLabel.Name = "MorphemeLabel";
            this.MorphemeLabel.Size = new System.Drawing.Size(59, 22);
            this.MorphemeLabel.Text = "形態素：";
            // 
            // MainRibbon
            // 
            this.MainRibbon.ApplicationMenuHolder = this.ribbonApplicationMenu1;
            this.MainRibbon.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.MainRibbon.BottomToolBarHolder = this.ribbonBottomToolBar1;
            this.MainRibbon.ConfigToolBarHolder = this.ribbonConfigToolBar1;
            this.MainRibbon.Location = new System.Drawing.Point(0, 0);
            this.MainRibbon.Name = "MainRibbon";
            this.MainRibbon.QatHolder = this.ribbonQat1;
            this.MainRibbon.Size = new System.Drawing.Size(1284, 153);
            this.MainRibbon.Tabs.Add(this.MainTab);
            this.MainRibbon.TopToolBarHolder = this.ribbonTopToolBar1;
            this.MainRibbon.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2010Black;
            // 
            // ribbonApplicationMenu1
            // 
            this.ribbonApplicationMenu1.Name = "ribbonApplicationMenu1";
            // 
            // ribbonBottomToolBar1
            // 
            this.ribbonBottomToolBar1.Name = "ribbonBottomToolBar1";
            // 
            // ribbonConfigToolBar1
            // 
            this.ribbonConfigToolBar1.Name = "ribbonConfigToolBar1";
            // 
            // ribbonQat1
            // 
            this.ribbonQat1.Name = "ribbonQat1";
            // 
            // MainTab
            // 
            this.MainTab.Groups.Add(this.FullTextSearchGroup);
            this.MainTab.Groups.Add(this.TagGroup);
            this.MainTab.Groups.Add(this.AnalyzeGroup);
            this.MainTab.Groups.Add(this.SandBoxGroup);
            this.MainTab.Groups.Add(this.HelpGroup);
            this.MainTab.Name = "MainTab";
            this.MainTab.Text = "メイン";
            // 
            // FullTextSearchGroup
            // 
            this.FullTextSearchGroup.Items.Add(this.FileExplorerFormButton);
            this.FullTextSearchGroup.Items.Add(this.SearchFormButton);
            this.FullTextSearchGroup.Items.Add(this.IndexBuildFormButton);
            this.FullTextSearchGroup.Name = "FullTextSearchGroup";
            this.FullTextSearchGroup.Text = "全文検索";
            // 
            // FileExplorerFormButton
            // 
            this.FileExplorerFormButton.LargeImage = global::PokudaSearch.Properties.Resources.Explorer24;
            this.FileExplorerFormButton.Name = "FileExplorerFormButton";
            this.FileExplorerFormButton.SmallImage = global::PokudaSearch.Properties.Resources.Explorer24;
            this.FileExplorerFormButton.Text = "エクスプローラ";
            this.FileExplorerFormButton.Click += new System.EventHandler(this.FileExplorerFormButton_Click);
            // 
            // SearchFormButton
            // 
            this.SearchFormButton.LargeImage = ((System.Drawing.Image)(resources.GetObject("SearchFormButton.LargeImage")));
            this.SearchFormButton.Name = "SearchFormButton";
            this.SearchFormButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("SearchFormButton.SmallImage")));
            this.SearchFormButton.Text = "ファイル検索";
            this.SearchFormButton.Click += new System.EventHandler(this.SearchFormButton_Click);
            // 
            // IndexBuildFormButton
            // 
            this.IndexBuildFormButton.LargeImage = ((System.Drawing.Image)(resources.GetObject("IndexBuildFormButton.LargeImage")));
            this.IndexBuildFormButton.Name = "IndexBuildFormButton";
            this.IndexBuildFormButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("IndexBuildFormButton.SmallImage")));
            this.IndexBuildFormButton.Text = "インデックス管理";
            this.IndexBuildFormButton.Click += new System.EventHandler(this.IndexBuildFormButton_Click);
            // 
            // TagGroup
            // 
            this.TagGroup.Items.Add(this.TagEditFormButton);
            this.TagGroup.Name = "TagGroup";
            this.TagGroup.Text = "タグ編集";
            // 
            // TagEditFormButton
            // 
            this.TagEditFormButton.Name = "TagEditFormButton";
            this.TagEditFormButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("TagEditFormButton.SmallImage")));
            this.TagEditFormButton.Text = "ボタン";
            this.TagEditFormButton.Click += new System.EventHandler(this.TagEditFormButton_Click);
            // 
            // AnalyzeGroup
            // 
            this.AnalyzeGroup.Items.Add(this.ribbonButton1);
            this.AnalyzeGroup.Name = "AnalyzeGroup";
            this.AnalyzeGroup.Text = "分析";
            // 
            // ribbonButton1
            // 
            this.ribbonButton1.Name = "ribbonButton1";
            this.ribbonButton1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.SmallImage")));
            this.ribbonButton1.Text = "抽出語リスト";
            this.ribbonButton1.Click += new System.EventHandler(this.ribbonButton1_Click);
            // 
            // SandBoxGroup
            // 
            this.SandBoxGroup.Items.Add(this.TestFormButton);
            this.SandBoxGroup.Name = "SandBoxGroup";
            this.SandBoxGroup.Text = "SandBox";
            this.SandBoxGroup.DialogLauncherClick += new System.EventHandler(this.SandBoxGroup_DialogLauncherClick);
            // 
            // TestFormButton
            // 
            this.TestFormButton.Name = "TestFormButton";
            this.TestFormButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("TestFormButton.SmallImage")));
            this.TestFormButton.Text = "TestForm";
            this.TestFormButton.Click += new System.EventHandler(this.TestFormButton_Click);
            // 
            // HelpGroup
            // 
            this.HelpGroup.Items.Add(this.ConfigButton);
            this.HelpGroup.Items.Add(this.VerifyLicenseButton);
            this.HelpGroup.Items.Add(this.HelpButton1);
            this.HelpGroup.Name = "HelpGroup";
            this.HelpGroup.Text = "ヘルプ";
            // 
            // ConfigButton
            // 
            this.ConfigButton.LargeImage = ((System.Drawing.Image)(resources.GetObject("ConfigButton.LargeImage")));
            this.ConfigButton.Name = "ConfigButton";
            this.ConfigButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("ConfigButton.SmallImage")));
            this.ConfigButton.Text = "設定";
            this.ConfigButton.Click += new System.EventHandler(this.ConfigButton_Click);
            // 
            // VerifyLicenseButton
            // 
            this.VerifyLicenseButton.LargeImage = ((System.Drawing.Image)(resources.GetObject("VerifyLicenseButton.LargeImage")));
            this.VerifyLicenseButton.Name = "VerifyLicenseButton";
            this.VerifyLicenseButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("VerifyLicenseButton.SmallImage")));
            this.VerifyLicenseButton.Text = "ライセンス認証";
            this.VerifyLicenseButton.Click += new System.EventHandler(this.VerifyLicenseButton_Click);
            // 
            // HelpButton1
            // 
            this.HelpButton1.LargeImage = ((System.Drawing.Image)(resources.GetObject("HelpButton1.LargeImage")));
            this.HelpButton1.Name = "HelpButton1";
            this.HelpButton1.SmallImage = ((System.Drawing.Image)(resources.GetObject("HelpButton1.SmallImage")));
            this.HelpButton1.Text = "ヘルプサイト";
            this.HelpButton1.Click += new System.EventHandler(this.HelpButton1_Click);
            // 
            // ribbonTopToolBar1
            // 
            this.ribbonTopToolBar1.Name = "ribbonTopToolBar1";
            // 
            // MainFrameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 861);
            this.Controls.Add(this.MainRibbon);
            this.Controls.Add(this.MainStatusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainFrameForm";
            this.Text = "PokudaSearch";
            this.Load += new System.EventHandler(this.MainFrameForm_Load);
            this.Shown += new System.EventHandler(this.MainFrameForm_Shown);
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainRibbon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private C1.Win.C1Ribbon.C1Ribbon MainRibbon;
        private C1.Win.C1Ribbon.RibbonApplicationMenu ribbonApplicationMenu1;
        private C1.Win.C1Ribbon.RibbonBottomToolBar ribbonBottomToolBar1;
        private C1.Win.C1Ribbon.RibbonConfigToolBar ribbonConfigToolBar1;
        private C1.Win.C1Ribbon.RibbonQat ribbonQat1;
        private C1.Win.C1Ribbon.RibbonTab MainTab;
        private C1.Win.C1Ribbon.RibbonGroup FullTextSearchGroup;
        private C1.Win.C1Ribbon.RibbonTopToolBar ribbonTopToolBar1;
        private C1.Win.C1Ribbon.RibbonButton SearchFormButton;
        private C1.Win.C1Ribbon.RibbonButton IndexBuildFormButton;
        private C1.Win.C1Ribbon.RibbonGroup SandBoxGroup;
        private C1.Win.C1Ribbon.RibbonButton TestFormButton;
        private C1.Win.C1Ribbon.RibbonButton FileExplorerFormButton;
        private C1.Win.C1Ribbon.RibbonGroup AnalyzeGroup;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton1;
        private C1.Win.C1Ribbon.RibbonGroup HelpGroup;
        private C1.Win.C1Ribbon.RibbonButton HelpButton1;
        private C1.Win.C1Ribbon.RibbonButton ConfigButton;
        private C1.Win.C1Ribbon.RibbonGroup TagGroup;
        private C1.Win.C1Ribbon.RibbonButton TagEditFormButton;
        private C1.Win.C1Ribbon.RibbonButton VerifyLicenseButton;
        private System.Windows.Forms.ToolStripStatusLabel MorphemeLabel;
    }
}