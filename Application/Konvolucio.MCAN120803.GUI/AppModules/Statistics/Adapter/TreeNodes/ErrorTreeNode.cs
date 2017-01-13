
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Adapter.TreeNodes
{
    using System;
    using System.Windows.Forms; /*TreeNode*/

    using Model;
    using Services; /*Culture*/

    internal sealed class ErrorTreeNode : TreeNode
    {
        readonly IDircectionStatistics _direction;


        public ErrorTreeNode(IDircectionStatistics direction)
        {
            _direction = direction;
            
            if (direction.Error.HasValue)
                Text = CultureService.Instance.GetString(CultureText.node_Error_Text) + @": " + direction.Error;
            else
                Text = CultureService.Instance.GetString(CultureText.node_Error_Text) + @": " + AppConstants.ValueNotAvailable2;

            SelectedImageKey = ImageKey = @"Warning16";

            TimerService.Instance.Tick += new EventHandler(Timer_Tick);

            EventAggregator.Instance.Subscribe<StopAppEvent>(e =>
            {
                Timer_Tick(this, EventArgs.Empty);          /*Leállás után még rá frissít, ez KELL!*/
            });

            EventAggregator.Instance.Subscribe<PlayAppEvent>(e =>
            {
                Timer_Tick(null, EventArgs.Empty);           /*Ajánlott!*/
            });

            direction.Reseted += (o, e) =>
            {
                Timer_Tick(this, EventArgs.Empty);
            };
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            if (_direction.Error.HasValue)
                Text = CultureService.Instance.GetString(CultureText.node_Error_Text) + @": " + _direction.Error;
            else
                Text = CultureService.Instance.GetString(CultureText.node_Error_Text) + @": " + AppConstants.ValueNotAvailable2;
        }
    }
}
