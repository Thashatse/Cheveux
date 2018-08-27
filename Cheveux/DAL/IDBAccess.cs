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
        bool addEmployee(string empID, string bio, string ad1, string ad2, string suburb, string city, string firstname
                                , string lastname, string username, string email, string contactNo, string password,
                                string userimage, string passReset);
        bool updateEmployee(EMPLOYEE emp);
        bool createRestCode(string emailOrUsername, string restCode);
        USER GetAccountForRestCode(string code);
        bool updateUserAccountPassword(string password, string userID);
        bool updateStylistBio(EMPLOYEE bioUpdate);
        bool updateService(PRODUCT p, SERVICE s);
        #endregion

        #region Authentication
        USER getPasHash(string identifier);

        USER logInEmail(string identifier, string password);
        #endregion

        #region Bookings
        bool deleteBookingService(string BookingID, string ServiceID);
        List<SP_GetBookingServices> getBookingServices(string BookingID);
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
        bool UpdateCustVisit(CUST_VISIT visit, BOOKING b);
        bool CreateCustVisit(CUST_VISIT cust_visit);
        List<SP_GetStylistBookings> getStylistPastBookings(string empID, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getStylistUpcomingBookings(string empID, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getStylistPastBksForDate(string empID, DateTime day, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getStylistPastBookingsDateRange(string empID, DateTime startDate, DateTime endDate, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getStylistUpcomingBookingsDR(string empID, DateTime startDate, DateTime endDate, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getAllStylistsUpcomingBksForDate(DateTime bookingDate, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getAllStylistsUpcomingBksDR(DateTime startDate, DateTime endDate, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getAllStylistsUpcomingBookings(string sortBy, string sortDir);
        List<SP_GetStylistBookings> getAllStylistsPastBookings(string sortBy, string sortDir);
        List<SP_GetStylistBookings> getAllStylistsPastBookingsDateRange(DateTime startDate, DateTime endDate, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getAllStylistsPastBksForDate(DateTime bookingDate, string sortBy, string sortDir);
        bool deleteSecondaryBooking(string BookingID);
        SP_GetMultipleServicesTime getMultipleServicesTime(string primaryBookingID);
        #endregion

        #region search
        Tuple<List<SP_ProductSearchByTerm>, List<SP_SearchStylistsBySearchTerm>> UniversalSearch(string searchTerm);
        #endregion

        #region Functions
        SP_GetCurrentVATate GetVATRate();
        #endregion

        #region Invoice/Sale
        SALE getSale(string SaleID);

        List<SP_getInvoiceDL> getInvoiceDL(string BookingID);

        bool createProductSalesDTLRecord(SALES_DTL Sale);

        bool removeProductSalesDTLRecord(SALES_DTL Sale);

        bool UpdateProductSalesDTLRecordQty(SALES_DTL Sale);
        #endregion

        #region Email/SMS Notifications
        List<OGBkngNoti> GetOGBkngNotis();
        bool updateNotiStatus(string bookingID, bool notiStatus);
        #endregion

        #region Products
        PRODUCT CheckForProduct(string id);
        //bool addAccessories(ACCESSORY a);
        //bool addTreatments(TREATMENT t);

        #endregion

        #region Services
        bool AddService(PRODUCT p, SERVICE s);
        bool AddBraidService(BRAID_SERVICE bs);
        List<SP_GetWidth> GetWidths();
        List<SP_GetLength> GetLengths();
        List<SP_GetStyles> GetStyles();
        SP_GetAllAccessories selectAccessory(string accessoryID);
        SP_GetAllTreatments selectTreatment(string treatmentID);
        #endregion

        #region Manager Dash Board
        ManagerStats GetManagerStats();
        #endregion

        List<SP_GetEmpNames> GetEmpNames();
        List<SP_GetEmpAgenda> GetEmpAgenda(string employeeID, DateTime bookingDate, string sortBy, string sortDir);
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
        SP_ViewStylistSpecialisationAndBio viewStylistSpecialisationAndBio(string empID);
        List<SP_ViewEmployee> viewAllEmployees();
        List<SP_GetEmployeeTypes> getEmpTypes();
        List<PRODUCT> getAllProducts();
        List<SP_GetProductTypes> getProductTypes();
        bool deactivateUser(string userID);
        bool AddBooking(BOOKING addBooking);
        bool AddToBookingService(BookingService bs);
        List<SP_GetServices> GetAllServices();
        List<SP_GetStylists> GetAllStylists();
        List<SP_GetBookedTimes> GetBookedStylistTimes(string stylistID, DateTime bookingDate);
        List<SP_GetSlotTimes> GetAllTimeSlots();
        Tuple<List<SP_GetAllAccessories>, List<SP_GetAllTreatments>> getAllProductsAndDetails();
        List<SP_GetTodaysBookings> getTodaysBookings();
        USER checkForAccountTypeEmail(string identifier);
        List<SP_UserList> userList();
        List<SP_BookingsReportForHairstylist> getBookingsReportForHairstylist(string stylistID);
        List<SP_BookingsReportForHairstylist> getBookingReportForHairstylistWithDateRange(string stylistID, DateTime startDate, DateTime endDate);
        List<SP_SaleOfHairstylist> getSaleOfHairstylist(string stylistID, DateTime startDate, DateTime endDate);
        List<SP_AboutStylist> aboutStylist();
        /*
        bool AddService(PRODUCT p, SERVICE s);
        */
      
        //bool addAccessories(ACCESSORY a);
        //bool addTreatments(TREATMENT t);
        SERVICE GetSlotLength(string serviceID);

    }
    }
