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
            this.TumbnailButton = new System.Windows.Forms.Button();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.MultiSearcherButton = new System.Windows.Forms.Button();
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
            // TumbnailButton
            // 
            this.TumbnailButton.Location = new System.Drawing.Point(31, 191);
            this.TumbnailButton.Name = "TumbnailButton";
            this.TumbnailButton.Size = new System.Drawing.Size(121, 30);
            this.TumbnailButton.TabIndex = 4;
            this.TumbnailButton.Text = "サムネイル化";
            this.TumbnailButton.UseVisualStyleBackColor = true;
            this.TumbnailButton.Click += new System.EventHandler(this.TumbnailButton_Click);
            // 
            // PictureBox
            // 
            this.PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox.Location = new System.Drawing.Point(34, 227);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(240, 133);
            this.PictureBox.TabIndex = 5;
            this.PictureBox.TabStop = false;
            // 
            // MultiSearcherButton
            // 
            this.MultiSearcherButton.Location = new System.Drawing.Point(168, 111);
            this.MultiSearcherButton.Name = "MultiSearcherButton";
            this.MultiSearcherButton.Size = new System.Drawing.Size(139, 32);
            this.MultiSearcherButton.TabIndex = 6;
            this.MultiSearcherButton.Text = "MultiReaderテスト";
            this.MultiSearcherButton.UseVisualStyleBackColor = true;
            this.MultiSearcherButton.Click += new System.EventHandler(this.MultiSearcherButton_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 407);
            this.Controls.Add(this.MultiSearcherButton);
            this.Controls.Add(this.PictureBox);
            this.Controls.Add(this.TumbnailButton);
            this.Controls.Add(this.TikaTestButton);
            this.Controls.Add(this.TestButton);
            this.Controls.Add(this.TargetDirText);
            this.Controls.Add(this.CreateIndexButton);
            this.Name = "TestForm";
            this.Text = "フィージビリティテスト";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestForm_FormClosed);
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
        private System.Windows.Forms.Button TumbnailButton;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.Button MultiSearcherButton;
    }
}

