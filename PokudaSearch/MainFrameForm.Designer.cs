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
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.MainRibbon = new C1.Win.C1Ribbon.C1Ribbon();
            this.ribbonApplicationMenu1 = new C1.Win.C1Ribbon.RibbonApplicationMenu();
            this.ribbonBottomToolBar1 = new C1.Win.C1Ribbon.RibbonBottomToolBar();
            this.ribbonConfigToolBar1 = new C1.Win.C1Ribbon.RibbonConfigToolBar();
            this.ribbonQat1 = new C1.Win.C1Ribbon.RibbonQat();
            this.ribbonTab1 = new C1.Win.C1Ribbon.RibbonTab();
            this.ribbonGroup1 = new C1.Win.C1Ribbon.RibbonGroup();
            this.FileExplorerButton = new C1.Win.C1Ribbon.RibbonButton();
            this.SearchFormButton = new C1.Win.C1Ribbon.RibbonButton();
            this.IndexBuildFormButton = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonGroup2 = new C1.Win.C1Ribbon.RibbonGroup();
            this.ribbonButton1 = new C1.Win.C1Ribbon.RibbonButton();
            this.SandBoxGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.TestFormButton = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonTopToolBar1 = new C1.Win.C1Ribbon.RibbonTopToolBar();
            this.MainStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainRibbon)).BeginInit();
            this.SuspendLayout();
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.ProgressBar});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 602);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(1043, 27);
            this.MainStatusStrip.TabIndex = 4;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(52, 22);
            this.StatusLabel.Text = "ステータス";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(100, 21);
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
            this.MainRibbon.Size = new System.Drawing.Size(1043, 153);
            this.MainRibbon.Tabs.Add(this.ribbonTab1);
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
            // ribbonTab1
            // 
            this.ribbonTab1.Groups.Add(this.ribbonGroup1);
            this.ribbonTab1.Groups.Add(this.ribbonGroup2);
            this.ribbonTab1.Groups.Add(this.SandBoxGroup);
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Text = "タブ";
            // 
            // ribbonGroup1
            // 
            this.ribbonGroup1.Items.Add(this.FileExplorerButton);
            this.ribbonGroup1.Items.Add(this.SearchFormButton);
            this.ribbonGroup1.Items.Add(this.IndexBuildFormButton);
            this.ribbonGroup1.Name = "ribbonGroup1";
            this.ribbonGroup1.Text = "全文検索";
            // 
            // FileExplorerButton
            // 
            this.FileExplorerButton.LargeImage = global::PokudaSearch.Properties.Resources.Explorer24;
            this.FileExplorerButton.Name = "FileExplorerButton";
            this.FileExplorerButton.SmallImage = global::PokudaSearch.Properties.Resources.Explorer24;
            this.FileExplorerButton.Text = "エクスプローラ";
            this.FileExplorerButton.Click += new System.EventHandler(this.FileExplorerButton_Click);
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
            this.IndexBuildFormButton.Text = "インデックス作成";
            this.IndexBuildFormButton.Click += new System.EventHandler(this.IndexBuildFormButton_Click);
            // 
            // ribbonGroup2
            // 
            this.ribbonGroup2.Items.Add(this.ribbonButton1);
            this.ribbonGroup2.Name = "ribbonGroup2";
            this.ribbonGroup2.Text = "分析";
            // 
            // ribbonButton1
            // 
            this.ribbonButton1.Name = "ribbonButton1";
            this.ribbonButton1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.SmallImage")));
            this.ribbonButton1.Text = "抽出語リスト";
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
            // ribbonTopToolBar1
            // 
            this.ribbonTopToolBar1.Name = "ribbonTopToolBar1";
            // 
            // MainFrameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 629);
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
        private C1.Win.C1Ribbon.RibbonTab ribbonTab1;
        private C1.Win.C1Ribbon.RibbonGroup ribbonGroup1;
        private C1.Win.C1Ribbon.RibbonTopToolBar ribbonTopToolBar1;
        private C1.Win.C1Ribbon.RibbonButton SearchFormButton;
        private C1.Win.C1Ribbon.RibbonButton IndexBuildFormButton;
        private C1.Win.C1Ribbon.RibbonGroup SandBoxGroup;
        private C1.Win.C1Ribbon.RibbonButton TestFormButton;
        private C1.Win.C1Ribbon.RibbonButton FileExplorerButton;
        private C1.Win.C1Ribbon.RibbonGroup ribbonGroup2;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton1;
    }
}