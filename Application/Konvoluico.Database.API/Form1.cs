using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;

namespace Konvolucio.Database.API
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            NodeCollection nodes = new NodeCollection();

            //for (int i = 0; i < 100000; i++)
            //{
                nodes.Add(new NodeItem("PC", 0));
                nodes.Add(new NodeItem("TGT", 1));
                nodes.Add(new NodeItem("MRLY", 2));
            ////}

            CommentCollection comments = new CommentCollection();
            comments.Add(new CommentItem("NODE", "PC", "First Comment to NODE, Node Name is PC"));
            comments.Add(new CommentItem("NODE", "PC", "Second Comment to NODE, Node Name is PC"));
            //comments.Add(new CommentItem("NODE", "TGT", "Comment to MRTLY"));
            comments.Add(new CommentItem("NODE", "MRLY", "Comment to TGT"));



            AttributeCollection attributes = new AttributeCollection();
            attributes.Add(new AttributeItem("Address","NODE","STRING", "A Node-hoz tartizó cím", "Address", "[0..64]"));
            attributes.Add(new AttributeItem("DeviceNetAddress", "NODE", "STRING", "DeviceNetAddress Node-hoz tartizó cím", "DeviceNet address", "[0..64]"));
            attributes.Add(new AttributeItem("UseAdapterSerialNumber", "NODE", "STRING", "Előírja hogy ha több adapter van a akkor melyiket használja", "Adapter Serial Number", "SerialNumber"));


            DataSet dataSet = new DataSet("Database");

            //dataSet.BeginInit();

            /*Nodes*/
            /*NodeNameColumnPK, AddressColumn */
            DataTable nodesDataTable = ToDataTable(nodes, "Nodes");
            nodesDataTable.PrimaryKey = new DataColumn[]{ nodesDataTable.Columns["Name"] };
            dataSet.Tables.Add(nodesDataTable);

            /*Comments*/
            /*Object - ObjectName PK, Content*/
            DataTable commentsDataTable = ToDataTable(comments, "Comments");
            nodesDataTable.PrimaryKey = new DataColumn[] { nodesDataTable.Columns["Object"], nodesDataTable.Columns["ObjectName"] };
            dataSet.Tables.Add(commentsDataTable);

            /*Attriubtes*/
            /*Object - Name PK, Type, ToolTip, ShowName, Unit, Comment*/
            DataTable attributesDataTable = ToDataTable(attributes, "Attributes");
            attributesDataTable.PrimaryKey = new DataColumn[] { attributesDataTable.Columns["Object"], attributesDataTable.Columns["Name"] };
            dataSet.Tables.Add(attributesDataTable);

            /*Node View*/
            /*Name, Comment*/
            DataTable nodesViewDataTable = new DataTable("NodesView");

            DataColumn nodeNameColumn = new DataColumn("Name", typeof(string));
            nodesViewDataTable.Columns.Add(nodeNameColumn);

            DataColumn commentColumn = new DataColumn("Comment", typeof(string));
            nodesViewDataTable.Columns.Add(commentColumn);

          
            //ForeignKeyConstraint commentFK = new ForeignKeyConstraint("First", 

//            DataRelation commentRelation = new DataRelation("A", 


            dataSet.Tables.Add(nodesViewDataTable);

           

            //var l = dataSet.GetChanges();



            dataSet.AcceptChanges();

            //var i =   dataSet.GetChanges();

            dataGridViewBackgorundText1.DataSource = dataSet;
            dataGridViewBackgorundText1.DataMember = "NodesView";




            //ForeignKeyConstraint commentFK = new ForeignKeyConstraint("First", 



     //       DataRelation nodeCommentsRelation = 


            //dataGridViewBackgorundText1.DataSource = dataSet.DefaultViewManager;  //new NodeCollectionView() { new NodeItemView() };
            
            //dataGridViewBackgorundText1.Da
            
            //dataGridViewBackgorundText1.AutoGenerateColumns = true;



        }
        public static DataTable ToDataTable<T>(List<T> items, string tableName)
        {
            //DataTable dataTable = new DataTable(typeof(T).Name);

            DataTable dataTable = new DataTable(tableName);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private void dataGridViewBackgorundText1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


 
    }
}
