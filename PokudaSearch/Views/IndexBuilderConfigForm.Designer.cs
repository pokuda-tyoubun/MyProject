namespace PokudaSearch.Views {
    partial class IndexBuilderConfigForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IndexBuilderConfigForm));
            this.RAMBufferSizeBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RAMBufferSizeText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ExtensionsGrid = new FxCommonLib.Controls.FlexGridEx(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.RAMBufferSizeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExtensionsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // RAMBufferSizeBar
            // 
            this.RAMBufferSizeBar.Location = new System.Drawing.Point(12, 37);
            this.RAMBufferSizeBar.Maximum = 1024;
            this.RAMBufferSizeBar.Minimum = 16;
            this.RAMBufferSizeBar.Name = "RAMBufferSizeBar";
            this.RAMBufferSizeBar.Size = new System.Drawing.Size(449, 45);
            this.RAMBufferSizeBar.TabIndex = 0;
            this.RAMBufferSizeBar.Value = 16;
            this.RAMBufferSizeBar.ValueChanged += new System.EventHandler(this.RAMBufferSizeBar_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(413, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "1,024MB";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "16MB";
            // 
            // RAMBufferSizeText
            // 
            this.RAMBufferSizeText.Location = new System.Drawing.Point(160, 73);
            this.RAMBufferSizeText.Name = "RAMBufferSizeText";
            this.RAMBufferSizeText.Size = new System.Drawing.Size(102, 19);
            this.RAMBufferSizeText.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(266, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "MB";
            // 
            // ExtensionsGrid
            // 
            this.ExtensionsGrid.AfterErrorInfo = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("ExtensionsGrid.AfterErrorInfo")));
            this.ExtensionsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExtensionsGrid.AutoClipboard = true;
            this.ExtensionsGrid.ColumnInfo = "10,1,0,0,0,90,Columns:";
            this.ExtensionsGrid.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.ExtensionsGrid.EnableReadOnlyColor = false;
            this.ExtensionsGrid.EnableUpdateCellStyle = false;
            this.ExtensionsGrid.GridName = null;
            this.ExtensionsGrid.IsCol1SelectCheck = false;
            this.ExtensionsGrid.IsEnterRight = false;
            this.ExtensionsGrid.Location = new System.Drawing.Point(2, 98);
            this.ExtensionsGrid.Name = "ExtensionsGrid";
            this.ExtensionsGrid.PulldownDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>)(resources.GetObject("ExtensionsGrid.PulldownDic")));
            this.ExtensionsGrid.Rows.DefaultSize = 18;
            this.ExtensionsGrid.Size = new System.Drawing.Size(478, 341);
            this.ExtensionsGrid.StyleInfo = resources.GetString("ExtensionsGrid.StyleInfo");
            this.ExtensionsGrid.TabIndex = 22;
            this.ExtensionsGrid.WindowsName = null;
            // 
            // IndexBuilderConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 451);
            this.Controls.Add(this.ExtensionsGrid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RAMBufferSizeText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RAMBufferSizeBar);
            this.Name = "IndexBuilderConfigForm";
            this.Text = "インデックス構築設定";
            ((System.ComponentModel.ISupportInitialize)(this.RAMBufferSizeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExtensionsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar RAMBufferSizeBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RAMBufferSizeText;
        private System.Windows.Forms.Label label3;
        private FxCommonLib.Controls.FlexGridEx ExtensionsGrid;
    }
}