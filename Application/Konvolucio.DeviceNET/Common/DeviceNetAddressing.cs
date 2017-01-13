// -----------------------------------------------------------------------
// <copyright file="DeviceNetAddressing.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.DeviceNet.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DeviceNetAddressing
    {
        /// <summary>
        /// Ha a CAN Id Check MACID akkor ture.
        /// </summary>
        /// <param name="canId"></param>
        /// <returns></returns>
        public static bool IsMacIdCheckCanId(uint canId)
        {
            return (((canId & 2 << 9) == 2 << 9) && (canId & 0x07) == 0x07);
        }

        /// <summary>
        /// Slave MAC id megszerzése a 2-es Id csoprotból.
        /// </summary>
        /// <param name="canId"></param>
        /// <returns></returns>
        public static ushort GetSlaveMacIdFormGorup2(uint canId)
        {
            canId >>= 3;
            return ((ushort)(canId & 0x3F));
        }

        public static ushort GetCheckMacId(int slaveMacId)
        {
            UInt16 retval = 0;
            retval |= 2 << 9;
            retval |= (UInt16)(slaveMacId << 3);
            retval |= 7;
            return retval;
        }

        public static ushort GetExplicitResponseCanId(int slaveMacId)
        {
            UInt16 retval = 0;
            retval |= 2 << 9;                    //Group 2
            retval |= (UInt16)(slaveMacId << 3); //Destination MAC ID
            retval |= 3;                         //Message ID
            return retval;
        }

        public static ushort GetExplicitRequestCanId(int slaveMacId)
        {
            UInt16 retval = 0;
            retval |= 2 << 9;
            retval |= (UInt16)(slaveMacId << 3);
            retval |= 4;
            return retval;
        }

        public static ushort GetUnconnctedExplicitRequestCanId(int slaveMacId)
        {
            UInt16 retval = 0;
            retval |= 2 << 9;                    //Group 2
            retval |= (UInt16)(slaveMacId << 3); //Destination MAC ID
            retval |= 6;                         //Message ID
            return retval;
        }

        public static ushort GetDuplicateMacCheckCanId(int slaveMacId)
        {
            UInt16 retval = 0;
            retval |= 2 << 9;
            retval |= (UInt16)(slaveMacId << 3);
            retval |= 7;
            return retval;
        }

    }
}
