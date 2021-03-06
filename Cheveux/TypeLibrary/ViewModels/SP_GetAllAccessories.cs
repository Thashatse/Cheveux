﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_GetAllAccessories
    {
        public object Item1;
        public int count;

        public string ProductID { get; set; }
        public string Name { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string ProductType { get; set; }
        public string Active { get; set; }
        public byte[] ProductImage { get; set; }
        public string Colour { get; set; }
        public int Qty { get; set; }
        public string BrandID { get; set; }
        public string Brandname { get; set; }
        public string brandType { get; set; }
        public string supplierID { get; set; }
        public string supplierName { get; set; }
        public string contactName { get; set; }
        public string contactNo { get; set; }
        public string contactEmail { get; set; }
    }
}
