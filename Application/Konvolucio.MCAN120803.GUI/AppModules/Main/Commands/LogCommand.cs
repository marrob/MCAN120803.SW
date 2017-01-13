
namespace Konvolucio.MCAN120803.GUI.AppModules.Main.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms; /*ToolStripMenuItem*/

    using Properties;
    using WinForms.Framework;

    internal sealed class LogCommand : ToolStripMenuItem
    {
        private readonly IApp app;

        public LogCommand(IApp app)
        {
            this.app = app;
            Image = Resources.log48;
            Enabled = true;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            ToolTipText = @"F8";
            ShortcutKeys = Keys.F8;
            Checked = false;
            EventAggregator.Instance.Subscribe<PlayAppEvent>(n => Enabled = false);
            EventAggregator.Instance.Subscribe<StopAppEvent>(n => Enabled = true);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (Enabled)
            {
               app.ShowLogInMainView();
            }
        }
    }
}
