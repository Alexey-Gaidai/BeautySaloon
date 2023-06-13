using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySaloon.Model
{
    internal class Records
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public TimeSpan Time { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ServiceName { get; set; }
        public string MasterName { get; set; }
        public string PaymentType { get; set; }
        public decimal ServiceCost { get; set; }
        public decimal MaterialCost { get; set; }
        public string Note { get; set; }
    }
}
