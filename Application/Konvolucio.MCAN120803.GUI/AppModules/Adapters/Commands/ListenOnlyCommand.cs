
namespace Konvolucio.MCAN120803.GUI.AppModules.Adapters.Commands
{
    using System;
    using System.Windows.Forms; 
    
    using Properties;
    using WinForms.Framework;
    using Services; 
    using Common;
    using DataStorage;

    internal sealed class ListenOnlyCommand : ToolStripButton
    {
        ProjectParameters _parmeters = null;

        public ListenOnlyCommand()
        {
            if (DesignMode)
                Text = "#LISTEN ONLY";
            else
                Text = CultureService.Instance.GetString(CultureText.menuItem_ListenOnly_Text);
            ToolTipText = CultureService.Instance.GetString(CultureText.menuItem_ListenOnly_ToolTip);
            Image = Resources.Listen16;
            CheckOnClick = true;
            Checked = false;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                if (e.ChangingType == FileChangingType.LoadComplete)
                {
                    _parmeters = e.Storage.Parameters;
                    Checked = e.Storage.Parameters.ListenOnly;
                }
            });

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                if (e.ChangingType == FileChangingType.ContentChanged)
                {
                    if (e.Details.DataObjects == DataObjects.ParameterProperty &&
                        e.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e.Storage.Parameters.ListenOnly))
                    {
                        Checked = e.Storage.Parameters.ListenOnly;
                    }
                }
            });


            EventAggregator.Instance.Subscribe<StopAppEvent>(e => Enabled = true);
            EventAggregator.Instance.Subscribe<PlayAppEvent>(e => Enabled = false);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _parmeters.ListenOnly = Checked;
        }
    }
}
