
namespace Konvolucio.MCAN120803.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WinUSB;
    using System.Threading;


    /// <summary>
    /// 
    /// </summary>
    public interface ICanAdapterDevice
    {
        bool IsConnected { get; }
        bool IsOpen { get; }
        Exception LasException { get; }
        CanAttributes Attributes { get; }
        CanServices Services { get; }
        void ConnectTo(CanAdapterItem adapter);
        void Connect();
        void ConnectTo(string serialNumber);
        void Disconnect();
        void Write(CanMessage[] frameBuffer);
        int Read(CanMessage[] frames, int offset, int length);
        void Flush();
        void Open(UInt32 baudrate);
        void Open(CanBaudRateCollection.BaudRateItem baudrate);
        void Close();
        void Dispose();
    }

    public class CanAdapterDevice: ICanAdapterDevice, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public Exception LasException { get; private set; }

        /// <summary>
        /// Gets a value indicating the connected or disconnected status of the MCanAdapter object.
        /// </summary>
        public bool IsConnected
        {
            get { return _isConnected; }
        }
        internal bool _isConnected;

        /// <summary>
        /// Gets a value indicating the open or closed status of the MCanAdapter object.
        /// </summary>
        public bool IsOpen
        {
            get { return _isOpen; }
        }
        bool _isOpen = false;

        /// <summary>
        /// Adapter attributes.
        /// </summary>
        public CanAttributes Attributes { get { return _Attributes; } }
        readonly CanAttributes _Attributes;

        /// <summary>
        /// Adapter actions.
        /// </summary>
        public CanServices Services { get { return _Services; } }
        readonly CanServices _Services;

        internal const byte USB_ATTR_IN_ADDR = 0x81;
        internal static int UsbAttrInEpSzie = 0x40;

        internal const byte USB_ATTR_OUT_ADDR = 0x01;
        internal static int UsbAttrOutEpSize = 0x40;

        internal const byte USB_MSG_IN_ADDR = 0x82;
        internal static int UsbMsgInEpSize = 0x40;
        
        internal const byte USB_MSG_OUT_ADDR = 0x02;
        internal static int UsbMsgOutEpSize = 0x40;

        internal const int MaxCanMsgInPacket = 3; //3xCAN frame(20byte) + 1xHeader(4byte) = 64byte
        
        internal delegate void WriteUsbAttributPipeDelegate(byte[] data, int length);
        internal delegate void ReadUsbAttributePipeDelegate(out byte[] data, out int length);

        internal WriteUsbAttributPipeDelegate WriteUsbAttrPipe;
        internal ReadUsbAttributePipeDelegate ReadUsbAttrPipe;

        internal USBDevice Usb;
        internal byte FrameCnt;

        internal SafeQueue<CanMessage> IncomingMsgQueue;
        internal SafeQueue<CanMessage> OutgoingMsgQueue;
        internal long RxCanMsgAbsTime;

        bool isAbort = false;
        internal readonly object LockObj;
        internal readonly object WriteLockObj;

        byte[] rxMsgPacketBuffer = new byte[64];
        AsyncCallback _asynReadCpltCallback;
        IAsyncResult _asyncReadResult;

        CanAsyncResult _asyncReadBeginResult;
        CanMessage[] _readBeginFrameArray;
        volatile int _readBeginLength;
        volatile int _readBeginOffset;

        Mutex _mutex = null;
        bool _disposed = false;


        /// <summary>
        /// Initializes a new instance of the MCanAdapter class.
        /// </summary>
        public CanAdapterDevice()
        {
            _Attributes = new CanAttributes(this);
            _Services = new CanServices(this);
            LockObj = new object();
            WriteLockObj = new object();
            WriteUsbAttrPipe = new WriteUsbAttributPipeDelegate(WriteUsbAttributePipe);
            ReadUsbAttrPipe = new ReadUsbAttributePipeDelegate(ReadUsbAttributePipe);
        }

        /// <summary>
        /// Specifies the list of adapters connected to a Host. 
        /// </summary>
        /// <returns>Adapaters list.</returns>
        public static List<CanAdapterItem> GetAdapters()
        {
            USBDeviceInfo[] dies = USBDevice.GetDevices("{AA40624D-0F4B-4F4F-8E23-BA4209EE3AF2}");         
            List<CanAdapterItem> adapters = new List<CanAdapterItem>();
            foreach (USBDeviceInfo info in dies)
            { 
                var adapter = new CanAdapterItem();
                adapter.DevicePath = info.DevicePath;

                adapter.SerialNumber = info.DevicePath.ToUpper().Split('#')[2];
                adapter.DeviceDescription = info.DeviceDescription;
                try
                {
                    Mutex mut = Mutex.OpenExisting(adapter.DeviceDescription + " " + adapter.SerialNumber);
                    adapter.InUse = true;
                    mut.Close();
                    mut = null;
                }
                catch (WaitHandleCannotBeOpenedException)
                {
                    adapter.InUse = false;
                }
                adapters.Add(adapter);
            }

            return adapters;
        }

        /// <summary>
        /// Connecting to a specific adapter.
        /// </summary>
        /// <param name="adapter">The adapter.</param>
        /// <exception cref="Konvolucio.MCAN120803.MCanAdatpterException->Already Connected.">Thrown when...</exception>
        /// <exception cref="Konvolucio.MCAN120803.MCanAdatpterException->Adapter already in use.">Thrown when...</exception>
        public void ConnectTo(CanAdapterItem adapter)
        {
            if (_isConnected)
                throw new CanAdapterException("Already Connected. Code:-8600."); //Doc.
            bool createdNewMutex = false;
            _mutex = new Mutex(false, adapter.DeviceDescription + " " + adapter.SerialNumber, out createdNewMutex);

            if (!createdNewMutex)
            {
                //ha nem hozott létre új mutexet, akkor már valaki létrehozta és használja
                _mutex.Close();
                _mutex = null;
                throw new CanAdapterException("Adapter already in use. Code:-8601.");//Doc.
            }

            Usb = new USBDevice(adapter.DevicePath);
            FrameCnt = 0;

            _isConnected = false;
            _isOpen = false;
            RxCanMsgAbsTime = 0;

            Usb.ControlPipeTimeout = 1000;

            UsbAttrInEpSzie = Usb.Pipes[USB_ATTR_IN_ADDR].MaximumPacketSize;
            UsbAttrOutEpSize = Usb.Pipes[USB_ATTR_OUT_ADDR].MaximumPacketSize;
            UsbMsgInEpSize = Usb.Pipes[USB_MSG_IN_ADDR].MaximumPacketSize;
            UsbMsgOutEpSize = Usb.Pipes[USB_MSG_OUT_ADDR].MaximumPacketSize;
            
            Usb.Pipes[USB_MSG_OUT_ADDR].Policy.PipeTransferTimeout = 7000;
            Usb.Pipes[USB_ATTR_IN_ADDR].Policy.AutoClearStall = true;
            if (Usb != null)
                _isConnected = true;
        }

        /// <summary>
        /// Connect the adapter.
        /// </summary>
        /// <exception cref="Konvolucio.MCAN120803.MCanAdatpterException->Adapter not found.">Thrown when...</exception>
        public void Connect()
        {
            if (GetAdapters().Count == 0)
            {
                throw new CanAdapterException("Adapter not found. Code:-8602."); //Doc.
            }
            else
            {
                ConnectTo(GetAdapters()[0]);
            }
        }

        /// <summary>
        /// Connect the adapter using the Serial Number.
        /// </summary>
        /// <param name="serialNumber">The adapter is a unique Serial Number.</param>
        /// <exception cref="Konvolucio.MCAN120803.MCanAdatpterException->Adapter not found by Serial Number.">Thrown when...</exception>
        public void ConnectTo(string serialNumber)
        {
            var item = GetAdapters().FirstOrDefault(n => n.SerialNumber.Contains(serialNumber));

            if (item == null)
            {
                var ex = new CanAdapterException("Adapter not found by Serial Number. Code:-8603."); //Doc
                throw ex;
            }
            else
            {
                ConnectTo(item);
            }
        }

        /// <summary>
        /// Disconnect and freeing resources.
        /// </summary>
        public void Disconnect()
        {
            if (_isOpen)
                Close();

            if (_isConnected)
                _isConnected = false;

            if (Usb != null)
                Usb.Dispose();

            if(_mutex != null)
                _mutex.Close();
        }

        /// <summary>
        /// Writes data from a frame buffer to the bus.
        /// </summary>
        /// <param name="frameBuffer">The frame buffer to write data from.</param>
        public void Write(CanMessage[] frameBuffer)
        {
            if (!IsConnected)
                throw new CanAdapterException("Adapter is Disconnected. Code:-8604."); //Doc
            if (!_isOpen)
                throw new CanAdapterException("Bus is Closed. Code:-8605."); //Doc
            if (frameBuffer == null)
                throw new ArgumentNullException("Frame Buffer cannot be null. Code:-8606."); //Doc

            WriteToQueue(frameBuffer, 0, frameBuffer.Length);
        }

        /// <summary>
        /// Writes data from a buffer to the bus.
        /// </summary>
        /// <param name="frameBuffer">The buffer to write data from.</param>
        /// <param name="offset">The frame offset in frame buffer from which to begin writing.</param>
        /// <param name="length">The number of frame to write, starting at offset</param>
        public void Write(CanMessage[] frameBuffer, int offset, int length)
        {
            if (!IsConnected)
                throw new CanAdapterException("Adapter is Disconnected. Code:-8604."); //Doc
            if (!_isOpen)
                throw new CanAdapterException("Bus is Closed. Code:-8605."); //Doc
            if (frameBuffer == null)
                throw new ArgumentNullException("Frame Buffer cannot be null. Code:-8606."); //Doc

            WriteToQueue(frameBuffer, offset, length);
        }

        /// <summary>
        /// Removes the incoming messages.
        /// </summary>
        public void Flush()
        {
            if (!IsConnected)
                throw new CanAdapterException("Adapter is Disconnected. Code:-8604."); //Doc
            if (!_isOpen)
                throw new CanAdapterException("Bus is Closed. Code:-8605."); //Doc
            int i = Attributes.PendingRxMessages;
            IncomingMsgQueue.Clear();
        }

        /// <summary>
        /// Reading a specified number of messages in the frame buffer.
        /// </summary>
        /// <param name="frames">Frame buffer that will receive the data read from the queue.</param>
        /// <param name="offset">Frame offset within the buffer at which to begin writing the data received.</param>
        /// <param name="length">Length of the data to transfer.</param>
        /// <returns>The number of frames read from the pipe.</returns>
        public int Read(CanMessage[] frames, int offset,  int length)
        {
            if (!IsConnected)
                throw new CanAdapterException("Adapter is Disconnected. Code:-8604."); //Doc
            if (!_isOpen)
                throw new CanAdapterException("Bus is Closed. Code:-8605."); //Doc
            if (frames == null)
                throw new ArgumentNullException("Frame Buffer cannot be null. Code:-8606."); //Doc

            for (int i = 0 ; i < length; i++)
                frames[i + offset] = IncomingMsgQueue.Dequeue();
            return length;
        }

        /// <summary>
        /// Initiates an asynchronous read operation on the bus.
        /// </summary>
        /// <param name="frames">Frame buffer that will receive the data read from the queue.</param>
        /// <param name="offset">Frame offset within the buffer at which to begin writing the data received.</param>
        /// <param name="length">Length of the data to transfer.</param>
        /// <param name="userCallback">An optional asynchronous callback, to be called when the operation is complete. Can be null if no callback is required.</param>
        /// <param name="stateObject">A user-provided object that distinguishes this particular asynchronous operation. Can be null if not required.</param>
        /// <returns>object repesenting the asynchronous operation, which could still be pending.</returns>
        [Obsolete]
        public IAsyncResult BeginRead(CanMessage[] frames, int offset, int length, AsyncCallback userCallback, object stateObject)
        {
            if (!IsConnected)
                throw new CanAdapterException("Adapter is Disconnected. Code:-8604."); //Doc
            if (!_isOpen)
                throw new CanAdapterException("Bus is Closed. Code:-8605"); //Doc
            if (frames == null)
                throw new ArgumentNullException("Frame Buffer cannot be null. Code:-8606."); //Doc

            _readBeginFrameArray = frames;
            _readBeginLength = length;
            _readBeginOffset = offset;
       
            _asyncReadBeginResult = new CanAsyncResult(userCallback, stateObject);
            try
            {
                //Van annyi elem, hogy most azonnal vége legyen.
                if (IncomingMsgQueue.Count >= length)
                {
                    Read(_readBeginFrameArray, offset, length);
                    _asyncReadBeginResult.OnCompletion(true, null, length, true);
                }
            }
            catch (Exception)
            {
                _asyncReadBeginResult.Dispose();
                throw;
            }

            return _asyncReadBeginResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itfAR"></param>
        /// <returns></returns>
        [Obsolete]
        public int EndRead(IAsyncResult itfAR)
        {
            if (itfAR == null)
                throw new ArgumentNullException("AsyncResult cannot be null");
            if (!(itfAR is CanAsyncResult))
                throw new ArgumentException("AsyncResult object was not created by calling BeginRead on this class.");

            CanAsyncResult result = (CanAsyncResult)itfAR;
            try
            {
                if (!result.IsCompleted)
                    result.AsyncWaitHandle.WaitOne();

                if (result.Error != null)
                    throw new USBException("Asynchronous read from pipe has failed.", result.Error);

                return result.MessageTransfered;
            }
            finally
            {
                result.Dispose();
            }
        }

        /// <summary>
        /// Opens a new CAN bus connection.
        /// </summary>
        public void Open()
        {
            LasException = null;

            if (!IsConnected)
                throw new CanAdapterException("Adapter is Disconnected. Code:-8604."); //Doc
            if (!_isOpen)
            {
                IncomingMsgQueue = new SafeQueue<CanMessage>();
                OutgoingMsgQueue = new SafeQueue<CanMessage>();

                _isOpen = true;
                isAbort = false;
                _asyncReadBeginResult = null;

                Services.Start();

                if (Attributes.State != CanState.SDEV_START)
                    throw new CanAdapterException("Unable to open the bus. Code:-8607."); //Doc
                Usb.Pipes[USB_MSG_IN_ADDR].Flush();
                
                _asynReadCpltCallback = new AsyncCallback(MsgPipeReadComplate);
                _asyncReadResult = Usb.Pipes[USB_MSG_IN_ADDR].BeginRead(rxMsgPacketBuffer, 0, UsbMsgInEpSize, _asynReadCpltCallback, Usb);
            }
            else
            {
                throw new CanAdapterException("The bus is already opened. Code:-8608."); //Doc
            }           
        }

        /// <summary>
        /// Opens a new CAN bus connection.
        /// </summary>
        /// <param name="baudrate">Baudrate in Baud.</param>
        public void Open(UInt32 baudrate)
        {
            Attributes.Baudrate = baudrate;
            Open();
        }

        /// <summary>
        /// Opens a new CAN bus connection with specified baudrate.
        /// </summary>
        /// <param name="baudrate">Baudrate</param>
        public void Open(CanBaudRateCollection.BaudRateItem baudrate)
        {
            Attributes.Baudrate = baudrate.Value;
            Open();
        }

        /// <summary>
        /// Closes the CAN connection, sets the IsOpen property to false, and disposes of the internal Stream object.
        /// </summary>
        public void Close()
        {
            if (_isOpen)
            {
                try
                {
                    isAbort = true;

                    Usb.Pipes[USB_MSG_IN_ADDR].Abort();
                    IncomingMsgQueue.Clear();
                    OutgoingMsgQueue.Clear();
                    _isOpen = false;
                    Services.Reset();
                    IncomingMsgQueue = null;
                    OutgoingMsgQueue = null;
                }
                catch { };
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
            if (_disposed)
                return;

            if (disposing)
            {
                Disconnect();
            }
            _disposed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frames"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        internal void WriteToQueue(CanMessage[] frames, int offset, int length)
        {
            lock (LockObj)
            {
                for (int i = 0; i < length; i++)
                    OutgoingMsgQueue.Enqueue(frames[i + offset]);
                try
                {
                    int size;
                    byte[] buffer;
                    do
                    {
                        CanMessage.QueueToMsgPacket(OutgoingMsgQueue, MaxCanMsgInPacket, out buffer, out size);
                        if (size != 0)
                            Usb.Pipes[USB_MSG_OUT_ADDR].Write(buffer, 0, size);
                    } while (OutgoingMsgQueue.Count != 0);
                }
                catch (WinUSB.USBException usbex)
                {
                    if (usbex.InnerException.InnerException is System.ComponentModel.Win32Exception)
                    {
                        var i = usbex.InnerException.InnerException as System.ComponentModel.Win32Exception;
                        if (i.NativeErrorCode == 121)
                            throw new CanAdapterException("Write timeout error. Code:-8609."); //Doc
                        else
                            throw new WinUSB.USBException(usbex.Message);
                    }
                    else
                    {
                        throw new WinUSB.USBException(usbex.Message);
                    }
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="length"></param>
        internal void WriteMsgEndpoint(byte[] buffer, int length)
        {
            Usb.Pipes[USB_MSG_OUT_ADDR].Write(buffer, 0, length);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iftAr"></param>
        void MsgPipeReadComplate(IAsyncResult iftAr)
        {
            int bytes = 0;
            CanMessage[] frames;
            //lock (LockObj) Ezt nem lehet lock-ni, mivel adatvesztés következik be.!
            {
                if (!this.isAbort)
                {
                    try
                    {
                        bytes = Usb.Pipes[USB_MSG_IN_ADDR].EndRead(iftAr);

                    }
                    catch(Exception ex)
                    {
                        /*Itt valahogy jelezni kell, hogy ha hibábval ért véget a pipe olvasása*/
                        /*Ha itt hiba történt akkor vége.*/
                        LasException = ex;
                        this.isAbort = true;
                    }
                }

                if (bytes != 0)
                {
                    CanMessage.MsgPacketToCanFrames(rxMsgPacketBuffer, bytes, out frames);

                    for (int i = 0; i < frames.Length; i++)
                    {

                        RxCanMsgAbsTime = (frames[i].TimestampTick * 10000) + RxCanMsgAbsTime;
                        frames[i].TimestampTick = RxCanMsgAbsTime;
                        if (IncomingMsgQueue != null)
                            IncomingMsgQueue.Enqueue(frames[i]);
                    }

                    if (_asyncReadBeginResult != null && !_asyncReadBeginResult.IsCompleted)
                    {
                        if (IncomingMsgQueue.Count >= _readBeginLength)
                        {
                            Read(_readBeginFrameArray, _readBeginOffset, _readBeginLength);
                            _asyncReadBeginResult.OnCompletion(false, null, _readBeginLength, true);
                        }
                    }
                }

                if (!this.isAbort)
                {
                    Usb.Pipes[USB_MSG_IN_ADDR].BeginRead(rxMsgPacketBuffer, 0, UsbMsgInEpSize, _asynReadCpltCallback, null);
                }
            }
        }
        /// <summary>
        /// WriteUsbAttributePipe
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="length"></param>
        void WriteUsbAttributePipe(byte[] buffer, Int32 length)
        {
            Usb.Pipes[USB_ATTR_OUT_ADDR].Write(buffer, 0, length);
        }

        /// <summary>
        /// ReadUsbAttributePipe
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="length"></param>
        void ReadUsbAttributePipe(out byte[] buffer, out Int32 length)
        {
            byte[] temp = new byte[UsbAttrInEpSzie];
            int bytes = Usb.Pipes[USB_ATTR_IN_ADDR].Read(temp);
            if (UsbPacketTool.IsFrameEnd(temp, bytes))
            {
                buffer = temp;
                length = bytes;
            }
            else
            {
                buffer = new byte[0];
                length = 0;
            }
        }
    }
}

