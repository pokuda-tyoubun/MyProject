namespace PokudaSearch.SandBox {
    partial class TestForm {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
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
            this.CreateIndexButton = new System.Windows.Forms.Button();
            this.TargetDirText = new System.Windows.Forms.TextBox();
            this.TestButton = new System.Windows.Forms.Button();
            this.TikaTestButton = new System.Windows.Forms.Button();
            this.ThumbnailButton = new System.Windows.Forms.Button();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.PathText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CreateIndexButton
            // 
            this.CreateIndexButton.Location = new System.Drawing.Point(22, 52);
            this.CreateIndexButton.Name = "CreateIndexButton";
            this.CreateIndexButton.Size = new System.Drawing.Size(130, 31);
            this.CreateIndexButton.TabIndex = 0;
            this.CreateIndexButton.Text = "インデックス作成";
            this.CreateIndexButton.UseVisualStyleBackColor = true;
            this.CreateIndexButton.Click += new System.EventHandler(this.CreateIndexButton_Click);
            // 
            // TargetDirText
            // 
            this.TargetDirText.Location = new System.Drawing.Point(22, 27);
            this.TargetDirText.Name = "TargetDirText";
            this.TargetDirText.Size = new System.Drawing.Size(409, 19);
            this.TargetDirText.TabIndex = 1;
            this.TargetDirText.Text = "C:\\Workspace\\MyToolBox\\Tool\\FullTextSearchCCC\\kitei";
            this.TargetDirText.TextChanged += new System.EventHandler(this.TargetDirText_TextChanged);
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(168, 52);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(139, 32);
            this.TestButton.TabIndex = 2;
            this.TestButton.Text = "インデックス作成テスト";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // TikaTestButton
            // 
            this.TikaTestButton.Location = new System.Drawing.Point(22, 111);
            this.TikaTestButton.Name = "TikaTestButton";
            this.TikaTestButton.Size = new System.Drawing.Size(124, 36);
            this.TikaTestButton.TabIndex = 3;
            this.TikaTestButton.Text = "TikaTest";
            this.TikaTestButton.UseVisualStyleBackColor = true;
            this.TikaTestButton.Click += new System.EventHandler(this.TikaTestButton_Click);
            // 
            // ThumbnailButton
            // 
            this.ThumbnailButton.Location = new System.Drawing.Point(22, 185);
            this.ThumbnailButton.Name = "ThumbnailButton";
            this.ThumbnailButton.Size = new System.Drawing.Size(162, 25);
            this.ThumbnailButton.TabIndex = 4;
            this.ThumbnailButton.Text = "サムネイル";
            this.ThumbnailButton.UseVisualStyleBackColor = true;
            this.ThumbnailButton.Click += new System.EventHandler(this.ThumbnailButton_Click);
            // 
            // PictureBox
            // 
            this.PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox.Location = new System.Drawing.Point(22, 216);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(162, 107);
            this.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox.TabIndex = 5;
            this.PictureBox.TabStop = false;
            // 
            // PathText
            // 
            this.PathText.Location = new System.Drawing.Point(22, 160);
            this.PathText.Name = "PathText";
            this.PathText.Size = new System.Drawing.Size(259, 19);
            this.PathText.TabIndex = 6;
            this.PathText.Text = "C:\\Temp\\WS000006.JPG";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 407);
            this.Controls.Add(this.PathText);
            this.Controls.Add(this.PictureBox);
            this.Controls.Add(this.ThumbnailButton);
            this.Controls.Add(this.TikaTestButton);
            this.Controls.Add(this.TestButton);
            this.Controls.Add(this.TargetDirText);
            this.Controls.Add(this.CreateIndexButton);
            this.Name = "TestForm";
            this.Text = "フィージビリティテスト";
            this.Load += new System.EventHandler(this.TestForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateIndexButton;
        private System.Windows.Forms.TextBox TargetDirText;
        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.Button TikaTestButton;
        private System.Windows.Forms.Button ThumbnailButton;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.TextBox PathText;
    }
}

