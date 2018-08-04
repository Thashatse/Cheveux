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
        #region Home Page Features
        List<HomePageFeatures> GetHomePageFeatures();
        #endregion

        #region User Accounts
        SP_CheckForUserType CheckForUserType(string id);
        SP_AddUser AddUser(USER User);
        USER GetUserDetails(string ID);
        EMPLOYEE getEmployeeType(string EmployeeID);
        bool updateUser(USER userUpdate);
        bool addEmployee(EMPLOYEE e);
        bool updateEmployee(EMPLOYEE emp);
        #endregion

        #region Bookings
        List<SP_GetCustomerBooking> getCustomerUpcomingBookings(string CustomerID);
        SP_GetCustomerBooking getCustomerUpcomingBookingDetails(string BookingID);
        bool deleteBooking(string BookingID);
        List<SP_GetCustomerBooking> getCustomerPastBookings(string CustomerID);
        SP_GetCustomerBooking getCustomerPastBookingDetails(string BookingID);
        bool updateBooking(BOOKING bookingUpdate);
        bool CheckIn(BOOKING bookingID);
        SP_GetAllofBookingDTL GetAllofBookingDTL(string bookingID, string customerID);
        SP_GetBookingServiceDTL GetBookingServiceDTL(string bookingID, string customerID);
        SP_ViewCustVisit ViewCustVisit(string customerID, string bookingID);
        bool UpdateCustVisit(CUST_VISIT visit);
        bool CreateCustVisit(CUST_VISIT cust_visit);
        List<SP_GetStylistBookings> getStylistPastBookings(string empID);
        List<SP_GetStylistBookings> getStylistPastBookingsDateRange(string empID, DateTime startDate, DateTime endDate);
        List<SP_GetStylistBookings> getStylistUpcomingBookings(string empID);
        #endregion

        #region search
        Tuple<List<SP_ProductSearchByTerm>, List<SP_SearchStylistsBySearchTerm>> UniversalSearch(string searchTerm);
        #endregion

        #region Functions
        SP_GetCurrentVATate GetVATRate();
        #endregion

        #region Invoice/Sale
        List<SP_getInvoiceDL> getInvoiceDL(string BookingID);

        bool createProductSalesDTLRecord(SALES_DTL Sale);

        bool removeProductSalesDTLRecord(SALES_DTL Sale);
        #endregion

        #region Email/SMS Notifications
        List<OGBkngNoti> GetOGBkngNotis();
        bool updateNotiStatus(string bookingID, bool notiStatus);
        #endregion

        List<SP_GetEmpNames> GetEmpNames();
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
        bool deactivateUser(string userID);
        bool AddBooking(BOOKING addBooking);
        List<SP_GetServices> GetAllServices();
        List<SP_GetStylists> GetAllStylists();
        List<SP_GetBookedTimes> GetBookedStylistTimes(string stylistID, DateTime bookingDate);
        List<SP_GetSlotTimes> GetAllTimeSlots();
        Tuple<List<SP_GetAllAccessories>, List<SP_GetAllTreatments>> getAllProductsAndDetails();
        List<SP_GetTodaysBookings> getTodaysBookings();
        USER checkForAccountTypeEmail(string identifier);
        USER logInEmail(string identifier, string password);
        List<SP_UserList> userList();
        List<SP_BookingsReportForHairstylist> getBookingsReportForHairstylist(string stylistID);
        List<SP_BookingsReportForHairstylist> getBookingReportForHairstylistWithDateRange(string stylistID, DateTime startDate, DateTime endDate);
        List<SP_SaleOfHairstylist> getSaleOfHairstylist (string stylistID, DateTime startDate, DateTime endDate);
    }
}
