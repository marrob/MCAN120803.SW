// -----------------------------------------------------------------------
// <copyright file="UnitTest_DataGridView.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework.DataGridViewControl.UnitTest
{
    using System;
    using System.Linq;
    using System.Net.Mime;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Commands;
    using Models;
    using NUnit.Framework;
    using View;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class UnitTest_DataGridView
    {
        [Test]
        public void _0001_IBingigList_AddNew_And_DeleteRow_ShortcutKeys_ContextMenuInDgv()
        {
            var form = new SingleGridForm();
            var dgv = form.GridView;

            var persons = new PersonCollection()
            {
                new PersonItem("Homer", "Simpson", 40),
                new PersonItem("Bart", "Simpson", 10),
                new PersonItem("Lisa", "Simpson", 13),
                new PersonItem("Marge", "Simpson", 10),
            };
            dgv.DataSource = persons;
            dgv.ContextMenuStrip.Items.AddRange(
                new ToolStripItem[]
                {
                    new NewRowCommand(dgv, persons),
                    new DeleteRowCommand(dgv, persons),
                    new NewPersonCommand(dgv, persons),
                    new NewPersonForPositionCommand(dgv, persons),
                });

            form.ShowDialog();
        }

        [Test]
        public void _0001_DualGrid()
        {
            IDualGridForm1 form = new DualGridForm();

            var personsRight = new PersonCollection()
            {
                new PersonItem("Homer", "Simpson", 40),
                new PersonItem("Bart", "Simpson", 10),
                new PersonItem("Lisa", "Simpson", 13),
                new PersonItem("Marge", "Simpson", 10),
            };


            var dgvRight = form.RightGird.GridView;
            dgvRight.DataSource = personsRight;
            form.RightGird.TextBacgkground = "RIGHT";
            dgvRight.ContextMenuStrip.Items.AddRange(
                new ToolStripItem[]
                {
                    new NewRowCommand(dgvRight, personsRight),
                    new DeleteRowCommand(dgvRight, personsRight),
                });

            var personsLeft = new PersonCollection();
            var dgvLeft = form.LeftGrid.GridView;
            form.LeftGrid.TextBacgkground = "LEFT";
            dgvLeft.DataSource = personsLeft;
            dgvLeft.ContextMenuStrip.Items.AddRange(
                new ToolStripItem[]
                {
                    new NewRowCommand(dgvLeft, personsLeft),
                    new DeleteRowCommand(dgvLeft, personsLeft),
                });

            form.ShowDialog();
        }

        [Test]
        [STAThread]
        public void _0002_DualGrid_Copy_Paste()
        {
            IDualGridForm1 form = new DualGridForm();

            var personsRight = new PersonCollection()
            {
                new PersonItem("Homer", "Simpson", 40),
                new PersonItem("Bart", "Simpson", 10),
                new PersonItem("Lisa", "Simpson", 13),
                new PersonItem("Marge", "Simpson", 10),
            };


            var dgvRight = form.RightGird.GridView;
            dgvRight.DataSource = personsRight;
            form.RightGird.TextBacgkground = "RIGHT";
            dgvRight.ContextMenuStrip.Items.AddRange(
                new ToolStripItem[]
                {
                    new NewRowCommand(dgvRight, personsRight),
                    new DeleteRowCommand(dgvRight, personsRight),
                    new CopyRowCommand(dgvRight, personsRight), 
                     new PasteRowsCommand(dgvRight, personsRight), 
                     new CutRowsCommand(dgvRight, personsRight), 
                });

            var personsLeft = new PersonCollection();
            var dgvLeft = form.LeftGrid.GridView;
            form.LeftGrid.TextBacgkground = "LEFT";
            dgvLeft.DataSource = personsLeft;
            dgvLeft.ContextMenuStrip.Items.AddRange(
                new ToolStripItem[]
                {
                    new NewRowCommand(dgvLeft, personsLeft),
                    new DeleteRowCommand(dgvLeft, personsLeft),
                    new CopyRowCommand(dgvLeft, personsLeft), 
                    new PasteRowsCommand(dgvLeft, personsLeft), 
                });

            form.ShowDialog();
        }
        [Test]
        [STAThread]
        public void _0003_New_Item_To_Not_Empty()
        {
            var form = new SingleGridForm();
            var dgv = form.GridView;
            var persons = new PersonCollection() {  };
            dgv.DataSource = persons;
            dgv.ContextMenuStrip.Items.AddRange(
                new ToolStripItem[]
                {
                    new NewRowCommand(dgv, persons), 
                });
            var menu = form.dataGridView1.ContextMenuStrip;
            var command = menu.Items.Cast<ToolStripItem>().FirstOrDefault(n => n.Text == @"New");
            if (command != null)
                command.PerformClick();
            Assert.AreEqual(1, persons.Count);
        }

        [Test]
        [STAThread]
        public void _0003_New_Item_To_Not_Empty_Collection()
        {
            var form = new SingleGridForm();
            var dgv = form.GridView;

            var persons = new PersonCollection() { new PersonItem("Homer", "Simpson", 40), };
            dgv.DataSource = persons;
            dgv.ContextMenuStrip.Items.AddRange(
                new ToolStripItem[]
                {
                    new NewRowCommand(dgv, persons), 
                });

            var menu = form.dataGridView1.ContextMenuStrip;

            dgv.Rows[0].Selected = true;

            var command = menu.Items.Cast<ToolStripItem>().FirstOrDefault(n => n.Text == @"New");
            if (command != null)
                command.PerformClick();

            Assert.AreEqual(2, persons.Count);

        }

        [Test]
        [STAThread]
        public void _0003_Copy_One_Item()
        {
            var form = new SingleGridForm();
            var dgv = form.GridView;

            var persons = new PersonCollection()
            {
                new PersonItem("Homer", "Simpson", 40),
                new PersonItem("Bart", "Simpson", 10),
                new PersonItem("Lisa", "Simpson", 13),
                new PersonItem("Marge", "Simpson", 10),
            };
            dgv.DataSource = persons;
            dgv.ContextMenuStrip.Items.AddRange(
                new ToolStripItem[]
                {
                    new CopyRowCommand(dgv, persons), 
                    new PasteRowsCommand(dgv, persons), 
                    new CutRowsCommand(dgv, persons), 
                });

            var menu = form.dataGridView1.ContextMenuStrip;

            dgv.Rows[0].Selected = true;

            var command = menu.Items.Cast<ToolStripItem>().FirstOrDefault(n => n.Text == @"Copy");
            if(command!= null)
                command.PerformClick();

            dgv.Rows[2].Selected = true;

            command = menu.Items.Cast<ToolStripItem>().FirstOrDefault(n => n.Text == @"Paste");
            if (command != null)
                command.PerformClick();

            Assert.AreEqual("Homer", persons[3].FirstName);
           
        }

        [Test]
        [STAThread]
        public void _0003_Minden_Elem_Kivágása_Bellesztése()
        {
            var form = new SingleGridForm();
            var dgv = form.GridView;

            var persons = new PersonCollection()
            {
                new PersonItem("Homer", "Simpson", 40),
                new PersonItem("Bart", "Simpson", 10),
                new PersonItem("Lisa", "Simpson", 13),
                new PersonItem("Marge", "Simpson", 10),
            };
            dgv.DataSource = persons;
            dgv.ContextMenuStrip.Items.AddRange(
                new ToolStripItem[]
                {
                    new CopyRowCommand(dgv, persons), 
                    new PasteRowsCommand(dgv, persons), 
                    new CutRowsCommand(dgv, persons), 
                });

            var menu = form.dataGridView1.ContextMenuStrip;
            for (int i = 0; i < dgv.Rows.Count; i++)
                dgv.Rows[i].Selected = true;

            var command = menu.Items.Cast<ToolStripItem>().FirstOrDefault(n => n.Text == @"Cut");
            if(command!= null)
                command.PerformClick();

            Assert.AreEqual(0, persons.Count);

            command = menu.Items.Cast<ToolStripItem>().FirstOrDefault(n => n.Text == @"Paste");
            if (command != null)
                command.PerformClick();

            Assert.AreEqual(4, persons.Count);

        }
    }
}
