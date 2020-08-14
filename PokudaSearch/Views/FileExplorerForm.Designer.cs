namespace PokudaSearch.Views {
    partial class FileExplorerForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileExplorerForm));
            this.MainExplorer = new Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.RightMainPanel = new System.Windows.Forms.Panel();
            this.SubExplorer = new Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.RightTopPanel = new System.Windows.Forms.Panel();
            this.UpwardSubExplorerButton = new System.Windows.Forms.Button();
            this.ForwardSubExplorerButton = new System.Windows.Forms.Button();
            this.BackwardSubExplorerButton = new System.Windows.Forms.Button();
            this.SubExplorerPathCombo = new System.Windows.Forms.ComboBox();
            this.collapsibleSplitter3 = new NJFLib.Controls.CollapsibleSplitter();
            this.MainTopPanel = new System.Windows.Forms.Panel();
            this.UpwardMainExplorerButton = new System.Windows.Forms.Button();
            this.ForwardMainExplorerButton = new System.Windows.Forms.Button();
            this.BackwardMainExplorerButton = new System.Windows.Forms.Button();
            this.MainExplorerPathCombo = new System.Windows.Forms.ComboBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.MainPanel.SuspendLayout();
            this.RightPanel.SuspendLayout();
            this.RightMainPanel.SuspendLayout();
            this.RightTopPanel.SuspendLayout();
            this.MainTopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainExplorer
            // 
            this.MainExplorer.BackColor = System.Drawing.Color.Transparent;
            this.MainExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainExplorer.Location = new System.Drawing.Point(0, 0);
            this.MainExplorer.Name = "MainExplorer";
            this.MainExplorer.PropertyBagName = "Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser";
            this.MainExplorer.Size = new System.Drawing.Size(579, 579);
            this.MainExplorer.TabIndex = 1;
            this.MainExplorer.NavigationComplete += new System.EventHandler<Microsoft.WindowsAPICodePack.Controls.NavigationCompleteEventArgs>(this.MainExplorer_NavigationComplete);
            this.MainExplorer.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.MainExplorer_HelpRequested);
            this.MainExplorer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainExplorer_KeyDown);
            this.MainExplorer.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainExplorer_PreviewKeyDown);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.MainExplorer);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 33);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(579, 579);
            this.MainPanel.TabIndex = 2;
            // 
            // RightPanel
            // 
            this.RightPanel.Controls.Add(this.RightMainPanel);
            this.RightPanel.Controls.Add(this.splitter2);
            this.RightPanel.Controls.Add(this.RightTopPanel);
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightPanel.Location = new System.Drawing.Point(587, 0);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(436, 612);
            this.RightPanel.TabIndex = 3;
            // 
            // RightMainPanel
            // 
            this.RightMainPanel.Controls.Add(this.SubExplorer);
            this.RightMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightMainPanel.Location = new System.Drawing.Point(0, 33);
            this.RightMainPanel.Name = "RightMainPanel";
            this.RightMainPanel.Size = new System.Drawing.Size(436, 579);
            this.RightMainPanel.TabIndex = 124;
            // 
            // SubExplorer
            // 
            this.SubExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SubExplorer.Location = new System.Drawing.Point(0, 0);
            this.SubExplorer.Name = "SubExplorer";
            this.SubExplorer.PropertyBagName = "Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser";
            this.SubExplorer.Size = new System.Drawing.Size(436, 579);
            this.SubExplorer.TabIndex = 0;
            this.SubExplorer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SubExplorer_KeyDown);
            this.SubExplorer.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.SubExplorer_PreviewKeyDown);
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 30);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(436, 3);
            this.splitter2.TabIndex = 123;
            this.splitter2.TabStop = false;
            // 
            // RightTopPanel
            // 
            this.RightTopPanel.Controls.Add(this.UpwardSubExplorerButton);
            this.RightTopPanel.Controls.Add(this.ForwardSubExplorerButton);
            this.RightTopPanel.Controls.Add(this.BackwardSubExplorerButton);
            this.RightTopPanel.Controls.Add(this.SubExplorerPathCombo);
            this.RightTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.RightTopPanel.Location = new System.Drawing.Point(0, 0);
            this.RightTopPanel.Name = "RightTopPanel";
            this.RightTopPanel.Size = new System.Drawing.Size(436, 30);
            this.RightTopPanel.TabIndex = 5;
            this.RightTopPanel.SizeChanged += new System.EventHandler(this.RightTopPanel_SizeChanged);
            // 
            // UpwardSubExplorerButton
            // 
            this.UpwardSubExplorerButton.Image = global::PokudaSearch.Properties.Resources.upward_nav;
            this.UpwardSubExplorerButton.Location = new System.Drawing.Point(45, 1);
            this.UpwardSubExplorerButton.Name = "UpwardSubExplorerButton";
            this.UpwardSubExplorerButton.Size = new System.Drawing.Size(23, 27);
            this.UpwardSubExplorerButton.TabIndex = 7;
            this.UpwardSubExplorerButton.UseVisualStyleBackColor = true;
            this.UpwardSubExplorerButton.Click += new System.EventHandler(this.UpwardSubExplorerButton_Click);
            // 
            // ForwardSubExplorerButton
            // 
            this.ForwardSubExplorerButton.Image = global::PokudaSearch.Properties.Resources.forward_nav;
            this.ForwardSubExplorerButton.Location = new System.Drawing.Point(23, 1);
            this.ForwardSubExplorerButton.Name = "ForwardSubExplorerButton";
            this.ForwardSubExplorerButton.Size = new System.Drawing.Size(23, 27);
            this.ForwardSubExplorerButton.TabIndex = 2;
            this.ForwardSubExplorerButton.UseVisualStyleBackColor = true;
            this.ForwardSubExplorerButton.Click += new System.EventHandler(this.ForwardSubExplorerButton_Click);
            // 
            // BackwardSubExplorerButton
            // 
            this.BackwardSubExplorerButton.Image = global::PokudaSearch.Properties.Resources.backward_nav;
            this.BackwardSubExplorerButton.Location = new System.Drawing.Point(1, 1);
            this.BackwardSubExplorerButton.Name = "BackwardSubExplorerButton";
            this.BackwardSubExplorerButton.Size = new System.Drawing.Size(23, 27);
            this.BackwardSubExplorerButton.TabIndex = 1;
            this.BackwardSubExplorerButton.UseVisualStyleBackColor = true;
            this.BackwardSubExplorerButton.Click += new System.EventHandler(this.BackwardSubExplorerButton_Click);
            // 
            // SubExplorerPathCombo
            // 
            this.SubExplorerPathCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SubExplorerPathCombo.FormattingEnabled = true;
            this.SubExplorerPathCombo.Location = new System.Drawing.Point(70, 5);
            this.SubExplorerPathCombo.Name = "SubExplorerPathCombo";
            this.SubExplorerPathCombo.Size = new System.Drawing.Size(362, 20);
            this.SubExplorerPathCombo.TabIndex = 0;
            this.SubExplorerPathCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SubExplorerPathCombo_KeyDown);
            // 
            // collapsibleSplitter3
            // 
            this.collapsibleSplitter3.AnimationDelay = 20;
            this.collapsibleSplitter3.AnimationStep = 20;
            this.collapsibleSplitter3.BorderStyle3D = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.collapsibleSplitter3.ControlToHide = this.RightPanel;
            this.collapsibleSplitter3.Dock = System.Windows.Forms.DockStyle.Right;
            this.collapsibleSplitter3.ExpandParentForm = false;
            this.collapsibleSplitter3.Location = new System.Drawing.Point(579, 0);
            this.collapsibleSplitter3.Name = "collapsibleSplitter3";
            this.collapsibleSplitter3.Size = new System.Drawing.Size(8, 612);
            this.collapsibleSplitter3.TabIndex = 118;
            this.collapsibleSplitter3.TabStop = false;
            this.collapsibleSplitter3.UseAnimations = false;
            this.collapsibleSplitter3.VisualStyle = NJFLib.Controls.VisualStyles.DoubleDots;
            // 
            // MainTopPanel
            // 
            this.MainTopPanel.Controls.Add(this.UpwardMainExplorerButton);
            this.MainTopPanel.Controls.Add(this.ForwardMainExplorerButton);
            this.MainTopPanel.Controls.Add(this.BackwardMainExplorerButton);
            this.MainTopPanel.Controls.Add(this.MainExplorerPathCombo);
            this.MainTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainTopPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTopPanel.Name = "MainTopPanel";
            this.MainTopPanel.Size = new System.Drawing.Size(579, 30);
            this.MainTopPanel.TabIndex = 119;
            // 
            // UpwardMainExplorerButton
            // 
            this.UpwardMainExplorerButton.Image = global::PokudaSearch.Properties.Resources.upward_nav;
            this.UpwardMainExplorerButton.Location = new System.Drawing.Point(46, 2);
            this.UpwardMainExplorerButton.Name = "UpwardMainExplorerButton";
            this.UpwardMainExplorerButton.Size = new System.Drawing.Size(23, 27);
            this.UpwardMainExplorerButton.TabIndex = 6;
            this.UpwardMainExplorerButton.UseVisualStyleBackColor = true;
            this.UpwardMainExplorerButton.Click += new System.EventHandler(this.UpwardMainExplorerButton_Click);
            // 
            // ForwardMainExplorerButton
            // 
            this.ForwardMainExplorerButton.Image = global::PokudaSearch.Properties.Resources.forward_nav;
            this.ForwardMainExplorerButton.Location = new System.Drawing.Point(24, 2);
            this.ForwardMainExplorerButton.Name = "ForwardMainExplorerButton";
            this.ForwardMainExplorerButton.Size = new System.Drawing.Size(23, 27);
            this.ForwardMainExplorerButton.TabIndex = 5;
            this.ForwardMainExplorerButton.UseVisualStyleBackColor = true;
            this.ForwardMainExplorerButton.Click += new System.EventHandler(this.ForwardMainExplorerButton_Click);
            // 
            // BackwardMainExplorerButton
            // 
            this.BackwardMainExplorerButton.Image = global::PokudaSearch.Properties.Resources.backward_nav;
            this.BackwardMainExplorerButton.Location = new System.Drawing.Point(2, 2);
            this.BackwardMainExplorerButton.Name = "BackwardMainExplorerButton";
            this.BackwardMainExplorerButton.Size = new System.Drawing.Size(23, 27);
            this.BackwardMainExplorerButton.TabIndex = 4;
            this.BackwardMainExplorerButton.UseVisualStyleBackColor = true;
            this.BackwardMainExplorerButton.Click += new System.EventHandler(this.BackwardMainExplorerButton_Click);
            // 
            // MainExplorerPathCombo
            // 
            this.MainExplorerPathCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainExplorerPathCombo.FormattingEnabled = true;
            this.MainExplorerPathCombo.Location = new System.Drawing.Point(71, 6);
            this.MainExplorerPathCombo.Name = "MainExplorerPathCombo";
            this.MainExplorerPathCombo.Size = new System.Drawing.Size(504, 20);
            this.MainExplorerPathCombo.TabIndex = 3;
            this.MainExplorerPathCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainExplorerPathCombo_KeyDown);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 30);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(579, 3);
            this.splitter1.TabIndex = 120;
            this.splitter1.TabStop = false;
            // 
            // FileExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 612);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.MainTopPanel);
            this.Controls.Add(this.collapsibleSplitter3);
            this.Controls.Add(this.RightPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FileExplorerForm";
            this.Text = "エクスプローラー";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FileExplorerForm_FormClosed);
            this.Shown += new System.EventHandler(this.FileExplorerForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FileExplorerForm_KeyDown);
            this.MainPanel.ResumeLayout(false);
            this.RightPanel.ResumeLayout(false);
            this.RightMainPanel.ResumeLayout(false);
            this.RightTopPanel.ResumeLayout(false);
            this.MainTopPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser MainExplorer;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel RightPanel;
        private NJFLib.Controls.CollapsibleSplitter collapsibleSplitter3;
        private System.Windows.Forms.Panel MainTopPanel;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel RightMainPanel;
        private Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser SubExplorer;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel RightTopPanel;
        private System.Windows.Forms.ComboBox SubExplorerPathCombo;
        private System.Windows.Forms.Button BackwardSubExplorerButton;
        private System.Windows.Forms.Button ForwardSubExplorerButton;
        private System.Windows.Forms.Button ForwardMainExplorerButton;
        private System.Windows.Forms.Button BackwardMainExplorerButton;
        private System.Windows.Forms.ComboBox MainExplorerPathCombo;
        private System.Windows.Forms.Button UpwardMainExplorerButton;
        private System.Windows.Forms.Button UpwardSubExplorerButton;
    }
}