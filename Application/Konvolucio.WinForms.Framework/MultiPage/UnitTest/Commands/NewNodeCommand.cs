// -----------------------------------------------------------------------
// <copyright file="NewTableNodeCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework.MultiPage.UnitTest.Commands
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Framework;
    using TreeNodes;
    using View;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class NewNodeCommand : ToolStripMenuItem
    {
        private readonly MultiPageCollection _collection;
        private readonly TreeView _treeView;

        public NewNodeCommand(TreeView treeView, MultiPageCollection collection)
        {

            Text = @"New";
            ShortcutKeys = Keys.Alt | Keys.N;
            _collection = collection;
            _treeView = treeView;

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

        protected virtual void Command()
        {
            INewForm rf = new NewForm();
            if (rf.ShowDialog() == DialogResult.OK)
            {
                var newItem = new DemoPage()
                {
                    TextX = rf.NewName,
                };
                _collection.Add( rf.NewName, newItem,null);
            }
        }
    }
}
