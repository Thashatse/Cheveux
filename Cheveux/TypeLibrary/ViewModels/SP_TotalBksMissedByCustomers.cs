using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_TotalBksMissedByCustomers
    {
        public string customerID { get; set; }
        public string customerName { get; set; }
        public int missed { get; set; }
    }
}
