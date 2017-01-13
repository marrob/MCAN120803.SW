
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Adapter.Model
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;

    public interface IAdapterStatistics
    {
        event EventHandler Reseted;
        IDircectionStatistics Transmitted { get; }
        IDircectionStatistics Received { get; }
        void UpdateTransmitted(long total, long drop, long error, long pending);
        void UpdateReceived(long total, long drop, long error, long pending);
        void Reset();
    }

    public class AdapterStatistics : IAdapterStatistics
    {
        public event EventHandler Reseted;
        public IDircectionStatistics Transmitted { get { return _transmitted; } }
        readonly IDircectionStatistics _transmitted;
        public IDircectionStatistics Received { get { return _received; } }
        readonly IDircectionStatistics _received;

        public AdapterStatistics()
        {
            _transmitted = new DirectionStatistics();
            _received = new DirectionStatistics();
        }

        public void UpdateTransmitted(long total, long drop, long error, long pending)
        {
            _transmitted.Total = total;
            _transmitted.Drop = drop;
            _transmitted.Error = error;
            _transmitted.Pending = pending;
        }

        public void UpdateReceived(long total, long drop, long error, long pending)
        {
            _received.Total = total;
            _received.Drop = drop;
            _received.Error = error;
            _received.Pending = pending;
        }

        public void Reset()
        {
            _transmitted.Reset();
            _received.Reset();

            if (Reseted != null)
                Reseted(this, EventArgs.Empty);
        }
    }
}
