using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Models
{
    public class TREATMENT
    {
        public string TreatmentID { get; set; }
        public Nullable<int> Qty { get; set; }
        public string Name { get; set; }
        public string TreatmentType { get; set; }
        public string BrandID { get; set; }
    }
}
