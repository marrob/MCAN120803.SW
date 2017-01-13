namespace Konvolucio.MCAN120803.GUI.AppModules.Log.Model
{
    using System;
    using System.ComponentModel;

    using Converters;
    using Common;


    public interface ILogMessageItem : INotifyPropertyChanged
    {
        int Index { get; }
        string Name { get; }
        DateTime Timestamp { get;}
        MessageDirection Direction { get; }
        ArbitrationIdType Type { get; }
        bool Remote { get;  }
        int Length { get; }
        [TypeConverter(typeof(ArbitrationIdConverter))]
        uint ArbitrationId { get; }
        [TypeConverter(typeof(DataFrameConverter))]
        byte[] Data { get; }
        string Documentation { get; set; }
        string Description { get; set; }
    }

    /// <summary>
    /// Egy CAN üzenet a log fájlban.
    /// </summary>
    class LogMessageItem: ILogMessageItem
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Index { get; private set; }
        public string Name { get; private set; }
        public DateTime Timestamp { get; set; }
        public MessageDirection Direction { get; set; }
        public ArbitrationIdType Type { get; private set; }
        public bool Remote { get; private set; }
        public int Length
        {
            get { return Data.Length; }
        }
        public uint ArbitrationId { get; private set; }
        public byte[] Data { get; private set; }
        public string Documentation
        {
            get { return _documentation; }
            set 
            {
                if (_documentation != value)
                {
                    _documentation = value;
                    string query = "UPDATE Messages SET Documentation = '" + _documentation + "' WHERE Id = '" + Index + "'";
                    _connection.ExecuteNonQuery(query);
                    OnPropertyChanged("Documentation");
                }
            }
        }
        string _documentation;
        public string Description 
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    string query = "UPDATE Messages SET Description = '" + _description + "' WHERE Id = '" + Index + "'";
                    _connection.ExecuteNonQuery(query);
                    OnPropertyChanged("Description");
                }
            }
        }
        string _description;

        /// <summary>
        /// A log fájl elérési utvonala.
        /// </summary>
        readonly LogFileConnection _connection;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public LogMessageItem(
                            LogFileConnection connection,
                            int index,
                            string name,    
                            DateTime timestamp,
                            MessageDirection direction,
                            ArbitrationIdType type,           
                            uint arbitrationId,
                            bool remote,
                            byte[] data,
                            string documentation,
                            string description
                          )
        {
            _connection = connection;
            Index = index;
            Name = name;
            Timestamp = timestamp;
            Direction = direction;
            Type = type;
            Remote = remote;
            ArbitrationId = arbitrationId;
            Data = data;
            _documentation = documentation;
            _description = description;
        }

        /// <summary>
        /// Tulajdonság változott.
        /// </summary>
        /// <param name="name"></param>
        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = string.Empty;
            str += Index.ToString() + ", ";
            str += Name + ", ";
            str += Timestamp.ToString(AppConstants.GenericTimestampFormat) + ", ";
            str += Direction + ", ";
            str += Type + ", ";
            str += Remote.ToString() + ", ";
            str += Length.ToString() + ", ";
            str += new ArbitrationIdConverter().ConvertTo(ArbitrationId, typeof(string)) + ", ";
            str += Data + ", ";
            str += Documentation + ", ";
            str += Description;
            return str;
        }
    }
}
