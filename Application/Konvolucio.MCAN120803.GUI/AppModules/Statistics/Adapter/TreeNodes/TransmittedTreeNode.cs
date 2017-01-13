
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Adapter.TreeNodes
{
    using System.Windows.Forms; /*TreeNode*/
    using Services;             /*Culture*/

    internal sealed class TransmittedTreeNode : TreeNode
    {
        public TransmittedTreeNode(TreeNode[] subNodes)
        {
            Text = CultureService.Instance.GetString(CultureText.node_Transmitted_Text);
            Nodes.AddRange(subNodes);
            SelectedImageKey = ImageKey = @"arrow_up";
        }
    }
}
