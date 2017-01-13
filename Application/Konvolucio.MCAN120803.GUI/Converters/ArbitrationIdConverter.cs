namespace Konvolucio.MCAN120803.GUI.Converters
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text;

    using Services;
    using Converters;

    public class ArbitrationIdConverter : TypeConverter
    {
        public static string[] TemplateFormats
        {
            get
            {
                return new string[]
                {                           
                    "{0:X8}",     /*Sample: "A0B1C2D3"*/
                    "0x{0:X8}",   /*Sample: "0xA0B1C2D3"*/
                    "{0:X4} ",    /*Sample: "0xA0B1 C2D3"*/
                    "{0:X2} "     /*Sample: "0xA0 B1 C2 D3"*/
                };
            }
        }

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
        static string _format = AppConstants.DefaultArbitrationIdFormat;


        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            else
                return base.CanConvertTo(context, destinationType);
        }
               

        /// <summary>
        ///  ArbitrationId Uint32 -> String
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value">UInt32:0</param>
        /// <param name="destinationType"></param>
        /// <returns>Type: String: "00000000"</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is uint)
            {
                return CustomDataConversion.UInt32ToString((uint)value, _format);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        ///  ArbitrationId String -> Uint32  
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value">Type: String: "0000000000"</param>
        /// <returns>UInt32:0</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                return CustomDataConversion.StringToUInt32(value as string);
            }
            else
            {
                return base.ConvertFrom(context, culture, value);
            }
        }
    }
}
