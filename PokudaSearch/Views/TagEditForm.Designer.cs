namespace PokudaSearch.Views {
    partial class TagEditForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagEditForm));
            this.MainPanel = new System.Windows.Forms.Panel();
            this.PackagePicture = new System.Windows.Forms.PictureBox();
            this.GetInfoButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.ImageRefButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RefButton = new System.Windows.Forms.Button();
            this.CancelButton1 = new System.Windows.Forms.Button();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.CauseLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.ImagePathText = new FxCommonLib.Controls.FilePathTextBox(this.components);
            this.CommentText = new FxCommonLib.Controls.FilePathTextBox(this.components);
            this.LylicsText = new FxCommonLib.Controls.FilePathTextBox(this.components);
            this.GenresText = new FxCommonLib.Controls.FilePathTextBox(this.components);
            this.ArtistText = new FxCommonLib.Controls.FilePathTextBox(this.components);
            this.TitleText = new FxCommonLib.Controls.FilePathTextBox(this.components);
            this.TargetPathText = new FxCommonLib.Controls.FilePathTextBox(this.components);
            this.ConfirmAgeButton = new System.Windows.Forms.Button();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PackagePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.ConfirmAgeButton);
            this.MainPanel.Controls.Add(this.PackagePicture);
            this.MainPanel.Controls.Add(this.GetInfoButton);
            this.MainPanel.Controls.Add(this.label6);
            this.MainPanel.Controls.Add(this.ImagePathText);
            this.MainPanel.Controls.Add(this.ImageRefButton);
            this.MainPanel.Controls.Add(this.CommentText);
            this.MainPanel.Controls.Add(this.label5);
            this.MainPanel.Controls.Add(this.LylicsText);
            this.MainPanel.Controls.Add(this.label4);
            this.MainPanel.Controls.Add(this.GenresText);
            this.MainPanel.Controls.Add(this.label2);
            this.MainPanel.Controls.Add(this.ArtistText);
            this.MainPanel.Controls.Add(this.label1);
            this.MainPanel.Controls.Add(this.TitleText);
            this.MainPanel.Controls.Add(this.label3);
            this.MainPanel.Controls.Add(this.TargetPathText);
            this.MainPanel.Controls.Add(this.RefButton);
            this.MainPanel.Controls.Add(this.CancelButton1);
            this.MainPanel.Controls.Add(this.ApplyButton);
            this.MainPanel.Controls.Add(this.CauseLabel);
            this.MainPanel.Controls.Add(this.TitleLabel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(580, 504);
            this.MainPanel.TabIndex = 0;
            this.MainPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainPanel_DragDrop);
            this.MainPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainPanel_DragEnter);
            this.MainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Paint);
            // 
            // PackagePicture
            // 
            this.PackagePicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PackagePicture.Location = new System.Drawing.Point(93, 389);
            this.PackagePicture.Name = "PackagePicture";
            this.PackagePicture.Size = new System.Drawing.Size(145, 112);
            this.PackagePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PackagePicture.TabIndex = 28;
            this.PackagePicture.TabStop = false;
            // 
            // GetInfoButton
            // 
            this.GetInfoButton.Location = new System.Drawing.Point(242, 471);
            this.GetInfoButton.Name = "GetInfoButton";
            this.GetInfoButton.Size = new System.Drawing.Size(88, 28);
            this.GetInfoButton.TabIndex = 27;
            this.GetInfoButton.Text = "Webから取得";
            this.GetInfoButton.UseVisualStyleBackColor = true;
            this.GetInfoButton.Click += new System.EventHandler(this.GetInfoButton_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(5, 366);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 19);
            this.label6.TabIndex = 24;
            this.label6.Text = "画像ファイル";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ImageRefButton
            // 
            this.ImageRefButton.Location = new System.Drawing.Point(500, 364);
            this.ImageRefButton.Name = "ImageRefButton";
            this.ImageRefButton.Size = new System.Drawing.Size(65, 22);
            this.ImageRefButton.TabIndex = 26;
            this.ImageRefButton.Text = "参照...";
            this.ImageRefButton.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(5, 262);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 17);
            this.label5.TabIndex = 22;
            this.label5.Text = "コメント";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(5, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "歌詞";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(6, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 17);
            this.label2.TabIndex = 18;
            this.label2.Text = "ジャンル";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(6, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "アーティスト";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(7, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "対象ファイル";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RefButton
            // 
            this.RefButton.Location = new System.Drawing.Point(501, 6);
            this.RefButton.Name = "RefButton";
            this.RefButton.Size = new System.Drawing.Size(65, 22);
            this.RefButton.TabIndex = 5;
            this.RefButton.Text = "参照...";
            this.RefButton.UseVisualStyleBackColor = true;
            this.RefButton.Click += new System.EventHandler(this.RefButton_Click);
            // 
            // CancelButton1
            // 
            this.CancelButton1.Location = new System.Drawing.Point(486, 471);
            this.CancelButton1.Name = "CancelButton1";
            this.CancelButton1.Size = new System.Drawing.Size(88, 28);
            this.CancelButton1.TabIndex = 14;
            this.CancelButton1.Text = "キャンセル";
            this.CancelButton1.UseVisualStyleBackColor = true;
            this.CancelButton1.Click += new System.EventHandler(this.CancelButton1_Click);
            // 
            // ApplyButton
            // 
            this.ApplyButton.Location = new System.Drawing.Point(392, 471);
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
            this.CauseLabel.Location = new System.Drawing.Point(6, 68);
            this.CauseLabel.Name = "CauseLabel";
            this.CauseLabel.Size = new System.Drawing.Size(119, 23);
            this.CauseLabel.TabIndex = 6;
            this.CauseLabel.Text = "メタデータ";
            this.CauseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TitleLabel
            // 
            this.TitleLabel.BackColor = System.Drawing.SystemColors.Control;
            this.TitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TitleLabel.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.TitleLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TitleLabel.Location = new System.Drawing.Point(6, 95);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(82, 17);
            this.TitleLabel.TabIndex = 7;
            this.TitleLabel.Text = "タイトル";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ImagePathText
            // 
            this.ImagePathText.AllowDrop = true;
            this.ImagePathText.Location = new System.Drawing.Point(92, 366);
            this.ImagePathText.Name = "ImagePathText";
            this.ImagePathText.Size = new System.Drawing.Size(404, 19);
            this.ImagePathText.TabIndex = 25;
            // 
            // CommentText
            // 
            this.CommentText.AllowDrop = true;
            this.CommentText.Location = new System.Drawing.Point(92, 262);
            this.CommentText.Multiline = true;
            this.CommentText.Name = "CommentText";
            this.CommentText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CommentText.Size = new System.Drawing.Size(464, 101);
            this.CommentText.TabIndex = 23;
            // 
            // LylicsText
            // 
            this.LylicsText.AllowDrop = true;
            this.LylicsText.Location = new System.Drawing.Point(92, 158);
            this.LylicsText.Multiline = true;
            this.LylicsText.Name = "LylicsText";
            this.LylicsText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LylicsText.Size = new System.Drawing.Size(464, 101);
            this.LylicsText.TabIndex = 21;
            // 
            // GenresText
            // 
            this.GenresText.AllowDrop = true;
            this.GenresText.Location = new System.Drawing.Point(92, 136);
            this.GenresText.Name = "GenresText";
            this.GenresText.Size = new System.Drawing.Size(183, 19);
            this.GenresText.TabIndex = 19;
            // 
            // ArtistText
            // 
            this.ArtistText.AllowDrop = true;
            this.ArtistText.Location = new System.Drawing.Point(92, 115);
            this.ArtistText.Name = "ArtistText";
            this.ArtistText.Size = new System.Drawing.Size(183, 19);
            this.ArtistText.TabIndex = 17;
            // 
            // TitleText
            // 
            this.TitleText.AllowDrop = true;
            this.TitleText.Location = new System.Drawing.Point(92, 94);
            this.TitleText.Name = "TitleText";
            this.TitleText.Size = new System.Drawing.Size(183, 19);
            this.TitleText.TabIndex = 15;
            // 
            // TargetPathText
            // 
            this.TargetPathText.AllowDrop = true;
            this.TargetPathText.Location = new System.Drawing.Point(93, 8);
            this.TargetPathText.Name = "TargetPathText";
            this.TargetPathText.Size = new System.Drawing.Size(404, 19);
            this.TargetPathText.TabIndex = 4;
            this.TargetPathText.Leave += new System.EventHandler(this.TargetPathText_Leave);
            // 
            // ConfirmAgeButton
            // 
            this.ConfirmAgeButton.Location = new System.Drawing.Point(242, 420);
            this.ConfirmAgeButton.Name = "ConfirmAgeButton";
            this.ConfirmAgeButton.Size = new System.Drawing.Size(88, 28);
            this.ConfirmAgeButton.TabIndex = 29;
            this.ConfirmAgeButton.Text = "年齢認証";
            this.ConfirmAgeButton.UseVisualStyleBackColor = true;
            this.ConfirmAgeButton.Click += new System.EventHandler(this.ConfirmAgeButton_Click);
            // 
            // TagEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 504);
            this.Controls.Add(this.MainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TagEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "タグ編集";
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PackagePicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel MainPanel;
        internal System.Windows.Forms.Label TitleLabel;
        internal System.Windows.Forms.Label CauseLabel;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Button CancelButton1;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button RefButton;
        private FxCommonLib.Controls.FilePathTextBox TargetPathText;
        private FxCommonLib.Controls.FilePathTextBox GenresText;
        internal System.Windows.Forms.Label label2;
        private FxCommonLib.Controls.FilePathTextBox ArtistText;
        internal System.Windows.Forms.Label label1;
        private FxCommonLib.Controls.FilePathTextBox TitleText;
        private FxCommonLib.Controls.FilePathTextBox LylicsText;
        internal System.Windows.Forms.Label label4;
        private FxCommonLib.Controls.FilePathTextBox CommentText;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label6;
        private FxCommonLib.Controls.FilePathTextBox ImagePathText;
        private System.Windows.Forms.Button ImageRefButton;
        private System.Windows.Forms.Button GetInfoButton;
        private System.Windows.Forms.PictureBox PackagePicture;
        private System.Windows.Forms.Button ConfirmAgeButton;
    }
}