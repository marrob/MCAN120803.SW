
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Adapter.TreeNodes
{
    using System.Windows.Forms; /*TreeNode*/
    using Services; /*Culture*/

    internal sealed class ReceivedTreeNode : TreeNode
    {
        public ReceivedTreeNode(TreeNode[] subNode)
        {
            Text = CultureService.Instance.GetString(CultureText.node_Received_Text);
            Nodes.AddRange(subNode);
            SelectedImageKey = ImageKey = @"arrow_down";
        }
    }
}
