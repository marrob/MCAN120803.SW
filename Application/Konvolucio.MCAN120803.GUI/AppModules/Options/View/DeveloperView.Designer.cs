namespace Konvolucio.MCAN120803.GUI.AppModules.Options.View
{
    partial class DeveloperView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeveloperView));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAppSettingsSaveCounter = new System.Windows.Forms.TextBox();
            this.textBoxAppSettingsUpgradeCounter = new System.Windows.Forms.TextBox();
            this.buttonDefault = new System.Windows.Forms.Button();
            this.numericUpDownGuiRefreshRateMs = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxKeyTracingEnabled = new System.Windows.Forms.CheckBox();
            this.checkBoxFirstStart = new System.Windows.Forms.CheckBox();
            this.checkBoxDeveleoper = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownTraceDataGridRefresRateMs = new System.Windows.Forms.NumericUpDown();
            this.comboBoxSenderThreadPriority = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxAdapterThreadPriority = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownSenderThreadSleepMs = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownStatisticsDataGridRefresRateMs = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGuiRefreshRateMs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTraceDataGridRefresRateMs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSenderThreadSleepMs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStatisticsDataGridRefresRateMs)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBoxAppSettingsSaveCounter
            // 
            this.textBoxAppSettingsSaveCounter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxAppSettingsSaveCounter, "textBoxAppSettingsSaveCounter");
            this.textBoxAppSettingsSaveCounter.Name = "textBoxAppSettingsSaveCounter";
            this.textBoxAppSettingsSaveCounter.ReadOnly = true;
            // 
            // textBoxAppSettingsUpgradeCounter
            // 
            this.textBoxAppSettingsUpgradeCounter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxAppSettingsUpgradeCounter, "textBoxAppSettingsUpgradeCounter");
            this.textBoxAppSettingsUpgradeCounter.Name = "textBoxAppSettingsUpgradeCounter";
            this.textBoxAppSettingsUpgradeCounter.ReadOnly = true;
            // 
            // buttonDefault
            // 
            resources.ApplyResources(this.buttonDefault, "buttonDefault");
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.UseVisualStyleBackColor = true;
            this.buttonDefault.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // numericUpDownGuiRefreshRateMs
            // 
            this.numericUpDownGuiRefreshRateMs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.numericUpDownGuiRefreshRateMs, "numericUpDownGuiRefreshRateMs");
            this.numericUpDownGuiRefreshRateMs.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownGuiRefreshRateMs.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownGuiRefreshRateMs.Name = "numericUpDownGuiRefreshRateMs";
            this.numericUpDownGuiRefreshRateMs.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // checkBoxKeyTracingEnabled
            // 
            resources.ApplyResources(this.checkBoxKeyTracingEnabled, "checkBoxKeyTracingEnabled");
            this.checkBoxKeyTracingEnabled.Checked = global::Konvolucio.MCAN120803.GUI.Properties.Settings.Default.KeyTracingEnable;
            this.checkBoxKeyTracingEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxKeyTracingEnabled.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Konvolucio.MCAN120803.GUI.Properties.Settings.Default, "KeyTracingEnable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxKeyTracingEnabled.Name = "checkBoxKeyTracingEnabled";
            this.checkBoxKeyTracingEnabled.UseVisualStyleBackColor = true;
            // 
            // checkBoxFirstStart
            // 
            resources.ApplyResources(this.checkBoxFirstStart, "checkBoxFirstStart");
            this.checkBoxFirstStart.Checked = global::Konvolucio.MCAN120803.GUI.Properties.Settings.Default.IsFirstStart;
            this.checkBoxFirstStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFirstStart.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Konvolucio.MCAN120803.GUI.Properties.Settings.Default, "IsFirstStart", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxFirstStart.Name = "checkBoxFirstStart";
            this.checkBoxFirstStart.UseVisualStyleBackColor = true;
            // 
            // checkBoxDeveleoper
            // 
            resources.ApplyResources(this.checkBoxDeveleoper, "checkBoxDeveleoper");
            this.checkBoxDeveleoper.Checked = global::Konvolucio.MCAN120803.GUI.Properties.Settings.Default.IsDeveloperMode;
            this.checkBoxDeveleoper.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Konvolucio.MCAN120803.GUI.Properties.Settings.Default, "IsDeveloperMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxDeveleoper.Name = "checkBoxDeveleoper";
            this.checkBoxDeveleoper.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // numericUpDownTraceDataGridRefresRateMs
            // 
            this.numericUpDownTraceDataGridRefresRateMs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.numericUpDownTraceDataGridRefresRateMs, "numericUpDownTraceDataGridRefresRateMs");
            this.numericUpDownTraceDataGridRefresRateMs.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownTraceDataGridRefresRateMs.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownTraceDataGridRefresRateMs.Name = "numericUpDownTraceDataGridRefresRateMs";
            this.numericUpDownTraceDataGridRefresRateMs.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // comboBoxSenderThreadPriority
            // 
            this.comboBoxSenderThreadPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxSenderThreadPriority, "comboBoxSenderThreadPriority");
            this.comboBoxSenderThreadPriority.FormattingEnabled = true;
            this.comboBoxSenderThreadPriority.Name = "comboBoxSenderThreadPriority";
            
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // comboBoxAdapterThreadPriority
            // 
            this.comboBoxAdapterThreadPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxAdapterThreadPriority, "comboBoxAdapterThreadPriority");
            this.comboBoxAdapterThreadPriority.FormattingEnabled = true;
            this.comboBoxAdapterThreadPriority.Name = "comboBoxAdapterThreadPriority";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";

            // 
            // numericUpDownSenderThreadSleepMs
            // 
            this.numericUpDownSenderThreadSleepMs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.numericUpDownSenderThreadSleepMs, "numericUpDownSenderThreadSleepMs");
            this.numericUpDownSenderThreadSleepMs.Name = "numericUpDownSenderThreadSleepMs";
            this.numericUpDownSenderThreadSleepMs.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // numericUpDownStatisticsDataGridRefresRateMs
            // 
            this.numericUpDownStatisticsDataGridRefresRateMs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.numericUpDownStatisticsDataGridRefresRateMs, "numericUpDownStatisticsDataGridRefresRateMs");
            this.numericUpDownStatisticsDataGridRefresRateMs.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownStatisticsDataGridRefresRateMs.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownStatisticsDataGridRefresRateMs.Name = "numericUpDownStatisticsDataGridRefresRateMs";
            this.numericUpDownStatisticsDataGridRefresRateMs.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // DeveloperView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numericUpDownStatisticsDataGridRefresRateMs);
            this.Controls.Add(this.numericUpDownSenderThreadSleepMs);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxAdapterThreadPriority);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxSenderThreadPriority);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownTraceDataGridRefresRateMs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownGuiRefreshRateMs);
            this.Controls.Add(this.checkBoxKeyTracingEnabled);
            this.Controls.Add(this.buttonDefault);
            this.Controls.Add(this.checkBoxFirstStart);
            this.Controls.Add(this.checkBoxDeveleoper);
            this.Controls.Add(this.textBoxAppSettingsUpgradeCounter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxAppSettingsSaveCounter);
            this.Controls.Add(this.label1);
            this.Name = "DeveloperView";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGuiRefreshRateMs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTraceDataGridRefresRateMs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSenderThreadSleepMs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStatisticsDataGridRefresRateMs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAppSettingsSaveCounter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAppSettingsUpgradeCounter;
        private System.Windows.Forms.CheckBox checkBoxDeveleoper;
        private System.Windows.Forms.CheckBox checkBoxFirstStart;
        private System.Windows.Forms.Button buttonDefault;
        private System.Windows.Forms.CheckBox checkBoxKeyTracingEnabled;
        private System.Windows.Forms.NumericUpDown numericUpDownGuiRefreshRateMs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownTraceDataGridRefresRateMs;
        private System.Windows.Forms.ComboBox comboBoxSenderThreadPriority;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxAdapterThreadPriority;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownSenderThreadSleepMs;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownStatisticsDataGridRefresRateMs;
    }
}
