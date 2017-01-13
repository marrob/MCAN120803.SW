
namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using NUnit.Framework;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows.Forms.VisualStyles;

    [TestFixture]
    class UnitTest_FactoryStartStopThread
    {


        [Test]
        public void _0001()
        {
            object lockObj =  new object();
    
            Thread th1 = new Thread(new ThreadStart(() =>
            {
                for (int i = 1; i < 100; i++)
                {
                    lock (lockObj)
                    {
                        Debug.WriteLine("TH1");   
                        Thread.Sleep(200);
                    }
                }
            }));

            Thread th2 = new Thread(new ThreadStart(() =>
            {
                for (int i = 1; i < 100; i++)
                {
                    lock (lockObj)
                    {
                        Debug.WriteLine("TH2");
                        Thread.Sleep(50);
                    }
                }
            }));

            th1.Start();
            th2.Start();
            th1.Join();
            th2.Join();
        }

        

        [Test]
        public void _0001_Start_Stop()
        {
            var wEvent = new AutoResetEvent(false);
            var me = new MockEngine();
            me.Start("normal");
            wEvent.WaitOne(100);
            me.Stop();
        }

        [Test]
        public void _0002_Working_With_Resource_LastException_is_NotNull()
        {
            var wEvent = new AutoResetEvent(false);
            var me = new MockEngine();
            me.Start("LastException");
            wEvent.WaitOne(100);
            if (me.IsRunning)
                me.Stop();
            me.Dispose();
        }
        [Test]
        public void _0002_Working_Rise_Loop_Exception()
        {
            var wEvent = new AutoResetEvent(false);
            var me = new MockEngine();
            me.Start("RiseLoopException");
            wEvent.WaitOne(5000);
            if (me.IsRunning)
                me.Stop();
            me.Dispose();
        }

        [Test]
        public void _0003_Stop_by_Dispose()
        {
            var wEvent = new AutoResetEvent(false);
            var me = new MockEngine();
            me.Start("normal");
            wEvent.WaitOne(100);
            me.Dispose();
        }

        [Test]
        public void _0004_Multiple_Start_Stop()
        {
            var wEvent = new AutoResetEvent(false);
            var me = new MockEngine();
            for (int i = 0; i < 3; i++)
            {
                me.Start("normal");
                wEvent.WaitOne(50);
                me.Stop();
            }
            me.Dispose();
        }

        [Test]
        public void _0005_Parameterzed_Thread_Start()
        {
            AutoResetEvent ev = new AutoResetEvent(false);
            string temp = string.Empty;
            Action<object> doWork = (arg) =>
            {
                temp = arg.ToString();
                ev.Set();
            };
            Thread th = new Thread(new ParameterizedThreadStart(doWork));
            th.Start("Hello World");
            ev.WaitOne();
            Assert.AreEqual("Hello World", temp);
        }

        class MockResource : IDisposable
        {
            private bool _disposed;
            public Exception LastException { get; private set; }
            private int _counter = 0;

            public void DoMethod(object arg)
            {
                if ((arg.ToString() == "LastException") && (_counter > 5))
                {
                    Debug.WriteLine("Resource object is working." + _counter);
                    LastException = new Exception("Resource rise LastExcpetion...");
                }
                else
                {
                    Debug.WriteLine("Resource object is working." + _counter);
                    _counter++;                   
                }
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (_disposed)
                    return;
                if (disposing)
                {
                    Debug.WriteLine("Resource was Disoposed.");
                }
                _disposed = true;
            }
        }


        class MockEngine : IDisposable
        {
            private bool _disposed;
            private AutoResetEvent _shutdownEvent;
            private AutoResetEvent _readyToDisposeEvent;
            private MockResource _mockResource;
            private bool _isRunning;

            public bool IsRunning
            {
                get { return _isRunning; }
            }

            public void Start(object arg)
            {
                Debug.WriteLine("Engine is starting.");
                _shutdownEvent = new AutoResetEvent(false);
                _readyToDisposeEvent = new AutoResetEvent(false);
                _mockResource = new MockResource();
                Thread th = new Thread(new ParameterizedThreadStart(DoWork));
                th.Start(arg);
                Debug.WriteLine("Engine is started.");
            }
            public void Stop()
            {
                if (_shutdownEvent != null)
                {
                    /*Jezem a fő ciklusnak, hogy amint tudja break-eljen a While-ból.*/
                    Debug.WriteLine("Stop is pending.");
                    _shutdownEvent.Set();
                }
                /*Addig nem megyek tovább amíg a While ciklus végén lévő esemény jelez
                    * Ez biztosítja, hogy többször biztos nem fut le a ciklus, és a cikluson belül nincs már végrehajtás.*/
                _readyToDisposeEvent.WaitOne();
                Debug.WriteLine("Stop is ready by Stop Method");
            }

            private void DoWork(object arg)
            {
                _isRunning = true;
                while (_isRunning)
                {
                    if (_mockResource.LastException != null)
                    {
                        Debug.WriteLine("DoWork is now shutdown by Resource LastException is not null.");
                        break;
                    }

                    if (_shutdownEvent.WaitOne(0))
                    {
                        Debug.WriteLine("DoWork is now shutdown by shutDownEvent.");
                        break;
                    }

                    try
                    {
                        _mockResource.DoMethod(arg);

                        if (arg.ToString() == "RiseLoopException")
                            throw new Exception("Loop Exception.");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("DoWork is now shutdown by Loop Exception. Message:" + ex.Message);
                        break;
                    }

                }
                /*Itt kezdődik a vége*/
                if (_mockResource != null)
                {
                    /*Erőforrás felszabadítása, itt a szabályos.*/
                    _mockResource.Dispose();
                    _mockResource = null;
                }
                /*Leáll és a Stop-nak jelzem, hogy mehet tovább.*/
                _isRunning = false;
                _readyToDisposeEvent.Set();

                Debug.WriteLine("Loop is colsed...");
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (_disposed)
                    return;
                if (disposing)
                {

                    if (_isRunning)
                    {  /*szabályos leállítás*/
                        Console.WriteLine("Running-> Stop()");
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
    }
}

