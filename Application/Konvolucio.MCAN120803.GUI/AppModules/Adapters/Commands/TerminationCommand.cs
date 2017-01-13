
namespace Konvolucio.MCAN120803.GUI.AppModules.Adapters.Commands
{
    using System;
    using System.Windows.Forms;

    using WinForms.Framework;
    using Properties;
    using Services;
    using Common;
    using DataStorage;



    internal sealed class TerminationCommand : ToolStripButton
    {
        ProjectParameters _parmeters = null;

        public TerminationCommand()
        {
            Text = CultureService.Instance.GetString(CultureText.menuItem_Termination_Text);
            ToolTipText = CultureService.Instance.GetString(CultureText.menuItem_Termination_ToolTip);
            Image = Resources.Resistor16;
            CheckOnClick = true;
            Checked = false;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                if (e.ChangingType == FileChangingType.LoadComplete)
                {
                    _parmeters = e.Storage.Parameters;
                    Checked = e.Storage.Parameters.Termination;
                }
            });

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                if (e.ChangingType == FileChangingType.ContentChanged)
                {
                    if (e.Details.DataObjects == DataObjects.ParameterProperty &&
                        e.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e.Storage.Parameters.Termination))
                    {
                        Checked = e.Storage.Parameters.Termination;
                    }
                }
            });

            EventAggregator.Instance.Subscribe<StopAppEvent>(e => Enabled = true);
            EventAggregator.Instance.Subscribe<PlayAppEvent>(e => Enabled = false);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _parmeters.Termination = Checked;
        }
    }
}
