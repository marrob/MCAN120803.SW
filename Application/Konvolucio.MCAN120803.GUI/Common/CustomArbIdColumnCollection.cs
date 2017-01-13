// -----------------------------------------------------------------------
// <copyright file="CustomArbIdEditorItem.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.Common
{
    using System.ComponentModel;
    using DataStorage;
    using WinForms.Framework; 

    /// <summary>
    /// Az egyedi mezők listája.
    /// Lista egy eleme az egyedi mező.
    /// </summary>
    public class CustomArbIdColumnCollection : BindingList<CustomArbIdColumnItem>
    {
        /// <summary>
        /// Lista változása megkezdődött.
        /// </summary>
        public event ListChangingEventHandler<CustomArbIdColumnItem> ListChanging;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public CustomArbIdColumnCollection()
        {
            AllowEdit = true;
            AllowNew = true;
            AllowRemove = true;
        }

        /// <summary>
        /// Új elem hozzáadása autmatizáltan.
        /// </summary>
        /// <returns>Új elem.</returns>
        protected override object AddNewCore()
        {
            var column = new CustomArbIdColumnItem
            {
                Columns = this
            };
            Add(column);
            return column;
        }

        /// <summary>
        /// Egyedi mező beszúrása a listába.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, CustomArbIdColumnItem item)
        {
            var arbIdColumnItem = item as CustomArbIdColumnItem;
            if (arbIdColumnItem != null)
                arbIdColumnItem.Columns = this;
            base.InsertItem(index, item);
        }

        /// <summary>
        /// Egyedi mező törlése a listából.
        /// </summary>
        /// <param name="index"></param>
        protected override void RemoveItem(int index)
        {
            OnItemRemoveing(this[index]);
            base.RemoveItem(index);
        }

        /// <summary>
        /// Minden egyedi mező törlése a listából.
        /// </summary>
        protected override void ClearItems()
        {
            OnReseting();
            base.ClearItems();
        }
        /// <summary>
        /// Egyedi mező tölrése referenciával.
        /// </summary>
        /// <param name="item"></param>
        public new void Remove(CustomArbIdColumnItem item)
        {
            OnItemRemoveing(item);
            base.Remove(item);
        }

        protected void OnItemRemoveing(CustomArbIdColumnItem item)
        {
            if (ListChanging != null)
                ListChanging(this, new ListChangingEventArgs<CustomArbIdColumnItem>(ListChangingType.ItemRemoving, item));
        }

        protected void OnReseting()
        {
            if (ListChanging != null)
                ListChanging(this, new ListChangingEventArgs<CustomArbIdColumnItem>(ListChangingType.Clearing, null));
        }

        public void CopyTo(StorageCustomArbIdColumnCollection target)
        {
            target.Clear();
            foreach (var item in this)
            {
                var targetItem = new StorageCustomArbIdColumnItem();
                item.CopyTo(targetItem);
                target.Add(targetItem);
            }
        }

        public void CopyTo(CustomArbIdColumnCollection target)
        {
            target.Clear();
            foreach (var item in this)
            {
                var targetItem = new CustomArbIdColumnItem();
                item.CopyTo(targetItem);
                target.Add(targetItem);
            }
        }
    }
}
