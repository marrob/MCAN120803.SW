// -----------------------------------------------------------------------
// <copyright file="DeviceNotSupported.cs" company="">
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
    public class DeviceNotSupported : BaseSlaveDevice
    {
        public IdentityObject Identity { get; private set; }

        public DeviceNotSupported()
        {
            Identity = new IdentityObject(this);
        }

        ushort _VendorId;
        ushort _ProductCode;

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

        //public override ushort VendorId
        //{
        //    get { return VendorId; }
        //}

        //public override ushort ProductCode
        //{
        //    get { return ProductCode; }
        //}

        public DeviceNotSupported(ushort vendorId, ushort productCode)
        {
            this._VendorId = vendorId;
            this._ProductCode = productCode;
        }
    }
}
