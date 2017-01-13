

namespace Konvolucio.MCAN120803.GUI.AppModules.Main.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms; /*ToolStripMenuItem*/
    
    using Properties;
    using WinForms.Framework;

    internal sealed class BackCommand : ToolStripMenuItem
    {
        private readonly IApp _app;

        public BackCommand(IApp app)
        {
            this._app = app;
            Image = Resources.Previous_48x48;
            // ShortcutKey = Keys.F1;
            Enabled = true;
            DisplayStyle = ToolStripItemDisplayStyle.Image;
            // ToolTip = "F1";
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            
            if (Enabled)
            {
                _app.ShowTraceInMainView();
            }
        }
    }
}
