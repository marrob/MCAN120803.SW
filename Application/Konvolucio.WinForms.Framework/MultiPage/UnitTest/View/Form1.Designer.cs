namespace Konvolucio.WinForms.Framework.MultiPage.UnitTest.View
{
    partial class Form1
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
            this.KnvMultiPageView1 = new Konvolucio.WinForms.Framework.KnvMultiPageView();
            this.buttonClear = new System.Windows.Forms.Button();
            this.treeView1 = new Konvolucio.WinForms.Framework.KnvTreeView();
            this.SuspendLayout();
            // 
            // KnvMultiPageView1
            // 
            this.KnvMultiPageView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.KnvMultiPageView1.Location = new System.Drawing.Point(152, 0);
            this.KnvMultiPageView1.Name = "KnvMultiPageView1";
            this.KnvMultiPageView1.Size = new System.Drawing.Size(488, 340);
            this.KnvMultiPageView1.TabIndex = 0;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(12, 27);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.Text = "button1";
            this.buttonClear.UseVisualStyleBackColor = true;
            // 
            // treeViewView1
            // 
            this.treeView1.Location = new System.Drawing.Point(2, 76);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(144, 252);
            this.treeView1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 340);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.KnvMultiPageView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        public KnvMultiPageView KnvMultiPageView1;
        public System.Windows.Forms.Button buttonClear;
        public KnvTreeView treeView1;


    }
}