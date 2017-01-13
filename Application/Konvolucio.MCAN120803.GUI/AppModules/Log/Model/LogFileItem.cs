
namespace Konvolucio.MCAN120803.GUI.AppModules.Log.Model
{
    using System;
    using System.IO;
    using System.ComponentModel;
    using System.Data.SQLite;

    public interface ILogFileItem : INotifyPropertyChanged
    {
        /// <summary>
        /// A log fájl egyedi azonosítója
        /// </summary>
        string Guid { get; }

        /// <summary>
        /// Log fáj utvonala.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// A fájl neve
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Info tábla
        /// </summary>
        ILogInfo Info { get; }
        
        /// <summary>
        /// Üzenetek táblálja
        /// </summary>
        ILogFileMessageCollection Messages { get; }
        
        /// <summary>
        /// A fájl hoz tartozó statisztika, származtatott.
        /// </summary>
        ILogFileMessageStatistics Statistics { get; }
        
        /// <summary>
        /// A log fájl átnevezése.
        /// </summary>
        /// <param name="newFileName">Az új fájl neve</param>
        
        void Rename(string newFileName);
        
        /// <summary>
        /// Az log fájl betöltése. Ehhez a betöltős konstruktort kell használnod.
        /// </summary>
        /// <returns>A betöltött log fájl példánya.</returns>
        LogFileItem Load();
    }

    /// <summary>
    /// Ez egy log fájl.
    /// </summary>
    public class LogFileItem : ILogFileItem
    {
        public event PropertyChangedEventHandler PropertyChanged;
        internal event ProgressChangedEventHandler ProgressChanged
        {
            add
            {
                ((LogFileMessageCollection)Messages).ProgressChanged += value;
            }
            remove
            {
                ((LogFileMessageCollection)Messages).ProgressChanged -= value;
            }
        }

        public ILogInfo Info { get; private set; }
        public ILogFileMessageCollection Messages { get; private set; }
        public ILogFileMessageStatistics Statistics { get; private set; }
        
        
        public string Path { get { return _connection.Path; } }
        public string Guid { get; set; }
        public string Name
        {
            get { return System.IO.Path.GetFileNameWithoutExtension(Path); }
        }

        private readonly LogFileConnection _connection;

        /// <summary>
        /// Konsturkor létező log betöltéséhez.
        /// </summary>
        public LogFileItem(string path)
        {
            _connection = new LogFileConnection(path);
            Statistics = new LogFileMessageStatistics(_connection);
            Messages = new LogFileMessageCollection(_connection, Statistics);
            Info = new LogFileInfo(_connection);
        }

        /// <summary>
        /// Konstruktor új log létrehozásához.
        /// </summary>
        public LogFileItem(string location, string projectName, string fileName)
        {
            var path = GetNewPath(location, projectName, fileName);
            SQLiteConnection.CreateFile(path);
            var item = new LogFileItem(path);

            _connection = new LogFileConnection(path);
            Statistics = new LogFileMessageStatistics(_connection);
            Messages = new LogFileMessageCollection(_connection, Statistics);
            Info = new LogFileInfo(_connection);

            item.Messages.Create();
            item.Info.Create();
            item.Info.Init();
        }

        public LogFileItem Load()
        {
            ((LogFileMessageCollection) Messages).Load();
            ((LogFileMessageStatistics) Statistics).RiseChanged();
            return this;
        }

        /// <summary>
        /// Az új log fájlh utvonalának létrhozása és ellenörzése.
        /// </summary>
        /// <param name="location">A log fájl helye.</param>
        /// <param name="projectName">Az új log projectjének neve.</param>
        /// <param name="fileName">A fájl neve.</param>
        /// <returns></returns>
        private string GetNewPath(string location, string projectName, string fileName)
        {

            string loglocation = location + "\\Log Storage for " + projectName + "\\";

            if (!Directory.Exists(loglocation))
                Directory.CreateDirectory(loglocation);

            string path = loglocation + fileName + LogFileConnection.DbFileExtension;

            if (File.Exists(path))
            {
                for (int i = 0; i < 99; i++)
                {
                    path = loglocation + fileName + "_" + i.ToString() + LogFileConnection.DbFileExtension;
                    if (!File.Exists(path))
                        break;
                }
            }

            if (File.Exists(path))
                throw new ApplicationException("File already exists:" + path);
            return path;
        }

   
        /// <summary>
        /// A log fájl neve kiterjesztés nélkül.
        /// </summary>
        /// <param name="newFileName"></param>
        public void Rename(string newFileName)
        {
            _connection.LogFileRename(newFileName);
            OnPropertyChanged("Name");
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Name != null)
                return Name;
            else
                return "NoName";
        }      
    }
}
