
namespace Konvolucio.MCAN120803.GUI.AppModules.CanTx.Commands
{
    using System.ComponentModel;
    using System.Windows.Forms;
    using WinForms.Framework;

    internal sealed class DeleteCommand : KnvDataGridViewDeleteRowsBaseCommand
    {
        public DeleteCommand(DataGridView gridView, IBindingList collection) : base(gridView,collection)
        {

        }
    }
}
