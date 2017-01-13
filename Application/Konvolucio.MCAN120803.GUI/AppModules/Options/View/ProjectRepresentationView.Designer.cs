namespace Konvolucio.MCAN120803.GUI.AppModules.Options.View
{
    partial class ProjectRepresentationView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectRepresentationView));
            this.comboBoxTimestampFormat = new System.Windows.Forms.ComboBox();
            this.textBoxSampleTimestamp = new System.Windows.Forms.TextBox();
            this.labelDataFormat = new System.Windows.Forms.Label();
            this.labelArbitrationIdFormat = new System.Windows.Forms.Label();
            this.comboBoxArbitrationIdFormat = new System.Windows.Forms.ComboBox();
            this.labelTimestampFormat = new System.Windows.Forms.Label();
            this.textBoxSampleDataFormat = new System.Windows.Forms.TextBox();
            this.comboBoxDataFormat = new System.Windows.Forms.ComboBox();
            this.textBoxSampleArbitrationIdFormat = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBoxTimestampFormat
            // 
            resources.ApplyResources(this.comboBoxTimestampFormat, "comboBoxTimestampFormat");
            this.comboBoxTimestampFormat.FormattingEnabled = true;
            this.comboBoxTimestampFormat.Name = "comboBoxTimestampFormat";
            this.comboBoxTimestampFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxTimestampFormat_SelectedIndexChanged);
            // 
            // textBoxSampleTimestamp
            // 
            this.textBoxSampleTimestamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxSampleTimestamp, "textBoxSampleTimestamp");
            this.textBoxSampleTimestamp.Name = "textBoxSampleTimestamp";
            this.textBoxSampleTimestamp.ReadOnly = true;
            // 
            // labelDataFormat
            // 
            resources.ApplyResources(this.labelDataFormat, "labelDataFormat");
            this.labelDataFormat.Name = "labelDataFormat";
            // 
            // labelArbitrationIdFormat
            // 
            resources.ApplyResources(this.labelArbitrationIdFormat, "labelArbitrationIdFormat");
            this.labelArbitrationIdFormat.Name = "labelArbitrationIdFormat";
            // 
            // comboBoxArbitrationIdFormat
            // 
            this.comboBoxArbitrationIdFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxArbitrationIdFormat, "comboBoxArbitrationIdFormat");
            this.comboBoxArbitrationIdFormat.FormattingEnabled = true;
            this.comboBoxArbitrationIdFormat.Name = "comboBoxArbitrationIdFormat";
            this.comboBoxArbitrationIdFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxArbitrationIdFormat_SelectedIndexChanged);
            // 
            // labelTimestampFormat
            // 
            resources.ApplyResources(this.labelTimestampFormat, "labelTimestampFormat");
            this.labelTimestampFormat.Name = "labelTimestampFormat";
            // 
            // textBoxSampleDataFormat
            // 
            this.textBoxSampleDataFormat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxSampleDataFormat, "textBoxSampleDataFormat");
            this.textBoxSampleDataFormat.Name = "textBoxSampleDataFormat";
            this.textBoxSampleDataFormat.ReadOnly = true;
            // 
            // comboBoxDataFormat
            // 
            this.comboBoxDataFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxDataFormat, "comboBoxDataFormat");
            this.comboBoxDataFormat.FormattingEnabled = true;
            this.comboBoxDataFormat.Name = "comboBoxDataFormat";
            this.comboBoxDataFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxDataFormat_SelectedIndexChanged);
            // 
            // textBoxSampleArbitrationIdFormat
            // 
            this.textBoxSampleArbitrationIdFormat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxSampleArbitrationIdFormat, "textBoxSampleArbitrationIdFormat");
            this.textBoxSampleArbitrationIdFormat.Name = "textBoxSampleArbitrationIdFormat";
            this.textBoxSampleArbitrationIdFormat.ReadOnly = true;
            // 
            // ProjectRepresentationView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxSampleArbitrationIdFormat);
            this.Controls.Add(this.comboBoxDataFormat);
            this.Controls.Add(this.textBoxSampleDataFormat);
            this.Controls.Add(this.labelTimestampFormat);
            this.Controls.Add(this.labelDataFormat);
            this.Controls.Add(this.textBoxSampleTimestamp);
            this.Controls.Add(this.comboBoxArbitrationIdFormat);
            this.Controls.Add(this.labelArbitrationIdFormat);
            this.Controls.Add(this.comboBoxTimestampFormat);
            this.Name = "ProjectRepresentationView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxTimestampFormat;
        private System.Windows.Forms.Label labelDataFormat;
        private System.Windows.Forms.TextBox textBoxSampleTimestamp;
        private System.Windows.Forms.Label labelArbitrationIdFormat;
        private System.Windows.Forms.ComboBox comboBoxArbitrationIdFormat;
        private System.Windows.Forms.Label labelTimestampFormat;
        private System.Windows.Forms.TextBox textBoxSampleDataFormat;
        private System.Windows.Forms.ComboBox comboBoxDataFormat;
        private System.Windows.Forms.TextBox textBoxSampleArbitrationIdFormat;
    }
}
