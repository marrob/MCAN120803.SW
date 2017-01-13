// -----------------------------------------------------------------------
// <copyright file="SignalValueDescriptionsItem.cs" company="">
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
    /// 8.3 Signal Value Descriptions (Value Encodings)
    /// Keyword: "VAL_"
    /// </summary>
    public class SignalValueDescriptionsItem : DbcObject<SignalValueDescriptionsItem>
    {
        public int ArbitrationId;
        public string Name;
        public List<ValueDescriptionItem> Descriptions;

        public SignalValueDescriptionsItem()
        {
            Descriptions = new List<ValueDescriptionItem>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">Sample: "VAL_ 31 DMD_DARK 0 "pas de demande" 1 "Dark 0 demandé" 2 "Dark 2 demandé" 3 "Invalide" ;"</param>
        /// <returns></returns>
        public SignalValueDescriptionsItem Parse(string input)
        {
            string remain = string.Empty;
            string[] array = input.Split(' ');
            if ("VAL_" == array[0])
            {
                remain = input.Substring("VAL_".Length);
                string arbIdStr = remain.Split(' ')[1];
                ArbitrationId = DbcDatabase.ParseStringToInt32(arbIdStr);
                Name = remain.Split(' ')[2];
                remain = remain.Substring(Name.Length + arbIdStr.Length + 2).Trim(new char[] { ' ', ';' });
                array = remain.Split('"');
                for (int i = 0;
                    i < ((array.Length - 1));
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
