
using System.Diagnostics;

namespace Konvolucio.MCAN120803.GUI.AppModules.Main.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms; /*ToolStripMenuItem*/
    
    using Properties;
    using WinForms.Framework;
    using AppModules.Adapters;

    internal sealed class StopCommand : ToolStripMenuItem
    {
        private readonly IAdapterService _adapter;

        public StopCommand(IAdapterService adapter)
        {
            _adapter = adapter;
            Image = Resources.Stop_48x48;
            Enabled = false;
            DisplayStyle = ToolStripItemDisplayStyle.Image;
            ToolTipText = @"F6";
            ShortcutKeys = Keys.F6;
            EventAggregator.Instance.Subscribe<PlayAppEvent>(n => Enabled = true);
            EventAggregator.Instance.Subscribe<StopAppEvent>(n => Enabled = false);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Debug.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            if (Enabled)
            {
                _adapter.Stop();
                
            }
        }
    }
}
