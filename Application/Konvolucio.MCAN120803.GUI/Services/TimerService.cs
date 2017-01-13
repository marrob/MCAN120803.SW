// -----------------------------------------------------------------------
// <copyright file="TimerService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Konvolucio.MCAN120803.GUI.UnitTest;

namespace Konvolucio.MCAN120803.GUI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms; /*TreeNode*/


    public interface ITimerService :IDisposable
    {
        event EventHandler Tick;
        
        int Interval { get; set; }
        void Start();
        void Stop();
    }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TimerService: ITimerService
    {
        //public event EventHandler Tick
        //{
        //    add {_timer.Tick += value; }
        //    remove {_timer.Tick -= value; }
        //}

        public event EventHandler Tick;

        public int Interval
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }



        public static ITimerService Instance { get { return _instance; } }

        private static readonly TimerService _instance = new TimerService();

        private readonly Timer _timer;

        public TimerService()
        {
            _timer = new Timer();
            _timer.Tick += Update;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        void Update(object sender, EventArgs e)
        {
            if(Tick!=null)
                Tick(this,EventArgs.Empty);
        }

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }
        }
    }
}
