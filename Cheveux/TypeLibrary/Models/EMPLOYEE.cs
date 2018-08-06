using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Models
{
    public class EMPLOYEE
    {
        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string Active { get; set; }
        public byte[] Employee1 { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Bio { get; set; }
    }
}
