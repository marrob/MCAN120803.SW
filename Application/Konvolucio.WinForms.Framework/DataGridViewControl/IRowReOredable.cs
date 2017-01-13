// -----------------------------------------------------------------------
// <copyright file="IRowReOredable.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework
{
    /// <summary>
    /// Az a kollekció ami megvalósítja, azont a sorok áthelyezhetőak lesznek DragDrop módszerrel.
    /// Tipikus implementáció:
    /// <code>
    /// public void ItemMoveTo(object item, int index)
    /// {
    ///     lock (_lockObj) 
    ///     {
    ///         RaiseListChangedEvents = false;
    ///         Remove(item as IMessageSenderItem);
    ///         Insert(index, item as IMessageSenderItem);
    ///         RaiseListChangedEvents = true;
    ///         ResetBindings();
    ///     }
    /// }
    /// </code>
    /// </summary>
    public interface IRowReOredable
    {
        /// <summary>
        /// Egy sor mozgatása az adott index-re.
        /// </summary>
        /// <param name="item">Mozgatandó elem.</param>
        /// <param name="index">Cél index.</param>
        void ItemMoveTo(object item, int index);
    }
}
