
namespace Konvolucio.MCAN120803.GUI.AppModules.CanTx.View
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Model;
    using Common;
    using Converters;
    using Export.Model;
    using Extensions;
    using Services;
    using WinForms.Framework;

    public partial class SenderGridView : UserControl, ISenderGridView
    {

        [Category("KNV")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataGridView BaseDataGridView => dataGridView1;

        [Category("KNV")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CanTxMessageCollection Source
        {
            get { return ((CanTxMessageCollection)dataGridView1.DataSource); }
            set { dataGridView1.DataSource = value; }
        }
       
                
        [Category("KNV")]
        public ContextMenuStrip Menu
        {
            get { return contextMenuStrip1; }
        }

        [Category("KNV")]
        public CustomArbIdColumnCollection CustomArbIdColumns
        {
            set { _arbIdToDataGridView.CustomArbIdColumns = value; }
        }

        [Category("KNV")]
        public CanTxMessageItem[] SelectedItems
        {
            get { return dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                .Select(n => (CanTxMessageItem)n.DataBoundItem).ToArray<CanTxMessageItem>(); }
        }

        [Category("KNV")]
        public bool ReadOnly
        {
            get { return dataGridView1.ReadOnly; }
            set { dataGridView1.ReadOnly = value; }
        }

        [Category("KNV")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ColumnLayoutCollection GridLayout
        {
            set { dataGridView1.ColumnLayout = value; }
            get { return dataGridView1.ColumnLayout; }
        }

        [Category("KNV")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IExportTable ExportTable
        {
            get
            {
                var table = new ExportTable();

                for (var column = 0; column < dataGridView1.ColumnCount; column++)
                {
                    table.Columns.Add(new ExportColumnItem()
                    {
                          Name = dataGridView1.Columns[column].Name,
                          HeaderText = dataGridView1.Columns[column].HeaderText,
                          DisplayIndex = dataGridView1.Columns[column].DisplayIndex,
                          Visible = dataGridView1.Columns[column].Visible,
                    });
                }

                for (var row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    table.Rows.Add(new ExportRowItem());
                    for (var column = 0; column < dataGridView1.ColumnCount; column++)
                    {
                        var value = dataGridView1.Rows[row].Cells[column].Value;

                        if (value == null)
                        {
                            table.Rows[row].Cells[column].Value = "";
                        }
                        else
                        {
                            switch (dataGridView1.Columns[column].Name)
                            {
                                case "columnTimestamp":
                                {
                                    table.Rows[row].Cells[column].Value =
                                        dataGridView1.Rows[row].Cells[column].Value.ToString();
                                    break;
                                }

                                case "columnArbitrationId":
                                {
                                        table.Rows[row].Cells[column].Value =
                                            new ArbitrationIdConverter().ConvertToString(null, value);
                                    break;
                                }

                                case "columnData":
                                {
                                        table.Rows[row].Cells[column].Value =
                                            new DataFrameConverter().ConvertToString(null, value);
                                    break;
                                }

                                default:
                                {
                                    table.Rows[row].Cells[column].Value = value.ToString();
                                    break;
                                }
                            }
                        }
                    }
                }


                /*Amit nem akarunk exportálni azt soroljuk itt fel...*/
                table.Columns.RemoveAt(columnClick.Index);
           
                return table;
            
            }

    }


        private readonly CustomArbIdToDataGridView _arbIdToDataGridView;

        public SenderGridView()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.KnvDoubleBuffered(true);
            columnType.ValueType = typeof(ArbitrationIdType);
            columnType.DataSource = Enum.GetValues(typeof(ArbitrationIdType));

            dataGridView1.KnvRowReOrdering();
            //dataGridView1.KnvNewRowAlwaysShow();
            dataGridView1.KnvColumnHeaderContextMenu();

            _arbIdToDataGridView = new CustomArbIdToDataGridView(dataGridView1);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == -1)
                return;

            var customArbIdColumnItem = dataGridView1.Columns[e.ColumnIndex].Tag as CustomArbIdColumnItem;
            if (customArbIdColumnItem != null)
            {
                var arbId = dataGridView1.Rows[e.RowIndex].Cells["columnArbitrationId"].Value;
                if (arbId != null)
                {
                    var customizer = customArbIdColumnItem;
                    if ((ArbitrationIdType)Enum.Parse(typeof(ArbitrationIdType), dataGridView1.Rows[e.RowIndex].Cells["columnType"].Value.ToString()) == customizer.Type)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = customizer.GetValue((UInt32)arbId).ToString(customizer.Format);
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                    }
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Tag is CustomArbIdColumnItem)
            {
                var arbId = (UInt32)dataGridView1.Rows[e.RowIndex].Cells["columnArbitrationId"].Value;
                var customizer = (CustomArbIdColumnItem)(dataGridView1.Columns[e.ColumnIndex].Tag);
                if ((ArbitrationIdType)dataGridView1.Rows[e.RowIndex].Cells["columnType"].Value == customizer.Type)
                {
                    UInt32 customValue = CustomDataConversion.StringToUInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string);
                    dataGridView1.Rows[e.RowIndex].Cells["columnArbitrationId"].Value = customizer.SetValue(customValue, arbId).ToString(customizer.Format);
                }
            }
        }

        private void SenderView_Load(object sender, EventArgs e)
        {
            UpdateLocalization();
        }

        void UpdateLocalization()
        {
            dataGridView1.BackgroundText = CultureService.Instance.GetString(CultureText.text_SENDER);

            if (DesignMode)
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (!(dataGridView1.Columns[i].Tag is CustomArbIdColumnItem))
                        dataGridView1.Columns[i].HeaderText = dataGridView1.Columns[i].Name;
                }
            }
            else
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (!(dataGridView1.Columns[i].Tag is CustomArbIdColumnItem))
                        dataGridView1.Columns[i].HeaderText = CultureService.Instance.GetString(dataGridView1.Columns[i].Name);
                }
            }    
        
        }

        private void buttonToolStripAutoSizeAll_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        private void dataGridViewSender_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex ==  columnIsPeriod.Index)
            {
                if (dataGridView1.IsCurrentCellDirty)
                {
                    dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        private void dataGridViewSender_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == -1) return;
            var column = dataGridView1.Columns[e.ColumnIndex].Name;
            if (column == "columnClick")
            {
                Source.Send(dataGridView1.Rows[e.RowIndex].DataBoundItem);
            }
        }


        #region Layout Save & Restore

        public void DefaultLayout()
        {
            _arbIdToDataGridView.RemoveAllCustomArbColumns();
            dataGridView1.ShowAllColums();
        }

        public void LayoutSave()
        {

        }

        public void LayoutRestore()
        {
        }
        #endregion
    }
}
