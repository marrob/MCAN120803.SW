
namespace Konvolucio.MCAN120803.GUI.Converters
{
    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Globalization;

    using Services;
    using Converters;

    public class DataFrameConverter : TypeConverter
    {
        /// <summary>
        /// Bájtok megjelenítési formátumát paraméterezhet segítségével
        /// pl: "{0:X2} ", "0x{0:X2} ", stb...
        /// </summary>
        public static string Format 
        {
            get 
            {
                return _format;
            }
            set
            {
                _format = value;
            }
        }
        static string _format = AppConstants.DefaultDataFormat;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// value:byte[] -> destinationType:String (return)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value">byte[]{0x00, 0x0A, 0x0B }</param>
        /// <param name="destinationType"></param>
        /// <returns>00 0A 0B 0C</returns>                                                                  
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is byte[])
            {
                return CustomDataConversion.ByteArrayToString(value as byte[], _format);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Overrides the CanConvertFrom method of TypeConverter.
        /// The ITypeDescriptorContext interface provides the context for the
        /// conversion. Typically, this interface is used at design time to 
        /// provide information about the design-time container.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// value:String -> return byte[]
        /// Overrides the ConvertFrom method of TypeConverter.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value">String</param>
        /// <returns>byte[]</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            byte[] retval = new byte[0];

            if (value is string)
            {
                return (CustomDataConversion.StringToByteArray(value as string));
            }
            else
            {
                return base.ConvertFrom(context, culture, value);
            }
        }
    }
}
