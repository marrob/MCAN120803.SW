
namespace Konvolucio.MCAN120803.GUI.AppModules.Main.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms; /*ToolStripMenuItem*/
    
    using Properties;
    using WinForms.Framework; /*Toolbar*/
    using AppModules.Options; /**/

    internal sealed class OptionsCommand : ToolStripMenuItem
    {
        readonly OptionsForm _optionForm;

        public OptionsCommand()
        {
            Image = Resources.Settings_48x48;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            EventAggregator.Instance.Subscribe<StopAppEvent>(e => Enabled = true);
            EventAggregator.Instance.Subscribe<PlayAppEvent>(e => Enabled = false);
            _optionForm = new OptionsForm();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _optionForm.ShowDialog();
        }
    }
}
