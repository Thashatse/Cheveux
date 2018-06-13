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
        SP_GetCustomerBooking getCustomerPastBookingDetails(string BookingID);
        List<SP_getInvoiceDL> getInvoiceDL(string BookingID);
        EMPLOYEE getEmployeeType(string EmployeeID);
        bool updateBooking(BOOKING bookingUpdate);
        bool updateUser(USER userUpdate);
        bool CheckIn(BOOKING bookingID);
        SP_GetAllofBookingDTL GetAllofBookingDTL(string bookingID, string customerID);
        SP_GetBookingServiceDTL GetBookingServiceDTL(string bookingID, string customerID);
        SP_ViewCustVisit ViewCustVisit(string customerID, string bookingID);
        bool UpdateCustVisit(CUST_VISIT visit);
        bool CreateCustVisit(CUST_VISIT cust_visit);
        List<SP_GetEmpAgenda> GetEmpAgenda(string employeeID, DateTime bookingDate);
        List<SP_GetMyNextCustomer> GetMyNextCustomer(string employeeID, DateTime bookingDate);
        SP_GetCustomerBooking getBookingDetaisForCheckOut(string BookingID);
        bool createSalesRecord(string bookingID);
        bool createSalesDTLRecord(SALES_DTL detailLine);
        string getSalePaymentType(String SaleID);
        /*        bool AddBooking(BOOKING addBooking);
                List<SP_GetServices> GetAllServices();
                List<SP_GetStylists> GetStylistsForService(string serviceID);*/
    }
}
