// -----------------------------------------------------------------------
// <copyright file="UnitTest_CustomIFormatProvider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using NUnit.Framework;
    using System.Globalization;

    public class UnitTest_CustomIFormatProvider
    {
        public class ArbitrationIdFormat : IFormatProvider, ICustomFormatter
        {
            public object GetFormat(Type formatType)
            {
                if (formatType == typeof(ICustomFormatter))
                    return this;
                else
                    return null;
            }
            public string Format(string fmt, object arg, IFormatProvider formatProvider)
            {
                if (arg.GetType() != typeof(UInt32))
                {
                    try
                    {
                        return HandleOtherFormats(fmt, arg);
                    }
                    catch (FormatException e)
                    {
                        throw new FormatException(String.Format("The format of '{0}' is invalid.", fmt), e);
                    }
                }
                return Converters.CustomDataConversion.UInt32ToString((UInt32)arg, "0x{0:X8}");
            }
            private string HandleOtherFormats(string format, object arg)
            {
                if (arg is IFormattable)
                    return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
                else if (arg != null)
                    return arg.ToString();
                else
                    return String.Empty;
            }
        }
        [Test]
        public void _0001_()
        {
            Assert.AreEqual("0x0000000A", string.Format(new ArbitrationIdFormat(), "{0:CAN_STD_ID}", (UInt32)10));
        }
    }
}
