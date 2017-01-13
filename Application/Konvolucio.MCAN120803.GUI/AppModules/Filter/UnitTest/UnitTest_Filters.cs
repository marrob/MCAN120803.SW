// -----------------------------------------------------------------------
// <copyright file="UnitTest_Filters.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Filter.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Common;
    using Model;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class UnitTest_Filters
    {
        [Test]
        public void _0001_Mode_ArbId()
        {

            MessageFilterCollection filters = new MessageFilterCollection();
            filters.GuiAdd(new MessageFilterItem("Test", true, MaskOrArbId.ArbId, 0x02, ArbitrationIdType.Standard, false, MessageDirection.Transmitted, MessageFilterMode.InsertToLog));
            Assert.IsTrue(filters.DoAddToLog(2, ArbitrationIdType.Standard, false, MessageDirection.Transmitted));
            Assert.AreEqual(1, filters[0].AcceptanceCount);
            Assert.IsFalse(filters.DoAddToLog(3, ArbitrationIdType.Standard, false, MessageDirection.Transmitted));
            Assert.AreEqual(1, filters[0].AcceptanceCount);
        }

        [Test]
        public void _0002_Mode_Mask()
        {

            MessageFilterCollection filters = new MessageFilterCollection();
            filters.GuiAdd(new MessageFilterItem("Test", true, MaskOrArbId.Mask, 0x03, ArbitrationIdType.Standard, false, MessageDirection.Transmitted, MessageFilterMode.InsertToLog));
            Assert.IsTrue(filters.DoAddToLog(2, ArbitrationIdType.Standard, false, MessageDirection.Transmitted));
            Assert.AreEqual(1, filters[0].AcceptanceCount);
            Assert.IsFalse(filters.DoAddToLog(4, ArbitrationIdType.Standard, false, MessageDirection.Transmitted));
            Assert.AreEqual(1, filters[0].AcceptanceCount);
        }
    }
}
