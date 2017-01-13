



using System.ComponentModel;

namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using NUnit.Framework;
    using System.Windows.Forms;
    using System.Diagnostics;
    using System.Threading;

    [TestFixture]
    class UintTest_Timers
    {
        [Test]
        public void _0001_ThreadingTimerInUse()
        {
            var are = new AutoResetEvent(false);

            TimerCallback tc = (state) =>
            {
                Debug.WriteLine("System.Threading.Timer Tick");
            };

            var timer = new System.Threading.Timer(tc,null, 0, 100);

            are.WaitOne(2000);

        }

        [Test]
        public void _0002_ThreadingTimer20msPrecision()
        {
            var are = new AutoResetEvent(false);
            var watch = new Stopwatch();
            var expectPeriodMs = 20;
            var elapsedTimes = new List<double>();
            TimerCallback tc = (state) =>
            {
                Debug.WriteLine("System.Threading.Timer Tick");
                Debug.WriteLine("Elapsed:" + watch.ElapsedMilliseconds.ToString("N3"));
                elapsedTimes.Add(watch.ElapsedMilliseconds);
                watch.Restart();
            };
            var timer = new System.Threading.Timer(tc, null, expectPeriodMs, expectPeriodMs);
            watch.Start();
            are.WaitOne(1000);
            Debug.WriteLine("Average:" + elapsedTimes.Average().ToString("N3"));
            Debug.WriteLine("Max:" + elapsedTimes.Max().ToString("N3"));
            Debug.WriteLine("Min:" + elapsedTimes.Min().ToString("N3"));
            Debug.WriteLine("Relative Error:" + (((elapsedTimes.Average() - expectPeriodMs) / (double)expectPeriodMs) * 100).ToString("N3") + " %");
            //Average:30.531
            //Max:38.000
            //Min:22.000
            //Relative Error:52.656 %
        }

        [Test]
        public void _0003_WindowFormsTimerInUse()
        {
            var timer = new System.Windows.Forms.Timer();
            timer.Tick += (o, e) =>
            {
                Debug.WriteLine(" System.Windows.Forms.Timer");
            };

            timer.Enabled = true;
            timer.Interval = 100;
            timer.Start();

            var timestamp = DateTime.Now.Ticks;
            var exitFlag = false;
            while (exitFlag == false)
            {
                Application.DoEvents();
                if (DateTime.Now.Ticks - timestamp > 10000*1000)
                    exitFlag = true;
            }
        }

        [Test]
        public void _0004_WindowFormsTimer20msPrecision()
        {
            var timer = new System.Windows.Forms.Timer();
            var watch = new Stopwatch();
            var expectPeriodMs = 20;
            var elapsedTimes = new List<double>();
            timer.Tick += (o, e) =>
            {
                Debug.WriteLine("Elapsed:" + watch.ElapsedMilliseconds.ToString("N3"));
                elapsedTimes.Add(watch.ElapsedMilliseconds);
                watch.Restart();
                Debug.WriteLine("System.Windows.Forms.Timer");
            };

            timer.Enabled = true;
            timer.Interval = expectPeriodMs;   
            watch.Start();
            timer.Start();

            var timestamp = DateTime.Now.Ticks;
            var exitFlag = false;
            while (exitFlag == false)
            {
                Application.DoEvents();
                if (DateTime.Now.Ticks - timestamp >= 10000 * 1000)
                    exitFlag = true;
            }
            Debug.WriteLine("Average:" + elapsedTimes.Average().ToString("N3"));
            Debug.WriteLine("Max:" + elapsedTimes.Max().ToString("N3"));
            Debug.WriteLine("Min:" + elapsedTimes.Min().ToString("N3"));
            Debug.WriteLine("Relative Error:" + (((elapsedTimes.Average() - expectPeriodMs)/(double)expectPeriodMs)*100).ToString("N3") + " %");
            //Average:31.194
            //Max:72.000
            //Min:11.000
            //Relative Error:55.968 %
        }


        [Test]
        public void _0005_ThreadPollingTimer20msPrecision()
        {
            var are = new AutoResetEvent(false);
            var timestamp = DateTime.Now.Ticks;
            var expectPeriodMs = 20;
            var watch = new Stopwatch();
            var elapsedTimes = new List<double>();
            var exitFlag = false;
            var th = new Thread(
                new ThreadStart(() =>
                {
                    timestamp = DateTime.Now.Ticks;
                    do
                    {
                        if (DateTime.Now.Ticks - timestamp >= expectPeriodMs*10000)
                        {
                            timestamp = DateTime.Now.Ticks;
                            elapsedTimes.Add(watch.ElapsedMilliseconds);
                            watch.Restart();
                        }
                    } while (exitFlag == false);

                }));
            watch.Start();
            th.Start();
            are.WaitOne(1000);
            exitFlag = true;
            Debug.WriteLine("Average:" + elapsedTimes.Average().ToString("N3"));
            Debug.WriteLine("Max:" + elapsedTimes.Max().ToString("N3"));
            Debug.WriteLine("Min:" + elapsedTimes.Min().ToString("N3"));
            Debug.WriteLine("Relative Error:" + (((elapsedTimes.Average() - expectPeriodMs) / (double)expectPeriodMs) * 100).ToString("N3") + " %");
            //Average:19.367
            //Max:21.000
            //Min:19.000
            //Relative Error:-3.163 %
        }

              
        [Test]
        public void _0006_BackgroundWorkerPollingTimer20msPrecision()
        {
            var are = new AutoResetEvent(false);
            var timestamp = DateTime.Now.Ticks;
            var expectPeriodMs = 20;
            var watch = new Stopwatch();
            var elapsedTimes = new List<double>();
            var exitFlag = false;

            var worker = new BackgroundWorker();
            worker.DoWork += (o, e) =>
            {
                timestamp = DateTime.Now.Ticks;
                do
                {
                    if (DateTime.Now.Ticks - timestamp >= expectPeriodMs*10000)
                    {
                        timestamp = DateTime.Now.Ticks;
                        elapsedTimes.Add(watch.ElapsedMilliseconds);
                        watch.Restart();
                    }
                } while (exitFlag == false);
            };

            watch.Start();
            worker.RunWorkerAsync();
            are.WaitOne(1000);
            exitFlag = true;
            Debug.WriteLine("Average:" + elapsedTimes.Average().ToString("N3"));
            Debug.WriteLine("Max:" + elapsedTimes.Max().ToString("N3"));
            Debug.WriteLine("Min:" + elapsedTimes.Min().ToString("N3"));
            Debug.WriteLine("Relative Error:" + (((elapsedTimes.Average() - expectPeriodMs) / (double)expectPeriodMs) * 100).ToString("N3") + " %");
            //Average:19.449
            //Max:24.000
            //Min:19.000
            //Relative Error:-2.755 %
        }
    
    }
}
