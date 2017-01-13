using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Konvolucio.MUDS150628.UnitTest
{
    [TestFixture]
    class UnitTest_Basic
    {

        /********************************************************************************/
        [Test]
        public void _0001_CP1540_TesterPresent()
        {
            UInt32 txId = 0x714;
            UInt32 rxId = 0x734;
            UInt32 baudRate = 500000;
            CanBusLink canLink =  new CanBusLink(txId,rxId,baudRate);
            canLink.Connect();
            canLink.BusTerminationEnable = true;
            canLink.Open();

            Iso15765NetwrorkLayer network = new Iso15765NetwrorkLayer(canLink);

            try
            {
                byte[] response;
                network.Transport(new byte[] { 0x3E, 0x00 }, out response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                network.SaveFrameLog("D:\\" + this.GetType().FullName + ".csv");
                canLink.Close();
                canLink.Disconnect();
            }
        }
        /********************************************************************************/
        [Test]
        public void _0002_Bxxx_TesterPresent()
        {
            UInt32 txId = 0x714;
            UInt32 rxId = 0x734;
            UInt32 baudRate = 500000;
            CanBusLink canLink = new CanBusLink(txId, rxId, baudRate);
            canLink.Connect();
            canLink.BusTerminationEnable = true;
            canLink.Open();

            Iso15765NetwrorkLayer network = new Iso15765NetwrorkLayer(canLink);

            try
            {
                byte[] response;

                Console.WriteLine("Tester Present:");
                network.Transport(new byte[] { 0x3E, 0x00 }, out response);
                Assert.AreEqual(new byte[] { 0x7E, 0x00}, response);

                Console.WriteLine("Diag Session:");
                network.Transport(new byte[] { 0x10, 0x03 }, out response);
                Assert.AreEqual(new byte[] { 0x50, 0x03, 0x00, 0x32, 0x01, 0xF4 }, response);

                Console.WriteLine("Read Dtc:");
                network.Transport(new byte[] { 0x19, 0x02, 0x08 }, out response);
                Console.WriteLine("Dtc:");
                Console.WriteLine(Tools.ByteArrayToCStyleString(response));
                Assert.AreEqual(new byte[] { 0x59, 0x02, 0xCA, 0x90, 0xB5, 0x15, //6
                                                         0x0A, 0x90, 0xB6, 0x15, //4
                                                         0x0A, 0x90, 0xB9, 0x12, //4
                                                         0x0A, 0x9A, 0x61, 0x15, //4
                                                         0x0A, 0x9A, 0x63, 0x15, //4
                                                         0x0A, 0x9A, 0x64, 0x15, 
                                                         0x0A, 0xC1, 0x40, 0x00, 
                                                         0x0A, 0xC1, 0x55, 0x00, //
                                                         0x0A }, response);      //Szum: 35

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                network.SaveFrameLog("D:\\" + this.GetType().FullName + ".csv");
                canLink.Close();
                canLink.Disconnect();
            }
        }
        /********************************************************************************/
        [Test]
        public void _0003_Tachograph_TesterPresent()
        {
            UInt32 txId = 0x38DAEEFB;
            UInt32 rxId = 0x38DAFBEE;
            UInt32 baudRate = 250000;
            
            CanBusLink canLink = new CanBusLink(txId,rxId,baudRate); 
            canLink.Connect();
            canLink.BusTerminationEnable = true;
            canLink.Open();

            Iso15765NetwrorkLayer network = new Iso15765NetwrorkLayer(canLink);

            try
            {
                byte[] response;

                Console.WriteLine("Tester Present:");
                network.Transport(new byte[] { 0x3E, 0x00 }, out response);
                Assert.AreEqual(new byte[] { 0x7E, 0x00 }, response);

                Console.WriteLine("DiagnosticSession:");
                network.Transport(new byte[] { 0x10, 0x7E }, out response);
                Assert.AreEqual(new byte[] { 0x50, 0x7E }, response);

                Console.WriteLine("CloseRemoteAuthentication:");
                network.Transport(new byte[] { 0x31, 0x01, 0x01, 0x80, 0x09 }, out response);
                Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x0A }, response);

                Console.WriteLine("RemoteCompanyCardReady:");
                network.Transport(new byte[] { 0x31, 0x01, 0x01, 0x80, 0x01, 0x3B, 0x3B, 0x9A,
                                             0x96, 0xC0, 0x10, 0x31, 0xFE, 0x5D, 0x00, 0x64, 
                                             0x05, 0x7B, 0x01, 0x02, 0x31, 0x80, 0x90, 0x00, 
                                             0x76 }, out response); //25:Length
                Assert.AreEqual(new byte[] { 0x71, 0x01, 0x01, 0x80, 0x02 }, response); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                network.SaveFrameLog("D:\\" + this.GetType().FullName + ".csv");
                canLink.Close(); 
                canLink.Disconnect();
            }
         
        }
 
    }
}
