namespace Konvolucio.MCAN120803.GUI.AppModules.Adapters.Commands
{
    using System;
    using System.Windows.Forms;

    using Properties;
    using WinForms.Framework;
    using Services;
    using Common;
    using DataStorage;

    internal sealed class LoopbackCommand : ToolStripButton
    {
        ProjectParameters _parmeters = null;

        public LoopbackCommand()
        {
            Text = CultureService.Instance.GetString(CultureText.menuItem_Loopback_Text);
            ToolTipText = CultureService.Instance.GetString(CultureText.menuItem_Loopback_ToolTip);
            Image = Resources.Loop16;
            CheckOnClick = true;
            Checked = false;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                if (e.ChangingType == FileChangingType.LoadComplete)
                {
                    _parmeters = e.Storage.Parameters;
                    Checked = e.Storage.Parameters.Loopback;
                }
            });

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                if (e.ChangingType == FileChangingType.ContentChanged)
                {
                    if (e.Details.DataObjects == DataObjects.ParameterProperty &&
                        e.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e.Storage.Parameters.Loopback))
                    {
                        Checked = e.Storage.Parameters.Loopback;
                    }
                }
            });


            EventAggregator.Instance.Subscribe<StopAppEvent>(e => Enabled = true);
            EventAggregator.Instance.Subscribe<PlayAppEvent>(e => Enabled = false);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _parmeters.Loopback = Checked;
        }
    }
}
