// -----------------------------------------------------------------------
// <copyright file="SenderTablesCollection.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MultiPageCollection : BindingList<MultiPageItem> , IDisposable
    {
        /// <summary>
        /// Aktuálisan látható Page változott.
        /// </summary>
        public event EventHandler CurrentPageChanged;

        /// <summary>
        /// Page listája változott.
        /// </summary>
        public event ListChangingEventHandler<MultiPageItem> ListChanging;

        /// <summary>
        /// Az aktuális Page
        /// </summary>
        public MultiPageItem CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage != null)
                {
                    if (!_currentPage.Equals(value))
                    {
                        PreviousPage = _currentPage;
                        PreviousPage.PageButton.Checked = false;
  
                        _currentPage = value;
                        _currentPage.PageButton.Checked = true;

                        OnCurrentPageChanged();
                    }
                }
                else
                {
                    
                    _currentPage = value;
                    PreviousPage = _currentPage;
                    _currentPage.PageButton.Checked = true;
                    OnCurrentPageChanged();
                }
            }
        }

        public object SyncRoot
        {
            get { return ((ICollection)this).SyncRoot; }
        }

        /// <summary>
        /// Elõzõ pagae, ha volt, egyébként null.
        /// </summary>
        public MultiPageItem PreviousPage { get; private set; }

        private MultiPageItem _currentPage;

        public MultiPageItem this[string name]
        {
            get { return this.FirstOrDefault(n => n.Name == name); }
        }

        private void OnCurrentPageChanged()
        {
            if (CurrentPageChanged != null)
                CurrentPageChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Page hozzáadása.
        /// </summary>
        /// <param name="item"></param>
        public new void Add(MultiPageItem item)
        {
            OnItemAdding(item);
            base.Add(item);
        }

        /// <summary>
        /// Page hozzáadása.
        /// </summary>
        /// <param name="userControl">Ez felel meg egy Page-nek, lehet UserControl, vagy Control.</param>
        /// <param name="name">Ezen a néven jelenik meg a menüben.</param>
        /// <param name="imageKey"></param>
        public void Add(string name, UserControl userControl, string imageKey)
        {
            var item = new MultiPageItem(name, userControl, imageKey);
            Add(item);
        }

        private void PageButton_Click(object sender, EventArgs e)
        {
            var button = sender as PageButton;
            if (button != null)
            {
                CurrentPage = (MultiPageItem)button.Tag;
                OnCurrentPageChanged();
            }
        }

        protected override void InsertItem(int index, MultiPageItem item)
        {
            CurrentPage = item;
            item.PageButton.Click += new EventHandler(PageButton_Click);
            base.InsertItem(index, item);
        }

        /// <summary>
        /// Page-ek törlése.
        /// </summary>
        public new void Clear()
        {
            lock ((this as ICollection).SyncRoot)
            {
                OnClearing();
                CurrentPage = new MultiPageItem("", new UserControl(), "");
                base.Clear();
            }
        }

        /// <summary>
        /// Page eltávolitása listából.
        /// </summary>
        public new bool Remove(MultiPageItem item)
        {
            if ((MultiPageItem)item == CurrentPage)
            {
                if (PreviousPage != null)
                    CurrentPage = PreviousPage;
            }

            if (Count == 1)
            {
                this.Clear();
            }

            OnItemRemoving(item);
            item.PageButton.Click -= new EventHandler(PageButton_Click);
            return base.Remove(item);
        }

        /// <summary>
        /// Page eltávolitása listából.
        /// </summary>
        public bool Remove(string name)
        {
            var item = this.FirstOrDefault(n => n.Name == name);
            if (item != null)
                return Remove(item);
            else
                return false;
        }

        /// <summary>
        /// Az eseméynben kapott Node.Tag alapján eldönti, hogy 
        /// A Node.Tag-jában található PageItem, ha igen, akkor azt 
        /// megjeleníti.
        /// Ezt a TreeView-nak kell mghívnia.
        /// <code>
        /// form.treeView1.NodeMouseClick += (o, e) =>
        /// {
        ///     pages.ClickOnNodeHandler(e.Node);
        /// };
        /// </code>
        /// </summary>
        /// <param name="node"></param>
        /// <returns>Az esemy kezelve lett.</returns>
        public bool ClickOnNodeHandler(TreeNode node)
        {
            var selectedPage = node.Tag as MultiPageItem;
            if (selectedPage != null)
            {
                CurrentPage = selectedPage;
                return true;
            }
            return false;
        }

        private void OnItemRemoving(MultiPageItem item)
        {
            if (ListChanging != null)
                ListChanging(this, new ListChangingEventArgs<MultiPageItem>(ListChangingType.ItemRemoving, item));
        }

        private void OnItemAdding(MultiPageItem item)
        {
            if (ListChanging != null)
                ListChanging(this, new ListChangingEventArgs<MultiPageItem>(ListChangingType.ItemAdding, item));
        }

        private void OnClearing()
        {
            if (ListChanging != null)
                ListChanging(this, new ListChangingEventArgs<MultiPageItem>(ListChangingType.Clearing, null));
        }

        public void Dispose()
        {
            foreach (var item in this)
                item.Dispose();
        }
    }
}
