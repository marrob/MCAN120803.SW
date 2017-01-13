
namespace Konvolucio.MCAN120803.GUI.AppModules.Tracer.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms; /*ToolStripMenuItem*/

    using Properties;
    using WinForms.Framework;
    using Services; /*Culutre*/
    using Model;

    internal sealed class ClearTraceCommand : ToolStripMenuItem
    {
        private readonly MessageTraceCollection _collection;

        public ClearTraceCommand(MessageTraceCollection collection)
        {
            _collection = collection;
            Image = Resources.Delete32x32;
            Text = CultureService.Instance.GetString(CultureText.menuItem_Clear_Text);
            ShortcutKeys = Keys.Alt | Keys.C;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _collection.Clear();
        }
    }
}
