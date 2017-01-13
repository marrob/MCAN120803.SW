
namespace Konvolucio.MCAN120803.GUI.AppModules.CanTx.Commands
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Common;
    using Model;
    using WinForms.Framework;

    internal sealed class NewRowCommand : KnvDataGridViewNewRowBaseCommand
    {
        private readonly IBindingList _collection;
        private readonly DataGridView _gridView;

        public NewRowCommand(DataGridView gridView, IBindingList collection):base(gridView, collection)
        {
            _collection = collection;
            _gridView = gridView;
            ShortcutKeys = Keys.Alt | Keys.N;
        }

        protected override object MakeNewItem(Type type)
        {
            var name = CollectionTools.GetNewName(_collection.Cast<CanTxMessageItem>().Select(n => n.Name).ToArray<string>(), "New_Message");
            var newItem = new CanTxMessageItem()
            {
                Name = name,
                Key = string.Empty,
                IsPeriod = false,
                Type  = ArbitrationIdType.Standard,
                Remote = false,
                ArbitrationId = 1,
                Data = new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
                Documentation = string.Empty,
                Description = string.Empty
            };
            return newItem;
        }
    }
}
