namespace Konvolucio.MCAN120803.GUI.AppModules.Options.View
{
    partial class GeneralView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralView));
            this.comboBoxCultureName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxShowWorkingDirectory = new System.Windows.Forms.CheckBox();
            this.checkBoxLoadProjectOnAppStart = new System.Windows.Forms.CheckBox();
            this.checkBoxSingleInstanceApp = new System.Windows.Forms.CheckBox();
            this.textBoxBrowseLocation = new System.Windows.Forms.TextBox();
            this.labelBrowseLoaction = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxCultureName
            // 
            resources.ApplyResources(this.comboBoxCultureName, "comboBoxCultureName");
            this.comboBoxCultureName.FormattingEnabled = true;
            this.comboBoxCultureName.Name = "comboBoxCultureName";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // checkBoxShowWorkingDirectory
            // 
            resources.ApplyResources(this.checkBoxShowWorkingDirectory, "checkBoxShowWorkingDirectory");
            this.checkBoxShowWorkingDirectory.Name = "checkBoxShowWorkingDirectory";
            this.checkBoxShowWorkingDirectory.UseVisualStyleBackColor = true;
            // 
            // checkBoxLoadProjectOnAppStart
            // 
            resources.ApplyResources(this.checkBoxLoadProjectOnAppStart, "checkBoxLoadProjectOnAppStart");
            this.checkBoxLoadProjectOnAppStart.Checked = true;
            this.checkBoxLoadProjectOnAppStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLoadProjectOnAppStart.Name = "checkBoxLoadProjectOnAppStart";
            this.checkBoxLoadProjectOnAppStart.UseVisualStyleBackColor = true;
            // 
            // checkBoxSingleInstanceApp
            // 
            resources.ApplyResources(this.checkBoxSingleInstanceApp, "checkBoxSingleInstanceApp");
            this.checkBoxSingleInstanceApp.Name = "checkBoxSingleInstanceApp";
            this.checkBoxSingleInstanceApp.UseVisualStyleBackColor = true;
            // 
            // textBoxBrowseLocation
            // 
            this.textBoxBrowseLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxBrowseLocation, "textBoxBrowseLocation");
            this.textBoxBrowseLocation.Name = "textBoxBrowseLocation";
            this.textBoxBrowseLocation.Tag = "";
            // 
            // labelBrowseLoaction
            // 
            resources.ApplyResources(this.labelBrowseLoaction, "labelBrowseLoaction");
            this.labelBrowseLoaction.Name = "labelBrowseLoaction";
            // 
            // buttonBrowse
            // 
            resources.ApplyResources(this.buttonBrowse, "buttonBrowse");
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Text_Click);
            // 
            // GeneralView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.labelBrowseLoaction);
            this.Controls.Add(this.textBoxBrowseLocation);
            this.Controls.Add(this.checkBoxSingleInstanceApp);
            this.Controls.Add(this.checkBoxLoadProjectOnAppStart);
            this.Controls.Add(this.checkBoxShowWorkingDirectory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxCultureName);
            this.Name = "GeneralView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCultureName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxShowWorkingDirectory;
        private System.Windows.Forms.CheckBox checkBoxLoadProjectOnAppStart;
        private System.Windows.Forms.CheckBox checkBoxSingleInstanceApp;
        private System.Windows.Forms.TextBox textBoxBrowseLocation;
        private System.Windows.Forms.Label labelBrowseLoaction;
        private System.Windows.Forms.Button buttonBrowse;
    }
}
