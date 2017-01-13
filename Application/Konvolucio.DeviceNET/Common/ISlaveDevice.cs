// -----------------------------------------------------------------------
// <copyright file="IDeviceNet.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.DeviceNet.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Konvolucio.DeviceNet.Objects;
    using Konvolucio.MCAN120803.API;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ISlaveDevice: IDisposable
    {
        int SlaveMacId { get; }
        DeviceNetMaster Master { get; }
        ObjectCollection Objects { get; }
        
        ushort GetExplicitResponseCanId { get; }
        ushort GetExplicitRequestCanId { get; }
        ushort GetUnconnctedExplicitRequestCanId { get; }
        ushort GetDuplicateMacCheckCanId { get; }
    }


    public class BaseSlaveDevice : ISlaveDevice
    {

        public virtual int SlaveMacId { get; internal set; }
        public virtual uint SerialNumber { get; internal set; }

        public bool Disposed { get; private set; }

        public DeviceNetMaster Master
        {
            get;
            internal set;
        }

        public virtual ObjectCollection Objects
        {
            get { throw new NotImplementedException(); }
        }

        //public virtual ushort VendorId
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //public virtual ushort ProductCode
        //{
        //    get { throw new NotImplementedException(); }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="macAddress"></param>
        public BaseSlaveDevice(int macAddress)
        {
            SlaveMacId = macAddress;
        }

        public SafeQueue<CanMessage> ExplicitTxQueue;
        public SafeQueue<CanMessage> ExplicitRxQueue;

        public BaseSlaveDevice()
        {
            ExplicitRxQueue = new SafeQueue<CanMessage>();
            ExplicitTxQueue = new SafeQueue<CanMessage>();
        }

        public ushort GetExplicitResponseCanId
        {
            get { return DeviceNetAddressing.GetExplicitResponseCanId(SlaveMacId); }
        }

        public ushort GetExplicitRequestCanId
        {
            get { return DeviceNetAddressing.GetExplicitRequestCanId(SlaveMacId); }
        }

        public ushort GetUnconnctedExplicitRequestCanId
        {
            get { return DeviceNetAddressing.GetUnconnctedExplicitRequestCanId(SlaveMacId); }
        }

        public ushort GetDuplicateMacCheckCanId
        {
            get { return DeviceNetAddressing.GetDuplicateMacCheckCanId(SlaveMacId); }
        }


        public void OpenExplicitConnection()
        {

        
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                ExplicitTxQueue.Clear();
                ExplicitRxQueue.Clear();
            }
            Disposed = true;
        }
    }


}
