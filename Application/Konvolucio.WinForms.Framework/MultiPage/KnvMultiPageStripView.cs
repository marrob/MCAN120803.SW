// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework
{
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class KnvMultiPageStripView : ToolStrip
    {
        [Category("KNV")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MultiPageCollection Pages
        {
            get { return _pages; }
            set
            {
                if (_pages != value)
                {
                    _pages = value;
                    SourceChanged(value);
                }

            }
        }
        private MultiPageCollection _pages;

        [Category("KNV")]
        public PageButton[] PageButtons
        {
            get { return Items.Cast<PageButton>().ToArray<PageButton>(); }
        }

        [Category("KNV")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MultiPageItem CurrentPage
        {
            get { return _pages.CurrentPage; }
            set
            {
                if(_pages != null)
                  _pages.CurrentPage = value;
            }
        }

        private void SourceChanged(MultiPageCollection value)
        {
            RemoveButtons();
            if (_pages != null)
            {
                foreach (var pageItem in _pages)
                    Items.Add(pageItem.PageButton);
            }

            if (_pages != null)
            {
                _pages.ListChanged += new ListChangedEventHandler(Page_ListChanged);
                _pages.ListChanging += new ListChangingEventHandler<MultiPageItem>(Page_ListChanging);
            }
        }

        private void Page_ListChanging(object sender, ListChangingEventArgs<MultiPageItem> e)
        {
            switch (e.ListChangedType)
            {
                case ListChangingType.ItemRemoving:
                {
                    var toRemoveItem = Items.Cast<ToolStripItem>().FirstOrDefault(n => n.Tag == e.Item);
                    if (toRemoveItem != null)
                    {
                        Items.Remove(toRemoveItem);
                    }
                    break;
                }
                case ListChangingType.Clearing:
                {
                    RemoveButtons();
                    break;
                }
            }
        }

        private void Page_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                {
                    var bindingList = sender as IBindingList;
                    if (bindingList != null)
                    {
                        var item = bindingList[e.NewIndex] as MultiPageItem;
                        if (item != null)
                            Items.Add(item.PageButton);

                    }
                    break;
                }
            }
        }

        private void RemoveButtons()
        {
            var items = Items.Cast<PageButton>().Where(n => n.Tag != null).ToArray();
            foreach (var item in items)
            {
                Items.Remove(item);
            }
        }
    }
}
