
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Adapter.TreeNodes
{
    using System;
    using System.Windows.Forms; /*TreeNode*/
   
    using Model;
    using Services;             /*Culture,TimerService*/

    internal sealed class DropTreeNode : TreeNode
    {
        readonly IDircectionStatistics _direction;

        public DropTreeNode(IDircectionStatistics direction)
        {
            _direction = direction;

            if (direction.Drop.HasValue)
                Text =  CultureService.Instance.GetString(CultureText.node_Drop_Text) + @": " + direction.Drop;
            else
                Text = CultureService.Instance.GetString(CultureText.node_Drop_Text) + @": " + AppConstants.ValueNotAvailable2;

            SelectedImageKey = ImageKey = @"trash16";

            TimerService.Instance.Tick += new EventHandler(Timer_Tick);

            EventAggregator.Instance.Subscribe<StopAppEvent>(e =>
            {
                Timer_Tick(this, EventArgs.Empty);  /*Leállás után még rá frissít, ez KELL!*/
            });

            EventAggregator.Instance.Subscribe<PlayAppEvent>(e =>
            {
             
                Timer_Tick(null, EventArgs.Empty);   /*Ajánlott!*/
            });

            direction.Reseted += (o, e) =>
            {
                Timer_Tick(this, EventArgs.Empty);
            };
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            if (_direction.Drop.HasValue)
                Text = CultureService.Instance.GetString(CultureText.node_Drop_Text) + @": " + _direction.Drop;
            else
                Text = CultureService.Instance.GetString(CultureText.node_Drop_Text) + @": " + AppConstants.ValueNotAvailable2;
        }
    }
}
