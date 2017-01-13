

namespace Konvolucio.MCAN120803.API
{
    using System;

    /// <summary>
    /// Adapter Services
    /// </summary>
    public sealed class CanServices
    {
        internal const byte SID_ATTR_READ = 0x02;
        internal const byte SID_ATTR_WRITE = 0x03;
        internal const byte SID_READ_PENDING = 0x09;
        internal const byte SID_OP_START = 0x17;
        internal const byte SID_OP_STOP = 0x18;
        internal const byte SID_OP_RESET = 0x19;
        internal const byte SID_OP_HARD_RESET = 0x20;
        CanAdapterDevice _adapter;

        /// <summary>
        /// Start communication to adapter.
        /// </summary>
        public void Start()
        {
            Excute(SID_OP_START);
            _adapter.RxCanMsgAbsTime = DateTime.Now.Ticks;
        }

        /// <summary>
        /// Stop communication to adapter.
        /// </summary>
        public void Stop()
        {
            Excute(SID_OP_STOP);
        }

        /// <summary>
        /// All attribute reset.
        /// </summary>
        public void Reset()
        {
            Excute(SID_OP_RESET);
            System.Threading.Thread.Sleep(2);
        }

        /// <summary>
        /// Hardware Reset.
        /// </summary>
        internal void HardReset()
        {
            Excute(SID_OP_HARD_RESET);
        }

        /// <summary>
        /// Read incoming messages from hardware FIFO.
        /// </summary>
        internal void ReadPending()
        {
            Excute(SID_READ_PENDING);
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="adatper"> Adapter instance </param>
        internal CanServices(CanAdapterDevice adatper)
        {
            _adapter = adatper;
        }

        /// <summary>
        /// Execute Service by SID.
        /// </summary>
        /// <param name="sid"> Service Id. </param>
        void Excute(byte sid)
        {
            if (!_adapter.IsConnected)
                throw new CanAdapterException("Adapter is Disconnected. Code:-8604."); //Doc

            lock (_adapter.LockObj)
            {
                UInt16 status = 0;
                byte[] request = new byte[CanAdapterDevice.UsbAttrOutEpSize];

                int bytes = MakeServiceRequest(request, _adapter.FrameCnt, sid);
                _adapter.WriteUsbAttrPipe(request, bytes);

                byte[] response;
                byte cnt;
                _adapter.ReadUsbAttrPipe(out response, out bytes);
                ParseActionResponse(response, out cnt, out status);
                if (_adapter.FrameCnt != cnt)
                    throw new CanAdapterException("Sync Error. Code:-8610."); //Doc

                if ((status & 0x8000) == 0x8000)
                {
                    throw new CanAdapterException("Error." + CanStatus.Decode(status)); //Doc
                }
                else if ((status & 0x4000) == 0x4000)
                {
                    throw new CanAdapterException("Warning." + CanStatus.Decode(status)); //Doc
                }
                _adapter.FrameCnt++;
            }
        }

        /// <summary>
        /// Make Service Request
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="frameCnt"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        internal static int MakeServiceRequest(byte[] buffer, byte frameCnt, byte sid)
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
            buffer[offset] = sid;
            offset += 1;
            Buffer.BlockCopy(new byte[] { 0x00, 0x00, 0x00, 0x00 }, 0, buffer, offset, 4);
            offset += 4;
            buffer[offset] = 0;
            offset += 1;
            Buffer.BlockCopy(new byte[] { 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0, buffer, offset, 8);
            offset += 8;
            return offset;
        }

        /// <summary>
        /// Parse Service Response
        /// </summary>
        /// <param name="buffer"> Buffer </param>
        /// <param name="frameCnt"> Frame Counter </param>
        /// <param name="status"> Status Code </param>
        internal static void ParseActionResponse(byte[] buffer, out byte frameCnt, out UInt16 status)
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
            UInt64 endFrame = (UInt64)BitConverter.ToInt64(buffer, offset);

            if (endFrame != 0x0000feff00020000)
                throw new CanAdapterException("Service Frame Error. Code:-8612."); //Doc

        }
    }
}
