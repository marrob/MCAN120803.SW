using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Konvolucio.DeviceNet.SlaveDevices;

namespace Konvolucio.DeviceNet
{
    class Program
    {
        static void Main(string[] args)
        {
            IDeviceNetIO dnet = new DeviceNetIO();

            const byte sMacAddr = 0x00;
            const byte dMacAddr = 0x02;

            dnet.Connect("3869366E3133");

            dnet.Open(250000);
            System.Threading.Thread.Sleep(1000);

            dnet.AllocateMaster(sMacAddr, dMacAddr, 0x1);

            //--
            byte[] productName = dnet.GetAttribute(sMacAddr, dMacAddr, 0x01, 0x01, 0x07);
            Console.WriteLine(Tools.ByteArrayToString(productName));


            //---
            Console.WriteLine("Registered flags + Abnormal flags: " + Tools.ByteArrayLogString(dnet.GetAttribute(sMacAddr, dMacAddr, 0x04, 146, 0x03)));
            Console.WriteLine("Input data: " + Tools.ByteArrayLogString(dnet.GetAttribute(sMacAddr, dMacAddr, 0x04, 144, 0x03)));
            Console.WriteLine("Generic Status + Input data: " + Tools.ByteArrayLogString(dnet.GetAttribute(sMacAddr, dMacAddr, 0x04, 147, 0x03)));
            Console.WriteLine("Output data: " + Tools.ByteArrayLogString(dnet.GetAttribute(sMacAddr, dMacAddr, 0x04, 160, 0x03)));
            dnet.ReleaseMaster(sMacAddr, dMacAddr);

            dnet.Dispose();

            Konvolucio.DeviceNet.SlaveDevices.Manager devices = new SlaveDevices.Manager();

            if ((devices.GetDevice[120] as DeviceMRLY).DiscrateOuputPoint.Instances[2].Value.Value == true)
            {
                
            }

            DeviceNetMaster master = new DeviceNetMaster();



            (master.Devices[1] as DeviceMRLY).DiscrateOuputPoint.Instances[1].Value.Value = true;




            //(devices.GetDevice[120] as DeviceMRLY).DiscrateOuputPoint.Instances[2].Value.Value = false;
            //(devices.GetDevice[120] as DeviceMRLY).DiscrateOuputPoint.Instances[2].FaultAction.Value = false;   


 
            //(devices.GetDevice[120] as DeviceMRLY).Identity.


            //(devices.GetDeviceByAddress[0].Objects[1] as DiscrateOutputPointObject)    
            //devices.GetDeviceByAddress[0].Objects[0].
            //Console.WriteLine(devices.GetDeviceByAddress[10].Identity.VendorId);
            //Console.WriteLine(devices.GetOnLineDevices);

            //devices.GetDeviceByAddress[100].Objects["Discrate Output Point"].

            //devices.GetDeviceByAddress[10].Objects.SupportedObjects;

            //devices["MRLY"].GetDeviceByAddress[100].DigitalOutput[1] = 1;
            
                //    devices["MX"].GetDeviceByAddress[100].DigitalOutput
        }
    }
  
}
