
namespace Konvolucio.MCAN120803.GUI.AppModules.AppDiag.Commands
{
    using System;
    using System.Windows.Forms;

    using Properties;
    using Services; /*Culutre*/
    using View;

    internal sealed class ClearCommand : ToolStripMenuItem
    {
        readonly IAppDiagView _diagnosticsView;

        public ClearCommand(IAppDiagView diagnosticsView)
        {
            Image = Resources.Delete32x32;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            Size = new System.Drawing.Size(50, 50);
            Text = CultureService.Instance.GetString(CultureText.menuItem_Clear_Text);
            _diagnosticsView = diagnosticsView;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _diagnosticsView.Clear();
        }
    }
}
