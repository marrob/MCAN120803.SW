
namespace Konvolucio.WinForms.Framework.MultiPage.UnitTest.Commands
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    internal sealed class DeleteNodeCommand : ToolStripMenuItem
    {
        private readonly MultiPageCollection _collection;
        private readonly TreeView _treeView;

        public DeleteNodeCommand(TreeView treeView, MultiPageCollection collection)
        {
            Text = @"Delete";
            _treeView = treeView;
            _collection = collection;
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
            lock (_collection.SyncRoot)
            {
                try
                {
                    _collection.Remove((MultiPageItem)_treeView.SelectedNode.Tag);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }
    }
}
