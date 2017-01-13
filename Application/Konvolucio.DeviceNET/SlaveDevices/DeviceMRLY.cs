// -----------------------------------------------------------------------
// <copyright file="DeviceNetDevice.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.DeviceNet.SlaveDevices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Konvolucio.DeviceNet.SlaveDevices;
    using Konvolucio.DeviceNet.Common;
    using Konvolucio.DeviceNet.Objects;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DeviceMRLY : BaseSlaveDevice
    {
        public IdentityObject Identity { get; private set; }
        public DiscrateOutputPointObject DiscrateOuputPoint { get; private set; }

        public DeviceMRLY(DeviceNetMaster master, int slaveMacId)
        {
            Identity = new IdentityObject(this);
            DiscrateOuputPoint = new DiscrateOutputPointObject(this, 32);
        }

        public override  ObjectCollection Objects
        {
            get
            {
                return new ObjectCollection 
                {
                    Identity,
                    DiscrateOuputPoint
                };
            }
        }

        public static ushort VendorId
        {
            get { return 1; }
        }

        public static ushort ProductCode
        {
            get { return 2; }
        }

    }
}
