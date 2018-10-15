using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_ReturnAvailServices
    {
        public string serviceID { get; set; }
        public string name { get; set; }
        public string productDescription { get; set; }
        public decimal price { get; set; }
        public int noSlots { get; set; }
        public string type { get; set; }
    }
}
