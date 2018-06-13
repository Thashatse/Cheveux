using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_GetCustomerBooking
    {
        public string serviceName { get; set; }
        public string serviceDescripion { get; set; }
        public string servicePrice { get; set; }
        public string stylistEmployeeID { get; set; }
        public string stylistFirstName { get; set; }
        public DateTime bookingDate { get; set; }
        public DateTime bookingStartTime { get; set; }
        public string bookingID { get; set; }
        public string CustomerID { get; set; }
        public Char arrived { get; set; }
    }
}
