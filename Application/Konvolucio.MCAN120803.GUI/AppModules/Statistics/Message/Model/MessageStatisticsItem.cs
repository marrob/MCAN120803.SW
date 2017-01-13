
namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.Model
{
    using System;
    using System.Diagnostics;
    using Common;

    public class MessageStatisticsItem 
    {
        /// <summary>
        /// Statisztika alaphelyzetbe került, ez alapján lehet frssítneni a képerenyőt.
        /// </summary>
        public event EventHandler DefaultStateComplete;

        /// <summary>
        /// Elem sorszáma, 1-es bázisú.
        /// </summary>
        public int Index { get; internal set; }
        /// <summary>
        /// Üzenet neve.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Arbitrációs azonosító
        /// </summary>
        public uint ArbitrationId { get; private set; }
        /// <summary>
        /// Távoli adatkérés jelzése.
        /// </summary>
        public bool Remote { get; private set; }
        /// <summary>
        /// Periodus idő. Nullázható.
        /// </summary>
        public long? PeriodTime { get; private set; }
        /// <summary>
        /// Minimum periodusidő. Nullázható.
        /// </summary>
        public long? DeltaMinTime { get; private set; }
        /// <summary>
        /// Maximum periodusidő. Nullázható.
        /// </summary>
        public long? DeltaMaxTime { get; private set; }
        /// <summary>
        /// Üzenet gyakorisága. Nullázható.
        /// </summary>
        public double? Rate { get;  private set; }
        /// <summary>
        /// Enyniszer jött be ez az üzenet. 
        /// </summary>
        public long Count { get; private set; }

        /// <summary>
        /// Adatkeret.
        /// </summary>
        public byte[] Data { get; private set; }

        /// <summary>
        /// Adatkeret hossza.
        /// </summary>
        public int? Length
        {
            get
            {
                if (Data != null)
                    return Data.Length;
                else
                    return null;
            }
        }

        /// <summary>
        /// Üzenet iránya.
        /// </summary>
        public MessageDirection Direction { get; private set; }

        /// <summary>
        /// Arbitrációs azonosító típusa.
        /// </summary>
        public ArbitrationIdType Type { get; private set; }

        /// <summary>
        /// Utoljára ekkor ékrezett üzenet. Nullázható.
        /// </summary>
        public DateTime? Timestamp { get { return _timestamp; } }

        /// <summary>
        /// Két frsíjtés között eletelt mért idő.
        /// </summary>
        public double? DeltaT { get { return _deltaT/1000.0;  } }

        private DateTime? _timestamp;
        private readonly Stopwatch _watch;
        private long _msgCountTemp;
        private long? _deltaT;

        public MessageStatisticsItem(   string name,
                                        uint arbitrationId, 
                                        MessageDirection direction,
                                        ArbitrationIdType type,
                                        bool remote,
                                        byte[] data,
                                        DateTime timestamp )
        {
            Name = name;
            Type = type;
            Direction = direction;
            ArbitrationId = arbitrationId;
            Remote = remote;
            _timestamp = timestamp;
            Data = data;
            PeriodTime = null;
            DeltaMinTime = null;
            DeltaMaxTime = null;
            _deltaT = null;
            Count = 1;

            _watch = new Stopwatch();
        }

        /// <summary>
        /// Ha az elem már létezett, akkor az adatkert és az időbélyeg ezzel frssíthető.
        /// </summary>
        /// <param name="timestap">Időbélyeg.</param>
        /// <param name="timestamp"></param>
        /// <param name="data">Adatkeret.</param>
        public void Increment(DateTime timestamp, byte[] data)
        {
            Data = data;
            Count++;
            if(_timestamp.HasValue)
                PeriodTime = (timestamp.Ticks - _timestamp.Value.Ticks) / 10000;
            
            if(!DeltaMaxTime.HasValue)
                DeltaMaxTime = PeriodTime;

            if(!DeltaMinTime.HasValue)
                DeltaMinTime = PeriodTime;

            if (PeriodTime > DeltaMaxTime)
                DeltaMaxTime = PeriodTime;

            if (PeriodTime < DeltaMinTime)
                DeltaMinTime = PeriodTime;

            _timestamp = timestamp;
        }

        /// <summary>
        /// Üzenethez tartozó statisztika alpahelyzetbe hozása.
        /// </summary>
        public void Default()
        {
            _timestamp = null;
            Data = null;
            PeriodTime = null;
            DeltaMinTime = null;
            DeltaMaxTime = null;
            Rate = null;
            _deltaT = null;
            _msgCountTemp = 0;
            Count = 0;

            if (DefaultStateComplete != null)
                DefaultStateComplete(this, EventArgs.Empty);
        }

        /// <summary>
        /// Üzenet gyakoriságának frirssítése. Ezt periodikusan kell frssíteni.
        /// </summary>
        public void UpdateRate()
        {
            if (!_watch.IsRunning)
            { 
                _watch.Start();
            }
            else
            {
                _deltaT = _watch.ElapsedMilliseconds;
                Rate = (((Count - _msgCountTemp) / (double)_deltaT)) * 1000;
                _msgCountTemp = Count;
                _watch.Restart();
            }
        }
    }
}
