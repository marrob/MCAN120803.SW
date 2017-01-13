namespace Konvolucio.MCAN120803.GUI.AppModules.Main.View
{
    partial class MainView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.splitContainerMainView = new Konvolucio.WinForms.Framework.KnvSplitContainer();
            this.splitContainerMainTree = new Konvolucio.WinForms.Framework.KnvSplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView1 = new Konvolucio.WinForms.Framework.KnvTreeView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.knvSplitContainer1 = new Konvolucio.WinForms.Framework.KnvSplitContainer();
            this.appTraceView1 = new Konvolucio.MCAN120803.GUI.AppModules.AppDiag.View.AppDiagnosticsView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainView)).BeginInit();
            this.splitContainerMainView.Panel1.SuspendLayout();
            this.splitContainerMainView.Panel2.SuspendLayout();
            this.splitContainerMainView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainTree)).BeginInit();
            this.splitContainerMainTree.Panel1.SuspendLayout();
            this.splitContainerMainTree.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.knvSplitContainer1)).BeginInit();
            this.knvSplitContainer1.Panel1.SuspendLayout();
            this.knvSplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 1, 3);
            this.toolStrip1.Size = new System.Drawing.Size(905, 48);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // splitContainerMainView
            // 
            this.splitContainerMainView.BackColor = System.Drawing.Color.Orange;
            this.splitContainerMainView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMainView.Location = new System.Drawing.Point(0, 48);
            this.splitContainerMainView.Name = "splitContainerMainView";
            this.splitContainerMainView.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMainView.Panel1
            // 
            this.splitContainerMainView.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainerMainView.Panel1.Controls.Add(this.splitContainerMainTree);
            this.splitContainerMainView.Panel1MinSize = 150;
            // 
            // splitContainerMainView.Panel2
            // 
            this.splitContainerMainView.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainerMainView.Panel2.Controls.Add(this.knvSplitContainer1);
            this.splitContainerMainView.Size = new System.Drawing.Size(905, 389);
            this.splitContainerMainView.SplitterDistance = 178;
            this.splitContainerMainView.SplitterPrecent = 0.46D;
            this.splitContainerMainView.TabIndex = 3;
            this.splitContainerMainView.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainerMainView_SplitterMoved);
            // 
            // splitContainerMainTree
            // 
            this.splitContainerMainTree.BackColor = System.Drawing.Color.Orange;
            this.splitContainerMainTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMainTree.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMainTree.Name = "splitContainerMainTree";
            // 
            // splitContainerMainTree.Panel1
            // 
            this.splitContainerMainTree.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainerMainTree.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainerMainTree.Panel2
            // 
            this.splitContainerMainTree.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainerMainTree.Size = new System.Drawing.Size(905, 178);
            this.splitContainerMainTree.SplitterDistance = 226;
            this.splitContainerMainTree.SplitterPrecent = 0.25D;
            this.splitContainerMainTree.TabIndex = 9;
            this.splitContainerMainTree.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainerMainTree_SplitterMoved);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Controls.Add(this.toolStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(226, 178);
            this.panel1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageList = null;
            this.treeView1.Location = new System.Drawing.Point(0, 25);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedNode = null;
            this.treeView1.Size = new System.Drawing.Size(226, 153);
            this.treeView1.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(226, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // knvSplitContainer1
            // 
            this.knvSplitContainer1.BackColor = System.Drawing.Color.Orange;
            this.knvSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.knvSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.knvSplitContainer1.Name = "knvSplitContainer1";
            // 
            // knvSplitContainer1.Panel1
            // 
            this.knvSplitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.knvSplitContainer1.Panel1.Controls.Add(this.appTraceView1);
            // 
            // knvSplitContainer1.Panel2
            // 
            this.knvSplitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.knvSplitContainer1.Size = new System.Drawing.Size(905, 207);
            this.knvSplitContainer1.SplitterDistance = 751;
            this.knvSplitContainer1.SplitterPrecent = 0.83D;
            this.knvSplitContainer1.TabIndex = 1;
            // 
            // appTraceView1
            // 
            this.appTraceView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appTraceView1.Location = new System.Drawing.Point(0, 0);
            this.appTraceView1.Name = "appTraceView1";
            this.appTraceView1.Size = new System.Drawing.Size(751, 207);
            this.appTraceView1.TabIndex = 0;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMainView);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainView";
            this.Size = new System.Drawing.Size(905, 437);
            this.Load += new System.EventHandler(this.MainView_Load);
            this.splitContainerMainView.Panel1.ResumeLayout(false);
            this.splitContainerMainView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainView)).EndInit();
            this.splitContainerMainView.ResumeLayout(false);
            this.splitContainerMainTree.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainTree)).EndInit();
            this.splitContainerMainTree.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.knvSplitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.knvSplitContainer1)).EndInit();
            this.knvSplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WinForms.Framework.KnvSplitContainer splitContainerMainView;
        private AppModules.AppDiag.View.AppDiagnosticsView appTraceView1;
        private WinForms.Framework.KnvSplitContainer splitContainerMainTree;
        private System.Windows.Forms.Panel panel1;
        private WinForms.Framework.KnvTreeView treeView1;
        private WinForms.Framework.KnvSplitContainer knvSplitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStrip toolStrip2;

    }
}
