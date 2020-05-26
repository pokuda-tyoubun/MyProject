namespace PokudaSearch.Views {
    partial class LicenseVerificationForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicenseVerificationForm));
            this.MainPanel = new System.Windows.Forms.Panel();
            this.GetLicenseLink = new System.Windows.Forms.LinkLabel();
            this.LicenseKeyText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CancelButton1 = new System.Windows.Forms.Button();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.ExpiredLabel = new System.Windows.Forms.Label();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.ExpiredLabel);
            this.MainPanel.Controls.Add(this.GetLicenseLink);
            this.MainPanel.Controls.Add(this.LicenseKeyText);
            this.MainPanel.Controls.Add(this.label3);
            this.MainPanel.Controls.Add(this.CancelButton1);
            this.MainPanel.Controls.Add(this.ApplyButton);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(518, 90);
            this.MainPanel.TabIndex = 0;
            // 
            // GetLicenseLink
            // 
            this.GetLicenseLink.AutoSize = true;
            this.GetLicenseLink.Location = new System.Drawing.Point(356, 8);
            this.GetLicenseLink.Name = "GetLicenseLink";
            this.GetLicenseLink.Size = new System.Drawing.Size(157, 12);
            this.GetLicenseLink.TabIndex = 4;
            this.GetLicenseLink.TabStop = true;
            this.GetLicenseLink.Text = "ライセンスキーの取得はこちらから";
            this.GetLicenseLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GetLicenseLink_LinkClicked);
            // 
            // LicenseKeyText
            // 
            this.LicenseKeyText.Location = new System.Drawing.Point(93, 27);
            this.LicenseKeyText.Name = "LicenseKeyText";
            this.LicenseKeyText.Size = new System.Drawing.Size(420, 19);
            this.LicenseKeyText.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(7, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "ライセンスキー";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CancelButton1
            // 
            this.CancelButton1.Location = new System.Drawing.Point(424, 57);
            this.CancelButton1.Name = "CancelButton1";
            this.CancelButton1.Size = new System.Drawing.Size(88, 28);
            this.CancelButton1.TabIndex = 3;
            this.CancelButton1.Text = "キャンセル";
            this.CancelButton1.UseVisualStyleBackColor = true;
            this.CancelButton1.Click += new System.EventHandler(this.CancelButton1_Click);
            // 
            // ApplyButton
            // 
            this.ApplyButton.Location = new System.Drawing.Point(330, 57);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(88, 28);
            this.ApplyButton.TabIndex = 2;
            this.ApplyButton.Text = "送信";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // ExpiredLabel
            // 
            this.ExpiredLabel.AutoSize = true;
            this.ExpiredLabel.Location = new System.Drawing.Point(10, 8);
            this.ExpiredLabel.Name = "ExpiredLabel";
            this.ExpiredLabel.Size = new System.Drawing.Size(285, 12);
            this.ExpiredLabel.TabIndex = 5;
            this.ExpiredLabel.Text = "試用期間が終了しました。ライセンスキーを入力して下さい。";
            // 
            // LicenseVerificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 90);
            this.Controls.Add(this.MainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseVerificationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ライセンス認証";
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Button CancelButton1;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox LicenseKeyText;
        private System.Windows.Forms.LinkLabel GetLicenseLink;
        private System.Windows.Forms.Label ExpiredLabel;
    }
}