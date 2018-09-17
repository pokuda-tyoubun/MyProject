namespace PokudaSearch {
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.UpdateDate2 = new C1.Win.C1Input.C1DateEdit();
            this.UpdateDate1 = new C1.Win.C1Input.C1DateEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.FailureFTSNavi = new System.Windows.Forms.BindingNavigator(this.components);
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.WriteExcelButton = new System.Windows.Forms.ToolStripButton();
            this.ResultGrid = new FxCommonLib.Controls.FlexGridEx(this.components);
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.SearchButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateDate2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateDate1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FailureFTSNavi)).BeginInit();
            this.FailureFTSNavi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(6, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "キーワード";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // KeywordText
            // 
            this.KeywordText.Location = new System.Drawing.Point(78, 10);
            this.KeywordText.Name = "KeywordText";
            this.KeywordText.Size = new System.Drawing.Size(516, 19);
            this.KeywordText.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "拡張子";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(78, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(104, 19);
            this.textBox1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(417, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 115;
            this.label3.Text = "～";
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(188, 34);
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
            this.UpdateDate2.Location = new System.Drawing.Point(440, 37);
            this.UpdateDate2.Name = "UpdateDate2";
            this.UpdateDate2.ShowFocusRectangle = true;
            this.UpdateDate2.Size = new System.Drawing.Size(154, 17);
            this.UpdateDate2.TabIndex = 117;
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
            this.UpdateDate1.Location = new System.Drawing.Point(260, 37);
            this.UpdateDate1.Name = "UpdateDate1";
            this.UpdateDate1.ShowFocusRectangle = true;
            this.UpdateDate1.Size = new System.Drawing.Size(154, 17);
            this.UpdateDate1.TabIndex = 116;
            this.UpdateDate1.Tag = null;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.Info;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(6, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 23);
            this.label7.TabIndex = 121;
            this.label7.Text = "検索結果";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.FailureFTSNavi.Location = new System.Drawing.Point(117, 60);
            this.FailureFTSNavi.MoveFirstItem = this.toolStripButton1;
            this.FailureFTSNavi.MoveLastItem = this.toolStripButton4;
            this.FailureFTSNavi.MoveNextItem = this.toolStripButton3;
            this.FailureFTSNavi.MovePreviousItem = this.toolStripButton2;
            this.FailureFTSNavi.Name = "FailureFTSNavi";
            this.FailureFTSNavi.PositionItem = this.toolStripTextBox1;
            this.FailureFTSNavi.Size = new System.Drawing.Size(234, 25);
            this.FailureFTSNavi.TabIndex = 120;
            this.FailureFTSNavi.Text = "BindingNavigator1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(37, 22);
            this.toolStripLabel1.Text = "/ {0}";
            this.toolStripLabel1.ToolTipText = "項目の総数";
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
            // 
            // ResultGrid
            // 
            this.ResultGrid.AfterErrorInfo = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("ResultGrid.AfterErrorInfo")));
            this.ResultGrid.AllowFiltering = true;
            this.ResultGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.ResultGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultGrid.AutoClipboard = true;
            this.ResultGrid.ColumnInfo = "10,1,0,0,0,90,Columns:";
            this.ResultGrid.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.ResultGrid.EnableReadOnlyColor = false;
            this.ResultGrid.EnableUpdateCellStyle = false;
            this.ResultGrid.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.ResultGrid.GridName = null;
            this.ResultGrid.IsCol1SelectCheck = false;
            this.ResultGrid.IsEnterRight = false;
            this.ResultGrid.Location = new System.Drawing.Point(3, 88);
            this.ResultGrid.Name = "ResultGrid";
            this.ResultGrid.PulldownDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>)(resources.GetObject("ResultGrid.PulldownDic")));
            this.ResultGrid.Rows.Count = 2;
            this.ResultGrid.Rows.DefaultSize = 18;
            this.ResultGrid.ShowErrors = true;
            this.ResultGrid.Size = new System.Drawing.Size(962, 492);
            this.ResultGrid.StyleInfo = resources.GetString("ResultGrid.StyleInfo");
            this.ResultGrid.TabIndex = 119;
            this.ResultGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            this.ResultGrid.WindowsName = null;
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
            // SearchButton
            // 
            this.SearchButton.Image = global::PokudaSearch.Properties.Resources.Search24;
            this.SearchButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SearchButton.Location = new System.Drawing.Point(600, 4);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(80, 52);
            this.SearchButton.TabIndex = 118;
            this.SearchButton.Text = "検索(&S)";
            this.SearchButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SearchButton.UseVisualStyleBackColor = true;
            // 
            // SimpleSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 580);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.FailureFTSNavi);
            this.Controls.Add(this.ResultGrid);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.UpdateDate2);
            this.Controls.Add(this.UpdateDate1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.KeywordText);
            this.Name = "SimpleSearchForm";
            this.Text = "SimpleSearchForm";
            ((System.ComponentModel.ISupportInitialize)(this.UpdateDate2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateDate1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FailureFTSNavi)).EndInit();
            this.FailureFTSNavi.ResumeLayout(false);
            this.FailureFTSNavi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox KeywordText;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1Input.C1DateEdit UpdateDate2;
        private C1.Win.C1Input.C1DateEdit UpdateDate1;
        internal System.Windows.Forms.Button SearchButton;
        private FxCommonLib.Controls.FlexGridEx ResultGrid;
        internal System.Windows.Forms.Label label7;
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
    }
}