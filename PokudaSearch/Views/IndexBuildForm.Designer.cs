namespace PokudaSearch.Views {
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
            this.TargetDirText = new FxCommonLib.Controls.FilePathTextBox(this.components);
            this.ReferenceButton = new System.Windows.Forms.Button();
            this.CreateIndexButton = new System.Windows.Forms.Button();
            this.LogViewerText = new System.Windows.Forms.TextBox();
            this.TikaRadio = new System.Windows.Forms.RadioButton();
            this.IFilterRadio = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TargetDirText
            // 
            this.TargetDirText.AllowDrop = true;
            this.TargetDirText.Location = new System.Drawing.Point(7, 9);
            this.TargetDirText.Name = "TargetDirText";
            this.TargetDirText.Size = new System.Drawing.Size(460, 19);
            this.TargetDirText.TabIndex = 5;
            // 
            // ReferenceButton
            // 
            this.ReferenceButton.Location = new System.Drawing.Point(475, 8);
            this.ReferenceButton.Name = "ReferenceButton";
            this.ReferenceButton.Size = new System.Drawing.Size(65, 22);
            this.ReferenceButton.TabIndex = 6;
            this.ReferenceButton.Text = "参照...";
            this.ReferenceButton.UseVisualStyleBackColor = true;
            this.ReferenceButton.Click += new System.EventHandler(this.ReferenceButton_Click);
            // 
            // CreateIndexButton
            // 
            this.CreateIndexButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CreateIndexButton.Location = new System.Drawing.Point(475, 36);
            this.CreateIndexButton.Name = "CreateIndexButton";
            this.CreateIndexButton.Size = new System.Drawing.Size(113, 39);
            this.CreateIndexButton.TabIndex = 14;
            this.CreateIndexButton.Text = "インデックス作成(&B)";
            this.CreateIndexButton.UseVisualStyleBackColor = true;
            this.CreateIndexButton.Click += new System.EventHandler(this.CreateIndexButton_Click);
            // 
            // LogViewerText
            // 
            this.LogViewerText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogViewerText.Location = new System.Drawing.Point(5, 81);
            this.LogViewerText.Multiline = true;
            this.LogViewerText.Name = "LogViewerText";
            this.LogViewerText.ReadOnly = true;
            this.LogViewerText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogViewerText.Size = new System.Drawing.Size(750, 382);
            this.LogViewerText.TabIndex = 15;
            // 
            // TikaRadio
            // 
            this.TikaRadio.AutoSize = true;
            this.TikaRadio.Checked = true;
            this.TikaRadio.Location = new System.Drawing.Point(6, 17);
            this.TikaRadio.Name = "TikaRadio";
            this.TikaRadio.Size = new System.Drawing.Size(87, 16);
            this.TikaRadio.TabIndex = 16;
            this.TikaRadio.TabStop = true;
            this.TikaRadio.Text = "Apache Tika";
            this.TikaRadio.UseVisualStyleBackColor = true;
            // 
            // IFilterRadio
            // 
            this.IFilterRadio.AutoSize = true;
            this.IFilterRadio.Location = new System.Drawing.Point(110, 17);
            this.IFilterRadio.Name = "IFilterRadio";
            this.IFilterRadio.Size = new System.Drawing.Size(53, 16);
            this.IFilterRadio.TabIndex = 17;
            this.IFilterRadio.Text = "IFilter";
            this.IFilterRadio.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TikaRadio);
            this.groupBox1.Controls.Add(this.IFilterRadio);
            this.groupBox1.Location = new System.Drawing.Point(7, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 39);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "テキストコンバータ";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(226, 51);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(241, 22);
            this.ProgressBar.TabIndex = 19;
            // 
            // IndexBuildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 466);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LogViewerText);
            this.Controls.Add(this.CreateIndexButton);
            this.Controls.Add(this.TargetDirText);
            this.Controls.Add(this.ReferenceButton);
            this.Name = "IndexBuildForm";
            this.Text = "IndexBuildForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IndexBuildForm_FormClosed);
            this.Load += new System.EventHandler(this.IndexBuildForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FxCommonLib.Controls.FilePathTextBox TargetDirText;
        private System.Windows.Forms.Button ReferenceButton;
        private System.Windows.Forms.Button CreateIndexButton;
        private System.Windows.Forms.TextBox LogViewerText;
        private System.Windows.Forms.RadioButton TikaRadio;
        private System.Windows.Forms.RadioButton IFilterRadio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar ProgressBar;
    }
}