
namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;
    using System.Threading;
    using NUnit.Framework;

    [TestFixture]
    class UnitTest_BeginInvoke
    {
        [Test]
        public void _0001_BeginInvoke()
        {
            var instance = new BeginInvokeClass();
            AutoResetEvent completeEvent = new AutoResetEvent(false);
            instance.ProgressChanged += (o, e) => 
            {
                Console.WriteLine(e.ProgressPercentage + "% " + e.UserState.ToString()); 
            };

            Action asyncMethod = () =>
            {
                instance.LongMethod();
            };

            AsyncCallback completeMethod = (iftAr) =>
            {
                completeEvent.Set();
                Console.WriteLine("Complete");
            };

            asyncMethod.BeginInvoke(completeMethod, null);
            completeEvent.WaitOne();
        }
    }

    public class BeginInvokeClass
    {
        public event ProgressChangedEventHandler ProgressChanged;
        public void LongMethod()
        {
            int total = 150;
            for (int i = 0; i < total; i++)
            {
                Thread.Sleep(10);
                OnProgressChanged(this, new ProgressChangedEventArgs((int)((i / (double)total) * 100), "Test"));
            }
        }
        protected void OnProgressChanged(object obj, ProgressChangedEventArgs e)
        {
            if (ProgressChanged != null)
                ProgressChanged(obj, e);
        }
    }
}
