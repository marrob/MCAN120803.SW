
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Adapter.TreeNodes
{
    using System.Windows.Forms;/*TreeNode*/
    using DataStorage;
    using Model;

    using Services;
    using WinForms.Framework;

/*Culture*/

    public class AdapterStatisticsTreeNode : TreeNode
    {
        public AdapterStatisticsTreeNode(IAdapterStatistics statistics)
        {
            Text = CultureService.Instance.GetString(CultureText.node_AdapterStatistics_Text);
            SelectedImageKey = ImageKey = @"Statistics16";

            Nodes.AddRange(new TreeNode[]
            {
                new TransmittedTreeNode(
                    new TreeNode[]
                    {
                        new TotalTreeNode(statistics.Transmitted),
                        new DropTreeNode(statistics.Transmitted),
                        new PendingTreeNode(statistics.Transmitted),
                        new ErrorTreeNode(statistics.Transmitted)
                    }),
                new ReceivedTreeNode(
                    new TreeNode[]
                    {
                        new TotalTreeNode(statistics.Received),
                        new DropTreeNode(statistics.Received),
                        new PendingTreeNode(statistics.Received),
                        new ErrorTreeNode(statistics.Received)
                    })
            });



            EventAggregator.Instance.Subscribe<StorageAppEvent>(e1 =>
            {
                switch (e1.ChangingType)
                {
                    case FileChangingType.LoadComplete:
                    {
                        if (e1.Storage.Parameters.AdapterStatisticsEnabled)
                        {
                            Text = CultureService.Instance.GetString(CultureText.node_AdapterStatistics_Text);
                        }
                        else
                        {
                            Text = CultureService.Instance.GetString(CultureText.node_AdapterStatistics_Text);
                            Text += string.Format(" [{0}] ", CultureService.Instance.GetString(CultureText.text_DISABLED));
                        }
                        break;
                    }

                    case FileChangingType.ContentChanged:
                    {
                        if (e1.Details.DataObjects == DataObjects.ParameterProperty &&
                            e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.AdapterStatisticsEnabled))
                        {
                            if (e1.Storage.Parameters.AdapterStatisticsEnabled)
                            {
                                Text = CultureService.Instance.GetString(CultureText.node_AdapterStatistics_Text);
                            }
                            else
                            {
                                Text = CultureService.Instance.GetString(CultureText.node_AdapterStatistics_Text);
                                Text += string.Format(" [{0}] ", CultureService.Instance.GetString(CultureText.text_DISABLED));
                            }
                        }
                        break;
                    }

                }
            });
        }
    }
}
