
namespace Konvolucio.MCAN120803.API
{
    using System;
    using System.Linq;


    /// <summary>
    /// 
    /// </summary>
    public struct CanMessage
    {
        public UInt16 TwoByte
        {
            get { return twoByte; }
            set { twoByte = value; }
        }
        public long TimestampTick
        {
            get { return timestamp; }
            set { timestamp = value; }
        }
        public UInt32 ArbitrationId
        {
            get { return arbitrationId; }
            set { arbitrationId = value; }
        }
        public byte IsRemote
        {
            get { return isRemote; }
            set { isRemote = value; }
        }
        public byte DataLength
        {
            get { return dataLength; }
            set { dataLength = value; }
        }
        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }

        private UInt16 twoByte;
        private long timestamp;
        private UInt32 arbitrationId;
        private byte isRemote;
        private byte dataLength;
        private byte[] data;
        
        public static CanMessage MessageStdA8
        {
            get
            {
                CanMessage msg = new CanMessage();
                msg.TwoByte = 0x0000;
                msg.TimestampTick = 0x00000000;
                msg.ArbitrationId = 0x000000A8;
                msg.IsRemote = 0x00;
                msg.DataLength = 0x08;
                msg.Data = new byte[] { 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, };
                return msg;
            }
        }
        public static CanMessage MessageStdA4
        {
            get
            {
                CanMessage msg = new CanMessage();
                msg.TwoByte = 0x0000;
                msg.TimestampTick = 0x00000000;
                msg.ArbitrationId = 0x000000A4;
                msg.IsRemote = 0x00;
                msg.DataLength = 0x04;
                msg.Data = new byte[] { 0xA1, 0xA2, 0xA3, 0xA4 };
                return msg;
            }
        }
        public static CanMessage MessageStdA0
        {
            get
            {
                CanMessage msg = new CanMessage();
                msg.TwoByte = 0x0000;
                msg.TimestampTick = 0x00000000;
                msg.ArbitrationId = 0x000000A0;
                msg.IsRemote = 0x00;
                msg.DataLength = 0x00;
                msg.Data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, };
                return msg;
            }
        }
        public static CanMessage MessageStdB8
        {
            get
            {
                CanMessage msg = new CanMessage();
                msg.TwoByte = 0x0000;
                msg.TimestampTick = 0x00000000;
                msg.ArbitrationId = 0x000000B8;
                msg.IsRemote = 0x00;
                msg.DataLength = 0x08;
                msg.Data = new byte[] { 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xB8, };
                return msg;
            }
        }
        public static CanMessage MessageStdC8
        {
            get
            {
                CanMessage msg = new CanMessage();
                msg.TwoByte = 0x0000;
                msg.TimestampTick = 0x00000000;
                msg.ArbitrationId = 0x000000C8;
                msg.IsRemote = 0x00;
                msg.DataLength = 0x08;
                msg.Data = new byte[] { 0xC1, 0xC2, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7, 0xC8, };
                return msg;
            }
        }
        public static CanMessage EmptyMessageStdE0
        {
            get
            {
                CanMessage msg = new CanMessage();
                msg.TwoByte = 0x0000;
                msg.TimestampTick = 0x00000000;
                msg.ArbitrationId = 0x00000000;
                msg.IsRemote = 0x00;
                msg.DataLength = 0x00;
                msg.Data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, };
                return msg;
            }
        }
        public static CanMessage EmptyMessageExtE0
        {
            get
            {
                CanMessage msg = new CanMessage();
                msg.TwoByte = 0x0000;
                msg.TimestampTick = 0x00000000;
                msg.ArbitrationId = 0x00000000 | 0x20000000;
                msg.IsRemote = 0x00;
                msg.DataLength = 0x00;
                msg.Data = new byte[0];
                return msg;
            }
        }         
        public static CanMessage MessageMaxStdId
        {
            get
            {
                CanMessage msg = new CanMessage();
                msg.TwoByte = 0x0000;
                msg.TimestampTick = 0x00000000;
                msg.ArbitrationId = 0x000007FF;
                msg.IsRemote = 0x00;
                msg.DataLength = 0x08;
                msg.Data = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
                return msg;
            }
        }
        public static CanMessage MessageMaxExtId
        {
            get
            {
                CanMessage msg = new CanMessage();
                msg.TwoByte = 0x0000;
                msg.TimestampTick = 0x00000000;
                msg.ArbitrationId = 0x1FFFFFFF | 0x20000000;
                msg.IsRemote = 0x00;
                msg.DataLength = 0x08;
                msg.Data = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
                return msg;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arbitrationId"></param>
        /// <param name="data"></param>
        public CanMessage(UInt32 arbitrationId, byte[] data)
        {

            twoByte = 0;
            timestamp = 0;
            this.arbitrationId = arbitrationId;
            isRemote = 0;
            dataLength = (byte)data.Length;
            this.data = new byte[data.Length];

            if (data != null)
            {
                if (data.Length > 8)
                {
                    throw new CanAdapterException("CAN Message cant be longer than 8 byte.");
                }
                else
                {
                    Buffer.BlockCopy(data, 0, this.data, 0, data.Length); 
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRemote"></param>
        /// <param name="arbitrationId"></param>
        /// <param name="data"></param>
        public CanMessage(byte isRemote, UInt32 arbitrationId, byte[] data)
        {
            twoByte = 0;
            timestamp = 0;
            this.arbitrationId = arbitrationId;
            this.isRemote = isRemote;
            dataLength = (byte)data.Length;
            this.data = new byte[8];

            if (data != null)
            {
                if (data.Length > 8)
                {
                    throw new CanAdapterException("CAN Message cant be longer than 8 byte.");
                }
                else
                {
                    Buffer.BlockCopy(data, 0, this.data, 0, data.Length);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }
            // If parameter cannot be cast to MctComponentItem return false.
            CanMessage p = (CanMessage)obj;
            if ((System.Object)p == null)
            {
                return false;
            }

            if (p.arbitrationId == this.arbitrationId &&
                p.IsRemote == this.IsRemote &&
                p.DataLength == this.DataLength)
            {
                if (p.data == null ^ this.data == null)
                    return false;
                else
                    if (p.data == null && this.data == null)
                        return true;
                    else
                        if (p.Data.Length != this.Data.Length)
                            return false;
                        else
                            if (p.Data.SequenceEqual(this.Data))
                                return true;
                            else
                                return false;
            }
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(CanMessage a, CanMessage b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Equals(b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(CanMessage a, CanMessage b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = string.Empty;

            if(TimestampTick != 0)
                str += new DateTime(TimestampTick).ToString("dd.MM.yyyy HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture) + " "; 

            if ((ArbitrationId & 0x20000000) == 0x20000000)
            { //ExtId
                str += string.Format("0x{0:X8}", TimestampTick) + " ";
                str += string.Format("0x{0:X8}*", ArbitrationId & 0x1FFFFFFF) + " ";
                str += string.Format("0x{0:X2}", IsRemote) + " ";
                str += string.Format("0x{0:X2}", DataLength) + " ";
            }
            else
            { //StdId
                str += string.Format("0x{0:X8}", TimestampTick) + " ";
                str += string.Format("0x{0:X8}", ArbitrationId ) + " ";
                str += string.Format("0x{0:X2}", IsRemote) + " ";
                str += string.Format("0x{0:X2}", DataLength) + " ";
            }

            if (Data != null && Data.Length != 0)
            {
                for (int i = 0; i < Data.Length; i++)   
                    str += string.Format("0x{0:X2}", Data[i]) + " ";
            }
            str = str.TrimEnd();
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="size"></param>
        /// <param name="frames"></param>
        internal static void MsgPacketToCanFrames(byte[] buffer, int size, out CanMessage[] frames)
        {
            CanMessage[] list = null;
            int offset;
            byte alwaysZero;
            UInt16 alwaysOne;
            int msgCnt;

            if(size == 0)
            {
                frames = new CanMessage[0];
                return;
            }
            offset = 0;
            msgCnt = buffer[offset];
            list = new CanMessage[msgCnt];
            offset += 1;
            alwaysZero = buffer[offset];
            offset += 1;
            alwaysOne = (UInt16)BitConverter.ToInt16(buffer, offset);
            offset += 2;

            for (int i = 0; i < msgCnt; i++)
            {
                list[i].TwoByte = (UInt16)BitConverter.ToInt16(buffer, offset);
                offset += 2;
                list[i].TimestampTick = (UInt32)BitConverter.ToInt32(buffer, offset);
                offset += 4;
                list[i].ArbitrationId = (UInt32)BitConverter.ToInt32(buffer, offset);
                offset += 4;
                list[i].IsRemote = buffer[offset];
                offset += 1;
                list[i].DataLength = buffer[offset];
                offset += 1;
                list[i].Data = new byte[list[i].DataLength];
                Buffer.BlockCopy(buffer, offset, list[i].Data, 0, list[i].DataLength);
                offset += 8;
            }
            frames = list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frames"></param>
        /// <param name="buffer"></param>
        /// <param name="size"></param>
        internal static void CanFramesToMsgPacket(CanMessage[] frames, byte[] buffer, out int size)
        {
            int offset;
            UInt16 olwaysOne;

            olwaysOne = 1;
            offset = 0;
            buffer[offset] = (byte)frames.Length;
            offset += 1;
            buffer[offset] = 0x00;
            offset += 1;
            Buffer.BlockCopy(BitConverter.GetBytes(olwaysOne), 0, buffer, offset, 2);
            offset += 2;

            for (int i = 0; i < frames.Length; i++)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(frames[i].TwoByte), 0, buffer, offset, 2);
                offset += 2;
                Buffer.BlockCopy(BitConverter.GetBytes(frames[i].TimestampTick), 0, buffer, offset, 4);
                offset += 4;
                Buffer.BlockCopy(BitConverter.GetBytes(frames[i].ArbitrationId), 0, buffer, offset, 4);
                offset += 4;
                buffer[offset] = frames[i].IsRemote;
                offset += 1;
                buffer[offset] = frames[i].DataLength;
                offset += 1;
                if (frames[i].Data != null && frames[i].DataLength != 0)
                {
                    Buffer.BlockCopy(frames[i].Data, 0, buffer, offset, frames[i].DataLength);
                }
                else
                {
                    byte[] emptyData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, };
                    Buffer.BlockCopy(emptyData, 0, buffer, offset, frames[i].DataLength);
                }
                offset += 8;
            }
            size = offset;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="msgInPacket"></param>
        /// <param name="buffer"></param>
        /// <param name="size"></param>
        internal static void QueueToMsgPacket(SafeQueue<CanMessage> queue, int msgInPacket, out byte[] buffer, out int size)
        {
            int items = queue.Count;
            CanMessage[] frames;

            if (items != 0)
            {
                if (items > msgInPacket)
                {
                    frames = new CanMessage[msgInPacket];
                    for (int i = 0; i < msgInPacket; i++)
                        frames[i] = queue.Dequeue();
                }
                else
                {
                    frames = new CanMessage[items];
                    for (int i = 0; i < items; i++)
                        frames[i] = queue.Dequeue();
                }
                buffer = new byte[64];
                CanMessage.CanFramesToMsgPacket(frames, buffer, out size);
            }
            else
            {
                buffer = new byte[0];
                size = 0;
            }
        }
    }

}
