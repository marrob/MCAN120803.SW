
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.TreeNodes
{
    using System;
    using System.Windows.Forms; /*TreeNode*/
    
    using Model;
    using Services; /*TimerService*/

    public class MsgCountTreeNode : TreeNode
    {
        private readonly MessageStatisticsItem _message;

        public MsgCountTreeNode(MessageStatisticsItem message)
        {
            _message = message;
            Name = "countTreeViewItem1";
            Text = CultureService.Instance.GetString(CultureText.node_Count_Text) + @": " + message.Count.ToString();
            SelectedImageKey = ImageKey = @"counter16";

            TimerService.Instance.Tick += new EventHandler(Timer_Tick);
            message.DefaultStateComplete += (o, e) => Timer_Tick(null, EventArgs.Empty);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Text = CultureService.Instance.GetString(CultureText.node_Count_Text) + @": " + _message.Count.ToString();
        }
    }
}
