namespace FxCommonLib.Controls {
    partial class BaseCodeSelector {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param customerName="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
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
            this.SelectButton = new System.Windows.Forms.Button();
            this.NameText = new C1.Win.C1Input.C1TextBox();
            this.Tooltip = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.CodeText = new FxCommonLib.Controls.CodeTextBoxEx(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.NameText)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectButton
            // 
            this.SelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectButton.Location = new System.Drawing.Point(143, 0);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(37, 20);
            this.SelectButton.TabIndex = 1;
            this.SelectButton.Text = "．．．";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // NameText
            // 
            this.NameText.Location = new System.Drawing.Point(1, 22);
            this.NameText.Name = "NameText";
            this.NameText.ReadOnly = true;
            this.NameText.Size = new System.Drawing.Size(178, 17);
            this.NameText.TabIndex = 2;
            this.NameText.Tag = null;
            this.NameText.MouseEnter += new System.EventHandler(this.NameText_MouseEnter);
            // 
            // Tooltip
            // 
            this.Tooltip.Font = new System.Drawing.Font("Tahoma", 8F);
            this.Tooltip.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            // 
            // CodeText
            // 
            this.CodeText.BackColor = System.Drawing.SystemColors.Menu;
            this.CodeText.Location = new System.Drawing.Point(0, 1);
            this.CodeText.MaxLength = 20;
            this.CodeText.Name = "CodeText";
            this.CodeText.NGWordRegex = "";
            this.CodeText.Size = new System.Drawing.Size(142, 19);
            this.CodeText.TabIndex = 0;
            // 
            // BaseCodeSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CodeText);
            this.Controls.Add(this.NameText);
            this.Controls.Add(this.SelectButton);
            this.Name = "BaseCodeSelector";
            this.Size = new System.Drawing.Size(180, 40);
            ((System.ComponentModel.ISupportInitialize)(this.NameText)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button SelectButton;
        protected C1.Win.C1Input.C1TextBox NameText;
        protected C1.Win.C1SuperTooltip.C1SuperTooltip Tooltip;
        protected FxCommonLib.Controls.CodeTextBoxEx CodeText;
    }
}
