

namespace Konvolucio.MCAN120803.API.UnitTest
{
    using System;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    class UnitTest_Message
    {

        [Test]
        public void _0001_StructQueueMsg()
        {
            CanMessage msg_1 = new CanMessage();
            msg_1.TwoByte = 0x0001;
            CanMessage msg_2 = new CanMessage();
            msg_2.TwoByte = 0x0002;

            SafeQueue<CanMessage> queue = new SafeQueue<CanMessage>();
            queue.Enqueue(msg_1);
            queue.Enqueue(msg_1);

            CanMessage msg_d1 = queue.Dequeue();
            Assert.AreEqual(msg_1.TwoByte, 0x0001);
            CanMessage msg_d2 = queue.Dequeue();
            Assert.AreEqual(msg_2.TwoByte, 0x0002);
        }
        /********************************************************************************/
        [Test]
        public void _0002_CanFramesToNiPacket()
        {
            byte[] buffer = new byte[64];
            int size;

            CanMessage msg_1 = new CanMessage();
            msg_1.TwoByte = 0x00A1;
            msg_1.TimestampTick = 0x000000A2;
            msg_1.ArbitrationId = 0x000000A3;
            msg_1.IsRemote = 0xA4;
            msg_1.DataLength = 0x08;
            msg_1.Data = new byte[] { 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, };

            CanMessage msg_2 = new CanMessage();
            msg_2.TwoByte = 0x00B1;
            msg_2.TimestampTick = 0x000000B2;
            msg_2.ArbitrationId = 0x000000B3;
            msg_2.IsRemote = 0xB4;
            msg_2.DataLength = 0x08;
            msg_2.Data = new byte[] { 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xB8, };

            CanMessage[] msglist = new CanMessage[] { msg_1, msg_2 };

            CanMessage.CanFramesToMsgPacket(msglist, buffer, out size);

            byte[] expect = new byte[]
            {
                0x02, 0x00, 0x01, 0x00,
                0xA1, 0x00, 0xA2, 0x00, 0x00, 0x00, 0xA3, 0x00, 0x00, 0x00, 0xA4, 0x08, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8,
                0xB1, 0x00, 0xB2, 0x00, 0x00, 0x00, 0xB3, 0x00, 0x00, 0x00, 0xB4, 0x08, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xB8,
            };

            byte[] result = new byte[size];
            Buffer.BlockCopy(buffer, 0, result, 0, size);
            Assert.IsTrue(result.SequenceEqual(expect));
        }
        /********************************************************************************/
        [Test]
        public void _0003_NiPacketToCanFrames()
        {
            byte[] expect = new byte[]
            {
                0x02, 0x00, 0x01, 0x00,
                0xA1, 0x00, 0xA2, 0x00, 0x00, 0x00, 0xA3, 0x00, 0x00, 0x00, 0xA4, 0x08, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8,
                0xB1, 0x00, 0xB2, 0x00, 0x00, 0x00, 0xB3, 0x00, 0x00, 0x00, 0xB4, 0x08, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xB8,
            };

            CanMessage msg = new CanMessage();
            CanMessage[] frames = null;
            CanMessage.MsgPacketToCanFrames(expect, expect.Length, out frames);
            msg.TwoByte = 0x00A1;
            msg.TimestampTick = 0x000000A2;
            msg.ArbitrationId = 0x000000A3;
            msg.IsRemote = 0xA4;
            msg.DataLength = 0x08;
            msg.Data = new byte[] { 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, };
            Assert.AreEqual(2, frames.Length);
            Assert.AreEqual(msg.TwoByte, frames[0].TwoByte);
            Assert.AreEqual(msg.TimestampTick, frames[0].TimestampTick);
            Assert.AreEqual(msg.ArbitrationId, frames[0].ArbitrationId);
            Assert.AreEqual(msg.IsRemote, frames[0].IsRemote);
            Assert.AreEqual(msg.DataLength, frames[0].DataLength);
            Assert.AreEqual(msg.Data.Length, frames[0].Data.Length);
            Assert.AreEqual(msg.Data[0], frames[0].Data[0]);
            Assert.AreEqual(msg.Data[7], frames[0].Data[7]);
        }
        /********************************************************************************/
        [Test]
        public void _0004_MessageToString()
        {
            Assert.AreEqual("0x00000000 0x000000A8 0x00 0x08 0xA1 0xA2 0xA3 0xA4 0xA5 0xA6 0xA7 0xA8", CanMessage.MessageStdA8.ToString());
            Assert.AreEqual("0x00000000 0x000000A4 0x00 0x04 0xA1 0xA2 0xA3 0xA4 0x00 0x00 0x00 0x00", CanMessage.MessageStdA4.ToString());
            Assert.AreEqual("0x00000000 0x000000A0 0x00 0x00 0x00 0x00 0x00 0x00 0x00 0x00 0x00 0x00", CanMessage.MessageStdA0.ToString());
            Assert.AreEqual("0x00000000 0x1FFFFFFF* 0x00 0x08 0xFF 0xFF 0xFF 0xFF 0xFF 0xFF 0xFF 0xFF", CanMessage.MessageMaxExtId.ToString());
            Assert.AreEqual("0x00000000 0x00000000* 0x00 0x00 0x00 0x00 0x00 0x00 0x00 0x00 0x00 0x00", CanMessage.EmptyMessageExtE0.ToString());
        }
        /********************************************************************************/
        [Test]
        public void _0005_MessageEqual()
        {
            Assert.IsTrue(CanMessage.MessageStdA0 == CanMessage.MessageStdA0);
            Assert.IsTrue(CanMessage.MessageStdA4 == CanMessage.MessageStdA4);
            Assert.IsTrue(CanMessage.MessageStdA8 == CanMessage.MessageStdA8);
            Assert.IsFalse(CanMessage.MessageStdA0 == CanMessage.MessageStdA8);
            Assert.IsFalse(CanMessage.EmptyMessageStdE0 == CanMessage.MessageStdA4);
        }
        /********************************************************************************/
        [Test]
        public void _0006_Timestamp_HowItsWork()
        {
            DateTime timestamp = DateTime.Now;
            DateTime.TryParse("2015.11.26 20:05:03", out timestamp);
            long apbsTick100ns = timestamp.Ticks;

            UInt32 msg_1_relTick_ms = 0;
            long msg_1_absTick_100ns = apbsTick100ns + msg_1_relTick_ms *10000;
            DateTime msg_1_dt = new DateTime(msg_1_absTick_100ns);

            UInt32 msg_2_relTick_ms = 1000;
            long msg_2_absTick_100ns = msg_1_absTick_100ns + (msg_2_relTick_ms * 10000);
            DateTime msg_2_dt = new DateTime(msg_2_absTick_100ns);


            UInt32 msg_3_relTick_ms = 1000;
            long msg_3_absTick_100ns = msg_2_absTick_100ns + (msg_3_relTick_ms * 10000);
            DateTime msg_3_dt = new DateTime(msg_3_absTick_100ns);

            DateTime start = new DateTime(0);
            DateTime sample = new DateTime(63584165103000 * 10000);

            DateTime labViewStartDT;
            DateTime.TryParse("1904.01.01 01:00:00", out labViewStartDT);
            long labViewStartTick = labViewStartDT.Ticks;

        }
        /********************************************************************************/
        [Test]
        public void _0006_Timeout()
        {
            int timeoutMs = 1000;
            
            long startTick = DateTime.Now.Ticks;
            bool isFound = false;
            do
            {
                if (DateTime.Now.Ticks - startTick > timeoutMs * 10000)
                {
                    //Timeout Occur
                    Assert.Pass();
                }
                //Do something...
            } while (!isFound);
            Assert.Fail();
        }
        /********************************************************************************/
        [Test]
        public void _0007_Baudrates()
        {
            CanBaudRateCollection.GetBaudRates();
        }
    }
}
