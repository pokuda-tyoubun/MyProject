namespace FxCommonLib.BaseForm {
    partial class BaseColConfDialog {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseColConfDialog));
            this.ReleaseAllButton = new System.Windows.Forms.Button();
            this.SelectAllButton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CancelButton1 = new System.Windows.Forms.Button();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ConfigPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.HeightNum = new C1.Win.C1Input.C1NumericEdit();
            this.FixedNum = new C1.Win.C1Input.C1NumericEdit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FixedNum)).BeginInit();
            this.SuspendLayout();
            // 
            // ReleaseAllButton
            // 
            resources.ApplyResources(this.ReleaseAllButton, "ReleaseAllButton");
            this.ReleaseAllButton.Name = "ReleaseAllButton";
            this.ReleaseAllButton.UseVisualStyleBackColor = true;
            this.ReleaseAllButton.Click += new System.EventHandler(this.ReleaseAllButton_Click);
            // 
            // SelectAllButton
            // 
            resources.ApplyResources(this.SelectAllButton, "SelectAllButton");
            this.SelectAllButton.Name = "SelectAllButton";
            this.SelectAllButton.UseVisualStyleBackColor = true;
            this.SelectAllButton.Click += new System.EventHandler(this.SelectAllButton_Click);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.SystemColors.Info;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.SystemColors.Info;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.SystemColors.Info;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Info;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // CancelButton1
            // 
            resources.ApplyResources(this.CancelButton1, "CancelButton1");
            this.CancelButton1.Name = "CancelButton1";
            this.CancelButton1.UseVisualStyleBackColor = true;
            this.CancelButton1.Click += new System.EventHandler(this.CancelButton1_Click);
            // 
            // ApplyButton
            // 
            resources.ApplyResources(this.ApplyButton, "ApplyButton");
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Info;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // ConfigPanel
            // 
            resources.ApplyResources(this.ConfigPanel, "ConfigPanel");
            this.ConfigPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ConfigPanel.Name = "ConfigPanel";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Info;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // HeightNum
            // 
            this.HeightNum.CharCategory = C1.Win.C1Input.CharCategory.Number;
            resources.ApplyResources(this.HeightNum, "HeightNum");
            this.HeightNum.ErrorInfo.ErrorAction = C1.Win.C1Input.ErrorActionEnum.ResetValue;
            this.HeightNum.ImagePadding = new System.Windows.Forms.Padding(0);
            this.HeightNum.Name = "HeightNum";
            // 
            // FixedNum
            // 
            this.FixedNum.CharCategory = C1.Win.C1Input.CharCategory.Number;
            resources.ApplyResources(this.FixedNum, "FixedNum");
            this.FixedNum.ErrorInfo.ErrorAction = C1.Win.C1Input.ErrorActionEnum.ResetValue;
            this.FixedNum.ImagePadding = new System.Windows.Forms.Padding(0);
            this.FixedNum.Name = "FixedNum";
            // 
            // BaseColConfDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FixedNum);
            this.Controls.Add(this.HeightNum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConfigPanel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ReleaseAllButton);
            this.Controls.Add(this.SelectAllButton);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CancelButton1);
            this.Controls.Add(this.ApplyButton);
            this.Name = "BaseColConfDialog";
            this.Load += new System.EventHandler(this.BaseColConfDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.HeightNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FixedNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ReleaseAllButton;
        private System.Windows.Forms.Button SelectAllButton;
        internal System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button CancelButton1;
        private System.Windows.Forms.Button ApplyButton;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel ConfigPanel;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label5;
        private C1.Win.C1Input.C1NumericEdit HeightNum;
        private C1.Win.C1Input.C1NumericEdit FixedNum;
    }
}