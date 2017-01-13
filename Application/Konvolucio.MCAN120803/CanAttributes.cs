

namespace Konvolucio.MCAN120803.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class CanAttributes
    {
        const UInt32 NC_ATTR_STATE                     = 0x80000009;//Not Used
        const UInt32 NC_ATTR_STATUS                    = 0x8000000A;
        const UInt32 NC_ATTR_BAUD_RATE                 = 0x80000007;
        const UInt32 NC_ATTR_START_ON_OPEN             = 0x80000006;//Not Used
        const UInt32 NC_ATTR_ABS_TIME                  = 0x80000008;//Not Used
        const UInt32 NC_ATTR_PERIOD                    = 0x8000000F;//Not Used
        const UInt32 NC_ATTR_TIMESTAMPING              = 0x80000010;//Not Used
        const UInt32 NC_ATTR_READ_PENDING              = 0x80000011;
        const UInt32 NC_ATTR_WRITE_PENDING             = 0x80000012;
        const UInt32 NC_ATTR_READ_Q_LEN                = 0x80000013;//Not Used
        const UInt32 NC_ATTR_WRITE_Q_LEN               = 0x80000014;//Not Used
        const UInt32 NC_ATTR_RX_CHANGES_ONLY           = 0x80000015;//Not Used
        const UInt32 NC_ATTR_COMM_TYPE                 = 0x80000016;//Not Used
        const UInt32 NC_ATTR_RTSI_MODE                 = 0x80000017;//Not Used
        const UInt32 NC_ATTR_RTSI_SIGNAL               = 0x80000018;//Not Used
        const UInt32 NC_ATTR_RTSI_SIG_BEHAV            = 0x80000019;//Not Used
        const UInt32 NC_ATTR_RTSI_FRAME                = 0x80000020;//Not Used
        const UInt32 NC_ATTR_RTSI_SKIP                 = 0x80000021;//Not Used
        const UInt32 NC_ATTR_LISTEN_ONLY               = 0x80010010;
        const UInt32 NC_ATTR_RX_ERROR_COUNTER          = 0x80010011;
        const UInt32 NC_ATTR_TX_ERROR_COUNTER          = 0x80010012;
        const UInt32 NC_ATTR_SERIES2_COMP              = 0x80010013;
        const UInt32 NC_ATTR_SERIES2_MASK              = 0x80010014;
        const UInt32 NC_ATTR_SERIES2_FILTER_MODE       = 0x80010015;//Not Used
        const UInt32 NC_ATTR_SELF_RECEPTION            = 0x80010016;//Not Used
        const UInt32 NC_ATTR_SINGLE_SHOT_TX            = 0x80010017;
        const UInt32 NC_ATTR_BEHAV_FINAL_OUT           = 0x80010018;
        const UInt32 NC_ATTR_TRANSCEIVER_MODE          = 0x80010019;//Not Used
        const UInt32 NC_ATTR_TRANSCEIVER_EXTERNAL_OUT  = 0x8001001A;//Not Used
        const UInt32 NC_ATTR_TRANSCEIVER_EXTERNAL_IN   = 0x8001001B;//Not Used
        const UInt32 NC_ATTR_SERIES2_ERR_ARB_CAPTURE   = 0x8001001C;//Not Used
        const UInt32 NC_ATTR_TRANSCEIVER_TYPE          = 0x80020007;//Not Used

        const UInt32 M_ATTR_FIRMWARE_REV               = 0x81000001;
        const UInt32 M_ATTR_PCB_REV                    = 0x81000002;
        const UInt32 M_ATTR_DEVICE_NAME                = 0x81000003;

        const UInt32 M_ATTR_LOOPBACK                   = 0x82000001;
        const UInt32 M_ATTR_TERMINATION                = 0x82000002;
        const UInt32 M_ATTR_MSG_GEN_2_HOST             = 0x82000003;
        const UInt32 M_ATTR_MSG_GEN_2_DEV              = 0x82000004;
        const UInt32 M_ATTR_DEV_TOT_RX                 = 0x82000005;
        const UInt32 M_ATTR_DEV_TOT_DROP_RX            = 0x82000006;
        const UInt32 M_ATTR_DEV_TOT_TX                 = 0x82000007;
        const UInt32 M_ATTR_DEV_TOT_DROP_TX            = 0x82000008;

      
        /// <summary>
        /// Get adapter current state.
        /// Default state is SDEV_IDLE.
        /// </summary>
        public CanState State
        {
            get { return (CanState) AttributeU32Read(NC_ATTR_STATE); }
        }

        /// <summary>
        /// 
        /// </summary>
        public Int32 PendingRxMessages
        {
            get 
            {
                if (!_adapter.IsOpen)
                    throw new CanAdapterException("Bus is Closed!");
                _adapter.Services.ReadPending();
                return _adapter.IncomingMsgQueue.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public UInt32 PendingTxMessages
        {
            get { return AttributeU32Read(NC_ATTR_WRITE_PENDING); }
        }

        /// <summary>
        /// 
        /// </summary>
        public UInt32 Baudrate
        {
            get {return AttributeU32Read(NC_ATTR_BAUD_RATE);}
            set{ AttributeU32Write(NC_ATTR_BAUD_RATE, value);}
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ListenOnly
        {
            get { return AttributeU32Read(NC_ATTR_LISTEN_ONLY) == 1; }
            set { AttributeU32Write(NC_ATTR_LISTEN_ONLY, value ? (uint)1 : (uint)0); }
        }

        /// <summary>
        /// 
        /// </summary>
        public UInt32 RxErrorCounter
        {
            get { return (UInt32)AttributeU32Read(NC_ATTR_RX_ERROR_COUNTER); }
        }

        /// <summary>
        /// 
        /// </summary>
        public UInt32 TxErrorCounter
        {
            get { return (UInt32)AttributeU32Read(NC_ATTR_TX_ERROR_COUNTER); }
        }

        /// <summary>
        /// 
        /// </summary>
        public UInt32 Filter
        {
            get { return AttributeU32Read(NC_ATTR_SERIES2_COMP); }
            set { AttributeU32Write(NC_ATTR_SERIES2_COMP, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public UInt32 Mask
        {
            get { return AttributeU32Read(NC_ATTR_SERIES2_MASK); }
            set { AttributeU32Write(NC_ATTR_SERIES2_MASK, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool NonAutoRetransmission
        {
            get { return AttributeU32Read(NC_ATTR_SINGLE_SHOT_TX) == 1; }
            set { AttributeU32Write(NC_ATTR_SINGLE_SHOT_TX, value ? (uint)1 : (uint)0); }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Loopback
        {
            get { return AttributeU32Read(M_ATTR_LOOPBACK) == 1; }
            set { AttributeU32Write(M_ATTR_LOOPBACK, value ? (uint)1 : (uint)0); }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Termination
        {           
            get { return AttributeU32Read(M_ATTR_TERMINATION) == 1; }
            set { AttributeU32Write(M_ATTR_TERMINATION, value ? (uint)1 : (uint)0); }       
        }

        /// <summary>
        /// 
        /// </summary>
        public bool MsgGeneratorToHost
        {
            get { return AttributeU32Read(M_ATTR_MSG_GEN_2_HOST) == 1; }
            set { AttributeU32Write(M_ATTR_MSG_GEN_2_HOST, value ? (uint)1 : (uint)0); }  
        }

        /// <summary>
        /// 
        /// </summary>
        public bool MsgGeneratorToBus
        {
            get { return AttributeU32Read(M_ATTR_MSG_GEN_2_DEV) == 1; }
            set { AttributeU32Write(M_ATTR_MSG_GEN_2_DEV, value ? (uint)1 : (uint)0); }
        }

        /// <summary>
        /// 
        /// </summary>
        public UInt32 RxTotal
        {
            get { return (UInt32)AttributeU32Read(M_ATTR_DEV_TOT_RX); }
        }

        /// <summary>
        /// 
        /// </summary>
        public UInt32 RxDrop
        {
            get { return (UInt32)AttributeU32Read(M_ATTR_DEV_TOT_DROP_RX); }
        }

        /// <summary>
        /// 
        /// </summary>
        public UInt32 TxTotal
        {
            get { return (UInt32)AttributeU32Read(M_ATTR_DEV_TOT_TX); }
        }

        /// <summary>
        /// 
        /// </summary>
        public UInt32 TxDrop
        {
            get { return (UInt32)AttributeU32Read(M_ATTR_DEV_TOT_DROP_TX); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FirmwareRev
        {
            get 
            {
                return AttributeStringRead(M_ATTR_FIRMWARE_REV);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PcbRev
        {
            get
            {
                return AttributeStringRead(M_ATTR_PCB_REV);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DeviceName
        {
            get
            {
                return AttributeStringRead(M_ATTR_DEVICE_NAME);
            }
        }

        /// <summary>
        /// The adapters unique number.
        /// Default eg.:387536633133.
        /// </summary>
        public string SerialNumber
        {
            get 
            {
               return _adapter.Usb.Descriptor.SerialNumber;
            }
        }

        /// <summary>
        /// The Konvolucio.MCAN120803.dll Assembly Version
        /// </summary>
        public string AssemblyVersion
        {
            get 
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                return assembly.GetName().Version.ToString();
            }
        }

        CanAdapterDevice _adapter;
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adatper"></param>
        internal CanAttributes(CanAdapterDevice adatper)
        {
            _adapter = adatper;    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attrId"></param>
        /// <returns></returns>
        internal string AttributeStringRead(UInt32 attrId)
        {
            byte[] data;
            AttributeRead(CanServices.SID_ATTR_READ, attrId , out data);
            string str = System.Text.Encoding.ASCII.GetString(data).Trim('\0');
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attrId"></param>
        /// <returns></returns>
        internal UInt32 AttributeU32Read(UInt32 attrId)
        {
            byte[] data = null;
            UInt32 attrValue = 0;
            AttributeRead(CanServices.SID_ATTR_READ, attrId , out data);
            attrValue = (UInt32)BitConverter.ToInt32(data, 0);
            return attrValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actId"></param>
        /// <param name="attrId"></param>
        /// <param name="data"></param>
        internal void AttributeRead(byte actId, UInt32 attrId, out byte[] data)
        {
            if (!_adapter.IsConnected)
                throw new CanAdapterException("Adapter is Disconnected. Code:-8604."); //Doc
            lock (_adapter.LockObj)
            {
                UInt16 status = 0;
                byte[] response;
                int resBytes = 0;
                byte[] request = new byte[CanAdapterDevice.UsbAttrOutEpSize];
                int reqBytes = 0;
                byte cnt;
                reqBytes = MakeAttriubtRequest(request, _adapter.FrameCnt, actId, attrId, 0, 4);
                _adapter.WriteUsbAttrPipe(request, reqBytes);
                _adapter.ReadUsbAttrPipe(out response, out resBytes);
                ParseAttributeResponse(response, out cnt, out data, out status);
                if (_adapter.FrameCnt != cnt)
                    throw new CanAdapterException("Sync Error. Code:-8610.");
                _adapter.FrameCnt++;
                if ((status & 0x8000) == 0x8000)
                {
                    throw new CanAdapterException("Error." + CanStatus.Decode(status)); //Doc
                }
                else if ((status & 0x4000) == 0x4000)
                {
                    throw new CanAdapterException("Warning." + CanStatus.Decode(status)); //Doc
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attrId"></param>
        /// <param name="value"></param>
        internal void AttributeU32Write(UInt32 attrId, UInt32 value)
        {
            if (!_adapter.IsConnected)
                throw new CanAdapterException("Adapter is Disconnected. Code:-8604."); //Doc
            lock (_adapter.LockObj)
            {
                UInt16 status = 0;
                byte[] buffer = null;
                byte[] response;
                int resBytes = 0;
                byte[] request = new byte[CanAdapterDevice.UsbAttrOutEpSize];
                int reqBytes = 0;
                byte cnt;

                reqBytes = MakeAttriubtRequest(request, _adapter.FrameCnt, CanServices.SID_ATTR_WRITE, attrId, value, 8);
                _adapter.WriteUsbAttrPipe(request, reqBytes);
                _adapter.ReadUsbAttrPipe(out response, out resBytes);
                ParseAttributeResponse(response, out cnt, out buffer, out status);
                if (_adapter.FrameCnt != cnt)
                    throw new CanAdapterException("Sync Error. Code:-8610."); //Doc
                _adapter.FrameCnt++;
                if ((status & 0x8000) == 0x8000)
                {
                    throw new CanAdapterException("Error." + CanStatus.Decode(status)); //Doc
                }
                else if ((status & 0x4000) == 0x4000)
                {
                    throw new CanAdapterException("Warning." + CanStatus.Decode(status)); //Doc
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public List<string> GetAttriubtes()
        {
            List<string> i = new List<string>();
            foreach (PropertyInfo prop in this.GetType().GetProperties())
                i.Add(prop.Name);
            return i;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        [Obsolete]
        public void SetAttributeValueByName(string name, UInt32 value)
        {
            var prop = this.GetType().GetProperties().First(n => n.Name == name);
            prop.SetValue(this, value, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Obsolete]
        public UInt32 GetAttributeValueByName(string name)
        {
            var prop = this.GetType().GetProperties().First(n => n.Name == name);
            return (UInt32)prop.GetValue(this, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="frameCnt"></param>
        /// <param name="actId"></param>
        /// <param name="attrId"></param>
        /// <param name="attrValue"></param>
        /// <param name="attrLenght"></param>
        /// <returns></returns>
        internal static int MakeAttriubtRequest(byte[] buffer, byte frameCnt, byte actId, UInt32 attrId, UInt32 attrValue, Int32 attrLenght)
        {
            int offset = 0;
            buffer[offset] = 0x01;
            offset += 1;
            buffer[offset] = frameCnt;
            offset += 1;
            Buffer.BlockCopy(new byte[] { 0x00, 0x00 }, 0, buffer, offset, 2);
            offset += 2;
            Buffer.BlockCopy(new byte[] { 0x10, 0x00 }, 0, buffer, offset, 2);
            offset += 2;
            buffer[offset] = actId;
            offset += 1;
            Buffer.BlockCopy(new byte[] { 0x00, 0x00, 0x00, 0x00 }, 0, buffer, offset, 4);
            offset += 4;
            buffer[offset] = (byte)attrLenght;
            offset += 1;
            Buffer.BlockCopy(BitConverter.GetBytes(attrId), 0, buffer, offset, 4);
            offset += 4;
            if (attrLenght > 4)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(attrValue), 0, buffer, offset, 4);
                offset += 4;
            }

            Buffer.BlockCopy(new byte[] { 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0, buffer, offset, 8);
            offset += 8;
            return offset;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="frameCnt"></param>
        /// <param name="data"></param>
        /// <param name="status"></param>
        internal static void ParseAttributeResponse(byte[] buffer, out byte frameCnt, out byte[] data, out UInt16 status)
        {
            int offset = 0;
            byte alwaysOne = buffer[offset];
            offset += 1;
            frameCnt = buffer[offset];
            offset += 1;
            UInt16 alwayZero = (UInt16)BitConverter.ToInt16(buffer, offset);
            offset += 2;
            status = (UInt16)BitConverter.ToInt16(buffer, offset);
            offset += 2;
            byte actionId = buffer[offset];
            offset += 1;
            UInt32 unknown = (UInt32)BitConverter.ToInt32(buffer, offset);
            offset += 4;
            byte attrLength = buffer[offset];
            offset += 1;
            UInt32 attrId = (UInt32)BitConverter.ToInt32(buffer, offset);
            offset += 4;
            data = new byte[attrLength-4]; //dataLength = attrLength - attrIdLength 
            Buffer.BlockCopy(buffer, offset, data, 0, attrLength - 4);
            offset += attrLength - 4;
            UInt64 endFrame = (UInt64)BitConverter.ToInt64(buffer, offset);

            if (endFrame != 0x0000feff00020000)
                throw new CanAdapterException("Attr Frame Error. Code:-8611."); //Doc
        }
    }
}
