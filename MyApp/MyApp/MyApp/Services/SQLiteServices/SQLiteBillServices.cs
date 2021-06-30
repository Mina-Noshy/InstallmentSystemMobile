using MyApp.MVVM.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Services.SQLiteServices
{
    public interface ISQLiteBillServices
    {
        List<Bill> GetAll();

        List<Bill> GetAll(int clientId);

        int AddAll(List<Bill> bills);

        int DeleteAll();

        int UpdateAll(List<Bill> bills);

        bool Any();
    }

    public class SQLiteBillServices : ISQLiteBillServices
    {
        public SQLiteConnection db = App.context.Connection;

        public List<Bill> GetAll()
        {
            return db.Table<Bill>().ToList();
        }

        public List<Bill> GetAll(int clientId)
        {
            return db.Table<Bill>().Where(x => x.clientId == clientId).ToList();
        }

        public int AddAll(List<Bill> bills)
        {
            return db.InsertAll(bills);
        }

        public int DeleteAll()
        {
            return db.DeleteAll<Bill>();
        }

        public int UpdateAll(List<Bill> bills)
        {
            DeleteAll();
            return AddAll(bills);
        }

        public bool Any()
        {
            return db.Table<Bill>().Count() > 0 ? true : false;
        }
    }
}
