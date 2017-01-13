
namespace Konvolucio.MCAN120803.GUI.AppModules.Baudrate.UnitTest
{
    using System;
    using System.Windows.Forms;
    using Baudrate;
    using Model;
    using NUnit.Framework;

    [TestFixture]
    class UintTest_BaudRate
    {

        [Test]
        public void _0001_FormTest_Default_B003F807()
        {
            IBaurateEditorForm form = new BaurateEditorForm();
            form.Default();

            if (form.ShowDialog() == DialogResult.OK)
            {
                Assert.AreEqual("B003F807", form.CustomBaudRateValue, "Ez nem a Defualt érétk.");
            }
        }

        [Test]
        public void _0001_FormTest_SetCustomBaudRate_B003F808()
        {
            IBaurateEditorForm form = new BaurateEditorForm();
            form.CustomBaudRateValue = "B003F808";
            if (form.ShowDialog() == DialogResult.OK)
            {
                Assert.AreEqual("B003F808", form.CustomBaudRateValue, "Nem egyezzik a beállított és visszakpaott érték.");
            }
        }

        [Test]
        public void _0002_250KBaud()
        {

            var calc = new BaudrateCalculator();
            calc.Calculate(8, 16, 4, 3);

            foreach (var line in calc.GetCalculateDetails)
                Console.WriteLine(line);

            Assert.AreEqual((double)249999.99999999997d, (double)calc.Baudrate);
        }

        [Test]
        public void _0003_500KBaud()
        {
            var calc = new BaudrateCalculator();
            calc.Calculate(4, 16, 4, 3);

            foreach (var line in calc.GetCalculateDetails)
                Console.WriteLine(line);

            Assert.AreEqual(499999.99999999994d, calc.Baudrate);

        }

        /// <summary>
        /// Én úgy számoltam, hogy vettem hogy 4 legyen az osztó, tehát BRP = 3 + 1 (adatlap),
        /// aztán megnéztem hogy ha 4 usec-et (250kbit) akarok baudnak, akkor azt hány egységből lehet kihozni,
        /// és kijött, hogy 11,0592 tehát a követkző paramétereket állítottam be:
        /// 1 sync
        /// 4 tprs  
        /// 3 phs2
        /// 3 phs1
        /// Mondjuk az lenne a legjobb ha 276 480 Baud-ot is be lehetne nálad konfigurálni… 
        /// Van erre esély szerinted? Mert akkor nem kellene változtatnom és hosszútávon visszamenlőeg 
        /// is maradnának kompatibilisem a dolgok.
        /// Mennyi kellene a CAN busz tűrés egyébként ? Az látszik hogy 250Kbuad helyett 276 már nem működik.
        /// </summary>
        [Test]
        public void _0004_Vamosnak()
        {
            var calc = new BaudrateCalculator();
            calc.Calculate(15, 6, 3, 1);
            Assert.AreEqual(280000, calc.Baudrate);
            Assert.AreEqual(3520.0d, calc.GetBaudrateAbsoluteError(276480));
            Assert.AreEqual(1.2731481481481481d, calc.GetBaudrateRealtiveError(276480));
        }

        [Test]
        public void _0005_250KBaud_PCLK42MHz()
        {
            var calc = new BaudrateCalculator();
            calc.Calculate(42000000, 8, 16, 4, 3);

            foreach (var line in calc.GetCalculateDetails)
                Console.WriteLine(line);
            Assert.AreEqual(249999.99999999997d, calc.Baudrate);
            Assert.AreEqual("B003F807", calc.GetCustomBaudRateValue());
 
        }
        
        [Test]
        public void _0006_250KBaud_PCLK36MHz()
        {
            var calc = new BaudrateCalculator();
            calc.Calculate(36000000, 9, 11, 4, 1);


            foreach (var line in calc.GetCalculateDetails)
                Console.WriteLine(line);

            Assert.AreEqual(250000, calc.Baudrate);
        }

        [Test]
        public void _0007_Default_SegmentsToCustomBaudRateValue()
        {
            var calc = new BaudrateCalculator();
            calc.Calculate(1, 1, 1, 1);
            var target = calc.GetCustomBaudRateValue();
            Assert.AreEqual("B0000000", target);
        }

        [Test]
        public void _0008_250kBaud_SegmentsToCustomBaudRateValue()
        {
            var calc = new BaudrateCalculator();
            calc.Calculate(8, 16, 4, 3);
            var target = calc.GetCustomBaudRateValue();
            Assert.AreEqual("B003F807", target);
        }

        [Test]
        public void _0009_Max_SegmentsToCustomBaudRateValue()
        {
            var calc = new BaudrateCalculator();
            calc.Calculate(1024, 16, 8, 4);
            var target = calc.GetCustomBaudRateValue();
            Assert.AreEqual("B007FFFF", target);
        }

        [Test]
        public void _0010_Default_CustomBaudRateValueToSegments()
        {
            var calc = new BaudrateCalculator();
            calc.Calculate("B0000000");
            Assert.AreEqual(1, calc.Segments.Brp);
            Assert.AreEqual(1, calc.Segments.Tseg1);
            Assert.AreEqual(1, calc.Segments.Tseg2);
            Assert.AreEqual(1, calc.Segments.Sjw);
            Assert.AreEqual(42000000.0d, calc.Segments.SystemClock);
        }

        [Test]
        public void _0011_250kBaud_CustomBaudRateValueToSegments()
        {
            var calc = new BaudrateCalculator();
            calc.Calculate("B003F807");
            Assert.AreEqual(8, calc.Segments.Brp);
            Assert.AreEqual(16, calc.Segments.Tseg1);
            Assert.AreEqual(4, calc.Segments.Tseg2);
            Assert.AreEqual(3, calc.Segments.Sjw);
            Assert.AreEqual(42000000.0d, calc.Segments.SystemClock);
        }
    }
}
