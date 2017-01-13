using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Konvolucio.MCAN120803.API;

namespace Konvolucio.MUDS150628
{
    /********************************************************************************/
    public static class Tools
    {
        /********************************************************************************/
        /// <summary>
        /// Byte Array To C-Style String.
        /// </summary>
        /// <param name="data">byte[] data</param>
        /// <returns>0x00,0x01</returns>
        public static string ByteArrayToCStyleString(byte[] data)
        {
            string retval = string.Empty;
            for (int i = 0; i < data.Length; i++)
                retval += string.Format("0x{0:X2},", data[i]);
            //Az utolsó vessző törlése
            if (data.Length > 1)
                retval = retval.Remove(retval.Length - 1, 1);
            return retval;
        }

        /********************************************************************************/
        //7 5 2 
        public static string ByteArrayToCStyleString(byte[] data, int startIndex, int length)
        {
            string retval = string.Empty;
            for (int i = 0; i < length; i++)
                retval += string.Format("0x{0:X2},", data[i + startIndex]);
            //Az utolsó vessző törlése
            if (data.Length > 1)
                retval = retval.Remove(retval.Length - 1, 1);
            return retval;
        }
        /********************************************************************************/
        /// <summary>
        /// CAN Messag formázása az alábbiak szerint:
        /// Id       DL Data
        /// 00000000;8;00 00 00 00 00 00 00
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string MsgFormat(CanMessage msg)
        {
            string retval = string.Empty;

            retval += string.Format("{0:X8}", msg.ArbitrationId) + ";";
            retval += msg.DataLength.ToString() + ";";
            for (int i = 0; i < msg.DataLength; i++)
                retval += string.Format("{0:X2} ", msg.Data[i]);
            //Az utolsó vessző törlése
            if (msg.Data.Length > 1)
                retval = retval.Remove(retval.Length - 1, 1);
            return retval;
        }
        /****************************************************************/
        /// <summary>
        /// Array forrmatter.
        /// Supported: offset, prefix, separator, block size.
        /// </summary>
        /// <typeparam name="T">eg.: byte[], string[]...</typeparam>
        /// <param name="array">eg.: byte[]</param>
        /// <param name="offset">eg.: 0</param>
        /// <param name="prefix">eg.: "0x"</param>
        /// <param name="format">eg.: "{0:X2}"</param>
        /// <param name="separator">eg: ','</param>
        /// <returns>eg.: 0x01,0x02,0x03...</returns>
        public static string ArrayToStringFormat<T>(T[] array, int offset, string prefix, string format, char separator, int blockSize)
        {
            string retval = string.Empty;
            if (array.Length == 0)
                return string.Empty;
            if (offset > array.Length)
                return string.Empty;
            int block = 1;

            for (int i = offset; i < array.Length; i++)
            {
                retval += string.Format(prefix + format + separator, array[i]);
                if ((block % blockSize) == 0)
                    retval += "\n";
                block++;
            }

            if (array.Length > 1)
            {
                retval = retval.TrimEnd('\n');
                retval = retval.Remove(retval.Length - 1, 1);
            }
            return (retval);
        }
        /********************************************************************************/
        internal static string TimestampFormat(long tick)
        {
            return new DateTime(tick).ToString("dd.MM.yyyy HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
        }


    }
}
