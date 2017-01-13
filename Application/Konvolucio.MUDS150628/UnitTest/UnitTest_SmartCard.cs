using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpringCardPCSC;

namespace Konvolucio.MUDS150628.UnitTest
{
    [TestFixture]
    class UnitTest_SmartCard
    {

        SCardChannel SmartCard;

        /********************************************************************************/
        [TestFixtureSetUp]
        public void Setup()
        {
            SmartCard = new SCardChannel("OMNIKEY CardMan 3x21 0");
            Assert.IsTrue(SmartCard.CardPresent, "Nincs kártya bedugva.");
            SmartCard.ShareMode = SCARD.SHARE_SHARED;
            SmartCard.Protocol = (uint)(SCARD.PROTOCOL_T0);
            SmartCard.Connect();

        }
        /********************************************************************************/
        [TestFixtureTearDown]
        public void ClenUp()
        {
            if (SmartCard != null)
            {
                SmartCard.Disconnect(1);
                SmartCard = null;
            }
        }
        /********************************************************************************/
        [Test]
        public void _0001_Connect_To_DriverCard()
        {

            //00 A4 02 0C 02 00 02
            //SELECT FILE
            byte[] command = new byte[] { 0x00, 0xA4, 0x02, 0x0C, 0x02, 0x00, 02 };
            CAPDU capdu = new CAPDU(command);
            RAPDU rapdu = SmartCard.Transmit(capdu);
            byte[] answer = rapdu.GetBytes();
            Assert.AreEqual(new byte[] { 0x90, 0x00 }, answer, "Válasz nem az amit vártam...");
          
        }
        /********************************************************************************/
        [Test]
        public void _0002_Connect_To_DriverCard()
        {
            //00 A4 02 0C 02 00 02
            //SELECT FILE
            byte[] command = new byte[] { 0x00, 0xA4, 0x02, 0x0C, 0x02, 0x00, 02 };
            CAPDU capdu = new CAPDU(command);
            RAPDU rapdu = SmartCard.Transmit(capdu);
            byte[] answer = rapdu.GetBytes();
            Assert.AreEqual(new byte[] { 0x90, 0x00 }, answer, "Válasz nem az amit vártam...");

        }
        /********************************************************************************/
        [Test]
        public void _0003_EF()
        {

            //Connect to 'OMNIKEY CardMan 3x21 0', share=2, protocol=1
            //Transmit << 00A4040C06FF544143484F
            //Transmit >> 9000
            //Transmit << 00A4020C020520
            //Transmit >> 9000
            //Disconnect, disposition=1
            //AID: ¡FF 54 41 43 48 4F¡ for the Tachograph application
            //MF->DF
            //    ->EF
            //    ->EF    
            byte[] command = new byte[] { 0x00, 0xA4, 0x04, 0x0C, 0x06, 0xFF, 0x54, 0x41, 0x43, 0x48, 0x4F };
            CAPDU capdu = new CAPDU(command);
            RAPDU rapdu = SmartCard.Transmit(capdu);
            byte[] answer = rapdu.GetBytes();
            Assert.AreEqual(new byte[] { 0x90, 0x00 }, answer, "Válasz nem az amit vártam...");

            //Select EF Identification
            command = new byte[] { 0x00, 0xA4, 0x02, 0x0C, 0x02, 0x05, 0x20 };
            capdu = new CAPDU(command);
            rapdu = SmartCard.Transmit(capdu);
            answer = rapdu.GetBytes();
            Assert.AreEqual(new byte[] { 0x90, 0x00 }, answer, "Válasz nem az amit vártam...");
        }
        /********************************************************************************/
        [Test]
        public void _0004_Read_EF_Identification()
        {


            RAPDU rapdu = null;

            // SELECT FILE tachograf application
            byte[] cmd1 = new byte[] { 0x00, 0xA4, 0x04, 0x0C, 0x06, 0xFF, 0x54, 0x41, 0x43, 0x48, 0x4F };
            rapdu = SmartCard.Transmit(new CAPDU(cmd1));
            Assert.AreEqual(new byte[] { 0x90, 0x00 }, rapdu.GetBytes(), "Válasz nem az amit vártam...");

            // SELECT FILE which file is EF Identification [0520] under DF Tachograf [0500] section
            byte[] cmd2 = new byte[] { 0x00, 0xA4, 0x02, 0x0C, 0x02, 0x05, 0x20 };
            rapdu = SmartCard.Transmit(new CAPDU(cmd2));

            // READ BINARY CardIdentification
            byte[] cmd3 = new byte[] { 0x00, 0xB0, 0x00, 0x00, 0x41 };
            rapdu = SmartCard.Transmit(new CAPDU(cmd3));
        }

                
        /********************************************************************************/
        [Test]
        public void _0005_GetChallange()
        {
            RAPDU rapdu = null;
            byte[] command;


            command = new byte[] { 0x00, 0x84, 0x00, 0x00, 0x08, 0x00, 0x57, 0x03, 0x8C, 0xE9, 0x9A, 0xFC, 0x38, 
                                   0x00, 0x00, 0x17, 0x36, 0x12, 0x10, 0x06, 0xA2, 0x00, 0x00, 0x00, 0xF0, 0xE7,
                                   0xBE, 0x3E, 0xFD, 0x7F, 0x00, 0x00, 0x70, 0x06, 0x88, 0xC6, 0x74, 0x7F, 0x00, 0x00, 0xEF };
            rapdu = SmartCard.Transmit(new CAPDU(command));

            // SELECT FILE tachograf application
            byte[] cmd1 = new byte[] { 0x00, 0xA4, 0x04, 0x0C, 0x06, 0xFF, 0x54, 0x41, 0x43, 0x48, 0x4F };
            rapdu = SmartCard.Transmit(new CAPDU(cmd1));
            Assert.AreEqual(new byte[] { 0x90, 0x00 }, rapdu.GetBytes(), "Válasz nem az amit vártam...");

        }
    }
}
