using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Models
{
    public class SALE
    {
        public string SaleID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string CustID { get; set; }
        public string PaymentType { get; set; }
        public string BookingID { get; set; }
    }
}
