namespace Konvolucio.MCAN120803.GUI.AppModules.Log.View
{
    partial class LogView
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
            this.splitContainerLogDesciription = new Konvolucio.WinForms.Framework.KnvSplitContainer();
            this.logDescriptionView1 = new Konvolucio.MCAN120803.GUI.AppModules.Log.View.LogDescriptionView();
            this.logGridView1 = new Konvolucio.MCAN120803.GUI.AppModules.Log.View.LogGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelName = new System.Windows.Forms.ToolStripLabel();
            this.buttonToolStripAutoColumnWidth = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLogDesciription)).BeginInit();
            this.splitContainerLogDesciription.Panel1.SuspendLayout();
            this.splitContainerLogDesciription.Panel2.SuspendLayout();
            this.splitContainerLogDesciription.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerLogDesciription
            // 
            this.splitContainerLogDesciription.BackColor = System.Drawing.Color.Orange;
            this.splitContainerLogDesciription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLogDesciription.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLogDesciription.Name = "splitContainerLogDesciription";
            this.splitContainerLogDesciription.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLogDesciription.Panel1
            // 
            this.splitContainerLogDesciription.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainerLogDesciription.Panel1.Controls.Add(this.logDescriptionView1);
            // 
            // splitContainerLogDesciription.Panel2
            // 
            this.splitContainerLogDesciription.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainerLogDesciription.Panel2.Controls.Add(this.logGridView1);
            this.splitContainerLogDesciription.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainerLogDesciription.Size = new System.Drawing.Size(905, 267);
            this.splitContainerLogDesciription.SplitterDistance = 66;
            this.splitContainerLogDesciription.SplitterPrecent = 0.25D;
            this.splitContainerLogDesciription.TabIndex = 1;
            // 
            // logDescriptionView1
            // 
            this.logDescriptionView1.Content = "";
            this.logDescriptionView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logDescriptionView1.Location = new System.Drawing.Point(0, 0);
            this.logDescriptionView1.LogName = "*";
            this.logDescriptionView1.Name = "logDescriptionView1";
            this.logDescriptionView1.Size = new System.Drawing.Size(905, 66);
            this.logDescriptionView1.TabIndex = 0;
            // 
            // logGridView1
            // 
            this.logGridView1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.logGridView1.BackgroundText = "LOG VIEW";
            this.logGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logGridView1.Location = new System.Drawing.Point(0, 25);
            this.logGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.logGridView1.Name = "logGridView1";
            this.logGridView1.Size = new System.Drawing.Size(905, 172);
            this.logGridView1.TabIndex = 0;
            this.logGridView1.TimestampFormat = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelName,
            this.buttonToolStripAutoColumnWidth});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(905, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabelName
            // 
            this.toolStripLabelName.Name = "toolStripLabelName";
            this.toolStripLabelName.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabelName.Text = "LOG VIEW";
            // 
            // buttonToolStripAutoColumnWidth
            // 
            this.buttonToolStripAutoColumnWidth.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonToolStripAutoColumnWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonToolStripAutoColumnWidth.Image = global::Konvolucio.MCAN120803.GUI.Properties.Resources.SizeHorizontal32;
            this.buttonToolStripAutoColumnWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonToolStripAutoColumnWidth.Name = "buttonToolStripAutoColumnWidth";
            this.buttonToolStripAutoColumnWidth.Size = new System.Drawing.Size(23, 22);
            this.buttonToolStripAutoColumnWidth.Text = "Auto Size All";
            this.buttonToolStripAutoColumnWidth.ToolTipText = "Column Auto Size All";
            this.buttonToolStripAutoColumnWidth.Click += new System.EventHandler(this.buttonToolStripAutoColumnWidth_Click);
            // 
            // LogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitContainerLogDesciription);
            this.Name = "LogView";
            this.Size = new System.Drawing.Size(905, 267);
            this.Load += new System.EventHandler(this.LogView_Load);
            this.splitContainerLogDesciription.Panel1.ResumeLayout(false);
            this.splitContainerLogDesciription.Panel2.ResumeLayout(false);
            this.splitContainerLogDesciription.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLogDesciription)).EndInit();
            this.splitContainerLogDesciription.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private LogGridView logGridView1;
        private WinForms.Framework.KnvSplitContainer splitContainerLogDesciription;
        private LogDescriptionView logDescriptionView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelName;
        private System.Windows.Forms.ToolStripButton buttonToolStripAutoColumnWidth;
    }
}
