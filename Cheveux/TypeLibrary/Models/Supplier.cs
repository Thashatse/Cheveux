using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Models
{
    public class Supplier
    {
        public string supplierID { get; set; }
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
