

namespace Konvolucio.WinUSB.UnitTest
{
    using NUnit.Framework;

    [TestFixture]
    class CanAdapterTest
    {

        [Test]
        public void FindDeviceByDeviceInterfaceGuid()
        {
            USBDeviceInfo[] dies = USBDevice.GetDevices("{AA40624D-0F4B-4F4F-8E23-BA4209EE3AF2}");
            USBDeviceInfo di = dies[0];
            USBDevice dev = new USBDevice(di.DevicePath);
            Assert.AreEqual("Konvolucio Bt", dev.Descriptor.Manufacturer);
            dev.Dispose();
        }

        [Test]
        public void SendCanMsg()
        {
            const byte CAN_MSG_EP_OUT_ADDR = 0x02;

            USBDeviceInfo[] dies = USBDevice.GetDevices("{AA40624D-0F4B-4F4F-8E23-BA4209EE3AF2}");
            USBDeviceInfo di = dies[0];
            byte[] status = new byte[] { 0xAA,0x55,0xFF };
            USBDevice dev = new USBDevice(di.DevicePath);
            dev.Pipes[CAN_MSG_EP_OUT_ADDR].Policy.PipeTransferTimeout = 1000;
            dev.Pipes[CAN_MSG_EP_OUT_ADDR].Write(status);
            dev.Dispose();
        }
    }
}
