
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.TreeNodes
{
    using Model;
    using Services;
    using System.Windows.Forms; /*TreeNode*/

    using Converters;
    using DataStorage;
    using WinForms.Framework; /*PropertyPlus*/

    public class MsgNameTreeNode : TreeNode
    {
        public MessageStatisticsItem Message { get; private set; }

        public MsgNameTreeNode(MessageStatisticsItem message, TreeNode[] subNodes)
        {
            Message = message;
            Name = "nameTreeViewItem1";
            Nodes.AddRange(subNodes);
            SelectedImageKey = ImageKey = @"Mail_16x16";

            var arbid = new ArbitrationIdConverter().ConvertTo(message.ArbitrationId, typeof(string)) as string;
            if (string.IsNullOrEmpty(message.Name))
            {
                /*Message: No Name [{0}]*/
                Text = string.Format(CultureService.Instance.GetString(CultureText.node_MessageNoNameArbId_Text), arbid);
            }
            else
            {
                /*Message: {0} [{1}]*/
                Text = string.Format(CultureService.Instance.GetString(CultureText.node_MessageNameArbId_Text), message.Name, arbid);
            }

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e1 =>
            {
                if (e1.Details.DataObjects == DataObjects.ParameterProperty &&
                    e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.ArbitrationIdFormat))
                {
                    arbid = new ArbitrationIdConverter().ConvertTo(message.ArbitrationId, typeof(string)) as string;
                    if (string.IsNullOrEmpty(message.Name))
                    {
                        /*Message: No Name [{0}]*/
                        Text = string.Format(CultureService.Instance.GetString(CultureText.node_MessageNoNameArbId_Text), arbid);
                    }
                    else
                    {
                        /*Message: {0} [{1}]*/
                        Text = string.Format(CultureService.Instance.GetString(CultureText.node_MessageNameArbId_Text), message.Name, arbid);
                    }
                }
            });
        }
    }
}
