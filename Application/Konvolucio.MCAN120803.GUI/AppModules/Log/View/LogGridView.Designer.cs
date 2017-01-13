namespace Konvolucio.MCAN120803.GUI.AppModules.Log.View
{
    partial class LogGridView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
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
            this.dataGridView1.BackgroundText = "LOG VIEW";
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 18;
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.FirstZebraColor = System.Drawing.Color.Bisque;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 16;
            this.dataGridView1.SecondZebraColor = System.Drawing.Color.Empty;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(858, 370);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.ZebraRow = true;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView1_RowPrePaint);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridViewLog_SelectionChanged);
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
            // columnTimestamp
            // 
            this.columnTimestamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnTimestamp.DataPropertyName = "Timestamp";
            this.columnTimestamp.HeaderText = "Timestamp";
            this.columnTimestamp.Name = "columnTimestamp";
            this.columnTimestamp.ReadOnly = true;
            this.columnTimestamp.Width = 50;
            // 
            // columnDirection
            // 
            this.columnDirection.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDirection.DataPropertyName = "Direction";
            this.columnDirection.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.columnDirection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnDirection.HeaderText = "Direction";
            this.columnDirection.Name = "columnDirection";
            this.columnDirection.ReadOnly = true;
            this.columnDirection.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnDirection.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.columnDirection.Width = 50;
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
            // columnLength
            // 
            this.columnLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnLength.DataPropertyName = "Length";
            this.columnLength.HeaderText = "Length";
            this.columnLength.Name = "columnLength";
            this.columnLength.ReadOnly = true;
            this.columnLength.Width = 50;
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
            // columnData
            // 
            this.columnData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnData.DataPropertyName = "Data";
            this.columnData.HeaderText = "Data";
            this.columnData.Name = "columnData";
            this.columnData.ReadOnly = true;
            this.columnData.Width = 50;
            // 
            // columnDocumentation
            // 
            this.columnDocumentation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDocumentation.DataPropertyName = "Documentation";
            this.columnDocumentation.HeaderText = "Documentation";
            this.columnDocumentation.Name = "columnDocumentation";
            this.columnDocumentation.Width = 50;
            // 
            // columnDescription
            // 
            this.columnDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDescription.DataPropertyName = "Description";
            this.columnDescription.HeaderText = "Description";
            this.columnDescription.Name = "columnDescription";
            this.columnDescription.Width = 50;
            // 
            // LogGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LogGridView";
            this.Size = new System.Drawing.Size(858, 370);
            this.Load += new System.EventHandler(this.LogGridViewl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WinForms.Framework.KnvDataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
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

    }
}