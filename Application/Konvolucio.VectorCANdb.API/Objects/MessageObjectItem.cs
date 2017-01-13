// -----------------------------------------------------------------------
// <copyright file="MessageObjectItem.cs" company="">
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
    /// TODO: Update summary.
    /// </summary>
    ///<summary>
    ///8 Message Definitions
    ///Keyword: BO_
    ///</summary>
    public class MessageObjectItem : DbcObject<MessageObjectItem>
    {
        public int ArbitrationId;
        public string Name;
        public int Size;
        public NodeObject TransmitterNodes;
        public List<SignalItemObject> Signals;

        public MessageObjectItem()
        {
            TransmitterNodes = new NodeObject();
            Signals = new List<SignalItemObject>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">
        /// "BO_ 143 ERROR_INDICATORS_1: 8 DGT"
        /// </param>
        /// <returns></returns>
        public MessageObjectItem Parse(string input)
        {
            string reamin = string.Empty;
            string[] array = input.Split(' ');
            int offset = 0;
            if (array[offset++] == "BO_")
            {
                ArbitrationId = DbcDatabase.ParseStringToInt32(array[offset++]);
                Name = array[offset++].Trim(new char[] { ' ', ':' });
                Size = DbcDatabase.ParseStringToInt32(array[offset++]);
                for (; offset < array.Length; offset++)
                    TransmitterNodes.Names.Add(array[offset]);
            }
            return this;
        }


        public string ToDbcFormat()
        {
            throw new NotImplementedException();
        }
    }
}
