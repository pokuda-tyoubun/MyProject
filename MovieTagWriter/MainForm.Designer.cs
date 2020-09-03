﻿namespace MovieTagWriter {
    partial class MainForm {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.PathText = new System.Windows.Forms.TextBox();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.RefButton = new System.Windows.Forms.Button();
            this.ResultNavi = new System.Windows.Forms.BindingNavigator(this.components);
            this.DenominatorLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.SearchGridText = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.TargetGrid = new FxCommonLib.Controls.FlexGridEx(this.components);
            this.collapsibleSplitter1 = new NJFLib.Controls.CollapsibleSplitter();
            this.FilterGridButton = new System.Windows.Forms.ToolStripButton();
            this.ClearFilterButton = new System.Windows.Forms.ToolStripButton();
            this.WriteExcelButton = new System.Windows.Forms.ToolStripButton();
            this.PackagePicture = new System.Windows.Forms.PictureBox();
            this.SearchMovieButton = new System.Windows.Forms.Button();
            this.WriteTagButton = new System.Windows.Forms.Button();
            this.TitleText = new System.Windows.Forms.TextBox();
            this.ArtistText = new System.Windows.Forms.TextBox();
            this.GenresText = new System.Windows.Forms.TextBox();
            this.MakerText = new System.Windows.Forms.TextBox();
            this.DirectorText = new System.Windows.Forms.TextBox();
            this.CommentText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.SelectedRowIndexLabel = new System.Windows.Forms.Label();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.ItemLinkLabel = new System.Windows.Forms.LinkLabel();
            this.LeftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultNavi)).BeginInit();
            this.ResultNavi.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PackagePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // PathText
            // 
            this.PathText.Location = new System.Drawing.Point(34, 6);
            this.PathText.Name = "PathText";
            this.PathText.Size = new System.Drawing.Size(220, 19);
            this.PathText.TabIndex = 0;
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.ItemLinkLabel);
            this.LeftPanel.Controls.Add(this.label8);
            this.LeftPanel.Controls.Add(this.SelectedRowIndexLabel);
            this.LeftPanel.Controls.Add(this.ApplyButton);
            this.LeftPanel.Controls.Add(this.label7);
            this.LeftPanel.Controls.Add(this.label6);
            this.LeftPanel.Controls.Add(this.label5);
            this.LeftPanel.Controls.Add(this.label4);
            this.LeftPanel.Controls.Add(this.label3);
            this.LeftPanel.Controls.Add(this.label2);
            this.LeftPanel.Controls.Add(this.CommentText);
            this.LeftPanel.Controls.Add(this.DirectorText);
            this.LeftPanel.Controls.Add(this.MakerText);
            this.LeftPanel.Controls.Add(this.GenresText);
            this.LeftPanel.Controls.Add(this.ArtistText);
            this.LeftPanel.Controls.Add(this.TitleText);
            this.LeftPanel.Controls.Add(this.label1);
            this.LeftPanel.Controls.Add(this.RefButton);
            this.LeftPanel.Controls.Add(this.PackagePicture);
            this.LeftPanel.Controls.Add(this.PathText);
            this.LeftPanel.Controls.Add(this.SearchMovieButton);
            this.LeftPanel.Controls.Add(this.WriteTagButton);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(257, 660);
            this.LeftPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 12);
            this.label1.TabIndex = 32;
            this.label1.Text = "パス：";
            // 
            // RefButton
            // 
            this.RefButton.Location = new System.Drawing.Point(188, 28);
            this.RefButton.Name = "RefButton";
            this.RefButton.Size = new System.Drawing.Size(65, 22);
            this.RefButton.TabIndex = 1;
            this.RefButton.Text = "参照...";
            this.RefButton.UseVisualStyleBackColor = true;
            this.RefButton.Click += new System.EventHandler(this.RefButton_Click);
            // 
            // ResultNavi
            // 
            this.ResultNavi.AddNewItem = null;
            this.ResultNavi.CountItem = this.DenominatorLabel;
            this.ResultNavi.CountItemFormat = "0件";
            this.ResultNavi.DeleteItem = null;
            this.ResultNavi.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DenominatorLabel,
            this.toolStripSeparator2,
            this.SearchGridText,
            this.FilterGridButton,
            this.ClearFilterButton,
            this.toolStripSeparator3,
            this.WriteExcelButton,
            this.toolStripSeparator6});
            this.ResultNavi.Location = new System.Drawing.Point(0, 0);
            this.ResultNavi.MoveFirstItem = null;
            this.ResultNavi.MoveLastItem = null;
            this.ResultNavi.MoveNextItem = null;
            this.ResultNavi.MovePreviousItem = null;
            this.ResultNavi.Name = "ResultNavi";
            this.ResultNavi.PositionItem = null;
            this.ResultNavi.Size = new System.Drawing.Size(769, 25);
            this.ResultNavi.TabIndex = 0;
            this.ResultNavi.Text = "BindingNavigator1";
            // 
            // DenominatorLabel
            // 
            this.DenominatorLabel.Name = "DenominatorLabel";
            this.DenominatorLabel.Size = new System.Drawing.Size(26, 22);
            this.DenominatorLabel.Text = "0件";
            this.DenominatorLabel.ToolTipText = "項目の総数";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // SearchGridText
            // 
            this.SearchGridText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchGridText.Name = "SearchGridText";
            this.SearchGridText.Size = new System.Drawing.Size(150, 25);
            this.SearchGridText.ToolTipText = "入力した文字でフィルタリングします。";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.TargetGrid);
            this.MainPanel.Controls.Add(this.ResultNavi);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(265, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(769, 660);
            this.MainPanel.TabIndex = 128;
            // 
            // TargetGrid
            // 
            this.TargetGrid.AfterErrorInfo = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("TargetGrid.AfterErrorInfo")));
            this.TargetGrid.AllowFiltering = true;
            this.TargetGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.TargetGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetGrid.AutoClipboard = true;
            this.TargetGrid.CellButtonDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.HashSet<string>>)(resources.GetObject("TargetGrid.CellButtonDic")));
            this.TargetGrid.ColumnInfo = "10,1,0,0,0,90,Columns:";
            this.TargetGrid.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.TargetGrid.EnableReadOnlyColor = false;
            this.TargetGrid.EnableUpdateCellStyle = false;
            this.TargetGrid.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.TargetGrid.GridName = null;
            this.TargetGrid.IsCol1SelectCheck = false;
            this.TargetGrid.IsEnterRight = false;
            this.TargetGrid.Location = new System.Drawing.Point(3, 29);
            this.TargetGrid.Name = "TargetGrid";
            this.TargetGrid.PulldownDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>)(resources.GetObject("TargetGrid.PulldownDic")));
            this.TargetGrid.Rows.Count = 2;
            this.TargetGrid.Rows.DefaultSize = 18;
            this.TargetGrid.ShowErrors = true;
            this.TargetGrid.Size = new System.Drawing.Size(763, 628);
            this.TargetGrid.StyleInfo = resources.GetString("TargetGrid.StyleInfo");
            this.TargetGrid.TabIndex = 1;
            this.TargetGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            this.TargetGrid.WindowsName = null;
            this.TargetGrid.SelChange += new System.EventHandler(this.TargetGrid_SelChange);
            // 
            // collapsibleSplitter1
            // 
            this.collapsibleSplitter1.AnimationDelay = 20;
            this.collapsibleSplitter1.AnimationStep = 20;
            this.collapsibleSplitter1.BorderStyle3D = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.collapsibleSplitter1.ControlToHide = this.LeftPanel;
            this.collapsibleSplitter1.ExpandParentForm = false;
            this.collapsibleSplitter1.Location = new System.Drawing.Point(257, 0);
            this.collapsibleSplitter1.Name = "collapsibleSplitter1";
            this.collapsibleSplitter1.Size = new System.Drawing.Size(8, 660);
            this.collapsibleSplitter1.TabIndex = 126;
            this.collapsibleSplitter1.TabStop = false;
            this.collapsibleSplitter1.UseAnimations = false;
            this.collapsibleSplitter1.VisualStyle = NJFLib.Controls.VisualStyles.DoubleDots;
            // 
            // FilterGridButton
            // 
            this.FilterGridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FilterGridButton.Image = global::MovieTagWriter.Properties.Resources.Search24;
            this.FilterGridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FilterGridButton.Name = "FilterGridButton";
            this.FilterGridButton.Size = new System.Drawing.Size(23, 22);
            this.FilterGridButton.Text = "入力した文字でフィルタリング";
            this.FilterGridButton.ToolTipText = "入力した文字でフィルタリング";
            this.FilterGridButton.Click += new System.EventHandler(this.FilterGridButton_Click);
            // 
            // ClearFilterButton
            // 
            this.ClearFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearFilterButton.Image = global::MovieTagWriter.Properties.Resources.EditClear24;
            this.ClearFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearFilterButton.Name = "ClearFilterButton";
            this.ClearFilterButton.Size = new System.Drawing.Size(23, 22);
            this.ClearFilterButton.Text = "フィルタリング解除";
            this.ClearFilterButton.ToolTipText = "フィルタリング解除";
            this.ClearFilterButton.Click += new System.EventHandler(this.ClearFilterButton_Click);
            // 
            // WriteExcelButton
            // 
            this.WriteExcelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.WriteExcelButton.Image = global::MovieTagWriter.Properties.Resources.Excel24;
            this.WriteExcelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.WriteExcelButton.Name = "WriteExcelButton";
            this.WriteExcelButton.Size = new System.Drawing.Size(23, 22);
            this.WriteExcelButton.Text = "Excelに出力";
            this.WriteExcelButton.ToolTipText = "Excelに出力";
            this.WriteExcelButton.Click += new System.EventHandler(this.WriteExcelButton_Click);
            // 
            // PackagePicture
            // 
            this.PackagePicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PackagePicture.Location = new System.Drawing.Point(6, 518);
            this.PackagePicture.Name = "PackagePicture";
            this.PackagePicture.Size = new System.Drawing.Size(245, 135);
            this.PackagePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PackagePicture.TabIndex = 29;
            this.PackagePicture.TabStop = false;
            // 
            // SearchMovieButton
            // 
            this.SearchMovieButton.Image = global::MovieTagWriter.Properties.Resources.Search24;
            this.SearchMovieButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SearchMovieButton.Location = new System.Drawing.Point(28, 56);
            this.SearchMovieButton.Name = "SearchMovieButton";
            this.SearchMovieButton.Size = new System.Drawing.Size(111, 32);
            this.SearchMovieButton.TabIndex = 2;
            this.SearchMovieButton.Text = "　　ファイル抽出";
            this.SearchMovieButton.UseVisualStyleBackColor = true;
            this.SearchMovieButton.Click += new System.EventHandler(this.SearchMovieButton_Click);
            // 
            // WriteTagButton
            // 
            this.WriteTagButton.Image = global::MovieTagWriter.Properties.Resources.Tag24;
            this.WriteTagButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.WriteTagButton.Location = new System.Drawing.Point(142, 56);
            this.WriteTagButton.Name = "WriteTagButton";
            this.WriteTagButton.Size = new System.Drawing.Size(111, 32);
            this.WriteTagButton.TabIndex = 3;
            this.WriteTagButton.Text = "　　タグ書込み";
            this.WriteTagButton.UseVisualStyleBackColor = true;
            this.WriteTagButton.Click += new System.EventHandler(this.WriteTagButton_Click);
            // 
            // TitleText
            // 
            this.TitleText.Location = new System.Drawing.Point(6, 185);
            this.TitleText.Name = "TitleText";
            this.TitleText.Size = new System.Drawing.Size(245, 19);
            this.TitleText.TabIndex = 7;
            this.TitleText.Validated += new System.EventHandler(this.TitleText_Validated);
            // 
            // ArtistText
            // 
            this.ArtistText.Location = new System.Drawing.Point(6, 223);
            this.ArtistText.Name = "ArtistText";
            this.ArtistText.Size = new System.Drawing.Size(245, 19);
            this.ArtistText.TabIndex = 8;
            // 
            // GenresText
            // 
            this.GenresText.Location = new System.Drawing.Point(6, 258);
            this.GenresText.Name = "GenresText";
            this.GenresText.Size = new System.Drawing.Size(245, 19);
            this.GenresText.TabIndex = 9;
            // 
            // MakerText
            // 
            this.MakerText.Location = new System.Drawing.Point(6, 295);
            this.MakerText.Name = "MakerText";
            this.MakerText.Size = new System.Drawing.Size(245, 19);
            this.MakerText.TabIndex = 10;
            // 
            // DirectorText
            // 
            this.DirectorText.Location = new System.Drawing.Point(6, 333);
            this.DirectorText.Name = "DirectorText";
            this.DirectorText.Size = new System.Drawing.Size(245, 19);
            this.DirectorText.TabIndex = 11;
            // 
            // CommentText
            // 
            this.CommentText.Location = new System.Drawing.Point(6, 371);
            this.CommentText.Multiline = true;
            this.CommentText.Name = "CommentText";
            this.CommentText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CommentText.Size = new System.Drawing.Size(245, 127);
            this.CommentText.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 12);
            this.label2.TabIndex = 39;
            this.label2.Text = "タイトル：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 12);
            this.label3.TabIndex = 40;
            this.label3.Text = "アーティスト：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 245);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 12);
            this.label4.TabIndex = 41;
            this.label4.Text = "ジャンル：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 280);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 12);
            this.label5.TabIndex = 42;
            this.label5.Text = "メーカー：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 317);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 43;
            this.label6.Text = "監督：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 356);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 12);
            this.label7.TabIndex = 44;
            this.label7.Text = "コメント：";
            // 
            // ApplyButton
            // 
            this.ApplyButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ApplyButton.Location = new System.Drawing.Point(101, 146);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(150, 20);
            this.ApplyButton.TabIndex = 6;
            this.ApplyButton.Text = "適用";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // SelectedRowIndexLabel
            // 
            this.SelectedRowIndexLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SelectedRowIndexLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.SelectedRowIndexLabel.Location = new System.Drawing.Point(6, 146);
            this.SelectedRowIndexLabel.Name = "SelectedRowIndexLabel";
            this.SelectedRowIndexLabel.Size = new System.Drawing.Size(89, 20);
            this.SelectedRowIndexLabel.TabIndex = 5;
            this.SelectedRowIndexLabel.Text = "行目";
            this.SelectedRowIndexLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.PapayaWhip;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(5, 122);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(246, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "タグ情報変更";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ItemLinkLabel
            // 
            this.ItemLinkLabel.AutoSize = true;
            this.ItemLinkLabel.Location = new System.Drawing.Point(8, 502);
            this.ItemLinkLabel.Name = "ItemLinkLabel";
            this.ItemLinkLabel.Size = new System.Drawing.Size(93, 12);
            this.ItemLinkLabel.TabIndex = 45;
            this.ItemLinkLabel.TabStop = true;
            this.ItemLinkLabel.Text = "製品紹介ページへ";
            this.ItemLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ItemLinkLabel_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 660);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.collapsibleSplitter1);
            this.Controls.Add(this.LeftPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MP4TagWriter";
            this.LeftPanel.ResumeLayout(false);
            this.LeftPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultNavi)).EndInit();
            this.ResultNavi.ResumeLayout(false);
            this.ResultNavi.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PackagePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SearchMovieButton;
        private System.Windows.Forms.Button WriteTagButton;
        private System.Windows.Forms.TextBox PathText;
        private FxCommonLib.Controls.FlexGridEx TargetGrid;
        private System.Windows.Forms.Panel LeftPanel;
        private NJFLib.Controls.CollapsibleSplitter collapsibleSplitter1;
        internal System.Windows.Forms.BindingNavigator ResultNavi;
        internal System.Windows.Forms.ToolStripLabel DenominatorLabel;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripTextBox SearchGridText;
        private System.Windows.Forms.ToolStripButton FilterGridButton;
        private System.Windows.Forms.ToolStripButton ClearFilterButton;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        internal System.Windows.Forms.ToolStripButton WriteExcelButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.PictureBox PackagePicture;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RefButton;
        private System.Windows.Forms.TextBox MakerText;
        private System.Windows.Forms.TextBox GenresText;
        private System.Windows.Forms.TextBox ArtistText;
        private System.Windows.Forms.TextBox TitleText;
        private System.Windows.Forms.TextBox CommentText;
        private System.Windows.Forms.TextBox DirectorText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Label SelectedRowIndexLabel;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.LinkLabel ItemLinkLabel;
    }
}
