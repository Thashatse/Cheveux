using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_GetCustomerBooking
    {
        public string stylistEmployeeID { get; set; }
        public string stylistFirstName { get; set; }
        public string CustFullName { get; set; }
        public DateTime bookingDate { get; set; }
        public DateTime bookingStartTime { get; set; }
        public string slotNo { get; set; }
        public string bookingID { get; set; }
        public string CustomerID { get; set; }
        public Char arrived { get; set; }
        public string BookingComment { get; set; }
    }
}
