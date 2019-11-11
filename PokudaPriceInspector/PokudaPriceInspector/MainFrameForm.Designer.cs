namespace PokudaPriceInspector {
    partial class MainFrameForm {
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
            this.RunButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(45, 57);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(111, 33);
            this.RunButton.TabIndex = 0;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // MainFrameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 576);
            this.Controls.Add(this.RunButton);
            this.Name = "MainFrameForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainFrameForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RunButton;
    }
}

