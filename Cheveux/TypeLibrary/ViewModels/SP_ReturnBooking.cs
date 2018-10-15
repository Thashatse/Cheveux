using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_ReturnBooking
    {
        public string bookingID { get; set; }
        public string customerID { get; set; }
        public string stylistID { get; set; }
        public string slotNo { get; set; }
        public string startTime { get; set; }
        public DateTime date { get; set; }
    }
}
