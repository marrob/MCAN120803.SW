// -----------------------------------------------------------------------
// <copyright file="LogTopTreeView.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework.MultiPage.UnitTest.TreeNodes
{
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TopTreeNode : TreeNode
    {
        private readonly MultiPageCollection _pages;

        public TopTreeNode(MultiPageCollection pages)
        {
            Text = @"TopNode";
            _pages = pages;
            pages.ListChanged += new ListChangedEventHandler(Page_ListChanged);
            pages.ListChanging += new ListChangingEventHandler<MultiPageItem>(Page_ListChanging);
            pages.CurrentPageChanged += new System.EventHandler(pages_CurrentPageChanged);

            Nodes.Clear();

            foreach (var item in pages)
                Nodes.Add(item.PageNode);
        }

        void pages_CurrentPageChanged(object sender, System.EventArgs e)
        {
            TreeView.SelectedNode = _pages.CurrentPage.PageNode;
        }

        private void Page_ListChanging(object sender, ListChangingEventArgs<MultiPageItem> e)
        {
            if (e.ListChangedType == ListChangingType.ItemRemoving)
            {
                var node = Nodes.Cast<TreeNode>().FirstOrDefault((n) => Equals(e.Item, n.Tag));
                if (node != null)
                    Nodes.Remove(node);
            }

            if (e.ListChangedType == ListChangingType.Clearing)
                Nodes.Clear();
        }

        private void Page_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
                Nodes.Add(_pages[e.NewIndex].PageNode);
        }
    }
}
