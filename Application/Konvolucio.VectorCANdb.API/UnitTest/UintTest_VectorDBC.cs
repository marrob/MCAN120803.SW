
namespace Konvolucio.VectorCANdb.API.UnitTest
{
    using System;
    using System.Text;
    using NUnit.Framework;
    using Konvolucio.VectorCANdb.API;

    [TestFixture]
    class UintTest_VectorDBC
    {
        [Test]
        public void _0001_Import_FILE_T0001()
        {
            DbcImporter import = new DbcImporter();
            import.LoadFile(@"D:\@@@!ProjectS\MCAN120803\ThirdParty\Vector\DBC Templates\T0001.dbc");
        }

        [Test]
        public void _0002_Import_FILE_CANDB01_TT9C_TestBox_CAN_DBC_v3()
        {
            DbcImporter import = new DbcImporter();
            import.LoadFile(@"D:\@@@!ProjectS\MCAN120803\ThirdParty\Vector\CANDB01 T9C_TestBox_CAN_DBC_v3.dbc");
        }


        [Test]
        public void _0003_Import_FILE_VectorDbcFileFormatSample()
        {
            DbcImporter import = new DbcImporter();
            import.LoadFile(@"D:\@@@!ProjectS\MCAN120803\ThirdParty\Vector\DBC Templates\VectorDbcFileFormatSample.dbc");
        }


        [Test]
        public void _0003_Version()
        {
            VersionObject Version = new VersionObject();
            Version.Parse("VERSION \"12\"");
            Assert.AreEqual("12", Version.Value);
        }

        [Test]
        public void _0004_Nodes()
        {
            NodeObject nodes = new NodeObject();
            nodes.Parse("BU_: New_Node_11New_Node_11 1New_Node_10New_Node_101 New_Node_8New_Node_8New_Node_8 New_Node_6_New_ DGT Omron PC");
            Assert.AreEqual("New_Node_11New_Node_11", nodes.Names[0]);   
        }

        [Test]
        public void _0005_ValueTable_1()
        {
            ValueTableObjectItem table = new ValueTableObjectItem();
            table.Parse("VAL_TABLE_ ValueTableItem1 3 \"Érték leírása 0x03\" ;");
            Assert.AreEqual("ValueTableItem1", table.Name);
       
            Assert.AreEqual(3, table.Descriptions[0].Value);
            Assert.AreEqual("Érték leírása 0x03", table.Descriptions[0].Description);
        }

        [Test]
        public void _0006_ValueTable_2()
        {
            ValueTableObjectItem table = new ValueTableObjectItem();
            table.Parse("VAL_TABLE_ ValueTableItem1 3 \"Érték leírása 0x03\" 2 \"Érték leírása 0x02\" ;");
            Assert.AreEqual("ValueTableItem1", table.Name);
       
            Assert.AreEqual(3, table.Descriptions[0].Value);
            Assert.AreEqual("Érték leírása 0x03", table.Descriptions[0].Description);

            Assert.AreEqual(2, table.Descriptions[1].Value);
            Assert.AreEqual("Érték leírása 0x02", table.Descriptions[1].Description);
        }

        [Test]
        public void _0007_ValueTable_3()
        {
            ValueTableObjectItem table = new ValueTableObjectItem();
            table.Parse("VAL_TABLE_ ValueTableItem1 3 \"Érték leírása 0x03\" 2 \"Érték leírása 0x02\" 1 \"Érték leírása 0x01\" ;");

            Assert.AreEqual("ValueTableItem1", table.Name);

            Assert.AreEqual(3, table.Descriptions[0].Value);
            Assert.AreEqual("Érték leírása 0x03", table.Descriptions[0].Description);

            Assert.AreEqual(2, table.Descriptions[1].Value);
            Assert.AreEqual("Érték leírása 0x02", table.Descriptions[1].Description);

            Assert.AreEqual(1, table.Descriptions[2].Value);
            Assert.AreEqual("Érték leírása 0x01", table.Descriptions[2].Description);
        }

        [Test]
        public void _0008_ValueTable_4()
        {
            ValueTableObjectItem table = new ValueTableObjectItem();
            table.Parse("VAL_TABLE_ ValueTableItem1 3 \"Érték leírása 0x03\" 2 \"Érték leírása 0x02\" 1 \"Érték leírása 0x01\" 0 \"Érték leírása\" ;");
          
            Assert.AreEqual("ValueTableItem1", table.Name);

            Assert.AreEqual(3, table.Descriptions[0].Value);
            Assert.AreEqual("Érték leírása 0x03", table.Descriptions[0].Description);

            Assert.AreEqual(2, table.Descriptions[1].Value);
            Assert.AreEqual("Érték leírása 0x02", table.Descriptions[1].Description);

            Assert.AreEqual(1, table.Descriptions[2].Value);
            Assert.AreEqual("Érték leírása 0x01", table.Descriptions[2].Description);

            Assert.AreEqual(0, table.Descriptions[3].Value);
            Assert.AreEqual("Érték leírása", table.Descriptions[3].Description);
        }

        [Test]
        public void _0009_Message()
        {
            var msg = new MessageObjectItem();
            msg.Parse("BO_ 143 ERROR_INDICATORS_1: 8 DGT");
            Assert.AreEqual(143, msg.ArbitrationId);
            Assert.AreEqual("ERROR_INDICATORS_1", msg.Name);
            Assert.AreEqual(8, msg.Size);
            Assert.AreEqual("DGT", msg.TransmitterNodes.Names[0]);
        }

        [Test]
        public void _0010_Signal()
        {
            var signal = new SignalItemObject();
            signal.Parse("SG_ DATATION_APPUI_3 : 39|16@0+ (0.1,0) [0|6553.5] \"ms 1\"  PC");

            Assert.AreEqual("DATATION_APPUI_3", signal.Name);
            Assert.AreEqual("", signal.MultiplesxerIndicator);
            Assert.AreEqual(39, signal.StartBit);
            Assert.AreEqual(16, signal.SignalSize);
            Assert.AreEqual('0', signal.ByteOrder);
            Assert.AreEqual('+', signal.ValueType);
            Assert.AreEqual(0.1, signal.Factor);
            Assert.AreEqual(0.0, signal.Offset);
            Assert.AreEqual(0.0, signal.Minimum);
            Assert.AreEqual(6553.5, signal.Maximum);
            Assert.AreEqual("ms 1", signal.Unit);
            Assert.AreEqual("PC", signal.ReceiverNodeName);
        }

        [Test]
        public void _0011_Signal()
        {
            var signal = new SignalItemObject();
            signal.Parse("SG_ COUNT_LINE_FAULT_ERRORS_MAX M : 55|16@0+ (1,0) [0|65535] \"ms\"  PC");

            Assert.AreEqual("COUNT_LINE_FAULT_ERRORS_MAX", signal.Name);
            Assert.AreEqual("M", signal.MultiplesxerIndicator);
            Assert.AreEqual(55, signal.StartBit);
            Assert.AreEqual(16, signal.SignalSize);
            Assert.AreEqual('0', signal.ByteOrder);
            Assert.AreEqual('+', signal.ValueType);
            Assert.AreEqual(1.0, signal.Factor);
            Assert.AreEqual(0.0, signal.Offset);
            Assert.AreEqual(0.0, signal.Minimum);
            Assert.AreEqual(65535.0, signal.Maximum);
            Assert.AreEqual("ms", signal.Unit);
            Assert.AreEqual("PC", signal.ReceiverNodeName);
        }

        [Test]
        public void _0012_MessageTransmitters()
        {
            var trasmitters = new MessageTransmittersObject();
            trasmitters.Parse("BO_TX_BU_ 1 : DEMO_NODE,OMRON_NODE,PC_NODE;");
            Assert.AreEqual(1, trasmitters.ArbitrationId);
            Assert.AreEqual("DEMO_NODE", trasmitters.TransmitterNames[0]);
            Assert.AreEqual("OMRON_NODE", trasmitters.TransmitterNames[1]);
            Assert.AreEqual("PC_NODE", trasmitters.TransmitterNames[2]);
        }



        [Test]
        public void _0013_SignalValueDescriptions_1()
        {

            var valueDesc = new SignalValueDescriptionsItem();
            valueDesc.Parse("VAL_ 31 DMD_DARK 0 \"pas de demande\" ;");

            Assert.AreEqual(31, valueDesc.ArbitrationId);
            Assert.AreEqual("DMD_DARK", valueDesc.Name);

            Assert.AreEqual(0, valueDesc.Descriptions[0].Value);
            Assert.AreEqual("pas de demande", valueDesc.Descriptions[0].Description);
        }

        [Test]
        public void _0014_SignalValueDescriptions_2()
        {

            var valueDesc = new SignalValueDescriptionsItem();
            valueDesc.Parse("VAL_ 31 DMD_DARK 0 \"pas de demande\" 3 \"Invalide\" ;");

            Assert.AreEqual(31, valueDesc.ArbitrationId);
            Assert.AreEqual("DMD_DARK", valueDesc.Name);

            Assert.AreEqual(0, valueDesc.Descriptions[0].Value);
            Assert.AreEqual("pas de demande", valueDesc.Descriptions[0].Description);
        }

        [Test]
        public void _0015_SignalValueDescriptions_3()
        {

            var valueDesc = new SignalValueDescriptionsItem();
            valueDesc.Parse("VAL_ 31 DMD_DARK 0 \"pas de demande\" 1 \"Dark 0 demandé\" 2 \"Dark 2 demandé\" ;");

            Assert.AreEqual(31, valueDesc.ArbitrationId);
            Assert.AreEqual("DMD_DARK", valueDesc.Name);
            
            Assert.AreEqual(0, valueDesc.Descriptions[0].Value);
            Assert.AreEqual("pas de demande", valueDesc.Descriptions[0].Description);

            Assert.AreEqual(1, valueDesc.Descriptions[1].Value);
            Assert.AreEqual("Dark 0 demandé", valueDesc.Descriptions[1].Description);

            Assert.AreEqual(2, valueDesc.Descriptions[2].Value);
            Assert.AreEqual("Dark 2 demandé", valueDesc.Descriptions[2].Description);
        }


        [Test]
        public void _0016_SignalValueDescriptions_4()
        {

            var valueDesc = new SignalValueDescriptionsItem();
            valueDesc.Parse("VAL_ 31 DMD_DARK 0 \"pas de demande\" 1 \"Dark 0 demandé\" 2 \"Dark 2 demandé\" 3 \"Invalide\" ;");

            Assert.AreEqual(31, valueDesc.ArbitrationId);
            Assert.AreEqual("DMD_DARK", valueDesc.Name);

            Assert.AreEqual(0, valueDesc.Descriptions[0].Value);
            Assert.AreEqual("pas de demande", valueDesc.Descriptions[0].Description);

            Assert.AreEqual(1, valueDesc.Descriptions[1].Value);
            Assert.AreEqual("Dark 0 demandé", valueDesc.Descriptions[1].Description);

            Assert.AreEqual(2, valueDesc.Descriptions[2].Value);
            Assert.AreEqual("Dark 2 demandé", valueDesc.Descriptions[2].Description);

            Assert.AreEqual(3, valueDesc.Descriptions[3].Value);
            Assert.AreEqual("Invalide", valueDesc.Descriptions[3].Description);
        }

        [Test]
        public void _0017_SignalValueDescriptions_5()
        {
            var valueDesc = new SignalValueDescriptionsItem();
            valueDesc.Parse("VAL_ 31 DMD_DARK 0 \"pas de demande\" 1 \"Dark 0 demandé\" 2 \"Dark 2 demandé\" 3 \"Invalide\" 4 \"AB CD\" ;");

            Assert.AreEqual(31, valueDesc.ArbitrationId);
            Assert.AreEqual("DMD_DARK", valueDesc.Name);

            Assert.AreEqual(0, valueDesc.Descriptions[0].Value);
            Assert.AreEqual("pas de demande", valueDesc.Descriptions[0].Description);

            Assert.AreEqual(1, valueDesc.Descriptions[1].Value);
            Assert.AreEqual("Dark 0 demandé", valueDesc.Descriptions[1].Description);

            Assert.AreEqual(2, valueDesc.Descriptions[2].Value);
            Assert.AreEqual("Dark 2 demandé", valueDesc.Descriptions[2].Description);

            Assert.AreEqual(3, valueDesc.Descriptions[3].Value);
            Assert.AreEqual("Invalide", valueDesc.Descriptions[3].Description);

            Assert.AreEqual(4, valueDesc.Descriptions[4].Value);
            Assert.AreEqual("AB CD", valueDesc.Descriptions[4].Description);
        }

        [Test]
        public void _0018_Comment()
        {
            string input = string.Empty;
            string expect = string.Empty;
            var comments = new CommentsObject();
        
            input = "CM_ \"Multi\r\nline\r\nNetwork\r\nComment\";";

            expect = "Multi\r\nline\r\nNetwork\r\nComment";
            comments.Parse(input);
            Assert.AreEqual(expect, comments.Network);

            input = "CM_ \"Single Line Comment\";";
            expect = "Single Line Comment";
            comments.Parse(input);
            Assert.AreEqual(expect, comments.Network);

            int msgTestIndex = 0;

            input = "CM_ BO_ 2 \"Single Line Comment\";";
            expect = "Single Line Comment";
            comments.Parse(input);
            Assert.AreEqual(2, comments.Messages[msgTestIndex].ArbitrationId);
            Assert.AreEqual(expect, comments.Messages[msgTestIndex].Comment);

            input = "CM_ BO_ 2 \"Multi\r\nline\r\nNetwork\r\nComment\";";
            expect = "Single Line Comment";
            comments.Parse(input);
            Assert.AreEqual(2, comments.Messages[msgTestIndex].ArbitrationId);
            Assert.AreEqual(expect, comments.Messages[msgTestIndex].Comment);


            int sigTestIndex = 0;

            input = "CM_ SG_ 4 EM_State_Signal \"Jel megjegyzés\";";
            expect = "Jel megjegyzés";
            comments.Parse(input);
            Assert.AreEqual("EM_State_Signal", comments.Signals[sigTestIndex].Name);
            Assert.AreEqual(4, comments.Signals[sigTestIndex].ArbitrationId);
            Assert.AreEqual(expect, comments.Signals[sigTestIndex].Comment);


            int nodeTestIndex = 0;

            input = "CM_ BU_ Omron \"Node Comment\";";
            expect = "Node Comment";
            comments.Parse(input);
            Assert.AreEqual("Omron", comments.Nodes[nodeTestIndex].Name);
            Assert.AreEqual(expect, comments.Nodes[nodeTestIndex].Comment);

        }

        [Test]
        public void _0019_AttributeValues()
        {
            string input = string.Empty;
            string expect = string.Empty;
            int testIndex = 0;
            var attrValue = new AttributeValueObject();

            input = "BA_ \"Manufacturer\" \"VAG\";";
            attrValue.Parse(input);
            testIndex = 0;
            Assert.AreEqual("Manufacturer", attrValue.Networks[testIndex].Name);
            Assert.AreEqual("VAG", attrValue.Networks[testIndex].Value);
            Assert.AreEqual(input,attrValue.Networks[testIndex].ToString());


            input = "BA_ \"ILUsed\" BU_ PC 1;";
            attrValue.Parse(input);
            testIndex = 0;
            Assert.AreEqual("ILUsed", attrValue.Nodes[testIndex].Name);
            Assert.AreEqual("PC", attrValue.Nodes[testIndex].NodeName);
            Assert.AreEqual("1", attrValue.Nodes[testIndex].Value);
            Assert.AreEqual(input, attrValue.Nodes[testIndex].ToString());


            input = "BA_ \"ModeTransmission\" BO_ 143 2;";
            attrValue.Parse(input);
            testIndex = 0;
            Assert.AreEqual("ModeTransmission", attrValue.Messages[testIndex].Name);
            Assert.AreEqual(143, attrValue.Messages[testIndex].ID);
            Assert.AreEqual("2", attrValue.Messages[testIndex].Value);
            Assert.AreEqual(input, attrValue.Messages[testIndex].ToString());


            input = "BA_ \"GenSigStartValue\" SG_ 31 REVCCEN_SER 1;";
            attrValue.Parse(input);
            testIndex = 0;
            Assert.AreEqual("GenSigStartValue", attrValue.Signals[testIndex].Name);
            Assert.AreEqual(31, attrValue.Signals[testIndex].ID);
            Assert.AreEqual("REVCCEN_SER", attrValue.Signals[testIndex].SignalName);
            Assert.AreEqual("1", attrValue.Signals[testIndex].Value);
            Assert.AreEqual(input, attrValue.Signals[testIndex].ToString());

            input = "BA_ \"GenMsgSendType\" EV_ COORDONNEE_X1 10;";
            attrValue.Parse(input);
            testIndex = 0;
            Assert.AreEqual("GenMsgSendType", attrValue.EnvironomentVariables[testIndex].Name);
            Assert.AreEqual("COORDONNEE_X1", attrValue.EnvironomentVariables[testIndex].VariableName);
            Assert.AreEqual("10", attrValue.EnvironomentVariables[testIndex].Value);
            Assert.AreEqual(input, attrValue.EnvironomentVariables[testIndex].ToString());
        }

        [Test]
        public void _0020_AttributeDefinition()
        {
            string input = string.Empty;
            string expect = string.Empty;
            int testIndex = 0;
            var attrDef = new AttributeDefinitionObject();

            input = "BA_DEF_ BU_  \"NodeLayerModules\" STRING;";
            attrDef.Parse(input);
            testIndex = 0;
            Assert.AreEqual("NodeLayerModules", attrDef.Nodes[testIndex].Name);
            Assert.AreEqual("STRING", attrDef.Nodes[testIndex].AttributeType.ToString());
            Assert.AreEqual(input, attrDef.Nodes[testIndex].ToString());

            input = "BA_DEF_ BU_  \"GenSigStartValue\" INT -2147483648 2147483647;";
            attrDef.Parse(input);
            testIndex++;
            Assert.AreEqual("GenSigStartValue", attrDef.Nodes[testIndex].Name);
            Assert.AreEqual("INT -2147483648 2147483647", attrDef.Nodes[testIndex].AttributeType.ToString());
            Assert.AreEqual(input, attrDef.Nodes[testIndex].ToString());


            input = "BA_DEF_ BU_  \"ModeTransmission\" ENUM  \"P\",\"E\",\"P+E\";";
            attrDef.Parse(input);
            testIndex++;
            Assert.AreEqual("ModeTransmission", attrDef.Nodes[testIndex].Name);
            Assert.AreEqual("ENUM \"P\",\"E\",\"P+E\"", attrDef.Nodes[testIndex].AttributeType.ToString());
            Assert.AreEqual(input, attrDef.Nodes[testIndex].ToDbcFormat());
        }

    }
}
