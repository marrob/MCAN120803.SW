
namespace Konvolucio.MCAN120803.GUI.AppModules.Log.Model
{
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// 
    /// </summary>
    public interface ILogFileMessageStatistics:INotifyPropertyChanged
    {
        int TransmittedMessageCount { get; }
        int ReceivedMessageCount { get; }
        List<uint> GetArbitationIds();
    }

    /// <summary>
    /// 
    /// </summary>
    public class LogFileMessageStatistics : ILogFileMessageStatistics
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int TransmittedMessageCount
        {
            get
            {
                return _connection.ReadInt32Query("SELECT COUNT(*) FROM Messages WHERE (Direction = 'Transmitted') ");
            }
        }
        public int ReceivedMessageCount
        {
            get
            {
                return _connection.ReadInt32Query("SELECT COUNT(*) FROM Messages WHERE (Direction = 'Received') ");
            }
        }
        readonly LogFileConnection _connection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public LogFileMessageStatistics(LogFileConnection connection)
        {
            _connection = connection;
        }

        public void RiseChanged()
        {
            OnPropertyChanged("TransmittedMessageCount");
            OnPropertyChanged("ReceivedMessageCount");
        }

        /// <summary>
        /// Az összes különböző Arbitációs azonosító.
        /// </summary>
        /// <returns>Arbitációs üzenetek listája.</returns>
        public List<uint> GetArbitationIds()
        {
            string query = "SELECT DISTINCT ArbitrationId FROM Messages";
            return _connection.ReadListInt32(query);
        }

        /// <summary>
        /// Tulajdonság változtott
        /// </summary>
        /// <param name="name"></param>
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
}
