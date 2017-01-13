
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.TreeNodes
{
    using System.Windows.Forms; /*TreeNode*/

    using Model;
    using Services; /*Culture*/

    public class MsgTypeTreeNode : TreeNode
    {
        public MsgTypeTreeNode(MessageStatisticsItem message)
        {
            Name = "typeTreeViewItem1";
            Text = CultureService.Instance.GetString(CultureText.node_Type_Text) + @": " + message.Type;
            SelectedImageKey = ImageKey = @"mail16";
        }
    }
}
