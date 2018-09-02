using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_GetEmpAgenda
    {
        public string BookingID { get; set; }
        public string PrimaryID { get; set;}
        public string UserID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CustomerFName { get; set; }
        public string EmpFName { get; set; }
        public string Arrived { get; set; }
        public DateTime Date { get; set; }
        public string empID { get; set; }
        public string Comment { get; set; }
    }
}
