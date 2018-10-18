namespace IndexBuilder.Demo {
    partial class DemoForm {
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.PathText = new System.Windows.Forms.TextBox();
            this.CreateIndexButton = new System.Windows.Forms.Button();
            this.ResultsList = new System.Windows.Forms.ListView();
            this.columnHeaderIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderScore = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenParentFolderMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchButton = new System.Windows.Forms.Button();
            this.QueryText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.ContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BrowseButton);
            this.groupBox1.Controls.Add(this.PathText);
            this.groupBox1.Controls.Add(this.CreateIndexButton);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(6, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(925, 104);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "インデックス";
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "検索対象フォルダ";
            // 
            // BrowseButton
            // 
            this.BrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BrowseButton.Location = new System.Drawing.Point(844, 42);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(75, 22);
            this.BrowseButton.TabIndex = 2;
            this.BrowseButton.Text = "参照...";
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // PathText
            // 
            this.PathText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathText.Location = new System.Drawing.Point(16, 44);
            this.PathText.Name = "PathText";
            this.PathText.Size = new System.Drawing.Size(822, 19);
            this.PathText.TabIndex = 1;
            this.PathText.Text = "C:\\Workspace";
            // 
            // CreateIndexButton
            // 
            this.CreateIndexButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CreateIndexButton.Location = new System.Drawing.Point(16, 74);
            this.CreateIndexButton.Name = "CreateIndexButton";
            this.CreateIndexButton.Size = new System.Drawing.Size(112, 21);
            this.CreateIndexButton.TabIndex = 3;
            this.CreateIndexButton.Text = "インデックス作成";
            this.CreateIndexButton.Click += new System.EventHandler(this.CreateIndexButton_Click);
            // 
            // ResultsList
            // 
            this.ResultsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderIcon,
            this.columnHeaderName,
            this.columnHeaderFolder,
            this.columnHeaderScore});
            this.ResultsList.ContextMenuStrip = this.ContextMenu;
            this.ResultsList.FullRowSelect = true;
            this.ResultsList.Location = new System.Drawing.Point(5, 211);
            this.ResultsList.Name = "ResultsList";
            this.ResultsList.Size = new System.Drawing.Size(930, 481);
            this.ResultsList.TabIndex = 11;
            this.ResultsList.UseCompatibleStateImageBehavior = false;
            this.ResultsList.View = System.Windows.Forms.View.Details;
            this.ResultsList.DoubleClick += new System.EventHandler(this.ResultsList_DoubleClick);
            // 
            // columnHeaderIcon
            // 
            this.columnHeaderIcon.Text = "";
            this.columnHeaderIcon.Width = 22;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "ファイル名";
            this.columnHeaderName.Width = 243;
            // 
            // columnHeaderFolder
            // 
            this.columnHeaderFolder.Text = "場所";
            this.columnHeaderFolder.Width = 500;
            // 
            // columnHeaderScore
            // 
            this.columnHeaderScore.Text = "スコア";
            // 
            // ContextMenu
            // 
            this.ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenParentFolderMenu,
            this.OpenFileMenu});
            this.ContextMenu.Name = "ContextMenu";
            this.ContextMenu.Size = new System.Drawing.Size(166, 48);
            // 
            // OpenParentFolderMenu
            // 
            this.OpenParentFolderMenu.Name = "OpenParentFolderMenu";
            this.OpenParentFolderMenu.Size = new System.Drawing.Size(165, 22);
            this.OpenParentFolderMenu.Text = "親フォルダを開く(&P)";
            this.OpenParentFolderMenu.Click += new System.EventHandler(this.OpenParentFolderMenu_Click);
            // 
            // OpenFileMenu
            // 
            this.OpenFileMenu.Name = "OpenFileMenu";
            this.OpenFileMenu.Size = new System.Drawing.Size(165, 22);
            this.OpenFileMenu.Text = "ファイルを開く(&O)";
            this.OpenFileMenu.Click += new System.EventHandler(this.OpenFileMenu_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SearchButton.Location = new System.Drawing.Point(851, 186);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(80, 20);
            this.SearchButton.TabIndex = 10;
            this.SearchButton.Text = "検索";
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // QueryText
            // 
            this.QueryText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.QueryText.Location = new System.Drawing.Point(5, 186);
            this.QueryText.Name = "QueryText";
            this.QueryText.Size = new System.Drawing.Size(840, 19);
            this.QueryText.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Location = new System.Drawing.Point(4, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(237, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "キーワードを入力してください。";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusLabel.Location = new System.Drawing.Point(4, 116);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(927, 37);
            this.StatusLabel.TabIndex = 13;
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 694);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ResultsList);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.QueryText);
            this.Name = "DemoForm";
            this.Text = "全文検索デモ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.TextBox PathText;
        private System.Windows.Forms.Button CreateIndexButton;
        private System.Windows.Forms.ListView ResultsList;
        private System.Windows.Forms.ColumnHeader columnHeaderIcon;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderFolder;
        private System.Windows.Forms.ColumnHeader columnHeaderScore;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox QueryText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.ContextMenuStrip ContextMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenParentFolderMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenFileMenu;
    }
}