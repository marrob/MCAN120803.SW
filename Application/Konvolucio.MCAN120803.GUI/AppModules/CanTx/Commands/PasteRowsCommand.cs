
namespace Konvolucio.MCAN120803.GUI.AppModules.CanTx.Commands
{
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Model;
    using WinForms.Framework;

    internal sealed class PasteRowsCommand : KnvDataGridViewPasteRowsBaseCommand
    {
        private readonly IBindingList _collection;
        public PasteRowsCommand(DataGridView gridView, IBindingList collection): base (gridView,collection)
        {
            _collection = collection;
        }

        protected override void AddItemToCollection(int index, object item)
        {
            var filterItem = item as CanTxMessageItem;
            if (filterItem != null)
                filterItem.Name = CollectionTools.GetExtendedUniqueItemName(filterItem.Name, _collection.Cast<CanTxMessageItem>().Select(n => n.Name).ToArray<string>());
            base.AddItemToCollection(index, item);
        }
    }
}
