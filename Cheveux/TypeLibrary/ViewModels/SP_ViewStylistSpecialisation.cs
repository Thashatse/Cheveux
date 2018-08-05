using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class SP_ViewStylistSpecialisationAndBio
    {
        public string EmployeeID { get; set; }
        public string serviceID { get; set; }
        public string serviceName { get; set; }
        public string serviceDescription { get; set; }
        public decimal servicePrice { get; set; }
        public char serviceType { get; set; }
        public byte[] serviceImage { get; set; }
        public string Stylistbio { get; set; }
    }
}
