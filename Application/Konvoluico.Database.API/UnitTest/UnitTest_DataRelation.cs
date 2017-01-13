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
    public class UnitTest_DataRelation
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

        public class SuplierItem 
        {
            public int SupplierId { get; set; }
            public string SupplierName { get; set; }

            public SuplierItem(int id, string name)
            {
                SupplierId = id;
                SupplierName = name;
            }
        }
        public class SupplierCollection : List<SuplierItem>
        {

        }

        public class ProductItem
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string Description { get; set; }
            public int SupplierId { get; set; }
            public ProductItem(int id, string name, string description, int supplierId)
            {
                ProductId = id;
                ProductName = name;
                Description = description;
                SupplierId = supplierId;
            }
        }
        public class ProductCollection : List<ProductItem>
        { 
        
        }

        [Test]
        public void _0001_Test()
        {
            SupplierCollection suppliers = new SupplierCollection();
            suppliers.Add(new SuplierItem(1,"Lomex"));
            suppliers.Add(new SuplierItem(2, "TME"));
            suppliers.Add(new SuplierItem(3, "Farnell"));
            suppliers.Add(new SuplierItem(4, "CBA"));

            ProductCollection products = new ProductCollection();
            products.Add(new ProductItem(1,"1N4148", "Dióda", 1 ));
            products.Add(new ProductItem(2,"BC128", "Tranzisztor", 1 ));
            products.Add(new ProductItem(3, "AT90CAN128", "Processzor", 2));
            products.Add(new ProductItem(4, "ESD szönyeg", "ESD Védelem", 2));
            //products.Add(new ProductItem(5, "Henkel On", "Forrasztás", 3));
            //products.Add(new ProductItem(6, "Weller WP szürő", "Egészség", 3));
            //products.Add(new ProductItem(7, "Banán", "Gyümölcs", 2));


            /*Data SET*/
            DataSet dataSet = new DataSet("DemoDataSet");

            /*Suppliers*/
            /*SupplierId-PK,SupplierName*/
            DataTable suppliersDt = ToDataTable(suppliers, "Suppliers");
            suppliersDt.PrimaryKey = new DataColumn[] { suppliersDt.Columns["SupplierId"] };
            dataSet.Tables.Add(suppliersDt);

            /*Products*/
            /*ProductId-PK, ProductName, Description, SupplierId*/
            DataTable productsDt = ToDataTable(products, "Products");
            suppliersDt.PrimaryKey = new DataColumn[] { suppliersDt.Columns["ProductId"] };
            DataColumn supplierNameColumn = new DataColumn("SupplierName", typeof(string));
            productsDt.Columns.Add(supplierNameColumn);

            dataSet.Tables.Add(productsDt);

            /* */

            DataRelation productsSuppliersRelation = dataSet.Relations.Add("ProductSuppliers",
                dataSet.Tables["Suppliers"].Columns["SupplierId"],
                    dataSet.Tables["Products"].Columns["SupplierId"], true);



            dataSet.AcceptChanges();

            DataGrid dg = new DataGrid();
            dg.SetDataBinding(dataSet, "Products");

            var form = new Form2();


            form.dataGridView1.DataSource = dataSet;
            form.dataGridView1.DataMember = "Products";

            form.dataGridView2.DataSource = dataSet;
            form.dataGridView2.DataMember = "Suppliers";

           
            Application.Run(form);

        }
    }
}
