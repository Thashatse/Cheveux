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
        bool deleteBookingService(string BookingID, string ServiceID);
        bool deleteSecondaryBooking(string BookingID);
        SP_GetMultipleServicesTime getMultipleServicesTime(string primaryBookingID);
        #endregion

        #region Home Page Features
        List<HomePageFeatures> GetHomePageFeatures();

        bool UpdatedHomePageFeatures(Home_Page UpdateFeature);
        #endregion

        #region Email/SMS Notifications
        List<OGBkngNoti> GetOGBkngNotis();
        bool updateNotiStatus(string bookingID, bool notiStatus);
        #endregion

        #region Invoice/Sale
        SALE getSale(string SaleID);

        List<SP_getInvoiceDL> getInvoiceDL(string BookingID);

        bool createProductSalesDTLRecord(SALES_DTL Sale);

        bool removeProductSalesDTLRecord(SALES_DTL Sale);

        bool UpdateProductSalesDTLRecordQty(SALES_DTL Sale);

        bool createSalesRecord(SALE newSale);
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
        SP_GetAllAccessories selectAccessory(string accessoryID);
        SP_GetAllTreatments selectTreatment(string treatmentID);
        bool addProduct(PRODUCT addProduct);
        List<SP_GetBrandsForProductType> getBrandsForProductType(char type);
        #endregion

        #region ProductTypes
        bool addProductType(ProductType newType);

        bool editProductType(ProductType updateType);
        #endregion

        #region Product Orders
        OrderViewModel getOrder(string orderID);

        List<OrderViewModel> getOutStandingOrders();

        List<OrderViewModel> getPastOrders();

        List<OrderViewModel> getProductOrderDL(string orderID);

        bool newProductOrder(Order newOrder);

        bool newProductOrderDL(Order_DTL newOrderDL);

        Order CheckForOrder(string id);
        #endregion

        #region Auto Product Orders
        List<SP_GetAuto_Purchase_Products> getAutoPurchOrdProds();
        bool newAutoPurchProd(Auto_Purchase_Products newProduct);
        bool deleteAutoPurchProd(Auto_Purchase_Products product);
        #endregion

        #region Stock Managment Settings
        Stock_Management getStockSettings();

        bool updateStockSettings(Stock_Management Update);
        #endregion

        #region Services
        bool BLL_AddService(PRODUCT p, SERVICE s);
        List<SP_GetWidth> BLL_GetWidths();
        List<SP_GetLength> BLL_GetLengths();
        List<SP_GetStyles> BLL_GetStyles();
        bool BLL_AddBraidService(BRAID_SERVICE bs);
        bool updateService(PRODUCT p, SERVICE s);
                SP_GetService BLL_GetServiceFromID(string serviceID);
        SP_GetBraidService BLL_GetBraidServiceFromID(string serviceID);
        #endregion

        #region Brands
        List<BRAND> getAllBrands();
        BRAND getBrand(string BrandID);
        bool newBrand(BRAND newBrand);
        BRAND CheckForBrand(string id);
        bool editBrand(BRAND brandUpdate);
        #endregion

        #region Supplier
        List<Supplier> getSuppliers();

        Supplier getSupplier(string suppID);

        bool newSupplier(Supplier newSupp);

        bool editSupplier(Supplier suppUpdate);

        Supplier CheckForSupplier(string id);
        #endregion

        #region Manager Dash Board
        ManagerStats GetManagerStats();
        #endregion

        #region search
        List<SP_GetCustomerBooking> searchBookings(DateTime startDate, DateTime endDate);
        Tuple<List<SP_ProductSearchByTerm>, List<SP_SearchStylistsBySearchTerm>> UniversalSearch(string searchTerm);
        #endregion

        #region Reviews
        List<SP_GetReviews> getAllBookingReviews();
        List<SP_GetReviews> getAllStylistReviews();
        List<SP_GetReviews> getCustomersReviews(string customerID);
        List<SP_GetReviews> getReviewsOfStylist(string stylistID);
        bool reviewBooking(REVIEW r);
        bool reviewStylist(REVIEW r);
        bool updateStylistReview(REVIEW r);
        bool updateBookingReview(REVIEW r);
        REVIEW CheckForReview(string reviewID);
        REVIEW customersReviewForStylist(string customerID, string stylistID);
        REVIEW customersReviewForBooking(string customerID, string bookingID);
        List<SP_ReturnStylistNamesForReview> returnStylistNamesForReview(string customerID);
        REVIEW getStylistRating(string stylistID);
        List<SP_GetCustomerBooking> getCustRecentBookings(string CustomerID);
        #endregion

        #region Report
        List<productSalesReport> getProductSalesVolumeAll(DateTime startDate, DateTime endDate);
        List<productSalesReport> getProductSalesVolumeCredit(DateTime startDate, DateTime endDate);
        List<productSalesReport> getProductSalesVolumeCash(DateTime startDate, DateTime endDate);
        List<productSalesReport> getProductSalesValueCredit(DateTime startDate, DateTime endDate);
        List<productSalesReport> getProductSalesValueCash(DateTime startDate, DateTime endDate);
        List<productSalesReport> getProductSalesValueAll(DateTime startDate, DateTime endDate);

        List<productSalesReport> getServiceSalesVolumeAll(DateTime startDate, DateTime endDate);
        List<productSalesReport> getServiceSalesVolumeCredit(DateTime startDate, DateTime endDate);
        List<productSalesReport> getServiceSalesVolumeCash(DateTime startDate, DateTime endDate);
        List<productSalesReport> getServiceSalesValueCredit(DateTime startDate, DateTime endDate);
        List<productSalesReport> getServiceSalesValueCash(DateTime startDate, DateTime endDate);
        List<productSalesReport> getServiceSalesValueAll(DateTime startDate, DateTime endDate);

        List<productSalesReport> getCustomerSalesVolumeAll(DateTime startDate, DateTime endDate);
        List<productSalesReport> getCustomerSalesVolumeCredit(DateTime startDate, DateTime endDate);
        List<productSalesReport> getCustomerSalesVolumeCash(DateTime startDate, DateTime endDate);
        List<productSalesReport> getCustomerSalesValueCredit(DateTime startDate, DateTime endDate);
        List<productSalesReport> getCustomerSalesValueCash(DateTime startDate, DateTime endDate);
        List<productSalesReport> getCustomerSalesValueAll(DateTime startDate, DateTime endDate);

        List<productSalesReport> getSalesGauge(string ProductID);
        List<SP_TotalBksMissedByCustomers> returnTotalbksMissedbyCustomers(DateTime startDate, DateTime endDate);
        List<SP_GetReviews> mostPopularStylist(DateTime startDate, DateTime endDate);
        List<SP_GetReviews> customerSatistfaction(DateTime startDate, DateTime endDate);
        #endregion

        USER getManagerContact();
        USER GetUserDetails(string ID);
        SP_GetCurrentVATate GetVATRate();
        List<SP_GetCustomerBooking> getCustomerUpcomingBookings(string CustomerID);
        SP_GetCustomerBooking getCustomerUpcomingBookingDetails(string BookingID);
        List<SP_GetStylistBookings> getStylistPastBookings(string empID, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getStylistPastBookingsDateRange(string empID, DateTime startDate, DateTime endDate, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getStylistUpcomingBookings(string empID, string sortBy, string sortDir);
        List<SP_GetStylistBookings> getStylistUpcomingBkForDate(string empID, DateTime day, string sortBy, string sortDir);
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
        bool createSalesRecordForBooking(string bookingID);
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
        List<ProductType> getProductTypes();
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
        bool addAccessories(ACCESSORY a, PRODUCT p );
        bool addTreatments(TREATMENT t, PRODUCT p);
        SERVICE BLL_GetSlotLength(string serviceID);
        SP_GetEmployee_S_ getEmployee_S(string stylistID);
        bool addSpecialisation(STYLIST_SERVICE ss);
        SP_GetEmployee_S_ getBio(string id);
        List<SP_GetTopCustomerbyBooking> getTopCustomerByBookings(DateTime startDate, DateTime endDate);
        List<SP_GetStylistBookings> getCustomerPastBookingsForDate(string customerID, DateTime day);
        List<SP_GetLeaveServices> BLL_GetLeaveServices();
        bool BLL_UpdateOrder(string orderID, DateTime dateReceived, bool received);
        bool BLL_UpdateQtyOnHand(string prodID, int qty);
        SP_ReturnBooking returnNextBooking(string startTime, string bookingID, string stylistID, DateTime date);
        SP_ReturnBooking returnBooking(string bookingID, string customerID, string stylistID, DateTime date);
        List<SP_ReturnAvailServices> returnAvailServices(int num);
    }
}

