// -----------------------------------------------------------------------
// <copyright file="AddMessageFilter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Filter.Commands
{
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Model;
    using WinForms.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class PasteRowsCommand : KnvDataGridViewPasteRowsBaseCommand
    {
        private readonly IBindingList _collection;
        public PasteRowsCommand(DataGridView dataGridView, IBindingList collection) : base(dataGridView, collection)
        {
            _collection = collection;
        }

        protected override void AddItemToCollection(int index, object item)
        {
            var filterItem = item as MessageFilterItem;
            if (filterItem != null)
                filterItem.Name = CollectionTools.GetExtendedUniqueItemName(filterItem.Name, _collection.Cast<MessageFilterItem>().Select(n => n.Name).ToArray<string>());
            base.AddItemToCollection(index, item);
        }
    }
}
