using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class OrderViewModel
    {
        public string supplierID { get; set; }
        public string OrderID { get; set; }
        public DateTime orderDate { get; set; }
        public bool Received { get; set; }
        public DateTime dateReceived { get; set; }
        public string ProductID { get; set; }
        public int Qty { get; set; }
        public string Name { get; set; }
        public string ProductDescription { get; set; }
        public double Price { get; set; }
        public string ProductType { get; set; }
        public string Active { get; set; }
        public byte[] ProductImage { get; set; }
        public string supplierName { get; set; }
        public string contactName { get; set; }
        public string contactNo { get; set; }
        public string contactEmail { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
    }
}
