namespace FxCommonLib.Controls {
    partial class DateEditDialog {
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
            this.CancelButton1 = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.InputDate = new C1.Win.C1Input.C1DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.InputDate)).BeginInit();
            this.SuspendLayout();
            // 
            // CancelButton1
            // 
            this.CancelButton1.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton1.Location = new System.Drawing.Point(118, 41);
            this.CancelButton1.Name = "CancelButton1";
            this.CancelButton1.Size = new System.Drawing.Size(100, 35);
            this.CancelButton1.TabIndex = 8;
            this.CancelButton1.Text = "キャンセル(&C)";
            this.CancelButton1.UseVisualStyleBackColor = false;
            this.CancelButton1.Click += new System.EventHandler(this.CancelButton1_Click);
            // 
            // OKButton
            // 
            this.OKButton.BackColor = System.Drawing.SystemColors.Control;
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(12, 41);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(100, 35);
            this.OKButton.TabIndex = 7;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = false;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // InputDate
            // 
            this.InputDate.AllowSpinLoop = false;
            // 
            // 
            // 
            this.InputDate.Calendar.DayNameLength = 1;
            this.InputDate.Font = new System.Drawing.Font("MS UI Gothic", 14.25F);
            this.InputDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.InputDate.ImagePadding = new System.Windows.Forms.Padding(0);
            this.InputDate.Location = new System.Drawing.Point(17, 12);
            this.InputDate.Name = "InputDate";
            this.InputDate.Size = new System.Drawing.Size(200, 24);
            this.InputDate.TabIndex = 9;
            this.InputDate.Tag = null;
            // 
            // DateEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 88);
            this.ControlBox = false;
            this.Controls.Add(this.InputDate);
            this.Controls.Add(this.CancelButton1);
            this.Controls.Add(this.OKButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DateEditDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "日付設定";
            this.Load += new System.EventHandler(this.DateEditDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.InputDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button CancelButton1;
        internal System.Windows.Forms.Button OKButton;
        private C1.Win.C1Input.C1DateEdit InputDate;

    }
}