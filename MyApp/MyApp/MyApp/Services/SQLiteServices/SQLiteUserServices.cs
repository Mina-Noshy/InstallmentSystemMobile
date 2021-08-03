using MyApp.MVVM.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Services.SQLiteServices
{
    public interface ISQLiteUserServices
    {
        List<User> GetAll();

        List<User> GetAll(string txt);

        int AddAll(List<User> users);

        int DeleteAll();

        int UpdateAll(List<User> clients);

        bool Any();
    }

    public class SQLiteUserServices : ISQLiteUserServices
    {
        public SQLiteConnection db = App.context.Connection;

        public List<User> GetAll()
        {
            return db.Table<User>().ToList();
        }

        public List<User> GetAll(string txt)
        {
            return db.Table<User>().Where(x => x.fullName.ToLower().Contains(txt.ToLower())).ToList();
        }


        public int AddAll(List<User> users)
        {
            return db.InsertAll(users);
        }

        public int DeleteAll()
        {
            return db.DeleteAll<User>();
        }

        public int UpdateAll(List<User> users)
        {
            DeleteAll();
            return AddAll(users);
        }

        public bool Any()
        {
            return db.Table<User>().Count() > 0 ? true : false;
        }
    }
}
