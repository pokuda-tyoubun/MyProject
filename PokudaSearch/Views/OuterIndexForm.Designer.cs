namespace PokudaSearch.Views {
    partial class OuterIndexForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OuterIndexForm));
            this.MainPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.RemotePathText = new FxCommonLib.Controls.FilePathTextBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.LocalPathText = new FxCommonLib.Controls.FilePathTextBox(this.components);
            this.RefButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ActiveIndexGrid = new FxCommonLib.Controls.FlexGridEx(this.components);
            this.CancelButton1 = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.IndexStorePathText = new FxCommonLib.Controls.FilePathTextBox(this.components);
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActiveIndexGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.label4);
            this.MainPanel.Controls.Add(this.IndexStorePathText);
            this.MainPanel.Controls.Add(this.label3);
            this.MainPanel.Controls.Add(this.RemotePathText);
            this.MainPanel.Controls.Add(this.label1);
            this.MainPanel.Controls.Add(this.LocalPathText);
            this.MainPanel.Controls.Add(this.RefButton);
            this.MainPanel.Controls.Add(this.label2);
            this.MainPanel.Controls.Add(this.ActiveIndexGrid);
            this.MainPanel.Controls.Add(this.CancelButton1);
            this.MainPanel.Controls.Add(this.OKButton);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(689, 505);
            this.MainPanel.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(6, 420);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 19);
            this.label3.TabIndex = 22;
            this.label3.Text = "リモート検索対象パス";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RemotePathText
            // 
            this.RemotePathText.AllowDrop = true;
            this.RemotePathText.Location = new System.Drawing.Point(130, 420);
            this.RemotePathText.Name = "RemotePathText";
            this.RemotePathText.ReadOnly = true;
            this.RemotePathText.Size = new System.Drawing.Size(479, 19);
            this.RemotePathText.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(6, 443);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 19);
            this.label1.TabIndex = 20;
            this.label1.Text = "対応ローカルパス";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LocalPathText
            // 
            this.LocalPathText.AllowDrop = true;
            this.LocalPathText.Location = new System.Drawing.Point(130, 443);
            this.LocalPathText.Name = "LocalPathText";
            this.LocalPathText.Size = new System.Drawing.Size(479, 19);
            this.LocalPathText.TabIndex = 21;
            // 
            // RefButton
            // 
            this.RefButton.Location = new System.Drawing.Point(613, 441);
            this.RefButton.Name = "RefButton";
            this.RefButton.Size = new System.Drawing.Size(65, 22);
            this.RefButton.TabIndex = 19;
            this.RefButton.Text = "参照...";
            this.RefButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(6, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 21);
            this.label2.TabIndex = 17;
            this.label2.Text = "外部有効インデックス";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ActiveIndexGrid
            // 
            this.ActiveIndexGrid.AfterErrorInfo = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("ActiveIndexGrid.AfterErrorInfo")));
            this.ActiveIndexGrid.AllowEditing = false;
            this.ActiveIndexGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ActiveIndexGrid.AutoClipboard = true;
            this.ActiveIndexGrid.CellButtonDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.HashSet<string>>)(resources.GetObject("ActiveIndexGrid.CellButtonDic")));
            this.ActiveIndexGrid.ColumnInfo = "10,1,0,0,0,90,Columns:";
            this.ActiveIndexGrid.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.ActiveIndexGrid.EnableReadOnlyColor = false;
            this.ActiveIndexGrid.EnableUpdateCellStyle = false;
            this.ActiveIndexGrid.GridName = null;
            this.ActiveIndexGrid.IsCol1SelectCheck = false;
            this.ActiveIndexGrid.IsEnterRight = false;
            this.ActiveIndexGrid.Location = new System.Drawing.Point(6, 30);
            this.ActiveIndexGrid.Name = "ActiveIndexGrid";
            this.ActiveIndexGrid.PulldownDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>)(resources.GetObject("ActiveIndexGrid.PulldownDic")));
            this.ActiveIndexGrid.Rows.DefaultSize = 18;
            this.ActiveIndexGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.ActiveIndexGrid.Size = new System.Drawing.Size(677, 361);
            this.ActiveIndexGrid.StyleInfo = resources.GetString("ActiveIndexGrid.StyleInfo");
            this.ActiveIndexGrid.TabIndex = 18;
            this.ActiveIndexGrid.WindowsName = null;
            this.ActiveIndexGrid.SelChange += new System.EventHandler(this.ActiveIndexGrid_SelChange);
            // 
            // CancelButton1
            // 
            this.CancelButton1.Location = new System.Drawing.Point(595, 472);
            this.CancelButton1.Name = "CancelButton1";
            this.CancelButton1.Size = new System.Drawing.Size(88, 28);
            this.CancelButton1.TabIndex = 14;
            this.CancelButton1.Text = "キャンセル";
            this.CancelButton1.UseVisualStyleBackColor = true;
            this.CancelButton1.Click += new System.EventHandler(this.CancelButton1_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(503, 472);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(88, 28);
            this.OKButton.TabIndex = 13;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(6, 397);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 19);
            this.label4.TabIndex = 24;
            this.label4.Text = "リモートインデックスパス";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IndexStorePathText
            // 
            this.IndexStorePathText.AllowDrop = true;
            this.IndexStorePathText.Location = new System.Drawing.Point(130, 397);
            this.IndexStorePathText.Name = "IndexStorePathText";
            this.IndexStorePathText.ReadOnly = true;
            this.IndexStorePathText.Size = new System.Drawing.Size(479, 19);
            this.IndexStorePathText.TabIndex = 25;
            // 
            // OuterIndexForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 505);
            this.Controls.Add(this.MainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OuterIndexForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "外部インデックス追加";
            this.Load += new System.EventHandler(this.OuterIndexForm_Load);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActiveIndexGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton1;
        private System.Windows.Forms.Label label2;
        private FxCommonLib.Controls.FlexGridEx ActiveIndexGrid;
        internal System.Windows.Forms.Label label1;
        private FxCommonLib.Controls.FilePathTextBox LocalPathText;
        private System.Windows.Forms.Button RefButton;
        internal System.Windows.Forms.Label label3;
        private FxCommonLib.Controls.FilePathTextBox RemotePathText;
        internal System.Windows.Forms.Label label4;
        private FxCommonLib.Controls.FilePathTextBox IndexStorePathText;
    }
}