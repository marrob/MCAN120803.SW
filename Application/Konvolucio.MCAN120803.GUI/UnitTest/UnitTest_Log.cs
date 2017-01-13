

namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using System.Diagnostics;
    using System.ComponentModel;
    using System.Threading;


    using AppModules.Log.Model;

    using Common;

    [TestFixture]
    class UnitTest_Log
    {
        string LogLocation = @"C:\Users\Margit Róbert\Documents\Konvolucio\MCAN120803\Sample Projects\OMRON GRT1-DRT";
        string ProjectName = "OMRON GRT1-DRT";

        [Test]
        public void _0001_ListingLogDirectory_Async()
        {
            var loadingCompleteEvent = new AutoResetEvent(false);

            ILogFileCollection logfiles = new LogFileCollection();

            Action asyncMethod = () =>
            { 
                logfiles.Load(LogLocation, ProjectName); 
            };
            
            AsyncCallback callback = (iftAr) => 
            { 
                loadingCompleteEvent.Set(); 
            };
            
            logfiles.ProgressChanged += (o, e) => { Console.WriteLine("Percent:" + e.ProgressPercentage + "%" + " Message:" + e.UserState); };

            asyncMethod.BeginInvoke(callback, null);

            loadingCompleteEvent.WaitOne();
            foreach (ILogFileItem item in logfiles)
                Console.WriteLine(item.Name);

            
            Assert.IsTrue(logfiles.Count != 0, "Nincs log fál a kövekező utvonalon:" + LogLocation);
        }

        [Test]
        public void _0002_ListingLogDirectory_Sync()
        {
            ILogFileCollection logfiles = new LogFileCollection();
            logfiles.ProgressChanged += (o, e) => { Console.WriteLine("Percent:" + e.ProgressPercentage + "%" + " Message:" + e.UserState); };
            logfiles.Load(LogLocation, ProjectName);
            foreach (ILogFileItem item in logfiles)
                Console.WriteLine(item.Name);
            Assert.IsTrue(logfiles.Count != 0, "Nincs log fál a kövekező utvonalon:" + LogLocation);
       
        }

        [Test]
        public void _0003_CreatingNewLog()
        {
            var logFileName = DateTime.Now.ToString("yyMMdd HHmmss");
            ILogFileItem log = new LogFileItem(LogLocation, ProjectName, logFileName);
            Assert.IsTrue(System.IO.File.Exists(log.Path), "Nem hozta létre a log fájlt.");
        }

        [Test]
        public void _0004_Instert_StopWatch()
        {
            var logFileName = DateTime.Now.ToString("yyMMdd HHmmss") + "_0003_CreatingNewLog_Insert";
            ILogFileItem log = new LogFileItem(LogLocation, ProjectName, logFileName);
            Assert.IsTrue(System.IO.File.Exists(log.Path), "Nem hozta létre a log fájlt.");

            var watch = new Stopwatch();
            watch.Start();
            log.Messages.AddToStorageBegin();
            for (int i = 0; i < 1000; i++)
            {
                log.Messages.AddToStorage("Proba", DateTime.Now, MessageDirection.Received, ArbitrationIdType.Standard, 0x0FF, false, new byte[]{0x00, 0x01, 0x02, 0x03, 0x05}, "nincs", "nincs");
            }
            log.Messages.AddToStorageEnd();
            watch.Stop();
            Console.WriteLine("Beirás ideje:" +  (watch.ElapsedMilliseconds).ToString() + "ms");
        }

        [Test]
        public void _0005_CreateNew_Insert_2_Record()
        {
            for (int repeat = 0; repeat < 1000; repeat++)
            {
                var logFileName = "_" + repeat.ToString("0000") + "" + System.Reflection.MethodBase.GetCurrentMethod().Name;
                ILogFileItem log = new LogFileItem(LogLocation, ProjectName, logFileName);
                Assert.IsTrue(System.IO.File.Exists(log.Path), "Nem hozta létre a log fájlt.");

                var watch = new Stopwatch();
                watch.Start();
                log.Messages.AddToStorageBegin();
                for (int i = 0; i < 1; i++)
                {
                    log.Messages.AddToStorage("Proba", DateTime.Now, MessageDirection.Transmitted, ArbitrationIdType.Standard, 0x0FF, false, new byte[]{0x00, 0x01, 0x02, 0x03, 0x05}, "nincs", "nincs");
                    log.Messages.AddToStorage("Proba", DateTime.Now, MessageDirection.Received, ArbitrationIdType.Standard, 0x0FF, false, new byte[]{0x00, 0x01, 0x02, 0x03, 0x05}, "nincs", "nincs");
                }
                log.Messages.AddToStorageEnd();
                watch.Stop();
                Console.WriteLine("Beirás ideje:" + (watch.ElapsedMilliseconds).ToString() + "ms");
            }
        }

        [Test]
        public void _0006_CreateNew_Insert_1000_Record()
        {
            for (int repeat = 0; repeat < 10; repeat++)
            {
                var logFileName = "_" + repeat.ToString("0000") + System.Reflection.MethodBase.GetCurrentMethod().Name;
                ILogFileItem log = new LogFileItem(LogLocation, ProjectName, logFileName);
                Assert.IsTrue(System.IO.File.Exists(log.Path), "Nem hozta létre a log fájlt.");

                var watch = new Stopwatch();
                watch.Start();
                log.Messages.AddToStorageBegin();
                for (int i = 0; i < 1000; i++)
                {
                    log.Messages.AddToStorage("Proba", DateTime.Now, MessageDirection.Received, ArbitrationIdType.Standard, 0x0FF, false, new byte[] { 0x00, 0x01, 0x02, 0x03, 0x05 }, "nincs", "nincs");
                }
                log.Messages.AddToStorageEnd();
                watch.Stop();
                Console.WriteLine("Beirás ideje:" + (watch.ElapsedMilliseconds).ToString() + "ms");
            }
        }

        [Test]
        public void _0007_CreateNew_Insert_1000000_Record()
        {
            for (int repeat = 0; repeat < 1; repeat++)
            {
                var logFileName = "_" + repeat.ToString("0000") + System.Reflection.MethodBase.GetCurrentMethod().Name;
                ILogFileItem log = new LogFileItem(LogLocation, ProjectName, logFileName);
                Assert.IsTrue(System.IO.File.Exists(log.Path), "Nem hozta létre a log fájlt.");

                var watch = new Stopwatch();
                watch.Start();
                log.Messages.AddToStorageBegin();
                for (int i = 0; i < 1000000; i++)
                {
                    log.Messages.AddToStorage("Proba", DateTime.Now, MessageDirection.Received, ArbitrationIdType.Standard, 0x0FF, false, new byte[] { 0x00, 0x01, 0x02, 0x03, 0x05 }, "nincs", "nincs");
                }
                log.Messages.AddToStorageEnd();
                watch.Stop();
                Console.WriteLine("Beirás ideje:" + (watch.ElapsedMilliseconds).ToString() + "ms");
            }
        }

        [Test]
        public void _0008_CreateNew_and_Load()
        {
            /*Egyedi fáljnév*/
            var logFileName = "_0005_CreateNew_and_Load" + Guid.NewGuid().ToString();
            ILogFileItem newLog = null;
            if (!System.IO.File.Exists(LogLocation + logFileName + ".s3db"))
            {
                newLog = new LogFileItem(LogLocation, ProjectName, logFileName);
                Assert.IsTrue(System.IO.File.Exists(newLog.Path), "Nem hozta létre a log fájlt.");
                newLog.Messages.AddToStorageBegin();
                for (int i = 0; i < 1000; i++)
                    newLog.Messages.AddToStorage("Proba", DateTime.Now, MessageDirection.Received, ArbitrationIdType.Standard, 0x0FF, false, new byte[] { 0x00, 0x01, 0x02, 0x03, 0x05 }, "nincs", "nincs");
                newLog.Messages.AddToStorageEnd();
            }

            var loadedLog = new LogFileItem(newLog.Path);
            loadedLog.ProgressChanged += (o, e) => { Console.WriteLine("Percent:" + e.ProgressPercentage + "%" + " Message:" + e.UserState); };
            loadedLog.Load();
            Assert.AreEqual(1000, loadedLog.Messages.Count, "Beirt és várt üzenetek száma nem egyezik.");            
        }

        [Test]
        public void _0009_PropretyTest()
        {
            ILogFileCollection logfiles = new LogFileCollection();
            logfiles.ProgressChanged += (o, e) => { Console.WriteLine("Percent:" + e.ProgressPercentage + "%" + " Message:" + e.UserState); };
            logfiles.Load(LogLocation, ProjectName);

            var logFileName = DateTime.Now.ToString("yyMMdd HHmmss") + "_0006_PropretyTest_" + Guid.NewGuid().ToString() ;

            ILogFileItem log = null;
            if (!System.IO.File.Exists(LogLocation + logFileName + ".s3db"))
            {
                log = new LogFileItem(LogLocation, ProjectName, logFileName);
                Assert.IsTrue(System.IO.File.Exists(log.Path), "Nem hozta létre a log fájlt.");
                log.Messages.AddToStorageBegin();
                for (int i = 0; i < 2; i++)
                {
                    log.Messages.AddToStorage("Proba", DateTime.Now, MessageDirection.Transmitted, ArbitrationIdType.Standard, 0x0FF, false, new byte[] { 0x00, 0x01, 0x02, 0x03, 0x05 }, "nincs", "nincs");
                    log.Messages.AddToStorage("Proba", DateTime.Now, MessageDirection.Received, ArbitrationIdType.Standard, 0x0FF, false, new byte[] { 0x00, 0x01, 0x02, 0x03, 0x05 }, "nincs", "nincs");
                }
                log.Messages.AddToStorageEnd();
            }



            AutoResetEvent completeEvetn = new AutoResetEvent(false);
            log.Statistics.PropertyChanged += (o, e) =>
            {
                Assert.True(e.PropertyName == "ReceivedMessageCount" || e.PropertyName == "TransmittedMessageCount");
                completeEvetn.Set();
                    
            };

            var logFile = (logfiles as LogFileCollection).FirstOrDefault(n => n.Name == logFileName);
            log.Load();

            Assert.False(completeEvetn.WaitOne(1000) == false, "Nem jött esemény.");

            Assert.AreEqual(2, log.Statistics.ReceivedMessageCount);
            Assert.AreEqual(2, log.Statistics.TransmittedMessageCount);

            foreach (var item in log.Messages)
                Console.WriteLine(item.ToString());

        }

        [Test]
        public void _0010_NewLogFile_Add_LogFileCollection()
        {
            var logFileName = "_0007_NewLogFile_Add_LogFileCollection" + Guid.NewGuid().ToString();
            var newLog = new LogFileItem(LogLocation, ProjectName, logFileName);
            Assert.IsTrue(System.IO.File.Exists(newLog.Path), "Nem hozta létre a log fájlt.");
            newLog.Messages.AddToStorageBegin();
            for (int i = 0; i < 100; i++)
            {
                newLog.Messages.AddToStorage("Proba", DateTime.Now, MessageDirection.Received, ArbitrationIdType.Standard, 0x0FF, false, new byte[] { 0x00, 0x01, 0x02, 0x03, 0x05 }, "nincs", "nincs");
            }
            newLog.Messages.AddToStorageEnd();

            var logFiles = new LogFileCollection();
            logFiles.Load(LogLocation, ProjectName);
            AutoResetEvent completeEvent = new AutoResetEvent(false);
            logFiles.ListChanged += (o, e) =>
            {
                if (e.ListChangedType == ListChangedType.ItemAdded)
                {
                    completeEvent.Set();
                }
            };
            logFiles.Add(newLog);
            Assert.True(completeEvent.WaitOne(1000), "Nem j0tt meg az esény az új logfile beszurásáról.");
        }

        [Test]
        public void _0011_NewLogFileModifyDescription()
        {
            var logFileName = "_0008_NewLogFileModifyDescription" + Guid.NewGuid().ToString();
            var newLog = new LogFileItem(LogLocation, ProjectName, logFileName);
            Assert.IsTrue(System.IO.File.Exists(newLog.Path), "Nem hozta létre a log fájlt.");

            string expect = "Hello World";
            newLog.Info.Description = expect;

            Assert.AreEqual(expect, newLog.Info.Description);

            var loadLog = new LogFileItem(newLog.Path);
            loadLog.Load();
        }

        [Test]
        public void _0012_RepeatedLogFileName()
        {
            var logFileName = "_0012_RepeatedLogFileName";
            new LogFileItem(LogLocation, ProjectName, logFileName);           
            new LogFileItem(LogLocation, ProjectName, logFileName);
            new LogFileItem(LogLocation, ProjectName, logFileName);
        }
    }
}
