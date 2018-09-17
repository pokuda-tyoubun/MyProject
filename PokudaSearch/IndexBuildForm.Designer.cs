namespace PokudaSearch {
    partial class IndexBuildForm {
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
            this.AppendFile1Text = new FxCommonLib.Controls.FilePathTextBox(this.components);
            this.Ref1Button = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AppendFile1Text
            // 
            this.AppendFile1Text.AllowDrop = true;
            this.AppendFile1Text.Location = new System.Drawing.Point(7, 9);
            this.AppendFile1Text.Name = "AppendFile1Text";
            this.AppendFile1Text.Size = new System.Drawing.Size(460, 19);
            this.AppendFile1Text.TabIndex = 5;
            // 
            // Ref1Button
            // 
            this.Ref1Button.Location = new System.Drawing.Point(475, 8);
            this.Ref1Button.Name = "Ref1Button";
            this.Ref1Button.Size = new System.Drawing.Size(65, 22);
            this.Ref1Button.TabIndex = 6;
            this.Ref1Button.Text = "参照...";
            this.Ref1Button.UseVisualStyleBackColor = true;
            // 
            // UpdateButton
            // 
            this.UpdateButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.UpdateButton.Location = new System.Drawing.Point(427, 36);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(113, 39);
            this.UpdateButton.TabIndex = 14;
            this.UpdateButton.Text = "インデックス作成(&B)";
            this.UpdateButton.UseVisualStyleBackColor = true;
            // 
            // IndexBuildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 466);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.AppendFile1Text);
            this.Controls.Add(this.Ref1Button);
            this.Name = "IndexBuildForm";
            this.Text = "IndexBuildForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FxCommonLib.Controls.FilePathTextBox AppendFile1Text;
        private System.Windows.Forms.Button Ref1Button;
        private System.Windows.Forms.Button UpdateButton;
    }
}