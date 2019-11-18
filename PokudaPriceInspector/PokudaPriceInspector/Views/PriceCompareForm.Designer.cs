namespace PokudaPriceInspector.Views {
    partial class PriceCompareForm {
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
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.LeftBrowser = new System.Windows.Forms.WebBrowser();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.RightBrowser = new System.Windows.Forms.WebBrowser();
            this.RunButton = new System.Windows.Forms.Button();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.RatePanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.KeywordText = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.LeftSplitter = new NJFLib.Controls.CollapsibleSplitter();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.GetUsdJpyRateButton = new System.Windows.Forms.Button();
            this.GetCnhJpyRateButton = new System.Windows.Forms.Button();
            this.UsdJpyRateNum = new C1.Win.C1Input.C1NumericEdit();
            this.CnhJpyRateNum = new C1.Win.C1Input.C1NumericEdit();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UsdJpyRateNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CnhJpyRateNum)).BeginInit();
            this.SuspendLayout();
            // 
            // LeftPanel
            // 
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 601);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(507, 102);
            this.LeftPanel.TabIndex = 0;
            // 
            // LeftBrowser
            // 
            this.LeftBrowser.Location = new System.Drawing.Point(440, 6);
            this.LeftBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.LeftBrowser.Name = "LeftBrowser";
            this.LeftBrowser.Size = new System.Drawing.Size(84, 64);
            this.LeftBrowser.TabIndex = 0;
            // 
            // RightPanel
            // 
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightPanel.Location = new System.Drawing.Point(507, 601);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(445, 102);
            this.RightPanel.TabIndex = 1;
            // 
            // RightBrowser
            // 
            this.RightBrowser.Location = new System.Drawing.Point(530, 6);
            this.RightBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.RightBrowser.Name = "RightBrowser";
            this.RightBrowser.Size = new System.Drawing.Size(73, 64);
            this.RightBrowser.TabIndex = 0;
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(803, 40);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(137, 30);
            this.RunButton.TabIndex = 2;
            this.RunButton.Text = "Run!!";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.CnhJpyRateNum);
            this.TopPanel.Controls.Add(this.UsdJpyRateNum);
            this.TopPanel.Controls.Add(this.GetCnhJpyRateButton);
            this.TopPanel.Controls.Add(this.GetUsdJpyRateButton);
            this.TopPanel.Controls.Add(this.label3);
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Controls.Add(this.RatePanel);
            this.TopPanel.Controls.Add(this.RightBrowser);
            this.TopPanel.Controls.Add(this.LeftBrowser);
            this.TopPanel.Controls.Add(this.label2);
            this.TopPanel.Controls.Add(this.KeywordText);
            this.TopPanel.Controls.Add(this.RunButton);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(952, 591);
            this.TopPanel.TabIndex = 3;
            // 
            // RatePanel
            // 
            this.RatePanel.Location = new System.Drawing.Point(614, 6);
            this.RatePanel.Name = "RatePanel";
            this.RatePanel.Size = new System.Drawing.Size(113, 74);
            this.RatePanel.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(6, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "キーワード";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // KeywordText
            // 
            this.KeywordText.Location = new System.Drawing.Point(78, 6);
            this.KeywordText.Name = "KeywordText";
            this.KeywordText.Size = new System.Drawing.Size(356, 19);
            this.KeywordText.TabIndex = 4;
            // 
            // splitter1
            // 
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.splitter1.Location = new System.Drawing.Point(0, 591);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(952, 10);
            this.splitter1.TabIndex = 237;
            this.splitter1.TabStop = false;
            // 
            // LeftSplitter
            // 
            this.LeftSplitter.AnimationDelay = 20;
            this.LeftSplitter.AnimationStep = 20;
            this.LeftSplitter.BorderStyle3D = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.LeftSplitter.ControlToHide = this.LeftPanel;
            this.LeftSplitter.ExpandParentForm = false;
            this.LeftSplitter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LeftSplitter.Location = new System.Drawing.Point(507, 601);
            this.LeftSplitter.Name = "LeftSplitter";
            this.LeftSplitter.Size = new System.Drawing.Size(8, 102);
            this.LeftSplitter.TabIndex = 238;
            this.LeftSplitter.TabStop = false;
            this.LeftSplitter.UseAnimations = false;
            this.LeftSplitter.VisualStyle = NJFLib.Controls.VisualStyles.DoubleDots;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "USDJPY";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(6, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "CNHJPY";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GetUsdJpyRateButton
            // 
            this.GetUsdJpyRateButton.Location = new System.Drawing.Point(180, 29);
            this.GetUsdJpyRateButton.Name = "GetUsdJpyRateButton";
            this.GetUsdJpyRateButton.Size = new System.Drawing.Size(69, 21);
            this.GetUsdJpyRateButton.TabIndex = 10;
            this.GetUsdJpyRateButton.Text = "再取得";
            this.GetUsdJpyRateButton.UseVisualStyleBackColor = true;
            this.GetUsdJpyRateButton.Click += new System.EventHandler(this.GetUsdJpyRateButton_Click);
            // 
            // GetCnhJpyRateButton
            // 
            this.GetCnhJpyRateButton.Location = new System.Drawing.Point(180, 53);
            this.GetCnhJpyRateButton.Name = "GetCnhJpyRateButton";
            this.GetCnhJpyRateButton.Size = new System.Drawing.Size(69, 22);
            this.GetCnhJpyRateButton.TabIndex = 11;
            this.GetCnhJpyRateButton.Text = "再取得";
            this.GetCnhJpyRateButton.UseVisualStyleBackColor = true;
            this.GetCnhJpyRateButton.Click += new System.EventHandler(this.GetCnhJpyRateButton_Click);
            // 
            // UsdJpyRateNum
            // 
            this.UsdJpyRateNum.ErrorInfo.ErrorAction = C1.Win.C1Input.ErrorActionEnum.ResetValue;
            this.UsdJpyRateNum.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UsdJpyRateNum.ImagePadding = new System.Windows.Forms.Padding(0);
            this.UsdJpyRateNum.Location = new System.Drawing.Point(78, 30);
            this.UsdJpyRateNum.Name = "UsdJpyRateNum";
            this.UsdJpyRateNum.ShowFocusRectangle = true;
            this.UsdJpyRateNum.Size = new System.Drawing.Size(100, 20);
            this.UsdJpyRateNum.TabIndex = 12;
            this.UsdJpyRateNum.Tag = null;
            this.UsdJpyRateNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UsdJpyRateNum.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
            // 
            // CnhJpyRateNum
            // 
            this.CnhJpyRateNum.ErrorInfo.ErrorAction = C1.Win.C1Input.ErrorActionEnum.ResetValue;
            this.CnhJpyRateNum.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CnhJpyRateNum.ImagePadding = new System.Windows.Forms.Padding(0);
            this.CnhJpyRateNum.Location = new System.Drawing.Point(78, 53);
            this.CnhJpyRateNum.Name = "CnhJpyRateNum";
            this.CnhJpyRateNum.ShowFocusRectangle = true;
            this.CnhJpyRateNum.Size = new System.Drawing.Size(100, 20);
            this.CnhJpyRateNum.TabIndex = 13;
            this.CnhJpyRateNum.Tag = null;
            this.CnhJpyRateNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CnhJpyRateNum.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
            // 
            // PriceCompareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 703);
            this.Controls.Add(this.LeftSplitter);
            this.Controls.Add(this.RightPanel);
            this.Controls.Add(this.LeftPanel);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.TopPanel);
            this.Name = "PriceCompareForm";
            this.Text = "PriceCompareForm";
            this.Load += new System.EventHandler(this.PriceCompareForm_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UsdJpyRateNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CnhJpyRateNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.WebBrowser LeftBrowser;
        private System.Windows.Forms.Panel RightPanel;
        private System.Windows.Forms.WebBrowser RightBrowser;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Splitter splitter1;
        private NJFLib.Controls.CollapsibleSplitter LeftSplitter;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox KeywordText;
        private System.Windows.Forms.Panel RatePanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button GetCnhJpyRateButton;
        private System.Windows.Forms.Button GetUsdJpyRateButton;
        private C1.Win.C1Input.C1NumericEdit UsdJpyRateNum;
        private C1.Win.C1Input.C1NumericEdit CnhJpyRateNum;
    }
}