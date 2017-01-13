namespace Konvolucio.MCAN120803.GUI.AppModules.CanTx.UnitTest
{
    using NUnit.Framework;

    [TestFixture]
    class UintTestMessageName
    {

        [Test]
        public void _0001_CreateNewMessageName_Empty()
        {
            string[] names = new string[0];
            var result = WinForms.Framework.CollectionTools.GetNewName(names, "New_Message");
            Assert.AreEqual("New_Message_1", result);
        }

        [Test]
        public void _0002_CreateNewMessageName_Sequental()
        {
            string[] names = new string[]
            {
                "New_Message_1",
                "New_Message_2",
                "New_Message_3",
                "New_Message_4"
            };
            var result = WinForms.Framework.CollectionTools.GetNewName(names, "New_Message");
            Assert.AreEqual("New_Message_5", result);
        }

        [Test]
        public void _0003_CreateNewMessageName_Sequental()
        {
            string[] names = new string[]
            {
                "New_Message_1",
                "Hello_World",
                "New_Message_3",
                "Homer_Simpson_Message 4"
            };
            var result = WinForms.Framework.CollectionTools.GetNewName(names, "New_Message");
            Assert.AreEqual("New_Message_4", result);
        }
    }
}
