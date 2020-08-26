namespace FxCommonLib.Tests.Controls.Demo {
    partial class FlexGridExDemo {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlexGridExDemo));
            this.flexGridEx2 = new FxCommonLib.Controls.FlexGridEx(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.flexGridEx1 = new FxCommonLib.Controls.FlexGridEx(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.flexGridEx2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flexGridEx1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flexGridEx2
            // 
            this.flexGridEx2.AfterErrorInfo = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("flexGridEx2.AfterErrorInfo")));
            this.flexGridEx2.AllowFiltering = true;
            this.flexGridEx2.AutoClipboard = true;
            this.flexGridEx2.AutoGenerateColumns = false;
            this.flexGridEx2.CellButtonDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.HashSet<string>>)(resources.GetObject("flexGridEx2.CellButtonDic")));
            this.flexGridEx2.ColumnInfo = "10,1,0,0,0,90,Columns:";
            this.flexGridEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flexGridEx2.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.flexGridEx2.EnableReadOnlyColor = false;
            this.flexGridEx2.EnableUpdateCellStyle = true;
            this.flexGridEx2.GridName = null;
            this.flexGridEx2.IsCol1SelectCheck = false;
            this.flexGridEx2.IsEnterRight = false;
            this.flexGridEx2.Location = new System.Drawing.Point(0, 302);
            this.flexGridEx2.Name = "flexGridEx2";
            this.flexGridEx2.PulldownDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>)(resources.GetObject("flexGridEx2.PulldownDic")));
            this.flexGridEx2.Rows.DefaultSize = 18;
            this.flexGridEx2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.flexGridEx2.Size = new System.Drawing.Size(1526, 261);
            this.flexGridEx2.StyleInfo = resources.GetString("flexGridEx2.StyleInfo");
            this.flexGridEx2.TabIndex = 9;
            this.flexGridEx2.WindowsName = null;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.checkBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 275);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1526, 27);
            this.panel2.TabIndex = 8;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(133, 5);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(107, 16);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "パスワード列表示";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.CheckBox2_CheckedChanged);
            // 
            // flexGridEx1
            // 
            this.flexGridEx1.AfterErrorInfo = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("flexGridEx1.AfterErrorInfo")));
            this.flexGridEx1.AllowFiltering = true;
            this.flexGridEx1.AutoClipboard = true;
            this.flexGridEx1.AutoGenerateColumns = false;
            this.flexGridEx1.CellButtonDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.HashSet<string>>)(resources.GetObject("flexGridEx1.CellButtonDic")));
            this.flexGridEx1.ColumnInfo = "10,1,0,0,0,90,Columns:";
            this.flexGridEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flexGridEx1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.flexGridEx1.EnableReadOnlyColor = false;
            this.flexGridEx1.EnableUpdateCellStyle = true;
            this.flexGridEx1.GridName = null;
            this.flexGridEx1.IsCol1SelectCheck = false;
            this.flexGridEx1.IsEnterRight = false;
            this.flexGridEx1.Location = new System.Drawing.Point(0, 27);
            this.flexGridEx1.Name = "flexGridEx1";
            this.flexGridEx1.PulldownDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>)(resources.GetObject("flexGridEx1.PulldownDic")));
            this.flexGridEx1.Rows.DefaultSize = 18;
            this.flexGridEx1.Size = new System.Drawing.Size(1526, 248);
            this.flexGridEx1.StyleInfo = resources.GetString("flexGridEx1.StyleInfo");
            this.flexGridEx1.TabIndex = 7;
            this.flexGridEx1.WindowsName = null;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1526, 27);
            this.panel1.TabIndex = 6;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(133, 8);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(107, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "パスワード列表示";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "SelectionMode：ListBox";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "SelectionMode：Default";
            // 
            // FlexGridExDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1526, 563);
            this.Controls.Add(this.flexGridEx2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.flexGridEx1);
            this.Controls.Add(this.panel1);
            this.Name = "FlexGridExDemo";
            this.Text = "FlexGridExDemo";
            ((System.ComponentModel.ISupportInitialize)(this.flexGridEx2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flexGridEx1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FxCommonLib.Controls.FlexGridEx flexGridEx2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBox2;
        private FxCommonLib.Controls.FlexGridEx flexGridEx1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}