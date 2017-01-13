
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.TreeNodes
{
    using System;
    using System.Windows.Forms; /*TreeNode, Timer*/
   
    using Common;
    using DataStorage;
    using Model;
    using Services;/*Culture,TimerService*/
    using WinForms.Framework;


    public class MsgUpdateTreeNode : TreeNode
    {
        private readonly MessageStatisticsItem _message;
        private readonly ProjectParameters _parameters;

        public MsgUpdateTreeNode(MessageStatisticsItem message, ProjectParameters parameters)
        {
            _message = message;
            _parameters = parameters;
            Name = "updatedTreeViewItem1";
            Text = string.Empty;
            SelectedImageKey = ImageKey = @"watch16";
            TimerService.Instance.Tick += new EventHandler(Timer_Tick);
            EventAggregator.Instance.Subscribe<StorageAppEvent>(e1 =>
            {
                if (e1.Details.DataObjects == DataObjects.ParameterProperty &&
                    e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.TimestampFormat))
                {
                    Text = CultureService.Instance.GetString(CultureText.node_Time_Text) + @": ";

                    if (_message.Timestamp != null)
                        Text += _message.Timestamp.Value.ToString(e1.Storage.Parameters.TimestampFormat, System.Globalization.CultureInfo.InvariantCulture);
                    else
                        Text += AppConstants.ValueNotAvailable2;
                }
            });
            message.DefaultStateComplete += (o, e) => Timer_Tick(null, EventArgs.Empty);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Text = CultureService.Instance.GetString(CultureText.node_Time_Text) + @": ";
            if (_message.Timestamp != null)
                Text += _message.Timestamp.Value.ToString(_parameters.TimestampFormat, System.Globalization.CultureInfo.InvariantCulture);
            else
                Text += AppConstants.ValueNotAvailable2;
        }
    }
}
