namespace Konvolucio.MCAN120803.GUI.AppModules.Main.View
{
    partial class TraceSenderView
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
            this.splitContainerTraceSender = new Konvolucio.WinForms.Framework.KnvSplitContainer();
            this._traceGridView1 = new Konvolucio.MCAN120803.GUI.AppModules.Tracer.View.TraceGridView();
            this.adapterParametersControl1 = new Konvolucio.MCAN120803.GUI.AppModules.Adapters.View.AdapterParametersView();
            this.knvMultiPageView1 = new Konvolucio.WinForms.Framework.KnvMultiPageView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTraceSender)).BeginInit();
            this.splitContainerTraceSender.Panel1.SuspendLayout();
            this.splitContainerTraceSender.Panel2.SuspendLayout();
            this.splitContainerTraceSender.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerTraceSender
            // 
            this.splitContainerTraceSender.BackColor = System.Drawing.Color.Orange;
            this.splitContainerTraceSender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTraceSender.Location = new System.Drawing.Point(0, 0);
            this.splitContainerTraceSender.Name = "splitContainerTraceSender";
            this.splitContainerTraceSender.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerTraceSender.Panel1
            // 
            this.splitContainerTraceSender.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainerTraceSender.Panel1.Controls.Add(this._traceGridView1);
            this.splitContainerTraceSender.Panel1.Controls.Add(this.adapterParametersControl1);
            this.splitContainerTraceSender.Panel1MinSize = 60;
            // 
            // splitContainerTraceSender.Panel2
            // 
            this.splitContainerTraceSender.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainerTraceSender.Panel2.Controls.Add(this.knvMultiPageView1);
            this.splitContainerTraceSender.Size = new System.Drawing.Size(800, 400);
            this.splitContainerTraceSender.SplitterDistance = 200;
            this.splitContainerTraceSender.SplitterPrecent = 0.5D;
            this.splitContainerTraceSender.TabIndex = 1;
            // 
            // _traceGridView1
            // 
            this._traceGridView1.AutoScrollEnabled = false;
            this._traceGridView1.BackColor = System.Drawing.SystemColors.Control;
            this._traceGridView1.BackgroundText = "TRACE";
            this._traceGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._traceGridView1.Location = new System.Drawing.Point(0, 25);
            this._traceGridView1.Margin = new System.Windows.Forms.Padding(0);
            this._traceGridView1.Name = "_traceGridView1";
            this._traceGridView1.RefreshRate = 100;
            this._traceGridView1.Size = new System.Drawing.Size(800, 175);
            this._traceGridView1.TabIndex = 0;
            this._traceGridView1.TimestampFormat = "";
            // 
            // adapterParametersControl1
            // 
            this.adapterParametersControl1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.adapterParametersControl1.Location = new System.Drawing.Point(0, 0);
            this.adapterParametersControl1.Name = "adapterParametersControl1";
            this.adapterParametersControl1.Size = new System.Drawing.Size(800, 25);
            this.adapterParametersControl1.TabIndex = 1;
            this.adapterParametersControl1.Text = "adapterParametersControl1";
            // 
            // knvMultiPageView1
            // 
            this.knvMultiPageView1.BackgroundText = "Backgorund Text";
            this.knvMultiPageView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.knvMultiPageView1.Location = new System.Drawing.Point(0, 0);
            this.knvMultiPageView1.Name = "knvMultiPageView1";
            this.knvMultiPageView1.Size = new System.Drawing.Size(800, 196);
            this.knvMultiPageView1.TabIndex = 0;
            // 
            // TraceSenderView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerTraceSender);
            this.Name = "TraceSenderView";
            this.Size = new System.Drawing.Size(800, 400);
            this.splitContainerTraceSender.Panel1.ResumeLayout(false);
            this.splitContainerTraceSender.Panel1.PerformLayout();
            this.splitContainerTraceSender.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTraceSender)).EndInit();
            this.splitContainerTraceSender.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WinForms.Framework.KnvSplitContainer splitContainerTraceSender;
        private AppModules.Tracer.View.TraceGridView _traceGridView1;
        private AppModules.Adapters.View.AdapterParametersView adapterParametersControl1;
        private WinForms.Framework.KnvMultiPageView knvMultiPageView1;
    }
}
