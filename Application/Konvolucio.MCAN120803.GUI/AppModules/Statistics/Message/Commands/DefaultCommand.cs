
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.Commands
{
    using System.Windows.Forms;/*ToolStripMenuItem, TreeNode*/
    using Model;

    using Properties;
    using Services;
    using View;

    internal sealed class DefaultCommand : ToolStripMenuItem
    {
        private readonly MessageStatistics _statistics;
        private readonly IStatisticsGridView _gridView;

        public DefaultCommand(MessageStatistics statistics, IStatisticsGridView gridView)
        {
            Image = Resources.Synchronize_16x16;
            Text = CultureService.Instance.GetString(CultureText.menuItem_Default_Text);
            _statistics = statistics;
            _gridView = gridView;
        }

        protected override void OnClick(System.EventArgs e)
        {
            base.OnClick(e);
            _statistics.Default();
            _gridView.Refresh();
        }
    }
}
