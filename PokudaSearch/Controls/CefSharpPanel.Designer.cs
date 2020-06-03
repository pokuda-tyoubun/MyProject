namespace PokudaSearch.Controls {
    partial class CefSharpPanel {
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.BrowserPanel = new System.Windows.Forms.Panel();
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.StatusPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BrowserPanel
            // 
            this.BrowserPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowserPanel.Location = new System.Drawing.Point(0, 0);
            this.BrowserPanel.Name = "BrowserPanel";
            this.BrowserPanel.Size = new System.Drawing.Size(473, 285);
            this.BrowserPanel.TabIndex = 0;
            // 
            // StatusPanel
            // 
            this.StatusPanel.Controls.Add(this.ProgressBar);
            this.StatusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StatusPanel.Location = new System.Drawing.Point(0, 285);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(473, 27);
            this.StatusPanel.TabIndex = 1;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(3, 2);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(125, 23);
            this.ProgressBar.TabIndex = 0;
            // 
            // CefSharpPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BrowserPanel);
            this.Controls.Add(this.StatusPanel);
            this.Name = "CefSharpPanel";
            this.Size = new System.Drawing.Size(473, 312);
            this.StatusPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BrowserPanel;
        private System.Windows.Forms.Panel StatusPanel;
        private System.Windows.Forms.ProgressBar ProgressBar;
    }
}
