
namespace Konvolucio.MCAN120803.GUI.AppModules.Main.Commands
{
    using System;
    using System.Windows.Forms;
    
    using Model;
    using Properties;
    using DataStorage;            

    internal sealed class SaveCommand : ToolStripMenuItem
    {
        Storage _storage;

        public SaveCommand(Storage storage)
        {
            _storage = storage;
            Image = Resources.Save_48x48;
            DisplayStyle = ToolStripItemDisplayStyle.Image;
            ToolTipText = @"Ctrl + S";
            ShortcutKeys = Keys.Control | Keys.S;
            /* 20160802 Ver 2.0.25.28-tól engedélyezve, de vissza vonva*/
            /*TODO MessageSender panel miatt futás időben is lehessen menteni.*/
            EventAggregator.Instance.Subscribe<StopAppEvent>(e => Enabled = true);
            EventAggregator.Instance.Subscribe<PlayAppEvent>(e => Enabled = false);

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                /*TODO egyszer majd ezt is ki lehetne próbálni...*/
                //if (e.ChangingType == FileChangingType.LoadComplete)
                //    _project = e.Project;

                if (e.ChangingType == FileChangingType.Loading || e.ChangingType == FileChangingType.Saving)
                    Enabled = false;

                if (e.ChangingType == FileChangingType.LoadComplete || e.ChangingType == FileChangingType.SaveComplete)
                    Enabled = true;
            });
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (Enabled)
            {
                if (!_storage.IsSaved)
                {
                    var sps = new ShowingParameters();
                    if (!sps.Show()) return;
                    _storage.Parameters.ProductName = sps.ProudctName;
                    _storage.Parameters.ProductVersion = sps.ProductVersion;
                    _storage.Parameters.ProductCode = sps.ProcutCode;
                    _storage.Parameters.CustomerName = sps.CustomerName;
                    _storage.Parameters.CustomerCode = sps.CustomerCode;
                    _storage.SaveAs(sps.Path);
                }
                else
                {
                    _storage.Save();
                }
            }
        }
    }
}
