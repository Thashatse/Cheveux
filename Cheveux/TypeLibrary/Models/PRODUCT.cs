using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Models
{
    public class PRODUCT
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string ProductType { get; set; }
        public string Active { get; set; }
        public byte[] Product1 { get; set; }
    }
}
