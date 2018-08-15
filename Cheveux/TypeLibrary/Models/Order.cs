using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Models
{
    public class Order
    {
        public string supplierID { get; set; }
        public string OrderID { get; set; }
        public DateTime orderDate { get; set; }
        public bool Received { get; set; }
        public DateTime dateReceived { get; set; }
    }
}
