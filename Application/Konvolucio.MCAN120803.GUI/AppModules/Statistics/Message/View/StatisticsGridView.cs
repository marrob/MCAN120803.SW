
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.View
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    using Common;
    using Converters;
    using Export.Model;
    using Model;
    using Properties;
    using Services;  /*Culutre*/
    using WinForms.Framework;


    public partial class StatisticsGridView : UserControl, IStatisticsGridView
    {

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MessageStatisticsCollection Source
        {
            get { return _source; }
            set
            {
                if (_source != value)
                {
                    _source = value;
                    SourceChanged();
                }
            }
        }
        private MessageStatisticsCollection _source;
        
        [Category("KNV")]
        public ContextMenuStrip Menu { get { return contextMenuStrip1; } }

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
            set
            {
                if (value == null)
                    DefaultLayout();
                else
                    dataGridView1.ColumnLayout = value;
            }
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

        [Category("KNV")]
        [DefaultValue("Background Text")]
        public string BackgroundText
        {
            get { return dataGridView1.BackgroundText; }
            set { dataGridView1.BackgroundText = value; }
        }


        [Category("KNV")]
        [DefaultValue(250)]
        public int RefreshRate
        {
            get { return _refreshTimer.Interval; }
            set { _refreshTimer.Interval = value; }
        }

        [Category("KNV")]
        [DefaultValue("HH:mm:ss.fff")]
        public string TimestampFormat
        {
            get { return columnTimestamp.DefaultCellStyle.Format; }
            set
            {
                columnTimestamp.DefaultCellStyle.Format = value;
                dataGridView1.Refresh();
                dataGridView1.Invalidate();
            }
        }

        private readonly Timer _refreshTimer;
        private readonly Stopwatch _watch;
        private readonly object _lockObj;

        public StatisticsGridView()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.KnvDoubleBuffered(true);
            _lockObj = new object();

            dataGridView1.KnvNewRowAlwaysShow();
            dataGridView1.KnvColumnHeaderContextMenu();

            columnDirection.ValueType = typeof(MessageDirection);
            columnDirection.DataSource = new BindingSource(CultureText.MessageDirectionDictionary, null);
            columnDirection.ValueMember = "Key";
            columnDirection.DisplayMember = "Value";

            _refreshTimer = new Timer();
            _refreshTimer.Tick += new EventHandler(RefreshTimer_Tick);
            _watch = new Stopwatch();

        }

        private void dataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            switch (dataGridView1.Columns[e.ColumnIndex].Name)
            {
                case "columnIndex": e.Value = _source[e.RowIndex].Index; break;
                case "columnName": e.Value = _source[e.RowIndex].Name; break;
                case "columnDirection": e.Value = _source[e.RowIndex].Direction; break;
                case "columnArbitrationId": e.Value = new ArbitrationIdConverter().ConvertTo(null, null, _source[e.RowIndex].ArbitrationId, typeof(string)); break;
                case "columnType": e.Value = _source[e.RowIndex].Type; break;
                case "columnRemote": e.Value = _source[e.RowIndex].Remote; break;
                case "columnRate":
                {
                    var value = _source[e.RowIndex].Rate;
                    e.Value = value != null ? value.Value.ToString("N3") : AppConstants.ValueNotAvailable2;
                    break;
                }
                case "columnCount": e.Value = _source[e.RowIndex].Count; break;
                case "columnPeriodTime":
                {
                    var value = _source[e.RowIndex].PeriodTime;
                    e.Value = value != null ? value.Value.ToString("N3") : AppConstants.ValueNotAvailable2;
                    break;
                }
                case "columnDeltaMin":
                {
                    var value = _source[e.RowIndex].DeltaMinTime;
                    e.Value = value != null ? value.Value.ToString("N3") : AppConstants.ValueNotAvailable2;
                    break;
                }
                case "columnDeltaMax":
                {
                    var value = _source[e.RowIndex].DeltaMaxTime;
                    e.Value = value != null ? value.Value.ToString("N3") : AppConstants.ValueNotAvailable2;
                    break;
                }
                case "columnTimestamp":
                {
                    var value = _source[e.RowIndex].Timestamp;
                    e.Value = value != null ? value.Value.ToString(TimestampFormat) : AppConstants.ValueNotAvailable2;
                    break;
                }
                case "columnData":
                {
                    var value = _source[e.RowIndex].Timestamp;
                    e.Value = value != null ? new DataFrameConverter().ConvertTo(null, null, _source[e.RowIndex].Data, typeof(string)) : AppConstants.ValueNotAvailable2;
                    break;
                }
                case "columnLength":
                {
                    var value = _source[e.RowIndex].Length;
                    e.Value = value != null ? value.Value.ToString() : AppConstants.ValueNotAvailable2;
                    break;
                }
                case "columnDeltaT":
                {
                    var value = _source[e.RowIndex].DeltaT;
                    e.Value = value != null ? value.Value.ToString("N3") : AppConstants.ValueNotAvailable2;
                    break;
                }
            }
        }

        private void SourceChanged()
        {
            if (_source != null)
            {
                _source.ListChanging += (o, e) =>
                {
                    if (e.ListChangedType == ListChangingType.Clearing)
                    {
                        /*A lista törlése előtt jelezni kell a DataGrid-nek, hogy minden elem el fog tünni és ne keresse.*/
                        dataGridView1.RowCount = 0;
                    }
                };
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            lock (_lockObj)
            {
                Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "Diff Time:" + _watch.ElapsedMilliseconds.ToString("N3"));
                _watch.Restart();

                /*DGV Virtual módban van, listánk hosszát periodikusan tudatom vele.*/
                if (_source != null && _source.Count > 0)
                    dataGridView1.RowCount = _source.Count;

                if (_source != null)
                    _source.UpdateRate();

                dataGridView1.Refresh();
            }
        }

        public void Start()
        {
            _refreshTimer.Start();
            _watch.Start();
        }

        public void Stop()
        {
            _refreshTimer.Stop();
            _watch.Stop();
            dataGridView1.RowCount = _source.Count;
            if (_source != null)
                _source.UpdateRate();
            dataGridView1.Refresh();
        }

        private void SenderView_Load(object sender, EventArgs e)
        {
            UpdateLocalization();
        }

        private void UpdateLocalization()
        {
            buttonToolStripAutoColumnWidth.Text = CultureService.Instance.GetString(CultureText.menuItem_AutoColumnWidth_Text);
            toolStripLabelName.Text = CultureService.Instance.GetString(CultureText.text_STATISTICS);

            if (DesignMode)
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        dataGridView1.Columns[i].HeaderText = dataGridView1.Columns[i].Name;
            }
            else
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Columns[i].HeaderText = CultureService.Instance.GetString(dataGridView1.Columns[i].Name);
            }    
        }

        private void buttonToolStripAutoSizeAll_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void DefaultLayout()
        {
            /*Meglévő Custom oszlopok törlése*/
            var toRemoveColumnsx = dataGridView1.Columns.Cast<DataGridViewColumn>().Where(n => n.Tag is CustomArbIdColumnItem).ToArray();
            foreach (var item in toRemoveColumnsx)
                dataGridView1.Columns.Remove(item);

            /*Minden oszlop legyen látható*/
            dataGridView1.ShowAllColums();
        }
    }
}
