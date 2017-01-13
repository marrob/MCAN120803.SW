// -----------------------------------------------------------------------
// <copyright file="UInt32MaskConverter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using System.ComponentModel;
    using System.Globalization;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UInt32MaskConverter : TypeConverter
    {

        /// <summary>
        /// 
        /// </summary>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            else
                return base.CanConvertTo(context, destinationType);
        }


        /// <summary>
        ///  Uint32 -> String
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value">UInt32:0</param>
        /// <param name="destinationType"></param>
        /// <returns>Type: String: "00000000"</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is UInt32)
            {
                return CustomDataConversion.UInt32ToStringHighSpeed((UInt32)value);
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
        ///  String -> Uint32  
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
