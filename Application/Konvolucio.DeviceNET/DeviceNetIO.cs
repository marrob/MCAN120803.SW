// -----------------------------------------------------------------------
// <copyright file="DeviceNetEngine.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.DeviceNet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Konvolucio.MCAN120803.API;

    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceNetIO : IDisposable
    {

        byte[] GetAttribute(byte soruceMacId, byte destinationMacId, byte classId, byte instanceId, byte attributeId);
        void Connect(string serialNumber);
        void Open(uint baudrate);


        void AllocateMaster(byte sMacId, byte dMacId, byte allocationChoiceByte);
        void ReleaseMaster(byte sMacId, byte dMacId);
    }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DeviceNetIO : IDeviceNetIO
    {
        CanAdapterDevice Adapter;
        public bool disposed;


        public const int FRAG_BIT_POS_IN_BYTE       = 0x80;
        public const int XID_BIT_POS_IN_BYTE        = 0x40;

        public const byte SOURCE_ADDR_POS_IN_FRAME  = 0x00;
        public const byte FRAG_COUNTER_POS_IN_FRAME = 0x01;

        /// <summary>
        /// 
        /// </summary>
        public DeviceNetIO()
        {
            Adapter = new CanAdapterDevice();
        }

        /// <summary>
        /// 
        /// </summary>
        public void AllocateMaster(byte sMacId, byte dMacId, byte allocationChoiceByte)
        {
            byte[] request = new byte[]
            {
                sMacId,                            //Source MAC Id
                Services.SRVS_ALLOCATE_CONN,       //SRVS_ALLOCATE_CONN
                0x03,                              //Class Id
                0x01,                              //Instance Id
                allocationChoiceByte,              //Attribute Id
                0x00,
            };

            byte[] response = RequestResponse(sMacId, dMacId, request, true);

            if (response[1] != (byte)(0x80 + Services.SRVS_ALLOCATE_CONN) && response[1] != 0x94)
                throw new ApplicationException("Invlaid Response");
            if (response[1] == 0x94)
                throw new ApplicationException(ErrorMessageToString(response[2], response[3]));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sMacId"></param>
        /// <param name="dMacId"></param>
        /// <param name="allocationChoiceByte"></param>
        public void ReleaseMaster(byte sMacId, byte dMacId)
        {
            byte[] request = new byte[]
            {
                sMacId,                            //Source MAC Id
                Services.SRVS_RELEASE_CONN,        //SRVS_RELEASE_CONN
                0x03,                              //Class Id
                0x01,                              //Instance Id
                0x01,                              //Value
            };

            byte[] response = RequestResponse(sMacId, dMacId, request, true);

            if (response[1] != (byte)(0x80 | Services.SRVS_RELEASE_CONN) && response[1] != 0x94)
                throw new ApplicationException("Invlaid Response");
            if (response[1] == 0x94)
                throw new ApplicationException(ErrorMessageToString(response[2], response[3]));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sMacId"></param>
        /// <param name="dMacId"></param>
        /// <param name="classId"></param>
        /// <param name="instId"></param>
        /// <param name="attrId"></param>
        /// <returns></returns>
        public byte[] GetAttribute(byte sMacId, byte dMacId,  byte classId, byte instId, byte attrId)
        {
            byte[] request = new byte[]
            {
                sMacId,                             //Source MAC Id
                Services.SRVS_GET_ATTRIB_SINGLE,    //Get Attribute Single
                classId,                            //Class Id
                instId,                             //Instance Id
                attrId,                             //Attribute Id
            };
            byte[] response = RequestResponse(sMacId, dMacId, request,false);

            if (response[1] != (byte)(0x80 | Services.SRVS_GET_ATTRIB_SINGLE) && response[1] != 0x94)
                throw new ApplicationException("Invlaid Response");
            if (response[1] == 0x94)
                throw new ApplicationException(ErrorMessageToString(response[2], response[3]));

            byte[] value = new byte[response.Length - 2];
            Buffer.BlockCopy(response, 2, value, 0, response.Length - 2);
            return value;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sMacId"></param>
        /// <param name="dMacId"></param>
        /// <param name="classId"></param>
        /// <param name="instanceId"></param>
        /// <param name="data"></param>
        public void SetAttribute(byte sMacId, byte dMacId, byte classId, byte instanceId, byte[] data)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serialNumber"></param>
        public void Connect(string serialNumber)
        {
            Adapter.ConnectTo(serialNumber);
            Adapter.Services.Reset();
            Adapter.Attributes.Termination = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baudrate"></param>
        public void Open(uint baudrate)
        {
            if (!Adapter.IsConnected)
                throw new ApplicationException("Adapter not Connected!");
            Adapter.Open(baudrate);
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
                Adapter.Disconnect();
            }
            disposed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dAddr"></param>
        /// <returns></returns>
        public static UInt16 GetMasterExplicitRequestAddress(byte dAddr)
        {
            UInt16 retval = 0;
            retval |= 2 << 9;
            retval |= (UInt16)(dAddr << 3);
            retval |= 4;
            return retval;
        }

        /// <summary>
        /// A válasz egyezik az MasterExplicitResponseAddress-címével.
        /// </summary>
        /// <param name="dAddr"></param>
        /// <returns></returns>
        public static UInt16 GetMasterUnconnctedExplicitRequestAddress(byte dAddr)
        {
            UInt16 retval = 0;
            retval |= 2 << 9;
            retval |= (UInt16)(dAddr << 3);
            retval |= 6;
            return retval;       
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dAddr"></param>
        /// <returns></returns>
        public static UInt16 GetMasterExplicitResponseAddress(byte dAddr)
        {
            UInt16 retval = 0;
            retval |= 2 << 9;                   //Group 2
            retval |= (UInt16)(dAddr << 3);     //Destination MAC ID
            retval |= 3;                        //Message ID
            return retval;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] RequestResponse(byte sMacId, byte dMacId, byte[] request, bool isUnconnected)
        {
            bool rxComplete = false;
            byte fragCounter = 0;
            byte[] response = new byte[8];
            int responseLength = 0;

            string log = string.Empty;

            CanMessage txmsg;
            if (isUnconnected)
                txmsg = new CanMessage(GetMasterUnconnctedExplicitRequestAddress(dMacId), request);
            else
                txmsg = new CanMessage(GetMasterExplicitRequestAddress(dMacId), request);
            Adapter.Write(new CanMessage[] { txmsg });

            do
            {
                var rxMsgBuffer = new CanMessage[1];
                bool isTimeout = false;
                long timestamp = DateTime.Now.Ticks;

                do
                {
                    if (Adapter.Attributes.PendingRxMessages >= 1)
                    {
                        Adapter.Read(rxMsgBuffer, 0, 1);
                        break;
                    }
                    isTimeout = DateTime.Now.Ticks - timestamp > 1000 * 10000;

                } while (!isTimeout);

                if (isTimeout)
                    throw new ApplicationException("Timoeut");

                //Console.WriteLine(string.Format("Frag: 0x{0:X2}", rxMsgBuffer[0].Data[SOURCE_ADDR_POS_IN_FRAME]));

                if ((rxMsgBuffer[0].Data[SOURCE_ADDR_POS_IN_FRAME] & FRAG_BIT_POS_IN_BYTE) == FRAG_BIT_POS_IN_BYTE)
                {
                    //Console.WriteLine(string.Format("Frag Counter: 0x{0:X2}", rxMsgBuffer[0].Data[FRAG_COUNTER_POS_IN_FRAME]));

                    switch ((rxMsgBuffer[0].Data[FRAG_COUNTER_POS_IN_FRAME] & 0xC0) >> 6)
                    {
                        case 0x00: { /*Console.WriteLine("First Fragment");*/ break; }
                        case 0x01: { /*Console.WriteLine("Middle Fragment");*/ break; }
                        case 0x02:
                            {
                                rxComplete = true;
                                /*Console.WriteLine("Last Fragment");*/
                                break;
                            }

                        default:
                            {
                                throw new ApplicationException("Invlaid Response");
                            }
                    }
                    //-------
                    byte[] ack = new byte[]
                    {
                        (byte)(0x80 | sMacId),
                        (byte)((3 << 6) | fragCounter),
                        0x00
                    };
                    txmsg = new CanMessage(GetMasterExplicitRequestAddress(dMacId), ack);
                    Adapter.Write(new CanMessage[] { txmsg });
                    //-------                    
                    if (responseLength == 0)
                    {//Beszurja a SourceMacId-t az fragmentált üzenet elejére
                        responseLength = 1;
                        Array.Resize(ref response, responseLength);
                        response[0] = rxMsgBuffer[0].Data[0];
                    }

                    Array.Resize(ref response, responseLength + rxMsgBuffer[0].Data.Length - 2);
                    Buffer.BlockCopy(rxMsgBuffer[0].Data, 2, response, responseLength, rxMsgBuffer[0].Data.Length - 2);
                    responseLength += rxMsgBuffer[0].Data.Length - 2;
                    fragCounter++;
                }
                else
                {
                    Buffer.BlockCopy(rxMsgBuffer[0].Data, 0, response, 0, rxMsgBuffer[0].Data.Length);
                    rxComplete = true;
                }
            } while (!rxComplete);
            return response;
        }


        /// <summary>
        /// B-1 General Status Codes Edition 3.3
        /// Page: 1223
        /// </summary>
        /// <param name="generalErrorCode"></param>
        /// <param name="adittionalCode"></param>
        /// <returns></returns>
        static string ErrorMessageToString(byte generalErrorCode, byte adittionalCode)
        {
            string retval = string.Empty;
            switch (generalErrorCode)
            {
                case 0x08: { retval = "Service Not Supported"; break; }
                case 0x0B: { retval = "Already in requested mode/state"; break; }
                case 0x16: { retval = "Object does not exist"; break; }
                default: { retval = "Unknwon Error Code"; break; }
            }

            retval += string.Format(" [Error Code:0x{0:X2} - Adittional Code:0x{1:X2}]", generalErrorCode, adittionalCode);
            return retval;
        }
    }
}
