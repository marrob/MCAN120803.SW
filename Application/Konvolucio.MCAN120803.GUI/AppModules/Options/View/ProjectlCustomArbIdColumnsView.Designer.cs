namespace Konvolucio.MCAN120803.GUI.AppModules.Options.View
{
    partial class ProjectlCustomArbIdColumnsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectlCustomArbIdColumnsView));
            this.knvDataGridView1 = new Konvolucio.WinForms.Framework.KnvDataGridView();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnStartBit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLengthBit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnShift = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnFormat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.knvDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // knvDataGridView1
            // 
            this.knvDataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.knvDataGridView1.BackgroundText = "Backgorund Text";
            this.knvDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.knvDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnName,
            this.columnType,
            this.columnStartBit,
            this.columnLengthBit,
            this.columnShift,
            this.columnFormat,
            this.columnDescription});
            resources.ApplyResources(this.knvDataGridView1, "knvDataGridView1");
            this.knvDataGridView1.FirstZebraColor = System.Drawing.Color.Bisque;
            this.knvDataGridView1.Name = "knvDataGridView1";
            this.knvDataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.knvDataGridView1.SecondZebraColor = System.Drawing.Color.White;
            this.knvDataGridView1.ZebraRow = true;
            // 
            // columnName
            // 
            this.columnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnName.DataPropertyName = "Name";
            resources.ApplyResources(this.columnName, "columnName");
            this.columnName.Name = "columnName";
            // 
            // columnType
            // 
            this.columnType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnType.DataPropertyName = "Type";
            resources.ApplyResources(this.columnType, "columnType");
            this.columnType.Name = "columnType";
            // 
            // columnStartBit
            // 
            this.columnStartBit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnStartBit.DataPropertyName = "StartBit";
            resources.ApplyResources(this.columnStartBit, "columnStartBit");
            this.columnStartBit.Name = "columnStartBit";
            // 
            // columnLengthBit
            // 
            this.columnLengthBit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnLengthBit.DataPropertyName = "LengthBit";
            resources.ApplyResources(this.columnLengthBit, "columnLengthBit");
            this.columnLengthBit.Name = "columnLengthBit";
            // 
            // columnShift
            // 
            this.columnShift.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnShift.DataPropertyName = "Shift";
            resources.ApplyResources(this.columnShift, "columnShift");
            this.columnShift.Name = "columnShift";
            // 
            // columnFormat
            // 
            this.columnFormat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnFormat.DataPropertyName = "Format";
            resources.ApplyResources(this.columnFormat, "columnFormat");
            this.columnFormat.Name = "columnFormat";
            // 
            // columnDescription
            // 
            this.columnDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDescription.DataPropertyName = "Description";
            resources.ApplyResources(this.columnDescription, "columnDescription");
            this.columnDescription.Name = "columnDescription";
            // 
            // GeneralCustomArbitrationIdColumnsView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.knvDataGridView1);
            this.Name = "GeneralCustomArbitrationIdColumnsView";
            ((System.ComponentModel.ISupportInitialize)(this.knvDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WinForms.Framework.KnvDataGridView knvDataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnStartBit;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLengthBit;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnShift;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnFormat;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDescription;







    }
}
