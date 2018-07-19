using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_BookingsReportForHairstylist
    {
        public string BookingID { get; set; }
        public string slotNo { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string CustomerID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string stylistID { get; set; }

        public string serviceID { get; set; }

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Available { get; set; }
        public string Arrived { get; set; }
        public string Comment { get; set; }

    }
}
