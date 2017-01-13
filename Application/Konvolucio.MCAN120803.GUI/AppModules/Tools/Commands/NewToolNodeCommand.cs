// -----------------------------------------------------------------------
// <copyright file="NewTableNodeCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Tools.Commands
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Common;
    using Model;
    using Properties;
    using Services;
    using TreeNodes;
    using View;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class NewToolNodeCommand : ToolStripMenuItem
    {
        private readonly ToolTableCollection _table;
        private readonly TreeView _treeView;

        public NewToolNodeCommand(TreeView treeView, ToolTableCollection table)
        {
            Text = @"New";
            ShortcutKeys = Keys.Alt | Keys.N;
            Image = Resources.New_24x24;
            _table = table;
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

        private void Command()
        {
            INewForm rf = new NewForm();
            rf.Types = Enum.GetNames(typeof (ToolTypes));
            rf.SelectedType = ToolTypes.CAN;
            if (rf.ShowDialog() == DialogResult.OK)
            {

                if (_table.FirstOrDefault(n => n.Name == rf.NewName) != null)
                {
                    MessageBox.Show(CultureService.Instance.GetString(CultureText.text_AlreadyExists), Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(rf.NewName) || string.IsNullOrEmpty(rf.NewName))
                {
                    MessageBox.Show(CultureService.Instance.GetString(CultureText.text_TheFieldCantBeEmtpy), Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    _table.Add(new ToolTableItem(rf.NewName, rf.SelectedType));
                }
            }
        }
    }
}