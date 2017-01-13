// -----------------------------------------------------------------------
// <copyright file="AddMessageFilter.cs" company="">
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
    internal sealed class CutRowsCommand : KnvDataGridViewCutRowsBaseCommand
    {
        public CutRowsCommand(DataGridView dataGridView, IBindingList collection)  : base(dataGridView, collection)
        {
        }
    }
}
