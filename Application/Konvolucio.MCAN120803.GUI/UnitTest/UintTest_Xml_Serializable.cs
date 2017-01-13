// -----------------------------------------------------------------------
// <copyright file="UintTest_Cast.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;   
    using System.ComponentModel;
    using System.IO;
    using System.Xml.Serialization;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class UintTest_Xml_Serializable
    {
        public class MockPersonItem : ICloneable
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
        public class MockPersonCollection : BindingList<MockPersonItem> { }

        public class MockCarItem
        {
            public string Type { get; set; }
            public string Color { get; set; }

            public MockCarItem() { }
            public MockCarItem(string type, string color)
            {
                Type = type;
                Color = color;
            }
            public override string ToString()
            {
                return Type + " " + Color;
            }
        }

        public class MockCarCollection : BindingList<MockCarItem> { }

        private Type[] SupportedTypes
        {

            get
            {
                return new Type[]
                {
                    typeof(string),
                    typeof(MockStorage),
                    typeof(MockPersonCollection),
                    typeof(MockCarItem),
                    typeof(MockCarCollection)
                };
            }
        }


        public class MockStorage
        {
            public UniCollection UniCollection { get; set; }
        }


        public class UniCollection : BindingList<object> { }

        #region SaveLoad
        private void SaveToFile(string path, object instance)
        {
            var xmlFormat = new XmlSerializer(typeof(MockStorage), null, SupportedTypes, new XmlRootAttribute(AppConstants.XmlRootElement), AppConstants.XmlNamespace);
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(fStream, instance);
            }
        }

        private MockStorage LoadFromFile(string path)
        {
            var xmlFormat = new XmlSerializer(typeof(MockStorage), null, SupportedTypes, new XmlRootAttribute(AppConstants.XmlRootElement), AppConstants.XmlNamespace);
            MockStorage instance;

            using (Stream fStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                instance = (MockStorage)xmlFormat.Deserialize(fStream);

            return instance;
        }
        #endregion




        [Test]
        public void _0001()
        {
            MockStorage storage = new MockStorage();

            var persons = new MockPersonCollection()
            {

                new MockPersonItem() {FirstName = "Homer", LastName = "Simpson", Age = 20},
                new MockPersonItem() {FirstName = "Bart", LastName = "Simpson", Age = 20},
                new MockPersonItem() {FirstName = "Lisa", LastName = "Simposn", Age = 25}
            };


            storage.UniCollection = new UniCollection();

            var cars = new MockCarCollection()
            {
                new MockCarItem("Suzuki", "Orage"),
                new MockCarItem("BMW", "Black"),
                new MockCarItem("Audi", "Green")
            };


            foreach (var item in persons)
                storage.UniCollection.Add(item);

            foreach (var item in cars)
                storage.UniCollection.Add(item);


            var pth = @"D:\test.xml";

            SaveToFile(pth, storage);

            var loaded = LoadFromFile(pth);
        }
    }
}
