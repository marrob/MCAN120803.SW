
namespace Konvolucio.MCAN120803.GUI.AppModules.Main.Commands
{
    using System;
    using System.Windows.Forms; /*ToolStripMenuItem*/

    using Properties;
    using WinForms.Framework;
    using Services;

    /// <summary>
    /// Összes szekció bezárása
    /// </summary>
    internal sealed class CollapseAllSectionCommand : ToolStripMenuItem
    {
        private TreeNode _node;

        public CollapseAllSectionCommand()
        {
            Image = Resources.treeview_collapse;
            Text = CultureService.Instance.GetString(CultureText.menuItem_CollapseAllSection_Text);
            EventAggregator.Instance.Subscribe<TreeViewSelectionChangedAppEvent>(e =>
            {
                _node = e.SelectedNode;
                if (_node.IsExpanded)
                {
                    /*Össze van csukva akkor megjelenik az "Összes szekció bezárása"*/
                    Visible = true;
                }
                else
                {
                    Visible = false;

                }
            });
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _node.Collapse();
        }
    }
}
