
namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using NUnit.Framework;

    [TestFixture]
    class UnitTest_BackgoroundWorker
    {
        [Test]
        public void _0001_BackgroundWorker()
        {
            var instance = new BackgorundWorkerClass();
            AutoResetEvent completeEvent = new AutoResetEvent(false);
            instance.ProgressChanged += (o, e) => { Console.WriteLine(e.ProgressPercentage + "% " + e.UserState.ToString()); };
            instance.Complete += (o, e) => 
            {
                completeEvent.Set();
                Console.WriteLine("Complete"); 
            };
            instance.LongMethodAsync();
            completeEvent.WaitOne();
        }
    }

    public class BackgorundWorkerClass
    {
        public event ProgressChangedEventHandler ProgressChanged;
        public event EventHandler Complete;
        public void LongMethodAsync()
        {
            var worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;

            worker.DoWork += (o, e) =>
            {
                int total = 150;
                for (int i = 0; i < total; i++)
                {
                    Thread.Sleep(10);
                    worker.ReportProgress((int)((i / (double)total) * 100), "Test");
                }
            };
            worker.RunWorkerCompleted += (o, e) => { OnComplete(this); };
            worker.ProgressChanged += (o, e) => { OnProgressChanged(this, e); };
            worker.RunWorkerAsync();
        }
        protected void OnProgressChanged(object obj, ProgressChangedEventArgs e)
        {
            if (ProgressChanged != null)
                ProgressChanged(obj, e);
        }
        protected void OnComplete(object obj)
        {
            if (Complete != null)
                Complete(this, EventArgs.Empty);
        }
    }
}
