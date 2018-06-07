using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_GetMyNextCustomer
    {
        public string BookingID { get; set; }
        public string UserID { get; set; }
        public Nullable<TimeSpan> StartTime { get; set; }
        public Nullable<TimeSpan> EndTime { get; set; }
        public string CustomerFName { get; set; }
        public string EmpFName { get; set; }
        public string ServiceName { get; set; }
        public string Arrived { get; set; }
        public Nullable<DateTime> Date { get; set; }
    }
}
