// -----------------------------------------------------------------------
// <copyright file="ValueTableObjectItem.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.VectorCANdb.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 7 Value Table Definitions
    /// Keyword: VAL_TABLE_
    /// </summary>
    public class ValueTableObjectItem : DbcObject<ValueTableObjectItem>
    {
        public string Name;
        public List<ValueDescriptionItem> Descriptions;

        public ValueTableObjectItem()
        {
            Descriptions = new List<ValueDescriptionItem>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">
        /// "VAL_TABLE_ ValueTableItem1 3 \"Érték leírása 0x03\" 2 \"Érték leírása 0x02\" 1 \"Érték leírása 0x01\" 0 \"Érték leírása\" ;"
        /// </param>
        /// <returns></returns>
        public ValueTableObjectItem Parse(string input)
        {
            string remain = string.Empty;
            string[] array = input.Split(' ');
            if ("VAL_TABLE_" == array[0])
            {
                remain = input.Substring("VAL_TABLE_".Length);
                Name = remain.Split(' ')[1];
                remain = remain.Substring(Name.Length + 1).Trim(' ');
                array = remain.Split('"');
                for (int i = 0;
                     i < (array.Length - 1);
                     i += 2)
                    Descriptions.Add(new ValueDescriptionItem(DbcDatabase.ParseStringToInt32(array[i]), array[i + 1]));
            }
            return this;
        }


        public string ToDbcFormat()
        {
            throw new NotImplementedException();
        }
    }
}
