
namespace Konvolucio.MCAN120803.GUI.AppModules.CanTx.Commands
{
    using System;
    using System.Windows.Forms;
    using DataStorage;
    using Export.Model;
    using Export.View;
    using Properties;
    using Services;

    internal sealed class ExportCommand : ToolStripMenuItem
    {
        readonly Storage _storage;
        private readonly IExportForm _exportForm;
        private readonly IExportableTableObject _table;

        public ExportCommand(Storage storage, IExportableTableObject table)
        {
            Image = Resources.shopping_cart16x16;
            Text = CultureService.Instance.GetString(CultureText.menuItem_Export_Text);

            _storage = storage;
            _table = table;
            _exportForm = new ExportForm();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (Enabled)
            {
                _exportForm.ExportTableSource = _table.ExportTable;
                _exportForm.Directory = "Exports for " + _storage.FileName;
                _exportForm.FileName =  _storage.FileName + " " + DateTime.Now.ToString(AppConstants.FileNameTimestampFormat);
                _exportForm.Title = "Export for Sender Table";
                _exportForm.ShowDialog();
            }
        }
    }
}
