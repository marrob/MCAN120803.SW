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
    public class DeviceGTR1 : BaseSlaveDevice
    {
        public IdentityObject Identity { get; private set; }
        public DiscrateOutputPointObject DiscrateOuputPoint { get; private set; }

        public DeviceGTR1(DeviceNetMaster master, int slaveMacId)
        {
            this.SlaveMacId = slaveMacId;
            this.Master = master;
            Identity = new IdentityObject(this);
        }

        public void AlllocateExplicitConnection()
        {
            Master.Unconnected.AllocateExplicitConnection(SlaveMacId);
        }

        public void ReleaseExplicitConnection()
        {
            Master.Unconnected.ReleaseExplicitConnection(SlaveMacId);
        }

        public override ObjectCollection Objects
        {
            get 
            {
                return new ObjectCollection 
                {
                    Identity,
                };
            }
        }

        public static ushort VendorId
        {
            get { return 47; }
        }

        public static ushort ProductCode
        {
            get { return 1388; }
        }
    }
}
