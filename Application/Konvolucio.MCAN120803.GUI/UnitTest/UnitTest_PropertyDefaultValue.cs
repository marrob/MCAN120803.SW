
namespace Konvolucio.MCAN120803.GUI
{
    using System.ComponentModel;
    using NUnit.Framework;

    [TestFixture]
    class UnitTest_PropertyDefaultValue
    {
        [Test]
        public void _0001_SetDefaultValue()
        {
            DefaultValuesTest dvt = new DefaultValuesTest();
            Assert.AreEqual(false, dvt.DefaultValueBool);
            dvt.SetDefault();
            Assert.AreEqual(true, dvt.DefaultValueBool);
        }
    }

    public class DefaultValuesTest
    {
        public void SetDefault()
        {
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this))
            {
                DefaultValueAttribute myAttribute = (DefaultValueAttribute)property.Attributes[typeof(DefaultValueAttribute)];

                if (myAttribute != null)
                {
                    property.SetValue(this, myAttribute.Value);
                }
            }
        }

        [DefaultValue(true)]
        public bool DefaultValueBool { get; set; }

        [DefaultValue("Good")]
        public string DefaultValueString { get; set; }

        [DefaultValue(27)]
        public int DefaultValueInt { get; set; }

    }    
}
