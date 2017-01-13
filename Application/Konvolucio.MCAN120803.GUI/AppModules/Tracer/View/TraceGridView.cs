
namespace Konvolucio.MCAN120803.GUI.AppModules.Tracer.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;

    using System.Windows.Forms;
    using System.Diagnostics;
    using System.Reflection;

    using WinForms.Framework;
    using Services; 
    using Common;
    using Model;
    using Converters;
    using Extensions;


    public partial class TraceGridView : UserControl, ITraceGridView
    {

        public event EventHandler FullScreenChanged;

        [Category("KNV")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MessageTraceCollection Source 
        {
            get { return _source; }
            set
            {
                if (_source != value)
                {
                    _source = value;
                    SourceListChanged();
                }
            }
        }
        private MessageTraceCollection _source;

        //[Category("KNV")]
        //public ContextMenuStrip Menu { get { return contextMenuStrip1; } }

        [Category("KNV")]
        [DefaultValue("Background Text")]
        public string BackgroundText
        {
            get { return dataGridView1.BackgroundText; }
            set { dataGridView1.BackgroundText = value; }
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

        [Category("KNV")]
        [DefaultValue(250)]
        public int RefreshRate
        {
            get { return _refreshTimer.Interval; }
            set { _refreshTimer.Interval = value; }
        }

        [Category("KNV")]
        [DefaultValue(true)]
        public bool AutoScrollEnabled 
        {
            get { return _autoScorllEnalbed; }
            set
            {
                _autoScorllEnalbed = value;
                buttonToolStripAutoScroll.Checked = value;
            }
        }

        [Category("KNV")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CustomArbIdColumnCollection CustomArbIdColumns
        {
            set { _arbIdToDataGridView.CustomArbIdColumns = value; }
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
        [DefaultValue(true)]
        public bool IsFullscreen
        {
            get { return toolStripButtonFullscreen.Checked; }
            set { toolStripButtonFullscreen.Checked = value; }
        }

        [Category("KNV")]
        public new ContextMenuStrip ContextMenuStrip {
            get
            {
                return dataGridView1.ContextMenuStrip;
            }
        }

        private readonly Timer _refreshTimer;
        private readonly Stopwatch _watch;
        private readonly CustomArbIdToDataGridView _arbIdToDataGridView;
        private bool _autoScorllEnalbed;
        private readonly object _lockObj;

        /// <summary>
        /// Konstrucktor
        /// </summary>
        public TraceGridView()
        {
            InitializeComponent();
            _refreshTimer = new Timer();
            _refreshTimer.Tick += new EventHandler(RefreshTimer_Tick);
            _watch = new Stopwatch();
            _lockObj = new object();

            columnDirection.ValueType = typeof(MessageDirection);
            columnDirection.DataSource = new BindingSource(CultureText.MessageDirectionDictionary, null);
            columnDirection.ValueMember = "Key";
            columnDirection.DisplayMember = "Value";

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.KnvDoubleBuffered(true);
            _arbIdToDataGridView = new CustomArbIdToDataGridView(dataGridView1)
            {
                ReadOnly = true
            };

            columnType.ValueType = typeof(ArbitrationIdType);
            dataGridView1.KnvColumnHeaderContextMenu();
        }

        /// <summary>
        /// Periodikusan frissti a Grid tartalmát, és ha kell Scroll-oz
        /// </summary>
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            lock (_lockObj)
            {
                Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "Diff Time:" + _watch.ElapsedMilliseconds.ToString("N3"));
                _watch.Restart();

                /*DGV Virtual módban van, listánk hosszát periodikusan tudatom vele.*/
                if (_source != null && _source.Count > 0)
                    dataGridView1.RowCount = _source.Count;

                if (_autoScorllEnalbed && dataGridView1.RowCount > 0)
                {
                    /*Új elem hozzáadásakor az utolsó sorba lép a Data Gridben.. oda teszi az új elemet...*/
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                    dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.RowCount - 1];

                    /*Ha engedélyezve van az autóscroll, akkor teljes soros kijelölést használ*/
                    if (dataGridView1.SelectionMode != DataGridViewSelectionMode.FullRowSelect)
                        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
            }
        }

        /// <summary>
        /// Periodikus frssítés indul.
        /// </summary>
        public void Start()
        {
            _refreshTimer.Start();
            _watch.Start();
        }

        /// <summary>
        /// Periodikus frssítés leáll.
        /// </summary>
        public void Stop()
        {
            _refreshTimer.Stop();
            _watch.Stop();
            dataGridView1.RowCount = _source.Count;
        }

        /// <summary>
        /// Hívja ha a Grid, ha egy cellának adatra van szüksége.
        /// </summary>
        private void dataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            switch (dataGridView1.Columns[e.ColumnIndex].Name)
            {
                case "columnIndex": e.Value = _source[e.RowIndex].Index; break;
                case "columnName": e.Value = _source[e.RowIndex].Name; break;
                case "columnTimestamp": e.Value = _source[e.RowIndex].Timestamp; break;
                case "columnDirection": e.Value = _source[e.RowIndex].Direction; break;
                case "columnType": e.Value = _source[e.RowIndex].Type; break;
                case "columnRemote": e.Value = _source[e.RowIndex].Remote; break;
                case "columnLength": e.Value = _source[e.RowIndex].Length; break;
                case "columnArbitrationId": e.Value = new ArbitrationIdConverter().ConvertTo(null, null, _source[e.RowIndex].ArbitrationId, typeof(string)); break;
                case "columnData": e.Value = new DataFrameConverter().ConvertTo(null,null, _source[e.RowIndex].Data, typeof(string)); break;
                case "columnDocumentation": e.Value = _source[e.RowIndex].Documentation; break;
                case "columnDescription": e.Value = _source[e.RowIndex].Description; break;
            }

            var customizer = dataGridView1.Columns[e.ColumnIndex].Tag as CustomArbIdColumnItem;
            if (customizer != null)
            {
                if (_source[e.RowIndex].Type == customizer.Type)
                {
                    e.Value = customizer.GetValue(_source[e.RowIndex].ArbitrationId).ToString(customizer.Format);
                }
            }
        }

        /// <summary>
        /// Lista forrása változott.
        /// </summary>
        private void SourceListChanged()
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

        /// <summary>
        /// Control betöltődött.
        /// </summary>
        private void TraceViewControl_Load(object sender, EventArgs e)
        {
            UpdateLocalization();    
        }

        /// <summary>
        /// Lokalizáció alkalmazása.
        /// </summary>
        private void UpdateLocalization()
        {
            buttonToolStripClear.Text = CultureService.Instance.GetString(CultureText.menuItem_Clear_Text);
            buttonToolStripClear.ToolTipText = buttonToolStripClear.Text + @" (Ctrl+C)";

            buttonToolStripAutoColumnWidth.Text = CultureService.Instance.GetString(CultureText.menuItem_AutoColumnWidth_Text);
            buttonToolStripAutoScroll.Text = CultureService.Instance.GetString(CultureText.menuItem_AutoScrolling_Text);
            toolStripLabelName.Text = CultureService.Instance.GetString(CultureText.text_TRACE);
            toolStripButtonFullscreen.Text = CultureService.Instance.GetString(CultureText.menuItem_Fullscreen);
            toolStripButtonFullscreen.ToolTipText = toolStripButtonFullscreen.Text + @" (F11)";

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
        /// Minden oszlop autmatikus széles.
        /// </summary>
        public void AutoSizeAll()
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        /// <summary>
        /// Minden oszlop autmatikus széles.
        /// </summary>
        private void buttonToolStripAutoSizeAll_Click(object sender, EventArgs e)
        {
            AutoSizeAll();
        }

        /// <summary>
        /// Tartalom törlése
        /// </summary>
        private void buttonToolStripClear_Click(object sender, EventArgs e)
        {
            if(_source != null)
                _source.Clear();
        }

        /// <summary>
        /// Gridbe Jobb klikkel, akkor leáll az autó scroll.
        /// </summary>
        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            var hitTestInfo = dataGridView1.HitTest(e.X, e.Y);

            if (hitTestInfo.Type != DataGridViewHitTestType.ColumnHeader)
            {
                /*Ha a gridbe kattint akkor leáll az autó scroll*/
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                    if (_autoScorllEnalbed)
                    {
                        _autoScorllEnalbed = false;
                        buttonToolStripAutoScroll.Checked = false;
                    }
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                }
            }
        }

        /// <summary>
        /// Szöveg színezése az üzenet íránya szerint.
        /// </summary>
        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (_source[e.RowIndex].Direction == MessageDirection.Transmitted)
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            else
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Green;
        }

        #region Layout Save & Restore

        /// <summary>
        /// DataGrid alapértelmezett megjelnése.
        /// </summary>
        public void DefaultLayout()
        {
            _arbIdToDataGridView.RemoveAllCustomArbColumns();
            dataGridView1.ShowAllColums();
        }

        #endregion

        /// <summary>
        /// Autmatikus Scrolll
        /// </summary>
        private void buttonToolStripAutoScroll_Click(object sender, EventArgs e)
        {
            _autoScorllEnalbed = buttonToolStripAutoScroll.Checked;
        }

        /// <summary>
        /// Trace teljes képernyőn.
        /// </summary>
        private void toolStripButtonFullscreen_Click(object sender, EventArgs e)
        {
            UpdateScreenState();
        }

        /// <summary>
        /// Hívd és frssíti a képet... 
        /// </summary>
        public void UpdateScreenState()
        {
            if (FullScreenChanged != null)
                FullScreenChanged(this, EventArgs.Empty);
        }
    }
}
