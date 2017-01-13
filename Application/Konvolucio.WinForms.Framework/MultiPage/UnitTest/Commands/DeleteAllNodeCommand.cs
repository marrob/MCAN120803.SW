
namespace Konvolucio.WinForms.Framework.MultiPage.UnitTest.Commands
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using TreeNodes;

    internal sealed class DeleteAllNodeCommand : ToolStripMenuItem
    {
        private readonly MultiPageCollection _collection;
        private readonly TreeView _treeView;

        public DeleteAllNodeCommand(TreeView treeView, MultiPageCollection collection)
        {
            Text = @"Delete All";
            _treeView = treeView;
            _collection = collection;
            treeView.ContextMenuStrip.Opening += new CancelEventHandler(ContextMenuStrip_Opening);
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            Visible = _treeView.SelectedNode is TopTreeNode;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (Enabled)
                Command();
        }

        private void Command()
        {
            lock (_collection.SyncRoot)
            {
                try
                {
                    _collection.Clear();
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }
    }
}
