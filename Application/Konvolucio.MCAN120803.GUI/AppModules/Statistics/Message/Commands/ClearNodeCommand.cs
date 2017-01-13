
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.Commands
{
    using System.Windows.Forms;/*ToolStripMenuItem, TreeNode*/
    using Model;

    using Properties;
    using Services;
    using TreeNodes;

    internal sealed class ClearNodeCommand : ToolStripMenuItem
    {
        private readonly MessageStatistics _statistics;

        public ClearNodeCommand(MessageStatistics statistics)
        {
            Image = Resources.Delete32x32;
            Text = CultureService.Instance.GetString(CultureText.menuItem_Reset_Text);
            _statistics = statistics;

            EventAggregator.Instance.Subscribe<TreeViewSelectionChangedAppEvent>(e =>
            {
                Visible = (e.SelectedNode is MessagesTreeNode);
            });


        }

        protected override void OnClick(System.EventArgs e)
        {
            base.OnClick(e);
            _statistics.Clear();
        }
    }
}
