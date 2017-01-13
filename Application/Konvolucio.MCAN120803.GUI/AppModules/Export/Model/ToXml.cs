// -----------------------------------------------------------------------
// <copyright file="ExportToCsv.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Export.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ToXml:IExporter
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public event RunWorkerCompletedEventHandler Completed;
        public event ProgressChangedEventHandler ProgressChanged;
        public string Name {
            get { return "Extensible Markup Language"; }
        }
        public string FileFilter {
            get { return "Extensible Markup Language (.xml)|*.xml"; }
        }
        public string Path { get; set; }
        public string FileExtension {
            get { return ".xml"; }
        }
        public object Options { get; set; }
        public IExportTable DataSource { get; set; }
        public Exception LastException { get; private set; }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
