namespace PokudaSearch.Views {
    partial class ConfigForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.MainPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.DiffToolText = new FxCommonLib.Controls.FilePathTextBox(this.components);
            this.RefButton = new System.Windows.Forms.Button();
            this.MaxSearchResultLabel = new System.Windows.Forms.Label();
            this.MaxSeachResultNum = new C1.Win.C1Input.C1NumericEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.CancelButton1 = new System.Windows.Forms.Button();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.CauseLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BufferSizeLimitNum = new C1.Win.C1Input.C1NumericEdit();
            this.BufferSizeLimitLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FileSizeLimitNum = new C1.Win.C1Input.C1NumericEdit();
            this.FileSizeLimitLabel = new System.Windows.Forms.Label();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxSeachResultNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BufferSizeLimitNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileSizeLimitNum)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.label3);
            this.MainPanel.Controls.Add(this.DiffToolText);
            this.MainPanel.Controls.Add(this.RefButton);
            this.MainPanel.Controls.Add(this.MaxSearchResultLabel);
            this.MainPanel.Controls.Add(this.MaxSeachResultNum);
            this.MainPanel.Controls.Add(this.label5);
            this.MainPanel.Controls.Add(this.CancelButton1);
            this.MainPanel.Controls.Add(this.ApplyButton);
            this.MainPanel.Controls.Add(this.CauseLabel);
            this.MainPanel.Controls.Add(this.label2);
            this.MainPanel.Controls.Add(this.BufferSizeLimitNum);
            this.MainPanel.Controls.Add(this.BufferSizeLimitLabel);
            this.MainPanel.Controls.Add(this.label1);
            this.MainPanel.Controls.Add(this.FileSizeLimitNum);
            this.MainPanel.Controls.Add(this.FileSizeLimitLabel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(580, 265);
            this.MainPanel.TabIndex = 0;
            this.MainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Paint);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(7, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "差分ツール";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DiffToolText
            // 
            this.DiffToolText.AllowDrop = true;
            this.DiffToolText.Location = new System.Drawing.Point(93, 53);
            this.DiffToolText.Name = "DiffToolText";
            this.DiffToolText.Size = new System.Drawing.Size(404, 19);
            this.DiffToolText.TabIndex = 4;
            // 
            // RefButton
            // 
            this.RefButton.Location = new System.Drawing.Point(501, 51);
            this.RefButton.Name = "RefButton";
            this.RefButton.Size = new System.Drawing.Size(65, 22);
            this.RefButton.TabIndex = 5;
            this.RefButton.Text = "参照...";
            this.RefButton.UseVisualStyleBackColor = true;
            this.RefButton.Click += new System.EventHandler(this.RefButton_Click);
            // 
            // MaxSearchResultLabel
            // 
            this.MaxSearchResultLabel.BackColor = System.Drawing.SystemColors.Control;
            this.MaxSearchResultLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MaxSearchResultLabel.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.MaxSearchResultLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MaxSearchResultLabel.Location = new System.Drawing.Point(7, 33);
            this.MaxSearchResultLabel.Name = "MaxSearchResultLabel";
            this.MaxSearchResultLabel.Size = new System.Drawing.Size(239, 17);
            this.MaxSearchResultLabel.TabIndex = 1;
            this.MaxSearchResultLabel.Text = "検索結果最大件数";
            this.MaxSearchResultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MaxSeachResultNum
            // 
            this.MaxSeachResultNum.ErrorInfo.ErrorAction = C1.Win.C1Input.ErrorActionEnum.ResetValue;
            this.MaxSeachResultNum.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.MaxSeachResultNum.ImagePadding = new System.Windows.Forms.Padding(0);
            this.MaxSeachResultNum.Location = new System.Drawing.Point(250, 33);
            this.MaxSeachResultNum.Name = "MaxSeachResultNum";
            this.MaxSeachResultNum.ShowFocusRectangle = true;
            this.MaxSeachResultNum.Size = new System.Drawing.Size(100, 17);
            this.MaxSeachResultNum.TabIndex = 2;
            this.MaxSeachResultNum.Tag = null;
            this.MaxSeachResultNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MaxSeachResultNum.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.MaxSeachResultNum.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Info;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(5, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "検索画面の設定";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CancelButton1
            // 
            this.CancelButton1.Location = new System.Drawing.Point(486, 232);
            this.CancelButton1.Name = "CancelButton1";
            this.CancelButton1.Size = new System.Drawing.Size(88, 28);
            this.CancelButton1.TabIndex = 14;
            this.CancelButton1.Text = "キャンセル";
            this.CancelButton1.UseVisualStyleBackColor = true;
            this.CancelButton1.Click += new System.EventHandler(this.CancelButton1_Click);
            // 
            // ApplyButton
            // 
            this.ApplyButton.Location = new System.Drawing.Point(392, 232);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(88, 28);
            this.ApplyButton.TabIndex = 13;
            this.ApplyButton.Text = "適用";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // CauseLabel
            // 
            this.CauseLabel.BackColor = System.Drawing.SystemColors.Info;
            this.CauseLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CauseLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.CauseLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CauseLabel.Location = new System.Drawing.Point(6, 95);
            this.CauseLabel.Name = "CauseLabel";
            this.CauseLabel.Size = new System.Drawing.Size(194, 23);
            this.CauseLabel.TabIndex = 6;
            this.CauseLabel.Text = "インデックス作成時の設定";
            this.CauseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(355, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "ＭＢ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BufferSizeLimitNum
            // 
            this.BufferSizeLimitNum.ErrorInfo.ErrorAction = C1.Win.C1Input.ErrorActionEnum.ResetValue;
            this.BufferSizeLimitNum.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.BufferSizeLimitNum.ImagePadding = new System.Windows.Forms.Padding(0);
            this.BufferSizeLimitNum.Location = new System.Drawing.Point(250, 143);
            this.BufferSizeLimitNum.Name = "BufferSizeLimitNum";
            this.BufferSizeLimitNum.ShowFocusRectangle = true;
            this.BufferSizeLimitNum.Size = new System.Drawing.Size(100, 17);
            this.BufferSizeLimitNum.TabIndex = 11;
            this.BufferSizeLimitNum.Tag = null;
            this.BufferSizeLimitNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.BufferSizeLimitNum.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.BufferSizeLimitNum.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
            // 
            // BufferSizeLimitLabel
            // 
            this.BufferSizeLimitLabel.BackColor = System.Drawing.SystemColors.Control;
            this.BufferSizeLimitLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BufferSizeLimitLabel.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.BufferSizeLimitLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BufferSizeLimitLabel.Location = new System.Drawing.Point(6, 143);
            this.BufferSizeLimitLabel.Name = "BufferSizeLimitLabel";
            this.BufferSizeLimitLabel.Size = new System.Drawing.Size(239, 17);
            this.BufferSizeLimitLabel.TabIndex = 10;
            this.BufferSizeLimitLabel.Text = "最大使用メモリサイズ";
            this.BufferSizeLimitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(355, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "ＭＢ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FileSizeLimitNum
            // 
            this.FileSizeLimitNum.ErrorInfo.ErrorAction = C1.Win.C1Input.ErrorActionEnum.ResetValue;
            this.FileSizeLimitNum.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.FileSizeLimitNum.ImagePadding = new System.Windows.Forms.Padding(0);
            this.FileSizeLimitNum.Location = new System.Drawing.Point(250, 122);
            this.FileSizeLimitNum.Name = "FileSizeLimitNum";
            this.FileSizeLimitNum.ShowFocusRectangle = true;
            this.FileSizeLimitNum.Size = new System.Drawing.Size(100, 17);
            this.FileSizeLimitNum.TabIndex = 8;
            this.FileSizeLimitNum.Tag = null;
            this.FileSizeLimitNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.FileSizeLimitNum.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.FileSizeLimitNum.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
            // 
            // FileSizeLimitLabel
            // 
            this.FileSizeLimitLabel.BackColor = System.Drawing.SystemColors.Control;
            this.FileSizeLimitLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FileSizeLimitLabel.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.FileSizeLimitLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.FileSizeLimitLabel.Location = new System.Drawing.Point(6, 122);
            this.FileSizeLimitLabel.Name = "FileSizeLimitLabel";
            this.FileSizeLimitLabel.Size = new System.Drawing.Size(239, 17);
            this.FileSizeLimitLabel.TabIndex = 7;
            this.FileSizeLimitLabel.Text = "最大ファイルサイズ";
            this.FileSizeLimitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 265);
            this.Controls.Add(this.MainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "設定";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigForm_FormClosed);
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxSeachResultNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BufferSizeLimitNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileSizeLimitNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel MainPanel;
        private C1.Win.C1Input.C1NumericEdit MaxSeachResultNum;
        internal System.Windows.Forms.Label MaxSearchResultLabel;
        internal System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1NumericEdit FileSizeLimitNum;
        internal System.Windows.Forms.Label FileSizeLimitLabel;
        internal System.Windows.Forms.Label label2;
        private C1.Win.C1Input.C1NumericEdit BufferSizeLimitNum;
        internal System.Windows.Forms.Label BufferSizeLimitLabel;
        internal System.Windows.Forms.Label CauseLabel;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Button CancelButton1;
        internal System.Windows.Forms.Label label3;
        private FxCommonLib.Controls.FilePathTextBox DiffToolText;
        private System.Windows.Forms.Button RefButton;
    }
}