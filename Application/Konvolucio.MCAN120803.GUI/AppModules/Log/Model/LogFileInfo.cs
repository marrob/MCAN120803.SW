

namespace Konvolucio.MCAN120803.GUI.AppModules.Log.Model
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// A log fájl Infó táblája
    /// </summary>
    public interface ILogInfo: INotifyPropertyChanged
    {
        /// <summary>
        /// A Log fájl leírása.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Infó tábla létrehozása.
        /// </summary>
        void Create();

        /// <summary>
        /// Létrehozás után az első sort beszurja.
        /// </summary>
        void Init();
    }

    /// <summary>
    /// A log fájl Infó táblája
    /// </summary>
    public class LogFileInfo : ILogInfo 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Description
        {
            get { return _connection.ReadString("SELECT Description FROM Info"); }
            set 
            {
                if (_description != value)
                {
                    _description = value;
                    _connection.ExecuteNonQuery("UPDATE Info SET Description = '" + value + "'");
                    OnPropertyChanged("Description");
                }
            }
        }
        string _description;
        readonly LogFileConnection _connection;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connection">Connection</param>
        public LogFileInfo(LogFileConnection connection)
        {
            _connection = connection;
        }

        public void Create()
        {
            string query = "CREATE TABLE Info " +
                         "(" +
                         "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                         "Description VARCHAR(80), " +
                         "Timestamp DATETIME" +
                         ")";
            _connection.ExecuteNonQuery(query);
        }

        public void Init()
        {
            string query = "INSERT INTO Info VALUES ( '1', '', '" + DateTime.Now.Ticks  + "')";
            _connection.ExecuteNonQuery(query);
        }

        /// <summary>
        /// Tulajdonság változtott
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
