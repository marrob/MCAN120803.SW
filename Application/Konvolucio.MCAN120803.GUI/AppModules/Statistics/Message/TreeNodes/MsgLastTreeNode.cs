
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.TreeNodes
{
    using System.Windows.Forms; /*TreeNode*/

    using Services; /*Culture*/

    public class MsgLastTreeNode : TreeNode
    {
        public MsgLastTreeNode(TreeNode[] subNodes)
        {
            Name = @"lastTreeViewItem1";
            Nodes.AddRange(subNodes);
            Text = CultureService.Instance.GetString(CultureText.node_Last_Text);
            SelectedImageKey = ImageKey = @"Refresh_16x16";
        }
    }
}
