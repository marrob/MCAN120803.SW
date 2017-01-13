// -----------------------------------------------------------------------
// <copyright file="AddMessageFilter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Filter.Commands
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Common;
    using WinForms.Framework;
    using Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class NewCommand : KnvDataGridViewNewRowBaseCommand
    {
        private readonly IBindingList _collection;
        public NewCommand(DataGridView dataGridView, IBindingList collection): base(dataGridView, collection)
        {
            _collection = collection;
        }

        protected override object MakeNewItem(Type type)
        {
            var name = CollectionTools.GetNewName(_collection.Cast<MessageFilterItem>().Select(n => n.Name).ToArray<string>(), "New_Filter");
            var newItem = new MessageFilterItem()
            {
                Name = name,
                Direction = MessageDirection.Transmitted,
                Enabled = true,
                MaskOrArbId = MaskOrArbId.ArbId,
                MaskOrArbIdValue = 0x0,
                Type = ArbitrationIdType.Standard,
            };
            return newItem;
        }
    }
}
