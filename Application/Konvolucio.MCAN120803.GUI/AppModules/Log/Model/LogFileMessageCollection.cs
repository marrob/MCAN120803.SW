
namespace Konvolucio.MCAN120803.GUI.AppModules.Log.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    using System.Data.SQLite;
    using Converters;
    using Common;

    /// <summary>
    /// 
    /// </summary>
    public interface ILogFileMessageCollection
    {
        event ListChangedEventHandler ListChanged;
        event EventHandler DescriptionChanged;
     
        ILogMessageItem this[int index] { get; set; }
        string Name { get; set; }
        List<ILogMessageItem> SelectedItems { get; }
        bool RaiseListChangedEvents { get; set; }

        IEnumerator<ILogMessageItem> GetEnumerator();
        void Add(ILogMessageItem item);
        void ResetBindings();
        void Clear();
        void Create();
        void OnDescriptionChanged();
        void RemoveSelectedMessage();

        int Count { get; }
        void AddToStorageBegin();
        void AddToStorageEnd();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        void AddToStorage(ILogMessageItem item);
        void AddToStorage(
                            string name,
                            DateTime timestamp,
                            MessageDirection direction,
                            ArbitrationIdType type,
                            uint arbitrationId,
                            bool isRemote,
                            byte[] data,
                            string documentation,
                            string description);

        string GetMessageNameByArbId(uint arbitrationId);
    }

    /// <summary>
    /// Az üzenetek listája.
    /// </summary>
    public class LogFileMessageCollection : BindingList<ILogMessageItem>, ILogFileMessageCollection
    {
        internal event ProgressChangedEventHandler ProgressChanged;
        public event EventHandler DescriptionChanged;
        public string Name { get; set; }
        readonly LogFileConnection _connection;
        readonly ILogFileMessageStatistics _statistics;

        /// <summary>
        /// Kijelölt üzenetk listája.
        /// </summary>
        public List<ILogMessageItem> SelectedItems { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public LogFileMessageCollection(LogFileConnection connection, ILogFileMessageStatistics statistics)
        {
            _connection = connection;
            _statistics = statistics;
            SelectedItems = new List<ILogMessageItem>();
        }

        public new int Count
        {
            get
            {
                return _connection.ReadInt32Query("SELECT COUNT('Index') FROM Messages");
            }
        }

        /// <summary>
        /// Jelezd, hogy változotott a Description.
        /// </summary>
        public void OnDescriptionChanged()
        {
            if (DescriptionChanged != null)
                DescriptionChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Messsage tábla létrehozása
        /// </summary>
        public void Create()
        {
            string query = "CREATE TABLE Messages " +
                         "(" +
                         "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                         "Name VARCHAR(80), " +
                         "Timestamp BIGINT," +
                         "Direction VARCHAR(20)," +
                         "Type VARCHAR(10)," +
                         "ArbitrationId INT," +
                         "IsRemote BOOL NOT NULL," +
                         "Data VARCHAR(80)," +
                         "Documentation VARCHAR(80)," +
                         "Description VARCHAR(80) " +
                         ")";

            _connection.ExecuteNonQuery(query);
        }

        /// <summary>
        /// Kijelölt üzenetek törlése.
        /// </summary>
        public void RemoveSelectedMessage()
        {
            var current = 0;
            this.RaiseListChangedEvents = false;
            try
            {
                foreach (var item in SelectedItems)
                {
                    RemoveMessage(item.Index);
                    Remove(item);
                    if (current % 20 == 0)
                        OnProgressChanged(this, new ProgressChangedEventArgs((int)((current / (double)SelectedItems.Count) * 100), "Deleting: " + item.ToString()));
                    current++;
                }
                OnProgressChanged(this, new ProgressChangedEventArgs(100, "Deleting Complete..."));
            }
            catch (Exception ex)
            {
                AppDiagService.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + "=>" + "ERROR" + " " + ex.Message);
                OnProgressChanged(this, new ProgressChangedEventArgs(100, "Deleting Failed..."));
                throw ex;
            }
            finally
            {
                this.RaiseListChangedEvents = true;
                this.ResetBindings();
                ((LogFileMessageStatistics) _statistics).RiseChanged();
            }
        }

        /// <summary>
        /// Használd ha sok egymást követő AddToStorage kövekezik
        /// </summary>
        public void AddToStorageBegin()
        {
            _connection.SqLiteConnection.Open();
            var command = new SQLiteCommand("BEGIN", _connection.SqLiteConnection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Használd ha sok egymást követő AddToStorage véget ért.
        /// </summary>
        public void AddToStorageEnd()
        {
            var command = new SQLiteCommand("END", _connection.SqLiteConnection);
            command.ExecuteNonQuery();
            _connection.SqLiteConnection.Close();
        }

        /// <summary>
        /// Az CAN üzenetek betöltése az adatbázis táblából.
        /// </summary>
        internal void Load()
        {
            RaiseListChangedEvents = false;
            Clear();

            int total = Count;
            int current = 0;

            string query = "SELECT " +
                            "Id, " +
                            "Name, " +
                            "Timestamp, " +
                            "Direction, " +
                            "Type, " +
                            "ArbitrationId, " +
                            "IsRemote, " +
                            "Data, " +
                            "Documentation, " +
                            "Description " +
                            "FROM " +
                            "Messages";

            _connection.SqLiteConnection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(query, _connection.SqLiteConnection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       var msg = new LogMessageItem(
                                                    _connection,
                                                    reader.GetInt32(0),                 /*item.Index*/
                                                    reader.GetString(1),                /*item.Name*/
                                                    new DateTime(reader.GetInt64(2)),   /*Timestamp*/
                                                    (MessageDirection)Enum.Parse(typeof(MessageDirection), reader.GetString(3) as string),   /*Direction*/
                                                    (ArbitrationIdType)Enum.Parse(typeof(ArbitrationIdType), reader.GetString(4) as string), /*Type*/
                                                    (uint)reader.GetInt32(5),           /*ArbitrationId*/
                                                    reader.GetBoolean(6),               /*Remote*/
                                                    CustomDataConversion.StringToByteArrayHighSpeed(reader.GetString(7)),                    /*Data*/
                                                    reader.GetString(8),                /*Documentation*/
                                                    reader.GetString(9));               /*Description*/
                        this.Add(msg);
                        if (current % 20 == 0)
                            OnProgressChanged(this, new ProgressChangedEventArgs((int)((current / (double)total) * 100.0), "Loading:" + msg.ToString()));
                        current++;
                    }
                    OnProgressChanged(this, new ProgressChangedEventArgs(100, "Loading Complete"));
                }
            }
            _connection.SqLiteConnection.Close();
            RaiseListChangedEvents = true;
            ResetBindings();
        }

        /// <summary>
        /// Üzenet törlése Id alapján
        /// </summary>
        /// <param name="id"></param>
        void RemoveMessage(int id)
        {
            string query = "DELETE FROM Messages WHERE Id = '" + id + "'";
            _connection.ExecuteNonQuery(query);
            
        }

        /// <summary>
        /// Egy sor beszurása a Message táblába.
        /// </summary>
        void AddToTable(
                            string name,
                            DateTime timestamp,
                            MessageDirection direction,
                            ArbitrationIdType type,
                            uint arbitrationId,
                            bool isRemote,
                            byte[] data,
                            string documentation,
                            string description)
        {

            string query = "INSERT INTO Messages " +
                "(Name, Timestamp, Direction, Type, ArbitrationId, IsRemote, Data, Documentation, Description)" +
                " values " +
                 "( '" + name + "', '" +
                         timestamp.Ticks + "', '" +
                         direction + "', '" +
                         type + "', '" +
                         arbitrationId.ToString() + "', '" +
                         (isRemote ? 1 : 0) + "', '" +
                         CustomDataConversion.ByteArrayToStringHighSpeed(data) + "', '" +
                         documentation + "', '" +
                         description + "')";

            var command = new SQLiteCommand(query, _connection.SqLiteConnection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Hozáadja a listához és a fájlhoz is.
        /// Ellőtte a AddToSotrageBeging és végén a AddtToStorageEand haszálata kötelező.
        /// </summary>
        /// <param name="item"></param>
        public void AddToStorage(ILogMessageItem item)
        {
            Add(item);
            AddToTable(item.Name, item.Timestamp, item.Direction, item.Type, item.ArbitrationId, item.Remote, item.Data, item.Documentation, item.Description);
        }



        /// <summary>
        /// 
        /// </summary>
        public void AddToStorage(
                    string name,
                    DateTime timestamp,
                    MessageDirection direction,
                    ArbitrationIdType type,
                    uint arbitrationId,
                    bool isRemote,
                    byte[] data,
                    string documentation,
                    string description)
        {
            AddToTable(name, timestamp, direction, type, arbitrationId, isRemote, data, documentation, description);
        }

        public string GetMessageNameByArbId(uint arbitrationId)
        { 
            return _connection.ReadString("SELECT Name FROM Messages WHERE (ArbitrationId = '" + arbitrationId + "') LIMIT 1");
        }

        protected void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (ProgressChanged != null)
                ProgressChanged(sender, e);
        }
    }
}
