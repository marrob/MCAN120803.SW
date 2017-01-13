
namespace Konvolucio.MCAN120803.API
{
    public class CanAdapterItem
    {
        /// <summary>
        /// DeviceDescription from in *.INF file.
        /// </summary>
        public string DeviceDescription { get; internal set; }
        public string SerialNumber { get; internal set; }
        public bool InUse { get; internal set; }
        public string DevicePath { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        internal CanAdapterItem()
        {
            DeviceDescription = "na";
            SerialNumber = "na";
            InUse = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = DeviceDescription + " " + SerialNumber + " " + ((InUse) ? "- InUse" : "");
            return str;
        }
    }
}
