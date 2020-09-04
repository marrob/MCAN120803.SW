

namespace Konvolucio.MCAN120803.GUI.AppModules.Log.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    using WinForms.Framework;   /*datgridViewBackgorunText*/
    using Services;             /*Culutre*/
    using Model;
    using Common;
    using Converters;
    using Export.Model;
    using Extensions;

    /// <summary>
    /// 
    /// </summary>
    public partial class LogGridView : UserControl, ILogGridView
    {
        [Category("KNV")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ILogFileMessageCollection Source 
        {
            get { return ((ILogFileMessageCollection)dataGridView1.DataSource); }
            set { dataGridView1.DataSource = value; }
        }

        [Category("KNV")]
        public ContextMenuStrip Menu { get { return _contextView; } }
        readonly ContextMenuStrip _contextView;

        [Category("KNV")]
        [DefaultValue("HH:mm:ss.fff")]
        public string TimestampFormat 
        {
            get {  return columnTimestamp.DefaultCellStyle.Format; }
            set { columnTimestamp.DefaultCellStyle.Format = value; }  
        }

        [Category("KNV")]
        [DefaultValue("Background Text")]
        public string BackgroundText
        {
            get { return dataGridView1.BackgroundText; }
            set { dataGridView1.BackgroundText = value; }
        }

        [Category("KNV")]
        public CustomArbIdColumnCollection CustomArbIdColumns
        {
            set { _arbIdToDataGridView.CustomArbIdColumns = value; }
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
                return table;
            }
        }

        public bool AllowClick { get; set; }

        private readonly CustomArbIdToDataGridView _arbIdToDataGridView;

        /// <summary>
        /// Constructor
        /// </summary>
        public LogGridView()
        {
            InitializeComponent();
            _contextView = new ContextMenuStrip();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.KnvDoubleBuffered(true);

            columnDirection.ValueType = typeof(MessageDirection);
            columnDirection.DataSource = new BindingSource(CultureText.MessageDirectionDictionary, null);
            columnDirection.ValueMember = "Key";
            columnDirection.DisplayMember = "Value";

            _arbIdToDataGridView = new CustomArbIdToDataGridView(dataGridView1)
            {
                ReadOnly = true
            };

            dataGridView1.KnvColumnHeaderContextMenu();
        }

        /// <summary>
        /// Egyedi arbitriációs mezőket rajzolja.
        /// </summary>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == -1)
                return;

            if (dataGridView1.Columns[e.ColumnIndex].Tag is CustomArbIdColumnItem)
            {
                var arbId = dataGridView1.Rows[e.RowIndex].Cells["columnArbitrationId"].Value;
                if (arbId != null)
                {
                    var customizer = (CustomArbIdColumnItem)(dataGridView1.Columns[e.ColumnIndex].Tag);
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

        /// <summary>
        /// Betöltés
        /// </summary>
        private void LogGridViewl_Load(object sender, EventArgs e)
        {
            UpdateLocalization();
        }

        /// <summary>
        /// Nyelvek 
        /// </summary>
        private void UpdateLocalization()
        {
            dataGridView1.BackgroundText = CultureService.Instance.GetString(CultureText.text_LOGVIEW);

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

        /// <summary>
        /// Column Auto Size All
        /// </summary>
        public void ColumnsAutoSizeAll()
        { 
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        
        /// <summary>
        /// Zebra az üezenet iránya szerint.
        /// </summary>
        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (Source[e.RowIndex].Direction == MessageDirection.Transmitted)
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            else
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Green;  
        }

        /// <summary>
        /// Mező váltás estén eseményt aggregál.
        /// </summary>
        private void dataGridViewLog_SelectionChanged(object sender, EventArgs e)
        {
            Source.SelectedItems.Clear();
            foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                Source.SelectedItems.Add(item.DataBoundItem as ILogMessageItem);
            EventAggregator.Instance.Publish<LogSelectionChangedAppEvent>(new LogSelectionChangedAppEvent(Source.SelectedItems));
        }

        /// <summary>
        /// Default Layout
        /// </summary>
        public void DefaultLayout()
        {
            _arbIdToDataGridView.RemoveAllCustomArbColumns();
            dataGridView1.ShowAllColums();
        }
    }
}
