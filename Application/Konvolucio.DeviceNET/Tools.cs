using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvolucio.DeviceNet
{
    internal static class Tools
    {
        /// <summary>
        /// byteArray to ASCII charArray = string;
        /// Ha a bájt értéke nem ASCII karakter akkor azt nem konvertálja
        /// </summary>
        /// <param name="byteArray">byte array</param>
        /// <returns>string</returns>
        public static string ByteArrayToString(byte[] byteArray)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

            return (enc.GetString(byteArray).Trim());
        }

        /// <summary>
        /// Az bájt tömb érték konvertálása string.
        /// </summary>
        /// <param name="byteArray">byte array</param>
        /// <param name="offset">az ofszettől kezdődően kezdődik a konvertálás</param>
        /// <returns>string pl.: (00 FF AA) </returns>
        internal static string ByteArrayLogString(byte[] byteArray, int offset, int length)
        {
            string retval = string.Empty;
            for (int i = offset; i < offset + length; i++)
                retval += string.Format("{0:X2} ", byteArray[i]);
            if (byteArray.Length > 1)
                retval = retval.Remove(retval.Length - 1, 1);
            return (retval);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        internal static string ByteArrayLogString(byte[] byteArray)
        {
            string retval = string.Empty;
            for (int i = 0; i < byteArray.Length; i++)
                retval += string.Format("{0:X2} ", byteArray[i]);
            if (byteArray.Length > 1)
                retval = retval.Remove(retval.Length - 1, 1);
            return (retval);
        }
    }
}
