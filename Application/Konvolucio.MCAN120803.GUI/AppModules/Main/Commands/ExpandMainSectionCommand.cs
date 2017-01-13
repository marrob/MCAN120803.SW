
namespace Konvolucio.MCAN120803.GUI.AppModules.Main.Commands
{
    using System;
    using System.Windows.Forms; /*ToolStripMenuItem,TreeNode*/
    
    using Properties;
    using Services;

    /// <summary>
    /// Fő szekció kibontása
    /// </summary>
    internal sealed class ExpandMainSectionCommand : ToolStripMenuItem
    {
        private TreeNode _node;

        public ExpandMainSectionCommand()
        {
            Text = CultureService.Instance.GetString(CultureText.menuItem_ExpandMainSection_Text);
            Image = Resources.treeView_expand;
 
            EventAggregator.Instance.Subscribe<TreeViewSelectionChangedAppEvent>(e =>
            {
                _node = e.SelectedNode;
                if (!_node.IsExpanded && _node.Nodes.Count != 0)
                {
                    /*Nincs kinyitva és van leglább egy gyereke, akkor megjelenik az "Fő szekció kibontása" */
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
            _node.Expand();
        }
    }
}
