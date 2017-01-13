



namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using System.Diagnostics;
    using System.Collections.ObjectModel;
    using System.Threading;
    using WinForms.Framework;

    using NUnit.Framework;


    [TestFixture]
    class UintTest_MessageSender
    {
        [Test]
        public void _0005()
        {
            var aev = new AutoResetEvent(false);
            var msgSender = new MockSendMsgCollection()
            {
                new MockSendMsgItem("No period", false, 0, 0, new byte[] {0x01}),
                new MockSendMsgItem("100ms Period", true, 100, 0x100, new byte[] {0x01, 0x02}),
                new MockSendMsgItem("1000ms Period", true, 1000, 0x100, new byte[] {0x01, 0x02}),
            };

            msgSender.Start();

            aev.WaitOne(5000);

            msgSender.Stop();
        }



        class MockSendMsgCollection : Collection<MockSendMsgItem>, IDisposable
        {
            public Exception LastException { get { return _lastException; } }
            private Exception _lastException = null;

            public SafeQueue<MockSendMsgItem> SendQueue
            {
                get { return _sendQueue; }
            }

            private readonly SafeQueue<MockSendMsgItem> _sendQueue;

            private bool _isRunning;
            private bool _disposed;
            private AutoResetEvent _shutdownEvent;
            private AutoResetEvent _readyToDisposeEvent;

            public MockSendMsgCollection()
            {
                _sendQueue = new SafeQueue<MockSendMsgItem>();
            }


            public void Start()
            {
                _isRunning = true;
                _shutdownEvent = new AutoResetEvent(false);
                _readyToDisposeEvent = new AutoResetEvent(false);
                var thread = new Thread(new ThreadStart(DoWork));
                thread.Start();
            }

            public void Stop()
            {
                Debug.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");

                if (_shutdownEvent != null)
                {
                    Debug.WriteLine("Stop is pending.");
                    _shutdownEvent.Set();
                }

                if (_readyToDisposeEvent.WaitOne())
                {
                    Debug.WriteLine("Stop is ready.");
                }
            }

            private void DoWork()
            {
                foreach (var item in this)
                    item.LastUpdateDateTimeTicks = DateTime.Now.Ticks;

                do
                {
                    try
                    {
                        var dateTimeNow = DateTime.Now.Ticks;
                        foreach (var item in this)
                        {
                            if (item.IsPeriod && (dateTimeNow - item.LastUpdateDateTimeTicks >= item.PeriodTime * 10000))
                            {
                                Debug.WriteLine("Last Period:" + ((dateTimeNow - item.LastUpdateDateTimeTicks) / (double)10000).ToString("N3"));
                                item.LastUpdateDateTimeTicks = dateTimeNow;
                                _sendQueue.Enqueue(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("DoWork is now shutdown by Loop Fuction Exception.");
                        _lastException = ex;
                        break;
                    }

                    if (_shutdownEvent.WaitOne(0))
                    {
                        Debug.WriteLine("DoWork is now shutdown!");
                        break;
                    }

                } while (_isRunning);

                _readyToDisposeEvent.Set();

                _isRunning = false;
            }



            /// <summary>
            ///  Public implementation of Dispose pattern callable by consumers. 
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// Protected implementation of Dispose pattern. 
            /// </summary>
            /// <param name="disposing"></param>
            protected virtual void Dispose(bool disposing)
            {
                if (_disposed)
                    return;
                if (disposing)
                {
                    if (_isRunning)
                    {
                        /*szabályos leállítás*/
                        Debug.WriteLine("Running-> Stop()");
                        Stop();
                    }

                    if (_shutdownEvent != null)
                    {
                        _shutdownEvent.Dispose();
                        _shutdownEvent = null;
                    }

                    if (_readyToDisposeEvent != null)
                    {
                        _readyToDisposeEvent.Dispose();
                        _readyToDisposeEvent = null;
                    }
                }
                _disposed = true;
            }
        }


        class MockSendMsgItem
        {
            public string Name { get; set; }
            public bool IsPeriod { get; set; }
            public int PeriodTime { get; set; }
            public uint ArbitrationId { get; set; }
            public byte[] Data { get; set; }
            public long LastUpdateDateTimeTicks { get; set; }

            public MockSendMsgItem(
                                    string name, 
                                    bool isPeriod,
                                    int periodTime,
                                    uint arbitrationId,
                                    byte[] data)
            {
                Name = name;
                IsPeriod = isPeriod;
                PeriodTime = periodTime;
                ArbitrationId = arbitrationId;
                Data = data;
            }
        }
    }
}
