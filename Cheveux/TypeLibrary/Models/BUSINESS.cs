using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Models
{
    public class BUSINESS
    {
        public string BusinessID { get; set; }
        public int Vat { get; set; }
        public string VatRegNo { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Phone { get; set; }
        public DateTime WeekdayStart { get; set; }
        public DateTime WeekdayEnd { get; set; }
        public DateTime WeekendStart { get; set; }
        public DateTime WeekendEnd { get; set; }
        public DateTime PublicHolStart { get; set; }
        public DateTime PublicHolEnd { get; set; }
        public byte[] Logo { get; set; }
    }
}
