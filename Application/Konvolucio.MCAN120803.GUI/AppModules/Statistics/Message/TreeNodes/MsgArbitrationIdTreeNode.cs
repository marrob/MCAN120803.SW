
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.TreeNodes
{
    using System.Windows.Forms;     /*TreeNode*/

    using WinForms.Framework;
    using Model;
    using Services;
    using Converters;
    using DataStorage;

    public class MsgArbitrationIdTreeNode : TreeNode
    {
        public MsgArbitrationIdTreeNode(MessageStatisticsItem message)
        {
            SelectedImageKey = ImageKey = @"id16";
            /*Arbitration Id: [{0}]*/
            Text = string.Format(CultureService.Instance.GetString(CultureText.node_ArbitrationId_Text), new ArbitrationIdConverter().ConvertTo(message.ArbitrationId, typeof(string)));
     
            EventAggregator.Instance.Subscribe<StorageAppEvent>(e1 =>
            {
                if (e1.Details.DataObjects == DataObjects.ParameterProperty &&
                    e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.ArbitrationIdFormat))
                {
                    Text = string.Format(CultureService.Instance.GetString(CultureText.node_ArbitrationId_Text), new ArbitrationIdConverter().ConvertTo(message.ArbitrationId, typeof(string)));
                }
            });
        }
    }
}
