using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_GetBookingServices
    {
        public string BookingID { get; set; }

        public string ServiceID { get; set; }

        public string ServiceName { get; set; }

        public double Price { get; set; }

        public string serviceDescripion { get; set; }
        public string type { get; set; }
    }
}
