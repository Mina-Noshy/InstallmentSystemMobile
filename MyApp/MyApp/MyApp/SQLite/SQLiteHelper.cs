using MyApp.MVVM.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyApp.SQLite
{
    public class SQLiteHelper
    {
        public SQLiteConnection Connection { get; set; }

        private static string databaseName = "GreenTrader.db3";

        public static string DatabasePath
        {
            get
            {
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(folderPath, databaseName);
            }
        }

        public SQLiteHelper()
        {
            Connection = new SQLiteConnection(DatabasePath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite);

            Connection.CreateTable<Setting>(CreateFlags.None);

            InitialDb();

        }

        private void InitialDb()
        {
            if (Connection.Table<Setting>().FirstOrDefault() == null)
            {
                Connection.Insert(new Setting
                {
                    settingsUpdatedAt = DateTime.UtcNow.AddMonths(-1),
                    isAuthenticated = false
                });
            }
        }
    }
}
