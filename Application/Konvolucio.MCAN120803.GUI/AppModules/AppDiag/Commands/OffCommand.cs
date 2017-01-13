
namespace Konvolucio.MCAN120803.GUI.AppModules.AppDiag.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.Resources;
    using WinForms.Framework;

    using Properties;
    using Services; /*Culutre*/
    using View;

    internal sealed class OffCommand : ToolStripMenuItem
    {
        readonly IAppDiagView _diagnosticsView;

        public OffCommand(IAppDiagView diagnosticsView)
        {
            Image = Resources.WindowsTurnOff32x32;
            Text = CultureService.Instance.GetString(CultureText.menuItem_Off_Text);
            _diagnosticsView = diagnosticsView;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Settings.Default.IsDeveloperMode = false;
        }
    }
}
