
namespace Konvolucio.VectorCANdb.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public interface IDbcImporter
    { 
        
    }

    public class DbcImporter : IDbcImporter
    {

        readonly DbcDatabase Dbc;


        public DbcImporter()
        {
            Dbc = new DbcDatabase();
        }

        public void LoadFile(string path)
        {
            string[] line = DbcDatabase.ReadFileToStringArray(path);
            Parse(line);
        }
        void Parse(string[] lines)
        {

            string currentKeyword = "VERSION";

            string multiLineTemp = string.Empty;
            string keyword = string.Empty;
            bool complete = true;

            var messageTemp = new MessageObjectItem();

            int lineIndex = 0;
            string nextLine = string.Empty;
            for ( lineIndex = 0;
                  lineIndex < lines.Length; 
                  lineIndex++)
            {
                string line = lines[lineIndex];

                if(complete)
                    currentKeyword = DbcDatabase.GetKeyword(line);


                switch (currentKeyword)
                {
                    case  "VERSION":
                        {
                            Dbc.Version.Parse(line);
                            complete = true;
                            break;
                        }

                    case "NS_": /*Version and New Symbol Specification*/
                        {
                            multiLineTemp += line;
                            if (string.IsNullOrEmpty(line))
                            {  
                                multiLineTemp = string.Empty;
                                complete = true;
                            }
                            else
                            {

                                complete = false;
                            }
                            break;
                        }

                    case "BS_:": /*Bit Timing Definition*/
                        {
                            Dbc.Timing.Parse(line);
                            complete = true;
                            break;
                        }
                    case "BU_:": /*Node Definitions*/
                        {
                            Dbc.Nodes.Parse(line);
                            complete = true;
                            break;
                        }
                    case "VAL_TABLE_": /*Value Table Definitions*/
                        {
                            Dbc.ValueTable.Add(new ValueTableObjectItem().Parse(line));
                            complete = true;
                            break;
                        }
                    case "VAL_":  /*Signal Value Descriptions (Value Encodings)*/
                        {
                            Dbc.SignalsDescriptions.Add(new SignalValueDescriptionsItem().Parse(line));
                            complete = true;
                            break;
                        }
                    case "BO_": /*Message*/
                        {
                            messageTemp = new MessageObjectItem().Parse(line);
                            Dbc.Messages.Add(messageTemp);
                            complete = true;
                            break;
                        }
                    case "SG_": /*Definition of Message Transmitters*/
                        {
                            messageTemp.Signals.Add(new SignalItemObject().Parse(line));
                            complete = true;
                            break;
                        }
                    case "CM_": /*Comment Definitions*/
                        {
                            multiLineTemp += line;
                            if (line.EndsWith(";", StringComparison.InvariantCulture))
                            {
                                Dbc.Comment.Parse(multiLineTemp);
                                multiLineTemp = string.Empty;
                                complete = true;
                            }
                            else
                            {
                                multiLineTemp += "\r\n";
                                complete = false;
                            }
                            break;
                        }
                    case "BA_DEF_":
                        {
                            multiLineTemp += line;
                            if (line.EndsWith(";", StringComparison.InvariantCulture))
                            {
                                Dbc.AttributeDefinition.Parse(multiLineTemp);
                                multiLineTemp = string.Empty;
                                complete = true;
                            }
                            else
                            {
                                multiLineTemp += "\r\n";
                                complete = false;
                            }
                            break;
                        }
                    case "BA_": /*Attribute Values*/
                        {
                            Dbc.AttributeValue.Parse(line);
                            complete = true;

                            break;
                        }
                    default:
                        {
                            complete = true;
                            break;
                        }
                }        
            }
        }
    }
}
