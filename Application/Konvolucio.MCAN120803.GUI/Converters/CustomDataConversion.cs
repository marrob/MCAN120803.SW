// -----------------------------------------------------------------------
// <copyright file="ByteArrayToStringConverter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    using System.Globalization; /**/
    using Services; /*CultureText*/

    /// <summary>
    /// Ez az osztály azért született, hogy a bájtos tömbök és értékek 
    /// az XML fájolokban is olvasható formában jelenjenek meg egyésgesen.
    /// Ez egy belső formátum ami a külvilággal nincs kapcsolatban, csak a saját cuccal.
    /// </summary>
    public class CustomDataConversion
    {
        /// <summary>
        /// Gyors belső számábrázolást használó konverzió.
        /// Byte[] -> String
        /// </summary>
        /// <param name="input">byte[]{0x00, 0x0A, 0x0B}</param>
        /// <returns>"00 0A 0B 0C"</returns>
        public static string ByteArrayToStringHighSpeed(byte[] input)
        {
            string retval = string.Empty;
            if (input != null && input.Length != 0)
            {

                for (int i = 0; i < input.Length; i++)
                    retval += string.Format("{0:X2} ", input[i]);
            }
            retval = retval.Trim(' ');
            return retval;
        }

        /// <summary>
        /// Gyors belső számábrázolást használó konverzió.
        /// String -> Byte[]
        /// </summary>
        /// <param name="input">"00 0A 0B 0C"</param>
        /// <returns>byte[]{0x00, 0x0A, 0x0B}</returns>
        public static byte[] StringToByteArrayHighSpeed(string input)
        {
            byte[] retval = null;

            if (!string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input))
            {
                input = input.Trim(' ');

                string[] strArray = input.Split(' ');

                retval = new byte[strArray.Length];

                for (int i = 0; i < strArray.Length; i++)
                    retval[i] = byte.Parse(strArray[i], System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            else
            {
                retval = new byte[0];
            }
            return retval;
        }

        /// <summary>
        /// String -> return byte[]
        /// </summary>
        /// <param name="input">"00 0A 0B 0C"</param>
        /// <returns>byte[]{0x00, 0x0A, 0x0B}</returns>
        public static byte[] StringToByteArray(string input)
        {
            byte[] retval = new byte[0];

            if (!string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input))
            {
                input = input.Trim();
                try
                {
                    if (input.Length == 0)
                        return retval;

                    if (input == "0x")
                        return new byte[] { 0x00 };

                    if (input.Length == 1)
                    {
                        retval = new byte[1];
                        retval[0] = byte.Parse(input, System.Globalization.NumberStyles.AllowHexSpecifier);
                    }
                    else
                    {
                        string temp = string.Empty;
                        if (!input.Contains(' ') && !input.Contains(','))
                        {
                            for (int i = 0; i < input.Length; i++)
                                if (i % 2 == 0)
                                    temp += " " + input[i];
                                else
                                    temp += input[i];
                            input = temp.Trim(' ');
                        }
                        string[] strArray = input.Split(new string[] { "0x", " ", "," }, StringSplitOptions.RemoveEmptyEntries);
                        retval = new byte[strArray.Length];
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            retval[i] = byte.Parse(strArray[i], System.Globalization.NumberStyles.AllowHexSpecifier);
                        }
                    }
                    return retval;
                }
                catch
                {
                    throw new ArgumentException(CultureService.Instance.GetString(CultureText.text_InvalidDataFormat));
                }
            }
            else
            {
                return retval;
            }
        }

        /// <summary>
        /// Megjelítésre.
        /// byte[] -> String
        /// </summary>
        /// <param name="input">Type:byte[]{0x00, 0x0A, 0x0B}</param>
        /// <param name="format">Format: "{0:X2} "</param>
        /// <returns>String: 00, 0A 0B</returns>
        public static string ByteArrayToString(byte[] input, string format)
        {
            string retval = string.Empty;
            if (input.Length != 0)
            {
                for (int i = 0; i < input.Length; i++)
                    retval += string.Format(format, input[i]);
            }
            retval = retval.Trim(' ', ',');
            return retval;
        }

        /// <summary>
        /// Megjelítésre.
        /// </summary>
        /// <param name="value">UInt32:0x12345678</param>
        /// <param name="format">Format: "0x{0:X8}"</param>
        /// <returns>String:0x12345678</returns>
        public static string UInt32ToString(UInt32 value, string format)
        {
            string result = string.Empty;

            if (format.ToUpper().Contains(":X2"))
            {
                byte[] ba = BitConverter.GetBytes(value);
                for (int i = 0; i < ba.Length; i++)
                    result += string.Format(format, ba.Reverse().ToArray()[i]);

            }
            else if (format.ToUpper().Contains(":X4"))
            {
                byte[] ba = BitConverter.GetBytes(value);
                result = string.Format(format, BitConverter.ToUInt16(ba, 2));
                result += string.Format(format, BitConverter.ToUInt16(ba, 0));
            }
            else if (format.ToUpper().Contains(":X8"))
            {
                result = string.Format(format, value);
            }
            result = result.Trim(' ', ',');
            return result;
        }
        
        /// <summary>
        /// Gyors belső számábrázolást használó konverzió.
        /// Uint32 -> String
        /// </summary>
        /// <param name="value">Uint32</param>
        /// <returns>00000000</returns>
        public static string UInt32ToStringHighSpeed(UInt32 value)
        {
            return string.Format("{0:X8}", value);
        }

        /// <summary>
        /// Megjelenítésre!
        /// String -> Uint32
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static UInt32 StringToUInt32(string input)
        {
            UInt32 retval = 0;
            try
            {
                if (!string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input))
                {
                    input = input.Trim(' ');
                    input = input.Replace("0x", "");
                    input = input.Replace(",", "");
                    input = input.Replace(" ", "");
                    retval = UInt32.Parse(input, System.Globalization.NumberStyles.HexNumber);
                }
                else
                {
                    retval = 0;
                }
            }
            catch
            {
                throw new ArgumentException(CultureService.Instance.GetString(CultureText.text_InvalidArbitationIdFormat));
            }
            return retval;
        }

        /// <summary>
        /// Gyors belső számábrázolást használó konverzió.
        /// String -> Uint32
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static UInt32 StringToUInt32HighSpeed(string input)
        {
            UInt32 retval = 0;
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input))
            {
                input = input.Trim(' ');
                retval = UInt32.Parse(input, System.Globalization.NumberStyles.HexNumber);
            }
            else
            { 
                retval = 0;                
            }
            return retval;
        }
    }
}
