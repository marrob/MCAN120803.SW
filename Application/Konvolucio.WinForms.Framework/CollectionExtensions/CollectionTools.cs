// -----------------------------------------------------------------------
// <copyright file="Tools.cs" company="">
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
    /// TODO: Update summary.
    /// </summary>
    public static class CollectionTools
    {
        /// <summary>
        /// Új név generálása
        /// 
        /// New_Message_1
        /// New_Message_2
        /// New_Message_3
        /// New_Message_4
        /// ...
        /// 
        /// A <paramref name="names"/> listát így szerezheted meg:
        /// <code>
        /// this.Select(n=>n.Name).ToArray string()
        /// </code>
        /// 
        /// </summary>
        /// <param name="names"></param>
        /// <param name="before">pl:New_Message_4</param>
        /// <returns></returns>
        public static string GetNewName(string[] names, string before)
        {
            //before | index | 
            if (names == null || names.Length == 0)
            {
                return before + "_1";
            }
            else
            {
                var na = names.Where(n => { return string.IsNullOrEmpty(n) ? false : n.Contains(before); }).ToArray<string>();
                int preValue = 1;
                foreach (string item in na)
                {
                    int currValue = 1;
                    var v = item.Split('_');
                    if (v.Length == 3)
                    {
                        if (int.TryParse(v[2], out currValue))
                        {
                            if (currValue > preValue)
                                preValue = currValue;
                        }
                    }
                }
                return before + "_" + (preValue + 1).ToString();
            }
        }

        /// <summary>
        /// 
        /// A <paramref name="names"/> listát így szerezheted meg:
        /// <code>
        /// this.Select(n=>n.Name).ToArray string()
        /// </code>
        /// </summary>
        /// <param name="currentName"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string GetExtendedUniqueItemName(string currentName, string[] names)
        {
            var newName = currentName;
            if (names.Contains(newName))
            {
                for (var i = 2; i < 99; i++)
                {
                    newName = currentName + "_" + i.ToString();
                    if (!names.Contains(newName))
                        break;
                }
            }
            return newName;
        }
    }
}
