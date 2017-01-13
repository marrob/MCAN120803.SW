// -----------------------------------------------------------------------
// <copyright file="DeviceNetObject.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.DeviceNET.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Konvolucio.DeviceNet.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DeviceNetObject
    {
        public byte ClassCode { get { return 0x03; } }
        public ISlaveDevice SlaveDevice { get; private set; }

        //public InstanceAttribute<ushort> MACID { get { return new InstanceAttribute<ushort>(this, 1, DataType.UINT); } }
         
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="device"></param>
        public DeviceNetObject(ISlaveDevice device)
        {
            this.SlaveDevice = device;
        }
    }
}
