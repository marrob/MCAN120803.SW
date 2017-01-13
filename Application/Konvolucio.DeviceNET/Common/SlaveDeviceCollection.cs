// -----------------------------------------------------------------------
// <copyright file="DeviceInstance.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.DeviceNet.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Konvolucio.DeviceNet.SlaveDevices;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SlaveDeviceCollection : List<ISlaveDevice>
    {
        DeviceNetMaster master;

        public SlaveDeviceCollection()
        {

        }

        public static SlaveDeviceCollection SupportedDevices
        {
            get
            {
                SlaveDeviceCollection retval = new SlaveDeviceCollection();
                retval.Add(new DeviceGTR1(null, -1));
                retval.Add(new DeviceMRLY(null, -1));
                return retval;
            }
        }

        public ISlaveDevice CreateDevice(int slaveMacId, ushort vendorId, ushort porductCode, uint serialNumber)
        {
            if (vendorId == DeviceGTR1.VendorId && porductCode == DeviceGTR1.ProductCode)
                return new DeviceGTR1(master, slaveMacId);
            else if (vendorId == DeviceMRLY.VendorId && porductCode == DeviceMRLY.ProductCode)
                return new DeviceMRLY(master, slaveMacId);
            else
                return new DeviceNotSupported();
        }

        public SlaveDeviceCollection(DeviceNetMaster master)
        {
            this.master = master;
        }
    }
}
