namespace PokudaSearch.Views {
    partial class FileExplorerForm {
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
            this.commandLink1 = new Microsoft.WindowsAPICodePack.Controls.WindowsForms.CommandLink();
            this.explorerBrowser1 = new Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser();
            this.SuspendLayout();
            // 
            // commandLink1
            // 
            this.commandLink1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.commandLink1.Location = new System.Drawing.Point(34, 43);
            this.commandLink1.Name = "commandLink1";
            this.commandLink1.NoteText = "";
            this.commandLink1.Size = new System.Drawing.Size(180, 60);
            this.commandLink1.TabIndex = 0;
            this.commandLink1.Text = "commandLink1";
            this.commandLink1.UseVisualStyleBackColor = true;
            // 
            // explorerBrowser1
            // 
            this.explorerBrowser1.Location = new System.Drawing.Point(291, 103);
            this.explorerBrowser1.Name = "explorerBrowser1";
            this.explorerBrowser1.PropertyBagName = "Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser";
            this.explorerBrowser1.Size = new System.Drawing.Size(453, 401);
            this.explorerBrowser1.TabIndex = 1;
            // 
            // FileExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 647);
            this.Controls.Add(this.explorerBrowser1);
            this.Controls.Add(this.commandLink1);
            this.Name = "FileExplorerForm";
            this.Text = "エクスプローラ";
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.WindowsAPICodePack.Controls.WindowsForms.CommandLink commandLink1;
        private Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser explorerBrowser1;
    }
}