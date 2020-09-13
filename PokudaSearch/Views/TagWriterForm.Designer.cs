namespace PokudaSearch.Views {
    partial class TagWriterForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagWriterForm));
            this.TargetGrid = new FxCommonLib.Controls.FlexGridEx(this.components);
            this.SearchButton = new System.Windows.Forms.Button();
            this.PathText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.WriteTagButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TargetGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // TargetGrid
            // 
            this.TargetGrid.AfterErrorInfo = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("TargetGrid.AfterErrorInfo")));
            this.TargetGrid.AllowEditing = false;
            this.TargetGrid.AllowFiltering = true;
            this.TargetGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.TargetGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetGrid.AutoClipboard = true;
            this.TargetGrid.CellButtonDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.HashSet<string>>)(resources.GetObject("TargetGrid.CellButtonDic")));
            this.TargetGrid.ColumnInfo = "10,1,0,0,0,90,Columns:";
            this.TargetGrid.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.TargetGrid.EnableReadOnlyColor = false;
            this.TargetGrid.EnableUpdateCellStyle = false;
            this.TargetGrid.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.TargetGrid.GridName = null;
            this.TargetGrid.IsCol1SelectCheck = false;
            this.TargetGrid.IsEnterRight = false;
            this.TargetGrid.Location = new System.Drawing.Point(274, 2);
            this.TargetGrid.Name = "TargetGrid";
            this.TargetGrid.PulldownDic = ((System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>)(resources.GetObject("TargetGrid.PulldownDic")));
            this.TargetGrid.Rows.Count = 2;
            this.TargetGrid.Rows.DefaultSize = 18;
            this.TargetGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.TargetGrid.ShowErrors = true;
            this.TargetGrid.Size = new System.Drawing.Size(711, 574);
            this.TargetGrid.StyleInfo = resources.GetString("TargetGrid.StyleInfo");
            this.TargetGrid.TabIndex = 10;
            this.TargetGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            this.TargetGrid.WindowsName = null;
            // 
            // SearchButton
            // 
            this.SearchButton.Image = global::PokudaSearch.Properties.Resources.Search24;
            this.SearchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SearchButton.Location = new System.Drawing.Point(12, 37);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(120, 40);
            this.SearchButton.TabIndex = 11;
            this.SearchButton.Text = "    検索(&S)";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // PathText
            // 
            this.PathText.Location = new System.Drawing.Point(82, 12);
            this.PathText.Name = "PathText";
            this.PathText.Size = new System.Drawing.Size(186, 19);
            this.PathText.TabIndex = 119;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(12, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 20);
            this.label5.TabIndex = 120;
            this.label5.Text = "パス";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WriteTagButton
            // 
            this.WriteTagButton.Image = global::PokudaSearch.Properties.Resources.Search24;
            this.WriteTagButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.WriteTagButton.Location = new System.Drawing.Point(138, 37);
            this.WriteTagButton.Name = "WriteTagButton";
            this.WriteTagButton.Size = new System.Drawing.Size(120, 40);
            this.WriteTagButton.TabIndex = 121;
            this.WriteTagButton.Text = "    タグ付け";
            this.WriteTagButton.UseVisualStyleBackColor = true;
            this.WriteTagButton.Click += new System.EventHandler(this.WriteTagButton_Click);
            // 
            // TagWriterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 588);
            this.Controls.Add(this.WriteTagButton);
            this.Controls.Add(this.PathText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.TargetGrid);
            this.Name = "TagWriterForm";
            this.Text = "TagWriterForm";
            ((System.ComponentModel.ISupportInitialize)(this.TargetGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FxCommonLib.Controls.FlexGridEx TargetGrid;
        internal System.Windows.Forms.Button SearchButton;
        internal System.Windows.Forms.TextBox PathText;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Button WriteTagButton;
    }
}