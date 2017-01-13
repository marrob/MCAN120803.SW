
namespace Konvolucio.DeviceNet.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DeviceNetException : Exception
    {
        public DeviceNetException()
        {
        }

        public DeviceNetException(string message)
            : base(message)
        {
        }

        public DeviceNetException(string message, Exception inner)
            : base(message, inner)
        {
        }
    
        /// <summary>
        /// B-1 General Status Codes Edition 3.3
        /// Page: 1223
        /// </summary>
        /// <param name="generalErrorCode"></param>
        /// <param name="adittionalCode"></param>
        /// <returns></returns>
        public static string ErrorMessageToString(byte generalErrorCode, byte adittionalCode)
        {
            string retval = string.Empty;
            switch (generalErrorCode)
            {
                case 0x08: { retval = "Service Not Supported."; break; }
                case 0x0B: { retval = "Already in requested mode/state."; break; }
                case 0x14: { retval = "Attribute not supported."; break; }
                case 0x16: { retval = "Object does not exist."; break; }
                default: { retval = "Unknwon Error Code"; break; }
            }

            retval += string.Format(" [Error Code:0x{0:X2} - Adittional Code:0x{1:X2}]", generalErrorCode, adittionalCode);
            return retval;
        }
    }
}
