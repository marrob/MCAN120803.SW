// -----------------------------------------------------------------------
// <copyright file="IDigitalOutput.cs" company="">
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
    public class DiscrateInputPointObject : IObject
    {
        public byte ClassCode
        {
            get { return 0x08; }
        }

        public ISlaveDevice SlaveDevice { get; private set; }

        public DiscrateInputPointObject()
        {

        }
    }
}
