
namespace Konvolucio.MCAN120803.GUI.AppModules.Adapters.TreeNodes
{
    using System;
    using System.Windows.Forms; /*TreeNode*/

    using WinForms.Framework;
    using Services;
    using View;

    internal sealed class AdapterTreeNode : TreeNode
    {
        public AdapterTreeNode(KnvTreeView treeView, TreeNode[] subNodes)
        {
            Nodes.AddRange(subNodes);
            Text = CultureService.Instance.GetString(CultureText.node_Adapter_Text);
            SelectedImageKey = ImageKey = @"connector16";
            

        }
    }
}
