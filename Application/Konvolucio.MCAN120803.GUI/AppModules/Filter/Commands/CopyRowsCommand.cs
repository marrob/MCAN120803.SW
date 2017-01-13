// -----------------------------------------------------------------------
// <copyright file="CopyCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Filter.Commands
{
    using System.ComponentModel;
    using System.Windows.Forms;
    using WinForms.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class CopyRowsCommand: KnvDataGridViewCopyRowsBaseCommand
    {
        public CopyRowsCommand(DataGridView dataGridView, IBindingList collection) : base(dataGridView, collection)
        {
        }
    }
}
