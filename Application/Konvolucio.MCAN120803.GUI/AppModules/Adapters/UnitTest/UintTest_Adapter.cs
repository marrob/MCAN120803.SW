
namespace Konvolucio.MCAN120803.GUI.AppModules.Adapters.UnitTest
{
    using System;
    using API;
    using NUnit.Framework;

    [TestFixture]
    class UintTest_Adapter
    {
        CanAdapterDevice _adapterWriter = new CanAdapterDevice();
        uint _targetBaudrate = 0;

        [TestFixtureSetUp]
        public void Setup()
        {
            _adapterWriter = new CanAdapterDevice();
            _adapterWriter.ConnectTo(CanAdapterDevice.GetAdapters()[0]);
            _targetBaudrate = 500000;

            _adapterWriter.Services.Reset();

            _adapterWriter.Open(_targetBaudrate);
        }

        [TestFixtureTearDown]
        public void Clean()
        {
            _adapterWriter.Close();
            _adapterWriter.Disconnect();

        }
        [Test]
        public void _0001_MaxWriteSpeed_500kBaud()
        {
            CanMessage cm = new CanMessage(0x000001, new byte[]{0x01, 0x02});
            long timestamp = DateTime.Now.Ticks;
            do
            {
                _adapterWriter.Write(new CanMessage[]{cm});

            } while ((DateTime.Now.Ticks - timestamp) < 10000 * 10000);
        }

    }
}
