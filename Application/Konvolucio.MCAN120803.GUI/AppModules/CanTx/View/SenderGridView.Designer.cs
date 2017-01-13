namespace Konvolucio.MCAN120803.GUI.AppModules.CanTx.View
{
    partial class SenderGridView
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dataGridView1 = new Konvolucio.WinForms.Framework.KnvDataGridView();
            this.columnIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnClick = new System.Windows.Forms.DataGridViewButtonColumn();
            this.columnKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnIsPeriod = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.columnPeriodTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnRemote = new System.Windows.Forms.DataGridViewCheckBoxColumn();
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
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BackgroundText = "SENDER";
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeight = 18;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnIndex,
            this.columnName,
            this.columnClick,
            this.columnKey,
            this.columnIsPeriod,
            this.columnPeriodTime,
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
            this.dataGridView1.FirstZebraColor = System.Drawing.Color.Bisque;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 20;
            this.dataGridView1.SecondZebraColor = System.Drawing.Color.Empty;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(800, 200);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.ZebraRow = true;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSender_CellClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewSender_CurrentCellDirtyStateChanged);
            // 
            // columnIndex
            // 
            this.columnIndex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnIndex.DataPropertyName = "Index";
            this.columnIndex.Frozen = true;
            this.columnIndex.HeaderText = " Index";
            this.columnIndex.Name = "columnIndex";
            // 
            // columnName
            // 
            this.columnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnName.DataPropertyName = "Name";
            this.columnName.HeaderText = " Name";
            this.columnName.Name = "columnName";
            this.columnName.Width = 63;
            // 
            // columnClick
            // 
            this.columnClick.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnClick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnClick.HeaderText = "Click";
            this.columnClick.Name = "columnClick";
            this.columnClick.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnClick.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.columnClick.Width = 55;
            // 
            // columnKey
            // 
            this.columnKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnKey.DataPropertyName = "Key";
            this.columnKey.HeaderText = "Key";
            this.columnKey.Name = "columnKey";
            this.columnKey.Width = 50;
            // 
            // columnIsPeriod
            // 
            this.columnIsPeriod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnIsPeriod.DataPropertyName = "IsPeriod";
            this.columnIsPeriod.FalseValue = "False";
            this.columnIsPeriod.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.columnIsPeriod.HeaderText = "Is Period";
            this.columnIsPeriod.Name = "columnIsPeriod";
            this.columnIsPeriod.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnIsPeriod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.columnIsPeriod.TrueValue = "True";
            this.columnIsPeriod.Width = 73;
            // 
            // columnPeriodTime
            // 
            this.columnPeriodTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnPeriodTime.DataPropertyName = "PeriodTime";
            this.columnPeriodTime.HeaderText = "PeriodTime(ms)";
            this.columnPeriodTime.Name = "columnPeriodTime";
            this.columnPeriodTime.Width = 104;
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
            this.columnType.Width = 56;
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
            this.columnRemote.Width = 69;
            // 
            // columnLength
            // 
            this.columnLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnLength.DataPropertyName = "Length";
            this.columnLength.HeaderText = "Length";
            this.columnLength.Name = "columnLength";
            this.columnLength.Width = 65;
            // 
            // columnArbitrationId
            // 
            this.columnArbitrationId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnArbitrationId.DataPropertyName = "ArbitrationId";
            this.columnArbitrationId.HeaderText = "Arbitration Id";
            this.columnArbitrationId.Name = "columnArbitrationId";
            this.columnArbitrationId.Width = 91;
            // 
            // columnData
            // 
            this.columnData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnData.DataPropertyName = "Data";
            this.columnData.HeaderText = "Data";
            this.columnData.Name = "columnData";
            this.columnData.Width = 55;
            // 
            // columnDocumentation
            // 
            this.columnDocumentation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDocumentation.DataPropertyName = "Documentation";
            this.columnDocumentation.HeaderText = "Documentation";
            this.columnDocumentation.Name = "columnDocumentation";
            this.columnDocumentation.Width = 104;
            // 
            // columnDescription
            // 
            this.columnDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDescription.DataPropertyName = "Description";
            this.columnDescription.HeaderText = " Description";
            this.columnDescription.Name = "columnDescription";
            this.columnDescription.Width = 5;
            // 
            // SenderGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "SenderGridView";
            this.Size = new System.Drawing.Size(800, 200);
            this.Load += new System.EventHandler(this.SenderView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public WinForms.Framework.KnvDataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewButtonColumn columnClick;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnKey;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnIsPeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPeriodTime;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnRemote;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnArbitrationId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnData;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDocumentation;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDescription;
    }
}
