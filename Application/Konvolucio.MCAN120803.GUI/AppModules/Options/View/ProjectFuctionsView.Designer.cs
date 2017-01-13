namespace Konvolucio.MCAN120803.GUI.AppModules.Options.View
{
    partial class ProjectFunctionsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectFunctionsView));
            this.checkBoxMessageStatisticsEnabled = new System.Windows.Forms.CheckBox();
            this.checkBoxAdapterStatisticsEnabled = new System.Windows.Forms.CheckBox();
            this.checkBoxPlayHistoryClearEnabled = new System.Windows.Forms.CheckBox();
            this.checkBoxTraceEnabled = new System.Windows.Forms.CheckBox();
            this.checkBoxLogEnable = new System.Windows.Forms.CheckBox();
            this.checkBoxRxMsgResolvingBySenderEnabled = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxMessageStatisticsEnabled
            // 
            resources.ApplyResources(this.checkBoxMessageStatisticsEnabled, "checkBoxMessageStatisticsEnabled");
            this.checkBoxMessageStatisticsEnabled.Name = "checkBoxMessageStatisticsEnabled";
            this.checkBoxMessageStatisticsEnabled.UseVisualStyleBackColor = true;
            // 
            // checkBoxAdapterStatisticsEnabled
            // 
            resources.ApplyResources(this.checkBoxAdapterStatisticsEnabled, "checkBoxAdapterStatisticsEnabled");
            this.checkBoxAdapterStatisticsEnabled.Name = "checkBoxAdapterStatisticsEnabled";
            this.checkBoxAdapterStatisticsEnabled.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlayHistoryClearEnabled
            // 
            resources.ApplyResources(this.checkBoxPlayHistoryClearEnabled, "checkBoxPlayHistoryClearEnabled");
            this.checkBoxPlayHistoryClearEnabled.Name = "checkBoxPlayHistoryClearEnabled";
            this.checkBoxPlayHistoryClearEnabled.UseVisualStyleBackColor = true;
            // 
            // checkBoxTraceEnabled
            // 
            resources.ApplyResources(this.checkBoxTraceEnabled, "checkBoxTraceEnabled");
            this.checkBoxTraceEnabled.Name = "checkBoxTraceEnabled";
            this.checkBoxTraceEnabled.UseVisualStyleBackColor = true;
            // 
            // checkBoxLogEnable
            // 
            resources.ApplyResources(this.checkBoxLogEnable, "checkBoxLogEnable");
            this.checkBoxLogEnable.Name = "checkBoxLogEnable";
            this.checkBoxLogEnable.UseVisualStyleBackColor = true;
            // 
            // checkBoxRxMsgResolvingBySenderEnabled
            // 
            resources.ApplyResources(this.checkBoxRxMsgResolvingBySenderEnabled, "checkBoxRxMsgResolvingBySenderEnabled");
            this.checkBoxRxMsgResolvingBySenderEnabled.Name = "checkBoxRxMsgResolvingBySenderEnabled";
            this.checkBoxRxMsgResolvingBySenderEnabled.UseVisualStyleBackColor = true;
            // 
            // ProjectFunctionsView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxRxMsgResolvingBySenderEnabled);
            this.Controls.Add(this.checkBoxMessageStatisticsEnabled);
            this.Controls.Add(this.checkBoxAdapterStatisticsEnabled);
            this.Controls.Add(this.checkBoxPlayHistoryClearEnabled);
            this.Controls.Add(this.checkBoxTraceEnabled);
            this.Controls.Add(this.checkBoxLogEnable);
            this.Name = "ProjectFunctionsView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxMessageStatisticsEnabled;
        private System.Windows.Forms.CheckBox checkBoxAdapterStatisticsEnabled;
        private System.Windows.Forms.CheckBox checkBoxPlayHistoryClearEnabled;
        private System.Windows.Forms.CheckBox checkBoxTraceEnabled;
        private System.Windows.Forms.CheckBox checkBoxLogEnable;
        private System.Windows.Forms.CheckBox checkBoxRxMsgResolvingBySenderEnabled;

    }
}
