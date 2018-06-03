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
        Tuple<List<SP_ProductSearchByTerm>, List<SP_SearchStylistsBySearchTerm>> UniversalSearch(string searchTerm);
        USER GetUserDetails(string ID);
        SP_GetCurrentVATate GetVATRate();
        List<SP_GetCustomerBooking> getCustomerUpcomingBookings(string CustomerID);
        SP_GetCustomerBooking getCustomerUpcomingBookingDetails(string BookingID);
        bool deleteBooking(string BookingID);
        List<SP_GetCustomerBooking> getCustomerPastBookings(string CustomerID);
        SP_GetCustomerBooking getCustomerPastBookingDetails(string BookingID);
        List<SP_getInvoiceDL> getInvoiceDL(string BookingID);
        List<SP_GetEmpNames> BLL_GetEmpNames();
        List<SP_GetEmpAgenda> BLL_GetEmpAgenda(string employeeID);
        EMPLOYEE getEmployeeType(string EmployeeID);
        bool updateBooking(BOOKING bookingUpdate);
        bool updateUser(USER userUpdate);
    }
}
