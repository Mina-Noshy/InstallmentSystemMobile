using MyApp.MVVM.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Services.SQLiteServices
{
    public interface ISQLiteClientServices
    {
        List<Client> GetAll();

        List<Client> GetAll(string txt);

        int AddAll(List<Client> clients);

        int DeleteAll();

        int UpdateAll(List<Client> clients);

        bool Any();
    }

    public class SQLiteClientServices : ISQLiteClientServices
    {
        public SQLiteConnection db = App.context.Connection;

        public List<Client> GetAll()
        {
            return db.Table<Client>().ToList();
        }

        public List<Client> GetAll(string txt)
        {
            return db.Table<Client>().Where(x => x.name.ToLower().Contains(txt.ToLower())).ToList();
        }


        public int AddAll(List<Client> clients)
        {
            return db.InsertAll(clients);
        }

        public int DeleteAll()
        {
            return db.DeleteAll<Client>();
        }

        public int UpdateAll(List<Client> clients)
        {
            DeleteAll();
            return AddAll(clients);
        }

        public bool Any()
        {
            return db.Table<Client>().Count() > 0 ? true : false;
        }
    }
}
