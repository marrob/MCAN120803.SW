namespace Konvolucio.MCAN120803.GUI.UnitTest2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using NUnit.Framework;

    public interface IListChanging<T>
    {
        event ListChangingEventHandler<T> ListChanging;
    }
    
    public enum ListChangingType
    {
        Reseting,
        ItemRemoving,
        ItemAdding,
    }
    
    public class ListChangingEventArgs<T> : EventArgs
    {
        public ListChangingType ListChangedType { get; private set; }
        public T Item { get; private set; }
        public ListChangingEventArgs(ListChangingType listChangedType, T item)
        {
            ListChangedType = listChangedType;
            Item = item;
        }
    }
    
    public delegate void ListChangingEventHandler<T>(object sender, ListChangingEventArgs<T> e);
    
        public interface IPersonItem : INotifyPropertyChanged
        {
            string First { get; }
            string Last { get; }
        }

        public class PersonItem : IPersonItem
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public string First { get; private set;}
            public string Last { get; private set; }
            public PersonItem(string first, string last)
            {
                First = first;
                Last = last;
            }
            protected void OnPorpretyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            public override string ToString()
            {
                return First + " " + Last;
            }
        }

        public interface IPersonCollection : IListChanging<IPersonItem>
        {
            event ListChangedEventHandler ListChanged;
            void Add(IPersonItem item);
            int Count { get; }
            void Remove(IPersonItem item);
            IEnumerator<IPersonItem> GetEnumerator();
        }

        public class PersonCollection : BindingList<IPersonItem>, IPersonCollection
        {
            public event ListChangingEventHandler<IPersonItem> ListChanging;

            public new void Add(IPersonItem item)
            {
                OnItemAdding(item);
                base.Add(item);
            }

            public new void Remove(IPersonItem item)
            {
                OnItemRemove(item);
                base.Remove(item);
            }

            public new void Clear()
            {
                OnReseting();
                base.Clear();
            }

            protected void OnItemRemove(IPersonItem item)
            {
                if (ListChanging != null)
                    ListChanging(this, new ListChangingEventArgs<IPersonItem>(ListChangingType.ItemRemoving, item));
            }

            protected void OnReseting()
            {
                if (ListChanging != null)
                    ListChanging(this, new ListChangingEventArgs<IPersonItem>(ListChangingType.Reseting, null));
            }

            protected void OnItemAdding(IPersonItem item)
            {
                if (ListChanging != null)
                    ListChanging(this, new ListChangingEventArgs<IPersonItem>(ListChangingType.ItemAdding, item));
            }
        }

    [TestFixture]
    class UnitTest_Collection
    {
        [Test]
        public void UintTest_Add()
        {
            List<string> events = new List<string>();
            IPersonCollection persons = new PersonCollection();
            persons.ListChanging += (o, e) =>
            {
                events.Add(e.ListChangedType.ToString());
            };
            persons.ListChanged += (o, e) =>
            {
                events.Add(e.ListChangedType.ToString());
            };

            persons.Add(new PersonItem("Homer","Simpons"));
            Assert.AreEqual(ListChangingType.ItemAdding.ToString(), events[0]);
            Assert.AreEqual(ListChangedType.ItemAdded.ToString(), events[1]);
        }
    }

}
