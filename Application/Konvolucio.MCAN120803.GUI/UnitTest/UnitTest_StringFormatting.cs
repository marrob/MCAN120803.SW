
namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using NUnit.Framework;
    using System.Text.RegularExpressions;

    [TestFixture]
    class UnitTest_StringFormatting
    {
        [Test]
        public void _0001_PadingLeftRight()
        {
            //->             hello<-
            //->hello             <-
            Console.WriteLine("->{0,18}<-", "hello");
            Console.WriteLine("->{0,-18}<-", "hello");
        }

        public int ParserStringToInt32(string input)
        {
            int retval = 0;
            if (input.Contains("0x"))
            {
                input = input.Replace("0x", "");
                retval = Int32.Parse(input, System.Globalization.NumberStyles.HexNumber);
            }
            else
            {
                retval = Int32.Parse(input);
            }
            return retval;
        }
        [Test]
        public void _0001_ParserStringToInt32()
        {
            Assert.AreEqual(10, ParserStringToInt32("0x0A"));
            Assert.AreEqual(10, ParserStringToInt32("10"));
        }



        [Test]
        public void _0002_Version()
        { 
            string input = "VERSION \"12\"";
            string pattern1 = Regex.Escape("\"") + "(.*?)\"";
            string pattern2 = "\"(.*?)\"";    //A mentő karatker a (\), ha strign lezárót használod (") a stringben, akkor azt mentned kell (\");
            string pattern3 = @"\""(.*?)\"""; //Ha kukaccal jelölöd a stringet, akkor megváltozik a a mentő karakter működése, duplázza a mentést! (elérési utvanalok problémája...)
   
           Assert.AreEqual("12", Regex.Match(input, pattern1).Groups[1].Value);
           Assert.AreEqual("12", Regex.Match(input, pattern2).Value.Trim('"'));
           Assert.AreEqual("12", Regex.Match(input, pattern3).Value.Trim('"'));    

        }
        [Test]
        public void _0003_Nodes()
        {
            string pattern;
            //             1.    2.     3.
            pattern = @"^(\d+)(\.\d+)?([RKM])?$";
            //Jelentése 
            // @"  Kezdődjön ezzel a kifjeezés a szövgben jelzett macskakörmöt duplázni kell
            // ^ Sor eleje
            // \d+ legalább egy szám vagy több
            // (\.\d+)? nullaszor vagy egyszer előforduló tizedes pont ÉS egy vagy több szám
            // \d? nullaszor vagy egyszer előforduló szám
            // ([RKM])? nullaszor vagy egyszer előforduló  nagy K vagy R vagy M (egyszerre csak egy)
            // $ sor vége
            // " kifejezés vége
            Assert.AreEqual("12.0M", Regex.Match("12.0M", pattern).Value);   

            pattern = @"\d+\s\w+";
            // @"  Kezdődjön ezzel a kifjeezés a szövgben jelzett macskakörmöt duplázni kell
			// \d+ legalább egy szám vagy több;
			// \s  szóköz
			// \w+ egy vagy több betű
            // "   kifejezés vége
            Assert.AreEqual("1234 sees", Regex.Match("my dad 1234 sees a kayak at noon", pattern).Value);

            pattern = @"\d+\s""\w+""";
            // @"  Kezdődjön ezzel a kifjeezés a szövgben jelzett macskakörmöt duplázni kell
            // \d+ legalább egy szám vagy több;
            // \s  szóköz
            // ""  egy macskaköröm
            // \w+ egy vagy több karakter
            // ""  egy macsaköröm 
            // "  kifejezés vége
            Assert.AreEqual("1235 \"Fuck\"", Regex.Match("my dad 1234 sees a kayak at noon 1235 \"Fuck\" Hello world", pattern).Value);


            pattern = @"\d+\s""[A-Za-z,;\s]+""";
            // @"  Kezdődjön ezzel a kifjeezés a szövgben jelzett macskakörmöt duplázni kell
            // \d+ legalább egy szám vagy több;
            // \s  szóköz
            // ""  egy macskaköröm
            //[A-Za-z,;\s]+ egy vagy több A-tó-Zig vagy a-tól-z-ig, vagy , vagy; vagy sapce
            // ""  egy macsaköröm 
            // "  kifejezés vége

        }

        [Test]
        public void _0001_Escape()
        {
            string input = "Abcdefg \"Hello World\" 12345";
            string temp = input.Substring(input.IndexOf('"') + 1); //Hello World" 12345
            string result = temp.Substring(0, temp.IndexOf('"'));
            Assert.AreEqual("Hello World", result);
        }

        [Test]
        public void _0002_Escape()
        {
            string input = "SG_ COUNT_LINE_FAULT_ERRORS_MAX M : 55|16@0+ (1,0) [0|65535] \"ms\"  PC";
            string result = input.Substring(input.IndexOf('[') + 1, input.IndexOf(']') - input.IndexOf('[') - 1);
            Assert.AreEqual("0|65535", result);
        }

        [Test]
        public void _0001_Trim()
        {
            string result = string.Empty;
            string input = "@; \" Hello World \";";

            result = input.Trim('@').Trim(';').Trim(' ').Trim('"').Trim(' ');
            Assert.AreEqual("Hello World", result);


            result = input.Trim(new char[] { '@', ';', ' ', '"', ' ' });
            Assert.AreEqual("Hello World", result);

        }


        /// <summary>
        /// Format a string with fixed spaces
        /// </summary>
        [Test]
        public void _0001_Format()
        {
            /*                         |->27
            "           String Goes Here"
            "     String Goes Here      "
            "String Goes Here           "
            */
            string s = "String goes here";
            string line1 = String.Format("{0,27}", s);
            Console.WriteLine(line1);
            string line2 = String.Format("{0,-27}", String.Format("{0," + ((27 + s.Length) / 2).ToString() + "}", s));
            Console.WriteLine(line2);
            string line3 = String.Format("{0,-27}", s);
            Console.WriteLine(line3);
        }
    }
}
