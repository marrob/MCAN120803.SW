

namespace Konvolucio.MCAN120803.GUI.AppModules.Export.View
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows.Forms;
    using Model;
    using System.IO;


    /// <inheritdoc />
    public partial class ExportForm : Form, IExportForm
    {
        /// <inheritdoc />
        public IExportTable ExportTableSource { get; set; }

        /// <inheritdoc />
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        /// <inheritdoc />
        public string Directory
        {
            get { return _directory; }
            set { _directory = value; }
        }

        /// <inheritdoc />
        public string Title
        {
            get { return Text; }
            set { Text = value; }
        }


        private IExporter _exporter;
        private readonly ICsvOptionsView _csvOptionsView = new CsvOptionsView() {Dock = DockStyle.Fill,};
        private readonly IXlsxOptionsView _xlsxOptionsView = new XlsxOptionsView() { Dock = DockStyle.Fill, };
        private readonly IXmlOptionsView _xmlOptionsView = new XmlOptionsView() {Dock = DockStyle.Fill,};
        private string _fileName;
        private string _directory;
        private readonly SaveFileDialog _saveFileDialog = new SaveFileDialog();


        /// <summary>
        /// Konstruktor
        /// </summary>
        public ExportForm()
        {

            InitializeComponent();
            Cursor.Current = Cursors.WaitCursor;
            var exporters = new ExporterCollection();
            comboBoxExportFormat.DataSource = exporters;
            comboBoxExportFormat.DisplayMember = "Name";
            exporters.Completed += new RunWorkerCompletedEventHandler(Exporters_Completed);
            exporters.ProgressChanged += new ProgressChangedEventHandler(Exporters_ProgressChanged);
            knvDataGridView1.ColumnStateChanged += KnvDataGridView1OnColumnStateChanged;
            Disposed += (o, e) =>
            {
                if (_exporter != null) _exporter.Dispose();
            };
        }

        /// <summary>
        /// 
        /// </summary>
        private void ExportForm_Shown(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            
            Show();

            TableToDataGridView(ExportTableSource, knvDataGridView1);
            UpdateCheckboxList(knvDataGridView1);

            knvDataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

            textBoxPath.Text = Environment.CurrentDirectory + "\\" + _directory + "\\" + _fileName + _exporter.FileExtension;
            _saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
            _saveFileDialog.Filter = _exporter.FileFilter;
            _saveFileDialog.FileName = _fileName;

            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// ExportTable konvertálása DataGrid-be
        /// </summary>
        private static void TableToDataGridView(IExportTable srcTable, DataGridView destDataGridView)
        {
            destDataGridView.Rows.Clear();
            destDataGridView.Columns.Clear();

            foreach (var tableColumn in srcTable.Columns)
            {
                DataGridViewTextBoxColumn dgvColumn;
                destDataGridView.Columns.Add(dgvColumn = new DataGridViewTextBoxColumn()
                {
                    Name = tableColumn.Name,
                    HeaderText = tableColumn.HeaderText,
                    DisplayIndex = tableColumn.DisplayIndex,
                    Visible = tableColumn.Visible,
                });

                if (srcTable.SourceIsSupportColumnWidth)
                    dgvColumn.Width = tableColumn.Width;
            }

            destDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            for (var rowIndex = 0; rowIndex < srcTable.Rows.Count; rowIndex++)
            {
                destDataGridView.Rows.Add(new DataGridViewRow());
                /*Sormagasság*/
                /*TODO: Túl hosszú müvelet a sor magasság állítása, letitlva.*/
                /*destDataGridView.Rows[rowIndex].Height = 18;*/
                for (var columnIndex = 0; columnIndex < srcTable.Columns.Count; columnIndex++)
                    destDataGridView.Rows[rowIndex].Cells[columnIndex].Value = srcTable.Rows[rowIndex].Cells[columnIndex].Value;
            }
        }

        /// <summary>
        /// DGV-t konvertálja Export Táblába.
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        private static IExportTable DataGridViewToTable(DataGridView dgv)
        {
            var table = new ExportTable();
            /*Minding támogatja az oszlopszélességeket.*/
            table.SourceIsSupportColumnWidth = true;
            foreach (DataGridViewColumn dgvColumn in dgv.Columns)
            {
                var exportColumn = new ExportColumnItem()
                {
                    Name = dgvColumn.Name,
                    HeaderText = dgvColumn.HeaderText,
                    DisplayIndex = dgvColumn.DisplayIndex,
                    Visible = dgvColumn.Visible,
                    Width = dgvColumn.Width,
                };
                table.Columns.Add(exportColumn);
            }

            for (var row = 0; row < dgv.Rows.Count; row++)
            {
                table.Rows.Add(new ExportRowItem());
                for (var column = 0; column < dgv.ColumnCount; column++)
                    table.Rows[row].Cells[column].Value = dgv.Rows[row].Cells[column].Value.ToString();
            }

            return table;
        }

        /// <summary>
        /// Kiválasztott exportálóhoz megjelniti a megfelelő Options Panelt.
        /// </summary>
        private void ComboBoxExportFormat_SelectedIndexChanged(object sender, EventArgs e)
        {

            _exporter = (IExporter) comboBoxExportFormat.SelectedItem;

            if (_exporter is ToCsv)
            {
                if (!PanelOptions.Controls.Contains((Control) _csvOptionsView))
                {
                    PanelOptions.Controls.Clear();
                    PanelOptions.Controls.Add((Control) _csvOptionsView);
                }
            }
            else if (_exporter is ToXlsx)
            {
                if (!PanelOptions.Controls.Contains((Control)_xlsxOptionsView))
                {
                    PanelOptions.Controls.Clear();
                    PanelOptions.Controls.Add((Control)_xlsxOptionsView);
                }
            }
            else if (_exporter is ToXml)
            {
                if (!PanelOptions.Controls.Contains((Control)_xmlOptionsView))
                {
                    PanelOptions.Controls.Clear();
                    PanelOptions.Controls.Add((Control)_xmlOptionsView);
                }
            }

            /*kiterjeszés változása miatt itt is frissíteni kell.*/
            textBoxPath.Text = Environment.CurrentDirectory + "\\" + _directory + "\\" + _fileName + _exporter.FileExtension;
            _saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
            _saveFileDialog.Filter = _exporter.FileFilter;
            _saveFileDialog.FileName = _fileName;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Exporters_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            labelProgressStatus.Text = e.UserState.ToString();
        }

        /// <summary>
        /// Exprtálás kész.
        /// </summary>
        private void Exporters_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_exporter.LastException != null)
                MessageBox.Show(_exporter.LastException.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            progressBar1.Visible = false;
            buttonAbort.Visible = false;
            labelProgressStatus.Visible = false;

            try
            {
                if (checkBoxOpenExported.Checked && !e.Cancelled)
                {
                    var myProcess = new Process();
                    myProcess.StartInfo.FileName = "Excel";
                    myProcess.StartInfo.Arguments = "\"" + _exporter.Path + "\"";
                    myProcess.Start();
                }

                if (checkBoxOpenFolder.Checked && !e.Cancelled)
                {
                    Process.Start("explorer.exe", Path.GetDirectoryName(_exporter.Path));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                /*
                 * Ha már nem látszódik a from, akkor Colse() bezárja az 
                 * FormCloseing esemény nélkül ami meg akadályozná az ablak teljes bezárást */
                if (Visible)
                    Close();
            }
        }

        /// <summary>
        /// Exportálás kezdőidk.
        /// </summary>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(Path.GetDirectoryName(textBoxPath.Text)) == false)
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(path: textBoxPath.Text));

            progressBar1.Visible = true;
            buttonAbort.Visible = true;
            labelProgressStatus.Visible = true;


            if (_exporter is ToCsv)
            {
                _exporter.Options = new CsvOptions()
                {
                    ColumnNameInFirstRow = _csvOptionsView.ColumnNameInFirstRow,
                    Delimiter = _csvOptionsView.Delimiter,
                    Escape = _csvOptionsView.Escape,
                    NewLine = _csvOptionsView.NewLine,
                };
            }
            else if (_exporter is ToXlsx)
            {
                _exporter.Options = new XlsxOptions()
                {
                    ColumnNameInFirstRow = _xlsxOptionsView.ColumnNameInFirstRow,
                    ApplyTheFormattingParameters =  _xlsxOptionsView.ApplyTheFormattingParameters,
                };
            }

            _exporter.Path = textBoxPath.Text;
            _exporter.DataSource = DataGridViewToTable(knvDataGridView1);
            _exporter.Start();
        }

        /// <summary>
        /// Tallózás
        /// </summary>
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if(_saveFileDialog.ShowDialog() == DialogResult.OK)
                textBoxPath.Text = _saveFileDialog.FileName;
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateCheckboxList(DataGridView dgv)
        {
            checkedListBox1.Items.Clear();
            foreach (DataGridViewColumn column in dgv.Columns)
                checkedListBox1.Items.Add(column.HeaderText, column.Visible);
        }

        /// <summary>
        /// 
        /// </summary>
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (checkedListBox1.SelectedIndex != -1)
                knvDataGridView1.Columns[checkedListBox1.SelectedIndex].Visible =
                    !knvDataGridView1.Columns[checkedListBox1.SelectedIndex].Visible;

            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// 
        /// </summary>
        private void KnvDataGridView1OnColumnStateChanged(object sender, EventArgs e)
        {
            UpdateCheckboxList(knvDataGridView1);
        }


        /// <summary>
        /// Ez az ablak olyan, hogy soha nem záródik be, csak azért, hogy a paramétereket ne veszítse el.
        /// 
        /// How to prevent a form object from disposing on close?
        /// http://stackoverflow.com/questions/6060471/how-to-prevent-a-form-object-from-disposing-on-close
        /// </summary>
        private void ExportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true; 
            _exporter.Stop();
        }

        /// <summary>
        /// Felhasználó megszakította az Exportálást.
        /// </summary>
        private void buttonAbort_Click(object sender, EventArgs e)
        {
            _exporter.Stop();
        }
    }
}
