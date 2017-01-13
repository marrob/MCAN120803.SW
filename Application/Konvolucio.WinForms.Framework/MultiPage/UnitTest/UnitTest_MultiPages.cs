// -----------------------------------------------------------------------
// <copyright file="UnitTest_MultiPages.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework.MultiPage.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Annotations;
    using Commands;
    using NUnit.Framework;
    using TreeNodes;
    using View;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class UnitTest_MultiPages
    {
        [Test]
        public void _0001()
        {
            Form1 form = new Form1();

            /*Page tároló létrehozása*/
            var pages = form.KnvMultiPageView1.Pages;

            var pageA = new UserControlA();
            var pageB = new UserControlB();
            var pageC = new UserControlC();
            var pageD = new UserControlD();

            /*Page hozzáadása listáhaoz.*/
            pages.Add("A", pageA, null);
            pages.Add("B", pageB, null);
            pages.Add("C", pageC, null);
            pages.Add("D", pageD, null);

            /*Page-eket megjelnítő összekötése a listával.*/


            /*Annyi Pagnek kell lennie mint amennyit hozzáadotunk.*/
            Assert.AreEqual(4, form.KnvMultiPageView1.Pages.Count, "Nem annyi page van mint kellne...");

            /*Annyi Nyomógombnak kell lennie mint amennyit hozzáadotunk.*/
            Assert.AreEqual(4, form.KnvMultiPageView1.StripView.PageButtons.Length, "Nem annyi nyomógomb van mint kellne.");

            /*Az utoljára hozzáadott nyomógombnak kell, hogy kiválasztva legyen.*/
            Assert.AreEqual(false, form.KnvMultiPageView1.StripView.PageButtons[0].Checked);
            Assert.AreEqual(false, form.KnvMultiPageView1.StripView.PageButtons[1].Checked);
            Assert.AreEqual(false, form.KnvMultiPageView1.StripView.PageButtons[2].Checked);
            Assert.AreEqual(true, form.KnvMultiPageView1.StripView.PageButtons[3].Checked);
        }

        [Test]
        public void _0002()
        {
            Form1 form = new Form1();

            /*Page tároló létrehozása*/
            var pages = form.KnvMultiPageView1.Pages;

            var pageA = new UserControlA();
            var pageB = new UserControlB();
            var pageC = new UserControlC();
            var pageD = new UserControlD();

            /*Page hozzáadása listáhaoz.*/
            pages.Add("A", pageA, null);
            pages.Add("B", pageB, null);
            pages.Add("C", pageC, null);
            pages.Add("D", pageD, null);

            /*Annyi Pagnek kell lennie mint amennyit hozzáadotunk.*/
            Assert.AreEqual(4, form.KnvMultiPageView1.Pages.Count, "Nem annyi page van mint kellne...");

            /*Annyi Nyomógombnak kell lennie mint amennyit hozzáadotunk.*/
            Assert.AreEqual(4, form.KnvMultiPageView1.StripView.PageButtons.Length, "Nem annyi nyomógomb van mint kellne.");

            /*Az utoljára hozzáadott nyomógombnak kell, hogy kiválasztva legyen.*/
            Assert.AreEqual(false, form.KnvMultiPageView1.StripView.PageButtons[0].Checked);
            Assert.AreEqual(false, form.KnvMultiPageView1.StripView.PageButtons[1].Checked);
            Assert.AreEqual(false, form.KnvMultiPageView1.StripView.PageButtons[2].Checked);
            Assert.AreEqual(true, form.KnvMultiPageView1.StripView.PageButtons[3].Checked);

            /*Az utoljára hozzáadott page látszódik.*/
            Assert.AreEqual(pageD, form.KnvMultiPageView1.CurrentControl);

            /*Minden page-hez egy nyomógomb tartozik.*/
            Assert.AreEqual(4, form.KnvMultiPageView1.StripView.PageButtons.Length, "nem a várt mennyiségü nyomgomb látható.");

            /*Egy page törlése.*/
            pages.Remove("B");

            /*Nem a törölt page volt kiválasztva, minden marad a régiben.*/
            Assert.AreEqual(false, form.KnvMultiPageView1.StripView.PageButtons[0].Checked);
            Assert.AreEqual(false, form.KnvMultiPageView1.StripView.PageButtons[1].Checked);
            Assert.AreEqual(true, form.KnvMultiPageView1.StripView.PageButtons[2].Checked);

            /*Az utoljára hozzáadott page látszódik.*/
            Assert.AreEqual(pageD, form.KnvMultiPageView1.CurrentControl);

            /*Minden page-hez egy nyomógomb tartozik.*/
            Assert.AreEqual(3, form.KnvMultiPageView1.StripView.PageButtons.Length, "nem a várt mennyiségü nyomgomb látható.");
        }

        [Test]
        public void _0003()
        {
            Form1 form = new Form1();
            /*Page tároló létrehozása*/
            var pages = form.KnvMultiPageView1.Pages;

            var pageA = new UserControlA();
            var pageB = new UserControlB();
            var pageC = new UserControlC();
            var pageD = new UserControlD();

            pages.Add("A", pageA, null);
            pages.Add("B", pageB, null);
            pages.Add("C", pageC, null);
            pages.Add("D", pageD, null);

            /*Kiválasztok egy Page-t*/
            pages.CurrentPage = pages["C"];

            /*Csak a hozzá tartozó nyomógomb Jelez*/
            Assert.AreEqual(false, form.KnvMultiPageView1.StripView.PageButtons[0].Checked);
            Assert.AreEqual(false, form.KnvMultiPageView1.StripView.PageButtons[1].Checked);
            Assert.AreEqual(true, form.KnvMultiPageView1.StripView.PageButtons[2].Checked);
            Assert.AreEqual(false, form.KnvMultiPageView1.StripView.PageButtons[3].Checked);

            /*Hozzá tartozó Page látszik*/
            Assert.AreEqual(pageC, form.KnvMultiPageView1.CurrentControl);

        }

        [Test]
        public void _0004()
        {
            var x1 = new MultiPageItem("A", null, null);
            var x2 = new MultiPageItem("A", null, null);
            Assert.IsTrue(x1.Equals(x2));
            Assert.AreEqual(x1, x2);
        }


        [Test]
        public void _0005()
        {
            var x1 = new MultiPageItem("A", null, null);
            var x2 = new MultiPageItem("A", null, null);
            var x3 = new MultiPageItem("B", null, null);
            Assert.IsTrue(x1 == x2);
            Assert.IsTrue(x3 != x2);

        }

        [Test]
        public void _0003_Dialog()
        {
            Form1 form = new Form1();


            var pages = form.KnvMultiPageView1.Pages;

            var pageA = new UserControlA();
            var pageB = new UserControlB();
            var pageC = new UserControlC();
            var pageD = new UserControlD();

            pages.Add("A", pageA, null);
            pages.Add("B", pageB, null);
            pages.Add("C", pageC, null);
            pages.Add("D", pageD, null);

            form.treeView1.HideSelection = false;
            form.treeView1.ContextMenuStrip = new ContextMenuStrip();
            form.treeView1.ContextMenuStrip.Items.AddRange(
                    new ToolStripItem[]
                    {
                       new NewNodeCommand(form.treeView1 ,pages),
                       new DeleteNodeCommand(form.treeView1, pages),
                       new RenameNodeCommand(form.treeView1, pages),
                       new DeleteAllNodeCommand(form.treeView1, pages)
                    });

            form.treeView1.Nodes.Add(new TopTreeNode(pages));

            form.treeView1.NodeMouseClick += (o, e) =>
            {
                pages.ClickOnNodeHandler(e.Node);
            };

            pages.ListChanged += (o, e) =>
            {
                if (e.ListChangedType == ListChangedType.ItemChanged)
                {
                    if (e.PropertyDescriptor != null)
                    {
                        if (e.PropertyDescriptor.DisplayName == "Name")
                        {
                            var bindingList = o as IBindingList;
                            if (bindingList != null)
                            {
                                var item = bindingList[e.NewIndex];
                                var pageItem = item as MultiPageItem;
                                if (pageItem != null)
                                {
                                    var demoPage = pageItem.PageControl as DemoPage;
                                    if(demoPage != null)
                                        demoPage.TextX = e.PropertyDescriptor.GetValue(item) as string;
                                }

                            }

                        }

                    }

                }

            };

            
            form.ShowDialog();

            form.Dispose();
        }

    }

}
