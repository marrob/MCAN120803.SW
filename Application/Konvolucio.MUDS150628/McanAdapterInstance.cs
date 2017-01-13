using System;
using Konvolucio.MCAN120803.API;

namespace Konvolucio.MUDS150628
{
    class CanBusLink : IPhysicalLink, IDisposable
    {
        CanAdapterDevice _adapter = new CanAdapterDevice();
        public bool IsConnected { get { return _adapter.IsConnected; } }
        public UInt32 TransmittId { get; private set; }
        public UInt32 ReceiveId { get; private set; }
        public UInt32 BaudRate { get; private set; }
        
        public bool BusTerminationEnable 
        { 
            get { return _adapter.Attributes.Termination; }
            set { _adapter.Attributes.Termination = value;}
        }
        bool _disposed = false;
        /********************************************************************************/
        public CanBusLink(UInt32 transmittId, UInt32 receiveId, UInt32 baudRate)
        {
            TransmittId = transmittId;
            ReceiveId = receiveId;
            BaudRate = baudRate;

        }
        /********************************************************************************/
        public void Connect()
        {
            _adapter.Connect();
            _adapter.Services.Reset();
        }
        /********************************************************************************/
        public void Disconnect()
        {
            _adapter.Disconnect();
        }
        /********************************************************************************/
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /********************************************************************************/
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _adapter.Dispose();
            }
            _disposed = true;
        }
        /********************************************************************************/
        public void Reset()
        {
            _adapter.Services.Reset();
        }
        /********************************************************************************/
        public void Open()
        {
            if (!_adapter.IsConnected)
                throw new Iso15765Exception("Adapter not Connected!");
            _adapter.Open(BaudRate);
        }
        /********************************************************************************/
        public void Close()
        {
            _adapter.Close();
        }
        /********************************************************************************/
        public void Write(byte[] data, ref string log)
        {
            CanMessage msg = new CanMessage(TransmittId, data);
            _adapter.Write(new CanMessage[] { msg });
            string templog = Tools.TimestampFormat(DateTime.Now.Ticks) + ";" + "Tx:;" + Tools.MsgFormat(msg);
            Console.WriteLine(templog);
            if(log != null)
                log += templog + "\r\n";
        }
        /********************************************************************************/
        public void Read(out byte[] data, int tiemoutMs, ref string log)
        {
            int items = 0;
            CanMessage[] rxFrames = new CanMessage[1];
            long startTick = DateTime.Now.Ticks;
            data = new byte[0];

            bool isFound = false;
            do
            {
                if (DateTime.Now.Ticks - startTick > tiemoutMs * 10000)
                    throw new Iso15765TimeoutException("P2 Max Expired,Read Timeout.");

                items = _adapter.Attributes.PendingRxMessages;
                if (items != 0)
                {
                    for (int i = 0; i < items; i++)
                    {
                        _adapter.Read(rxFrames, 0, 1);
                        CanMessage msg = rxFrames[0];
                        string templog = Tools.TimestampFormat(msg.TimestampTick) + ";" + "Rx:;" + Tools.MsgFormat(msg);
                        Console.WriteLine(templog);
                        if(log !=null)
                            log += templog + "\r\n";
                        if (msg.ArbitrationId == ReceiveId)
                        {
                            isFound = true;
                            data = msg.Data;
                        }
                    }
                }

            } while (!isFound);
        }
    }
}
