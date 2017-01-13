
namespace Konvolucio.MCAN120803.GUI.AppModules.Filter.Commands
{
    using System.ComponentModel;
    using System.Windows.Forms;/*Keys, ToolStripMenuItem*/

    using Properties;
    using Services; /*Culture*/
    using WinForms.Framework;

    internal sealed class DeleteRowsCommand : KnvDataGridViewDeleteRowsBaseCommand
    {
        public DeleteRowsCommand(DataGridView dataGridView, IBindingList collection): base(dataGridView, collection)
        {
            Image = Resources.Email_Delete24;
            Text = CultureService.Instance.GetString(CultureText.menuItem_Delete_Text);
            ShortcutKeys = Keys.Delete;
        }
    }
}
