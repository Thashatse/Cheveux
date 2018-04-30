using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Models
{
    public class CUST_VISIT
    {
        public string CustomerID { get; set; }
        public System.DateTime Date { get; set; }
        public string BookingID { get; set; }
        public string Description { get; set; }
    }
}
