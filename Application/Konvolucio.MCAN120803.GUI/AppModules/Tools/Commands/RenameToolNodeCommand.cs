
namespace Konvolucio.MCAN120803.GUI.AppModules.Tools.Commands
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Model;
    using Properties;
    using View;
    using WinForms.Framework;

    internal sealed class RenameToolNodeCommand : ToolStripMenuItem
    {

        private readonly ToolTableCollection _table;
        private readonly TreeView _treeView;
        private readonly MultiPageCollection _pages;

        public RenameToolNodeCommand(TreeView treeView, ToolTableCollection table, MultiPageCollection pages)
        {
            Text = @"Rename";
            Image = Resources.rename24;
            _table = table;
            _treeView = treeView;
            _pages = pages;

            treeView.ContextMenuStrip.Opening += new CancelEventHandler(ContextMenuStrip_Opening);
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            Visible = _treeView.SelectedNode != null && _treeView.SelectedNode.Tag is MultiPageItem;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (Enabled)
                Command();
        }

        private void Command()
        {
            IRenameForm rf = new RenameForm();
            rf.NewName = _treeView.SelectedNode.Text;

            var toRenameTableItem = _table.First(n => n.Name == _treeView.SelectedNode.Text);
            var toRenamePageItem = _pages.First(n => n.Name == _treeView.SelectedNode.Text);

            if (rf.ShowDialog() == DialogResult.OK)
            {
                if (toRenameTableItem != null)
                    toRenameTableItem.Name = rf.NewName;

                if (toRenamePageItem != null)
                    toRenamePageItem.Name = rf.NewName;
            }
        }
    }
}
