﻿namespace FxCommonLib.Tests.Controls.Demo {
    partial class TextBoxExDemo {
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
            this.textBoxEx1 = new FxCommonLib.Controls.TextBoxEx(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxEx1
            // 
            this.textBoxEx1.Location = new System.Drawing.Point(29, 112);
            this.textBoxEx1.Multiline = true;
            this.textBoxEx1.Name = "textBoxEx1";
            this.textBoxEx1.NGWordRegex = "";
            this.textBoxEx1.Size = new System.Drawing.Size(310, 236);
            this.textBoxEx1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "・Ctrl+Aで全選択可能";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "・正規表現で入力禁止文字を指定可能\r\n　（このデモでは\'a\'が入力禁止）";
            // 
            // TextBoxExDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 360);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxEx1);
            this.Name = "TextBoxExDemo";
            this.Text = "TextBoxExDemo";
            this.Load += new System.EventHandler(this.TextBoxExDemo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FxCommonLib.Controls.TextBoxEx textBoxEx1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}