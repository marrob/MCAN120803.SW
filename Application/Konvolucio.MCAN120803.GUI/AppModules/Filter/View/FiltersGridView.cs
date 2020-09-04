
namespace Konvolucio.MCAN120803.GUI.AppModules.Filter.View
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Common;
    using Converters;
    using Export.Model;
    using Model;

    using Services;/*Culutre*/
    using WinForms.Framework;

    public partial class FiltersGridView : UserControl, IFiltersGridView
    {

        [Category("KNV")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MessageFilterCollection Source
        {
            get { return ((MessageFilterCollection)dataGridView1.DataSource); }
            set { dataGridView1.DataSource = value; }
        }

        [Category("KNV")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MessageFilterItem[] SelectedItems
        {
            get
            {
               return dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                                    .Select(n => (MessageFilterItem)n.DataBoundItem).ToArray<MessageFilterItem>();
            }
        }

        [Category("KNV")]
        public int CurrentRow
        {
            get
            {
                if (dataGridView1.CurrentRow != null)
                    return dataGridView1.CurrentRow.Index;
                else
                    return -1;
            }
        }
   
        [Category("KNV")]
        public ContextMenuStrip Menu { get { return dataGridView1.ContextMenuStrip; } }

        [Category("KNV")]
        [DefaultValue("Background Text")]
        public string BackgroundText
        {
            get { return dataGridView1.BackgroundText; }
            set
            {
                dataGridView1.BackgroundText = value;
                toolStripLabelName.Text = value;
            }
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
        public System.Windows.Forms.DataGridView DataGridViewBase 
        {
            get { return dataGridView1; }
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
                return table;
            }
        }

        public bool AllowClick { get; set; }

        public FiltersGridView()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.KnvDoubleBuffered(true);

            columnMaskOrArbId.ValueType = typeof(MaskOrArbId);
            columnMaskOrArbId.DataSource = new BindingSource(CultureText.MaskOrArbIdDictionary, null);
            columnMaskOrArbId.ValueMember = "Key";
            columnMaskOrArbId.DisplayMember = "Value";         

            columnType.ValueType = typeof(ArbitrationIdType);
            columnType.DataSource = Enum.GetValues(typeof(ArbitrationIdType));

            columnDirection.ValueType = typeof(MessageDirection);
            columnDirection.DataSource = new BindingSource(CultureText.MessageDirectionDictionary, null);
            columnDirection.ValueMember = "Key";
            columnDirection.DisplayMember = "Value";


            columnMode.ValueType = typeof(MessageFilterMode);
            columnMode.DataSource = new BindingSource(CultureText.MessageFilterModeDictionary, null);
            columnMode.ValueMember = "Key";
            columnMode.DisplayMember = "Value";

            dataGridView1.KnvRowReOrdering();
            dataGridView1.KnvColumnHeaderContextMenu();
        }

        private void SenderView_Load(object sender, EventArgs e)
        {
            UpdateLocalization();
        }

        private void UpdateLocalization()
        {
             buttonToolStripAutoColumnWidth.Text = CultureService.Instance.GetString(CultureText.menuItem_AutoColumnWidth_Text);

            if (DesignMode)
            {
                for (var i = 0; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Columns[i].HeaderText = dataGridView1.Columns[i].Name;
            }
            else
            {
                for (var i = 0; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Columns[i].HeaderText = CultureService.Instance.GetString(dataGridView1.Columns[i].Name);
            }    
        }

        private void buttonToolStripAutoSizeAll_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        public void DefaultLayout()
        {
            dataGridView1.ShowAllColums();
        }
    }
}
