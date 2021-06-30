using MyApp.MVVM.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Services.SQLiteServices
{
    public interface ISQLiteInstallmentServices
    {
        List<Installment> GetAll();

        List<Installment> GetAll(int billId);

        int AddAll(List<Installment> installments);

        int DeleteAll();

        int UpdateAll(List<Installment> installments);

        bool Any();
    }

    public class SQLiteInstallmentServices : ISQLiteInstallmentServices
    {
        public SQLiteConnection db = App.context.Connection;

        public List<Installment> GetAll()
        {
            return db.Table<Installment>().ToList();
        }

        public List<Installment> GetAll(int billId)
        {
            return db.Table<Installment>().Where(x => x.billId == billId).ToList();
        }

        public int AddAll(List<Installment> installments)
        {
            return db.InsertAll(installments);
        }

        public int DeleteAll()
        {
            return db.DeleteAll<Installment>();
        }

        public int UpdateAll(List<Installment> installments)
        {
            DeleteAll();
            return AddAll(installments);
        }

        public bool Any()
        {
            return db.Table<Installment>().Count() > 0 ? true : false;
        }
    }
}
