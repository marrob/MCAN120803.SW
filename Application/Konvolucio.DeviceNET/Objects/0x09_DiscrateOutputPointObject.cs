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
    public class DiscrateOutputPointObject : IObject
    {
        public Byte ClassCode { get { return 0x09; } }

        //public ClassAttribute<int> Status { get { return new ClassAttribute<int>(); } }

        public DiscreteOutputPointInstance this[int instance]
        {
            get { return Instances[instance]; }
        }

        public List<DiscreteOutputPointInstance> Instances { get; private set; }


        public ISlaveDevice SlaveDevice { get; private set; }

        public DiscrateOutputPointObject(ISlaveDevice device, int instances)
        {
            this.SlaveDevice = device;

            Instances = new List<DiscreteOutputPointInstance>();

            for (byte i = 0; i < instances; i++)
            {
                Instances.Add(new DiscreteOutputPointInstance(this, (byte) (i + 1) ));
            }
        }
    }


    public class DiscreteOutputPointInstance
    {
        public InstanceAttribute<bool> Value { get { return new InstanceAttribute<bool>(obj,  instanceId, 8,  DataType.BOOL); } }
        public InstanceAttribute<bool> FaultAction { get { return new InstanceAttribute<bool>(obj,  instanceId, 5, DataType.BOOL); } }
        
        internal IObject obj;
        internal byte instanceId;

        public DiscreteOutputPointInstance(IObject obj, byte instanceId)
        {
            this.obj = obj;
            this.instanceId = instanceId;
        }
    }
}
