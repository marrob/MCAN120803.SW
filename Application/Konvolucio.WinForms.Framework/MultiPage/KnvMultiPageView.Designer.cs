namespace Konvolucio.WinForms.Framework
{
    partial class KnvMultiPageView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.mulitPageStripView1 = new Konvolucio.WinForms.Framework.KnvMultiPageStripView();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(703, 209);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Orange;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(703, 209);
            this.label1.TabIndex = 0;
            this.label1.Text = "Backgorund Text";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mulitPageStripView1
            // 
            this.mulitPageStripView1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mulitPageStripView1.Location = new System.Drawing.Point(0, 0);
            this.mulitPageStripView1.Name = "mulitPageStripView1";
            this.mulitPageStripView1.Size = new System.Drawing.Size(703, 25);
            this.mulitPageStripView1.TabIndex = 0;
            this.mulitPageStripView1.Text = "navigatorStripView1";
            // 
            // KnvMultiPageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mulitPageStripView1);
            this.Name = "KnvMultiPageView";
            this.Size = new System.Drawing.Size(703, 234);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private KnvMultiPageStripView mulitPageStripView1;
        private System.Windows.Forms.Label label1;
    }
}
