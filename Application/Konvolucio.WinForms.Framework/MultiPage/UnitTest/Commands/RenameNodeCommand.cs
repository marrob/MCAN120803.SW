
namespace Konvolucio.WinForms.Framework.MultiPage.UnitTest.Commands
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Framework;
    using View;

    internal sealed class RenameNodeCommand : ToolStripMenuItem
    {

        private readonly MultiPageCollection _pages;
        private readonly TreeView _treeView;

        public RenameNodeCommand(TreeView treeView, MultiPageCollection pages)
        {
            Text = @"Rename";
            _pages = pages;
            _treeView = treeView;

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
            if (rf.ShowDialog() == DialogResult.OK)
            {
                var toRename = _treeView.SelectedNode.Tag as MultiPageItem;
                if(toRename!= null)
                    toRename.Name = rf.NewName;
            }
        }
    }
}
