
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.TreeNodes
{
    using System.Windows.Forms; /*TreeNode*/

    using Model;
    using Services;             /*Culture*/

    public class MsgDirectionTreeNode : TreeNode
    {
        public MsgDirectionTreeNode(MessageStatisticsItem message)
        {
            Name = "directionTreeViewItem1";
            Text = CultureService.Instance.GetString(CultureText.node_Direction_Text) + @": " + message.Direction.ToString();
            SelectedImageKey = ImageKey = @"Sync16";
        }
    }
}
