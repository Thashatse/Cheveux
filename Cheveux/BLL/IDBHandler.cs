using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;

namespace BLL
{
    public interface IDBHandler
    {
        SP_CheckForUserType BLL_CheckForUserType(string id);
        SP_AddUserGoogleAuth BLL_AddUser(USER user);
        List<SP_ProductSearchByTerm> UniversalSearch(string searchTerm);
        USER GetUserDetails(string ID);
        SP_GetCurrentVATate GetVATRate();
        List<SP_GetCustomerUpcomingBooking> getCustomerUpcomingBookings(string CustomerID);
        SP_GetCustomerUpcomingBooking getCustomerUpcomingBookingDetails(string BookingID);
    }
}
