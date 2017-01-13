
namespace Konvolucio.MCAN120803.GUI.AppModules.Main.Commands
{
    using System;
    using System.Windows.Forms;
    using Model;
    
    using Properties;
    using DataStorage;

    internal sealed class SaveAsCommand : ToolStripMenuItem
    {
        private readonly Storage _storage;

        public SaveAsCommand(Storage storage)
        {
            _storage = storage;
            Image = Resources.SaveAs48;
            DisplayStyle = ToolStripItemDisplayStyle.Image;
            EventAggregator.Instance.Subscribe<StopAppEvent>(e => Enabled = true);
            EventAggregator.Instance.Subscribe<PlayAppEvent>(e => Enabled = false);

            /*Amíg fájlmüvelet van addíg nem elérhető*/
            EventAggregator.Instance.Subscribe<StorageAppEvent>( e => 
            {
                if (e.ChangingType == FileChangingType.Loading || e.ChangingType == FileChangingType.Saving)
                    Enabled = false;

                if (e.ChangingType == FileChangingType.LoadComplete || e.ChangingType == FileChangingType.SaveComplete)
                    Enabled = true;
            });
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            var saps = new ShowingParameters
            {
                ProudctName = _storage.Parameters.ProductName,
                ProductVersion = _storage.Parameters.ProductVersion,
                ProcutCode = _storage.Parameters.ProductCode,
                CustomerName = _storage.Parameters.CustomerName,
                CustomerCode = _storage.Parameters.CustomerCode,
            };
            if (saps.Show())
            {
                _storage.Parameters.ProductName = saps.ProudctName;
                _storage.Parameters.ProductVersion = saps.ProductVersion;
                _storage.Parameters.ProductCode = saps.ProcutCode;
                _storage.Parameters.CustomerName = saps.CustomerName;
                _storage.Parameters.CustomerCode = saps.CustomerCode;
                _storage.SaveAs(saps.Path);
            }
        }
    }
}
