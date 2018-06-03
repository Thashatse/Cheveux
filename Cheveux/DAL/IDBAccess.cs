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
        Tuple<List<SP_ProductSearchByTerm>, List<SP_SearchStylistsBySearchTerm>> UniversalSearch(string searchTerm);
        USER GetUserDetails(string ID);
        SP_GetCurrentVATate GetVATRate();
        List<SP_GetCustomerBooking> getCustomerUpcomingBookings(string CustomerID);
        SP_GetCustomerBooking getCustomerUpcomingBookingDetails(string BookingID);
        bool deleteBooking(string BookingID);
        List<SP_GetCustomerBooking> getCustomerPastBookings(string CustomerID);
        List<SP_GetEmpNames> GetEmpNames();
        List<SP_GetEmpAgenda> GetEmpAgenda(string employeeID);
        SP_GetCustomerBooking getCustomerPastBookingDetails(string BookingID);
        List<SP_getInvoiceDL> getInvoiceDL(string BookingID);
        EMPLOYEE getEmployeeType(string EmployeeID);
        bool updateBooking(BOOKING bookingUpdate);
        bool CheckIn(BOOKING bookingID);
    }
}
