
namespace Konvolucio.MCAN120803.GUI.AppModules.Main.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms; /*ToolStripMenuItem*/

    using Properties;
    using WinForms.Framework;

    internal sealed class HamburgerCommand : ToolStripMenuItem
    {
        private readonly IApp _app;

        public HamburgerCommand(IApp app)
        {
            _app = app;
            Image = Resources.HamburgerMenu48;
            // ShortcutKey = Keys.F1;
            Enabled = true;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            // ToolTip = "F1";
            EventAggregator.Instance.Subscribe<StopAppEvent>(e => Enabled = true);
            EventAggregator.Instance.Subscribe<PlayAppEvent>(e => Enabled = false);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (Enabled)
            {
                _app.ShowWorksapce();
            }
        }
    }
}
