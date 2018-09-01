using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_GetServices
    {
        public string ServiceID { get; set; }
        public string Name { get; set; }
        public char ServiceType { get; set; }
        public decimal Price { get; set; }
    }
}
