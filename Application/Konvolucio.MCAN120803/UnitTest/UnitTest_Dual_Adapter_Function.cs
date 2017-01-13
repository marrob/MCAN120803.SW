
namespace Konvolucio.MCAN120803.API.UnitTest
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using System.Threading;
    using Konvolucio.MCAN120803.API;

    [TestFixture]
    class UnitTest_Dual_Adapters_Function
    {
        CanAdapterDevice _adapterWriter = new CanAdapterDevice();
        CanAdapterDevice _adapterReader = new CanAdapterDevice();
        uint _targetBaudrate = 0;


        [TestFixtureSetUp]
        public void Setup()
        {
            _adapterWriter = new CanAdapterDevice();
            _adapterWriter.ConnectTo(CanAdapterDevice.GetAdapters()[0]);
            _adapterReader = new CanAdapterDevice();
            _adapterReader.ConnectTo(CanAdapterDevice.GetAdapters()[1]);

            _targetBaudrate = 125000;

        }

        [TestFixtureTearDown]
        public void Clean()
        {
            _adapterWriter.Close();
            _adapterWriter.Disconnect();

            _adapterReader.Close();
            _adapterReader.Disconnect();
        }

        [Test]
        public void _0001_WriteRead_SingleMessage()
        {
            if (_adapterWriter.IsOpen)
                _adapterWriter.Close();

            if (_adapterReader.IsOpen)
                _adapterReader.Close();

            if (_adapterWriter.Attributes.State != CanState.SDEV_IDLE)
                _adapterWriter.Services.Reset();

            if (_adapterReader.Attributes.State != CanState.SDEV_IDLE)
                _adapterReader.Services.Reset();

            _adapterWriter.Attributes.Termination = true;
            _adapterReader.Attributes.Termination = true;

            _adapterWriter.Open(_targetBaudrate);
            Thread.Sleep(50);
            _adapterReader.Open(_targetBaudrate);

            _adapterWriter.Write(new CanMessage[] { CanMessage.MessageStdC8 });

            CanMessage[] rxFrames = new CanMessage[128];
            int offset = 0, length = 0;
            long startTick = DateTime.Now.Ticks;
            do
            {


                length = _adapterReader.Attributes.PendingRxMessages;
                _adapterReader.Read(rxFrames, offset, length);
                offset += length;

                if (DateTime.Now.Ticks - startTick > 1000 * 10000)
                {
                    throw new Exception("Timeout");
                }

            } while (offset == 0);

            Assert.AreEqual(0, _adapterReader.Attributes.RxDrop, "Reader RxDrop nem nulla: " + _adapterReader.Attributes.RxDrop.ToString());
            Assert.AreEqual(1, _adapterReader.Attributes.RxTotal, "");

            Assert.AreEqual(0, _adapterWriter.Attributes.TxDrop, "Writer TxDrop nem nulla: " + _adapterWriter.Attributes.TxDrop.ToString());
            Assert.AreEqual(1, _adapterWriter.Attributes.TxTotal, "Writer TxDrop nem nulla: " + _adapterWriter.Attributes.TxTotal.ToString());

            Assert.AreEqual(0, _adapterWriter.Attributes.TxDrop, "Writer TxDrop nem nulla: " + _adapterWriter.Attributes.TxDrop.ToString());
            Assert.AreEqual(1, _adapterReader.Attributes.RxTotal, "Az adapter FIFO-jába nem jött meg minden:" + _adapterReader.Attributes.RxTotal.ToString());
            Assert.AreEqual(1, offset, "A Host FIFO-jaba nem jött meg minden.");
        }

        [Test]
        public void _0002_WirteRead_MessegWithOffset()
        {
            if (_adapterWriter.IsOpen)
                _adapterWriter.Close();

            if (_adapterReader.IsOpen)
                _adapterReader.Close();

            if (_adapterWriter.Attributes.State != CanState.SDEV_IDLE)
                _adapterWriter.Services.Reset();

            if (_adapterReader.Attributes.State != CanState.SDEV_IDLE)
                _adapterReader.Services.Reset();

            _adapterWriter.Attributes.Termination = true;
            _adapterReader.Attributes.Termination = true;

            _adapterWriter.Open(_targetBaudrate);
            Thread.Sleep(50);
            _adapterReader.Open(_targetBaudrate);

            CanMessage[] txFrames = new CanMessage[] 
            {
                CanMessage.MessageStdA4,
                CanMessage.MessageStdA8,
            };

            _adapterWriter.Write(txFrames, 1, 1);

            CanMessage[] rxFrames = new CanMessage[128];
            int offset = 0, length = 0;
            long startTick = DateTime.Now.Ticks;
            do
            {
                if (DateTime.Now.Ticks - startTick > 1000 * 10000)
                {
                    throw new Exception("Timeout");
                }

                length = _adapterReader.Attributes.PendingRxMessages;
                _adapterReader.Read(rxFrames, offset, length);
                offset += length;
            } while (offset != 1);

            Assert.AreEqual(0, _adapterReader.Attributes.RxDrop, "Reader RxDrop nem nulla. ");
            Assert.AreEqual(1, _adapterReader.Attributes.RxTotal, "");

            Assert.AreEqual(0, _adapterWriter.Attributes.TxDrop, "Writer TxDrop nem nulla. ");
            Assert.AreEqual(1, _adapterWriter.Attributes.TxTotal, "");

            Assert.AreEqual(0, _adapterWriter.Attributes.TxDrop, "Writer TxDrop nem nulla. ");
            Assert.AreEqual(1, _adapterReader.Attributes.RxTotal, "Az adapter FIFO-jába nem jött meg minden.");
            Assert.AreEqual(1, offset, "A Host FIFO-jaba nem jött meg minden.");
        }

        [Test]
        public void _0003_WirteRead_StdId_0x7FF()
        {
            CanMessage rxFrame;
            WirteReadSingleMessage(_targetBaudrate, CanMessage.MessageMaxStdId, out rxFrame);
            Assert.AreEqual(CanMessage.MessageMaxStdId, rxFrame);
        }

        [Test]
        public void _0004_WirteRead_ExtId_0x7FF()
        {
            CanMessage rxFrame;
            CanMessage outgoing = CanMessage.MessageMaxStdId;
            outgoing.ArbitrationId |= 0x20000000;
            WirteReadSingleMessage(_targetBaudrate, outgoing, out rxFrame);
            Assert.AreEqual(outgoing, rxFrame);
        }

        [Test]
        public void _0005_WirteRead_ExtId_0x1FFFFFFF()
        {
            CanMessage rxFrame;
            WirteReadSingleMessage(_targetBaudrate, CanMessage.MessageMaxExtId, out rxFrame);
            Assert.AreEqual(CanMessage.MessageMaxExtId, rxFrame);
        }

        [Test]
        public void _0006_WirteRead_ExtId_0x00000000()
        {
            CanMessage rxFrame;
            WirteReadSingleMessage(_targetBaudrate, CanMessage.EmptyMessageExtE0, out rxFrame);
            Assert.AreEqual(CanMessage.EmptyMessageExtE0, rxFrame);
        }

        void WirteReadSingleMessage(uint baudrate, CanMessage outgoing, out CanMessage incoming)
        {
            if (_adapterWriter.IsOpen)
                _adapterWriter.Close();

            if (_adapterReader.IsOpen)
                _adapterReader.Close();

            if (_adapterWriter.Attributes.State != CanState.SDEV_IDLE)
                _adapterWriter.Services.Reset();

            if (_adapterReader.Attributes.State != CanState.SDEV_IDLE)
                _adapterReader.Services.Reset();

            _adapterWriter.Attributes.Termination = true;
            _adapterReader.Attributes.Termination = true;

            _adapterWriter.Open(_targetBaudrate);
            Thread.Sleep(50);
            _adapterReader.Open(_targetBaudrate);

            _adapterWriter.Write(new CanMessage[] { outgoing }, 0, 1);

            CanMessage[] rxFrames = new CanMessage[128];
            int offset = 0, length = 0;
            long startTick = DateTime.Now.Ticks;
            do
            {
                if (DateTime.Now.Ticks - startTick > 1000 * 10000)
                    throw new Exception("Timeout");

                length = _adapterReader.Attributes.PendingRxMessages;
                _adapterReader.Read(rxFrames, offset, length);
                offset += length;
            } while (offset == 0);

            if (length >= 1)
                incoming = rxFrames[0];
            else
                incoming = new CanMessage();

            Assert.AreEqual(0, _adapterReader.Attributes.RxDrop, "Reader RxDrop nem nulla. ");
            Assert.AreEqual(1, _adapterReader.Attributes.RxTotal, "");

            Assert.AreEqual(0, _adapterWriter.Attributes.TxDrop, "Writer TxDrop nem nulla. ");
            Assert.AreEqual(1, _adapterWriter.Attributes.TxTotal, "");

            Assert.AreEqual(0, _adapterWriter.Attributes.TxDrop, "Writer TxDrop nem nulla. ");
            Assert.AreEqual(1, _adapterReader.Attributes.RxTotal, "Az adapter FIFO-jába nem jött meg minden.");
            Assert.AreEqual(1, offset, "A Host FIFO-jaba nem jött meg minden.");
        }

        [Test]
        public void _0007_MsgGeneratorToBus()
        {
            if (_adapterWriter.IsOpen)
                _adapterWriter.Close();

            if (_adapterReader.IsOpen)
                _adapterReader.Close();

            if (_adapterWriter.Attributes.State != CanState.SDEV_IDLE)
                _adapterWriter.Services.Reset();

            if (_adapterReader.Attributes.State != CanState.SDEV_IDLE)
                _adapterReader.Services.Reset();

            _adapterWriter.Attributes.Termination = true;
            _adapterReader.Attributes.Termination = true;

            _adapterWriter.Attributes.MsgGeneratorToBus = true;

            Thread.Sleep(50);
            _adapterReader.Open(_targetBaudrate);
            _adapterWriter.Open(_targetBaudrate);

            const int MAX_MSG_COUNT = 128;
            CanMessage[] rxFrames = new CanMessage[MAX_MSG_COUNT];
            int offset = 0, length = 0;

            long startTick = DateTime.Now.Ticks;
            do
            {
                length = _adapterReader.Attributes.PendingRxMessages;

                if (length > MAX_MSG_COUNT - 1)
                    _adapterReader.Read(rxFrames, offset, MAX_MSG_COUNT-1);
                else
                    _adapterReader.Read(rxFrames, offset, length);

                offset += length;

                if (offset > MAX_MSG_COUNT)
                    break;

                if (DateTime.Now.Ticks - startTick > 1000 * 1000)
                    throw new Exception("Timeout");
            
            } while (offset == 0);


            Assert.AreEqual(0, _adapterReader.Attributes.RxDrop, "Reader RxDrop nem nulla:" + _adapterReader.Attributes.RxDrop.ToString());
            Assert.AreEqual(0, _adapterWriter.Attributes.TxDrop, "Writer TxDrop nem nulla:" + _adapterWriter.Attributes.TxDrop.ToString());
            Assert.IsTrue(offset != 0, "A küldő vagy a fogadó adapter nem küldött vagy fogadott..");
        }

        [Test]
        public void _0008_MsgGeneratorToHost()
        {
            
            if (_adapterReader.IsOpen)
                _adapterReader.Close();

            Thread.Sleep(1000);
            _adapterReader.Services.Reset();
            _adapterReader.Attributes.MsgGeneratorToHost = true;
            _adapterReader.Open(_targetBaudrate);
            const int MAX_MSG_COUNT = 128;
            CanMessage[] rxFrames = new CanMessage[MAX_MSG_COUNT];
            int offset = 0, length = 0;
            long startTick = DateTime.Now.Ticks;
            do
            {
                length = _adapterReader.Attributes.PendingRxMessages;

                if (length > MAX_MSG_COUNT - 1)
                    _adapterReader.Read(rxFrames, offset, MAX_MSG_COUNT - 1);
                else
                    _adapterReader.Read(rxFrames, offset, length);

                offset += length;

                if (DateTime.Now.Ticks - startTick > 1000 * 1000)
                    throw new Exception("Timeout");

            } while (offset + length < MAX_MSG_COUNT);

            Assert.AreEqual(0, _adapterReader.Attributes.RxDrop, "Reader RxDrop nem nulla. ");
            Assert.IsTrue(_adapterReader.Attributes.RxTotal != 0, "");

            Assert.AreEqual(0, _adapterReader.Attributes.TxDrop, "Writer TxDrop nem nulla. ");
            Assert.AreEqual(0, _adapterReader.Attributes.TxTotal, "");

            Assert.AreEqual(0, _adapterReader.Attributes.TxDrop, "Writer TxDrop nem nulla. ");
            Assert.IsTrue(_adapterReader.Attributes.RxTotal != 0, "Az adapter FIFO-jába nem jött meg minden.");
        }

        [Test]
        public void _0009_WirteRead_ZerroLengthArray()
        {
            if (_adapterWriter.IsOpen)
                _adapterWriter.Close();

            if (_adapterReader.IsOpen)
                _adapterReader.Close();

            if (_adapterWriter.Attributes.State != CanState.SDEV_IDLE)
                _adapterWriter.Services.Reset();

            if (_adapterReader.Attributes.State != CanState.SDEV_IDLE)
                _adapterReader.Services.Reset();

            _adapterWriter.Attributes.Termination = true;
            _adapterReader.Attributes.Termination = true;

            _adapterWriter.Open(_targetBaudrate);
            Thread.Sleep(50);
            _adapterReader.Open(_targetBaudrate);
            
            _adapterWriter.Write(new CanMessage[] { CanMessage.MessageStdA8 }, 0, 0);

            CanMessage[] rxFrames = new CanMessage[128];
            int offset = 0, length = 0;
            bool isTimeout = false;

            long startTick = DateTime.Now.Ticks;
            do
            {
                if (DateTime.Now.Ticks - startTick > 1000 * 10000)
                    isTimeout = true;

                length = _adapterReader.Attributes.PendingRxMessages;
                _adapterReader.Read(rxFrames, offset, length);
                offset += length;
            } while (!isTimeout);

            Assert.AreEqual(0, _adapterReader.Attributes.RxDrop, "Reader RxDrop nem nulla. ");
            Assert.AreEqual(0, _adapterReader.Attributes.RxTotal, "");

            Assert.AreEqual(0, _adapterWriter.Attributes.TxDrop, "Writer TxDrop nem nulla. ");
            Assert.AreEqual(0, _adapterWriter.Attributes.TxTotal, "");

            Assert.AreEqual(0, _adapterWriter.Attributes.TxDrop, "Writer TxDrop nem nulla. ");
            Assert.AreEqual(0, _adapterReader.Attributes.RxTotal, "Az adapter FIFO-jába nem jött meg minden.");
            Assert.AreEqual(0, offset, "A Host FIFO-jaba nem jött meg minden.");
        }

        [Test]
        public void _0010_RunningStopSetFilterRun()
        {
            if (_adapterWriter.IsOpen)
                _adapterWriter.Close();

            if (_adapterReader.IsOpen)
                _adapterReader.Close();

            if (_adapterWriter.Attributes.State != CanState.SDEV_IDLE)
                _adapterWriter.Services.Reset();

            if (_adapterReader.Attributes.State != CanState.SDEV_IDLE)
                _adapterReader.Services.Reset();

            _adapterWriter.Attributes.Termination = true;
            _adapterReader.Attributes.Termination = true;

            _adapterWriter.Open(_targetBaudrate);
            Thread.Sleep(50);
            _adapterReader.Open(_targetBaudrate);

            UInt32 maskAndFilter = 0x000;
            CanMessage[] rxFrames = new CanMessage[128];
            int offset = 0; 
            int length = 0;
            long startTick = 0;
            bool isTimeout = false;

            for (int i = 0; i < 2; i++){
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x295, new byte[] { 0x01, 0x02, 0x03 }) });
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x296, new byte[] { 0x04, 0x05, 0x06 }) });
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x4D9, new byte[] { 0x07, 0x08, 0x09 }) });
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x503, new byte[] { 0x10, 0x11, 0x12 }) });
            }
            startTick = DateTime.Now.Ticks;
            do{
                if (DateTime.Now.Ticks - startTick > 1000 * 10000)
                    isTimeout = true;

                length = _adapterReader.Attributes.PendingRxMessages;
                _adapterReader.Read(rxFrames, offset, length);
                offset += length;
            } while (!isTimeout);

            Assert.AreEqual(2, rxFrames.Count(n => n.ArbitrationId == 0x295), "0x295/200ms");
            Assert.AreEqual(2, rxFrames.Count(n => n.ArbitrationId == 0x295), "0x296/200ms");
            Assert.AreEqual(2, rxFrames.Count(n => n.ArbitrationId == 0x4D9), "0x4D9/1000ms");
            Assert.AreEqual(2, rxFrames.Count(n => n.ArbitrationId == 0x503), "0x503/1000ms");

            _adapterReader.Services.Stop();
            _adapterReader.Flush();
            offset = 0;
            Array.Clear(rxFrames, 0, rxFrames.Length);
            maskAndFilter = 0x295;
            _adapterReader.Attributes.Mask = (0x000007FF & maskAndFilter) << 21;
            _adapterReader.Attributes.Filter = (0x000007FF & maskAndFilter) << 21;
            _adapterReader.Services.Start();
            
            for (int i = 0; i < 2; i++){
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x295, new byte[] { 0x01, 0x02, 0x03 }) });
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x296, new byte[] { 0x04, 0x05, 0x06 }) });
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x4D9, new byte[] { 0x07, 0x08, 0x09 }) });
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x503, new byte[] { 0x10, 0x11, 0x12 }) });
            }
            offset = 0;
            isTimeout = false;
            startTick = DateTime.Now.Ticks;
            do{
                if (DateTime.Now.Ticks - startTick > 1000 * 10000)
                    isTimeout = true;
                length = _adapterReader.Attributes.PendingRxMessages;
                _adapterReader.Read(rxFrames, offset, length);
                offset += length;
            } while (!isTimeout);

            Assert.AreEqual(2, rxFrames.Count(n => n.ArbitrationId == 0x295), "0x295/200ms");
            Assert.AreEqual(0, rxFrames.Count(n => n.ArbitrationId == 0x296), "0x296/200ms");
            Assert.AreEqual(0, rxFrames.Count(n => n.ArbitrationId == 0x4D9), "0x4D9/1000ms");
            Assert.AreEqual(0, rxFrames.Count(n => n.ArbitrationId == 0x503), "0x503/1000ms");


            _adapterReader.Services.Stop();
            _adapterReader.Flush();
            offset = 0;
            Array.Clear(rxFrames, 0, rxFrames.Length);
            maskAndFilter = 0x00;
            _adapterReader.Attributes.Mask = (0x000007FF & maskAndFilter) << 21;
            _adapterReader.Attributes.Filter = (0x000007FF & maskAndFilter) << 21;
            _adapterReader.Services.Start();

            for (int i = 0; i < 2; i++)
            {
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x295, new byte[] { 0x01, 0x02, 0x03 }) });
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x296, new byte[] { 0x04, 0x05, 0x06 }) });
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x4D9, new byte[] { 0x07, 0x08, 0x09 }) });
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x503, new byte[] { 0x10, 0x11, 0x12 }) });
            }
            offset = 0;
            isTimeout = false;
            startTick = DateTime.Now.Ticks;
            do
            {
                if (DateTime.Now.Ticks - startTick > 1000 * 10000)
                    isTimeout = true;
                length = _adapterReader.Attributes.PendingRxMessages;
                _adapterReader.Read(rxFrames, offset, length);
                offset += length;
            } while (!isTimeout);

            Assert.AreEqual(2, rxFrames.Count(n => n.ArbitrationId == 0x295), "0x295/200ms");
            Assert.AreEqual(2, rxFrames.Count(n => n.ArbitrationId == 0x296), "0x296/200ms");
            Assert.AreEqual(2, rxFrames.Count(n => n.ArbitrationId == 0x4D9), "0x4D9/1000ms");
            Assert.AreEqual(2, rxFrames.Count(n => n.ArbitrationId == 0x503), "0x503/1000ms");
        }

        [Test]
        public void _0011_Filtering()
        {
            if (_adapterWriter.IsOpen)
                _adapterWriter.Close();

            if (_adapterReader.IsOpen)
                _adapterReader.Close();

            if (_adapterWriter.Attributes.State != CanState.SDEV_IDLE)
                _adapterWriter.Services.Reset();

            if (_adapterReader.Attributes.State != CanState.SDEV_IDLE)
                _adapterReader.Services.Reset();

            _adapterWriter.Attributes.Termination = true;
            _adapterReader.Attributes.Termination = true;

            Assert.AreEqual(0, _adapterReader.Attributes.RxTotal, "RxTotal");
            Assert.AreEqual(0, _adapterReader.Attributes.RxDrop,  "RxDrop");
            Assert.AreEqual(0, _adapterWriter.Attributes.TxTotal, "TxTotal");
            Assert.AreEqual(0, _adapterWriter.Attributes.TxDrop,  "TxDrop");

            UInt32 maskAndFilter = 0x503;
            _adapterReader.Attributes.Mask = (0x000007FF & maskAndFilter) << 21;
            _adapterReader.Attributes.Filter = (0x000007FF & maskAndFilter) << 21;

            _adapterWriter.Open(_targetBaudrate);
            Thread.Sleep(50);
            _adapterReader.Open(_targetBaudrate);

            CanMessage[] rxFrames = new CanMessage[128];
            int offset = 0;
            int length = 0;
            long startTick = 0;
            bool isTimeout = false;

            for (int i = 0; i < 2; i++)
            {
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x295, new byte[] { 0x01, 0x02, 0x03 }) });
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x296, new byte[] { 0x04, 0x05, 0x06 }) });
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x4D9, new byte[] { 0x07, 0x08, 0x09 }) });
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x503, new byte[] { 0x10, 0x11, 0x12 }) });
            }
            startTick = DateTime.Now.Ticks;
            do
            {
                if (DateTime.Now.Ticks - startTick > 1000 * 10000)
                    isTimeout = true;

                length = _adapterReader.Attributes.PendingRxMessages;
                _adapterReader.Read(rxFrames, offset, length);
                offset += length;
            } while (!isTimeout);

            Assert.AreEqual(0, rxFrames.Count(n => n.ArbitrationId == 0x295), "0x295/200ms");
            Assert.AreEqual(0, rxFrames.Count(n => n.ArbitrationId == 0x296), "0x296/200ms");
            Assert.AreEqual(0, rxFrames.Count(n => n.ArbitrationId == 0x4D9), "0x4D9/1000ms");
            Assert.AreEqual(2, rxFrames.Count(n => n.ArbitrationId == 0x503), "0x503/1000ms");

            Assert.AreEqual(2, _adapterReader.Attributes.RxTotal, "RxTotal");
            Assert.AreEqual(0, _adapterReader.Attributes.RxDrop, "RxDrop");
            Assert.AreEqual(8, _adapterWriter.Attributes.TxTotal, "TxTotal");
            Assert.AreEqual(0, _adapterWriter.Attributes.TxDrop, "TxDrop");
        }

        [Test]
        public void _0012_WriteBeginReadCallback()
        {
            if (_adapterWriter.IsOpen)
                _adapterWriter.Close();

            if (_adapterReader.IsOpen)
                _adapterReader.Close();

            if (_adapterWriter.Attributes.State != CanState.SDEV_IDLE)
                _adapterWriter.Services.Reset();

            if (_adapterReader.Attributes.State != CanState.SDEV_IDLE)
                _adapterReader.Services.Reset();
            
            _adapterWriter.Attributes.Termination = true;
            _adapterReader.Attributes.Termination = true;

            _adapterWriter.Open(_targetBaudrate);
            Thread.Sleep(50);
            _adapterReader.Open(_targetBaudrate);

            for (int i = 0; i < 12; i++)
            {
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x295, new byte[] { 0x01, 0x02, 0x03 }) });
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x296, new byte[] { 0x04, 0x05, 0x06 }) });
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x4D9, new byte[] { 0x07, 0x08, 0x09 }) });
            }
            CanMessage[] rxFrames = new CanMessage[128];

            var state = new MessageReaderAsynState(rxFrames, _adapterReader, 0);
            _adapterReader.BeginRead(rxFrames, state.Offset, 3, ReadComplete, state);

            Thread.Sleep(1000);
            Assert.AreEqual(12, rxFrames.Count(n => n.ArbitrationId == 0x295), "0x295/200ms");
            Assert.AreEqual(12, rxFrames.Count(n => n.ArbitrationId == 0x296), "0x296/200ms");
            Assert.AreEqual(12, rxFrames.Count(n => n.ArbitrationId == 0x4D9), "0x4D9/1000ms");
        }
        void ReadComplete(IAsyncResult itfAR)
        {
            var state = itfAR.AsyncState as MessageReaderAsynState;
            int readMsg = state.Adapter.EndRead(itfAR);
            state.Offset += 3;
            state.Adapter.BeginRead(state.Frames, state.Offset, 3, ReadComplete, state);
            Console.WriteLine("Message packet read complate.");
        }
        class MessageReaderAsynState
        {
            public CanMessage[] Frames;
            public CanAdapterDevice Adapter;
            public int Offset;
            public MessageReaderAsynState(CanMessage[] frames, CanAdapterDevice adapter, int offset)
            {
                Frames = frames;
                Adapter = adapter;
                Offset = offset;
            }
        }

        [Test]
        public void _0013_WriteBeginReadEndRead()
        {
            if (_adapterWriter.IsOpen)
                _adapterWriter.Close();

            if (_adapterReader.IsOpen)
                _adapterReader.Close();

            if (_adapterWriter.Attributes.State != CanState.SDEV_IDLE)
                _adapterWriter.Services.Reset();

            if (_adapterReader.Attributes.State != CanState.SDEV_IDLE)
                _adapterWriter.Services.Reset();

            _adapterWriter.Attributes.Termination = true;
            _adapterReader.Attributes.Termination = true;

            

            _adapterWriter.Open(_targetBaudrate);
            Thread.Sleep(50);
            _adapterReader.Open(_targetBaudrate);
            _adapterReader.Flush();


            for (int i = 0; i < 2; i++)
            {
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x295, new byte[] { 0x01, 0x02, 0x03 }) });
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x296, new byte[] { 0x04, 0x05, 0x06 }) });
                _adapterWriter.Write(new CanMessage[] { new CanMessage(0x4D9, new byte[] { 0x07, 0x08, 0x09 }) });
            }

            CanMessage[] rxFrames = new CanMessage[128];
            IAsyncResult iftAR = _adapterReader.BeginRead(rxFrames, 0, 6, null, null);
            Thread.Sleep(1000);
            int msgCount = _adapterReader.EndRead(iftAR);
            Assert.AreEqual( 6, msgCount, "Nem érkezett be a várt számú üzenet");
        }
    }
}
