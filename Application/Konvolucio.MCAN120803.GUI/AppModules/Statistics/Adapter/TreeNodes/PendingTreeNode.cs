
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Adapter.TreeNodes
{
    using System;
    using System.Windows.Forms; /*TreeNode*/
    
    using Model;
    using Services; /*Culture*/

    internal sealed class PendingTreeNode : TreeNode
    {
        readonly IDircectionStatistics _direction;

        public PendingTreeNode(IDircectionStatistics direction)
        {
            _direction = direction;
            Name = "dropTreeViewItem1";

            if (direction.Pending.HasValue)
                Text = CultureService.Instance.GetString(CultureText.node_Pending_Text) + ": " + direction.Pending;
            else
                Text = CultureService.Instance.GetString(CultureText.node_Pending_Text) + ": " + AppConstants.ValueNotAvailable2;

            SelectedImageKey = ImageKey = @"Sandglass16";

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
                Timer_Tick(this, null);
            };
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            if (_direction.Pending.HasValue)
                Text = CultureService.Instance.GetString(CultureText.node_Pending_Text) + ": " + _direction.Pending;
            else
                Text = CultureService.Instance.GetString(CultureText.node_Pending_Text) + ": " + AppConstants.ValueNotAvailable2;
        }
    }
}
