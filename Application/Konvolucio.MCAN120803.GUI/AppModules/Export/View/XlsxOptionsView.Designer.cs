namespace Konvolucio.MCAN120803.GUI.AppModules.Export.View
{
    partial class XlsxOptionsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XlsxOptionsView));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBoxApplyTheFormattingParameters = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBoxApplyTheFormattingParameters
            // 
            resources.ApplyResources(this.checkBoxApplyTheFormattingParameters, "checkBoxApplyTheFormattingParameters");
            this.checkBoxApplyTheFormattingParameters.Checked = true;
            this.checkBoxApplyTheFormattingParameters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxApplyTheFormattingParameters.Name = "checkBoxApplyTheFormattingParameters";
            this.checkBoxApplyTheFormattingParameters.UseVisualStyleBackColor = true;
            // 
            // XlsxOptionsView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxApplyTheFormattingParameters);
            this.Controls.Add(this.checkBox1);
            this.Name = "XlsxOptionsView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBoxApplyTheFormattingParameters;

    }
}
