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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileExplorerForm));
            this.MainExplorer = new Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.RightMainPanel = new System.Windows.Forms.Panel();
            this.SubExplorer = new Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.RightTopPanel = new System.Windows.Forms.Panel();
            this.SearchSubButton = new System.Windows.Forms.Button();
            this.RefreshSubButton = new System.Windows.Forms.Button();
            this.OpenExplorerSubButton = new System.Windows.Forms.Button();
            this.SubExplorerCombo = new C1.Win.C1Input.C1ComboBox();
            this.UpwardSubExplorerButton = new System.Windows.Forms.Button();
            this.ForwardSubExplorerButton = new System.Windows.Forms.Button();
            this.BackwardSubExplorerButton = new System.Windows.Forms.Button();
            this.MainTopPanel = new System.Windows.Forms.Panel();
            this.SearchButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.MainExplorerCombo = new C1.Win.C1Input.C1ComboBox();
            this.OpenExplorerButton = new System.Windows.Forms.Button();
            this.UpwardMainExplorerButton = new System.Windows.Forms.Button();
            this.ForwardMainExplorerButton = new System.Windows.Forms.Button();
            this.BackwardMainExplorerButton = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.collapsibleSplitter3 = new NJFLib.Controls.CollapsibleSplitter();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.MainPanel.SuspendLayout();
            this.RightPanel.SuspendLayout();
            this.RightMainPanel.SuspendLayout();
            this.RightTopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SubExplorerCombo)).BeginInit();
            this.MainTopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainExplorerCombo)).BeginInit();
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
            this.MainExplorer.TabStop = false;
            this.MainExplorer.NavigationComplete += new System.EventHandler<Microsoft.WindowsAPICodePack.Controls.NavigationCompleteEventArgs>(this.MainExplorer_NavigationComplete);
            this.MainExplorer.LocationChanged += new System.EventHandler(this.MainExplorer_LocationChanged);
            this.MainExplorer.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.MainExplorer_HelpRequested);
            this.MainExplorer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainExplorer_KeyDown);
            this.MainExplorer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainExplorer_MouseDown);
            this.MainExplorer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainExplorer_MouseUp);
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
            this.SubExplorer.TabStop = false;
            this.SubExplorer.NavigationComplete += new System.EventHandler<Microsoft.WindowsAPICodePack.Controls.NavigationCompleteEventArgs>(this.SubExplorer_NavigationComplete);
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
            this.RightTopPanel.Controls.Add(this.SearchSubButton);
            this.RightTopPanel.Controls.Add(this.RefreshSubButton);
            this.RightTopPanel.Controls.Add(this.OpenExplorerSubButton);
            this.RightTopPanel.Controls.Add(this.SubExplorerCombo);
            this.RightTopPanel.Controls.Add(this.UpwardSubExplorerButton);
            this.RightTopPanel.Controls.Add(this.ForwardSubExplorerButton);
            this.RightTopPanel.Controls.Add(this.BackwardSubExplorerButton);
            this.RightTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.RightTopPanel.Location = new System.Drawing.Point(0, 0);
            this.RightTopPanel.Name = "RightTopPanel";
            this.RightTopPanel.Size = new System.Drawing.Size(436, 30);
            this.RightTopPanel.TabIndex = 5;
            // 
            // SearchSubButton
            // 
            this.SearchSubButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchSubButton.Image = global::PokudaSearch.Properties.Resources.Search16;
            this.SearchSubButton.Location = new System.Drawing.Point(412, 2);
            this.SearchSubButton.Name = "SearchSubButton";
            this.SearchSubButton.Size = new System.Drawing.Size(23, 27);
            this.SearchSubButton.TabIndex = 12;
            this.toolTip1.SetToolTip(this.SearchSubButton, "現在のフォルダ以下を全文検索(Ctrl+F)");
            this.SearchSubButton.UseVisualStyleBackColor = true;
            this.SearchSubButton.Click += new System.EventHandler(this.SearchSubButton_Click);
            // 
            // RefreshSubButton
            // 
            this.RefreshSubButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshSubButton.Image = global::PokudaSearch.Properties.Resources.Refresh16;
            this.RefreshSubButton.Location = new System.Drawing.Point(390, 2);
            this.RefreshSubButton.Name = "RefreshSubButton";
            this.RefreshSubButton.Size = new System.Drawing.Size(23, 27);
            this.RefreshSubButton.TabIndex = 11;
            this.toolTip1.SetToolTip(this.RefreshSubButton, "最新情報に更新(F5)");
            this.RefreshSubButton.UseVisualStyleBackColor = true;
            this.RefreshSubButton.Click += new System.EventHandler(this.RefreshSubButton_Click);
            // 
            // OpenExplorerSubButton
            // 
            this.OpenExplorerSubButton.Image = global::PokudaSearch.Properties.Resources.explorer16;
            this.OpenExplorerSubButton.Location = new System.Drawing.Point(67, 1);
            this.OpenExplorerSubButton.Name = "OpenExplorerSubButton";
            this.OpenExplorerSubButton.Size = new System.Drawing.Size(23, 27);
            this.OpenExplorerSubButton.TabIndex = 10;
            this.toolTip1.SetToolTip(this.OpenExplorerSubButton, "Explorerで開く(Ctrl+E)");
            this.OpenExplorerSubButton.UseVisualStyleBackColor = true;
            this.OpenExplorerSubButton.Click += new System.EventHandler(this.OpenExplorerSubButton_Click);
            // 
            // SubExplorerCombo
            // 
            this.SubExplorerCombo.AllowSpinLoop = false;
            this.SubExplorerCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SubExplorerCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SubExplorerCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SubExplorerCombo.AutoSuggestMode = C1.Win.C1Input.AutoSuggestMode.Contains;
            this.SubExplorerCombo.GapHeight = 0;
            this.SubExplorerCombo.ImagePadding = new System.Windows.Forms.Padding(0);
            this.SubExplorerCombo.InitialSelection = C1.Win.C1Input.InitialSelectionEnum.CaretAtEnd;
            this.SubExplorerCombo.ItemsDisplayMember = "";
            this.SubExplorerCombo.ItemsValueMember = "";
            this.SubExplorerCombo.Location = new System.Drawing.Point(93, 6);
            this.SubExplorerCombo.Name = "SubExplorerCombo";
            this.SubExplorerCombo.Size = new System.Drawing.Size(294, 17);
            this.SubExplorerCombo.TabIndex = 9;
            this.SubExplorerCombo.Tag = null;
            this.SubExplorerCombo.SelectedItemChanged += new System.EventHandler(this.SubExplorerCombo_SelectedItemChanged);
            this.SubExplorerCombo.DoubleClick += new System.EventHandler(this.SubExplorerCombo_DoubleClick);
            this.SubExplorerCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SubExplorerCombo_KeyDown);
            // 
            // UpwardSubExplorerButton
            // 
            this.UpwardSubExplorerButton.Image = global::PokudaSearch.Properties.Resources.upward_nav;
            this.UpwardSubExplorerButton.Location = new System.Drawing.Point(45, 1);
            this.UpwardSubExplorerButton.Name = "UpwardSubExplorerButton";
            this.UpwardSubExplorerButton.Size = new System.Drawing.Size(23, 27);
            this.UpwardSubExplorerButton.TabIndex = 7;
            this.toolTip1.SetToolTip(this.UpwardSubExplorerButton, "1つ上へ(Alt+Up)");
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
            this.toolTip1.SetToolTip(this.ForwardSubExplorerButton, "進む(Alt+Right)");
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
            this.toolTip1.SetToolTip(this.BackwardSubExplorerButton, "戻る(Alt+Left)");
            this.BackwardSubExplorerButton.UseVisualStyleBackColor = true;
            this.BackwardSubExplorerButton.Click += new System.EventHandler(this.BackwardSubExplorerButton_Click);
            // 
            // MainTopPanel
            // 
            this.MainTopPanel.Controls.Add(this.SearchButton);
            this.MainTopPanel.Controls.Add(this.RefreshButton);
            this.MainTopPanel.Controls.Add(this.MainExplorerCombo);
            this.MainTopPanel.Controls.Add(this.OpenExplorerButton);
            this.MainTopPanel.Controls.Add(this.UpwardMainExplorerButton);
            this.MainTopPanel.Controls.Add(this.ForwardMainExplorerButton);
            this.MainTopPanel.Controls.Add(this.BackwardMainExplorerButton);
            this.MainTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainTopPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTopPanel.Name = "MainTopPanel";
            this.MainTopPanel.Size = new System.Drawing.Size(579, 30);
            this.MainTopPanel.TabIndex = 119;
            // 
            // SearchButton
            // 
            this.SearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButton.Image = global::PokudaSearch.Properties.Resources.Search16;
            this.SearchButton.Location = new System.Drawing.Point(554, 2);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(23, 27);
            this.SearchButton.TabIndex = 10;
            this.toolTip1.SetToolTip(this.SearchButton, "現在のフォルダ以下を全文検索(Ctrl+F)");
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshButton.Image = global::PokudaSearch.Properties.Resources.Refresh16;
            this.RefreshButton.Location = new System.Drawing.Point(532, 2);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(23, 27);
            this.RefreshButton.TabIndex = 9;
            this.toolTip1.SetToolTip(this.RefreshButton, "最新情報に更新(F5)");
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // MainExplorerCombo
            // 
            this.MainExplorerCombo.AllowSpinLoop = false;
            this.MainExplorerCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainExplorerCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.MainExplorerCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.MainExplorerCombo.AutoSuggestMode = C1.Win.C1Input.AutoSuggestMode.Contains;
            this.MainExplorerCombo.GapHeight = 0;
            this.MainExplorerCombo.ImagePadding = new System.Windows.Forms.Padding(0);
            this.MainExplorerCombo.InitialSelection = C1.Win.C1Input.InitialSelectionEnum.CaretAtEnd;
            this.MainExplorerCombo.ItemsDisplayMember = "";
            this.MainExplorerCombo.ItemsValueMember = "";
            this.MainExplorerCombo.Location = new System.Drawing.Point(93, 7);
            this.MainExplorerCombo.Name = "MainExplorerCombo";
            this.MainExplorerCombo.Size = new System.Drawing.Size(436, 17);
            this.MainExplorerCombo.TabIndex = 8;
            this.MainExplorerCombo.Tag = null;
            this.MainExplorerCombo.SelectedItemChanged += new System.EventHandler(this.MainExplorerCombo_SelectedItemChanged);
            this.MainExplorerCombo.DoubleClick += new System.EventHandler(this.MainExplorerCombo_DoubleClick);
            this.MainExplorerCombo.Enter += new System.EventHandler(this.MainExplorerCombo_Enter);
            this.MainExplorerCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainExplorerCombo_KeyDown);
            // 
            // OpenExplorerButton
            // 
            this.OpenExplorerButton.Image = global::PokudaSearch.Properties.Resources.explorer16;
            this.OpenExplorerButton.Location = new System.Drawing.Point(68, 2);
            this.OpenExplorerButton.Name = "OpenExplorerButton";
            this.OpenExplorerButton.Size = new System.Drawing.Size(23, 27);
            this.OpenExplorerButton.TabIndex = 7;
            this.toolTip1.SetToolTip(this.OpenExplorerButton, "Explorerで開く(Ctrl+E)");
            this.OpenExplorerButton.UseVisualStyleBackColor = true;
            this.OpenExplorerButton.Click += new System.EventHandler(this.OpenExplorerButton_Click);
            // 
            // UpwardMainExplorerButton
            // 
            this.UpwardMainExplorerButton.Image = global::PokudaSearch.Properties.Resources.upward_nav;
            this.UpwardMainExplorerButton.Location = new System.Drawing.Point(46, 2);
            this.UpwardMainExplorerButton.Name = "UpwardMainExplorerButton";
            this.UpwardMainExplorerButton.Size = new System.Drawing.Size(23, 27);
            this.UpwardMainExplorerButton.TabIndex = 6;
            this.toolTip1.SetToolTip(this.UpwardMainExplorerButton, "1つ上へ(Alt+Up)");
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
            this.toolTip1.SetToolTip(this.ForwardMainExplorerButton, "進む(Alt+Right)");
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
            this.toolTip1.SetToolTip(this.BackwardMainExplorerButton, "戻る(Alt+Left)");
            this.BackwardMainExplorerButton.UseVisualStyleBackColor = true;
            this.BackwardMainExplorerButton.Click += new System.EventHandler(this.BackwardMainExplorerButton_Click);
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
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FileExplorerForm_FormClosed);
            this.Load += new System.EventHandler(this.FileExplorerForm_Load);
            this.Shown += new System.EventHandler(this.FileExplorerForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FileExplorerForm_KeyDown);
            this.MainPanel.ResumeLayout(false);
            this.RightPanel.ResumeLayout(false);
            this.RightMainPanel.ResumeLayout(false);
            this.RightTopPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SubExplorerCombo)).EndInit();
            this.MainTopPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainExplorerCombo)).EndInit();
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
        private System.Windows.Forms.Button BackwardSubExplorerButton;
        private System.Windows.Forms.Button ForwardSubExplorerButton;
        private System.Windows.Forms.Button ForwardMainExplorerButton;
        private System.Windows.Forms.Button BackwardMainExplorerButton;
        private System.Windows.Forms.Button UpwardMainExplorerButton;
        private System.Windows.Forms.Button UpwardSubExplorerButton;
        private System.Windows.Forms.Button OpenExplorerButton;
        private C1.Win.C1Input.C1ComboBox MainExplorerCombo;
        private C1.Win.C1Input.C1ComboBox SubExplorerCombo;
        private System.Windows.Forms.Button OpenExplorerSubButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button SearchSubButton;
        private System.Windows.Forms.Button RefreshSubButton;
    }
}