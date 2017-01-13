
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.TreeNodes
{
    using System.Linq;
    using System.ComponentModel;
    using System.Windows.Forms; /*TreeNode*/

    using Model;
    using Services;
    using Adapters;
    using Common;
    using DataStorage;
    using WinForms.Framework; /*PropertyPlus*/

    public class MessagesTreeNode : TreeNode
    {
        private readonly MessageStatistics _statistics;
        private readonly IAdapterService _adapter;
        private readonly ProjectParameters _parameters;

        public MessagesTreeNode(MessageStatistics statistics, IAdapterService adapter, ProjectParameters parameters)
        {
            _statistics = statistics;
            _adapter = adapter;
            _parameters = parameters;
            Text = CultureService.Instance.GetString(CultureText.node_MessagesStatistics_Text);
            SelectedImageKey = ImageKey = @"mails16";


            EventAggregator.Instance.Subscribe<StorageAppEvent>(e1 =>
            {
                switch (e1.ChangingType)
                {
                    case FileChangingType.LoadComplete:
                        {
                            if (e1.Storage.Parameters.MessageStatisticsEnabled)
                            {
                                Text = CultureService.Instance.GetString(CultureText.node_MessagesStatistics_Text);
                            }
                            else
                            {
                                Text = CultureService.Instance.GetString(CultureText.node_MessagesStatistics_Text);
                                Text += string.Format(" [{0}] ", CultureService.Instance.GetString(CultureText.text_DISABLED));
                            }
                            break;
                        }
                    case FileChangingType.ContentChanged:
                        {
                            if (e1.Details.DataObjects == DataObjects.ParameterProperty &&
                                e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.MessageStatisticsEnabled))
                            {
                                if (e1.Storage.Parameters.MessageStatisticsEnabled)
                                {
                                    Text = CultureService.Instance.GetString(CultureText.node_MessagesStatistics_Text);
                                }
                                else
                                {
                                    Text = CultureService.Instance.GetString(CultureText.node_MessagesStatistics_Text);
                                    Text += string.Format(" [{0}] ", CultureService.Instance.GetString(CultureText.text_DISABLED));
                                }
                            }
                            break;
                        };
                }
            });

            _statistics.Messages.ListChanged += new ListChangedEventHandler(Messages_ListChanged);
        }

        /// <summary>
        /// Az üzentek csompontja alá szurja be és törli
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Messages_ListChanged(object sender, ListChangedEventArgs e)
        {
            /*Új üzenet került a statisztiába, ez futás időben töréntik*/
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                var message = _statistics.Messages[e.NewIndex];
                var messageTreeItem = new TreeNode[]
                {
                    new MsgNameTreeNode(message,  
                        new TreeNode[]
                            {
                                new MsgArbitrationIdTreeNode(message),
                                new MsgDirectionTreeNode(message),
                                new MsgTypeTreeNode(message),
                                new MsgCountTreeNode(message),
                                new MsgPeriodTimeTreeNode(message, 
                                    new TreeNode[]
                                        {
                                            new MsgDeltaMinTimeTreeNode(message),
                                            new MsgDeltaMaxTimeTreeNode(message),
                                        }),
                                new MsgLastTreeNode(
                                    new TreeNode[]
                                        {
                                            new MsgUpdateTreeNode(message, _parameters),
                                            new MsgDataTreeNode(message),
                                        }),
                            }
                       ),
                };

                Nodes.AddRange(messageTreeItem.ToArray());

            }
            /*List.Clear*/
            if (e.ListChangedType == ListChangedType.Reset)
            {
                /*Törli a MsgNamTreeNode típusó Node-okat*/
                var items = Nodes.Cast<TreeNode>().Where((e1) => { return (e1 is MsgNameTreeNode); }).ToList();
                //if (items != null)
                    foreach (var item in items)
                        Nodes.Remove(item);
            }
        }
    }
}
