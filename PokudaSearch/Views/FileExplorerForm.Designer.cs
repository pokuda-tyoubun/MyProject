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
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.BackwardButton = new System.Windows.Forms.ToolStripButton();
            this.ForwardButton = new System.Windows.Forms.ToolStripButton();
            this.MainPathCombo = new System.Windows.Forms.ToolStripComboBox();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.RightMainPanel = new System.Windows.Forms.Panel();
            this.SubExplorer = new Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser();
            this.RightTopPanel = new System.Windows.Forms.Panel();
            this.SubToolStrip = new System.Windows.Forms.ToolStrip();
            this.BackwardSubButton = new System.Windows.Forms.ToolStripButton();
            this.ForwardSubButton = new System.Windows.Forms.ToolStripButton();
            this.SubPathCombo = new System.Windows.Forms.ToolStripComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.OnDemandSearchButton = new System.Windows.Forms.Button();
            this.collapsibleSplitter3 = new NJFLib.Controls.CollapsibleSplitter();
            this.MainTopPanel = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.CreateIndexButton = new System.Windows.Forms.Button();
            this.MainPanel.SuspendLayout();
            this.MainToolStrip.SuspendLayout();
            this.RightPanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.RightMainPanel.SuspendLayout();
            this.RightTopPanel.SuspendLayout();
            this.SubToolStrip.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.MainTopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainExplorer
            // 
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
            // MainToolStrip
            // 
            this.MainToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BackwardButton,
            this.ForwardButton,
            this.MainPathCombo});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.Size = new System.Drawing.Size(579, 30);
            this.MainToolStrip.TabIndex = 2;
            this.MainToolStrip.Text = "toolStrip1";
            this.MainToolStrip.SizeChanged += new System.EventHandler(this.MainToolStrip_SizeChanged);
            // 
            // BackwardButton
            // 
            this.BackwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BackwardButton.Image = ((System.Drawing.Image)(resources.GetObject("BackwardButton.Image")));
            this.BackwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BackwardButton.Name = "BackwardButton";
            this.BackwardButton.Size = new System.Drawing.Size(23, 27);
            this.BackwardButton.Text = "toolStripButton1";
            this.BackwardButton.Click += new System.EventHandler(this.BackwardButton_Click);
            // 
            // ForwardButton
            // 
            this.ForwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ForwardButton.Image = ((System.Drawing.Image)(resources.GetObject("ForwardButton.Image")));
            this.ForwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ForwardButton.Name = "ForwardButton";
            this.ForwardButton.Size = new System.Drawing.Size(23, 27);
            this.ForwardButton.Text = "toolStripButton1";
            this.ForwardButton.Click += new System.EventHandler(this.ForwardButton_Click);
            // 
            // MainPathCombo
            // 
            this.MainPathCombo.AutoSize = false;
            this.MainPathCombo.Name = "MainPathCombo";
            this.MainPathCombo.Size = new System.Drawing.Size(300, 23);
            this.MainPathCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainPathCombo_KeyDown);
            // 
            // RightPanel
            // 
            this.RightPanel.Controls.Add(this.tabControl1);
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightPanel.Location = new System.Drawing.Point(587, 0);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(436, 612);
            this.RightPanel.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(436, 612);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitter2);
            this.tabPage1.Controls.Add(this.RightMainPanel);
            this.tabPage1.Controls.Add(this.RightTopPanel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(428, 586);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SubExplorer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(3, 33);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(422, 3);
            this.splitter2.TabIndex = 122;
            this.splitter2.TabStop = false;
            // 
            // RightMainPanel
            // 
            this.RightMainPanel.Controls.Add(this.SubExplorer);
            this.RightMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightMainPanel.Location = new System.Drawing.Point(3, 33);
            this.RightMainPanel.Name = "RightMainPanel";
            this.RightMainPanel.Size = new System.Drawing.Size(422, 550);
            this.RightMainPanel.TabIndex = 122;
            // 
            // SubExplorer
            // 
            this.SubExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SubExplorer.Location = new System.Drawing.Point(0, 0);
            this.SubExplorer.Name = "SubExplorer";
            this.SubExplorer.PropertyBagName = "Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser";
            this.SubExplorer.Size = new System.Drawing.Size(422, 550);
            this.SubExplorer.TabIndex = 0;
            this.SubExplorer.NavigationComplete += new System.EventHandler<Microsoft.WindowsAPICodePack.Controls.NavigationCompleteEventArgs>(this.SubExplorer_NavigationComplete);
            this.SubExplorer.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.SubExplorer_HelpRequested);
            // 
            // RightTopPanel
            // 
            this.RightTopPanel.Controls.Add(this.SubToolStrip);
            this.RightTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.RightTopPanel.Location = new System.Drawing.Point(3, 3);
            this.RightTopPanel.Name = "RightTopPanel";
            this.RightTopPanel.Size = new System.Drawing.Size(422, 30);
            this.RightTopPanel.TabIndex = 4;
            // 
            // SubToolStrip
            // 
            this.SubToolStrip.AutoSize = false;
            this.SubToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SubToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BackwardSubButton,
            this.ForwardSubButton,
            this.SubPathCombo});
            this.SubToolStrip.Location = new System.Drawing.Point(0, 0);
            this.SubToolStrip.Name = "SubToolStrip";
            this.SubToolStrip.Size = new System.Drawing.Size(422, 30);
            this.SubToolStrip.TabIndex = 3;
            this.SubToolStrip.Text = "toolStrip2";
            // 
            // BackwardSubButton
            // 
            this.BackwardSubButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BackwardSubButton.Image = ((System.Drawing.Image)(resources.GetObject("BackwardSubButton.Image")));
            this.BackwardSubButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BackwardSubButton.Name = "BackwardSubButton";
            this.BackwardSubButton.Size = new System.Drawing.Size(23, 27);
            this.BackwardSubButton.Text = "toolStripButton1";
            this.BackwardSubButton.Click += new System.EventHandler(this.BackwardSubButton_Click);
            // 
            // ForwardSubButton
            // 
            this.ForwardSubButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ForwardSubButton.Image = ((System.Drawing.Image)(resources.GetObject("ForwardSubButton.Image")));
            this.ForwardSubButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ForwardSubButton.Name = "ForwardSubButton";
            this.ForwardSubButton.Size = new System.Drawing.Size(23, 27);
            this.ForwardSubButton.Text = "toolStripButton1";
            this.ForwardSubButton.Click += new System.EventHandler(this.ForwardSubButton_Click);
            // 
            // SubPathCombo
            // 
            this.SubPathCombo.Name = "SubPathCombo";
            this.SubPathCombo.Size = new System.Drawing.Size(300, 30);
            this.SubPathCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SubPathCombo_KeyDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.CreateIndexButton);
            this.tabPage2.Controls.Add(this.OnDemandSearchButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(428, 586);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "オンデマンド検索";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // OnDemandSearchButton
            // 
            this.OnDemandSearchButton.Location = new System.Drawing.Point(17, 11);
            this.OnDemandSearchButton.Name = "OnDemandSearchButton";
            this.OnDemandSearchButton.Size = new System.Drawing.Size(109, 25);
            this.OnDemandSearchButton.TabIndex = 0;
            this.OnDemandSearchButton.Text = "オンデマンド検索";
            this.OnDemandSearchButton.UseVisualStyleBackColor = true;
            this.OnDemandSearchButton.Click += new System.EventHandler(this.OnDemandSearchButton_Click);
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
            this.MainTopPanel.Controls.Add(this.MainToolStrip);
            this.MainTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainTopPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTopPanel.Name = "MainTopPanel";
            this.MainTopPanel.Size = new System.Drawing.Size(579, 30);
            this.MainTopPanel.TabIndex = 119;
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
            // CreateIndexButton
            // 
            this.CreateIndexButton.Location = new System.Drawing.Point(17, 55);
            this.CreateIndexButton.Name = "CreateIndexButton";
            this.CreateIndexButton.Size = new System.Drawing.Size(109, 25);
            this.CreateIndexButton.TabIndex = 1;
            this.CreateIndexButton.Text = "インデックス作成";
            this.CreateIndexButton.UseVisualStyleBackColor = true;
            this.CreateIndexButton.Click += new System.EventHandler(this.CreateIndexButton_Click);
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
            this.Name = "FileExplorerForm";
            this.Text = "エクスプローラ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FileExplorerForm_FormClosed);
            this.Shown += new System.EventHandler(this.FileExplorerForm_Shown);
            this.MainPanel.ResumeLayout(false);
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.RightPanel.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.RightMainPanel.ResumeLayout(false);
            this.RightTopPanel.ResumeLayout(false);
            this.SubToolStrip.ResumeLayout(false);
            this.SubToolStrip.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.MainTopPanel.ResumeLayout(false);
            this.MainTopPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser MainExplorer;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel RightPanel;
        private NJFLib.Controls.CollapsibleSplitter collapsibleSplitter3;
        private Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser SubExplorer;
        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripButton BackwardButton;
        private System.Windows.Forms.ToolStripComboBox MainPathCombo;
        private System.Windows.Forms.ToolStrip SubToolStrip;
        private System.Windows.Forms.ToolStripButton BackwardSubButton;
        private System.Windows.Forms.ToolStripComboBox SubPathCombo;
        private System.Windows.Forms.ToolStripButton ForwardButton;
        private System.Windows.Forms.ToolStripButton ForwardSubButton;
        private System.Windows.Forms.Panel MainTopPanel;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel RightMainPanel;
        private System.Windows.Forms.Panel RightTopPanel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button OnDemandSearchButton;
        private System.Windows.Forms.Button CreateIndexButton;
    }
}