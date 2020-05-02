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
            this.IndexHistoryContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.IndexHistoryGrid = new FxCommonLib.Controls.FlexGridEx(this.components);
            this.TargetDirText = new FxCommonLib.Controls.FilePathTextBox(this.components);
            this.UpdateIndexButton = new System.Windows.Forms.Button();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.Log4NetTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.collapsibleSplitter3 = new NJFLib.Controls.CollapsibleSplitter();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.StopButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ActiveIndexGrid = new FxCommonLib.Controls.FlexGridEx(this.components);
            this.ActiveIndexContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.UpdateIndexMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateIndexMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteIndexMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ActiveIndexCopyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.IndexHistoryContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IndexHistoryGrid)).BeginInit();
            this.BottomPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActiveIndexGrid)).BeginInit();
            this.ActiveIndexContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReferenceButton
            // 
            this.ReferenceButton.Location = new System.Drawing.Point(470, 6);
            this.ReferenceButton.Name = "ReferenceButton";
            this.ReferenceButton.Size = new System.Drawing.Size(65, 22);
            this.ReferenceButton.TabIndex = 1;
            this.ReferenceButton.Text = "参照...";
            this.ReferenceButton.UseVisualStyleBackColor = true;
            this.ReferenceButton.Click += new System.EventHandler(this.ReferenceButton_Click);
            // 
            // CreateIndexButton
            // 
            this.CreateIndexButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CreateIndexButton.Location = new System.Drawing.Point(784, 31);
            this.CreateIndexButton.Name = "CreateIndexButton";
            this.CreateIndexButton.Size = new System.Drawing.Size(126, 22);
            this.CreateIndexButton.TabIndex = 2;
            this.CreateIndexButton.Text = "インデックス作成";
            this.CreateIndexButton.UseVisualStyleBackColor = true;
            this.CreateIndexButton.Visible = false;
            this.CreateIndexButton.Click += new System.EventHandler(this.CreateIndexButton_Click);
            // 
            // LogViewerText
            // 
            this.LogViewerText.Location = new System.Drawing.Point(223, 48);
            this.LogViewerText.Multiline = true;
            this.LogViewerText.Name = "LogViewerText";
            this.LogViewerText.ReadOnly = true;
            this.LogViewerText.Size = new System.Drawing.Size(123, 20);
            this.LogViewerText.TabIndex = 6;
            // 
            // TikaRadio
            // 
            this.TikaRadio.AutoSize = true;
            this.TikaRadio.Checked = true;
            this.TikaRadio.Location = new System.Drawing.Point(6, 17);
            this.TikaRadio.Name = "TikaRadio";
            this.TikaRadio.Size = new System.Drawing.Size(87, 16);
            this.TikaRadio.TabIndex = 0;
            this.TikaRadio.TabStop = true;
            this.TikaRadio.Text = "Apache Tika";
            this.TikaRadio.UseVisualStyleBackColor = true;
            // 
            // IFilterRadio
            // 
            this.IFilterRadio.AutoSize = true;
            this.IFilterRadio.Enabled = false;
            this.IFilterRadio.Location = new System.Drawing.Point(110, 17);
            this.IFilterRadio.Name = "IFilterRadio";
            this.IFilterRadio.Size = new System.Drawing.Size(53, 16);
            this.IFilterRadio.TabIndex = 1;
            this.IFilterRadio.Text = "IFilter";
            this.IFilterRadio.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TikaRadio);
            this.groupBox1.Controls.Add(this.IFilterRadio);
            this.groupBox1.Location = new System.Drawing.Point(5, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 39);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "テキストコンバータ";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(350, 47);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(241, 22);
            this.ProgressBar.TabIndex = 7;
            // 
            // MergeIndexButton
            // 
            this.MergeIndexButton.Location = new System.Drawing.Point(784, 6);
            this.MergeIndexButton.Name = "MergeIndexButton";
            this.MergeIndexButton.Size = new System.Drawing.Size(102, 22);
            this.MergeIndexButton.TabIndex = 4;
            this.MergeIndexButton.Text = "インデックスマージ";
            this.MergeIndexButton.UseVisualStyleBackColor = true;
            this.MergeIndexButton.Visible = false;
            this.MergeIndexButton.Click += new System.EventHandler(this.MergeIndexButton_Click);
            // 
            // IndexHistoryContext
            // 
            this.IndexHistoryContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyMenu});
            this.IndexHistoryContext.Name = "ActiveIndexContext";
            this.IndexHistoryContext.Size = new System.Drawing.Size(120, 26);
            // 
            // CopyMenu
            // 
            this.CopyMenu.Name = "CopyMenu";
            this.CopyMenu.Size = new System.Drawing.Size(119, 22);
            this.CopyMenu.Text = "コピー(&C)";
            this.CopyMenu.Click += new System.EventHandler(this.CopyMenu_Click);
            // 
            // IndexHistoryGrid
            // 
            this.IndexHistoryGrid.AfterErrorInfo = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("IndexHistoryGrid.AfterErrorInfo")));
            this.IndexHistoryGrid.AllowEditing = false;
            this.IndexHistoryGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IndexHistoryGrid.AutoClipboard = true;
            this.IndexHistoryGrid.CellButtonDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.HashSet<string>>)(resources.GetObject("IndexHistoryGrid.CellButtonDic")));
            this.IndexHistoryGrid.ColumnInfo = "10,1,0,0,0,90,Columns:";
            this.IndexHistoryGrid.ContextMenuStrip = this.IndexHistoryContext;
            this.IndexHistoryGrid.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.IndexHistoryGrid.EnableReadOnlyColor = false;
            this.IndexHistoryGrid.EnableUpdateCellStyle = false;
            this.IndexHistoryGrid.GridName = null;
            this.IndexHistoryGrid.IsCol1SelectCheck = false;
            this.IndexHistoryGrid.IsEnterRight = false;
            this.IndexHistoryGrid.Location = new System.Drawing.Point(3, 297);
            this.IndexHistoryGrid.Name = "IndexHistoryGrid";
            this.IndexHistoryGrid.PulldownDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>)(resources.GetObject("IndexHistoryGrid.PulldownDic")));
            this.IndexHistoryGrid.Rows.DefaultSize = 18;
            this.IndexHistoryGrid.Size = new System.Drawing.Size(923, 91);
            this.IndexHistoryGrid.StyleInfo = resources.GetString("IndexHistoryGrid.StyleInfo");
            this.IndexHistoryGrid.TabIndex = 12;
            this.IndexHistoryGrid.WindowsName = null;
            // 
            // TargetDirText
            // 
            this.TargetDirText.AllowDrop = true;
            this.TargetDirText.Location = new System.Drawing.Point(5, 8);
            this.TargetDirText.Name = "TargetDirText";
            this.TargetDirText.Size = new System.Drawing.Size(460, 19);
            this.TargetDirText.TabIndex = 0;
            // 
            // UpdateIndexButton
            // 
            this.UpdateIndexButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.UpdateIndexButton.Location = new System.Drawing.Point(538, 6);
            this.UpdateIndexButton.Name = "UpdateIndexButton";
            this.UpdateIndexButton.Size = new System.Drawing.Size(113, 22);
            this.UpdateIndexButton.TabIndex = 3;
            this.UpdateIndexButton.Text = "インデックス作成(&B)";
            this.UpdateIndexButton.UseVisualStyleBackColor = true;
            this.UpdateIndexButton.Click += new System.EventHandler(this.UpdateIndexButton_Click);
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.Log4NetTextBox);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 402);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(929, 165);
            this.BottomPanel.TabIndex = 24;
            // 
            // Log4NetTextBox
            // 
            this.Log4NetTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Log4NetTextBox.Location = new System.Drawing.Point(0, 0);
            this.Log4NetTextBox.MaxLength = 100;
            this.Log4NetTextBox.Multiline = true;
            this.Log4NetTextBox.Name = "Log4NetTextBox";
            this.Log4NetTextBox.ReadOnly = true;
            this.Log4NetTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Log4NetTextBox.Size = new System.Drawing.Size(929, 165);
            this.Log4NetTextBox.TabIndex = 0;
            this.Log4NetTextBox.WordWrap = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Info;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(3, 273);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 21);
            this.label4.TabIndex = 11;
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
            this.collapsibleSplitter3.TabIndex = 0;
            this.collapsibleSplitter3.TabStop = false;
            this.collapsibleSplitter3.UseAnimations = false;
            this.collapsibleSplitter3.VisualStyle = NJFLib.Controls.VisualStyles.DoubleDots;
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.StopButton);
            this.MainPanel.Controls.Add(this.IndexHistoryGrid);
            this.MainPanel.Controls.Add(this.label4);
            this.MainPanel.Controls.Add(this.TargetDirText);
            this.MainPanel.Controls.Add(this.label1);
            this.MainPanel.Controls.Add(this.ReferenceButton);
            this.MainPanel.Controls.Add(this.CreateIndexButton);
            this.MainPanel.Controls.Add(this.LogViewerText);
            this.MainPanel.Controls.Add(this.ActiveIndexGrid);
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
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(595, 47);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(70, 22);
            this.StopButton.TabIndex = 8;
            this.StopButton.Text = "中断";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(3, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 21);
            this.label1.TabIndex = 9;
            this.label1.Text = "有効インデックス";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ActiveIndexGrid
            // 
            this.ActiveIndexGrid.AfterErrorInfo = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("ActiveIndexGrid.AfterErrorInfo")));
            this.ActiveIndexGrid.AllowEditing = false;
            this.ActiveIndexGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ActiveIndexGrid.AutoClipboard = true;
            this.ActiveIndexGrid.CellButtonDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.HashSet<string>>)(resources.GetObject("ActiveIndexGrid.CellButtonDic")));
            this.ActiveIndexGrid.ColumnInfo = "10,1,0,0,0,90,Columns:";
            this.ActiveIndexGrid.ContextMenuStrip = this.ActiveIndexContext;
            this.ActiveIndexGrid.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.ActiveIndexGrid.EnableReadOnlyColor = false;
            this.ActiveIndexGrid.EnableUpdateCellStyle = false;
            this.ActiveIndexGrid.GridName = null;
            this.ActiveIndexGrid.IsCol1SelectCheck = false;
            this.ActiveIndexGrid.IsEnterRight = false;
            this.ActiveIndexGrid.Location = new System.Drawing.Point(3, 97);
            this.ActiveIndexGrid.Name = "ActiveIndexGrid";
            this.ActiveIndexGrid.PulldownDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>)(resources.GetObject("ActiveIndexGrid.PulldownDic")));
            this.ActiveIndexGrid.Rows.DefaultSize = 18;
            this.ActiveIndexGrid.Size = new System.Drawing.Size(923, 173);
            this.ActiveIndexGrid.StyleInfo = resources.GetString("ActiveIndexGrid.StyleInfo");
            this.ActiveIndexGrid.TabIndex = 10;
            this.ActiveIndexGrid.WindowsName = null;
            // 
            // ActiveIndexContext
            // 
            this.ActiveIndexContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UpdateIndexMenu,
            this.CreateIndexMenu,
            this.DeleteIndexMenu,
            this.toolStripMenuItem1,
            this.ActiveIndexCopyMenu});
            this.ActiveIndexContext.Name = "ActiveIndexContext";
            this.ActiveIndexContext.Size = new System.Drawing.Size(170, 98);
            // 
            // UpdateIndexMenu
            // 
            this.UpdateIndexMenu.Name = "UpdateIndexMenu";
            this.UpdateIndexMenu.Size = new System.Drawing.Size(169, 22);
            this.UpdateIndexMenu.Text = "インデックス更新(&U)";
            this.UpdateIndexMenu.Click += new System.EventHandler(this.UpdateIndexMenu_Click);
            // 
            // CreateIndexMenu
            // 
            this.CreateIndexMenu.Name = "CreateIndexMenu";
            this.CreateIndexMenu.Size = new System.Drawing.Size(169, 22);
            this.CreateIndexMenu.Text = "インデックス作成";
            this.CreateIndexMenu.Visible = false;
            this.CreateIndexMenu.Click += new System.EventHandler(this.CreateIndexMenu_Click);
            // 
            // DeleteIndexMenu
            // 
            this.DeleteIndexMenu.Name = "DeleteIndexMenu";
            this.DeleteIndexMenu.Size = new System.Drawing.Size(169, 22);
            this.DeleteIndexMenu.Text = "インデックス削除(&D)";
            this.DeleteIndexMenu.Click += new System.EventHandler(this.DeleteIndexMenu_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(166, 6);
            // 
            // ActiveIndexCopyMenu
            // 
            this.ActiveIndexCopyMenu.Name = "ActiveIndexCopyMenu";
            this.ActiveIndexCopyMenu.Size = new System.Drawing.Size(169, 22);
            this.ActiveIndexCopyMenu.Text = "コピー(&C)";
            this.ActiveIndexCopyMenu.Click += new System.EventHandler(this.ActiveIndexCopyMenu_Click);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "インデックス作成";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IndexBuildForm_FormClosed);
            this.Load += new System.EventHandler(this.IndexBuildForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.IndexHistoryContext.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IndexHistoryGrid)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            this.BottomPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActiveIndexGrid)).EndInit();
            this.ActiveIndexContext.ResumeLayout(false);
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
        private FxCommonLib.Controls.FlexGridEx ActiveIndexGrid;
        private System.Windows.Forms.ContextMenuStrip ActiveIndexContext;
        private System.Windows.Forms.ToolStripMenuItem DeleteIndexMenu;
        private System.Windows.Forms.ContextMenuStrip IndexHistoryContext;
        private System.Windows.Forms.ToolStripMenuItem CopyMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ActiveIndexCopyMenu;
        private System.Windows.Forms.ToolStripMenuItem UpdateIndexMenu;
        private System.Windows.Forms.ToolStripMenuItem CreateIndexMenu;
        private System.Windows.Forms.TextBox Log4NetTextBox;
        private System.Windows.Forms.Button StopButton;
    }
}