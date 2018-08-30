using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_GetStylistBookings
    {
        public string BookingID { get; set; }
        public string PrimaryID { get; set; }
        public string StylistID { get; set; }
        public string CustomerID { get; set; }
        public string StylistName { get; set; }
        public string FullName { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        //public string ServiceID { get; set; }
        //public string ServiceName { get; set; }
        //public string ServiceDescription { get; set; }
        public string Arrived { get; set; }
    }
}
