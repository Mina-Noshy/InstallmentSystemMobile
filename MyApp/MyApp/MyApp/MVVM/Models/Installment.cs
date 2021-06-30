using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.MVVM.Models
{
    public class Installment
    {
        public int id { get; set; }
        public int billId { get; set; }
        public string client { get; set; }
        public double amountValue { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime? receivedDate { get; set; }
    }
}
