// -----------------------------------------------------------------------
// <copyright file="UnconnectedSalveDevice.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.DeviceNet.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Konvolucio.MCAN120803.API;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UnconnectedSalveDevice
    {
        CanAdapterDevice Adapter;

        public UnconnectedSalveDevice(CanAdapterDevice adapter)
        {
            this.Adapter = adapter;
        }

        /// <summary>
        /// 
        /// </summary>
        public void AllocateExplicitConnection(int slaveMacId)
        {
            Adapter.Write(new CanMessage[] 
            {
                new CanMessage(DeviceNetAddressing.GetUnconnctedExplicitRequestCanId(slaveMacId), new byte[]{0x00, 0x4B, 0x03, 0x01, 0x01, 0x00}) 
            });

            int retrty = 3;
            long timestamp = DateTime.Now.Ticks;
            bool isTimeout = false;
            CanMessage[] rxMsgBuffer = new CanMessage[1];
            do
            {
                do
                {
                    if (Adapter.Attributes.PendingRxMessages > 0 )
                    {
                        isTimeout = false;
                        Adapter.Read(rxMsgBuffer, 0, 1);
                        break;
                    }
                    isTimeout = DateTime.Now.Ticks - timestamp > 1000 * 10000;
                } while (!isTimeout);

                if (isTimeout && retrty != 0)
                {
                    timestamp = DateTime.Now.Ticks;
                }
            } while (--retrty != 0 && isTimeout);

            CanMessage response = rxMsgBuffer[0];

            if (isTimeout)
                throw new DeviceNetException("DeviceNet: Response Timeout.");

            if (response.Data[1] == 0x94)
                throw new DeviceNetException("DeviceNet:" + DeviceNetException.ErrorMessageToString(response.Data[2], response.Data[3]));

            if ((response.Data[1] & 0x80) != 0x80)
                throw new ApplicationException("DeviceNet:" + "Response Invalid.");
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReleaseExplicitConnection(int slaveMacId)
        {
            Adapter.Write(new CanMessage[] 
            {
                new CanMessage(DeviceNetAddressing.GetUnconnctedExplicitRequestCanId(slaveMacId), new byte[]{0x00, 0x4C, 0x03, 0x01, 0x01}) 
            });

            long timestamp = DateTime.Now.Ticks;
            bool isTimeout = false;
            CanMessage[] rxMsgBuffer = new CanMessage[1];
            do
            {
                if (Adapter.Attributes.PendingRxMessages >= 1)
                {
                    Adapter.Read(rxMsgBuffer, 0, 1);
                    break;
                }
                isTimeout = DateTime.Now.Ticks - timestamp > 10 * 10000;

            } while (!isTimeout);

            CanMessage response = rxMsgBuffer[0];

            if (isTimeout)
                throw new DeviceNetException("DeviceNet: Response Timeout.");

            if (response.Data[1] == 0x94)
                throw new DeviceNetException("DeviceNet:" + DeviceNetException.ErrorMessageToString(response.Data[2], response.Data[3]));

            if ((response.Data[1] & 0x80) != 0x80)
                throw new ApplicationException("DeviceNet:" + "Response Invalid.");

        }
    }
}
