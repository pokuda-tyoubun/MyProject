namespace PokudaSearch.Views {
    partial class IndexBuildForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IndexBuildForm));
            this.ReferenceButton = new System.Windows.Forms.Button();
            this.CreateIndexButton = new System.Windows.Forms.Button();
            this.LogViewerText = new System.Windows.Forms.TextBox();
            this.TikaRadio = new System.Windows.Forms.RadioButton();
            this.IFilterRadio = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.MergeIndexButton = new System.Windows.Forms.Button();
            this.IndexHistoryGrid = new FxCommonLib.Controls.FlexGridEx(this.components);
            this.TargetDirText = new FxCommonLib.Controls.FilePathTextBox(this.components);
            this.UpdateIndexButton = new System.Windows.Forms.Button();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.collapsibleSplitter3 = new NJFLib.Controls.CollapsibleSplitter();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ReservedGrid = new FxCommonLib.Controls.FlexGridEx(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IndexHistoryGrid)).BeginInit();
            this.BottomPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReservedGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ReferenceButton
            // 
            this.ReferenceButton.Location = new System.Drawing.Point(473, 3);
            this.ReferenceButton.Name = "ReferenceButton";
            this.ReferenceButton.Size = new System.Drawing.Size(65, 22);
            this.ReferenceButton.TabIndex = 6;
            this.ReferenceButton.Text = "参照...";
            this.ReferenceButton.UseVisualStyleBackColor = true;
            this.ReferenceButton.Click += new System.EventHandler(this.ReferenceButton_Click);
            // 
            // CreateIndexButton
            // 
            this.CreateIndexButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CreateIndexButton.Location = new System.Drawing.Point(592, 29);
            this.CreateIndexButton.Name = "CreateIndexButton";
            this.CreateIndexButton.Size = new System.Drawing.Size(126, 39);
            this.CreateIndexButton.TabIndex = 14;
            this.CreateIndexButton.Text = "インデックス再作成(&B)";
            this.CreateIndexButton.UseVisualStyleBackColor = true;
            this.CreateIndexButton.Click += new System.EventHandler(this.CreateIndexButton_Click);
            // 
            // LogViewerText
            // 
            this.LogViewerText.Location = new System.Drawing.Point(3, 74);
            this.LogViewerText.Multiline = true;
            this.LogViewerText.Name = "LogViewerText";
            this.LogViewerText.ReadOnly = true;
            this.LogViewerText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogViewerText.Size = new System.Drawing.Size(789, 63);
            this.LogViewerText.TabIndex = 15;
            // 
            // TikaRadio
            // 
            this.TikaRadio.AutoSize = true;
            this.TikaRadio.Checked = true;
            this.TikaRadio.Location = new System.Drawing.Point(6, 17);
            this.TikaRadio.Name = "TikaRadio";
            this.TikaRadio.Size = new System.Drawing.Size(87, 16);
            this.TikaRadio.TabIndex = 16;
            this.TikaRadio.TabStop = true;
            this.TikaRadio.Text = "Apache Tika";
            this.TikaRadio.UseVisualStyleBackColor = true;
            // 
            // IFilterRadio
            // 
            this.IFilterRadio.AutoSize = true;
            this.IFilterRadio.Location = new System.Drawing.Point(110, 17);
            this.IFilterRadio.Name = "IFilterRadio";
            this.IFilterRadio.Size = new System.Drawing.Size(53, 16);
            this.IFilterRadio.TabIndex = 17;
            this.IFilterRadio.Text = "IFilter";
            this.IFilterRadio.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TikaRadio);
            this.groupBox1.Controls.Add(this.IFilterRadio);
            this.groupBox1.Location = new System.Drawing.Point(5, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 39);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "テキストコンバータ";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(224, 46);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(241, 22);
            this.ProgressBar.TabIndex = 19;
            // 
            // MergeIndexButton
            // 
            this.MergeIndexButton.Location = new System.Drawing.Point(592, -1);
            this.MergeIndexButton.Name = "MergeIndexButton";
            this.MergeIndexButton.Size = new System.Drawing.Size(102, 29);
            this.MergeIndexButton.TabIndex = 20;
            this.MergeIndexButton.Text = "インデックスマージ";
            this.MergeIndexButton.UseVisualStyleBackColor = true;
            this.MergeIndexButton.Visible = false;
            this.MergeIndexButton.Click += new System.EventHandler(this.MergeIndexButton_Click);
            // 
            // IndexHistoryGrid
            // 
            this.IndexHistoryGrid.AfterErrorInfo = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("IndexHistoryGrid.AfterErrorInfo")));
            this.IndexHistoryGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IndexHistoryGrid.AutoClipboard = true;
            this.IndexHistoryGrid.CellButtonDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.HashSet<string>>)(resources.GetObject("IndexHistoryGrid.CellButtonDic")));
            this.IndexHistoryGrid.ColumnInfo = "10,1,0,0,0,90,Columns:";
            this.IndexHistoryGrid.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.IndexHistoryGrid.EnableReadOnlyColor = false;
            this.IndexHistoryGrid.EnableUpdateCellStyle = false;
            this.IndexHistoryGrid.GridName = null;
            this.IndexHistoryGrid.IsCol1SelectCheck = false;
            this.IndexHistoryGrid.IsEnterRight = false;
            this.IndexHistoryGrid.Location = new System.Drawing.Point(3, 26);
            this.IndexHistoryGrid.Name = "IndexHistoryGrid";
            this.IndexHistoryGrid.PulldownDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>)(resources.GetObject("IndexHistoryGrid.PulldownDic")));
            this.IndexHistoryGrid.Rows.DefaultSize = 18;
            this.IndexHistoryGrid.Size = new System.Drawing.Size(923, 136);
            this.IndexHistoryGrid.StyleInfo = resources.GetString("IndexHistoryGrid.StyleInfo");
            this.IndexHistoryGrid.TabIndex = 21;
            this.IndexHistoryGrid.WindowsName = null;
            // 
            // TargetDirText
            // 
            this.TargetDirText.AllowDrop = true;
            this.TargetDirText.Location = new System.Drawing.Point(5, 4);
            this.TargetDirText.Name = "TargetDirText";
            this.TargetDirText.Size = new System.Drawing.Size(460, 19);
            this.TargetDirText.TabIndex = 5;
            // 
            // UpdateIndexButton
            // 
            this.UpdateIndexButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.UpdateIndexButton.Location = new System.Drawing.Point(473, 29);
            this.UpdateIndexButton.Name = "UpdateIndexButton";
            this.UpdateIndexButton.Size = new System.Drawing.Size(113, 39);
            this.UpdateIndexButton.TabIndex = 22;
            this.UpdateIndexButton.Text = "インデックス更新(&U)";
            this.UpdateIndexButton.UseVisualStyleBackColor = true;
            this.UpdateIndexButton.Click += new System.EventHandler(this.UpdateIndexButton_Click);
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.label4);
            this.BottomPanel.Controls.Add(this.IndexHistoryGrid);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 402);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(929, 165);
            this.BottomPanel.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Info;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(3, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 21);
            this.label4.TabIndex = 84;
            this.label4.Text = "インデックス作成履歴";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // collapsibleSplitter3
            // 
            this.collapsibleSplitter3.AnimationDelay = 20;
            this.collapsibleSplitter3.AnimationStep = 20;
            this.collapsibleSplitter3.BorderStyle3D = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.collapsibleSplitter3.ControlToHide = this.BottomPanel;
            this.collapsibleSplitter3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.collapsibleSplitter3.ExpandParentForm = false;
            this.collapsibleSplitter3.Location = new System.Drawing.Point(0, 394);
            this.collapsibleSplitter3.Name = "collapsibleSplitter3";
            this.collapsibleSplitter3.Size = new System.Drawing.Size(929, 8);
            this.collapsibleSplitter3.TabIndex = 119;
            this.collapsibleSplitter3.TabStop = false;
            this.collapsibleSplitter3.UseAnimations = false;
            this.collapsibleSplitter3.VisualStyle = NJFLib.Controls.VisualStyles.DoubleDots;
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.TargetDirText);
            this.MainPanel.Controls.Add(this.label1);
            this.MainPanel.Controls.Add(this.ReferenceButton);
            this.MainPanel.Controls.Add(this.CreateIndexButton);
            this.MainPanel.Controls.Add(this.LogViewerText);
            this.MainPanel.Controls.Add(this.ReservedGrid);
            this.MainPanel.Controls.Add(this.groupBox1);
            this.MainPanel.Controls.Add(this.UpdateIndexButton);
            this.MainPanel.Controls.Add(this.ProgressBar);
            this.MainPanel.Controls.Add(this.MergeIndexButton);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(929, 394);
            this.MainPanel.TabIndex = 121;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(2, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 21);
            this.label1.TabIndex = 120;
            this.label1.Text = "インデックシング予約";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReservedGrid
            // 
            this.ReservedGrid.AfterErrorInfo = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("ReservedGrid.AfterErrorInfo")));
            this.ReservedGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReservedGrid.AutoClipboard = true;
            this.ReservedGrid.CellButtonDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.HashSet<string>>)(resources.GetObject("ReservedGrid.CellButtonDic")));
            this.ReservedGrid.ColumnInfo = "10,1,0,0,0,90,Columns:";
            this.ReservedGrid.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.ReservedGrid.EnableReadOnlyColor = false;
            this.ReservedGrid.EnableUpdateCellStyle = false;
            this.ReservedGrid.GridName = null;
            this.ReservedGrid.IsCol1SelectCheck = false;
            this.ReservedGrid.IsEnterRight = false;
            this.ReservedGrid.Location = new System.Drawing.Point(3, 163);
            this.ReservedGrid.Name = "ReservedGrid";
            this.ReservedGrid.PulldownDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>)(resources.GetObject("ReservedGrid.PulldownDic")));
            this.ReservedGrid.Rows.DefaultSize = 18;
            this.ReservedGrid.Size = new System.Drawing.Size(923, 227);
            this.ReservedGrid.StyleInfo = resources.GetString("ReservedGrid.StyleInfo");
            this.ReservedGrid.TabIndex = 23;
            this.ReservedGrid.WindowsName = null;
            // 
            // IndexBuildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 567);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.collapsibleSplitter3);
            this.Controls.Add(this.BottomPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IndexBuildForm";
            this.Text = "IndexBuildForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IndexBuildForm_FormClosed);
            this.Load += new System.EventHandler(this.IndexBuildForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IndexHistoryGrid)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReservedGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FxCommonLib.Controls.FilePathTextBox TargetDirText;
        private System.Windows.Forms.Button ReferenceButton;
        private System.Windows.Forms.Button CreateIndexButton;
        private System.Windows.Forms.TextBox LogViewerText;
        private System.Windows.Forms.RadioButton TikaRadio;
        private System.Windows.Forms.RadioButton IFilterRadio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Button MergeIndexButton;
        private FxCommonLib.Controls.FlexGridEx IndexHistoryGrid;
        private System.Windows.Forms.Button UpdateIndexButton;
        private System.Windows.Forms.Panel BottomPanel;
        private NJFLib.Controls.CollapsibleSplitter collapsibleSplitter3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label label1;
        private FxCommonLib.Controls.FlexGridEx ReservedGrid;
    }
}