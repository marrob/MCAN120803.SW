// -----------------------------------------------------------------------
// <copyright file="UnitTest_Clipboard.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using System.Windows.Forms;
    using Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class UnitTest_Clipboard
    {
        [Test]
        [STAThread]
        public void _0001_()
        {
            Clipboard.Clear();
            Clipboard.SetText("Test");
            Assert.IsTrue(Clipboard.ContainsText());
            Assert.AreEqual("Test",Clipboard.GetText());
        }

        [Test]
        [STAThread]
        public void _0002_()
        {
            Clipboard.Clear();
            MockPersonItem sourcePersonItem = new MockPersonItem() {FirstName = "Homer", LastName = "Simpson", Age = 40};
            Clipboard.SetData("PersonItem", sourcePersonItem.ToString());

            Assert.IsTrue(Clipboard.ContainsData("PersonItem"));          

            var data = Clipboard.GetData("PersonItem");

            Assert.AreEqual("Homer Simpson", data.ToString());
        }

        [Test]
        [STAThread]
        public void _0003_()
        {
            Clipboard.Clear();
            
            var data = new DataObject();
            var sourcePersonItem = new MockPersonItem() {FirstName = "Homer", LastName = "Simpson", Age = 40};

            data.SetData(typeof(MockPersonItem),sourcePersonItem);
            Clipboard.SetDataObject(data);
            Assert.IsTrue(typeof(MockPersonItem).IsSerializable);
          

            var retviredDataObj = Clipboard.GetDataObject();
            Assert.IsTrue(retviredDataObj != null);

            Assert.IsTrue(retviredDataObj.GetDataPresent(typeof(MockPersonItem)));
            var person = retviredDataObj.GetData(typeof(MockPersonItem)) as MockPersonItem;
            Assert.AreEqual("Homer", person.FirstName);

        }

        [Test]
        [STAThread]
        public void _0004_()
        {
            Clipboard.Clear();

            var data = new DataObject();

            List<MockPersonItem> personItems = new List<MockPersonItem>()
            {
                new MockPersonItem(){FirstName= "Homer", LastName = "Simpson", Age = 40},
                new MockPersonItem(){FirstName= "Bart", LastName = "Simpson", Age = 10},
            };

            data.SetData(typeof(List<MockPersonItem>), personItems);
            Clipboard.SetDataObject(data);
            Assert.IsTrue(typeof(List<MockPersonItem>).IsSerializable);


            var retviredDataObj = Clipboard.GetDataObject();
            Assert.IsTrue(retviredDataObj != null);

            Assert.IsTrue(retviredDataObj.GetDataPresent(typeof(List<MockPersonItem>)));
            var person = retviredDataObj.GetData(typeof(List<MockPersonItem>)) as List<MockPersonItem>;
            Assert.AreEqual("Homer", person[0].FirstName);

        }

        [Test]
        [STAThread]
        public void _0005_()
        {
            Clipboard.Clear();

            var data = new DataObject();

            var personItems = new MockPersonItem[]
            {
                new MockPersonItem(){FirstName= "Homer", LastName = "Simpson", Age = 40},
                new MockPersonItem(){FirstName= "Bart", LastName = "Simpson", Age = 10},
            };

            data.SetData(typeof(MockPersonItem[]), personItems);
            Clipboard.SetDataObject(data);
            Assert.IsTrue(typeof(MockPersonItem[]).IsSerializable);


            var retviredDataObj = Clipboard.GetDataObject();
            Assert.IsTrue(retviredDataObj != null);

            Assert.IsTrue(retviredDataObj.GetDataPresent(typeof(MockPersonItem[])));
            var person = retviredDataObj.GetData(typeof(MockPersonItem[])) as MockPersonItem[];
            Assert.AreEqual("Homer", person[0].FirstName);

        }

        [Test]
        public void _0006()
        {
            var items = new MockPersonCollection()
            {
                new MockPersonItem(){FirstName= "Homer", LastName = "Simpson", Age = 40},
                new MockPersonItem(){FirstName= "Bart", LastName = "Simpson", Age = 10},
                new MockPersonItem(){FirstName= "Lisa", LastName = "Simpson", Age = 15}, 
            };


            Assert.IsTrue(typeof(MockPersonItem).IsSerializable);

            var objects = new object[items.Count];

            for (var i = 0; i < items.Count; i++)
                objects[i] = ((ICloneable)items[i]).Clone();

            var type = items.GetType().GetProperty("Item").PropertyType;
            Assert.IsTrue(type.IsSerializable);

            var targetObj = objects;

            foreach (var item in (Array)targetObj)
            {
                var person = item as MockPersonItem;
                if (person != null)
                {
                    Console.WriteLine(person.FirstName);
                }
            }
        }

        [Test]
        [STAThread]
        public void _0007_()
        {
            Clipboard.Clear();

            var data = new DataObject();

            var sourcePersonItem = new MockPersonCollection()
            {
                new MockPersonItem(){FirstName= "Homer", LastName = "Simpson", Age = 40},
                new MockPersonItem(){FirstName= "Bart", LastName = "Simpson", Age = 10},
                new MockPersonItem(){FirstName= "Lisa", LastName = "Simpson", Age = 15},
            };

            data.SetData(typeof(MockPersonItem), sourcePersonItem[0]);
            Clipboard.SetDataObject(data);
            Assert.IsTrue(typeof(MockPersonItem).IsSerializable);


            var retviredDataObj = Clipboard.GetDataObject();
            Assert.IsTrue(retviredDataObj != null);

            Assert.IsTrue(retviredDataObj.GetDataPresent(typeof(MockPersonItem)));
            var person = retviredDataObj.GetData(typeof(MockPersonItem)) as MockPersonItem;
            Assert.AreEqual("Homer", person.FirstName);

        }

        [Test]
        [STAThread]
        public void _0008()
        {
            Clipboard.Clear();

            var data = new DataObject();
        }

        class MockPersonCollection : BindingList<MockPersonItem>
        {

        }


        [Serializable]
        class MockPersonItem : ICloneable
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public object Clone()
            {
                return new MockPersonItem() { FirstName = this.FirstName, LastName = this.LastName, Age = this.Age };
            }

            public override string ToString()
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
