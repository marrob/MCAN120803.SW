namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Adapter.TreeNodes
{
    using System;
    using System.Windows.Forms; /*TreeNode*/
    using System.Diagnostics;   /*StopWatch*/
    
    using Model;
    using Services;             /*Culture*/

    internal sealed class TotalTreeNode : TreeNode
    {
       private readonly IDircectionStatistics _direction;
       private readonly Stopwatch _watch;

       private long _msgCountTemp = 0;
       private string _msgPerMs = string.Empty;
       private long _deltaT = 0;

        public TotalTreeNode(IDircectionStatistics direction)
        {
            _direction = direction;
            _watch = new Stopwatch();
            
            if (direction.Total.HasValue)
                Text = CultureService.Instance.GetString(CultureText.node_Total_Text) + @": " + direction.Total;
            else
                Text = CultureService.Instance.GetString(CultureText.node_Total_Text) + @": " + AppConstants.ValueNotAvailable2;
            
            SelectedImageKey = ImageKey = @"counter16";

            TimerService.Instance.Tick += new EventHandler(Timer_Tick);

            EventAggregator.Instance.Subscribe<StopAppEvent>(e =>
            {
                _watch.Stop();
                Timer_Tick(this, EventArgs.Empty);          /*Leállás után még rá frissít, ez KELL!*/
            });

            EventAggregator.Instance.Subscribe<PlayAppEvent>(e =>
            {
                Timer_Tick(null, EventArgs.Empty);           /*Ajánlott!*/
            });

            direction.Reseted += (o, e) =>
            {
                _msgCountTemp = 0;
                _msgPerMs = AppConstants.ValueNotAvailable2;
                _deltaT = 0;
                Timer_Tick(this, EventArgs.Empty);
            };
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            if (_direction.Total.HasValue)
            {
                if (!_watch.IsRunning)
                { /*Elindul*/
                    _watch.Start();
                }
                else
                {
                    _deltaT = _watch.ElapsedMilliseconds;
                    _msgPerMs = (((_direction.Total.Value - _msgCountTemp) / (double)_deltaT)*TimerService.Instance.Interval).ToString("N2");
                    _msgCountTemp = _direction.Total.Value;
                    _watch.Restart();
                }
            }

            if (_direction.Total.HasValue)
                Text = CultureService.Instance.GetString(CultureText.node_Total_Text) + @": " + _direction.Total;
            else
                Text = CultureService.Instance.GetString(CultureText.node_Total_Text) + @": " + AppConstants.ValueNotAvailable2;


            Text += @" [ " + _msgPerMs + @" msg/s ]" + @" " + @" [ deltaT: " + (_deltaT / 1000.0).ToString("N3") + @"s ]";
        }
    }
}
