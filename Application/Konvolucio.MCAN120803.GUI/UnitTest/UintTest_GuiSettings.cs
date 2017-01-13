
namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using System.Text;
    using NUnit.Framework;
    using Konvolucio.MCAN120803.GuiSettings;

    [TestFixture]
    class UintTest_GuiSettings
    {
        [Test]
        public void _0001_FileCreate()
        {
            AppGuiSettings.Instance.Load("D\\Demo.mcanx");
        }

        [Test]
        public void _0002_Default()
        {
            AppGuiSettings.Instance.Load("D\\Demo.mcanx");
            //Assert.AreEqual(0.23, AppGuiSettings.Instance.Log.SplitContainerLog_SplitterDistance);
        }
    }
}
