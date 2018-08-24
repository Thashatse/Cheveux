using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class ManagerStats
    {
        public decimal sales { get; set; }
        public int upcomingBookings { get; set; }
        public int totalBookings { get; set; }
        public int registeredCustomers { get; set; }
    }
}
