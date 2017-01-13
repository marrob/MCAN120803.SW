
namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    class UintTest_Event
    {
        [Test]
        public void _0001_Subscribe()
        {
            var mc = new MockClass();
            bool rised = false;

            mc.SampleEvent += (o, e) => { rised = true; };

            /*vagy zárójel nélkül*/

            mc.SampleEvent += (o, e) => rised = true;
    
            mc.RiseSampleEvent();
            Assert.True(rised);
        }

        class MockClass
        {
            public EventHandler SampleEvent;

            public void RiseSampleEvent()
            {
                if (SampleEvent != null)
                    SampleEvent(this, EventArgs.Empty);
            }
        }
    }
}
