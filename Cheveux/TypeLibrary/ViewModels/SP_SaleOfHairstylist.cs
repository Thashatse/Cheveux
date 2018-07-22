using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_SaleOfHairstylist
    {
        public string SaleID { get; set; }
        public DateTime date { get; set; }
        public string CustomerID { get; set; }
        public char paymentType { get; set; }
        public string stylistID { get; set; }
        public string serviceID { get; set; }
        public char Available { get; set; }
        public char Arrived { get; set; }
        public string Comment { get; set; }
        
    }
}
