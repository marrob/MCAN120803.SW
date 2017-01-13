
namespace Konvolucio.MCAN120803.API.UnitTest
{
    using System;
    using NUnit.Framework;
    using System.Threading;
    using System.Diagnostics;

    [TestFixture]
    class UnitTest_Dual_Adapters_Speed
    {
        [Test]
        public void _0001_Async_5000_Bps()
        {
            AsyncTest(512, 5000);
        }

        [Test]
        public void _0002_Async_5555_Bps()
        {
            AsyncTest(512, 5555);
        }

        [Test]
        public void _0003_Async_6250_Bps()
        {
            AsyncTest(512, 6250);
        }

        [Test]
        public void _0004_Async_8000_Bps()
        {
            AsyncTest(512, 8000);
        }

        [Test]
        public void _0005_Async_10000_Bps()
        {
            AsyncTest(512, 10000);
        }

        [Test]
        public void _0006_Async_12500_Bps()
        {
            AsyncTest(512, 12500);
        }

        [Test]
        public void _0007_Async_15625_Bps()
        {
            AsyncTest(512, 15625);
        }

        [Test]
        public void _0008_Async_16000_Bps()
        {
            AsyncTest(512, 16000);
        }

        [Test]
        public void _0009_Async_20000_Bps()
        {
            AsyncTest(512, 20000);
        }

        [Test]
        public void _0010_Async_25000_Bps()
        {
            AsyncTest(512, 25000);
        }

        [Test]
        public void _0011_Async_31250_Bps()
        {
            AsyncTest(512, 31250);
        }

        [Test]
        public void _0012_Async_33333_Bps()
        {
            AsyncTest(512, 33333);
        }

        [Test]
        public void _0013_Async_40000_Bps()
        {
            AsyncTest(512, 40000);
        }

        [Test]
        public void _0014_Async_50000_Bps()
        {
            AsyncTest(512, 50000);
        }

        [Test]
        public void _0015_Async_62500_Bps()
        {
            AsyncTest(512, 62500);
        }

        [Test]
        public void _0016_Async_80000_Bps()
        {
            AsyncTest(512, 80000);
        }

        [Test]
        public void _0017_Async_83333_Bps()
        {
            AsyncTest(512, 83333);
        }

        [Test]
        public void _0018_Async_100000_Bps()
        {
            AsyncTest(512, 100000);
        }

        [Test]
        public void _0019_Async_125000_Bps()
        {
            AsyncTest(512, 125000);
        }

        [Test]
        public void _0020_Async_166666_Bps()
        {
            AsyncTest(512, 166666);
        }

        [Test]
        public void _0021_Async_200000_Bps()
        {
            AsyncTest(512, 200000);
        }

        [Test]
        public void _0022_Async_250000_Bps()
        {
            AsyncTest(512, 250000);
        }

        [Test]
        public void _0023_Async_400000_Bps()
        {
            AsyncTest(512, 400000);
        }

        [Test]
        public void _0024_Async_500000_Bps()
        {
            AsyncTest(512, 500000);
        }

        [Test]
        public void _0025_Async_800000_Bps()
        {
            AsyncTest(512, 800000);
        }

        [Test]
        public void _0026_Async_1000000Bps()
        {
            AsyncTest(8191, 1000000);
        }


        void AsyncTest(int msgCount, uint baudrate)
        {
            CanAdapterDevice adapterWriter = new CanAdapterDevice();
            var selectedAdapterWriter = CanAdapterDevice.GetAdapters()[0]; //{CAN Bus Adpater - MCAN120803 3873366E3133 }
            adapterWriter.ConnectTo(selectedAdapterWriter);

            CanAdapterDevice adapterReader = new CanAdapterDevice();
            var selectedAdapterReader = CanAdapterDevice.GetAdapters()[1]; //{CAN Bus Adpater - MCAN120803 387536633133 }
            adapterReader.ConnectTo(selectedAdapterReader);

            Stopwatch watch = new Stopwatch();

            if (adapterWriter.Attributes.State != CanState.SDEV_IDLE)
                adapterWriter.Services.Reset();

            if (adapterReader.Attributes.State != CanState.SDEV_IDLE)
                adapterReader.Services.Reset();

            adapterWriter.Attributes.Termination = true;
            adapterReader.Attributes.Termination = true;

            try
            {

                CanMessage[] txFrames = new CanMessage[msgCount];

                Action writer = () =>
                    {
                        try
                        {
                            for (int i = 0; i < msgCount; i++)
                            {
                                txFrames[i] = CanMessage.MessageStdC8;
                                byte[] valuebytes = BitConverter.GetBytes((uint)i + 1);
                                Buffer.BlockCopy(valuebytes, 0, txFrames[i].Data, 0, valuebytes.Length);
                            }
                            adapterWriter.Write(txFrames);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    };


                CanMessage[] rxFrames = new CanMessage[msgCount];
                int offset = 0;

                watch.Start();
                long startTick = DateTime.Now.Ticks;
                AutoResetEvent doneEv = new AutoResetEvent(false);
                Action reader = () =>
                    {
                        try
                        {
                            do
                            {
                                if (DateTime.Now.Ticks - startTick > 15000 * 10000)
                                {
                                    throw new Exception("Timeout");
                                }

                                int length = adapterReader.Attributes.PendingRxMessages;
                                adapterReader.Read(rxFrames, offset, length);
                                //Console.WriteLine("Pending: " + length + "Length: " + offset );
                                offset += length;

                            } while (offset != msgCount);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("RxTotal:" + adapterReader.Attributes.RxTotal.ToString());
                            Console.WriteLine("RxDrop:" + adapterReader.Attributes.RxDrop.ToString());    

                        }
                        doneEv.Set();
                    };

                adapterWriter.Open(baudrate);
                adapterReader.Open(baudrate);

                reader.BeginInvoke(null, null);
                Thread.Sleep(50);
                writer.BeginInvoke(null, null);

                doneEv.WaitOne();
                watch.Stop();

                double calcBps1 = ((msgCount * 8E+0) /(watch.ElapsedMilliseconds / 1000E+0));
                double calcBps2 = ((msgCount * 8E+0) / (((DateTime.Now.Ticks - startTick) / 10000E+0) / 1000E+0));

                uint rxTotal = adapterReader.Attributes.RxTotal;
                Console.WriteLine("Reader RxDrop: " + adapterReader.Attributes.RxDrop);
                Console.WriteLine("Writer TxDrop: " + adapterWriter.Attributes.TxDrop);
                Console.WriteLine("Átjött Host FIFO-jába: " + offset);
                Console.WriteLine("Számított Baudrate 1: " + calcBps1.ToString() );
                Console.WriteLine("Számított Baudrate 2: " + calcBps2.ToString());
                adapterReader.Close();
                adapterWriter.Close();

                Assert.AreEqual(msgCount, rxTotal, "Az adapter FIFO-jába nem jött meg minden.");
                Assert.AreEqual(msgCount, offset, "A Host FIFO-jaba nem jött meg minden.");
            }
            catch
            {
                throw;
            }
            finally
            {
                adapterWriter.Disconnect();
                adapterReader.Disconnect();
            }
        }
    }
}
