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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BackwardButton = new System.Windows.Forms.ToolStripButton();
            this.MainPathCombo = new System.Windows.Forms.ToolStripComboBox();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.SubPathCombo = new System.Windows.Forms.ToolStripComboBox();
            this.SubExplorer = new Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser();
            this.collapsibleSplitter3 = new NJFLib.Controls.CollapsibleSplitter();
            this.MainPanel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.RightPanel.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainExplorer
            // 
            this.MainExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainExplorer.Location = new System.Drawing.Point(1, 31);
            this.MainExplorer.Name = "MainExplorer";
            this.MainExplorer.PropertyBagName = "Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser";
            this.MainExplorer.Size = new System.Drawing.Size(547, 615);
            this.MainExplorer.TabIndex = 1;
            this.MainExplorer.NavigationComplete += new System.EventHandler<Microsoft.WindowsAPICodePack.Controls.NavigationCompleteEventArgs>(this.MainExplorer_NavigationComplete);
            this.MainExplorer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainExplorer_KeyDown);
            this.MainExplorer.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainExplorer_PreviewKeyDown);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.toolStrip1);
            this.MainPanel.Controls.Add(this.MainExplorer);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1193, 647);
            this.MainPanel.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BackwardButton,
            this.MainPathCombo});
            this.toolStrip1.Location = new System.Drawing.Point(4, 4);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(537, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BackwardButton
            // 
            this.BackwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BackwardButton.Image = ((System.Drawing.Image)(resources.GetObject("BackwardButton.Image")));
            this.BackwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BackwardButton.Name = "BackwardButton";
            this.BackwardButton.Size = new System.Drawing.Size(23, 22);
            this.BackwardButton.Text = "toolStripButton1";
            this.BackwardButton.Click += new System.EventHandler(this.BackwardButton_Click);
            // 
            // MainPathCombo
            // 
            this.MainPathCombo.AutoSize = false;
            this.MainPathCombo.Name = "MainPathCombo";
            this.MainPathCombo.Size = new System.Drawing.Size(500, 23);
            this.MainPathCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainPathCombo_KeyDown);
            // 
            // RightPanel
            // 
            this.RightPanel.Controls.Add(this.toolStrip2);
            this.RightPanel.Controls.Add(this.SubExplorer);
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightPanel.Location = new System.Drawing.Point(555, 0);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(638, 647);
            this.RightPanel.TabIndex = 3;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.SubPathCombo});
            this.toolStrip2.Location = new System.Drawing.Point(2, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(598, 27);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 24);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // SubPathCombo
            // 
            this.SubPathCombo.AutoSize = false;
            this.SubPathCombo.Name = "SubPathCombo";
            this.SubPathCombo.Size = new System.Drawing.Size(500, 23);
            this.SubPathCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SubPathCombo_KeyDown);
            // 
            // SubExplorer
            // 
            this.SubExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SubExplorer.Location = new System.Drawing.Point(0, 31);
            this.SubExplorer.Name = "SubExplorer";
            this.SubExplorer.PropertyBagName = "Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser";
            this.SubExplorer.Size = new System.Drawing.Size(638, 616);
            this.SubExplorer.TabIndex = 0;
            this.SubExplorer.NavigationComplete += new System.EventHandler<Microsoft.WindowsAPICodePack.Controls.NavigationCompleteEventArgs>(this.SubExplorer_NavigationComplete);
            // 
            // collapsibleSplitter3
            // 
            this.collapsibleSplitter3.AnimationDelay = 20;
            this.collapsibleSplitter3.AnimationStep = 20;
            this.collapsibleSplitter3.BorderStyle3D = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.collapsibleSplitter3.ControlToHide = this.RightPanel;
            this.collapsibleSplitter3.Dock = System.Windows.Forms.DockStyle.Right;
            this.collapsibleSplitter3.ExpandParentForm = false;
            this.collapsibleSplitter3.Location = new System.Drawing.Point(547, 0);
            this.collapsibleSplitter3.Name = "collapsibleSplitter3";
            this.collapsibleSplitter3.Size = new System.Drawing.Size(8, 647);
            this.collapsibleSplitter3.TabIndex = 118;
            this.collapsibleSplitter3.TabStop = false;
            this.collapsibleSplitter3.UseAnimations = false;
            this.collapsibleSplitter3.VisualStyle = NJFLib.Controls.VisualStyles.DoubleDots;
            // 
            // FileExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 647);
            this.Controls.Add(this.collapsibleSplitter3);
            this.Controls.Add(this.RightPanel);
            this.Controls.Add(this.MainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FileExplorerForm";
            this.Text = "エクスプローラ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FileExplorerForm_FormClosed);
            this.Shown += new System.EventHandler(this.FileExplorerForm_Shown);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.RightPanel.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser MainExplorer;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel RightPanel;
        private NJFLib.Controls.CollapsibleSplitter collapsibleSplitter3;
        private Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser SubExplorer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton BackwardButton;
        private System.Windows.Forms.ToolStripComboBox MainPathCombo;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripComboBox SubPathCombo;
    }
}