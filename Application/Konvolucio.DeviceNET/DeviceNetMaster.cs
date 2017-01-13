// -----------------------------------------------------------------------
// <copyright file="IDeviceNetBus.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.DeviceNet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;
    using Konvolucio.DeviceNet.Common;
    using Konvolucio.MCAN120803.API;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DeviceNetMaster : IDisposable
    {

        public byte MacAddress { get; set; }
        public int Baudrate { get; set; }
        public SlaveDeviceCollection Devices { get; private set; }
        public DeviceExploring Exploring { get; private set; }
        public ConnectedSalveDevice Connected { get; private set; }
        public UnconnectedSalveDevice Unconnected { get; private set; }

        
        CanAdapterDevice Adapter;
        BackgroundWorker Worker;
        bool Disposed = false;
     
        /// <summary>
        /// Constructor
        /// </summary>
        public DeviceNetMaster()
        {
            Adapter = new CanAdapterDevice();
            Devices = new SlaveDeviceCollection(this);
            Exploring = new DeviceExploring(Devices, Adapter);
            Connected = new ConnectedSalveDevice(Adapter, 0);
            Unconnected = new UnconnectedSalveDevice(Adapter);

            Worker = new BackgroundWorker();
            Worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {

                if (Adapter.Attributes.PendingRxMessages > 0)
                {

                }
            }
        }

        /// <summary>
        /// Connect To Can Adapter
        /// </summary>
        /// <param name="serialNumber"></param>
        public void Connect(string serialNumber)
        {
            Adapter.ConnectTo(serialNumber);
            Adapter.Services.Reset();
            Adapter.Attributes.Termination = true;
            //Worker.RunWorkerAsync();
            Adapter.Open(250000);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slaveMacId"></param>
        /// <returns></returns>
        public ISlaveDevice GetSlaveByMacId(int slaveMacId)
        {
            
            foreach(var dev in Devices)
            {
                if(dev.SlaveMacId == slaveMacId)
                    return dev;
            }

            Exploring.RunSync();

            foreach(var dev in Devices)
            {
                if(dev.SlaveMacId == slaveMacId)
                    return dev;
            }

            throw new DeviceNetException("DeviceNet: Slave device not found. SlaveMacId:" + slaveMacId.ToString());
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
                // Free any other managed objects here. 
                if (Adapter != null)
                {
                    Adapter.Dispose();
                    Adapter.Disconnect();
                    Adapter = null;
                }
            }

            // Free any unmanaged objects here. 
            //
            Disposed = true;
        }

    }


}

