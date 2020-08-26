namespace FxCommonLib.Controls {
    partial class NumberOfRowsInputDialog {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumberOfRowsInputDialog));
            this.RowNumericEdit = new C1.Win.C1Input.C1NumericEdit();
            this.label9 = new System.Windows.Forms.Label();
            this.AddButton = new System.Windows.Forms.Button();
            this.CancelButton1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RowNumericEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // RowNumericEdit
            // 
            this.RowNumericEdit.CharCategory = C1.Win.C1Input.CharCategory.Number;
            this.RowNumericEdit.DataType = typeof(long);
            this.RowNumericEdit.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat | C1.Win.C1Input.FormatInfoInheritFlags.NullText) 
            | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)));
            this.RowNumericEdit.ErrorInfo.ErrorAction = C1.Win.C1Input.ErrorActionEnum.ResetValue;
            this.RowNumericEdit.ErrorInfo.ErrorMessage = "範囲外の設定値です。";
            this.RowNumericEdit.ErrorInfo.ErrorMessageCaption = "入力値エラー";
            this.RowNumericEdit.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RowNumericEdit.ImagePadding = new System.Windows.Forms.Padding(0);
            this.RowNumericEdit.Location = new System.Drawing.Point(65, 9);
            this.RowNumericEdit.Name = "RowNumericEdit";
            this.RowNumericEdit.Size = new System.Drawing.Size(140, 21);
            this.RowNumericEdit.TabIndex = 1;
            this.RowNumericEdit.Tag = null;
            this.RowNumericEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.RowNumericEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.Info;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label9.Location = new System.Drawing.Point(12, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 23);
            this.label9.TabIndex = 0;
            this.label9.Text = "行数";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(15, 42);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(90, 30);
            this.AddButton.TabIndex = 2;
            this.AddButton.Text = "追加(&A)";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton1
            // 
            this.CancelButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton1.Location = new System.Drawing.Point(110, 42);
            this.CancelButton1.Name = "CancelButton1";
            this.CancelButton1.Size = new System.Drawing.Size(90, 30);
            this.CancelButton1.TabIndex = 3;
            this.CancelButton1.Text = "キャンセル(&C)";
            this.CancelButton1.UseVisualStyleBackColor = true;
            this.CancelButton1.Click += new System.EventHandler(this.CancelButton1_Click);
            // 
            // NumberOfRowsInputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 85);
            this.ControlBox = false;
            this.Controls.Add(this.CancelButton1);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.RowNumericEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NumberOfRowsInputDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新規追加行数";
            this.Load += new System.EventHandler(this.NumberOfRowsInputDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RowNumericEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Input.C1NumericEdit RowNumericEdit;
        internal System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button CancelButton1;
    }
}