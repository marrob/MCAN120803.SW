// -----------------------------------------------------------------------
// <copyright file="ToCsv.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Export.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class ToCsv : IExporter
    {
        /// <inheritdoc />
        public event RunWorkerCompletedEventHandler Completed
        {
            remove { _worker.RunWorkerCompleted -= value; }
            add { _worker.RunWorkerCompleted += value; }
        }
        /// <inheritdoc />
        public event ProgressChangedEventHandler ProgressChanged
        {
            add { _worker.ProgressChanged += value; }
            remove { _worker.ProgressChanged -= value; }
        }
        /// <inheritdoc />
        public object Options { get; set; }
        /// <inheritdoc />
        public IExportTable DataSource { get; set; }
        /// <inheritdoc />
        public Exception LastException { get { return _lastException; } }
        /// <inheritdoc />
        public string Name { get { return "Comma-separated values"; } }
        /// <inheritdoc />
        public string FileFilter { get { return "Comma-separated values(.csv)|*.csv"; } }
        /// <inheritdoc />
        public string FileExtension { get { return ".csv"; } }
        /// <inheritdoc />
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        private readonly BackgroundWorker _worker;
        private AutoResetEvent _readyToDisposeEvent;
        private bool _disposed;
        private Exception _lastException;
        private string _path;
   
        /// <summary>
        /// Konstructor
        /// </summary>
        public ToCsv()
        {
            _worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _worker.DoWork += new DoWorkEventHandler(DoWork);
        }

        /// <summary>
        ///  <inheritdoc />
        /// </summary>
        public void Start()
        {
            _lastException = null;
            _readyToDisposeEvent = new AutoResetEvent(false);
            _worker.RunWorkerAsync(Options);
        }

        /// <summary>
        ///  <inheritdoc />
        /// </summary>
        public void Stop()
        {
            if (_worker.IsBusy)
            {
                _worker.CancelAsync();

                Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");

                if (_readyToDisposeEvent.WaitOne(5000))
                {
                    Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): Stop is ready.");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
            var lines = new List<string>();
            var line = string.Empty;
            var current = 0;
            var watch = new Stopwatch();
            
            watch.Start();
            try
            {
                var opt = (CsvOptions) e.Argument;
                var delimiter = string.Empty;

                if (opt.Delimiter == @"\t")
                    delimiter = "\t";
                else
                    delimiter = opt.Delimiter;
                /*
                 * Display Index alapján sorba rendezem az oszlopokat és így mentem, mert ezek tárolja a látható oszlop srorrendet.
                 * A forech már a DisplayIdex alapján halad végig listán.
                 */
                var sortedColumns = ((ExportColumnCollection) DataSource.Columns).OrderBy(n => n.DisplayIndex);

                /*A Cellák megjelnítési sorrendjét tartalmazza.*/
                var sortedDisplayIndexes = new int[DataSource.Columns.Count];

                int index = 0;
                foreach (var columnItem in sortedColumns)
                {
                       
                    if (columnItem.Visible && opt.ColumnNameInFirstRow)
                        line += opt.Escape + columnItem.HeaderText + opt.Escape + delimiter;
                    if (_worker.CancellationPending) break;

                    /*Ez alapján lépkedek a Cellákon.*/
                    sortedDisplayIndexes[index++] = ((ExportColumnCollection)DataSource.Columns).IndexOf(columnItem);
                }

                if (opt.ColumnNameInFirstRow)
                    lines.Add(line);

                for (var rowIndex = 0;  rowIndex < DataSource.Rows.Count; rowIndex++)
                {
                    line = string.Empty;
                    for (var columnIndex = 0; columnIndex < DataSource.Columns.Count; columnIndex++)
                    {
                        if (DataSource.Columns[sortedDisplayIndexes[columnIndex]].Visible)
                            line += opt.Escape + DataSource.Rows[rowIndex].Cells[sortedDisplayIndexes[columnIndex]].Value + opt.Escape + delimiter;
                    }

                    lines.Add(line);

                    if (current % 20 == 0)
                    {
                        var percent = (int)((current / (double)DataSource.Rows.Count) * 100);
                        _worker.ReportProgress(percent, "Creating... " + percent.ToString() + "%/" + (watch.ElapsedMilliseconds / 1000.0).ToString("N3") + "s");
                    }
                    current++;

                    if (_worker.CancellationPending) break;
                }

                var fileWrite = new StreamWriter(_path, true, Encoding.ASCII);
                switch(opt.NewLine)
                {
                    case @"\r\n": fileWrite.NewLine = "\r\n"; break;
                    case @"\r": fileWrite.NewLine = "\r"; break;
                    case @"\n": fileWrite.NewLine = "\n"; break;
                }

                current = 0;
                foreach (var l in lines)
                {
                    fileWrite.WriteLine(l);
                    var percent = (int)((current / (double)DataSource.Rows.Count) * 100);
                    _worker.ReportProgress(percent, "Creating... " + percent.ToString() + "%/" + (watch.ElapsedMilliseconds / 1000.0).ToString("N3") + "s");
                    current++;
                    if (_worker.CancellationPending) break;
                }
                fileWrite.Flush();
                fileWrite.Close();

                if (!_worker.CancellationPending) 
                    _worker.ReportProgress(100, "COMPLETE Elapsed:" + (watch.ElapsedMilliseconds / 1000.0).ToString("N3") + "s");

            }
            catch (Exception ex)
            {
                _worker.ReportProgress(100, "COMPLETE Elapsed:" + (watch.ElapsedMilliseconds / 1000.0).ToString("N3") + "s");
                _lastException = ex;
            }
            finally 
            {
                if (_worker.CancellationPending)
                    e.Cancel = true;
                _readyToDisposeEvent.Set();
            }
        }

        /// <summary>
        ///  Public implementation of Dispose pattern callable by consumers. 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern. 
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                if (_worker.IsBusy)
                {
                    /*szabályos leállítás*/
                    Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): Running-> Stop()");
                    Stop();
                }

                if (_readyToDisposeEvent != null)
                {
                    _readyToDisposeEvent.Dispose();
                    _readyToDisposeEvent = null;
                }
            }
            _disposed = true;
        }
    }
}
