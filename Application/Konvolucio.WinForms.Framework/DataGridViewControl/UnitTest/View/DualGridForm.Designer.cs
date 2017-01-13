namespace Konvolucio.WinForms.Framework.DataGridViewControl.UnitTest.View
{
    partial class DualGridForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.knvSplitContainer1 = new Konvolucio.WinForms.Framework.KnvSplitContainer();
            this.userControl11 = new Konvolucio.WinForms.Framework.DataGridViewControl.UnitTest.View.UserControlView();
            this.userControl12 = new Konvolucio.WinForms.Framework.DataGridViewControl.UnitTest.View.UserControlView();
            ((System.ComponentModel.ISupportInitialize)(this.knvSplitContainer1)).BeginInit();
            this.knvSplitContainer1.Panel1.SuspendLayout();
            this.knvSplitContainer1.Panel2.SuspendLayout();
            this.knvSplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.AccessibleName = "";
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
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
            this.knvSplitContainer1.Panel1.Controls.Add(this.userControl12);
            // 
            // knvSplitContainer1.Panel2
            // 
            this.knvSplitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.knvSplitContainer1.Panel2.Controls.Add(this.userControl11);
            this.knvSplitContainer1.Size = new System.Drawing.Size(722, 216);
            this.knvSplitContainer1.SplitterDistance = 351;
            this.knvSplitContainer1.SplitterPrecent = 0.49D;
            this.knvSplitContainer1.TabIndex = 1;
            // 
            // userControl11
            // 
            this.userControl11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControl11.Location = new System.Drawing.Point(0, 0);
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(367, 216);
            this.userControl11.TabIndex = 0;
            // 
            // userControl12
            // 
            this.userControl12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControl12.Location = new System.Drawing.Point(0, 0);
            this.userControl12.Name = "userControl12";
            this.userControl12.Size = new System.Drawing.Size(351, 216);
            this.userControl12.TabIndex = 0;
            // 
            // UnitTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 216);
            this.Controls.Add(this.knvSplitContainer1);
            this.Name = "DualGridForm";
            this.Text = "form";
            this.knvSplitContainer1.Panel1.ResumeLayout(false);
            this.knvSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.knvSplitContainer1)).EndInit();
            this.knvSplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KnvSplitContainer knvSplitContainer1;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private UserControlView userControl12;
        private UserControlView userControl11;
    }
}