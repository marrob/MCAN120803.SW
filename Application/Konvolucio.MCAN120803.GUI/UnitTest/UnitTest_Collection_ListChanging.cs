namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using NUnit.Framework;

    [TestFixture]
    class UnitTest_Collection_ListChanging
    {

        interface IListChanging<T>
        {
            event ListChangingEventHandler<T> ListChanging;
        }

        enum ListChangingType
        {
            Reseting,
            ItemRemoving,
            ItemAdding,
        }

        class ListChangingEventArgs<T> : EventArgs
        {
            public ListChangingType ListChangedType { get; private set; }
            public T Item { get; private set; }

            public ListChangingEventArgs(ListChangingType listChangedType, T item)
            {
                ListChangedType = listChangedType;
                Item = item;
            }
        }

        delegate void ListChangingEventHandler<T>(object sender, ListChangingEventArgs<T> e);

        class PersonItem
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public string First { get; private set; }
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

        class MockPersonCollection : BindingList<PersonItem>
        {
            public event ListChangingEventHandler<PersonItem> ListChanging;

            public new void Add(PersonItem item)
            {
                OnItemAdding(item);
                base.Add(item);
            }

            public new void Remove(PersonItem item)
            {
                OnItemRemoving(item);
                base.Remove(item);
            }

            public new void Clear()
            {
                OnReseting();
                base.Clear();
            }

            private void OnItemRemoving(PersonItem item)
            {
                if (ListChanging != null)
                    ListChanging(this, new ListChangingEventArgs<PersonItem>(ListChangingType.ItemRemoving, item));
            }

            private void OnReseting()
            {
                if (ListChanging != null)
                    ListChanging(this, new ListChangingEventArgs<PersonItem>(ListChangingType.Reseting, null));
            }

            private void OnItemAdding(PersonItem item)
            {
                if (ListChanging != null)
                    ListChanging(this, new ListChangingEventArgs<PersonItem>(ListChangingType.ItemAdding, item));
            }
        }


        [Test]
        public void UintTest_Add()
        {
            List<string> events = new List<string>();
            MockPersonCollection persons = new MockPersonCollection();
            persons.ListChanging += (o, e) =>
            {
                events.Add(e.ListChangedType.ToString());
            };
            persons.ListChanged += (o, e) =>
            {
                events.Add(e.ListChangedType.ToString());
            };

            persons.Add(new PersonItem("Homer", "Simpons"));
            Assert.AreEqual(ListChangingType.ItemAdding.ToString(), events[0]);
            Assert.AreEqual(ListChangedType.ItemAdded.ToString(), events[1]);
        }

        private class MockPersonCollection2 : BindingList<PersonItem>, INotifyCollectionChanged
        {
            public event NotifyCollectionChangedEventHandler CollectionChanged;
        }
    }
}
