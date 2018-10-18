namespace JapaneseAnalyzerTest {
    partial class Form1 {
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
            this.TestText = new System.Windows.Forms.TextBox();
            this.AnalyzeButton = new System.Windows.Forms.Button();
            this.ResultText = new System.Windows.Forms.TextBox();
            this.CreateIndexButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TestText
            // 
            this.TestText.Location = new System.Drawing.Point(1, 1);
            this.TestText.Multiline = true;
            this.TestText.Name = "TestText";
            this.TestText.Size = new System.Drawing.Size(707, 170);
            this.TestText.TabIndex = 0;
            // 
            // AnalyzeButton
            // 
            this.AnalyzeButton.Location = new System.Drawing.Point(5, 176);
            this.AnalyzeButton.Name = "AnalyzeButton";
            this.AnalyzeButton.Size = new System.Drawing.Size(93, 29);
            this.AnalyzeButton.TabIndex = 1;
            this.AnalyzeButton.Text = "解析";
            this.AnalyzeButton.UseVisualStyleBackColor = true;
            this.AnalyzeButton.Click += new System.EventHandler(this.AnalyzeButton_Click);
            // 
            // ResultText
            // 
            this.ResultText.Location = new System.Drawing.Point(1, 211);
            this.ResultText.Multiline = true;
            this.ResultText.Name = "ResultText";
            this.ResultText.Size = new System.Drawing.Size(707, 363);
            this.ResultText.TabIndex = 2;
            // 
            // CreateIndexButton
            // 
            this.CreateIndexButton.Location = new System.Drawing.Point(293, 176);
            this.CreateIndexButton.Name = "CreateIndexButton";
            this.CreateIndexButton.Size = new System.Drawing.Size(93, 29);
            this.CreateIndexButton.TabIndex = 3;
            this.CreateIndexButton.Text = "インデックス作成";
            this.CreateIndexButton.UseVisualStyleBackColor = true;
            this.CreateIndexButton.Click += new System.EventHandler(this.CreateIndexButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 576);
            this.Controls.Add(this.CreateIndexButton);
            this.Controls.Add(this.ResultText);
            this.Controls.Add(this.AnalyzeButton);
            this.Controls.Add(this.TestText);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TestText;
        private System.Windows.Forms.Button AnalyzeButton;
        private System.Windows.Forms.TextBox ResultText;
        private System.Windows.Forms.Button CreateIndexButton;
    }
}

