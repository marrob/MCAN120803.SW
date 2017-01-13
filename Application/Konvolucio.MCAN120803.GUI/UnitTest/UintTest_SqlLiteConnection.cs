
//http://www.codeproject.com/Articles/236918/Using-SQLite-embedded-database-with-entity-framewo
//http://blog.tigrangasparian.com/2012/02/09/getting-started-with-sqlite-in-c-part-one/
namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;

    using NUnit.Framework;
    using System.Data.SQLite;

    [TestFixture]
    class UintTest_SqlLiteConnection
    {
        string FullPath = "D:\\MyDatabase.sqlite.s3db";

        [Test]
        public void _0001_CreateFile_Connect()
        {
            if (System.IO.File.Exists(FullPath))
                System.IO.File.Delete(FullPath);

            SQLiteConnection.CreateFile(FullPath);
            //var m_dbConnection = new SQLiteConnection("Data Source=D:\\MyDatabase.sqlite.s3db;Version=3;");
            var m_dbConnection = new SQLiteConnection("Data Source="+ FullPath + ";Version=3;");
            m_dbConnection.Open();
            string sql = "create table highscores (name varchar(20), score int)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        [Test]
        public void _0002_CrateFile_Open_CreateTable_Insert_Close()
        {
            if (System.IO.File.Exists(FullPath))
                System.IO.File.Delete(FullPath);

            SQLiteConnection.CreateFile(FullPath);
            var m_dbConnection = new SQLiteConnection("Data Source=" + FullPath + ";Version=3;");
            m_dbConnection.Open();
            string sql = "CREATE TABLE highscores (name VARCHAR(20), score INT)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into highscores (name, score) values ('Me', 3000)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into highscores (name, score) values ('Myself', 6000)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into highscores (name, score) values ('And I', 9001)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        [Test]
        public void _0003_Select()
        {
            var m_dbConnection = new SQLiteConnection("Data Source=" + FullPath + ";Version=3;");
            m_dbConnection.Open();
            string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
            m_dbConnection.Close();
        }
        [Test]
        public void _0004_DeleteFile()
        {
            System.IO.File.Delete(FullPath);
        }
    }
}
