
namespace Konvolucio.MCAN120803.GUI.AppModules.Log.Model
{
    using System.Collections.Generic;
    using System.Data.SQLite;

    public class LogFileConnection
    {
        public const string DbFileExtension = ".s3db";
        public string Path { get; private set; }

        public SQLiteConnection SqLiteConnection
        {
            get
            {
                if(_connection == null)
                {
                    _connection = new SQLiteConnection();
                    var connectionString = "Data Source=" + Path + ";Version=3;";
                    _connection.ConnectionString = connectionString;   
                } 
                return _connection;
            }
        }
        SQLiteConnection _connection;

        public LogFileConnection(string path)
        {
            this.Path = path;
        }

        public void ExecuteNonQuery(string query) 
        {
            SqLiteConnection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(query, SqLiteConnection))
            {
                cmd.ExecuteNonQuery();
            }
            SqLiteConnection.Close();
        }

        public int ReadInt32Query(string query)
        {
            int retval = 0;
            SqLiteConnection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(query, SqLiteConnection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    retval = reader.GetInt32(0);
                }
            }
            SqLiteConnection.Close();
            return retval;
        }

        public List<uint> ReadListInt32(string query)
        {
            SqLiteConnection.Open();
            List<uint> retval = new List<uint>();

            using (SQLiteCommand cmd = new SQLiteCommand(query, SqLiteConnection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        retval.Add((uint)reader.GetInt32(0));
                    }
                }
            }
            SqLiteConnection.Close();
            return retval;
        }

        public string ReadString(string query)
        {
            string retval = string.Empty;
            SqLiteConnection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(query, SqLiteConnection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    if(reader.HasRows)
                        retval = reader.GetString(0);
                    else
                        retval = string.Empty;
                }
            }
            SqLiteConnection.Close();
            return retval;
        }

        public void LogFileRename(string newFileName)
        {
            string newPath = System.IO.Path.GetDirectoryName(Path) + "\\" + newFileName + DbFileExtension;
            System.IO.File.Copy(Path, newPath);
            System.IO.File.Delete(Path);
            Path = newPath;
            _connection = null;
        }
    }
}
