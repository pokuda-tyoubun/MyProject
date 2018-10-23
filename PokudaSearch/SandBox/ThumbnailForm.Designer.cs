namespace PokudaSearch.SandBox {
    partial class ThumbnailForm {
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
            this.FilePathText = new System.Windows.Forms.TextBox();
            this.PreviewPicture = new System.Windows.Forms.PictureBox();
            this.PreviewButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // FilePathText
            // 
            this.FilePathText.Location = new System.Drawing.Point(38, 24);
            this.FilePathText.Name = "FilePathText";
            this.FilePathText.Size = new System.Drawing.Size(203, 19);
            this.FilePathText.TabIndex = 0;
            // 
            // PreviewPicture
            // 
            this.PreviewPicture.Location = new System.Drawing.Point(39, 49);
            this.PreviewPicture.Name = "PreviewPicture";
            this.PreviewPicture.Size = new System.Drawing.Size(280, 178);
            this.PreviewPicture.TabIndex = 1;
            this.PreviewPicture.TabStop = false;
            // 
            // PreviewButton
            // 
            this.PreviewButton.Location = new System.Drawing.Point(247, 24);
            this.PreviewButton.Name = "PreviewButton";
            this.PreviewButton.Size = new System.Drawing.Size(72, 19);
            this.PreviewButton.TabIndex = 2;
            this.PreviewButton.Text = "表示";
            this.PreviewButton.UseVisualStyleBackColor = true;
            this.PreviewButton.Click += new System.EventHandler(this.PreviewButton_Click);
            // 
            // ThumbnailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 382);
            this.Controls.Add(this.PreviewButton);
            this.Controls.Add(this.PreviewPicture);
            this.Controls.Add(this.FilePathText);
            this.Name = "ThumbnailForm";
            this.Text = "ThumbnailForm";
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FilePathText;
        private System.Windows.Forms.PictureBox PreviewPicture;
        private System.Windows.Forms.Button PreviewButton;
    }
}