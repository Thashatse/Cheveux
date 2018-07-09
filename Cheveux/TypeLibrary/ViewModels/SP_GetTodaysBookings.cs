using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_GetTodaysBookings
    {
        public string BookingID { get; set; }
        public string SlotNo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CustomerID { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string StylistID { get; set; }
        public string ServiceID { get; set; }
        public string ServiceName { get; set; }
        public DateTime Date { get; set; }
        public string Available { get; set; }
        public string Arrived { get; set; }
        public string Comment { get; set; }
    }
}
