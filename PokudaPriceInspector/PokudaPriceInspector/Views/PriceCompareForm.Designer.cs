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
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.LeftSplitter = new NJFLib.Controls.CollapsibleSplitter();
            this.LeftPanel.SuspendLayout();
            this.RightPanel.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.LeftBrowser);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 108);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(507, 595);
            this.LeftPanel.TabIndex = 0;
            // 
            // LeftBrowser
            // 
            this.LeftBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftBrowser.Location = new System.Drawing.Point(0, 0);
            this.LeftBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.LeftBrowser.Name = "LeftBrowser";
            this.LeftBrowser.Size = new System.Drawing.Size(507, 595);
            this.LeftBrowser.TabIndex = 0;
            // 
            // RightPanel
            // 
            this.RightPanel.Controls.Add(this.RightBrowser);
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightPanel.Location = new System.Drawing.Point(507, 108);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(445, 595);
            this.RightPanel.TabIndex = 1;
            // 
            // RightBrowser
            // 
            this.RightBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightBrowser.Location = new System.Drawing.Point(0, 0);
            this.RightBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.RightBrowser.Name = "RightBrowser";
            this.RightBrowser.Size = new System.Drawing.Size(445, 595);
            this.RightBrowser.TabIndex = 0;
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(723, 37);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(137, 30);
            this.RunButton.TabIndex = 2;
            this.RunButton.Text = "Run!!";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.RunButton);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(952, 100);
            this.TopPanel.TabIndex = 3;
            // 
            // splitter1
            // 
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.splitter1.Location = new System.Drawing.Point(0, 100);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(952, 8);
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
            this.LeftSplitter.Location = new System.Drawing.Point(507, 108);
            this.LeftSplitter.Name = "LeftSplitter";
            this.LeftSplitter.Size = new System.Drawing.Size(8, 595);
            this.LeftSplitter.TabIndex = 238;
            this.LeftSplitter.TabStop = false;
            this.LeftSplitter.UseAnimations = false;
            this.LeftSplitter.VisualStyle = NJFLib.Controls.VisualStyles.DoubleDots;
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
            this.LeftPanel.ResumeLayout(false);
            this.RightPanel.ResumeLayout(false);
            this.TopPanel.ResumeLayout(false);
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
    }
}