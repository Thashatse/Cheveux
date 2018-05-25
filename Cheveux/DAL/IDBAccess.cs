using System;
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
        SP_CheckForUserType CheckForUserType(string id);
        SP_AddUserGoogleAuth AddUser(USER User);
        List<SP_ProductSearchByTerm> UniversalSearch(string searchTerm);
        USER GetUserDetails(string ID);
        SP_GetCurrentVATate GetVATRate();
    }
}
