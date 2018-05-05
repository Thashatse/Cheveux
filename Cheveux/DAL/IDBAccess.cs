﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;

namespace DAL
{
    public interface IDBAccess
    {
        SP_CheckForUser CheckForUser(string id);
        SP_AddCustomer AddCustomer(CUSTOMER Cust);
    }
}
