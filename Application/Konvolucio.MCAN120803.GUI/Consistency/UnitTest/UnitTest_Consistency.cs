// -----------------------------------------------------------------------
// <copyright file="UnitTest_Consistency.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.Consistency.UnitTest
{
    using System;
    using NUnit.Framework;
    using Services;

/*Culutre*/

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class UnitTest_Consistency
    {
        [Test]
        public void _0001_Symbol()
        {
            string msg;
            Assert.IsNull(ConsistencyCheck.Symbol("TEST_MESSAGE"));
            Assert.IsNull(ConsistencyCheck.Symbol("Test_Message"));
            Assert.IsNull(ConsistencyCheck.Symbol("TestMessage"));
            Assert.IsNull(ConsistencyCheck.Symbol("_TEST_MESSAGE"));
            Assert.IsNull(ConsistencyCheck.Symbol("B0123456789012345678901234567890"));
           
            CultureService.Instance.CurrentCultureName = "hu-HU";
            Assert.IsNotEmpty(msg = ConsistencyCheck.Symbol("B01234567890123456789012345678901"));
            Assert.AreEqual("Maximális szímbólum hossza: 32 karkter lehet.", msg); Console.WriteLine(msg);
            CultureService.Instance.CurrentCultureName = "en-US";
            Assert.IsNotEmpty(msg = ConsistencyCheck.Symbol("B01234567890123456789012345678901"));
            Assert.AreEqual("Symbol Name maximum lenght is: 32.", msg); Console.WriteLine(msg);


            Assert.IsNotEmpty(msg = ConsistencyCheck.Symbol(" TEST_MESSAGE"));
            Assert.IsNotEmpty(ConsistencyCheck.Symbol("0_TEST_MESSAGE"));
            Assert.IsNotEmpty(ConsistencyCheck.Symbol("?_TEST_MESSAGE"));
            Assert.IsNotEmpty(ConsistencyCheck.Symbol("UNICODE_Á_MESSAGE"));   
        }

        [Test]
        public void _0002_FileName()
        {
            Assert.IsNull(ConsistencyCheck.FileName("FileName"));
            Assert.IsNull(ConsistencyCheck.FileName("File Name"));
            Assert.IsNull(ConsistencyCheck.FileName("0123456789012345678901234567890"));
            Assert.IsNull(ConsistencyCheck.FileName("UNICODE_Á_MESSAGE"));

            string msg;
            CultureService.Instance.CurrentCultureName = "hu-HU";
            Assert.IsNotEmpty(msg = ConsistencyCheck.FileName("UNICODE_<_MESSAGE"));
            Assert.AreEqual("A fájlnév nem megnengdett karakter(eket) tartalmaz: '<'", msg); Console.WriteLine(msg);

            Assert.IsNotEmpty(ConsistencyCheck.FileName("UNICODE < MESSAGE")); 
        }
    }
}
