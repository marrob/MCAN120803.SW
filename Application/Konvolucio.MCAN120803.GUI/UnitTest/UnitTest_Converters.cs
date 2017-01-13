
namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;
    using NUnit.Framework;

    using Converters;

    [TestFixture]
    class UnitTest_Converters
    {
        [Test]
        public void _1001_ArbitrationId_TypeConverter_CanConvertToString()
        {
            var aic = new ArbitrationIdConverter();
            Assert.IsTrue(aic.CanConvertTo(typeof(string)));
        }

        [Test]
        public void _1002_ArbitrationId_TypeConverter_ToString()
        {
            var aic = new ArbitrationIdConverter();
            uint id = 0x800;
            object result = aic.ConvertTo(id, typeof(string));
            Assert.AreEqual("00000800", result.ToString());
        }

        [Test]
        public void _1003_ArbitrationId_TypeConverter_FromString()
        {
            var aic = new ArbitrationIdConverter();
            object result = aic.ConvertFrom("F0000000");
            Assert.AreEqual(0xF0000000, (uint)result);
        }
   
        [Test]
        public void _X000_UInt32ToString()
        {
            Assert.AreEqual("00000800", CustomDataConversion.UInt32ToString(0x00000800, "{0:X8}"));
            Assert.AreEqual("0x00000000", CustomDataConversion.UInt32ToString(0, "0x{0:X8}"));
            Assert.AreEqual("00000000", CustomDataConversion.UInt32ToString(0, "{0:X2}"));
            Assert.AreEqual("0000 0000", CustomDataConversion.UInt32ToString(0, "{0:X4} "));
            Assert.AreEqual("00 00 00 00", CustomDataConversion.UInt32ToString(0, "{0:X2} "));
            Assert.AreEqual("00000001", CustomDataConversion.UInt32ToString(1, "{0:X4}"));
            Assert.AreEqual("12345678", CustomDataConversion.UInt32ToString(0x12345678, "{0:X4}"));
        }

        [Test]
        public void _X000_StringToUInt32()
        {
            Assert.AreEqual(0, CustomDataConversion.StringToUInt32(" "));
            Assert.AreEqual(0x0A, CustomDataConversion.StringToUInt32(" 0A"));
            Assert.AreEqual(0x12345678, CustomDataConversion.StringToUInt32("12 34 56 78"));
            Assert.AreEqual(0x00000800, CustomDataConversion.StringToUInt32(" 00000800"));
            Assert.AreEqual(0, CustomDataConversion.StringToUInt32("0000 0000"));
            Assert.AreEqual(0, CustomDataConversion.StringToUInt32("00 00 00 00"));

        }


        [Test]
        public void _2020_StringToByteArray()
        {
            Assert.AreEqual(new byte[] { }, CustomDataConversion.StringToByteArray("   "));
            Assert.AreEqual(new byte[] { }, CustomDataConversion.StringToByteArray(" "));
            Assert.AreEqual(new[] { 0x00 }, CustomDataConversion.StringToByteArray(" 00"));
            Assert.AreEqual(new[] { 0x0A }, CustomDataConversion.StringToByteArray(" A"));
            Assert.AreEqual(new[] { 0x00 }, CustomDataConversion.StringToByteArray("0"));

            Assert.AreEqual(new[] { 0x01, 0x00 }, CustomDataConversion.StringToByteArray("01 0"));
            Assert.AreEqual(new[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 }, CustomDataConversion.StringToByteArray("01 02 03 04 05 06 07 08"));

            Assert.AreEqual(new[] { 0x01, 0x02, 0x00 }, CustomDataConversion.StringToByteArray("01020"));
            Assert.AreEqual(new[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 }, CustomDataConversion.StringToByteArray("0102030405060708"));

            Assert.AreEqual(new[] { 0x00 }, CustomDataConversion.StringToByteArray("0x"));
            Assert.AreEqual(new[] { 0x00 }, CustomDataConversion.StringToByteArray("0x00"));
            Assert.AreEqual(new[] { 0x00, 0x10 }, CustomDataConversion.StringToByteArray("0x00 0x10"));
            Assert.AreEqual(new[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 }, CustomDataConversion.StringToByteArray("0x01 0x02 0x03 0x04 0x05 0x06 0x07 0x08"));

            Assert.AreEqual(new[] { 0x00, 0x10 }, CustomDataConversion.StringToByteArray("0x00, 0x10"));
            Assert.AreEqual(new[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 }, CustomDataConversion.StringToByteArray("0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08"));
        }


        [Test]
        public void _3002_ByteArrayToStringHighSpeed()
        {
            Assert.AreEqual("", CustomDataConversion.ByteArrayToStringHighSpeed(null));
            Assert.AreEqual("01 02 03 04 05 06 07 08", CustomDataConversion.ByteArrayToStringHighSpeed(new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 }));
        }

        [Test]
        public void _3004_StringToByteArrayHighSpeed()
        {
            Assert.AreEqual(new byte[0], CustomDataConversion.StringToByteArrayHighSpeed(""));
            Assert.AreEqual(new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 }, CustomDataConversion.StringToByteArrayHighSpeed("01 02 03 04 05 06 07 08"));
        }
        
        [Test]
        public void _4001_StringToUInt32HighSpeed()
        {
            Assert.AreEqual(0, CustomDataConversion.StringToUInt32HighSpeed(""));
            Assert.AreEqual(0, CustomDataConversion.StringToUInt32HighSpeed(" "));
            Assert.AreEqual(0, CustomDataConversion.StringToUInt32HighSpeed("00000000"));
            Assert.AreEqual(0, CustomDataConversion.StringToUInt32HighSpeed("00000000 "));
            Assert.AreEqual(0, CustomDataConversion.StringToUInt32HighSpeed(" 00000000 "));
            Assert.AreEqual(0, CustomDataConversion.StringToUInt32HighSpeed(" 00000000 "));
            Assert.AreEqual(0x00000000, CustomDataConversion.StringToUInt32HighSpeed(" 00000000 "));
            Assert.AreEqual(0xFFFFFFFF, CustomDataConversion.StringToUInt32HighSpeed("FFFFFFFF"));
        }

        [Test]
        public void _4002_SUInt32ToStringHighSpeed()
        {
            Assert.AreEqual("00000000", CustomDataConversion.UInt32ToStringHighSpeed(0));
            Assert.AreEqual("12345678", CustomDataConversion.UInt32ToStringHighSpeed(0x12345678));
            Assert.AreEqual("00000801", CustomDataConversion.UInt32ToStringHighSpeed(0x00000801));
        }

      

    }
}
