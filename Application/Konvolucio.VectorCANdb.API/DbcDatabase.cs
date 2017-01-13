namespace Konvolucio.VectorCANdb.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public interface DbcObject<T>
    {
        T Parse(string input);
        string ToDbcFormat();
    }

    /// <summary>
    /// 
    /// </summary>
    public class DbcDatabase
    {
        public VersionObject Version;
        public TimingObject Timing;
        public NodeObject Nodes;
        public List<ValueTableObjectItem> ValueTable;
        public List<SignalValueDescriptionsItem> SignalsDescriptions;
        public List<MessageObjectItem> Messages;
        public CommentsObject Comment;
        public AttributeDefinitionObject AttributeDefinition;
        public AttributeValueObject AttributeValue;

        public DbcDatabase()
        {
            Version = new VersionObject();
            Timing = new TimingObject();
            Nodes = new NodeObject();
            ValueTable = new List<ValueTableObjectItem>();
            SignalsDescriptions = new List<SignalValueDescriptionsItem>();
            Messages = new List<MessageObjectItem>();
            Comment = new CommentsObject();
            AttributeDefinition = new AttributeDefinitionObject();
            AttributeValue = new AttributeValueObject();  
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ParseStringToInt32(string input)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double ParseStringToDouble(string input)
        {
            return double.Parse(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        /// 
        public static string GetKeyword(string line)
        {
            string[] lineWords = line.Trim(' ').Split(' ');
            if (lineWords != null && lineWords.Length != 0)
                return lineWords[0];
            else
                return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] ReadFileToStringArray(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File does not exits", path);

            String line = string.Empty;
            using (StreamReader sr = new StreamReader(path, Encoding.ASCII))
                line = sr.ReadToEnd();

            string[] stringSeparators = new string[] { "\r\n" };
            string[] lines = line.Split(stringSeparators, StringSplitOptions.None);
            return lines;
        }
    }
}
