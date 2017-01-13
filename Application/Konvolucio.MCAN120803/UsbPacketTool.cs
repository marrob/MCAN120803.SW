
namespace Konvolucio.MCAN120803.API
{
    using System;
    using System.Runtime.InteropServices;

    class UsbPacketTool
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool IsFrameEnd(byte[] packet, int length)
        {
            bool isEnd = false;
            if (length >= 8)
            {
                if (BitConverter.ToInt64(packet, length - 8) == 0x0000feff00020000)
                    isEnd = true;
            }
            return isEnd;
        }

        /// <summary>
        /// Ezt meg kell tartani ez jó strukurára és osztályjra is
        /// Osztály esetén Az osztájly meg kell jeölni 
        /// [StructLayout(LayoutKind.Sequential, Pack = 1)]
        /// public class DisconnectionComplete
        /// Native tipusra is jó
        /// byte[] nativeTypeValue = Common.Serialize<UInt32>(0xFFFFFFFF);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Byte[] Serialize<T>(T msg)
        {
            //lock (msg)
            {
                int objsize = Marshal.SizeOf(typeof(T));
                Byte[] ret = new Byte[objsize];
                IntPtr buff = Marshal.AllocHGlobal(objsize);
                Marshal.StructureToPtr(msg, buff, true);
                Marshal.Copy(buff, ret, 0, objsize);
                Marshal.FreeHGlobal(buff);
                return ret;
            }
        }
    }
}
