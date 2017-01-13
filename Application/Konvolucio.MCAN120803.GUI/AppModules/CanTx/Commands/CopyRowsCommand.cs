// -----------------------------------------------------------------------
// <copyright file="CopyCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.CanTx.Commands
{
    using System.ComponentModel;
    using System.Windows.Forms;
    using WinForms.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class CopyRowsCommand : KnvDataGridViewCopyRowsBaseCommand
    {
        public CopyRowsCommand(DataGridView gridView, IBindingList collection):base(gridView, collection)
        {

        }
    }
}
