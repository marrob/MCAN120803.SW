

namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Adapter.Model
{
    using System;


    public interface IDircectionStatistics
    {
        event EventHandler Reseted;
        long? Total { get; set; }
        long? Pending { get; set; }
        long? Drop { get; set; }
        long? Error { get; set; }
        void Reset();
    };

    public class DirectionStatistics : IDircectionStatistics
    {
        public event EventHandler Reseted;
        public long? Total { get; set; }
        public long? Pending { get; set; }
        public long? Drop { get; set; }
        public long? Error { get; set; }

        public void Reset()
        {
            Total = null;
            Pending = null;
            Drop = null;
            Error = null;

            if (Reseted != null)
                Reseted(this, EventArgs.Empty);
        }
    }
}
