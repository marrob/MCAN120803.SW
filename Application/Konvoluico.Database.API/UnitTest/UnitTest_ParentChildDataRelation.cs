// -----------------------------------------------------------------------
// <copyright file="UnitTest_00AA.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.Database.API.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using System.Reflection;
    using NUnit.Framework;
    using System.Windows.Forms;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class UnitTest_ParentChildDataRelation
    {
        #region DataTable Helper
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(List<T> items, string tableName)
        {
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
        #endregion 

        public class ParentItem 
        {
            public int ParentId { get; set; }
            public string ParentName { get; set; }

            public ParentItem(int id, string name)
            {
                ParentId = id;
                ParentName = name;
            }
        }  //Szülő
        public class ParentCollection : List<ParentItem>
        {

        }

        public class ChildItem
        {
            public int ChildId { get; set; }
            public string ChildName { get; set; }
            public int ParentId { get; set; }
            public ChildItem(int id, string name, int parentId)
            {
                ChildId = id;
                ChildName = name;
                ParentId = parentId;
            }
        }  //Gyerek
        public class ChildCollection : List<ChildItem>
        { 
        
        }

        [Test]
        public void _0001_Test()
        {
            //https://msdn.microsoft.com/en-us/library/system.data.datatable.childrelations(v=vs.110).aspx
            ParentCollection parents = new ParentCollection();
            parents.Add(new ParentItem(1, "Parent-1"));
            parents.Add(new ParentItem(2, "Parent-2"));
            parents.Add(new ParentItem(3, "Parent-3"));
            parents.Add(new ParentItem(4, "Parent-4"));

            ChildCollection childs = new ChildCollection();
            childs.Add(new ChildItem(1, "Child-1", 1));
            childs.Add(new ChildItem(2, "Child-2", 1));
            childs.Add(new ChildItem(3, "Child-3", 3));
            childs.Add(new ChildItem(4, "Child-4", 4));
            childs.Add(new ChildItem(5, "Child-5", 3));
            childs.Add(new ChildItem(6, "Child-6", 4));


            /*Data SET*/
            DataSet dataSet = new DataSet("DemoDataSet");

            /*Parent*/
            /*ParentId-PK,ParentName*/
            DataTable parentsDt = ToDataTable(parents, "Parents");
            parentsDt.PrimaryKey = new DataColumn[] { parentsDt.Columns["ParentId"] };
            dataSet.Tables.Add(parentsDt);

            /*Childs*/
            /*ChildId-PK, ChildName, ParentId*/
            DataTable childsDt = ToDataTable(childs, "Childs");
            childsDt.PrimaryKey = new DataColumn[] { parentsDt.Columns["ChildId"] };
            DataColumn parentsNameColumn = new DataColumn("ParentName", typeof(string));

            childsDt.Columns.Add(parentsNameColumn);
            dataSet.Tables.Add(childsDt);

            /*SzÜlők gyerekei reláció*/
            /*A szülő táblán végig lépkedve megtudható a szülők gyerekei*/
            /*A szülő táblázat lesz amiben megjelnnek az eredmények!*/
            DataRelation parentChildRelation = dataSet.Relations.Add("ParentChilds",
                dataSet.Tables["Parents"].Columns["ParentId"],
                    dataSet.Tables["Childs"].Columns["ParentId"], false);

            foreach (DataRow parent in parentsDt.Rows)
            {
                /*Itt használja a relációt*/
                DataRow[] children = parent.GetChildRows(parentChildRelation); //A Child táblával dolgozik
                Console.WriteLine("\n{0}, has {1} children", parent["ParentName"].ToString(), children.Count<DataRow>());
                foreach (DataRow child in children)
                {
                    Console.WriteLine("\t{0}", child["ChildName"].ToString());
                }
            }


            Form2 f2 = new Form2();

            f2.dataGridView1.DataSource = dataSet;
            f2.dataGridView1.DataMember = "Parents";

            /*A szülők gyerekei*/
            /*Egy szülőnek több gyereke is lehet, ezért az eredmény táblázat a gyerek számával nő, a szülők ismétlődnek*/
            /*Ez a JOIN*/
            var results = from table1 in parentsDt.AsEnumerable()
                          join table2 in childsDt.AsEnumerable() on table1["ParentId"] equals table2["ParentId"]
                          select new
                          {
                              ParentId = table1["ParentId"],
                              ParentName = table1["ParentName"],
                              ChildName = table2["ChildName"],
                          };

            var list = results.ToList();
            f2.dataGridView2.DataSource = list;


            Application.Run(f2);
        }
    }
}
