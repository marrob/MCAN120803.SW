
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.TreeNodes
{
    using System;
    using System.Windows.Forms; /*TreeNode*/
    
    using Model;
    using Services; /*TimerService*/

    public class MsgDeltaMinTimeTreeNode : TreeNode
    {
        private readonly MessageStatisticsItem _message;

        public MsgDeltaMinTimeTreeNode(MessageStatisticsItem message)
        {
            _message = message;
            Name = "deltaMinTimeTreeViewItem1";
            Text = @"dtMin: " + (message.DeltaMinTime.HasValue ? message.DeltaMinTime.ToString() : AppConstants.ValueNotAvailable2) + @" ms";
            SelectedImageKey = ImageKey = @"stats_min";
            TimerService.Instance.Tick += new EventHandler(Timer_Tick);
            message.DefaultStateComplete += (o, e) => Timer_Tick(null, EventArgs.Empty);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Text = @"dtMin: " + (_message.DeltaMinTime.HasValue ? _message.DeltaMinTime.ToString() : AppConstants.ValueNotAvailable2) + @" ms";
        }
    }
}
