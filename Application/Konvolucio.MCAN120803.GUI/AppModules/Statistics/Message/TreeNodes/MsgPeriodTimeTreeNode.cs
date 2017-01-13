
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.TreeNodes
{
    using System;
    using System.Windows.Forms; /*TreeNode*/

    using Model;
    using Services; /*Culture*/

    public class MsgPeriodTimeTreeNode : TreeNode
    {
        readonly MessageStatisticsItem _messge;

        public MsgPeriodTimeTreeNode(MessageStatisticsItem message, TreeNode[] subNodes)
        {
            _messge = message;
            Text = CultureService.Instance.GetString(CultureText.node_PeriodTime_Text) + @": ";
            Text += (message.PeriodTime.HasValue ? message.PeriodTime.ToString() : AppConstants.ValueNotAvailable2) + @" ms";
            SelectedImageKey = ImageKey = @"l_clock";
            Nodes.AddRange(subNodes);
            TimerService.Instance.Tick += new EventHandler(Timer_Tick);
            message.DefaultStateComplete += (o, e) => Timer_Tick(null, EventArgs.Empty);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Text = CultureService.Instance.GetString(CultureText.node_PeriodTime_Text) + @": ";
            Text += (_messge.PeriodTime.HasValue ? _messge.PeriodTime.ToString() : AppConstants.ValueNotAvailable2) + @" ms";
        }
    }
}
