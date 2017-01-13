
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Adapter.Commands
{
    using System.Windows.Forms;/*ToolStripMenuItem, TreeNode*/
    using Model;

    using Properties;
    using Services;
    using TreeNodes;

    internal  sealed class DefaultNodeCommand : ToolStripMenuItem
    {
       private TreeNode _node;
       private readonly IAdapterStatistics _statistics;

       public DefaultNodeCommand(IAdapterStatistics statistics)
       {
             _statistics = statistics;
            Image = Resources.Synchronize_16x16;
            Text = CultureService.Instance.GetString(CultureText.menuItem_Reset_Text);

            EventAggregator.Instance.Subscribe<TreeViewSelectionChangedAppEvent>(e =>
            {
                if (e.SelectedNode is AdapterStatisticsTreeNode)
                {
                    Visible = true;
                    _node = e.SelectedNode;
                }
                else
                {
                    Visible = false;
                }
            });
        }

        protected override void OnClick(System.EventArgs e)
        {
            base.OnClick(e);
            _statistics.Reset();
        }
    }
}
