using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Globalization;
using System.Resources;
using System.Threading;
namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    [TestFixture]
    class UnitTest_Culuture
    {
        ResourceManager TextResource;
        CultureInfo NewCulture;

        [Test]
        public void _0001_CultureChange()
        {
            string text = string.Empty;

            TextResource = new ResourceManager("Konvolucio.MCAN120803.GUI.TextResource", typeof(UnitTest_Culuture).Assembly);

            NewCulture = new CultureInfo("hu-HU");
            Thread.CurrentThread.CurrentCulture = NewCulture;
            Thread.CurrentThread.CurrentUICulture = NewCulture;
            text = TextResource.GetString("columnName");
            Console.WriteLine("hu-HU:" + text);

            NewCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = NewCulture;
            Thread.CurrentThread.CurrentUICulture = NewCulture;
            text = TextResource.GetString("columnName");
            Console.WriteLine("en-US:" + text);
        }
      //  [Test]
        [SetUICulture("fr-BE")]
        public void _0002_NUnit_CultureAttribute()
        {
            Assert.AreEqual("fr-BE", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        }
    }
}
