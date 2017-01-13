namespace Konvolucio.MCAN120803.GUI.AppModules.Tracer.View
{
    partial class TraceGridView
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonToolStripClear = new System.Windows.Forms.ToolStripButton();
            this.buttonToolStripAutoScroll = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelName = new System.Windows.Forms.ToolStripLabel();
            this.buttonToolStripAutoColumnWidth = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFullscreen = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dataGridView1 = new Konvolucio.WinForms.Framework.KnvDataGridView();
            this.columnIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDirection = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnRemote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnArbitrationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDocumentation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonToolStripClear,
            this.buttonToolStripAutoScroll,
            this.toolStripLabelName,
            this.buttonToolStripAutoColumnWidth,
            this.toolStripButtonFullscreen});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonToolStripClear
            // 
            this.buttonToolStripClear.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonToolStripClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonToolStripClear.Image = global::Konvolucio.MCAN120803.GUI.Properties.Resources.Delete48x48;
            this.buttonToolStripClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonToolStripClear.Name = "buttonToolStripClear";
            this.buttonToolStripClear.Size = new System.Drawing.Size(23, 22);
            this.buttonToolStripClear.Text = "CLEAR";
            this.buttonToolStripClear.Click += new System.EventHandler(this.buttonToolStripClear_Click);
            // 
            // buttonToolStripAutoScroll
            // 
            this.buttonToolStripAutoScroll.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonToolStripAutoScroll.CheckOnClick = true;
            this.buttonToolStripAutoScroll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonToolStripAutoScroll.Image = global::Konvolucio.MCAN120803.GUI.Properties.Resources.Scroll48;
            this.buttonToolStripAutoScroll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonToolStripAutoScroll.Name = "buttonToolStripAutoScroll";
            this.buttonToolStripAutoScroll.Size = new System.Drawing.Size(23, 22);
            this.buttonToolStripAutoScroll.Text = "Auto-Scrolling";
            this.buttonToolStripAutoScroll.Click += new System.EventHandler(this.buttonToolStripAutoScroll_Click);
            // 
            // toolStripLabelName
            // 
            this.toolStripLabelName.Name = "toolStripLabelName";
            this.toolStripLabelName.Size = new System.Drawing.Size(69, 22);
            this.toolStripLabelName.Text = "TRACE VIEW";
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
            // toolStripButtonFullscreen
            // 
            this.toolStripButtonFullscreen.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonFullscreen.CheckOnClick = true;
            this.toolStripButtonFullscreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFullscreen.Image = global::Konvolucio.MCAN120803.GUI.Properties.Resources.fullscreen16;
            this.toolStripButtonFullscreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFullscreen.Name = "toolStripButtonFullscreen";
            this.toolStripButtonFullscreen.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonFullscreen.Text = "toolStripButton1";
            this.toolStripButtonFullscreen.Click += new System.EventHandler(this.toolStripButtonFullscreen_Click);
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
            this.dataGridView1.BackgroundText = "TRACE";
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeight = 20;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnIndex,
            this.columnName,
            this.columnTimestamp,
            this.columnDirection,
            this.columnType,
            this.columnRemote,
            this.columnLength,
            this.columnArbitrationId,
            this.columnData,
            this.columnDocumentation,
            this.columnDescription});
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
            this.dataGridView1.FirstZebraColor = System.Drawing.Color.AliceBlue;
            this.dataGridView1.Location = new System.Drawing.Point(0, 25);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 16;
            this.dataGridView1.SecondZebraColor = System.Drawing.Color.Empty;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(800, 175);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.VirtualMode = true;
            this.dataGridView1.ZebraRow = true;
            this.dataGridView1.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView1_CellValueNeeded);
            this.dataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView1_RowPrePaint);
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // columnIndex
            // 
            this.columnIndex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnIndex.Frozen = true;
            this.columnIndex.HeaderText = "Index";
            this.columnIndex.Name = "columnIndex";
            this.columnIndex.ReadOnly = true;
            // 
            // columnName
            // 
            this.columnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnName.HeaderText = "Name";
            this.columnName.Name = "columnName";
            this.columnName.ReadOnly = true;
            this.columnName.Width = 60;
            // 
            // columnTimestamp
            // 
            this.columnTimestamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnTimestamp.HeaderText = "Timestamp";
            this.columnTimestamp.Name = "columnTimestamp";
            this.columnTimestamp.ReadOnly = true;
            this.columnTimestamp.Width = 83;
            // 
            // columnDirection
            // 
            this.columnDirection.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDirection.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.columnDirection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnDirection.HeaderText = "Direction";
            this.columnDirection.Name = "columnDirection";
            this.columnDirection.ReadOnly = true;
            this.columnDirection.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnDirection.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.columnDirection.Width = 74;
            // 
            // columnType
            // 
            this.columnType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnType.HeaderText = "Type";
            this.columnType.Name = "columnType";
            this.columnType.ReadOnly = true;
            this.columnType.Width = 56;
            // 
            // columnRemote
            // 
            this.columnRemote.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnRemote.HeaderText = "Remote";
            this.columnRemote.Name = "columnRemote";
            this.columnRemote.ReadOnly = true;
            this.columnRemote.Width = 69;
            // 
            // columnLength
            // 
            this.columnLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnLength.HeaderText = "Length";
            this.columnLength.Name = "columnLength";
            this.columnLength.ReadOnly = true;
            this.columnLength.Width = 65;
            // 
            // columnArbitrationId
            // 
            this.columnArbitrationId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnArbitrationId.HeaderText = "Arbitration Id";
            this.columnArbitrationId.Name = "columnArbitrationId";
            this.columnArbitrationId.ReadOnly = true;
            this.columnArbitrationId.Width = 91;
            // 
            // columnData
            // 
            this.columnData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnData.HeaderText = "Data";
            this.columnData.Name = "columnData";
            this.columnData.ReadOnly = true;
            this.columnData.Width = 55;
            // 
            // columnDocumentation
            // 
            this.columnDocumentation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDocumentation.HeaderText = "Documentation";
            this.columnDocumentation.Name = "columnDocumentation";
            this.columnDocumentation.ReadOnly = true;
            this.columnDocumentation.Width = 104;
            // 
            // columnDescription
            // 
            this.columnDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDescription.HeaderText = "Description";
            this.columnDescription.Name = "columnDescription";
            this.columnDescription.ReadOnly = true;
            this.columnDescription.Width = 5;
            // 
            // TraceGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TraceGridView";
            this.Size = new System.Drawing.Size(800, 200);
            this.Load += new System.EventHandler(this.TraceViewControl_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WinForms.Framework.KnvDataGridView dataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonToolStripClear;
        private System.Windows.Forms.ToolStripButton buttonToolStripAutoScroll;
        private System.Windows.Forms.ToolStripLabel toolStripLabelName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripButton buttonToolStripAutoColumnWidth;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTimestamp;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnDirection;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnRemote;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnArbitrationId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnData;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDocumentation;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDescription;
        private System.Windows.Forms.ToolStripButton toolStripButtonFullscreen;
    }
}