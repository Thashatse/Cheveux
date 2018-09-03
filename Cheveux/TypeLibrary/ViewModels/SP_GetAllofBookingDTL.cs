using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_GetAllofBookingDTL
    {
        public string BookingID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public string Price { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Comment { get; set; }
    }
}
