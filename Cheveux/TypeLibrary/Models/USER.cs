﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Models
{
    public class USER
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Password { get; set; }
        public char UserType { get; set; }
        public char Active { get; set; }
        public string UserImage { get; set; }
        public string AccountType { get; set; }
        public char PreferredCommunication { get; set; }
        public string PassRestCode { get; set; }
    }
}
