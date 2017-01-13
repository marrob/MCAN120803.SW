
namespace Konvolucio.MCAN120803.GUI.AppModules.Tools.Commands
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Model;
    using Properties;
    using WinForms.Framework;

    internal sealed class DeleteToolNodeCommand : ToolStripMenuItem
    {
        private readonly ToolTableCollection  _table;
        private readonly TreeView _treeView;

        public DeleteToolNodeCommand(TreeView treeView, ToolTableCollection table)
        {
            Text = @"Delete";
            Image = Resources.Delete_24x24;
            _treeView = treeView;
            _table = table;
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
            //lock (_table.SyncRoot)
            //{
                try
                {
                    _table.Remove(_table.First(n=>n.Name == _treeView.SelectedNode.Text));
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            //}
        }
    }
}
