
namespace Konvolucio.MCAN120803.GUI.AppModules.AppDiag.Commands
{
    using System;
    using System.Windows.Forms;

    using Properties;
    using Services; /*Culutre*/
    using View;

    internal sealed class SaveCommand : ToolStripMenuItem
    {
        readonly IAppDiagView DiagnosticsView;

        public SaveCommand(IAppDiagView diagnosticsView)
        {
            Image = Resources.Save_32x32;
            Text = CultureService.Instance.GetString(CultureText.menuItem_Save_Text);
            DiagnosticsView = diagnosticsView;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            /*TODO Felugró ablakos legyen?*/
            throw new NotImplementedException("felugró balkos legyen...?");
        }
    }
}
