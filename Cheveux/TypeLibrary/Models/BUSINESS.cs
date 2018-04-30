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
        public string Address { get; set; }
        public string Phone { get; set; }
        public System.DateTime WeekdayStart { get; set; }
        public System.TimeSpan WeekdayEnd { get; set; }
        public System.TimeSpan WeekendStart { get; set; }
        public System.TimeSpan WeekendEnd { get; set; }
        public System.TimeSpan PublicHolStart { get; set; }
        public System.TimeSpan PublicHolEnd { get; set; }
        public byte[] Logo { get; set; }
    }
}
