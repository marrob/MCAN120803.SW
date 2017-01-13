// -----------------------------------------------------------------------
// <copyright file="ColumnLayoutCollection.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    /// <summary>
    /// Egy DataGird oszlopainak Layout listája. 
    /// Ezelapjaán elmenthető a teljes DataGird megjelnése, majd vissza állítható.
    /// 
    /// Settings Type Name:
    /// Konvolucio.WinForms.Framework.ColumnLayoutCollection
    /// </summary>
    public class ColumnLayoutCollection : BindingList<ColumnLayoutItem>
    {
        public override string ToString()
        {
            string str = string.Empty;
            str = "\r\n";

            foreach (var i in this)
                str += i.ToString() + "\t\r\n";
            if (str.Length != 0)
                str = str.Trim('\r', '\n', '\t');
            return str;
        }

        public void CopyTo(ColumnLayoutCollection target)
        {
            target.Clear();
            foreach (var item in this)
            {
                var newItem = new ColumnLayoutItem();
                item.CopyTo(newItem);
                target.Add(newItem);
            }
        }
    }
}
