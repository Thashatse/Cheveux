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

        public SP_CheckForUserType BLL_CheckForUserType(string id)
        {
            return db.CheckForUserType(id);
        }

        public SP_AddUserGoogleAuth BLL_AddUser(USER user)
        {
            return db.AddUser(user);
        }

        public List<SP_ProductSearchByTerm> UniversalSearch(string searchTerm)
        {
            return db.UniversalSearch(searchTerm);
        }

        public USER GetUserDetails(string ID)
        {
            return db.GetUserDetails(ID);
        }
    }
}
