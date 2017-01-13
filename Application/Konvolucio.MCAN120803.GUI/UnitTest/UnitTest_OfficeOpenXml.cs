

namespace Konvolucio.MCAN120803.GUI.UnitTest
{

    //http://stackoverflow.com/questions/13299862/add-style-to-excel-file-created-with-xml-using-xslt

    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using NUnit.Framework;
    using System.IO;
    using System.Windows.Forms;
    using OfficeOpenXml;
    using WinForms.Framework;


    [TestFixture]
    public class UnitTest_OfficeOpenXml
    {
        [Test]
        public void _0001_CreateSamplXlsFile()
        {
            var path = @"D:\CreateSamplXlsFile File " + Guid.NewGuid().ToString() + ".xlsx";

            FileInfo newFile = new FileInfo(path);
            using (ExcelPackage xlPackage = new ExcelPackage(newFile))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("Tinned Goods");

                // write some strings into column 1
                worksheet.Cells[1, 1].Value = "Product";
                worksheet.Cells[2, 1].Value = "Broad Beans";
                worksheet.Cells[3, 1].Value = "Carrots";
                worksheet.Cells[4, 1].Value = "Peas";
                worksheet.Cells[5, 1].Value = "Total";

                // increase the width of column one as these strings will be too wide to display
                worksheet.Column(1).Width = 15;

                // save our new workbook and we are done!
                xlPackage.Save();
            }


            var myProcess = new Process();
            myProcess.StartInfo.FileName = "Excel";
            myProcess.StartInfo.Arguments = "\"" + path + "\"";
            myProcess.Start();
        }

        [Test]
        public void _0002_ColumnWidth()
        {
            var path = @"D:\CreateSamplXlsFile File " + Guid.NewGuid().ToString() + ".xlsx";

            var persons = TestPersons();

            FileInfo newFile = new FileInfo(path);
            using (ExcelPackage xlPackage = new ExcelPackage(newFile))
            {
                // az új worksheet üres workbook
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("Persons");

                var xlsRow = 1;
                var xlsColumn = 1;


                worksheet.Cells[xlsRow, xlsColumn++].Value = "First Name";
                worksheet.Cells[xlsRow, xlsColumn].Value = "Last Name";

                /*Sorok beírása az oszlopokba*/
                foreach (var personItem in persons)
                {
                    xlsColumn = 1;
                    xlsRow++;
                    worksheet.Cells[xlsRow, xlsColumn++].Value = personItem.First;
                    worksheet.Cells[xlsRow, xlsColumn].Value = personItem.Last;
                }

                // increase the width of column one as these strings will be too wide to display
                worksheet.Column(1).Width = 225/*(pixel)*/ / 7.5;
                worksheet.Column(2).Width = 262 / 7.5;

                /*Az új workbook mentése.*/
                xlPackage.Save();
            }

            /*Excel Indul*/
            var myProcess = new Process();
            myProcess.StartInfo.FileName = "Excel";
            myProcess.StartInfo.Arguments = "\"" + path + "\"";
            myProcess.Start();
        }

        private MockPersonCollection TestPersons()
        {
            var persons = new MockPersonCollection
            {
                new MockPersonItem(first: "Homer", last: "Simposon"),
                new MockPersonItem(first: "Marge", last: "Simposon"),
                new MockPersonItem(first: "Bart", last: "Simposon"),
                new MockPersonItem(first: "Lisa", last: "Simposon")
            };
            return persons;
        }

        private class MockPersonItem
        {
            public string First { get; private set; }
            public string Last { get; private set; }

            public MockPersonItem(string first, string last)
            {
                First = first;
                Last = last;
            }
            public override string ToString()
            {
                return First + " " + Last;
            }
        }

        private class MockPersonCollection : Collection<MockPersonItem>
        {

        }
    }
}
