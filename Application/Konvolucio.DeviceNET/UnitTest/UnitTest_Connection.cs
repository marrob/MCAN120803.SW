
namespace Konvolucio.DeviceNet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using NUnit.Framework;
    using Konvolucio.MCAN120803.API;
    using Konvolucio.DeviceNet.Common;

    [TestFixture]
    class UnitTest_Connection
    {

        CanAdapterDevice Adapter;

        [TestFixtureSetUp]
        public void Setup()
        {
            Adapter = new CanAdapterDevice();
            Adapter.ConnectTo("3869366E3133");
            Adapter.Attributes.Termination = true;
            Adapter.Open(250000);
        }

        [TestFixtureTearDown]
        public void Clean()
        {
            Adapter.Close();
            Adapter.Disconnect();
            Adapter.Dispose();
        }

        /// <summary>
        /// 2016.07.26 10:02:37.295	00000416	00 4B 03 01 01 00
        /// 2016.07.26 10:02:37.296	00000413	00 CB 00
        /// 2016.07.26 10:02:37.297	00000414	00 0E 01 01 07
        /// 2016.07.26 10:02:37.299	00000413	80 00 8E 08 47 52 54 31
        /// 2016.07.26 10:02:37.300	00000414	80 C0 00
        /// 2016.07.26 10:02:37.301	00000413	80 81 2D 44 52 54
        /// 2016.07.26 10:02:37.302	00000414	80 C1 00
        /// 2016.07.26 10:02:37.302	00000414	00 05 01 01
        /// 2016.07.26 10:02:37.304	00000413	00 85
        /// </summary>
        [Test]
        public void _0003_Connected_Reset()
        {
            var unconnected = new UnconnectedSalveDevice(Adapter);
            var connected = new ConnectedSalveDevice(Adapter, 0x00);
            string porductName = string.Empty;
            try
            {
                unconnected.AllocateExplicitConnection(0x02);
                byte[] bytes = connected.GetAttribute(0x02, 1, 1, 7);
                porductName = Encoding.UTF8.GetString(bytes, 1, bytes[0]);
                connected.ObjectResetService(0x02, 0x01, 0x01);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            Assert.AreEqual("GRT1-DRT", porductName);
        }

     
    }
}
