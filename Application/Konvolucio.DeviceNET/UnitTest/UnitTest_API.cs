// -----------------------------------------------------------------------
// <copyright file="UnitTest_API.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.DeviceNet.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Konvolucio.DeviceNet;
    using Konvolucio.DeviceNet.SlaveDevices;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UnitTest_API
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

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void _0001_UsingDeviceBySlaveMacId()
        {
            DeviceGTR1 omron = Master.GetSlaveByMacId(2) as DeviceGTR1;    
            omron.AlllocateExplicitConnection();
            try
            {
                Assert.AreEqual(47, omron.Identity.VendorId.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                omron.ReleaseExplicitConnection();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void _0002_DeviceGTR1_Attribute()
        {
            DeviceGTR1 omron = Master.GetSlaveByMacId(2) as DeviceGTR1;
            omron.AlllocateExplicitConnection();
            try
            {
                Assert.AreEqual(47, omron.Identity.VendorId.Value, "VendorId");
                Assert.AreEqual(12, omron.Identity.DeviceType.Value, "DeviceType");
                Assert.AreEqual(1388, omron.Identity.ProductCode.Value, "ProductCode");
                Assert.AreEqual(257, omron.Identity.Revision.Value, "Revision");
                Assert.AreEqual(1, omron.Identity.Status.Value, "Status");
                Assert.AreEqual(5317938, omron.Identity.SerialNumber.Value, "SerialNumber");
                Assert.AreEqual("GRT1-DRT", omron.Identity.ProductName.Value, "ProductName");
                /*Assert.AreEqual(1, omron.Identity.HeartbeatInterval.Value, "HeartbeatInterval");*/
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                omron.ReleaseExplicitConnection();
            }
        }
    }
}
