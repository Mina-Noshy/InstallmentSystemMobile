using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyApp.MVVM.Models.StandardModels
{
    public class StandardBill
    {
        public int id { get; set; }
        public int clientId { get; set; }
        public double originalAmount { get; set; }
        public double percentage { get; set; }
        public double totalAmount { get; set; }
        public double amountPaid { get; set; }
        public double restAmount { get; set; }
        public short installmentCount { get; set; } // عدد الاقساط
        public double delayFine { get; set; } = 0; // غرامه تاخير
        public InstallmentTypes delayFineType { get; set; }
        public InstallmentTypes installmentType { get; set; }
        public DateTime billDate { get; set; } = DateTime.UtcNow;
        public string description { get; set; }
    }

    public enum InstallmentTypes
    {
        سنوى,
        شهرى,
        يومى
    }

    public enum InstallmentTypesEN
    {
        Yearly,
        Monthly,
        Daily
    }

}
