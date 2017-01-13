
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
    class UnitTest_Attribute
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
        /// 2016.07.26 09:58:27.827	00000416	00 4B 03 01 01 00
        /// 2016.07.26 09:58:27.828	00000413	00 CB 00
        /// 2016.07.26 09:58:27.829	00000416	00 4C 03 01 01
        /// 2016.07.26 09:58:27.831	00000413	00 CC
        /// </summary>
        [Test]
        public void _0001_Allacate_Release()
        {
            var unconnected = new UnconnectedSalveDevice(Adapter);
            unconnected.AllocateExplicitConnection(0x02);
            unconnected.ReleaseExplicitConnection(0x02);
        }

        /// <summary>
        /// 2016.07.25 20:58:09.845	00000416	00 4B 03 01 01 00
        /// 2016.07.25 20:58:09.847	00000413	00 CB 00
        /// 2016.07.25 20:58:09.854	00000414	00 0E 01 01 03
        /// 2016.07.25 20:58:09.856	00000413	00 8E 6C 05
        /// 2016.07.25 20:58:09.860	00000416	00 4C 03 01 01
        /// 2016.07.25 20:58:09.861	00000413	00 CC
        /// </summary>
        [Test]
        public void _0002_Connected_GetProductCode()
        {
            var unconnected = new UnconnectedSalveDevice(Adapter);
            var connected = new ConnectedSalveDevice(Adapter, 0x00);
            unconnected.AllocateExplicitConnection(0x02);
            ushort porductCode = 0;
            try
            {
                porductCode = BitConverter.ToUInt16(connected.GetAttribute(0x02, 1, 1, 3), 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                unconnected.ReleaseExplicitConnection(0x02);
            }
            Assert.AreEqual(1388, porductCode);
        }

        /// <summary>
        /// 2016.07.25 21:19:28.996	00000416	00 4B 03 01 01 00
        /// 2016.07.25 21:19:28.998	00000413	00 CB 00
        /// 2016.07.25 21:19:29.002	00000414	00 0E 01 01 07
        /// 2016.07.25 21:19:29.004	00000413	80 00 8E 08 47 52 54 31
        /// 2016.07.25 21:19:29.007	00000414	80 C0 00
        /// 2016.07.25 21:19:29.008	00000413	80 81 2D 44 52 54
        /// 2016.07.25 21:19:29.012	00000414	80 C1 00
        /// 2016.07.25 21:19:29.013	00000416	00 4C 03 01 01
        /// 2016.07.25 21:19:29.015	00000413	00 CC
        /// </summary>
        [Test]
        public void _0003_Connected_GetProductName()
        {
            var unconnected = new UnconnectedSalveDevice(Adapter);
            var connected = new ConnectedSalveDevice(Adapter, 0x00);
            string porductName = string.Empty;
            try
            {
                unconnected.AllocateExplicitConnection(0x02);
                byte[] bytes = connected.GetAttribute(0x02, 1, 1, 7);
                porductName = Encoding.UTF8.GetString(bytes, 1, bytes[0]); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                unconnected.ReleaseExplicitConnection(0x02);
            }
            Assert.AreEqual("GRT1-DRT", porductName);
        }

        /// <summary>
        /// 2016.07.26 09:54:25.818	00000416	00 4B 03 01 01 00
        /// 2016.07.26 09:54:25.819	00000413	00 CB 00
        /// 2016.07.26 09:54:25.825	00000414	00 0E 05 01 09
        /// 2016.07.26 09:54:25.826	00000413	00 8E C4 09
        /// 2016.07.26 09:54:25.835	00000414	00 10 05 01 09 82 00
        /// 2016.07.26 09:54:25.837	00000413	00 90 82 00
        /// 2016.07.26 09:54:25.838	00000414	00 0E 05 01 09
        /// 2016.07.26 09:54:25.839	00000413	00 8E 82 00
        /// 2016.07.26 09:54:25.841	00000416	00 4C 03 01 01
        /// 2016.07.26 09:54:25.842	00000413	00 CC
        /// </summary>
        [Test]
        public void _0004_ExpectPacketRate_GetSet()
        {
            var unconnected = new UnconnectedSalveDevice(Adapter);
            var connected = new ConnectedSalveDevice(Adapter, 0x00);
            ushort expectPacketRate = 0;
            byte[] bytes = null;
            try
            {
                unconnected.AllocateExplicitConnection(0x02);
                bytes = connected.GetAttribute(0x02, 5, 1, 9);
                expectPacketRate = BitConverter.ToUInt16(bytes, 0);
                Assert.AreEqual(2500, expectPacketRate);


                connected.SetAttribute(0x02, 5, 1, 9, BitConverter.GetBytes((ushort)0x82));
                bytes = connected.GetAttribute(0x02, 5, 1, 9);
                expectPacketRate = BitConverter.ToUInt16(bytes, 0);
                Assert.AreEqual(0x82, expectPacketRate);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                unconnected.ReleaseExplicitConnection(0x02);
            }
        }
    }
}
