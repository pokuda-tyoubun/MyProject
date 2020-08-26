namespace FxCommonLib.Controls {
    partial class SearchBox {
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchBox));
            this._lblWatermark = new System.Windows.Forms.Label();
            this._txtSearch = new System.Windows.Forms.TextBox();
            this._lblIcon = new System.Windows.Forms.Label();
            this._lblClear = new System.Windows.Forms.Label();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // _lblWatermark
            // 
            this._lblWatermark.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._lblWatermark.AutoSize = true;
            this._lblWatermark.BackColor = System.Drawing.Color.Transparent;
            this._lblWatermark.ForeColor = System.Drawing.SystemColors.ControlDark;
            this._lblWatermark.Location = new System.Drawing.Point(18, 0);
            this._lblWatermark.Name = "_lblWatermark";
            this._lblWatermark.Size = new System.Drawing.Size(29, 12);
            this._lblWatermark.TabIndex = 15;
            this._lblWatermark.Text = "検索";
            this._lblWatermark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._lblWatermark.Click += new System.EventHandler(this._lblWatermark_Click);
            // 
            // _txtSearch
            // 
            this._txtSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._txtSearch.BackColor = System.Drawing.SystemColors.Window;
            this._txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._txtSearch.Location = new System.Drawing.Point(24, 4);
            this._txtSearch.Margin = new System.Windows.Forms.Padding(0);
            this._txtSearch.Name = "_txtSearch";
            this._txtSearch.Size = new System.Drawing.Size(86, 12);
            this._txtSearch.TabIndex = 12;
            this._txtSearch.TextChanged += new System.EventHandler(this._txtSearch_TextChanged);
            this._txtSearch.Enter += new System.EventHandler(this._txtSearch_Enter);
            this._txtSearch.Leave += new System.EventHandler(this._txtSearch_Leave);
            // 
            // _lblIcon
            // 
            this._lblIcon.BackColor = System.Drawing.Color.Transparent;
            this._lblIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this._lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("_lblIcon.Image")));
            this._lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._lblIcon.Location = new System.Drawing.Point(0, 0);
            this._lblIcon.Name = "_lblIcon";
            this._lblIcon.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this._lblIcon.Size = new System.Drawing.Size(24, 20);
            this._lblIcon.TabIndex = 13;
            // 
            // _lblClear
            // 
            this._lblClear.BackColor = System.Drawing.Color.Transparent;
            this._lblClear.Dock = System.Windows.Forms.DockStyle.Right;
            this._lblClear.Image = ((System.Drawing.Image)(resources.GetObject("_lblClear.Image")));
            this._lblClear.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._lblClear.Location = new System.Drawing.Point(108, 0);
            this._lblClear.Name = "_lblClear";
            this._lblClear.Size = new System.Drawing.Size(20, 20);
            this._lblClear.TabIndex = 14;
            this._lblClear.Click += new System.EventHandler(this._lblClear_Click);
            // 
            // _timer
            // 
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // SearchBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._lblWatermark);
            this.Controls.Add(this._txtSearch);
            this.Controls.Add(this._lblIcon);
            this.Controls.Add(this._lblClear);
            this.Name = "SearchBox";
            this.Size = new System.Drawing.Size(128, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _lblWatermark;
        private System.Windows.Forms.TextBox _txtSearch;
        private System.Windows.Forms.Label _lblIcon;
        private System.Windows.Forms.Label _lblClear;
        private System.Windows.Forms.Timer _timer;
    }
}
