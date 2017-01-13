// -----------------------------------------------------------------------
// <copyright file="AttributeValueObject.cs" company="">
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
    /// TODO: Update summary.
    /// </summary>
    /// <summary>
    /// 12.2 Attribute Values
    /// Attributum név és aktuális érték összerendelése, ebből 5db létezik '' BU_ BO_ SG_ EV_
    /// </summary>
    public class AttributeValueObject : DbcObject<AttributeValueObject>
    {
        /// <summary>
        /// Network Value Object
        /// Keyword: NONE
        /// </summary>
        public class NetworkAttributeValueObjectItem : DbcObject<NetworkAttributeValueObjectItem>
        {
            public string Name;
            public string Value;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="input">"BA_ \"Manufacturer\" \"VAG\";"</param>
            /// <returns></returns>
            public NetworkAttributeValueObjectItem Parse(string input)
            {
                string temp = input.Substring("BA_".Length).Trim(' ', ';');

                string[] array = temp.Split('"');
                Name = array[1];
                Value = array[3];
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return "BA_ \"" + Name + "\" " + "\"" + Value + "\";";
            }


            public string ToDbcFormat()
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Node Value Object
        /// Keyword: BU_
        /// </summary>
        public class NodeAttributeValueObjectItem : DbcObject<NodeAttributeValueObjectItem>
        {
            public string Name;
            public string NodeName;
            public string Value;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="input">"BA_ \"ILUsed\" BU_ PC 1;"</param>
            /// <returns></returns>
            public NodeAttributeValueObjectItem Parse(string input)
            {
                string temp = input.Substring("BA_".Length).Trim(' ', ';');
                string[] array = temp.Split('"');
                Name = array[1];

                temp = array[2];
                array = temp.Split(' ');
                NodeName = array[2];
                Value = array[3];

                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return "BA_ \"" + Name + "\"" + " BU_ " + NodeName + " " + Value + ";";
            }


            public string ToDbcFormat()
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Messages Value Object
        /// Keyword: BO_
        /// </summary>
        public class MessageAttributeValueObjectItem : DbcObject<MessageAttributeValueObjectItem>
        {
            public string Name;
            public int ID;
            public string Value;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="input">"BA_ \"ModeTransmission\" BO_ 143 2;"</param>
            /// <returns></returns>
            public MessageAttributeValueObjectItem Parse(string input)
            {
                string temp = input.Substring("BA_".Length).Trim(' ', ';');
                string[] array = temp.Split('"');
                Name = array[1];

                temp = array[2];
                array = temp.Split(' ');
                ID = DbcDatabase.ParseStringToInt32(array[2]);
                Value = array[3];

                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return "BA_ \"" + Name + "\"" + " BO_ " + ID.ToString() + " " + Value + ";";
            }


            public string ToDbcFormat()
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Signal Value Object
        /// Keyword: SG_
        /// </summary>
        public class SignalAttributeValueObjectItem : DbcObject<SignalAttributeValueObjectItem>
        {
            public string Name;
            public int ID;
            public string SignalName;
            public string Value;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="input">"BA_ \"GenSigStartValue\" SG_ 31 REVCCEN_SER 1;"</param>
            /// <returns></returns>
            public SignalAttributeValueObjectItem Parse(string input)
            {
                string temp = input.Substring("BA_".Length).Trim(' ', ';');
                string[] array = temp.Split('"');
                Name = array[1];

                temp = array[2];
                array = temp.Split(' ');
                ID = DbcDatabase.ParseStringToInt32(array[2]);
                SignalName = array[3];
                Value = array[4];
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return "BA_ \"" + Name + "\"" + " SG_ " + ID.ToString() + " " + SignalName + " " + Value + ";";
            }


            public string ToDbcFormat()
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Environment Variable Value Object
        /// Keyword: EV_
        /// </summary>
        public class EnvironmentVariableAttributeValueObjectItem : DbcObject<EnvironmentVariableAttributeValueObjectItem>
        {
            public string Name;
            public string VariableName;
            public string Value;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="input">"BA_ \"GenMsgSendType\" EV_ COORDONNEE_X1 10;"</param>
            /// <returns></returns>
            public EnvironmentVariableAttributeValueObjectItem Parse(string input)
            {
                string temp = input.Substring("BA_".Length).Trim(' ', ';');
                string[] array = temp.Split('"');
                Name = array[1];

                temp = array[2];
                array = temp.Split(' ');
                VariableName = array[2];
                Value = array[3];

                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return "BA_ \"" + Name + "\"" + " EV_ " + VariableName + " " + Value + ";";
            }


            public string ToDbcFormat()
            {
                throw new NotImplementedException();
            }
        }

        public List<NetworkAttributeValueObjectItem> Networks;
        public List<NodeAttributeValueObjectItem> Nodes;
        public List<MessageAttributeValueObjectItem> Messages;
        public List<SignalAttributeValueObjectItem> Signals;
        public List<EnvironmentVariableAttributeValueObjectItem> EnvironomentVariables;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public AttributeValueObject()
        {
            Networks = new List<NetworkAttributeValueObjectItem>();
            Nodes = new List<NodeAttributeValueObjectItem>();
            Messages = new List<MessageAttributeValueObjectItem>();
            Signals = new List<SignalAttributeValueObjectItem>();
            EnvironomentVariables = new List<EnvironmentVariableAttributeValueObjectItem>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">
        /// 1. "BA_ \"Manufacturer\" \"VAG\";"
        /// 2. "BA_ \"ILUsed\" BU_ PC 1;"
        /// 3. "BA_ \"ModeTransmission\" BO_ 143 2;"
        /// 4. "BA_ \"GenSigStartValue\" SG_ 31 REVCCEN_SER 1;"
        /// </param>
        /// <returns></returns>
        public AttributeValueObject Parse(string input)
        {

            string[] array = input.Split(' ');
            switch (array[2])
            {
                case "BU_": /*Node*/
                    {
                        Nodes.Add(new NodeAttributeValueObjectItem().Parse(input));
                        break;
                    }
                case "BO_": /*Message*/
                    {
                        Messages.Add(new MessageAttributeValueObjectItem().Parse(input));
                        break;
                    }
                case "SG_": /*Signal*/
                    {
                        Signals.Add(new SignalAttributeValueObjectItem().Parse(input));
                        break;
                    }
                case "EV_": /*Environment Variable*/
                    {
                        EnvironomentVariables.Add(new EnvironmentVariableAttributeValueObjectItem().Parse(input));
                        break;
                    }
                default: /*Empty*/
                    {
                        Networks.Add(new NetworkAttributeValueObjectItem().Parse(input));
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
