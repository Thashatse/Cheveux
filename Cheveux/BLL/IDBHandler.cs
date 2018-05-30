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
        List<SP_GetCustomerBooking> getCustomerUpcomingBookings(string CustomerID);
        SP_GetCustomerBooking getCustomerUpcomingBookingDetails(string BookingID);
        bool deleteBooking(string BookingID);
        List<SP_GetCustomerBooking> getCustomerPastBookings(string CustomerID);
        List<SP_GetEmpNames> BLL_GetEmpNames();
        List<SP_GetEmpAgenda> BLL_GetEmpAgenda(string employeeID);
    }
}
