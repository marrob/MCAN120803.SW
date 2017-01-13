
namespace Konvolucio.MCAN120803.API.UnitTest
{
    using NUnit.Framework;

    [TestFixture]
    class UnitTest_AdapterInUse
    {
        CanAdapterDevice _adapter;

        [Test]
        /*ExpectedMessage = "_adapter not found by Serial Number. Code:-8603."*/
        public void _0001_ConnectByInvalidSerialNumberException()
        {
            Assert.Catch<CanAdapterException>(() =>
            {
                _adapter = new CanAdapterDevice();
                _adapter.ConnectTo("abc");
            });
        }

        [Test]
        /*ExpectedMessage = "_adapter already in use."*/
        [Ignore("Ez nem tesztelhető")]
        public void Connect_Open_Dissconnect()
        {
            /*
            Exception exceptionResult = null; 

            MCanAdapter Can = new MCanAdapter();
            Action act1 = () =>
                {
                   
                    var adapter = MCanAdapter.GetAdapters()[0];
                    Can.ConnectTo(adapter);
                    Console.WriteLine("act1 siker.");
                };
            act1.BeginInvoke(null, null);

            Action act2 = () =>
                {
                    try
                    {

                        MCanAdapter Can1 = new MCanAdapter();
                        var adapter2 = MCanAdapter.GetAdapters()[0];
                        Can1.ConnectTo(adapter2);
                        Thread.Sleep(1000);
                    }
                    catch (Exception ex)
                    {
                        exceptionResult = ex;
                    }
                };

            act2.BeginInvoke(null, null);

            if (exceptionResult != null)
                throw exceptionResult;

            Thread.Sleep(1000);
         */
        }
    }
}
