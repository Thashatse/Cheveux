using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_GetAllTreatments
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string ProductType { get; set; }
        public string Active { get; set; }
        public byte[] ProductImage { get; set; }
        public int Qty { get; set; }
        public string TreatmentType { get; set; }
        public string BrandID { get; set; }
        public string Brandname { get; set; }
        public string brandType { get; set; }
    }
}
