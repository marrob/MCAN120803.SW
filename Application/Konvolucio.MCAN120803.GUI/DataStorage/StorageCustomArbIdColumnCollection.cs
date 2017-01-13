// -----------------------------------------------------------------------
// <copyright file="CustomArbIdColumnCollection.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.DataStorage
{
    using System.Collections.Generic;
    using Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class StorageCustomArbIdColumnCollection : List<StorageCustomArbIdColumnItem>
    {
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

        public override string ToString()
        {
            var str = string.Empty;
            str = "\r\n";

            foreach (var i in this)
                str += i.ToString() + "\t\r\n";
            if (str.Length != 0)
                str = str.Trim('\r', '\n', '\t');
            return str;
        }
    }
}
