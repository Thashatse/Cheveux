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
        #region Bookings
        List<SP_GetBookingServices> getBookingServices(string BookingID);
        #endregion
        
        #region Home Page Features
        List<HomePageFeatures> GetHomePageFeatures();
        #endregion

        #region Email/SMS Notifications
        List<OGBkngNoti> GetOGBkngNotis();
        bool updateNotiStatus(string bookingID, bool notiStatus);
        #endregion

        #region Invoice/Sale
        List<SP_getInvoiceDL> getInvoiceDL(string BookingID);

        bool createProductSalesDTLRecord(SALES_DTL Sale);

        bool removeProductSalesDTLRecord(SALES_DTL Sale);

        bool UpdateProductSalesDTLRecordQty(SALES_DTL Sale);
        #endregion

        #region User Accounts
        bool updateUserAccountPassword(string password, string userID);
        USER GetAccountForRestCode(string code);
        bool createRestCode(string emailOrUsername, string restCode);
        SP_CheckForUserType BLL_CheckForUserType(string id);
        SP_AddUser BLL_AddUser(USER user);
        bool updateStylistBio(EMPLOYEE bioUpdate);
        #endregion

        #region Authentication
        USER getPasHash(string identifier);

        USER logInEmail(string identifier, string password);
        #endregion

        #region Products
        PRODUCT CheckForProduct(string id);
        #endregion

        #region Services
        bool BLL_AddService(PRODUCT p, SERVICE s);
        List<SP_GetWidth> BLL_GetWidths();
        List<SP_GetLength> BLL_GetLengths();
        List<SP_GetStyles> BLL_GetStyles();
        bool BLL_AddBraidService(BRAID_SERVICE bs);
        #endregion

        Tuple<List<SP_ProductSearchByTerm>, List<SP_SearchStylistsBySearchTerm>> UniversalSearch(string searchTerm);
        USER GetUserDetails(string ID);
        SP_GetCurrentVATate GetVATRate();
        List<SP_GetCustomerBooking> getCustomerUpcomingBookings(string CustomerID);
        SP_GetCustomerBooking getCustomerUpcomingBookingDetails(string BookingID);
        List<SP_GetStylistBookings> getStylistPastBookings(string empID, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getStylistPastBookingsDateRange(string empID, DateTime startDate, DateTime endDate, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getStylistUpcomingBookings(string empID, string sortBy, string sortDir);
        bool deleteBooking(string BookingID);
        List<SP_GetCustomerBooking> getCustomerPastBookings(string CustomerID);
        SP_GetCustomerBooking getCustomerPastBookingDetails(string BookingID);
        List<SP_GetEmpNames> BLL_GetEmpNames();
        List<SP_GetEmpAgenda> BLL_GetEmpAgenda(string employeeID, DateTime bookingDate, string sortBy, string sortDir);
        EMPLOYEE getEmployeeType(string EmployeeID);
        bool updateBooking(BOOKING bookingUpdate);
        bool BLL_CheckIn(BOOKING booking);
        bool updateUser(USER userUpdate);
        SP_GetAllofBookingDTL BLL_GetAllofBookingDTL(string bookingID, string customerID);
        SP_GetBookingServiceDTL BLL_GetBookingServiceDTL(string bookingID, string customerID);
        SP_ViewCustVisit BLL_ViewCustVisit(string customerID, string bookingID);
        bool BLL_UpdateCustVisit(CUST_VISIT visit, BOOKING b);
        bool BLL_CreateCustVisit(CUST_VISIT cust_visit);
        bool BLL_AddBooking(BOOKING addBooking);
        bool BLL_AddToBookingService(BookingService bs);
        List<SP_GetBookedTimes> BLL_GetBookedStylistTimes(string stylistID, DateTime bookingDate);
        List<SP_GetSlotTimes> BLL_GetAllTimeSlots();
        List<SP_GetServices> BLL_GetAllServices();
        List<SP_GetStylists> BLL_GetStylists();
        List<SP_GetMyNextCustomer> BLL_GetMyNextCustomer(string employeeID, DateTime bookingDate);
        bool createSalesDTLRecord(SALES_DTL detailLine);
        SP_GetCustomerBooking getBookingDetaisForCheckOut(string BookingID);
        bool createSalesRecord(string bookingID);
        string getSalePaymentType(String SaleID);
        bool addPaymentTypeToSalesRecord(string paymentType, string saleID);
        BUSINESS getBusinessTable();
        bool updateVatRate(string bussinesID, int vatRate);
        bool updateVatRegNo(string bussinesID, string vatRegNo);
        bool updateAddress(string bussinesID, string addresLine1, string addressLine2);
        bool updateWeekdayHours(string bussinesID, DateTime wDStart, DateTime wDEnd);
        bool updateWeekendHours(string bussinesID, DateTime wEStart, DateTime wEEnd);
        bool updatePublicHolidayHours(string bussinesID, DateTime pHStart, DateTime pHEnd);
        bool updatePhoneNumber(string bussinesID, string PhoneNumber);
        SP_ViewEmployee viewEmployee(string empID);
        SP_ViewStylistSpecialisationAndBio viewStylistSpecialisationAndBio(string empID);
        List<SP_ViewEmployee> viewAllEmployees();
        List<SP_GetEmployeeTypes> getEmpTypes();
        List<PRODUCT> getAllProducts();
        List<SP_GetProductTypes> getProductTypes();
        Tuple<List<SP_GetAllAccessories>, List<SP_GetAllTreatments>> getAllProductsAndDetails();
        List<SP_GetTodaysBookings> getTodaysBookings();
        USER checkForAccountTypeEmail(string identifier);
        List<SP_UserList> userList();
        bool addEmployee(string empID, string bio, string ad1, string ad2, string suburb, string city, string firstname
                                , string lastname, string username, string email, string contactNo, string password,
                                string userimage, string passReset);
        bool updateEmployee(EMPLOYEE emp);
        List<SP_BookingsReportForHairstylist> getBookingsReportForHairstylist(string stylistID);
        List<SP_BookingsReportForHairstylist> getBookingReportForHairstylistWithDateRange(string stylistID, DateTime startDate, DateTime endDate);
        List<SP_SaleOfHairstylist> getSaleOfHairstylist(string stylistID, DateTime startDate, DateTime endDate);
        List<SP_GetStylists> BLL_GetAllStylists();
        bool deactivateUser(string userID);
        List<SP_AboutStylist> aboutStylist();
        List<SP_GetStylistBookings> getStylistUpcomingBookingsDR(string empID, DateTime startDate, DateTime endDate, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getAllStylistsUpcomingBksForDate(DateTime bookingDate, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getAllStylistsUpcomingBksDR(DateTime startDate, DateTime endDate, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getAllStylistsUpcomingBookings(string sortBy, string sortDir);
        List<SP_GetStylistBookings> getAllStylistsPastBookings(string sortBy, string sortDir);
        List<SP_GetStylistBookings> getAllStylistsPastBookingsDateRange(DateTime startDate, DateTime endDate, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getAllStylistsPastBksForDate(DateTime bookingDate, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getStylistPastBksForDate(string empID, DateTime day, string sortBy, string sortDir);
        //bool addAccessories(ACCESSORY a);
        //bool addTreatments(TREATMENT t);
    }
}


