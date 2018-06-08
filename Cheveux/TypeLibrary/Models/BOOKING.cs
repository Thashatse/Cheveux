using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Models
{
    public class BOOKING
    {
        public string BookingID { get; set; }
        public string SlotNo { get; set; }
        public string CustomerID { get; set; }
        public string StylistID { get; set; }
        public string ServiceID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Available { get; set; }
        public string Arrived { get; set; }
        public string Comment { get; set; }
    }
}
