
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.Commands
{
    using System.Windows.Forms;/*ToolStripMenuItem, TreeNode*/
    using Model;

    using Properties;
    using Services;
    using TreeNodes;
    using View;

    internal sealed class DefaultNodeCommand : ToolStripMenuItem
    {
        private readonly MessageStatistics _statistics;
        private readonly IStatisticsGridView _gridView;

        public DefaultNodeCommand(MessageStatistics statistics, IStatisticsGridView gridView)
        {
            Image = Resources.Synchronize_16x16;
            Text = CultureService.Instance.GetString(CultureText.menuItem_Default_Text);
            _statistics = statistics;
            _gridView = gridView;

            EventAggregator.Instance.Subscribe<TreeViewSelectionChangedAppEvent>(e =>
            {
                Visible = (e.SelectedNode is MessagesTreeNode);
            });
        }

        protected override void OnClick(System.EventArgs e)
        {
            base.OnClick(e);
            _statistics.Default();
            _gridView.Refresh();
        }
    }
}
