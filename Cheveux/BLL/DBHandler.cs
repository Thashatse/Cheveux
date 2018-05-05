using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;

namespace BLL
{
    public class DBHandler : IDBHandler
    {
        private IDBAccess db;

        public DBHandler()
        {
            db = new DBAccess();
        }

        public SP_CheckForUser BLL_CheckForUser(string id)
        {
            return db.CheckForUser(id);
        }

        public SP_AddCustomer BLL_AddCustomer(CUSTOMER Cust)
        {
            return db.AddCustomer(Cust);
        }
    }
}
