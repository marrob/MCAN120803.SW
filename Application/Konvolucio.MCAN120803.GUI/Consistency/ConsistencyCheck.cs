// -----------------------------------------------------------------------
// <copyright file="Consitency.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Konvolucio.MCAN120803.GUI.Services; /*Culutre*/

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ConsistencyCheck
    {
        /// <summary>
        /// Valid Symbol: "TEST_MESSAGE","_TEST_MESSAGE", "TestMessage"
        /// Invalid Symbol: "0_TEST_MESSAGE", "?_TEST_MESSAGE","UNICODE_Á_MESSAGE"
        /// </summary>
        /// <param name="symbol">Symbol Name</param>
        /// <returns>Valid if null</returns>
        public static string Symbol(string symbol)
        {
            const int maxLength = 32;
            /*Hossz*/
            if (symbol.Length > maxLength)
            {
                /*textConsistencyCheck_SymbolNameMaxLength*/
                /*"Symbol Name maximum lenght is: {0}."*/
                 return (string.Format(CultureService.Instance.GetString(CultureText.text_SymbolNameMaxLength), maxLength));
            }

            /*Első karakter csak betű vagy _ lehet*/
            if (symbol.Length != 0)
            {
                char c = symbol[0];
                if (!(char.IsLetter(c) || c == '_'))
                {
                    /*textConsistencyCheck_SymbolFirscCharacter*/
                    /*"A Symbol Name must begin with an alapha character or a '_' and may only contain alpha, numeric and '_' characters."*/
                    /*"A szimbólum első karaktere csak betüvel vagy '_' karakterrel kezdődhet a többi karatker lehet vagy betű vagy '_', lásd: ANSI C azanósítók."*/
                    return (string.Format(CultureService.Instance.GetString(CultureText.text_SymbolFirscCharacter)));
                }
            }

            /*Csak karakter vagy '_' taralmazhat.*/
            foreach(char c in symbol)
            {
                if (!(char.IsLetter(c) || char.IsDigit(c) || c == '_'))
                {
                    /*textConsistencyCheck_SymbolInvalidCharacter*/
                    /*"Ivalid character in Symbol Name:'{0}'"*/
                    /*"A szimbólum nem megengedett karaktert tartalmaz:'{0}'"*/
                    return (string.Format(CultureService.Instance.GetString(CultureText.text_SymbolInvalidCharacter), c));    
                }
            }

            /*Unicode*/
            /*ASCII defines only character codes in the range 0-127. Unicode is explicitly 
             * defined such as to overlap in that same range with ASCII. Thus, if you look 
             * at the character codes in your string, and it contains anything that is
             * higher than 127, the string contains Unicode characters that are not
             * ASCII characters.*/
            foreach (char c in symbol)
            {
                if (c > 127)
                {
                    /*textConsistencyCheck_SymbolInvalidCharacter*/
                    /*"Ivalid character in Symbol Name:'{0}'"*/
                    /*"A szimbólum nem megengedett karaktert tartalmaz:'{0}'"*/
                    return (string.Format(CultureService.Instance.GetString(CultureText.text_SymbolInvalidCharacter), c));    
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string FileName(string fileName)
        {
            char[] invalidFileChars = System.IO.Path.GetInvalidFileNameChars();
            var foundInvChars = invalidFileChars.Intersect(fileName.ToArray<char>()).ToArray();

            if (foundInvChars.Length != 0)
            {
                string inv = string.Join(",", foundInvChars);
                /*textConsistencyCheck_FileNameInvalidCharacter*/
                /*"Invalid character in file name: {0}"*/
                /*"A fájlnév nem megnengdett karakter(eket) tartalmaz: {0}"*/
                return (string.Format(CultureService.Instance.GetString(CultureText.text_FileNameInvalidCharacter), inv));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customBaudrateValue"></param>
        /// <returns></returns>
        public static string CustomBaudRate(string customBaudrateValue)
        {
            uint baudrate = 0;
            if (UInt32.TryParse(customBaudrateValue.Remove(customBaudrateValue.IndexOf("Custom")).Trim(),
                                System.Globalization.NumberStyles.HexNumber,
                                System.Globalization.CultureInfo.InvariantCulture,
                                out baudrate)) 
            {
                return ("Invalid Custom Buad Rate Value" );
            }
            return null;
        }
    }
}
