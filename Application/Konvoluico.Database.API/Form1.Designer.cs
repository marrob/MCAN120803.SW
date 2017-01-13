namespace Konvolucio.Database.API
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
            this.dataGridViewBackgorundText1 = new Konvolucio.Database.API.DataGridViewBackgorundText();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBackgorundText1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewBackgorundText1
            // 
            this.dataGridViewBackgorundText1.BackgroundText = null;
            this.dataGridViewBackgorundText1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBackgorundText1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridViewBackgorundText1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewBackgorundText1.Name = "dataGridViewBackgorundText1";
            this.dataGridViewBackgorundText1.Size = new System.Drawing.Size(385, 273);
            this.dataGridViewBackgorundText1.TabIndex = 0;
            this.dataGridViewBackgorundText1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBackgorundText1_CellContentClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 273);
            this.Controls.Add(this.dataGridViewBackgorundText1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBackgorundText1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridViewBackgorundText dataGridViewBackgorundText1;
    }
}