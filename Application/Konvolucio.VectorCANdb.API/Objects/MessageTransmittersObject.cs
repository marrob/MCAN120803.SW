// -----------------------------------------------------------------------
// <copyright file="MessageTransmittersObject.cs" company="">
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
    /// 8.2 Definition of Message Transmitters
    /// Keyword: BO_TX_BU_
    /// </summary>
    public class MessageTransmittersObject : DbcObject<MessageTransmittersObject>
    {
        public int ArbitrationId;
        public List<string> TransmitterNames;

        public MessageTransmittersObject()
        {
            TransmitterNames = new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">Sample: "BO_TX_BU_ 1 : DEMO_NODE,OMRON_NODE,PC_NODE;"</param>
        /// <returns></returns>
        public MessageTransmittersObject Parse(string input)
        {
            input = input.Trim(';');
            string[] array = null;
            if (input.Contains("BO_TX_BU_"))
            {
                /*left*/
                array = input.Split(':');
                ArbitrationId = DbcDatabase.ParseStringToInt32(array[0].Split(' ')[1]);

                /*right*/
                array[1] = array[1].Trim(' ');
                foreach (string name in array[1].Split(','))
                    TransmitterNames.Add(name);
            }
            return this;
        }


        public string ToDbcFormat()
        {
            throw new NotImplementedException();
        }
    }
}
