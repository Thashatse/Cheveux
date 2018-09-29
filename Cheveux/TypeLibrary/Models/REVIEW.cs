using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Models
{
    public class REVIEW
    {
        public string ReviewID { get; set; }
        public string CustomerID { get; set; }
        public string EmployeeID { get; set; }
        public string PrimaryBookingID { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
