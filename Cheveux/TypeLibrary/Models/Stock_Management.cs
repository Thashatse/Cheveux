using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Models
{
    public class Stock_Management
    {
        public string BusinessID { get; set; }
        public int LowStock { get; set; }
        public int PurchaseQty { get; set; }
        public bool AutoPurchase { get; set; }
        public string AutoPurchaseFrequency { get; set; }
        public bool AutoPurchaseProducts { get; set; }
        public DateTime NxtOrderdDate { get; set; }
    }
}
