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
            this.MaxMoreLikeThisResultLabel = new System.Windows.Forms.Label();
            this.MaxMoreLikeThisResultNum = new C1.Win.C1Input.C1NumericEdit();
            this.ShowSuggestionCheck = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.OuterIndexCheck = new System.Windows.Forms.CheckBox();
            this.LocalIndexCheck = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DiffToolText = new FxCommonLib.Controls.FilePathTextBox(this.components);
            this.RefButton = new System.Windows.Forms.Button();
            this.MaxSearchResultLabel = new System.Windows.Forms.Label();
            this.MaxSearchResultNum = new C1.Win.C1Input.C1NumericEdit();
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
            ((System.ComponentModel.ISupportInitialize)(this.MaxMoreLikeThisResultNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxSearchResultNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BufferSizeLimitNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileSizeLimitNum)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.MaxMoreLikeThisResultLabel);
            this.MainPanel.Controls.Add(this.MaxMoreLikeThisResultNum);
            this.MainPanel.Controls.Add(this.ShowSuggestionCheck);
            this.MainPanel.Controls.Add(this.label6);
            this.MainPanel.Controls.Add(this.label4);
            this.MainPanel.Controls.Add(this.OuterIndexCheck);
            this.MainPanel.Controls.Add(this.LocalIndexCheck);
            this.MainPanel.Controls.Add(this.label3);
            this.MainPanel.Controls.Add(this.DiffToolText);
            this.MainPanel.Controls.Add(this.RefButton);
            this.MainPanel.Controls.Add(this.MaxSearchResultLabel);
            this.MainPanel.Controls.Add(this.MaxSearchResultNum);
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
            this.MainPanel.Size = new System.Drawing.Size(580, 338);
            this.MainPanel.TabIndex = 0;
            // 
            // MaxMoreLikeThisResultLabel
            // 
            this.MaxMoreLikeThisResultLabel.BackColor = System.Drawing.SystemColors.Control;
            this.MaxMoreLikeThisResultLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MaxMoreLikeThisResultLabel.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.MaxMoreLikeThisResultLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MaxMoreLikeThisResultLabel.Location = new System.Drawing.Point(14, 54);
            this.MaxMoreLikeThisResultLabel.Name = "MaxMoreLikeThisResultLabel";
            this.MaxMoreLikeThisResultLabel.Size = new System.Drawing.Size(239, 17);
            this.MaxMoreLikeThisResultLabel.TabIndex = 20;
            this.MaxMoreLikeThisResultLabel.Text = "類似検索結果最大件数";
            this.MaxMoreLikeThisResultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MaxMoreLikeThisResultNum
            // 
            this.MaxMoreLikeThisResultNum.ErrorInfo.ErrorAction = C1.Win.C1Input.ErrorActionEnum.ResetValue;
            this.MaxMoreLikeThisResultNum.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.MaxMoreLikeThisResultNum.ImagePadding = new System.Windows.Forms.Padding(0);
            this.MaxMoreLikeThisResultNum.Location = new System.Drawing.Point(257, 54);
            this.MaxMoreLikeThisResultNum.Name = "MaxMoreLikeThisResultNum";
            this.MaxMoreLikeThisResultNum.ShowFocusRectangle = true;
            this.MaxMoreLikeThisResultNum.Size = new System.Drawing.Size(100, 17);
            this.MaxMoreLikeThisResultNum.TabIndex = 21;
            this.MaxMoreLikeThisResultNum.Tag = null;
            this.MaxMoreLikeThisResultNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MaxMoreLikeThisResultNum.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.MaxMoreLikeThisResultNum.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
            // 
            // ShowSuggestionCheck
            // 
            this.ShowSuggestionCheck.AutoSize = true;
            this.ShowSuggestionCheck.Location = new System.Drawing.Point(13, 277);
            this.ShowSuggestionCheck.Name = "ShowSuggestionCheck";
            this.ShowSuggestionCheck.Size = new System.Drawing.Size(163, 16);
            this.ShowSuggestionCheck.TabIndex = 19;
            this.ShowSuggestionCheck.Text = "「もしかしてキーワード」を表示";
            this.ShowSuggestionCheck.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Info;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(5, 247);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 23);
            this.label6.TabIndex = 18;
            this.label6.Text = "「もしかして」キーワード";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Info;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(5, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 23);
            this.label4.TabIndex = 17;
            this.label4.Text = "デフォルト対象インデックス";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OuterIndexCheck
            // 
            this.OuterIndexCheck.Checked = true;
            this.OuterIndexCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OuterIndexCheck.Image = global::PokudaSearch.Properties.Resources.NetworkDrive16;
            this.OuterIndexCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OuterIndexCheck.Location = new System.Drawing.Point(167, 213);
            this.OuterIndexCheck.Name = "OuterIndexCheck";
            this.OuterIndexCheck.Size = new System.Drawing.Size(133, 21);
            this.OuterIndexCheck.TabIndex = 16;
            this.OuterIndexCheck.Text = "　　　外部インデックス";
            this.OuterIndexCheck.UseVisualStyleBackColor = true;
            // 
            // LocalIndexCheck
            // 
            this.LocalIndexCheck.Checked = true;
            this.LocalIndexCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LocalIndexCheck.Image = global::PokudaSearch.Properties.Resources.LocalDrive16;
            this.LocalIndexCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LocalIndexCheck.Location = new System.Drawing.Point(12, 213);
            this.LocalIndexCheck.Name = "LocalIndexCheck";
            this.LocalIndexCheck.Size = new System.Drawing.Size(151, 21);
            this.LocalIndexCheck.TabIndex = 15;
            this.LocalIndexCheck.Text = "　　　ローカルインデックス";
            this.LocalIndexCheck.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(14, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "差分ツール";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DiffToolText
            // 
            this.DiffToolText.AllowDrop = true;
            this.DiffToolText.Location = new System.Drawing.Point(100, 75);
            this.DiffToolText.Name = "DiffToolText";
            this.DiffToolText.Size = new System.Drawing.Size(404, 19);
            this.DiffToolText.TabIndex = 4;
            // 
            // RefButton
            // 
            this.RefButton.Location = new System.Drawing.Point(508, 73);
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
            this.MaxSearchResultLabel.Location = new System.Drawing.Point(14, 33);
            this.MaxSearchResultLabel.Name = "MaxSearchResultLabel";
            this.MaxSearchResultLabel.Size = new System.Drawing.Size(239, 17);
            this.MaxSearchResultLabel.TabIndex = 1;
            this.MaxSearchResultLabel.Text = "検索結果最大件数";
            this.MaxSearchResultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MaxSearchResultNum
            // 
            this.MaxSearchResultNum.ErrorInfo.ErrorAction = C1.Win.C1Input.ErrorActionEnum.ResetValue;
            this.MaxSearchResultNum.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.MaxSearchResultNum.ImagePadding = new System.Windows.Forms.Padding(0);
            this.MaxSearchResultNum.Location = new System.Drawing.Point(257, 33);
            this.MaxSearchResultNum.Name = "MaxSearchResultNum";
            this.MaxSearchResultNum.ShowFocusRectangle = true;
            this.MaxSearchResultNum.Size = new System.Drawing.Size(100, 17);
            this.MaxSearchResultNum.TabIndex = 2;
            this.MaxSearchResultNum.Tag = null;
            this.MaxSearchResultNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MaxSearchResultNum.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.MaxSearchResultNum.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
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
            this.CancelButton1.Location = new System.Drawing.Point(486, 304);
            this.CancelButton1.Name = "CancelButton1";
            this.CancelButton1.Size = new System.Drawing.Size(88, 28);
            this.CancelButton1.TabIndex = 14;
            this.CancelButton1.Text = "キャンセル";
            this.CancelButton1.UseVisualStyleBackColor = true;
            this.CancelButton1.Click += new System.EventHandler(this.CancelButton1_Click);
            // 
            // ApplyButton
            // 
            this.ApplyButton.Location = new System.Drawing.Point(392, 304);
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
            this.CauseLabel.Location = new System.Drawing.Point(6, 106);
            this.CauseLabel.Name = "CauseLabel";
            this.CauseLabel.Size = new System.Drawing.Size(181, 23);
            this.CauseLabel.TabIndex = 6;
            this.CauseLabel.Text = "インデックス作成時の設定";
            this.CauseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(362, 154);
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
            this.BufferSizeLimitNum.Location = new System.Drawing.Point(257, 154);
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
            this.BufferSizeLimitLabel.Location = new System.Drawing.Point(13, 154);
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
            this.label1.Location = new System.Drawing.Point(362, 133);
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
            this.FileSizeLimitNum.Location = new System.Drawing.Point(257, 133);
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
            this.FileSizeLimitLabel.Location = new System.Drawing.Point(13, 133);
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
            this.ClientSize = new System.Drawing.Size(580, 338);
            this.Controls.Add(this.MainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
            ((System.ComponentModel.ISupportInitialize)(this.MaxMoreLikeThisResultNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxSearchResultNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BufferSizeLimitNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileSizeLimitNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel MainPanel;
        private C1.Win.C1Input.C1NumericEdit MaxSearchResultNum;
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
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox OuterIndexCheck;
        private System.Windows.Forms.CheckBox LocalIndexCheck;
        private System.Windows.Forms.CheckBox ShowSuggestionCheck;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label MaxMoreLikeThisResultLabel;
        private C1.Win.C1Input.C1NumericEdit MaxMoreLikeThisResultNum;
    }
}