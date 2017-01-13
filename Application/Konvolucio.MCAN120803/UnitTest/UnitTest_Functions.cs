

namespace Konvolucio.MCAN120803.API.UnitTest
{
    using System;
    using System.Linq;
    using System.Threading;
    using NUnit.Framework;

    [TestFixture]
    class UnitTestFunctions
    {
        CanAdapterDevice _adapter;
        CanMessage[] _msgBuffer;
        uint _targetBaudrate;

        [TestFixtureSetUp]
        public void Setup()
        {
            _adapter = new CanAdapterDevice();
            _adapter.Connect();
            _targetBaudrate = 125000;
            _msgBuffer = new CanMessage[UInt16.MaxValue];
        }
    
        [TestFixtureTearDown]
        public void Clean()
        {
            _adapter.Close();
            _adapter.Disconnect();
        }

        [Test]
        /*ExpectedMessage = "Error.Attribute value is invalid. Code:-8613."*/
        public void _0001_SetInvalidBaudrate_Exception()
        {
            Assert.Catch<CanAdapterException>(() =>
            {
                if (_adapter.IsOpen)
                    _adapter.Close();

                if (_adapter.Attributes.State != CanState.SDEV_IDLE)
                    _adapter.Services.Reset();

                _adapter.Attributes.Baudrate = 0x12345678;
                UInt32 baudrate = _adapter.Attributes.Baudrate;
                Assert.AreEqual(0x12345678, baudrate);
            });
        }

        [Test]
        public void _0002_SetValidBaudrate()
        {
            if (_adapter.IsOpen)
                _adapter.Close();

            if (_adapter.Attributes.State != CanState.SDEV_IDLE)
                _adapter.Services.Reset();

            _adapter.Attributes.Baudrate = _targetBaudrate;
            UInt32 baudrate = _adapter.Attributes.Baudrate;
            Assert.AreEqual(_targetBaudrate, baudrate, "Beálított és a lekérdezett Baudrate nem egyezik.");
        }

        [Test]
        /*ExpectedMessage = "The bus is already opened. Code:-8608."*/
        public void _0003_ReOpen_Exception()
        {
            Assert.Catch<CanAdapterException>(() =>
            {
                if (_adapter.IsOpen)
                    _adapter.Close();

                if (_adapter.Attributes.State != CanState.SDEV_IDLE)
                    _adapter.Services.Reset();

                _adapter.Attributes.Baudrate = _targetBaudrate;
                _adapter.Open();
                _adapter.Open();
            });
        }

        [Test]
        public void _0004_ReClose()
        {
            if (_adapter.IsOpen)
                _adapter.Close();

            if (_adapter.Attributes.State != CanState.SDEV_IDLE)
                _adapter.Services.Reset();

            _adapter.Close();
            _adapter.Close();
        }

        [Test]
        /*ExpectedMessage = "Warning.This function not supported in this mode. Code:-8617."*/
        public void _0005_SetWhenRunning_Exception()
        {
            Assert.Catch<CanAdapterException>(() =>
            {
                if (_adapter.IsOpen)
                    _adapter.Close();

                if (_adapter.Attributes.State != CanState.SDEV_IDLE)
                    _adapter.Services.Reset();

                _adapter.Open(_targetBaudrate);
                Thread.Sleep(10);
                _adapter.Attributes.Baudrate = _targetBaudrate;

            });
        }

        [Test]
        public void _0006_StartStopReset()
        {
            if (_adapter.IsOpen)
                _adapter.Close();

            if (_adapter.Attributes.State != CanState.SDEV_IDLE)
                _adapter.Services.Reset();

            _adapter.Attributes.Baudrate = _targetBaudrate;

            Assert.AreEqual(CanState.SDEV_IDLE, _adapter.Attributes.State, "Az állapota nem alaphelyzet.");

            _adapter.Services.Start();
            Assert.AreEqual(CanState.SDEV_START, _adapter.Attributes.State, "Start után nem állt be a START állapot.");

            _adapter.Services.Stop();
            Assert.AreEqual(CanState.SDEV_STOP, _adapter.Attributes.State, "Stop után nem állt be a STOP állapot");

            _adapter.Services.Reset();
            Assert.AreEqual(CanState.SDEV_IDLE, _adapter.Attributes.State, "Reset után nem állt be az IDLE állapot");

        }
    
        [Test]
        public void _0007_OpenClose()
        {
            if (_adapter.IsOpen)
                _adapter.Close();

            if (_adapter.Attributes.State != CanState.SDEV_IDLE)
                _adapter.Services.Reset();
            _adapter.Open(_targetBaudrate);
            _adapter.Close();
            _adapter.Open(_targetBaudrate);
            _adapter.Close();
            _adapter.Open(_targetBaudrate);
            _adapter.Close();
            _adapter.Open(_targetBaudrate);
            _adapter.Close();
            _adapter.Open(_targetBaudrate);
            _adapter.Close();
        }

        [Test]
        public void _0009_Loopback_MessageStdA4()
        {
            if (_adapter.IsOpen)
                _adapter.Close();

            if (_adapter.Attributes.State != CanState.SDEV_IDLE)
                _adapter.Services.Reset();

            CanMessage[] buffer = new CanMessage[1];
            _adapter.Attributes.Loopback = true;
            _adapter.Open(_targetBaudrate);
            _adapter.Write(new CanMessage[] { CanMessage.MessageStdA4 });
            do { } while (_adapter.Attributes.PendingRxMessages != 1);
            _adapter.Read(buffer, 0, 1);
            Assert.IsTrue(buffer.Contains(CanMessage.MessageStdA4));
        }

        [Test]
        public void _0010_Loopback_MessageMaxExtId()
        {
            if (_adapter.IsOpen)
                _adapter.Close();

            if (_adapter.Attributes.State != CanState.SDEV_IDLE)
                _adapter.Services.Reset();

            CanMessage[] buffer = new CanMessage[1];
            _adapter.Attributes.Loopback = true;
            _adapter.Open(_targetBaudrate);
            _adapter.Write(new CanMessage[] { CanMessage.MessageMaxExtId });
            do { } while (_adapter.Attributes.PendingRxMessages != 1);
            _adapter.Read(buffer, 0, 1);
            Assert.IsTrue(buffer.Contains(CanMessage.MessageMaxExtId));
        }

        [Test]
        public void _0011_SilentLoopback()
        {
            if (_adapter.IsOpen)
                _adapter.Close();

            if (_adapter.Attributes.State != CanState.SDEV_IDLE)
                _adapter.Services.Reset();

            Array.Clear(_msgBuffer, 0, _msgBuffer.Length);
            _adapter.Attributes.Loopback = true;
            _adapter.Attributes.ListenOnly = true;
            _adapter.Open(_targetBaudrate);
            _adapter.Write(new CanMessage[] { CanMessage.MessageStdA4 });
            do { } while (_adapter.Attributes.PendingRxMessages != 1);
            _adapter.Read(_msgBuffer, 0, 1);
            Assert.IsTrue(_msgBuffer.Contains(CanMessage.MessageStdA4));
        }

        [Test]
        public void _0012_Termination()
        {
            if (_adapter.IsOpen)
                _adapter.Close();

            if (_adapter.Attributes.State != CanState.SDEV_IDLE)
                _adapter.Services.Reset();

            _adapter.Attributes.Termination = true;
            Assert.IsTrue(_adapter.Attributes.Termination);
            _adapter.Open(_targetBaudrate);
            Thread.Sleep(1000);
            _adapter.Close();
            Thread.Sleep(1000);
            _adapter.Open(_targetBaudrate);
        }

        [Test]
        public void _0013_DeviceInfo()
        {
            if (_adapter.IsOpen)
                _adapter.Close();

            if (_adapter.Attributes.State != CanState.SDEV_IDLE)
                _adapter.Services.Reset();

            Assert.AreEqual("1.0.0.24", _adapter.Attributes.FirmwareRev,"Firmware verzió nem jó.");
            Assert.AreEqual("V00", _adapter.Attributes.PcbRev);
            Assert.AreEqual("MCAN120803", _adapter.Attributes.DeviceName);
            Assert.AreEqual("1.0.0.26", _adapter.Attributes.AssemblyVersion,"Assembly verzió nem jó.");
            Assert.NotNull(_adapter.Attributes.SerialNumber);

            _adapter.Open(_targetBaudrate);
        }
 
        [Test]
        public void _0014_Write_Exception()
        {
            if (_adapter.IsOpen)
                _adapter.Close();

            if (_adapter.Attributes.State != CanState.SDEV_IDLE)
                _adapter.Services.Reset();

            _adapter.Open(_targetBaudrate);
        
            CanMessage[] txFrames = new CanMessage[512];

            for (int i = 0; i < 512; i++)
            {
                txFrames[i] = CanMessage.MessageStdC8;
                byte[] valuebytes = BitConverter.GetBytes((uint)i + 1);
                Buffer.BlockCopy(valuebytes, 0, txFrames[i].Data, 0, valuebytes.Length);
            }

            _adapter.Write(txFrames);
            Thread.Sleep(200);
            Assert.AreEqual(512, _adapter.Attributes.TxDrop);
        }

        [Test]
        public void _0015_2xAsyncWrite_1xAsyncRead()
        {
            if (_adapter.IsOpen)
                _adapter.Close();

            if (_adapter.Attributes.State != CanState.SDEV_IDLE)
                _adapter.Services.Reset();

            _adapter.Attributes.MsgGeneratorToHost = true;
            _adapter.Open(_targetBaudrate);

            AutoResetEvent write1Ev = new AutoResetEvent(false);
            Action write1 = () =>
                {
                                    
                    long startTick = DateTime.Now.Ticks;
                    do 
                    {
                        try
                        {
                            _adapter.Write(new CanMessage[] { CanMessage.MessageStdA4 });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message + "1" );
                            write1Ev.Set();
                        }
                    }while(DateTime.Now.Ticks - startTick < 1500 * 10000);
                    write1Ev.Set();
                };

            AutoResetEvent write2Ev = new AutoResetEvent(false);
            Action write2 = () =>
            {
                long startTick = DateTime.Now.Ticks;
                do
                {
                    try
                    {
                        _adapter.Write(new CanMessage[] { CanMessage.MessageStdC8 });
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message + "2");
                        write2Ev.Set();
                    }
                } while (DateTime.Now.Ticks - startTick < 1500 * 10000);
                write2Ev.Set();
            };


            AutoResetEvent readEv = new AutoResetEvent(false);
            CanMessage[] rxFrames = new CanMessage[65535];
            int offset = 0;
            Action read = () =>
            {
                long startTick = DateTime.Now.Ticks;
                do
                {
                    int length = _adapter.Attributes.PendingRxMessages;
                    _adapter.Read(rxFrames, offset, length);
                    offset += length;

                } while (DateTime.Now.Ticks - startTick < 1500 * 10000);
                readEv.Set();
            };

            read.BeginInvoke(null, null);
            write1.BeginInvoke(null, null);
            write2.BeginInvoke(null, null);
            write1Ev.WaitOne();
            write2Ev.WaitOne();
            readEv.WaitOne();
        }

    }
}
