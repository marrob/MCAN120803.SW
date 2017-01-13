namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.View
{
    partial class StatisticsGridView
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
            this.columnDirection = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnArbitrationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnRemote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnPeriodTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDeltaMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDeltaMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDeltaT = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.toolStripLabelName.Size = new System.Drawing.Size(114, 22);
            this.toolStripLabelName.Text = "MESSAGE STATISTICS";
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
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BackgroundText = "MESSAGE STATISTICS";
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeight = 18;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnIndex,
            this.columnName,
            this.columnDirection,
            this.columnArbitrationId,
            this.columnType,
            this.columnRemote,
            this.columnRate,
            this.columnCount,
            this.columnPeriodTime,
            this.columnDeltaMin,
            this.columnDeltaMax,
            this.columnTimestamp,
            this.columnData,
            this.columnLength,
            this.columnDeltaT});
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
            this.dataGridView1.FirstZebraColor = System.Drawing.Color.LightCyan;
            this.dataGridView1.Location = new System.Drawing.Point(0, 25);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 20;
            this.dataGridView1.SecondZebraColor = System.Drawing.Color.Empty;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(800, 175);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.VirtualMode = true;
            this.dataGridView1.ZebraRow = true;
            this.dataGridView1.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView1_CellValueNeeded);
            // 
            // columnIndex
            // 
            this.columnIndex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnIndex.DataPropertyName = "Index";
            this.columnIndex.Frozen = true;
            this.columnIndex.HeaderText = "Index";
            this.columnIndex.Name = "columnIndex";
            this.columnIndex.ReadOnly = true;
            this.columnIndex.Width = 50;
            // 
            // columnName
            // 
            this.columnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnName.DataPropertyName = "Name";
            this.columnName.HeaderText = "Name";
            this.columnName.Name = "columnName";
            this.columnName.ReadOnly = true;
            this.columnName.Width = 50;
            // 
            // columnDirection
            // 
            this.columnDirection.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDirection.DataPropertyName = "Direction";
            this.columnDirection.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.columnDirection.HeaderText = "Direction";
            this.columnDirection.Name = "columnDirection";
            this.columnDirection.ReadOnly = true;
            this.columnDirection.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnDirection.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.columnDirection.Width = 50;
            // 
            // columnArbitrationId
            // 
            this.columnArbitrationId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnArbitrationId.DataPropertyName = "ArbitrationId";
            this.columnArbitrationId.HeaderText = "Arbitration Id";
            this.columnArbitrationId.Name = "columnArbitrationId";
            this.columnArbitrationId.ReadOnly = true;
            this.columnArbitrationId.Width = 50;
            // 
            // columnType
            // 
            this.columnType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnType.DataPropertyName = "Type";
            this.columnType.HeaderText = "Type";
            this.columnType.Name = "columnType";
            this.columnType.ReadOnly = true;
            this.columnType.Width = 50;
            // 
            // columnRemote
            // 
            this.columnRemote.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnRemote.DataPropertyName = "Remote";
            this.columnRemote.HeaderText = "Remote";
            this.columnRemote.Name = "columnRemote";
            this.columnRemote.ReadOnly = true;
            this.columnRemote.Width = 50;
            // 
            // columnRate
            // 
            this.columnRate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnRate.DataPropertyName = "Rate";
            this.columnRate.HeaderText = "Rate";
            this.columnRate.Name = "columnRate";
            this.columnRate.ReadOnly = true;
            this.columnRate.Width = 50;
            // 
            // columnCount
            // 
            this.columnCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnCount.DataPropertyName = "Count";
            this.columnCount.HeaderText = "Count";
            this.columnCount.Name = "columnCount";
            this.columnCount.ReadOnly = true;
            this.columnCount.Width = 50;
            // 
            // columnPeriodTime
            // 
            this.columnPeriodTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnPeriodTime.DataPropertyName = "PeriodTime";
            this.columnPeriodTime.HeaderText = "PeriodTime [ms]";
            this.columnPeriodTime.Name = "columnPeriodTime";
            this.columnPeriodTime.ReadOnly = true;
            this.columnPeriodTime.Width = 50;
            // 
            // columnDeltaMin
            // 
            this.columnDeltaMin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDeltaMin.DataPropertyName = "DeltaMinTime";
            this.columnDeltaMin.HeaderText = "dtMin [ms]";
            this.columnDeltaMin.Name = "columnDeltaMin";
            this.columnDeltaMin.ReadOnly = true;
            this.columnDeltaMin.Width = 50;
            // 
            // columnDeltaMax
            // 
            this.columnDeltaMax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDeltaMax.DataPropertyName = "DeltaMaxTime";
            this.columnDeltaMax.HeaderText = "dtMax [ms]";
            this.columnDeltaMax.Name = "columnDeltaMax";
            this.columnDeltaMax.ReadOnly = true;
            this.columnDeltaMax.Width = 50;
            // 
            // columnTimestamp
            // 
            this.columnTimestamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnTimestamp.DataPropertyName = "Timestamp";
            this.columnTimestamp.HeaderText = "Timestamp";
            this.columnTimestamp.Name = "columnTimestamp";
            this.columnTimestamp.ReadOnly = true;
            this.columnTimestamp.Width = 50;
            // 
            // columnData
            // 
            this.columnData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnData.DataPropertyName = "Data";
            this.columnData.HeaderText = "Data";
            this.columnData.Name = "columnData";
            this.columnData.ReadOnly = true;
            this.columnData.Width = 50;
            // 
            // columnLength
            // 
            this.columnLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnLength.DataPropertyName = "Length";
            this.columnLength.HeaderText = "Length";
            this.columnLength.Name = "columnLength";
            this.columnLength.ReadOnly = true;
            this.columnLength.Width = 50;
            // 
            // columnDeltaT
            // 
            this.columnDeltaT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDeltaT.DataPropertyName = "DeltaT";
            this.columnDeltaT.HeaderText = "DeltaT";
            this.columnDeltaT.Name = "columnDeltaT";
            this.columnDeltaT.ReadOnly = true;
            this.columnDeltaT.Width = 50;
            // 
            // StatisticsGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "StatisticsGridView";
            this.Size = new System.Drawing.Size(800, 200);
            this.Load += new System.EventHandler(this.SenderView_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WinForms.Framework.KnvDataGridView dataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelName;
        private System.Windows.Forms.ToolStripButton buttonToolStripAutoColumnWidth;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnDirection;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnArbitrationId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnRemote;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPeriodTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDeltaMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDeltaMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnData;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDeltaT;
    }
}
