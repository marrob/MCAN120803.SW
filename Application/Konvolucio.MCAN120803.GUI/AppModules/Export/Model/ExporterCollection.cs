// -----------------------------------------------------------------------
// <copyright file="ExportTypes.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Export.Model
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;



    /// <summary>
    /// Ezportálókat összefogó lista.
    /// </summary>
    public class ExporterCollection : Collection<IExporter>
    {
        public event RunWorkerCompletedEventHandler Completed;
        public event ProgressChangedEventHandler ProgressChanged;

        public ExporterCollection()
        {
            Add(new ToCsv());
            Add(new ToXlsx());
          //  Add(new ToXml());
        }

        protected override void InsertItem(int index, IExporter item)
        {
            item.Completed += OnClompleted;
            item.ProgressChanged += (o, e) => OnProgressChanged(o, e);

            base.InsertItem(index, item);
        }

        protected void OnClompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Completed != null)
                Completed(sender, e);
        }

        protected void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (ProgressChanged != null)
                ProgressChanged(sender, e);
        }
    }
}
