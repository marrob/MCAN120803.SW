namespace Konvolucio.MCAN120803.GUI.AppModules.Export.View
{
    partial class CsvOptionsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CsvOptionsView));
            this.comboBoxEscape = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxDelimiter = new System.Windows.Forms.ComboBox();
            this.comboBoxNewLine = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // comboBoxEscape
            // 
            resources.ApplyResources(this.comboBoxEscape, "comboBoxEscape");
            this.comboBoxEscape.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEscape.FormattingEnabled = true;
            this.comboBoxEscape.Name = "comboBoxEscape";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // comboBoxDelimiter
            // 
            resources.ApplyResources(this.comboBoxDelimiter, "comboBoxDelimiter");
            this.comboBoxDelimiter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDelimiter.FormattingEnabled = true;
            this.comboBoxDelimiter.Name = "comboBoxDelimiter";
            // 
            // comboBoxNewLine
            // 
            resources.ApplyResources(this.comboBoxNewLine, "comboBoxNewLine");
            this.comboBoxNewLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNewLine.FormattingEnabled = true;
            this.comboBoxNewLine.Name = "comboBoxNewLine";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // CsvOptionsView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxNewLine);
            this.Controls.Add(this.comboBoxDelimiter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxEscape);
            this.Name = "CsvOptionsView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxEscape;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxDelimiter;
        private System.Windows.Forms.ComboBox comboBoxNewLine;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;

    }
}
