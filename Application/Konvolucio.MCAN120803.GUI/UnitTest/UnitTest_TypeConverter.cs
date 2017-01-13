namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using NUnit.Framework;
    using System.ComponentModel;
    using System.Globalization;

    [TestFixture]
    class UnitTest_Sample_TypeConverter
    {
        public struct MockPoint
        {
            public int X;
            public int Y;
            public MockPoint(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
        public class MockPointConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                {
                    return true;
                }
                return base.CanConvertFrom(context, sourceType);
            }
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value is string)
                {
                    string[] v = ((string)value).Split(new char[] { ',' });
                    return new MockPoint(int.Parse(v[0]), int.Parse(v[1]));
                }
                return base.ConvertFrom(context, culture, value);
            }
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    return ((MockPoint)value).X + "," + ((MockPoint)value).Y;
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
        [Test]
        public void _0001_()
        {
            Assert.AreEqual(new MockPoint(1, 5), (MockPoint)new MockPointConverter().ConvertFrom("1, 5"));
            Assert.AreEqual("2,5", new MockPointConverter().ConvertTo(new MockPoint(2, 5), typeof(string)) as string);
        }
    }

    [TestFixture]
    class UnitTest_Context_TypeConverter
    {
        public struct MockPoint
        {
            public int X;
            public int Y;
            public MockPoint(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public class MockPointConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                {
                    return true;
                }
                return base.CanConvertFrom(context, sourceType);
            }
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value is string)
                {
                    string[] v = ((string)value).Split(new char[] { ',' });
                    return new MockPoint(int.Parse(v[0]), int.Parse(v[1]));
                }
                return base.ConvertFrom(context, culture, value);
            }
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    return ((MockPoint)value).X + "," + ((MockPoint)value).Y;
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }

        public class MockContext : ITypeDescriptorContext
        {
            public MockContext(object instance, string propertyName)
            {
                Instance = instance;
                PropertyDescriptor = TypeDescriptor.GetProperties(instance)[propertyName];
            }

            public object Instance { get; private set; }
            public PropertyDescriptor PropertyDescriptor { get; private set; }
            public IContainer Container { get; private set; }

            public void OnComponentChanged()
            {
            }

            public bool OnComponentChanging()
            {
                return true;
            }

            public object GetService(Type serviceType)
            {
                return null;
            }
        }


        class MockObject
        {

            public string Name { get; set; }
            public MockPoint PointOnTable { get; set; }

            public MockObject()
            {

            }
        }


        [Test]
        public void _0001_()
        {
            Assert.AreEqual(new MockPoint(1, 5), (MockPoint)new MockPointConverter().ConvertFrom("1, 5"));
            Assert.AreEqual("2,5", new MockPointConverter().ConvertTo(new MockPoint(2, 5), typeof(string)) as string);
            Assert.AreEqual(new MockPoint(1, 5), (MockPoint)new MockPointConverter().ConvertFrom("1, 5"));
            Assert.AreEqual(new MockPoint(1, 5), (MockPoint)new MockPointConverter().ConvertFrom(new MockContext(new MockObject(), "PointOnTable"), null, "1,5"));

        }

    }
}

