
using System.Diagnostics;

namespace Konvolucio.MCAN120803.GUI.AppModules.Main.Commands
{
    using System;
    using System.Linq;
    using System.Windows.Forms;

    using Properties;
    using Adapters;
    using Model;
    using DataStorage;
    using NUnit.Engine.Services;
    using View;

/*IProjectService*/

    internal sealed class PlayCommand : ToolStripMenuItem
    {
        private readonly IAdapterService _adapter;
        private readonly Storage _storage;

        public PlayCommand(Storage storage, IAdapterService adapter)
        {
            _storage = storage;
            _adapter = adapter;
            Image = Resources.Play_48x48;
            ShortcutKeys = Keys.F5;
            Enabled = true;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            ToolTipText = @"F5";
            EventAggregator.Instance.Subscribe<StopAppEvent>(e => Enabled = true);
            EventAggregator.Instance.Subscribe<PlayAppEvent>(e => Enabled = false);

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
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
            Debug.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");

            if (Enabled)
            {
                if (!_storage.IsSaved)
                {
                    /*Prject még soha nem volt mentve, itt az idő hogy megtegye a felhasználó*/
                    var sps = new ShowingParameters();
                    if (sps.Show())
                    {
                        _storage.Parameters.ProductName = sps.ProudctName;
                        _storage.Parameters.ProductVersion = sps.ProductVersion;
                        _storage.Parameters.ProductCode = sps.ProcutCode;
                        _storage.Parameters.CustomerName = sps.CustomerName;
                        _storage.Parameters.CustomerCode = sps.CustomerCode;
                        _storage.SaveAs(sps.Path);
                        _adapter.Play();
                    }
                }
                else
                {
                    /*Ha nincs vadapater kiválasztva akkor kényszerítjük  felhsználót hogy vállaszon valamit.*/
                    if (!AdapterService.GetAdapters().Contains(_storage.Parameters.DeviceName))
                    {
                        IAdapterSelectForm selector = new SelectAdapterForm();
                        selector.ShowDialog();
                        _storage.Parameters.DeviceName = selector.SelectedAdapter;
                    }
                    _adapter.Play();

                   
                    
                }
            }
        }
    }
}
