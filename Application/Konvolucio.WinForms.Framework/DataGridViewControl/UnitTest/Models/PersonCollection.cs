namespace Konvolucio.WinForms.Framework.DataGridViewControl.UnitTest.Models
{
    using System.Collections;
    using System.ComponentModel;

    public class PersonCollection : BindingList<PersonItem>, ICollection
    {
        public event ListChangingEventHandler<PersonItem> ListChanging;


        public new PersonItem AddNew()
        {
            var newItem = new PersonItem();
            return newItem;
        }

        public new void Add(PersonItem item)
        {
            OnItemAdding(item);
            base.Add(item);
        }

        public new void Remove(PersonItem item)
        {
            OnItemRemove(item);
            base.Remove(item);
        }

        public new void Clear()
        {
            OnReseting();
            base.Clear();
        }

        protected void OnItemRemove(PersonItem item)
        {
            if (ListChanging != null)
                ListChanging(this, new ListChangingEventArgs<PersonItem>(ListChangingType.ItemRemoving, item));
        }

        protected void OnReseting()
        {
            if (ListChanging != null)
                ListChanging(this, new ListChangingEventArgs<PersonItem>(ListChangingType.Clearing, null));
        }

        protected void OnItemAdding(PersonItem item)
        {
            if (ListChanging != null)
                ListChanging(this, new ListChangingEventArgs<PersonItem>(ListChangingType.ItemAdding, item));
        }
    }

  
}
