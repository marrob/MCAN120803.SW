

namespace Konvolucio.WinUSB.UnitTest
{
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class STLinkTest
    {

        [Test]
        public void OpenClose()
        {

            USBDeviceInfo[] dies = USBDevice.GetDevices("{A5DCBF10-6530-11D2-901F-00C04FB951ED}");
            USBDeviceInfo di = dies.FirstOrDefault(n => n.Manufacturer == "STMicroelectronics");
            USBDevice dev = new USBDevice(di.DevicePath);
            Assert.AreEqual("STMicroelectronics", dev.Descriptor.Manufacturer);
            dev.Dispose();
            dev = new USBDevice(di.DevicePath);
            Assert.AreEqual("STMicroelectronics", dev.Descriptor.Manufacturer);
            dev.Dispose();
            dev = new USBDevice(di.DevicePath);
            Assert.AreEqual("STMicroelectronics", dev.Descriptor.Manufacturer);
            dev.Dispose();
        }

        [Test]
        public void PipeWriteRead()
        {
            USBDeviceInfo[] dies = USBDevice.GetDevices("{A5DCBF10-6530-11D2-901F-00C04FB951ED}");
            USBDeviceInfo di = dies.FirstOrDefault(n => n.Manufacturer == "STMicroelectronics");
            USBDevice dev = new USBDevice(di.DevicePath);

            byte[] send = new byte[] { 
                0xF1, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};

            byte[] result = new byte[512];
            byte[] expect = new byte[] { 0x23, 0x80, 0x83, 0x04, 0x48, 0x37 };

            dev.Pipes[0x02].Write(send);
            int readLen = dev.Pipes[0x81].Read(result);
            Assert.AreEqual(expect.Length, readLen);

            dev.Dispose();
        }


        [Test]
        /*ExpectedMessage = "A szemaforhoz rendelt határidő lejárt. (A kivétel HRESULT-értéke: 0x80070079)")]*/
        public void ReadTimeoutTest()
        {
            Assert.Catch<System.Runtime.InteropServices.COMException>(() =>
            {

                USBDeviceInfo[] dies = USBDevice.GetDevices("{A5DCBF10-6530-11D2-901F-00C04FB951ED}");
                USBDeviceInfo di = dies.FirstOrDefault(n => n.Manufacturer == "STMicroelectronics");
                USBDevice dev = new USBDevice(di.DevicePath);

                dev.Pipes[0x81].Policy.PipeTransferTimeout = 1000;
                byte[] result = new byte[512];
                dev.Pipes[0x81].Read(result);
                dev.Dispose();
            });
        }
    }
}
