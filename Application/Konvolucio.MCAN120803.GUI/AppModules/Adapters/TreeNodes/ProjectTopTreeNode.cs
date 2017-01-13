
namespace Konvolucio.MCAN120803.GUI.AppModules.Adapters.TreeNodes
{
    using WinForms.Framework;
    using System.Windows.Forms; /*TreeNode*/
    using DataStorage;
    using Services;

    internal sealed class ProjectTopTreeNode : TreeNode
    {
        public ProjectTopTreeNode(TreeNode[] subNodes)
        {
            Nodes.AddRange(subNodes);
            Text = @"CP1540";
            SelectedImageKey = ImageKey = @"Statistics16";
          

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                if(e.ChangingType == FileChangingType.LoadComplete)
                    Text = e.Storage.FileName;
            });

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                if (e.Details.DataObjects == DataObjects.ParameterProperty &&
                  e.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e.Storage.Parameters.Comment))
                {
                    ToolTipText = e.Storage.Parameters.Comment;
                }

            });
           
        }
    }
}
