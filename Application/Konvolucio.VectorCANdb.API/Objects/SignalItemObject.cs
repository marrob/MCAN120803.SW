// -----------------------------------------------------------------------
// <copyright file="SignalItemObject.cs" company="">
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
    /// 8.1 Signal Definitions
    /// Keyword: SG_
    /// </summary>
    public class SignalItemObject : DbcObject<SignalItemObject>
    {
        public string Name;     /*C Style*/
        public string MultiplesxerIndicator; /*T M1 | m multiplexer_switch_value*/
        public int StartBit;
        public int SignalSize;  /*bits*/
        public char ByteOrder;  /*0=little endian, l=big endian */
        public char ValueType;  /*+=unsigned, -=signed */
        public double Factor;
        public double Offset;
        public double Minimum;
        public double Maximum;
        public string Unit;

        /// <summary>
        /// The receiver name has to
        /// be defined in the set ol node names in the node section, ff the signal shall have no
        /// receiver the string 'vector_xxx1 has to be given here.
        /// </summary>
        public string ReceiverNodeName;

        public SignalItemObject()
        {

        }

        /// <summary>http://www.thewindowsclub.com/enable-old-windows-clock-calendar-windows-10
        /// 
        /// </summary>
        /// <param name="input">
        /// "SG_ COUNT_LINE_FAULT_ERRORS_MAX M : 55|16@0+ (1,0) [0|65535] \"ms\"  PC"
        /// "SG_ DATATION_APPUI_3 : 39|16@0+ (0.1,0) [0|6553.5] \"ms 1\"  PC"
        /// </param>
        /// <returns></returns>
        public SignalItemObject Parse(string input)
        {

            int offset = 0;
            string[] array;

            if (input.Contains("SG_"))
            {
                /*LEFT:*/
                /*Name,MultiplesxerIndicator*/
                string left = input.Substring(0, input.IndexOf(':')).Trim(' ');
                array = left.Split(' ');
                Name = array[++offset];
                if (array.Length >= offset + 2)
                    MultiplesxerIndicator = array[++offset];
                else
                    MultiplesxerIndicator = string.Empty;

                /*:RIGHT*/
                string right = input.Substring(input.IndexOf(':') + 1).Trim(' ');
                /*Unit*/
                offset = 0;
                array = null;
                string temp = string.Empty;
                temp = right.Substring(right.IndexOf('"') + 1);
                if (temp.Contains('"'))
                    Unit = temp.Substring(0, temp.IndexOf('"'));
                else
                    Unit = string.Empty;

                /*Minimum, Maximum*/
                offset = 0;
                array = null;
                temp = string.Empty;
                temp = right.Substring(right.IndexOf('[') + 1, right.IndexOf(']') - right.IndexOf('[') - 1);
                array = temp.Split('|');
                Minimum = DbcDatabase.ParseStringToDouble(array[offset++]);
                Maximum = DbcDatabase.ParseStringToDouble(array[offset++]);

                /*Factor, Offset*/
                offset = 0;
                array = null;
                temp = string.Empty;
                temp = right.Substring(right.IndexOf('(') + 1, right.IndexOf(')') - right.IndexOf('(') - 1);
                array = temp.Split(',');
                Factor = DbcDatabase.ParseStringToDouble(array[offset++]);
                Offset = DbcDatabase.ParseStringToDouble(array[offset++]);

                /*StartBit, SignalSize, ByteOrder, ValueType*/
                StartBit = DbcDatabase.ParseStringToInt32(right.Substring(0, right.IndexOf('|')));
                SignalSize = DbcDatabase.ParseStringToInt32(right.Substring(right.IndexOf('|') + 1, input.IndexOf('@') - input.IndexOf('|') - 1));
                ByteOrder = right.Substring(right.IndexOf('@') + 1, 1).ToCharArray()[0];
                ValueType = right.Substring(right.IndexOf('@') + 2, 1).ToCharArray()[0];

                /*Receiver node_name*/
                ReceiverNodeName = right.Split(' ')[right.Split(' ').Length - 1];

            }
            return this;
        }


        public string ToDbcFormat()
        {
            throw new NotImplementedException();
        }
    }
}
