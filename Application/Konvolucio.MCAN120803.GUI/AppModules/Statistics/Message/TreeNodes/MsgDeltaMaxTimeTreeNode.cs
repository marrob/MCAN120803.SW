
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.TreeNodes
{
    using System;
    using System.Windows.Forms; /*TreeNode*/

    using Model;
    using Services; /*TimerService*/

    public class MsgDeltaMaxTimeTreeNode : TreeNode
    {
        private readonly MessageStatisticsItem _message;

        public MsgDeltaMaxTimeTreeNode(MessageStatisticsItem message)
        {
            _message = message;
            Name = "deltaMaxTimeTreeViewItem1";
            Text = @"dtMax: " + (message.DeltaMaxTime.HasValue ? message.DeltaMaxTime.ToString() : AppConstants.ValueNotAvailable2) + @" ms";
            SelectedImageKey = ImageKey = @"stats_max";
            TimerService.Instance.Tick += new EventHandler(Timer_Tick);
            message.DefaultStateComplete += (o, e) => Timer_Tick(null, EventArgs.Empty);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Text = @"dtMax: " + (_message.DeltaMaxTime.HasValue ? _message.DeltaMaxTime.ToString() : AppConstants.ValueNotAvailable2) + @" ms";
        }
    }
}
