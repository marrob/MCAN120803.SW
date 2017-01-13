namespace Konvolucio.MCAN120803.GUI.AppModules.Main.Commands
{
    using System;
    using System.Windows.Forms;
    
    using Properties;
    using WinForms.Framework;
    using Adapters;
    using DataStorage;
    using View;

    /// <summary>
    /// 
    /// </summary>
    internal sealed class NewCommand : ToolStripMenuItem
    {
        private readonly Storage _storage;
        private readonly IAdapterService _adapter;

        public NewCommand(Storage storage, IAdapterService adapter)
        {
            _storage = storage;
            _adapter = adapter;
            Image = Resources.New_48x48;
            DisplayStyle = ToolStripItemDisplayStyle.Image;
            ToolTipText = @"Ctrl + N";
            ShortcutKeys = Keys.Control | Keys.N;
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

            if (Enabled)
            {
                if (_storage.IsChanged)
                {
                    var userAction = new SaveMessageBox().Show(_storage.FileName + AppConstants.FileExtension);
                    if (userAction == UserAction.Yes)
                    {
                        new SaveCommand(_storage).PerformClick();
                        _storage.New(_adapter.GetDefaultDeviceName, _adapter.GetDefaultBaudrate);
                    }
                    if (userAction == UserAction.No)
                    {
                        _storage.New(_adapter.GetDefaultDeviceName, _adapter.GetDefaultBaudrate);
                    }
                }
                else
                {
                    _storage.New(_adapter.GetDefaultDeviceName, _adapter.GetDefaultBaudrate);
                }
            }
        }
    }
}
