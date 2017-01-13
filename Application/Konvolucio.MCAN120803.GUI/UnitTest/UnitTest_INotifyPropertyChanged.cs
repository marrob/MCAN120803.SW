using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using NUnit.Framework;

namespace Konvolucio.MCAN120803.GUI
{
    [TestFixture]
    class UnitTest_INotifyPropertyChanged
    {
        public class TestBaseClass
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }



        public class TestDerivedClass : TestBaseClass, INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public new string FirstName
            {
                get { return base.FirstName; }
                set
                {
                    if (base.FirstName != value)
                    {
                        base.FirstName = value;
                        OnPropertyChanged("FirstName");
                    }
                }
            }
            public new string LastName
            {
                get { return base.LastName; }
                set
                {
                    if (base.LastName != value)
                    {
                        base.LastName = value;
                        OnPropertyChanged("LastName");
                    }
                }
            }
            public void OnPropertyChanged(string name)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs(name));
            }
        }
        [Test]
        public void _0001_ChangeTest()
        {
            var instance = new TestDerivedClass();
            instance.PropertyChanged += (sender, e) =>
                 Console.WriteLine("Changed PropName:" + e.PropertyName); 
            instance.FirstName = "Homer";
            instance.LastName = "Simpson";
            instance.FirstName = "Bart";
            instance.LastName = "Simpson";
            /*Changed PropName:FirstName
             *Changed PropName:LastName
             *Changed PropName:FirstName*/
        }

        [Test]
        public void _0001_GetStaticPropertyName()
        {
            Assert.AreEqual("FirstName", Konvolucio.WinForms.Framework.PropertyPlus.GetPropertyName(() => MocPropertyNameClass.FirstName));
        }

        class MocPropertyNameClass
        {
            public static string FirstName { get; set; }
        }
    }
}
