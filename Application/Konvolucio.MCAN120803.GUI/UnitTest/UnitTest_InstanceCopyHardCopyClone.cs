// -----------------------------------------------------------------------
// <copyright file="UnitTest_InstanceCopy.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using System.Drawing; /*Color*/
    using System.IO;      /*MemoryStream*/        
    using System.Runtime.Serialization.Formatters.Binary; /*Binary Formatter*/

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UnitTest_InstanceCopyHardCopyClone
    {
        [Serializable] /*Binary Formatter*/
        public class MockItem
        {
            public string Hasi { get { return this.GetHashCode().ToString(); } }
            public string ItemName {get; private set;}
            public MockItem(string itemName) { ItemName = itemName; }
        }

        [Serializable] /*Binary Formatter*/
        class MockClass1:ICloneable
        {
            public string Hasi { get { return this.GetHashCode().ToString(); } }
            public string CarType { get; private set; }  /*Referncia típus*/
            public Color CarColor { get; private set; }  /*Érték típus (enum..)*/
            public EventHandler TestHandler;             /*Refencia típus (Delegate "lista")*/
            public List<MockItem> TestItems;             /*Referncia típus*/

            public MockClass1()  { TestItems = new List<MockItem>(); } /*Konstruktor*/

            public MockClass1 Default()
            { 
                CarType = "BMW"; CarColor = Color.Black;
                TestHandler = (o, e) => { Console.WriteLine(" "); };
                TestItems = new List<MockItem>();
                TestItems.Add(new MockItem("First"));
                return this;
            }
            /// <summary>
            /// _0001_ManualCopyTo()
            /// </summary>
            public void CopyTo(MockClass1 target)
            {
                target.CarType = CarType;
                target.CarColor = CarColor;
                target.TestItems.Clear();
                foreach (var item in this.TestItems)
                    target.TestItems.Add(new MockItem(item.ItemName));
                target.TestHandler = TestHandler;
            }

            /// <summary>
            ///  _0002_MemberwiseClone()
            /// </summary>
            public object Clone() { return this.MemberwiseClone(); }

            /// <summary>
            /// _0003_ShallowCopy()
            /// A modszerr lényege, hogy az objektumot binárisan sorosítja (bájtosítja)
            /// Majd egy új területre azt a bájtsorozotaot ráírja és ezt a területet kasztolva 
            /// használhajtuk. Lényegében a referncia típusok is másolódnak, de a belső refencia típusok
            /// új refenciával létrejönnek. 
            /// </summary>
            public static T DeepCopy<T>(T item)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();
                formatter.Serialize(stream, item);
                stream.Seek(0, SeekOrigin.Begin);
                T result = (T)formatter.Deserialize(stream);
                stream.Close();
                return result;
            }
        }

        /// <summary>
        /// Ehhez a módszer hez kell hogy legyen forrás és cél példány.
        /// A célpélny refrenecia típusait nem írom felül.
        /// A refencia típusok nem másolódnak át, csak az értékeket adom át a célnak.
        /// </summary>
        [Test]
        public void _0001_ManualCopyTo()
        {
            MockClass1 source = new MockClass1().Default();
            MockClass1 target = new MockClass1(); 
            source.CopyTo(target);
            Assert.AreNotEqual(source.Hasi, target.Hasi);
            Assert.AreEqual(source.TestItems[0].ItemName, target.TestItems[0].ItemName);

            /* Nem azonos a két lista mivel source és target két különböző példány,
             * nem rontom el a referenciát másoláskor a target példányba másolom az új értékeket.*/
            Assert.AreNotEqual(source.TestItems.GetHashCode(), target.TestItems.GetHashCode()); 

            /*Nem azons a két objektum mivel a copy-ban én hoztam létre új elemet*/
            Assert.AreNotEqual(source.TestItems[0].Hasi, target.TestItems[0].Hasi);     
        }

        /// <summary>
        /// Ehhez módszerhez nem kell cél példeány a Colne hoz létre egy példányt amibe a
        /// refernciákat másolja.
        /// _NAGYON ÜGYELNI KELL ARRA, HOGY HA TARTALMAZ REFERENCIA PÉLDÁNYT a FORRÁST, AKKOR a 
        /// REFERENCIÁT MÁSOLJA ÁT ÉS NEM AZ ÉRTÉKEIT!!!! PL A LISTÁNÁL érezni legjobban a problémát!
        /// Az esemény kezelőt ilyen szinten másolni az katasztrófa lehet.
        /// </summary>
        [Test]
        public void _0002_MemberwiseClone()
        {
            MockClass1 source = new MockClass1().Default();
            MockClass1 target = null;
            target = source.Clone() as MockClass1;
            Assert.AreEqual(source.CarType, target.CarType);
            Assert.AreEqual(source.CarColor, target.CarColor);
            Assert.AreNotEqual(source.Hasi, target.Hasi);
            /*NEM ÚJ PÉLDÁNY!!! A kettő ugyan az!!!*/
            Assert.AreEqual(source.TestItems.GetHashCode(), target.TestItems.GetHashCode()); 
            /*Mivel targert refencia ugyan az innentől kezdve minden ugyan az... */
            Assert.AreEqual(source.TestItems[0].ItemName, target.TestItems[0].ItemName);
            Assert.AreEqual(source.TestItems[0].Hasi, target.TestItems[0].Hasi);
        }

        [Test]
        public void _0003_ShallowCopy()
        {
            MockClass1 source = new MockClass1().Default();
            MockClass1 target = MockClass1.DeepCopy(source);
            Assert.AreNotEqual(source.Hasi, target.Hasi);
            Assert.AreEqual(source.CarType, target.CarType);
            Assert.AreEqual(source.CarColor, target.CarColor);
            Assert.AreEqual(source.TestHandler, target.TestHandler);
            /*A MemberwiseClone-tól eltérően, ÚJ PÉLDÁNY!!! A kettő nemugyan az!!!*/
            Assert.AreNotEqual(source.TestItems.GetHashCode(), target.TestItems.GetHashCode()); 
            Assert.AreEqual(source.TestItems[0].ItemName, target.TestItems[0].ItemName);
            /*Ezek már nem ugyan azok!*/
            Assert.AreNotEqual(source.TestItems[0].Hasi, target.TestItems[0].Hasi);
        }
    }
}
