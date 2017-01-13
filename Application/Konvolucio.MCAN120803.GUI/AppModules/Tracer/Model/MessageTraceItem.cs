
namespace Konvolucio.MCAN120803.GUI.AppModules.Tracer.Model
{
    using System;
    using System.ComponentModel;
    using Common;      /*MessageDirection*/
    using Converters;  /*ArbitrationIdConverter, DataFrameConverter*/

    public class MessageTraceItem
    {
        /// <summary>
        /// Üzenet sorszáma 1.-es bázisú
        /// </summary>
        public int Index { get; internal set; }
        /// <summary>
        /// Üzenet neve.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Üzenet időbéyege.
        /// </summary>
        public DateTime Timestamp { get; private set; }
        /// <summary>
        /// Üzenet iránya.
        /// </summary>
        public MessageDirection Direction { get; private set; }
        /// <summary>
        /// Arbitrációs azonosító típusa.
        /// </summary>
        public ArbitrationIdType Type { get; private set; }
        /// <summary>
        /// Távoli adatkérés jelzése.
        /// </summary>
        public bool Remote { get; private set; }
        /// <summary>
        /// Adatkeret hossza.
        /// </summary>
        public int Length
        {
            get
            {
                if (Data != null)
                    return Data.Length;
                else
                    return 0;
            }
        }
        /// <summary>
        /// Üzenet arbitrációs azonosítója.
        /// </summary>
        [TypeConverter(typeof(ArbitrationIdConverter))]
        public uint ArbitrationId { get; private set; }
        /// <summary>
        /// Üzenet adatkerete.
        /// </summary>
        [TypeConverter(typeof(DataFrameConverter))]
        public byte[] Data { get; private set; }
        /// <summary>
        /// Üzenet dokumentációja.
        /// </summary>
        public string Documentation { get; set; }
        /// <summary>
        /// Üzenet leírása.
        /// </summary>
        public string Description { get; set; }

        public MessageTraceItem() { }

        public MessageTraceItem(   string name,
                                   DateTime timestamp,
                                   MessageDirection direction,
                                   ArbitrationIdType type,
                                   bool remote,
                                   uint arbitrationId,
                                   byte[] data,
                                   string documentation,
                                   string description)
        {
            Name = name;
            Timestamp = timestamp;
            Direction = direction;
            Type = type;
            Remote = remote;
            ArbitrationId = arbitrationId;
            Data = data;
            Documentation = documentation;
            Description = description;
        }
    }
}
