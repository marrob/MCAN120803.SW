// -----------------------------------------------------------------------
// <copyright file="AttributeDefinitionObject.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.VectorCANdb.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 12 User Defined Attribute Definitions
    /// Keyword: BA_DEF_
    /// </summary>
    public class AttributeDefinitionObject : DbcObject<AttributeDefinitionObject>
    {

        //Attributum táblázatok:
        //BA_DEF_   Attributum név és Attributum típus összerendelése    
        //BA_DEF_DEF_ Attributum név és alapértelmezett érték összerendelése 
        //BA_ Attributum név és aktuális érték összerendelése  ebből 5db létezik BA_ BU_ BO_ SG_ EV_

        public interface IAttributeDefinition
        {
            string Name { get; }
            string ToDbcFormat();
        }

        public class AttributeEnumDefinition : IAttributeDefinition
        {
            public string Name { get { return "ENUM"; } }
            public List<string> Items;

            /// <summary>
            /// 
            /// </summary>
            public AttributeEnumDefinition()
            {
                Items = new List<string>();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ToDbcFormat()
            {
                string retval = string.Empty;
                retval = Name + "  "; /*Double! Space*/

                foreach (string item in Items)
                    retval += "\"" + item + "\",";
                retval = retval.Trim(',');
                return retval;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                string retval = string.Empty;
                retval = Name + " ";

                foreach (string item in Items)
                    retval += "\"" + item + "\",";
                retval = retval.Trim(',');
                return retval;
            }
        }

        public class AttributeIntegerDefinition : IAttributeDefinition
        {
            public string Name { get { return "INT"; } }
            public int Minimum;
            public int Maximum;

            public AttributeIntegerDefinition(int minimum, int maximum)
            {
                Minimum = minimum;
                Maximum = maximum;
            }

            public override string ToString()
            {
                return this.ToDbcFormat();
            }

            public string ToDbcFormat()
            {
                return Name + " " + Minimum.ToString() + " " + Maximum.ToString();
            }
        }

        public class AttributeStringDefinition : IAttributeDefinition
        {
            public string Name { get { return "STRING"; } }

            public override string ToString()
            {
                return this.ToDbcFormat();
            }

            public string ToDbcFormat()
            {
                return Name;
            }
        }

        public class AttributeDefinitionObjectItem : DbcObject<AttributeDefinitionObjectItem>
        {
            public string Name;
            public IAttributeDefinition AttributeType;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="input">
            /// "\"NodeLayerModules\" STRING ;"
            /// "\"GenSigStartValue\" INT -2147483648 2147483647;"
            /// "\"ModeTransmission\" ENUM  \"P\",\"E\",\"P+E\";"
            /// </param>
            /// <returns></returns>
            public AttributeDefinitionObjectItem Parse(string input)
            {

                string temp = input.Trim(';', ' ');
                string[] array = temp.Split('"');
                Name = array[1];

                if (temp.Contains("STRING"))
                {
                    AttributeType = new AttributeStringDefinition();
                }
                else if (temp.Contains("INT"))
                {
                    temp = array[2].Trim();
                    array = temp.Split(' ');
                    AttributeType = new AttributeIntegerDefinition(DbcDatabase.ParseStringToInt32(array[1]), DbcDatabase.ParseStringToInt32(array[2]));
                }
                else if (temp.Contains("ENUM"))
                {
                    AttributeType = new AttributeEnumDefinition();

                    for (int i = 3;
                         i < array.Length;
                         i += 2)
                    {
                        (AttributeType as AttributeEnumDefinition).Items.Add(array[i]);
                    }
                }
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return this.ToDbcFormat();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ToDbcFormat()
            {
                return "BA_DEF_ BU_  " + "\"" + Name + "\"" + " " + AttributeType.ToDbcFormat() + ";";
            }
        }

        public List<AttributeDefinitionObjectItem> Netowrks;
        public List<AttributeDefinitionObjectItem> Nodes;
        public List<AttributeDefinitionObjectItem> Messages;
        public List<AttributeDefinitionObjectItem> Signals;
        public List<AttributeDefinitionObjectItem> EnvironomentVariables;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public AttributeDefinitionObject()
        {
            Netowrks = new List<AttributeDefinitionObjectItem>();
            Nodes = new List<AttributeDefinitionObjectItem>();
            Messages = new List<AttributeDefinitionObjectItem>();
            Signals = new List<AttributeDefinitionObjectItem>();
            EnvironomentVariables = new List<AttributeDefinitionObjectItem>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">"BA_DEF_ BO_  \"ModeTransmission\" ENUM  \"P\",\"E\",\"P+E\";"</param>
        /// <returns></returns>
        public AttributeDefinitionObject Parse(string input)
        {
            string remain = string.Empty;
            string typeString = input.Substring(input.IndexOf('"'));

            string[] array = input.Split(' ');
            switch (array[1])
            {

                case "BU_": /*Nodes*/
                    {
                        Nodes.Add(new AttributeDefinitionObjectItem().Parse(typeString));
                        break;
                    }
                case "BO_": /*Messages*/
                    {
                        Messages.Add(new AttributeDefinitionObjectItem().Parse(typeString));
                        break;
                    }
                case "SG_": /*Signals*/
                    {
                        Signals.Add(new AttributeDefinitionObjectItem().Parse(typeString));
                        break;
                    }
                default: /*Networks*/
                    {
                        Netowrks.Add(new AttributeDefinitionObjectItem().Parse(typeString));
                        break;
                    }
            }

            return this;
        }

        public string ToDbcFormat()
        {
            throw new NotImplementedException();
        }
    }
}
