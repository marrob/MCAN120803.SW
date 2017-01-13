// -----------------------------------------------------------------------
// <copyright file="Attribute.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.DeviceNet.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class InstanceAttribute<T>
    {
        public byte InstanceId { get; private set; }
        public byte AttributeId { get; private set; }
        public DataType Type { get; private set; }
        internal byte ClassCode { get; private set; }
        internal ConnectedSalveDevice ConnectedDevice { get; private set; }
        internal int SlaveMacId { get; private set; }

        public T Value
        {
            get
            {
                byte[] value = ConnectedDevice.GetAttribute(SlaveMacId, ClassCode, InstanceId, AttributeId);

                switch (Type)
                {
                    case DataType.BOOL: 
                        { /* 1 byte */
                            return (T)Convert.ChangeType(true, typeof(T));
                        }
                    case DataType.UINT:
                        { /* 2 byte unsigned */
                            return (T)Convert.ChangeType(BitConverter.ToUInt16(value, 0), typeof(T));
                        }
                    case DataType.UDINT:
                        { /* 4 byte signed */
                            return (T)Convert.ChangeType(BitConverter.ToUInt32(value, 0), typeof(T));
                        }
                    case DataType.SHORT_STRING: 
                        { /*  */
                            string str = Encoding.UTF8.GetString(value, 1, value[0]);
                            return (T)Convert.ChangeType(str, typeof(T));
                        }
                    default:
                        {
                            return (T)Convert.ChangeType(false, typeof(T));
                        }

                }
            }

            set
            {
                switch (Type)
                {
                    case DataType.BOOL:
                        {
                            break;
                        }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="instanceId"></param>
        /// <param name="attributeId"></param>
        /// <param name="type"></param>
        public InstanceAttribute(IObject obj, byte instanceId, byte attributeId, DataType type)
        {
            this.ConnectedDevice = obj.SlaveDevice.Master.Connected;
            this.ClassCode = obj.ClassCode;
            this.SlaveMacId = obj.SlaveDevice.SlaveMacId;
            this.AttributeId = attributeId;
            this.InstanceId = instanceId;
            this.Type = type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="instanceId"></param>
        /// <param name="attributeId"></param>
        /// <param name="type"></param>
        public InstanceAttribute(IObject obj, byte attributeId, DataType type)
        {
            this.ConnectedDevice = obj.SlaveDevice.Master.Connected;
            this.ClassCode = obj.ClassCode;
            this.SlaveMacId = obj.SlaveDevice.SlaveMacId;
            this.AttributeId = attributeId;
            this.InstanceId = 1;
            this.Type = type;
        }
    }
}
