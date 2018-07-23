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
        SP_AddUser AddUser(USER User);
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
        bool addPaymentTypeToSalesRecord(string paymentType, string saleID);
        string getSalePaymentType(String SaleID);
        BUSINESS getBusinessTable();
        bool updateVatRate(string bussinesID, int vatRate);
        bool updateVatRegNo(string bussinesID, string vatRegNo);
        bool updateAddress(string bussinesID, string addresLine1, string addressLine2);
        bool updateWeekdayHours(string bussinesID, DateTime wDStart, DateTime wDEnd);
        bool updateWeekendHours(string bussinesID, DateTime wEStart, DateTime wEEnd);
        bool updatePublicHolidayHours(string bussinesID, DateTime pHStart, DateTime pHEnd);
        bool updatePhoneNumber(string bussinesID, string PhoneNumber);
        SP_ViewEmployee viewEmployee(string empID);
        SP_ViewStylistSpecialisation viewStylistSpecialisation(string empID);
        List<SP_ViewEmployee> viewAllEmployees();
        List<SP_GetEmployeeTypes> getEmpTypes();
        List<PRODUCT> getAllProducts();
        List<SP_GetProductTypes> getProductTypes();

        /*        bool AddBooking(BOOKING addBooking);
                List<SP_GetServices> GetAllServices();
                List<SP_GetStylists> GetStylistsForService(string serviceID);*/

        Tuple<List<SP_GetAllAccessories>, List<SP_GetAllTreatments>> getAllProductsAndDetails();
        List<SP_GetTodaysBookings> getTodaysBookings();
        USER checkForAccountTypeEmail(string identifier);
        USER logInEmail(string identifier, string password);
        List<SP_UserList> userList();
        List<SP_SearchForUser> searchForUser(string term);
        bool addEmployee(EMPLOYEE e);
        bool updateEmployee(EMPLOYEE emp);
        List<SP_BookingsReportForHairstylist> getBookingsReportForHairstylist(string stylistID);
        List<SP_BookingsReportForHairstylist> getBookingReportForHairstylistWithDateRange(string stylistID, DateTime startDate, DateTime endDate);
        List<SP_SaleOfHairstylist> getSaleOfHairstylist (string stylistID);
    }
}
