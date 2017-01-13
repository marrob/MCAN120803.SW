// -----------------------------------------------------------------------
// <copyright file="MessageStatistics.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.Model
{
    using System;
    using System.Linq;
    using Common;

    /// <summary>
    /// Üzenet statisztika.
    /// </summary>
    public class MessageStatistics
    {
        /// <summary>
        /// Statisztika alaphelyzetbe került, ez alapján lehet frssítneni a képerenyőt.
        /// </summary>
        public event EventHandler DefaultStateComplete;

        /// <summary>
        /// Statisztika üzenet listája
        /// </summary>
        public MessageStatisticsCollection Messages { get { return _messages; } }
        private readonly MessageStatisticsCollection _messages;

        public MessageStatistics()
        {
            _messages = new MessageStatisticsCollection();
        }

        /// <summary>
        /// Üzenet hozzá adása a statisztikához
        /// </summary>
        /// <param name="name">Üzenet neve.</param>
        /// <param name="direction">Iránya.</param>
        /// <param name="type">ArbId típusa</param>
        /// <param name="arbitrationId">Arbitációs azonosító.</param>
        /// <param name="remote">Távli adat volt?</param>
        /// <param name="data">Adatkert.</param>
        /// <param name="timestamp">Időbélyeg.</param>
        public void InsertMessage( string name, 
                                   MessageDirection direction, 
                                   ArbitrationIdType type, 
                                   uint arbitrationId, 
                                   bool remote, 
                                   byte[] data, 
                                   DateTime timestamp)
        {
            var targetItem = ((MessageStatisticsCollection) _messages).FirstOrDefault(n =>
            {
                return
                    n.ArbitrationId == arbitrationId &&
                    n.Direction == direction &&
                    n.Type == type &&
                    n.ArbitrationId == arbitrationId &&
                    n.Remote == remote;
            });

            if (targetItem == null)
            {
                _messages.Add(new MessageStatisticsItem(name, arbitrationId, direction, type, remote, data, timestamp));
            }
            else
            {
                targetItem.Increment(timestamp, data);
            }
        }

        /// <summary>
        /// Törli a listát.
        /// </summary>
        public void Clear()
        {
            _messages.Clear();
        }

        /// <summary>
        /// A lista tartalmát alaphelyzetbe teszti.
        /// </summary>
        public void Default()
        {
            foreach (var message in (MessageStatisticsCollection)_messages )
                message.Default();

            if (DefaultStateComplete != null)
                DefaultStateComplete(this, EventArgs.Empty);
        }
    }
}
