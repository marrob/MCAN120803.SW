
namespace Konvolucio.DeviceNet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using NUnit.Framework;

    [TestFixture]
    class UnitTest_Explorirng
    {

        DeviceNetMaster Master;


        [TestFixtureSetUp]
        public void Setup()
        {
            Master = new DeviceNetMaster();
            Master.Connect("3869366E3133");
        }

        [TestFixtureTearDown]
        public void Clean()
        {
            Master.Dispose();
        }

        [Test]
        public void _0001_Exploring()
        {
            AutoResetEvent complete = new AutoResetEvent(false);
            Master.Exploring.Completed += (o, ea) =>
            {
                Console.WriteLine("Exploring.Completed");
                complete.Set();
            };
            Master.Exploring.ProgressChange += (o, ea) =>
            {
                Console.WriteLine("Exploring.ProgressChange:" + ea.ProgressPercentage.ToString());
            };

            Master.Exploring.RunAsync();
            complete.WaitOne();
        }

    }
}
