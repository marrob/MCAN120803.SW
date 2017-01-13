namespace Konvolucio.MCAN120803.GUI.AppModules.Filter.View
{
    partial class FiltersGridView
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelName = new System.Windows.Forms.ToolStripLabel();
            this.buttonToolStripAutoColumnWidth = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dataGridView1 = new Konvolucio.WinForms.Framework.KnvDataGridView();
            this.columnIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.columnMaskOrArbId = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnArbitrationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnRemote = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.columnDirection = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnMode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelName,
            this.buttonToolStripAutoColumnWidth});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabelName
            // 
            this.toolStripLabelName.Name = "toolStripLabelName";
            this.toolStripLabelName.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabelName.Text = "FILTERS";
            // 
            // buttonToolStripAutoColumnWidth
            // 
            this.buttonToolStripAutoColumnWidth.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonToolStripAutoColumnWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonToolStripAutoColumnWidth.Image = global::Konvolucio.MCAN120803.GUI.Properties.Resources.SizeHorizontal32;
            this.buttonToolStripAutoColumnWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonToolStripAutoColumnWidth.Name = "buttonToolStripAutoColumnWidth";
            this.buttonToolStripAutoColumnWidth.Size = new System.Drawing.Size(23, 22);
            this.buttonToolStripAutoColumnWidth.Text = "Auto Size All";
            this.buttonToolStripAutoColumnWidth.Click += new System.EventHandler(this.buttonToolStripAutoSizeAll_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BackgroundText = "FILTERS";
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeight = 18;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnIndex,
            this.columnName,
            this.columnEnabled,
            this.columnMaskOrArbId,
            this.columnArbitrationId,
            this.columnType,
            this.columnRemote,
            this.columnDirection,
            this.columnMode});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.FirstZebraColor = System.Drawing.Color.LightGoldenrodYellow;
            this.dataGridView1.Location = new System.Drawing.Point(0, 25);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 20;
            this.dataGridView1.SecondZebraColor = System.Drawing.Color.Empty;
            this.dataGridView1.Size = new System.Drawing.Size(800, 175);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.ZebraRow = true;
            // 
            // columnIndex
            // 
            this.columnIndex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnIndex.DataPropertyName = "Index";
            this.columnIndex.Frozen = true;
            this.columnIndex.HeaderText = "Index";
            this.columnIndex.Name = "columnIndex";
            this.columnIndex.Width = 50;
            // 
            // columnName
            // 
            this.columnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnName.DataPropertyName = "Name";
            this.columnName.HeaderText = "Name";
            this.columnName.Name = "columnName";
            this.columnName.Width = 50;
            // 
            // columnEnabled
            // 
            this.columnEnabled.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnEnabled.DataPropertyName = "Enabled";
            this.columnEnabled.FalseValue = "False";
            this.columnEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnEnabled.HeaderText = "Enabled";
            this.columnEnabled.Name = "columnEnabled";
            this.columnEnabled.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnEnabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.columnEnabled.TrueValue = "True";
            this.columnEnabled.Width = 50;
            // 
            // columnMaskOrArbId
            // 
            this.columnMaskOrArbId.DataPropertyName = "MaskOrArbId";
            this.columnMaskOrArbId.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.columnMaskOrArbId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnMaskOrArbId.HeaderText = "Mask Or ArbId";
            this.columnMaskOrArbId.Name = "columnMaskOrArbId";
            this.columnMaskOrArbId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnMaskOrArbId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.columnMaskOrArbId.Width = 50;
            // 
            // columnArbitrationId
            // 
            this.columnArbitrationId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnArbitrationId.DataPropertyName = "MaskOrArbIdValue";
            this.columnArbitrationId.HeaderText = "Value";
            this.columnArbitrationId.Name = "columnArbitrationId";
            this.columnArbitrationId.Width = 50;
            // 
            // columnType
            // 
            this.columnType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnType.DataPropertyName = "Type";
            this.columnType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.columnType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnType.HeaderText = "Type";
            this.columnType.Name = "columnType";
            this.columnType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.columnType.Width = 50;
            // 
            // columnRemote
            // 
            this.columnRemote.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnRemote.DataPropertyName = "Remote";
            this.columnRemote.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.columnRemote.HeaderText = "Remote";
            this.columnRemote.Name = "columnRemote";
            this.columnRemote.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnRemote.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.columnRemote.Width = 50;
            // 
            // columnDirection
            // 
            this.columnDirection.DataPropertyName = "Direction";
            this.columnDirection.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.columnDirection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnDirection.HeaderText = "Direction";
            this.columnDirection.Name = "columnDirection";
            this.columnDirection.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnDirection.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.columnDirection.Width = 50;
            // 
            // columnMode
            // 
            this.columnMode.DataPropertyName = "Mode";
            this.columnMode.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.columnMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnMode.HeaderText = "Mode";
            this.columnMode.Name = "columnMode";
            this.columnMode.Width = 50;
            // 
            // DataGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DataGridView";
            this.Size = new System.Drawing.Size(800, 200);
            this.Load += new System.EventHandler(this.SenderView_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelName;
        private System.Windows.Forms.ToolStripButton buttonToolStripAutoColumnWidth;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public WinForms.Framework.KnvDataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnEnabled;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnMaskOrArbId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnArbitrationId;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnRemote;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnDirection;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnMode;
    }
}
