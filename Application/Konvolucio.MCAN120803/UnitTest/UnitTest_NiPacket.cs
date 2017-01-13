

namespace Konvolucio.MCAN120803.API.UnitTest
{
    using System;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    class UnitTest_NiPacket
    {

        [Test]
        public void _0001_HasEndPacket_True()
        {
            byte[] Response_GetRxErrCnt_0x41 =
            { 
                0x01, 0x05, 0x00, 0x00, 
                0x00, 0x00, 0x02, 0x00, 0x08, 0x00, 0x00, 0x08, 0x80, 0x01, 0x00, 0x11, 0x00, 0x00, 0x00, 0x41, 
                0x00, 0x00, 0x02, 0x00, 0xFF, 0xFE, 0x00, 0x00,
            };
            long lastEight =  BitConverter.ToInt64(Response_GetRxErrCnt_0x41, Response_GetRxErrCnt_0x41.Length - 8);
            Assert.IsTrue(0x0000feff00020000 == lastEight);
        }

        [Test]
        public void _0002_ArrayToType()
        {
            byte[] data = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            long lastEight = BitConverter.ToInt64(data, data.Length - 8);
            Assert.IsTrue(0x0908070605040302 == lastEight);
        }
 
        [Test]
        public void _0003_HasEndPacket_False()
        {
            byte[] Response_GetRxErrCnt_0x41 =
            { 
                0x01, 0x05, 0x00, 0x00, 
                0x00, 0x00, 0x02, 0x00, 0x08, 0x00, 0x00, 0x08, 0x80, 0x01, 0x00, 0x11, 0x00, 0x00, 0x00, 0x41, 
                0x00, 0x00, 0x02, 0x00, 0xFF, 0xFE, 0x00, 0x00,
            };
            long lastEight = BitConverter.ToInt64(Response_GetRxErrCnt_0x41, Response_GetRxErrCnt_0x41.Length - 1 - 8);
            Assert.IsFalse(0x0000feff00020000 == lastEight);
        }

        [Test]
        public void _0005_MakeAttributeRequest()
        {
            byte[] expect = new byte[]
            { 
                0x01, 0x05, 0x00, 0x00, 
                0x10, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x04, 0x80, 0x01, 0x00, 0x11,
                0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00
            };

            byte[] request = new byte[512];
            int length = CanAttributes.MakeAttriubtRequest(request, 0x05, 0x02, 0x80010011, 0, 4);

            for (int i = 0; i < expect.Length; i++)
                Assert.AreEqual(expect[i], request[i]);
        }

        [Test]
        public void _0006_ParseAttributeReadResponse()
        { 
            byte[]	Response_GetRxErrCnt_0x41 =
            { 
                0x01, 0x05, 0x00, 0x09, 
                0x00, 0x04, 0x02, 0x00, 0x08, 0x00, 0x00, 0x08, 0x80, 0x01, 0x00, 0x11, 0x00, 0x00, 0x00, 0x41, 
                0x00, 0x00, 0x02, 0x00, 0xFF, 0xFE, 0x00, 0x00,
            };
            UInt16 status;
            byte[] data;
            byte frameCnt;
            CanAttributes.ParseAttributeResponse(Response_GetRxErrCnt_0x41,
                                                  out frameCnt,
                                                  out data,
                                                  out status);
            Assert.AreEqual(0x05, frameCnt);
            Assert.AreEqual(0x0004, status);
            //Assert.AreEqual(0x41, value);
            
        }

        [Test]
        public void _0007_MakeActionRequest()
        {
            byte[] AtmUnitTest_Request_ActionStart = 
            {
                 0x01, 0x21, 0x00, 0x00,
                 0x10, 0x00, 0x17, 0x00, 0x00, 0x00, 0x00, 0x00, 
                 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00
            };

            byte[] buffer = new byte[64];

            int bytes = CanServices.MakeServiceRequest(buffer, 0x21, 0x17);

            byte[] result = new byte[bytes];
            Buffer.BlockCopy(buffer, 0, result, 0, bytes);
            Assert.IsTrue(AtmUnitTest_Request_ActionStart.SequenceEqual(result));
        }

        [Test]
        public void _0008_ParseActionResponse()
        {
            byte[] AtmUnitTest_Response_ActionStart = 
            {
                 0x01, 0x21, 0x00, 0x00,
                 0x00, 0x00, 0x17, 0x00, 0x08, 0x00, 0x00, 0x00, 
                 0x00, 0x00, 0x02, 0x00, 0xFF, 0xFE, 0x00, 0x00,
            };

            UInt16 status;
            byte frameCnt;
            CanServices.ParseActionResponse(AtmUnitTest_Response_ActionStart, out frameCnt, out status);

            Assert.AreEqual(0x21, frameCnt);
            Assert.AreEqual(0x0000, status);
        }
    }
}
