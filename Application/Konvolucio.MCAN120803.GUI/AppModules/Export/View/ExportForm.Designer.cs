namespace Konvolucio.MCAN120803.GUI.AppModules.Export.View
{
    partial class ExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.knvSplitContainer1 = new Konvolucio.WinForms.Framework.KnvSplitContainer();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.knvDataGridView1 = new Konvolucio.WinForms.Framework.KnvDataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelProgressStatus = new System.Windows.Forms.Label();
            this.buttonAbort = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.PanelOptions = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxOpenFolder = new System.Windows.Forms.CheckBox();
            this.checkBoxOpenExported = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxExportFormat = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.knvSplitContainer1)).BeginInit();
            this.knvSplitContainer1.Panel1.SuspendLayout();
            this.knvSplitContainer1.Panel2.SuspendLayout();
            this.knvSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.knvDataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // knvSplitContainer1
            // 
            resources.ApplyResources(this.knvSplitContainer1, "knvSplitContainer1");
            this.knvSplitContainer1.BackColor = System.Drawing.Color.Orange;
            this.knvSplitContainer1.Name = "knvSplitContainer1";
            // 
            // knvSplitContainer1.Panel1
            // 
            resources.ApplyResources(this.knvSplitContainer1.Panel1, "knvSplitContainer1.Panel1");
            this.knvSplitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.knvSplitContainer1.Panel1.Controls.Add(this.checkedListBox1);
            // 
            // knvSplitContainer1.Panel2
            // 
            resources.ApplyResources(this.knvSplitContainer1.Panel2, "knvSplitContainer1.Panel2");
            this.knvSplitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.knvSplitContainer1.Panel2.Controls.Add(this.knvDataGridView1);
            this.knvSplitContainer1.SplitterPrecent = 0.13D;
            // 
            // checkedListBox1
            // 
            resources.ApplyResources(this.checkedListBox1, "checkedListBox1");
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // knvDataGridView1
            // 
            resources.ApplyResources(this.knvDataGridView1, "knvDataGridView1");
            this.knvDataGridView1.AllowUserToAddRows = false;
            this.knvDataGridView1.AllowUserToDeleteRows = false;
            this.knvDataGridView1.AllowUserToOrderColumns = true;
            this.knvDataGridView1.AllowUserToResizeRows = false;
            this.knvDataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.knvDataGridView1.BackgroundText = "EXPORTED";
            this.knvDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.knvDataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.knvDataGridView1.FirstZebraColor = System.Drawing.Color.LightGreen;
            this.knvDataGridView1.MultiSelect = false;
            this.knvDataGridView1.Name = "knvDataGridView1";
            this.knvDataGridView1.ReadOnly = true;
            this.knvDataGridView1.RowHeadersVisible = false;
            this.knvDataGridView1.RowTemplate.Height = 40;
            this.knvDataGridView1.RowTemplate.ReadOnly = true;
            this.knvDataGridView1.SecondZebraColor = System.Drawing.Color.White;
            this.knvDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.knvDataGridView1.ShowCellErrors = false;
            this.knvDataGridView1.ShowCellToolTips = false;
            this.knvDataGridView1.ShowEditingIcon = false;
            this.knvDataGridView1.ShowRowErrors = false;
            this.knvDataGridView1.ZebraRow = false;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.knvSplitContainer1, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.labelProgressStatus);
            this.panel2.Controls.Add(this.buttonAbort);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.buttonOK);
            this.panel2.Controls.Add(this.buttonCancel);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Name = "panel2";
            // 
            // labelProgressStatus
            // 
            resources.ApplyResources(this.labelProgressStatus, "labelProgressStatus");
            this.labelProgressStatus.Name = "labelProgressStatus";
            // 
            // buttonAbort
            // 
            resources.ApplyResources(this.buttonAbort, "buttonAbort");
            this.buttonAbort.Name = "buttonAbort";
            this.buttonAbort.UseVisualStyleBackColor = true;
            this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.buttonBrowse);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBoxPath);
            this.groupBox2.Controls.Add(this.PanelOptions);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // buttonBrowse
            // 
            resources.ApplyResources(this.buttonBrowse, "buttonBrowse");
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBoxPath
            // 
            resources.ApplyResources(this.textBoxPath, "textBoxPath");
            this.textBoxPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPath.Name = "textBoxPath";
            // 
            // PanelOptions
            // 
            resources.ApplyResources(this.PanelOptions, "PanelOptions");
            this.PanelOptions.Name = "PanelOptions";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.checkBoxOpenFolder);
            this.groupBox1.Controls.Add(this.checkBoxOpenExported);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBoxExportFormat);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // checkBoxOpenFolder
            // 
            resources.ApplyResources(this.checkBoxOpenFolder, "checkBoxOpenFolder");
            this.checkBoxOpenFolder.Checked = true;
            this.checkBoxOpenFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOpenFolder.Name = "checkBoxOpenFolder";
            this.checkBoxOpenFolder.UseVisualStyleBackColor = true;
            // 
            // checkBoxOpenExported
            // 
            resources.ApplyResources(this.checkBoxOpenExported, "checkBoxOpenExported");
            this.checkBoxOpenExported.Checked = true;
            this.checkBoxOpenExported.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOpenExported.Name = "checkBoxOpenExported";
            this.checkBoxOpenExported.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // comboBoxExportFormat
            // 
            resources.ApplyResources(this.comboBoxExportFormat, "comboBoxExportFormat");
            this.comboBoxExportFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExportFormat.FormattingEnabled = true;
            this.comboBoxExportFormat.Name = "comboBoxExportFormat";
            this.comboBoxExportFormat.SelectedIndexChanged += new System.EventHandler(this.ComboBoxExportFormat_SelectedIndexChanged);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // ExportForm
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExportForm_FormClosing);
            this.Shown += new System.EventHandler(this.ExportForm_Shown);
            this.knvSplitContainer1.Panel1.ResumeLayout(false);
            this.knvSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.knvSplitContainer1)).EndInit();
            this.knvSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.knvDataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private WinForms.Framework.KnvSplitContainer knvSplitContainer1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private WinForms.Framework.KnvDataGridView knvDataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxOpenExported;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        public System.Windows.Forms.ComboBox comboBoxExportFormat;
        public System.Windows.Forms.Panel PanelOptions;
        private System.Windows.Forms.Label labelProgressStatus;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button buttonAbort;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.CheckBox checkBoxOpenFolder;
    }
}