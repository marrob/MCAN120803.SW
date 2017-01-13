


namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.TreeNodes
{
    using System;
    using System.Windows.Forms; /*TreeNode*/

    using Model;
    using Services; /*Culture*/
    using Converters; /*DataFrameConverter*/
    using DataStorage;
    using WinForms.Framework;

    public class MsgDataTreeNode : TreeNode
    {
        private readonly MessageStatisticsItem _message;

        public MsgDataTreeNode(MessageStatisticsItem message)
        {
            _message = message;
            Name = "dataTreeViewItem1";
            Text = CultureService.Instance.GetString(CultureText.node_Data_Text) + @": " + new DataFrameConverter().ConvertTo(message.Data, typeof(string));
            SelectedImageKey = ImageKey = @"data16";

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e1 =>
            {
                if (e1.Details.DataObjects == DataObjects.ParameterProperty &&
                    e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.DataFormat))
                {
                    if (message.Data != null)
                        Text = CultureService.Instance.GetString(CultureText.node_Data_Text) + @": " + new DataFrameConverter().ConvertTo(message.Data, typeof(string));
                    else
                        Text = AppConstants.ValueNotAvailable2;
                }
            });

            TimerService.Instance.Tick += new EventHandler(Timer_Tick);
            message.DefaultStateComplete += (o, e) => Timer_Tick(null, EventArgs.Empty);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_message.Data != null)
                Text = CultureService.Instance.GetString(CultureText.node_Data_Text) + @": " + new DataFrameConverter().ConvertTo(_message.Data, typeof(string));
            else
                Text = AppConstants.ValueNotAvailable2;
        }
    }
}
