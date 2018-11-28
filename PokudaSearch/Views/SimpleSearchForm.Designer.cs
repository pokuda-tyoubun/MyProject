namespace PokudaSearch.Views {
    partial class SimpleSearchForm {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleSearchForm));
            this.label2 = new System.Windows.Forms.Label();
            this.KeywordText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ExtentionText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.UpdateDate2 = new C1.Win.C1Input.C1DateEdit();
            this.UpdateDate1 = new C1.Win.C1Input.C1DateEdit();
            this.FailureFTSNavi = new System.Windows.Forms.BindingNavigator(this.components);
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.WriteExcelButton = new System.Windows.Forms.ToolStripButton();
            this.ResultContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenParentMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchButton = new System.Windows.Forms.Button();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.BottomLeftPanel = new System.Windows.Forms.Panel();
            this.PreviewLabel = new C1.Win.C1SuperTooltip.C1SuperLabel();
            this.WebBrowser = new System.Windows.Forms.WebBrowser();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.PreviewCheck = new System.Windows.Forms.CheckBox();
            this.ResultGrid = new FxCommonLib.Controls.FlexGridEx(this.components);
            this.collapsibleSplitter3 = new NJFLib.Controls.CollapsibleSplitter();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.BrowserProgress = new System.Windows.Forms.ProgressBar();
            this.PreviewSplitter = new NJFLib.Controls.CollapsibleSplitter();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.CopyMenu = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateDate2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateDate1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FailureFTSNavi)).BeginInit();
            this.FailureFTSNavi.SuspendLayout();
            this.ResultContext.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.BottomLeftPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultGrid)).BeginInit();
            this.RightPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(5, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "キーワード";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // KeywordText
            // 
            this.KeywordText.Location = new System.Drawing.Point(77, 5);
            this.KeywordText.Name = "KeywordText";
            this.KeywordText.Size = new System.Drawing.Size(516, 19);
            this.KeywordText.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(5, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "拡張子";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ExtentionText
            // 
            this.ExtentionText.Location = new System.Drawing.Point(77, 30);
            this.ExtentionText.Name = "ExtentionText";
            this.ExtentionText.Size = new System.Drawing.Size(104, 19);
            this.ExtentionText.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(416, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 115;
            this.label3.Text = "～";
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(187, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.TabIndex = 114;
            this.label4.Text = "更新日時";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UpdateDate2
            // 
            this.UpdateDate2.AllowSpinLoop = false;
            this.UpdateDate2.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.UpdateDate2.EmptyAsNull = true;
            this.UpdateDate2.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.UpdateDate2.ImagePadding = new System.Windows.Forms.Padding(0);
            this.UpdateDate2.Location = new System.Drawing.Point(439, 32);
            this.UpdateDate2.Name = "UpdateDate2";
            this.UpdateDate2.ShowFocusRectangle = true;
            this.UpdateDate2.Size = new System.Drawing.Size(154, 17);
            this.UpdateDate2.TabIndex = 6;
            this.UpdateDate2.Tag = null;
            // 
            // UpdateDate1
            // 
            this.UpdateDate1.AllowSpinLoop = false;
            // 
            // 
            // 
            this.UpdateDate1.Calendar.DayNameLength = 1;
            this.UpdateDate1.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.UpdateDate1.EmptyAsNull = true;
            this.UpdateDate1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.UpdateDate1.ImagePadding = new System.Windows.Forms.Padding(0);
            this.UpdateDate1.Location = new System.Drawing.Point(259, 32);
            this.UpdateDate1.Name = "UpdateDate1";
            this.UpdateDate1.ShowFocusRectangle = true;
            this.UpdateDate1.Size = new System.Drawing.Size(154, 17);
            this.UpdateDate1.TabIndex = 5;
            this.UpdateDate1.Tag = null;
            // 
            // FailureFTSNavi
            // 
            this.FailureFTSNavi.AddNewItem = null;
            this.FailureFTSNavi.CountItem = this.toolStripLabel1;
            this.FailureFTSNavi.DeleteItem = null;
            this.FailureFTSNavi.Dock = System.Windows.Forms.DockStyle.None;
            this.FailureFTSNavi.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripTextBox1,
            this.toolStripLabel1,
            this.toolStripSeparator2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripSeparator3,
            this.WriteExcelButton});
            this.FailureFTSNavi.Location = new System.Drawing.Point(8, 55);
            this.FailureFTSNavi.MoveFirstItem = this.toolStripButton1;
            this.FailureFTSNavi.MoveLastItem = this.toolStripButton4;
            this.FailureFTSNavi.MoveNextItem = this.toolStripButton3;
            this.FailureFTSNavi.MovePreviousItem = this.toolStripButton2;
            this.FailureFTSNavi.Name = "FailureFTSNavi";
            this.FailureFTSNavi.PositionItem = this.toolStripTextBox1;
            this.FailureFTSNavi.Size = new System.Drawing.Size(234, 25);
            this.FailureFTSNavi.TabIndex = 8;
            this.FailureFTSNavi.Text = "BindingNavigator1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(37, 22);
            this.toolStripLabel1.Text = "/ {0}";
            this.toolStripLabel1.ToolTipText = "項目の総数";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.RightToLeftAutoMirrorImage = true;
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "最初に移動";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.RightToLeftAutoMirrorImage = true;
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "前に戻る";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.AccessibleName = "位置";
            this.toolStripTextBox1.AutoSize = false;
            this.toolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(50, 23);
            this.toolStripTextBox1.Text = "0";
            this.toolStripTextBox1.ToolTipText = "現在の場所";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.RightToLeftAutoMirrorImage = true;
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "次に移動";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.RightToLeftAutoMirrorImage = true;
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "最後に移動";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // WriteExcelButton
            // 
            this.WriteExcelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.WriteExcelButton.Image = global::PokudaSearch.Properties.Resources.Excel24;
            this.WriteExcelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.WriteExcelButton.Name = "WriteExcelButton";
            this.WriteExcelButton.Size = new System.Drawing.Size(23, 22);
            this.WriteExcelButton.Text = "Excelに出力";
            this.WriteExcelButton.ToolTipText = "Excelに出力";
            this.WriteExcelButton.Click += new System.EventHandler(this.WriteExcelButton_Click);
            // 
            // ResultContext
            // 
            this.ResultContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFileMenu,
            this.OpenParentMenu,
            this.toolStripMenuItem2,
            this.CopyMenu});
            this.ResultContext.Name = "ResultContext";
            this.ResultContext.Size = new System.Drawing.Size(166, 98);
            // 
            // OpenFileMenu
            // 
            this.OpenFileMenu.Name = "OpenFileMenu";
            this.OpenFileMenu.Size = new System.Drawing.Size(165, 22);
            this.OpenFileMenu.Text = "ファイルを開く(&O)";
            this.OpenFileMenu.Click += new System.EventHandler(this.OpenFileMenu_Click);
            // 
            // OpenParentMenu
            // 
            this.OpenParentMenu.Name = "OpenParentMenu";
            this.OpenParentMenu.Size = new System.Drawing.Size(165, 22);
            this.OpenParentMenu.Text = "親フォルダを開く(&P)";
            this.OpenParentMenu.Click += new System.EventHandler(this.OpenParentMenu_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Image = global::PokudaSearch.Properties.Resources.Search24;
            this.SearchButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SearchButton.Location = new System.Drawing.Point(599, 4);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(80, 52);
            this.SearchButton.TabIndex = 2;
            this.SearchButton.Text = "検索(&S)";
            this.SearchButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.BottomLeftPanel);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 427);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(690, 153);
            this.BottomPanel.TabIndex = 116;
            // 
            // BottomLeftPanel
            // 
            this.BottomLeftPanel.Controls.Add(this.PreviewLabel);
            this.BottomLeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomLeftPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomLeftPanel.Name = "BottomLeftPanel";
            this.BottomLeftPanel.Size = new System.Drawing.Size(690, 153);
            this.BottomLeftPanel.TabIndex = 3;
            // 
            // PreviewLabel
            // 
            this.PreviewLabel.BackColor = System.Drawing.SystemColors.Window;
            this.PreviewLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewLabel.Location = new System.Drawing.Point(0, 0);
            this.PreviewLabel.Name = "PreviewLabel";
            this.PreviewLabel.Size = new System.Drawing.Size(690, 153);
            this.PreviewLabel.TabIndex = 0;
            this.PreviewLabel.UseMnemonic = true;
            // 
            // WebBrowser
            // 
            this.WebBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WebBrowser.Location = new System.Drawing.Point(0, 0);
            this.WebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowser.Name = "WebBrowser";
            this.WebBrowser.Size = new System.Drawing.Size(321, 551);
            this.WebBrowser.TabIndex = 0;
            this.WebBrowser.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.WebBrowser_ProgressChanged);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.PreviewCheck);
            this.MainPanel.Controls.Add(this.label2);
            this.MainPanel.Controls.Add(this.KeywordText);
            this.MainPanel.Controls.Add(this.label1);
            this.MainPanel.Controls.Add(this.ExtentionText);
            this.MainPanel.Controls.Add(this.ResultGrid);
            this.MainPanel.Controls.Add(this.label4);
            this.MainPanel.Controls.Add(this.FailureFTSNavi);
            this.MainPanel.Controls.Add(this.label3);
            this.MainPanel.Controls.Add(this.UpdateDate1);
            this.MainPanel.Controls.Add(this.SearchButton);
            this.MainPanel.Controls.Add(this.UpdateDate2);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(690, 419);
            this.MainPanel.TabIndex = 118;
            // 
            // PreviewCheck
            // 
            this.PreviewCheck.AutoSize = true;
            this.PreviewCheck.Checked = true;
            this.PreviewCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PreviewCheck.Location = new System.Drawing.Point(559, 64);
            this.PreviewCheck.Name = "PreviewCheck";
            this.PreviewCheck.Size = new System.Drawing.Size(120, 16);
            this.PreviewCheck.TabIndex = 116;
            this.PreviewCheck.Text = "プレビュー機能を使う";
            this.PreviewCheck.UseVisualStyleBackColor = true;
            this.PreviewCheck.CheckedChanged += new System.EventHandler(this.PreviewCheck_CheckedChanged);
            // 
            // ResultGrid
            // 
            this.ResultGrid.AfterErrorInfo = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("ResultGrid.AfterErrorInfo")));
            this.ResultGrid.AllowEditing = false;
            this.ResultGrid.AllowFiltering = true;
            this.ResultGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.ResultGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultGrid.AutoClipboard = true;
            this.ResultGrid.ColumnInfo = "10,1,0,0,0,90,Columns:";
            this.ResultGrid.ContextMenuStrip = this.ResultContext;
            this.ResultGrid.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.ResultGrid.EnableReadOnlyColor = false;
            this.ResultGrid.EnableUpdateCellStyle = false;
            this.ResultGrid.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.ResultGrid.GridName = null;
            this.ResultGrid.IsCol1SelectCheck = false;
            this.ResultGrid.IsEnterRight = false;
            this.ResultGrid.Location = new System.Drawing.Point(5, 83);
            this.ResultGrid.Name = "ResultGrid";
            this.ResultGrid.PulldownDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>)(resources.GetObject("ResultGrid.PulldownDic")));
            this.ResultGrid.Rows.Count = 2;
            this.ResultGrid.Rows.DefaultSize = 18;
            this.ResultGrid.ShowErrors = true;
            this.ResultGrid.Size = new System.Drawing.Size(682, 333);
            this.ResultGrid.StyleInfo = resources.GetString("ResultGrid.StyleInfo");
            this.ResultGrid.TabIndex = 9;
            this.ResultGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            this.ResultGrid.WindowsName = null;
            this.ResultGrid.SelChange += new System.EventHandler(this.ResultGrid_SelChange);
            this.ResultGrid.OwnerDrawCell += new C1.Win.C1FlexGrid.OwnerDrawCellEventHandler(this.ResultGrid_OwnerDrawCell);
            this.ResultGrid.DoubleClick += new System.EventHandler(this.ResultGrid_DoubleClick);
            // 
            // collapsibleSplitter3
            // 
            this.collapsibleSplitter3.AnimationDelay = 20;
            this.collapsibleSplitter3.AnimationStep = 20;
            this.collapsibleSplitter3.BorderStyle3D = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.collapsibleSplitter3.ControlToHide = this.BottomPanel;
            this.collapsibleSplitter3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.collapsibleSplitter3.ExpandParentForm = false;
            this.collapsibleSplitter3.Location = new System.Drawing.Point(0, 419);
            this.collapsibleSplitter3.Name = "collapsibleSplitter3";
            this.collapsibleSplitter3.Size = new System.Drawing.Size(690, 8);
            this.collapsibleSplitter3.TabIndex = 117;
            this.collapsibleSplitter3.TabStop = false;
            this.collapsibleSplitter3.UseAnimations = false;
            this.collapsibleSplitter3.VisualStyle = NJFLib.Controls.VisualStyles.DoubleDots;
            // 
            // RightPanel
            // 
            this.RightPanel.Controls.Add(this.BrowserProgress);
            this.RightPanel.Controls.Add(this.WebBrowser);
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightPanel.Location = new System.Drawing.Point(698, 0);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(324, 580);
            this.RightPanel.TabIndex = 119;
            // 
            // BrowserProgress
            // 
            this.BrowserProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BrowserProgress.Location = new System.Drawing.Point(4, 554);
            this.BrowserProgress.Name = "BrowserProgress";
            this.BrowserProgress.Size = new System.Drawing.Size(126, 23);
            this.BrowserProgress.TabIndex = 1;
            // 
            // PreviewSplitter
            // 
            this.PreviewSplitter.AnimationDelay = 20;
            this.PreviewSplitter.AnimationStep = 20;
            this.PreviewSplitter.BorderStyle3D = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.PreviewSplitter.ControlToHide = this.RightPanel;
            this.PreviewSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.PreviewSplitter.ExpandParentForm = false;
            this.PreviewSplitter.Location = new System.Drawing.Point(690, 0);
            this.PreviewSplitter.Name = "PreviewSplitter";
            this.PreviewSplitter.Size = new System.Drawing.Size(8, 580);
            this.PreviewSplitter.TabIndex = 120;
            this.PreviewSplitter.TabStop = false;
            this.PreviewSplitter.UseAnimations = false;
            this.PreviewSplitter.VisualStyle = NJFLib.Controls.VisualStyles.DoubleDots;
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(162, 6);
            // 
            // CopyMenu
            // 
            this.CopyMenu.Name = "CopyMenu";
            this.CopyMenu.Size = new System.Drawing.Size(165, 22);
            this.CopyMenu.Text = "コピー(&C) Ctrl+C";
            this.CopyMenu.Click += new System.EventHandler(this.CopyMenu_Click);
            // 
            // SimpleSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 580);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.collapsibleSplitter3);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.PreviewSplitter);
            this.Controls.Add(this.RightPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SimpleSearchForm";
            this.Text = "SimpleSearchForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SimpleSearchForm_FormClosed);
            this.Load += new System.EventHandler(this.SimpleSearchForm_Load);
            this.Shown += new System.EventHandler(this.SimpleSearchForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.UpdateDate2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateDate1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FailureFTSNavi)).EndInit();
            this.FailureFTSNavi.ResumeLayout(false);
            this.FailureFTSNavi.PerformLayout();
            this.ResultContext.ResumeLayout(false);
            this.BottomPanel.ResumeLayout(false);
            this.BottomLeftPanel.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultGrid)).EndInit();
            this.RightPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox KeywordText;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox ExtentionText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1Input.C1DateEdit UpdateDate2;
        private C1.Win.C1Input.C1DateEdit UpdateDate1;
        internal System.Windows.Forms.Button SearchButton;
        private FxCommonLib.Controls.FlexGridEx ResultGrid;
        internal System.Windows.Forms.BindingNavigator FailureFTSNavi;
        internal System.Windows.Forms.ToolStripLabel toolStripLabel1;
        internal System.Windows.Forms.ToolStripButton toolStripButton1;
        internal System.Windows.Forms.ToolStripButton toolStripButton2;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton toolStripButton3;
        internal System.Windows.Forms.ToolStripButton toolStripButton4;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        internal System.Windows.Forms.ToolStripButton WriteExcelButton;
        private System.Windows.Forms.ContextMenuStrip ResultContext;
        private System.Windows.Forms.ToolStripMenuItem OpenFileMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenParentMenu;
        private System.Windows.Forms.Panel BottomPanel;
        private NJFLib.Controls.CollapsibleSplitter collapsibleSplitter3;
        private C1.Win.C1SuperTooltip.C1SuperLabel PreviewLabel;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel BottomLeftPanel;
        private System.Windows.Forms.WebBrowser WebBrowser;
        private System.Windows.Forms.CheckBox PreviewCheck;
        private System.Windows.Forms.Panel RightPanel;
        private NJFLib.Controls.CollapsibleSplitter PreviewSplitter;
        private System.Windows.Forms.ProgressBar BrowserProgress;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem CopyMenu;
    }
}