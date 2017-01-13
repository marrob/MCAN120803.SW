// -----------------------------------------------------------------------
// <copyright file="AddMessageFilter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework.DataGridViewControl.UnitTest.Commands
{
    using System.ComponentModel;
    using System.Windows.Forms;
    using Framework;
    using Models;
    using Properties;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class NewRowCommand : KnvDataGridViewNewRowBaseCommand
    {
        public NewRowCommand(DataGridView dataGridView, IBindingList collection) : base(dataGridView, collection)
        {
            Text = @"New";
        }
    }
}
