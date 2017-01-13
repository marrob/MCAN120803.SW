// -----------------------------------------------------------------------
// <copyright file="IIdentityObject.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.DeviceNet.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Konvolucio.DeviceNet.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class IdentityObject : IObject
    {
        public byte ClassCode { get { return 0x01; } }
        public ISlaveDevice SlaveDevice { get; private set; }

        public InstanceAttribute<ushort> VendorId { get { return new InstanceAttribute<ushort>(this, 1, DataType.UINT); } }
        public InstanceAttribute<ushort> DeviceType { get { return new InstanceAttribute<ushort>(this, 2, DataType.UINT); } }
        public InstanceAttribute<ushort> ProductCode { get { return new InstanceAttribute<ushort>(this, 3, DataType.UINT); } }
        public InstanceAttribute<ushort> Revision { get { return new InstanceAttribute<ushort>(this, 4, DataType.UINT); } }
        public InstanceAttribute<ushort> Status { get { return new InstanceAttribute<ushort>(this, 5, DataType.UINT); } }
        public InstanceAttribute<uint> SerialNumber { get { return new InstanceAttribute<uint>(this, 6, DataType.UDINT); } }
        public InstanceAttribute<string> ProductName { get { return new InstanceAttribute<string>(this, 7, DataType.SHORT_STRING); } }
        public InstanceAttribute<byte> HeartbeatInterval { get { return new InstanceAttribute<byte>(this, 10, DataType.USINT); } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="device"></param>
        public IdentityObject(ISlaveDevice device)
        {
            this.SlaveDevice = device;
        }

        /// <summary>
        /// Reset Service
        /// </summary>
        public void Reset()
        {
            SlaveDevice.Master.Connected.ObjectResetService(SlaveDevice.SlaveMacId, ClassCode, 1);
        }

        /// <summary>
        /// Get_Attribute_Single Service
        /// </summary>
        /// <param name="instatnceId"></param>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        public byte[] GetAttributeSingle(byte instatnceId, byte attributeId)
        {
            return SlaveDevice.Master.Connected.GetAttribute(SlaveDevice.SlaveMacId,ClassCode, instatnceId, attributeId);
        }
    }
}
