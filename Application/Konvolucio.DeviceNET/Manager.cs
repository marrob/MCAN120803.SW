// -----------------------------------------------------------------------
// <copyright file="KonwonDevices.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.DeviceNet.SlaveDevices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Konvolucio.DeviceNet.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Manager
    {

        public Manager()
        {

        }

        public DeviceFind GetDevice { get { return new DeviceFind(); } }

        List<SlaveDeviceCollection> devices = new List<SlaveDeviceCollection>()
        {
            //new DeviceGTR1(),
        };

        public List<ISlaveDevice> GetOnLineDevices
        {
            get
            {
                return null;
            }
        }


        public class DeviceFind
        {
            public ISlaveDevice this[int i]
            {
                get
                {
                    return null;
                }
            }
        }
    }
}
