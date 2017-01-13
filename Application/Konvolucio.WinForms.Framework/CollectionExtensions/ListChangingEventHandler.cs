// -----------------------------------------------------------------------
// <copyright file="ListChangingEventhandler.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A Binding List típust egésziti ki, hogy az esemény előtt értesüljön pl. a GUI a trölés esményről,
    /// így GUI tudja majd hogy mit is kell törölni
    /// 
    /// A BindingList alapesetben elem törlésekor nem küldi meg azt az infomrációt, hogy meliyk elem lett törölve. 
    /// Emiatt vált szükségessé ennek bevezetése.
    /// 
    ///<code>
    ///internal class MockPersonCollection : BindingList<IPersonItem>, IPersonCollection
    ///{
    ///    public event ListChangingEventHandler<IPersonItem> ListChanging;
    ///
    ///    public new void Add(IPersonItem item)
    ///    {
    ///        OnItemAdding(item);
    ///        base.Add(item);
    ///    }
    ///
    ///    public new void Remove(IPersonItem item)
    ///    {
    ///        OnItemRemoving(item);
    ///        base.Remove(item);
    ///    }
    ///
    ///    public new void Clear()
    ///    {
    ///        OnReseting();
    ///        base.Clear();
    ///    }
    ///
    ///    protected void OnItemRemoving(IPersonItem item)
    ///    {
    ///        if (ListChanging != null)
    ///            ListChanging(this, new ListChangingEventArgs<IPersonItem>(ListChangingType.ItemRemoving, item));
    ///    }
    ///
    ///    protected void OnClearing()
    ///    {
    ///        if (ListChanging != null)
    ///            ListChanging(this, new ListChangingEventArgs<IPersonItem>(ListChangingType.Clearing, null));
    ///    }
    ///
    ///    protected void OnItemAdding(IPersonItem item)
    ///    {
    ///        if (ListChanging != null)
    ///            ListChanging(this, new ListChangingEventArgs<IPersonItem>(ListChangingType.ItemAdding, item));
    ///    }
    ///}
    ///</code>
    /// 
    ///
    /// </summary>

    public interface IListChanging<T>
    { 
       event ListChangingEventHandler<T> ListChanging;
    }

    public enum ListChangingType
    {
        Clearing,
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

}
