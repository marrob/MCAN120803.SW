// -----------------------------------------------------------------------
// <copyright file="DeviceExploring.cs" company="">
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
    using System.Threading;
    using System.Diagnostics;
    using Konvolucio.DeviceNet.Common;
    using Konvolucio.DeviceNet.Objects;
    using Konvolucio.MCAN120803.API;
    using Konvolucio.DeviceNet.SlaveDevices;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DeviceExploring : IDisposable
    {
        public event RunWorkerCompletedEventHandler Completed
        {
            remove { BackgroundWorker.RunWorkerCompleted -= value; }
            add { BackgroundWorker.RunWorkerCompleted += value; }
        }

        public event ProgressChangedEventHandler ProgressChange
        {
            add { BackgroundWorker.ProgressChanged += value; }
            remove { BackgroundWorker.ProgressChanged -= value; }
        }

        readonly BackgroundWorker BackgroundWorker;
        readonly AutoResetEvent WaitForDoneEvent;
        readonly AutoResetEvent WaitForDelayEvent;

        readonly SlaveDeviceCollection Devices;
        readonly CanAdapterDevice Adapter;

        bool disposed = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public DeviceExploring(SlaveDeviceCollection devices, CanAdapterDevice adapter)
        {
            this.BackgroundWorker = new BackgroundWorker();
            this.BackgroundWorker.DoWork+=new DoWorkEventHandler(BackgroundWorker_DoWork);
            this.Devices = devices;
            this.Adapter = adapter;
        }

        /// <summary>
        /// Run Async Slave Device Exploring
        /// </summary>
        public void RunAsync()
        {
            BackgroundWorker.WorkerReportsProgress = true;
            BackgroundWorker.WorkerSupportsCancellation = true;
            BackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Run Sync Slave Device Exploring
        /// </summary>
        public void RunSync()
        {
            BackgroundWorker.WorkerReportsProgress = true;
            BackgroundWorker.WorkerSupportsCancellation = true;
            BackgroundWorker_DoWork(null,null);
        }

        /// <summary>
        /// Abort Slave Exploring
        /// </summary>
        public void Abort()
        {
            if (BackgroundWorker.IsBusy)
            {
                WaitForDelayEvent.Set();
                BackgroundWorker.CancelAsync();
                WaitForDoneEvent.WaitOne();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            Stopwatch watch = new Stopwatch();
            watch.Start();

            Adapter.Write(new CanMessage[] { new CanMessage(0x0000, new byte[0]) });
            Thread.Sleep(2000);

            for (int slaveMacId = 0; slaveMacId < 64; slaveMacId++)
            {
                try
                {
                    if (BackgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    long timestamp = DateTime.Now.Ticks;
                    bool isTimeout = false;
                    CanMessage[] rxMsgBuffer = new CanMessage[1];

                    Adapter.Write(new CanMessage[] { new CanMessage(DeviceNetAddressing.GetCheckMacId(slaveMacId), new byte[0]) });

                    do
                    {
                        if (Adapter.Attributes.PendingRxMessages >= 1)
                        {
                            Adapter.Read(rxMsgBuffer, 0, 1);
                            break;
                        }
                        isTimeout = DateTime.Now.Ticks - timestamp > 10 * 10000;

                    } while (!isTimeout);
                    CanMessage responseBuffer = rxMsgBuffer[0];

                    BackgroundWorker.ReportProgress((int)((slaveMacId / 63.0) * 100.0), "STATUS: " + slaveMacId.ToString() + ".");

                    if (!isTimeout)
                    {
                        if (DeviceNetAddressing.IsMacIdCheckCanId(responseBuffer.ArbitrationId))
                        {
                            ushort vendorId = BitConverter.ToUInt16(responseBuffer.Data, 1);
                            uint serialNumber = BitConverter.ToUInt32(responseBuffer.Data, 2);

                            var unconnected = new UnconnectedSalveDevice(Adapter);
                            var connected = new ConnectedSalveDevice(Adapter, 0);
                            unconnected.AllocateExplicitConnection(slaveMacId);
                            ushort productCode = 0;
                            try
                            {
                                productCode = BitConverter.ToUInt16(connected.GetAttribute(slaveMacId, 1, 1, 3), 0);
                                Devices.Add(Devices.CreateDevice(slaveMacId, vendorId, productCode, serialNumber));
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            finally
                            {
                                unconnected.ReleaseExplicitConnection(slaveMacId);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    e.Result = ex;
                }
            }
            watch.Stop();

            if (!BackgroundWorker.CancellationPending)
            {
                BackgroundWorker.ReportProgress(0, "COMPLETE Elapsed:" + (watch.ElapsedMilliseconds / 1000.0).ToString() + "s");
                //e.Result = data;
            }
            else
            {
                BackgroundWorker.ReportProgress(0, "ABORTED Elapsed:" + (watch.ElapsedMilliseconds / 1000.0).ToString() + "s");
            }

 
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
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here. 
                if (BackgroundWorker.IsBusy)
                {
                    BackgroundWorker.CancelAsync();
                    WaitForDoneEvent.WaitOne();
                }
            }

            // Free any unmanaged objects here. 
            //
            disposed = true;
        }
    }
}
