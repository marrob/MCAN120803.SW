// -----------------------------------------------------------------------
// <copyright file="ConnectedSalveDevice.cs" company="">
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
    public class ConnectedSalveDevice
    {
              
        CanAdapterDevice Adapter;
        int MasterMacId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="slaveMacId"></param>
        /// <param name="masterMacId"></param>
        public ConnectedSalveDevice(CanAdapterDevice adapter, int masterMacId)
        {
            this.Adapter = adapter;
            this.MasterMacId = masterMacId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slaveMacId"></param>
        /// <param name="classCode"></param>
        /// <param name="instanceId"></param>
        public void ObjectResetService(int slaveMacId, byte classCode, byte instanceId)
        {
            byte[] request = new byte[]
            {
                (byte)MasterMacId,                  /*Source MAC Id: 0..64 */
                Services.SRVS_RESET,                /*Get Attribute Single*/
                classCode,                          /*Class Code*/
                instanceId,                         /*Instance Id*/
            };

            Adapter.Write(new CanMessage[] 
            {
                new CanMessage(DeviceNetAddressing.GetExplicitRequestCanId(slaveMacId), request) 
            });

            int retrty = 3;
            long timestamp = DateTime.Now.Ticks;
            bool isTimeout = false;
            CanMessage[] rxMsgBuffer = new CanMessage[1];
            do
            {
                do
                {
                    if (Adapter.Attributes.PendingRxMessages > 0)
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

            if (isTimeout)
                throw new DeviceNetException("DeviceNet: Response Timeout.");

            byte[] response = rxMsgBuffer[0].Data;

            if (response[1] == 0x94)
                throw new DeviceNetException("DeviceNet:" + DeviceNetException.ErrorMessageToString(response[2], response[3]));

            if ((response[1] & 0x80) != 0x80)
                throw new ApplicationException("DeviceNet:" + "Response Invalid.");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="slaveMacId"></param>
        /// <param name="classCode"></param>
        /// <param name="instanceId"></param>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        public byte[] GetAttribute(int slaveMacId, byte classCode, byte instanceId, byte attributeId)
        {
            bool rxComplete = false;
            byte fragCounter = 0;
            byte[] response = new byte[256];
            int responseLength = 0;

            byte[] request = new byte[]
            {
                (byte)MasterMacId,                  /*Source MAC Id: 0..64 */
                Services.SRVS_GET_ATTRIB_SINGLE,    /*Get Attribute Single*/
                classCode,                          /*Class Code*/
                instanceId,                         /*Instance Id*/
                attributeId,                        /*Attribute Id*/
            };

            Adapter.Write(new CanMessage[] 
            {
                new CanMessage(DeviceNetAddressing.GetExplicitRequestCanId(slaveMacId), request) 
            });

            do
            {
                int retrty = 3;
                long timestamp = DateTime.Now.Ticks;
                bool isTimeout = false;
                CanMessage[] rxMsgBuffer = new CanMessage[1];
                do
                {
                    do
                    {
                        if (Adapter.Attributes.PendingRxMessages > 0)
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

                if (isTimeout)
                    throw new DeviceNetException("DeviceNet: Response Timeout.");

                CanMessage responseBuffer = rxMsgBuffer[0];

                if ((responseBuffer.Data[0] & 0x80) == 0x80)
                { /*Fragmented Response*/

                    int fragmentType = (responseBuffer.Data[1] & 0xC0) >> 6;

                    if (fragmentType == 0x02)
                        rxComplete = true;

                    byte[] ack = new byte[]
                    {
                        (byte)(0x80 | MasterMacId),     /*0x80|MasterMacId*/
                        (byte)(0xC0 | fragCounter),     /*0xC0|FragCounter  -> ACK*/ 
                        0x00
                    };

                    Adapter.Write(new CanMessage[] {
                        new CanMessage(DeviceNetAddressing.GetExplicitRequestCanId(slaveMacId), ack) 
                    });

                    Buffer.BlockCopy(responseBuffer.Data, 2, response, responseLength, responseBuffer.Data.Length - 2);
                    responseLength += responseBuffer.Data.Length - 2;
                    fragCounter++;
                }
                else
                { /*Not Fragmented Response*/
                    Buffer.BlockCopy(responseBuffer.Data, 1, response, 0, responseBuffer.Data.Length - 1);
                    responseLength += responseBuffer.Data.Length - 1;
                    rxComplete = true;
                }

            } while (!rxComplete);

            if (response[0] == 0x94)
                throw new DeviceNetException("DeviceNet:" + DeviceNetException.ErrorMessageToString(response[1], response[2]));

            if ((response[0] & 0x80) != 0x80)
                throw new ApplicationException("DeviceNet:" + "Response Invalid.");

            byte[] value = new byte[responseLength - 1];
            Buffer.BlockCopy(response, 1, value, 0, responseLength - 1);

            return value;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="slaveMacId"></param>
        /// <param name="classCode"></param>
        /// <param name="instanceId"></param>
        /// <param name="attributeId"></param>
        /// <param name="value"></param>
        public void SetAttribute(int slaveMacId, byte classCode, byte instanceId, byte attributeId, byte[] value)
        {
            //bool rxComplete = false;
            //byte fragCounter = 0;

            byte[] requestHeader = new byte[]
            {
                (byte)MasterMacId,                  /*Source MAC Id: 0..64 */
                Services.SRVS_SET_ATTRIB_SINGLE,    /*Set Attribute Single*/
                classCode,                          /*Class Code*/
                instanceId,                         /*Instance Id*/
                attributeId,                        /*Attribute Id*/
            };

            byte[] request = new byte[requestHeader.Length + value.Length];
            Buffer.BlockCopy(requestHeader, 0, request, 0, requestHeader.Length);
            Buffer.BlockCopy(value, 0, request, requestHeader.Length, value.Length);

            Adapter.Write(new CanMessage[] 
            {
                new CanMessage(DeviceNetAddressing.GetExplicitRequestCanId(slaveMacId), request) 
            });

            int retrty = 3;
            long timestamp = DateTime.Now.Ticks;
            bool isTimeout = false;
            CanMessage[] rxMsgBuffer = new CanMessage[1];
            do
            {
                do
                {
                    if (Adapter.Attributes.PendingRxMessages > 0)
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

            if (isTimeout)
                throw new DeviceNetException("DeviceNet: Response Timeout.");

            byte[] response = rxMsgBuffer[0].Data;

            if (response[1] == 0x94)
                throw new DeviceNetException("DeviceNet:" + DeviceNetException.ErrorMessageToString(response[2], response[3]));

            if ((response[1] & 0x80) != 0x80)
                throw new ApplicationException("DeviceNet:" + "Response Invalid.");
        }
    }
}
