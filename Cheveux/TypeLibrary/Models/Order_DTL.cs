using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Models
{
    public class Order_DTL
    {
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public Nullable<int> Qty { get; set; }
    }
}
