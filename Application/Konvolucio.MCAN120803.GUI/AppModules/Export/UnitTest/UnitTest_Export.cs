// -----------------------------------------------------------------------
// <copyright file="UnitTest_Export.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Export.UnitTest
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using Model;
    using NUnit.Framework;
    using View;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class UnitTest_Export
    {
        [Test]
        public void _0001_AddRows()
        {

            var table = new ExportTable();

            table.Columns.Add(new ExportColumnItem() {Name = "columnIndex"});
            table.Columns.Add(new ExportColumnItem() {Name = "columnFirstName"});
            table.Columns.Add(new ExportColumnItem() {Name = "columnLastName"});

            for (int i = 0; i < 5; i++)
            {
                var row = new ExportRowItem();
                table.Rows.Add(row);
                row.Cells[0].Value = "Index:" + i.ToString();
                row.Cells[1].Value = "Homer";
                row.Cells[2].Value = "Simpson";
            }
            foreach (var item in table.Rows)
                Debug.WriteLine(item);

            Assert.AreEqual(3, table.Columns.Count);
            Assert.AreEqual(5, table.Rows.Count);
        }


        [Test]
        public void _0002_AddRows()
        {
            var table = new ExportTable();
            table.Columns.Add(new ExportColumnItem() {Name = "columnFirstName"});
            table.Columns.Add(new ExportColumnItem() {Name = "columnLastName"});
            table.Rows.Add(new ExportRowItem());

            Assert.AreEqual(2, table.Columns.Count);
            Assert.AreEqual(1, table.Rows.Count);
        }

        [Test]
        public void _0003_DataGrid_To_ExportTable()
        {
            var dgv = TestDataGridView();
            var table = new ExportTable();

            foreach (DataGridViewColumn dgvColumn in dgv.Columns)
            {
                var exportColumn = new ExportColumnItem()
                {
                    Name = dgv.Name,
                    HeaderText = dgvColumn.HeaderText,
                    DisplayIndex = dgvColumn.DisplayIndex,
                    Visible = dgvColumn.Visible,
                };
                table.Columns.Add(exportColumn);
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                var newRow = new ExportRowItem();
                table.Rows.Add(newRow);
                for (var i = 0; i < dgvRow.Cells.Count; i++)
                    newRow.Cells[i].Value = dgvRow.Cells[i].Value.ToString();
            }

            foreach (var item in table.Rows)
                Debug.WriteLine(item);


            Assert.AreEqual(3, table.Columns.Count);
            Assert.AreEqual(4, table.Rows.Count);
        }

        [Test]
        public void _0004_DataGrid_To_ExportTable()
        {
            var dgv = TestDataGridView();
            var table = new ExportTable();

            foreach (DataGridViewColumn dgvColumn in dgv.Columns)
            {
                var exportColumn = new ExportColumnItem()
                {
                    Name = dgvColumn.Name,
                    HeaderText = dgvColumn.HeaderText,
                    DisplayIndex = dgvColumn.DisplayIndex,
                    Visible = dgvColumn.Visible,
                };
                table.Columns.Add(exportColumn);
            }

            for (var row = 0; row < dgv.Rows.Count; row++)
            {
                table.Rows.Add(new ExportRowItem());
                for (var column = 0; column < dgv.ColumnCount; column++)
                    table.Rows[row].Cells[column].Value = dgv.Rows[row].Cells[column].Value.ToString();
            }

            foreach (var item in table.Rows)
                Debug.WriteLine(item);

            Assert.AreEqual(3, table.Columns.Count);
            Assert.AreEqual(4, table.Rows.Count);
        }

        [Test]
        public void _0005_ExportTable_To_DataGrid()
        {
            var table = TestExportTable();
            var dgv = new DataGridView() {AllowUserToAddRows = false};

            foreach (var tableColumn in table.Columns)
            {
                dgv.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = tableColumn.Name,
                    HeaderText = tableColumn.HeaderText,
                    DisplayIndex = tableColumn.DisplayIndex,
                    Visible = tableColumn.Visible,
                });
            }

            for (var rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
            {
                dgv.Rows.Add(new DataGridViewRow());

                for (var columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
                    dgv.Rows[rowIndex].Cells[columnIndex].Value = table.Rows[rowIndex].Cells[columnIndex].Value;
            }

            Assert.AreEqual("Bart", dgv.Rows[1].Cells[1].Value.ToString());
        }

        private ExportTable TestExportTable()
        {
            var table = new ExportTable();

            table.Columns.Add(new ExportColumnItem()
            {
                Name = "columnIndex",
                HeaderText = "Index",
                DisplayIndex = 1,
                Visible = true,
            });

            table.Columns.Add(new ExportColumnItem()
            {
                Name = "columnFirst",
                HeaderText = "First Name",
                DisplayIndex = 2,
                Visible = true,
            });

            table.Columns.Add(new ExportColumnItem()
            {
                Name = "columnLast",
                HeaderText = "Last Name",
                DisplayIndex = 3,
                Visible = true,
            });

            table.Rows.Add(new ExportRowItem());
            table.Rows[0].Cells[0].Value = "1";
            table.Rows[0].Cells[1].Value = "Homer";
            table.Rows[0].Cells[2].Value = "Simpsons";

            table.Rows.Add(new ExportRowItem());
            table.Rows[1].Cells[0].Value = "2";
            table.Rows[1].Cells[1].Value = "Bart";
            table.Rows[1].Cells[2].Value = "Simpsons";

            table.Rows.Add(new ExportRowItem());
            table.Rows[2].Cells[0].Value = "3";
            table.Rows[2].Cells[1].Value = "Marge";
            table.Rows[2].Cells[2].Value = "Simpsons";

            table.Rows.Add(new ExportRowItem());
            table.Rows[3].Cells[0].Value = "4";
            table.Rows[3].Cells[1].Value = "Lisa";
            table.Rows[3].Cells[2].Value = "Simpsons";

            return table;
        }

        private DataGridView TestDataGridView()
        {
            var dgv = new DataGridView
            {
                AllowUserToAddRows = false
            };

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "columnIndex",
                HeaderText = "Index",
                Visible = true,
                DisplayIndex = 1,
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "columnFirstName",
                HeaderText = "First Name",
                Visible = true,
                DisplayIndex = 2,
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "columnLastName",
                HeaderText = "Last Name",
                Visible = true,
                DisplayIndex = 3,
            });

            dgv.Rows.Add(new DataGridViewRow());
            dgv.Rows[0].Cells[0].Value = "1";
            dgv.Rows[0].Cells[1].Value = "Homer";
            dgv.Rows[0].Cells[2].Value = "Simpsons";

            dgv.Rows.Add(new DataGridViewRow());
            dgv.Rows[1].Cells[0].Value = "2";
            dgv.Rows[1].Cells[1].Value = "Bart";
            dgv.Rows[1].Cells[2].Value = "Simpsons";

            dgv.Rows.Add(new DataGridViewRow());
            dgv.Rows[2].Cells[0].Value = "3";
            dgv.Rows[2].Cells[1].Value = "Marge";
            dgv.Rows[2].Cells[2].Value = "Simpsons";

            dgv.Rows.Add(new DataGridViewRow());
            dgv.Rows[3].Cells[0].Value = "4";
            dgv.Rows[3].Cells[1].Value = "Lisa";
            dgv.Rows[3].Cells[2].Value = "Simpsons";

            return dgv;
        }

        [Test]
        public void _0006_ShowDialg()
        {
            ExportForm ef  = new ExportForm();           
            ExportTable table = TestExportTable();

            var uniqueFileName = "Exported File " + Guid.NewGuid().ToString();

            ef.ExportTableSource = table;
            ef.FileName = uniqueFileName;
            ef.Directory = "Könyvtár";

            ef.ShowDialog();

            Assert.IsTrue(System.IO.File.Exists(Environment.CurrentDirectory + "\\" + ef.Directory + "\\" + ef.FileName + ".csv"),"A fájl nem jött létre.");
        }


        [Test]
        public void _0007_ShowDialg()
        {
            ExportForm ef = new ExportForm();
            ExportTable table = TestExportTable();
            ef.ExportTableSource = table;
            ef.FileName = "Demo";
            ef.Directory = "Könyvtár";
            ef.ShowDialog();

   
        
        }

        [Test]
        public void _0007_CsvExporters()
        {
            var exporters = new ExporterCollection();
            var ev = new AutoResetEvent(false);

            foreach (var item in exporters)
                Debug.WriteLine(item.Name);


            var exporter = exporters.FirstOrDefault(n => n.Name == "Comma-separated values");

            if(exporter== null)
                Assert.Fail("Fail");

            var options = new CsvOptions();
            exporter.Path = "D:\\Test.csv";
            exporter.Options = options;
            exporter.DataSource = TestExportTable();

            exporter.ProgressChanged += (o, e) => { Debug.WriteLine(e.UserState + " " + e.ProgressPercentage + "%" ); };
            exporter.Completed += (o, e) => { ev.Set(); };

            exporter.Start();

            ev.WaitOne();

            exporter.Dispose();
        }
    }
}
