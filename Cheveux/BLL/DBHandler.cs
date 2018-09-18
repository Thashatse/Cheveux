using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;

namespace BLL
{
    public class DBHandler : IDBHandler
    {
        private IDBAccess db;

        public DBHandler()
        {
            db = new DBAccess();
        }

        #region Home Page Features
        public List<HomePageFeatures> GetHomePageFeatures()
        {
            return db.GetHomePageFeatures();
        }
        #endregion

        #region Email/SMS Notifications
        public List<OGBkngNoti> GetOGBkngNotis()
        {
            return db.GetOGBkngNotis();
        }

        public bool updateNotiStatus(string bookingID, bool notiStatus)
        {
            return db.updateNotiStatus(bookingID, notiStatus);
        }
        #endregion

        #region Invoice/Sale
        public SALE getSale(string SaleID)
        {
            return db.getSale(SaleID);
        }

        public bool createProductSalesDTLRecord(SALES_DTL Sale)
        {
            return db.createProductSalesDTLRecord(Sale);
        }

        public bool removeProductSalesDTLRecord(SALES_DTL Sale)
        {
            return db.removeProductSalesDTLRecord(Sale);
        }

        public bool UpdateProductSalesDTLRecordQty(SALES_DTL Sale)
        {
            return db.UpdateProductSalesDTLRecordQty(Sale);
        }
        #endregion

        #region User Accounts
        public bool updateStylistBio(EMPLOYEE bioUpdate)
        {
            return db.updateStylistBio(bioUpdate);
        }

        public bool updateUserAccountPassword(string password, string userID)
        {
            return db.updateUserAccountPassword(password, userID);
        }

        public USER GetAccountForRestCode(string code)
        {
            return db.GetAccountForRestCode(code);
        }

        public bool createRestCode(string emailOrUsername, string restCode)
        {
            return db.createRestCode(emailOrUsername, restCode);
        }

        public bool deactivateUser(string userID)
        {
            return db.deactivateUser(userID);
        }

        public USER checkForAccountTypeEmail(string identifier)
        {
            return db.checkForAccountTypeEmail(identifier);
        }
        #endregion

        #region Authentication
        public USER getPasHash(string identifier)
        {
            return db.getPasHash(identifier);
        }

        public USER logInEmail(string identifier, string password)
        {
            return db.logInEmail(identifier, password);
        }
        #endregion

        #region Products
        public PRODUCT CheckForProduct(string id)
        {
            return db.CheckForProduct(id);
        }


        public bool addAccessories(ACCESSORY a, PRODUCT p)
        {
            return db.addAccessories(a, p);
        }

        public bool addTreatments(TREATMENT t, PRODUCT p)
        {
            return db.addTreatments(t, p);
        }

        public SP_GetAllAccessories selectAccessory(string accessoryID)
        {
            return db.selectAccessory(accessoryID);
        }

        public SP_GetAllTreatments selectTreatment(string treatmentID)
        {
            return db.selectTreatment(treatmentID);
        }

        //addProduct
        public bool addProduct(PRODUCT addProduct)
        {
            return db.addProduct(addProduct);
        }

        public List<SP_GetBrandsForProductType> getBrandsForProductType(char type)
        {
            return db.getBrandsForProductType(type);
        }
        #endregion

        #region Product Orders
        public OrderViewModel getOrder(string orderID)
        {
            return db.getOrder(orderID);
        }

        public List<OrderViewModel> getOutStandingOrders()
        {
            return db.getOutStandingOrders();
        }

        public List<OrderViewModel> getPastOrders()
        {
            return db.getPastOrders();
        }

        public List<OrderViewModel> getProductOrderDL(string orderID)
        {
            return db.getProductOrderDL(orderID);
        }
        
        public List<Supplier> getSuppliers()
        {
            return db.getSuppliers();
        }

        public Supplier getSupplier(string suppID)
        {
            return db.getSupplier(suppID);
        }
        #endregion

        #region Bookings
        public List<SP_GetBookingServices> getBookingServices(string bookingID)
        {
            return db.getBookingServices(bookingID);
        }

        public bool deleteBookingService(string BookingID, string ServiceID)
        {
            return db.deleteBookingService(BookingID, ServiceID);
        }

        public bool deleteSecondaryBooking(string BookingID)
        {
            return db.deleteSecondaryBooking(BookingID);
        }
        public SP_GetMultipleServicesTime getMultipleServicesTime(string primaryBookingID)
        {
            return db.getMultipleServicesTime(primaryBookingID);
        }
        #endregion

        #region Services

        public bool BLL_AddService(PRODUCT p, SERVICE s)
        {
            return db.AddService(p, s);
        }
        

        public List<SP_GetWidth> BLL_GetWidths()
        {
            return db.GetWidths();
        }

        public List<SP_GetLength> BLL_GetLengths()
        {
            return db.GetLengths();
        }

        public List<SP_GetStyles> BLL_GetStyles()
        {
            return db.GetStyles(); 
        }
        
        public bool BLL_AddBraidService(BRAID_SERVICE bs)
        {
            return db.AddBraidService(bs);
        }
       public SP_GetBraidService BLL_GetBraidServiceFromID(string serviceID)
        {
            return db.GetBraidServiceFromID(serviceID);
        }
        public SP_GetService BLL_GetServiceFromID(string serviceID)
        {
            return db.GetServiceFromID(serviceID);
        } 
        #endregion

        #region Manager Dash Board
        public ManagerStats GetManagerStats()
        {
            return db.GetManagerStats();
        }
        #endregion

        #region search
        public Tuple<List<SP_ProductSearchByTerm>, List<SP_SearchStylistsBySearchTerm>> UniversalSearch(string searchTerm)
        {
            return db.UniversalSearch(searchTerm);
        }

        public List<SP_GetCustomerBooking> searchBookings(DateTime startDate, DateTime endDate)
        {
            return db.searchBookings(startDate, endDate);
        }
        #endregion

        public List<SP_GetTodaysBookings> getTodaysBookings() 
        {
            return db.getTodaysBookings();
        }

        public SP_ViewStylistSpecialisationAndBio viewStylistSpecialisationAndBio(string empID)
        {
            return db.viewStylistSpecialisationAndBio(empID);
        }

        public SP_ViewEmployee viewEmployee(string empID)
        {
            return db.viewEmployee(empID);
        }

        public BUSINESS getBusinessTable()
        {
            return db.getBusinessTable();
        }

        public bool addPaymentTypeToSalesRecord(string paymentType, string saleID)
        {
            return db.addPaymentTypeToSalesRecord(paymentType, saleID);
        }

        public string getSalePaymentType(String SaleID)
        {
            return db.getSalePaymentType(SaleID);
        }

        public bool createSalesDTLRecord(SALES_DTL detailLine)
        {
            return db.createSalesDTLRecord(detailLine);
        }

        public SP_CheckForUserType BLL_CheckForUserType(string id)
        {
            return db.CheckForUserType(id);
        }

        public SP_AddUser BLL_AddUser(USER user)
        {
            return db.AddUser(user);
        }

        public Tuple<List<SP_GetAllAccessories>, List<SP_GetAllTreatments>> getAllProductsAndDetails()
        {
            return db.getAllProductsAndDetails();
        }

        public USER GetUserDetails(string ID)
        {
            return db.GetUserDetails(ID);
        }

        public SP_GetCurrentVATate GetVATRate()
        {
            return db.GetVATRate();
        }

        public List<SP_GetCustomerBooking> getCustomerUpcomingBookings(string CustomerID)
        {
            return db.getCustomerUpcomingBookings(CustomerID);
        }

        public SP_GetCustomerBooking getCustomerUpcomingBookingDetails(string BookingID)
        {
            return db.getCustomerUpcomingBookingDetails(BookingID);
        }

        public bool deleteBooking(string BookingID)
        {
            return db.deleteBooking(BookingID);
        }

        public List<SP_GetCustomerBooking> getCustomerPastBookings(string CustomerID)
        {
            return db.getCustomerPastBookings(CustomerID);
        }

        public List<SP_GetEmpNames> BLL_GetEmpNames()
        {
            return db.GetEmpNames();
        }

        public List<SP_GetEmpAgenda> BLL_GetEmpAgenda(string employeeID, DateTime bookingDate, string sortBy, string sortDir)
        {
            return db.GetEmpAgenda(employeeID, bookingDate,sortBy,sortDir);
        }

        public SP_GetCustomerBooking getCustomerPastBookingDetails(string BookingID)
        {
            return db.getCustomerPastBookingDetails(BookingID);
        }

        public List<SP_getInvoiceDL> getInvoiceDL(string BookingID)
        {
            return db.getInvoiceDL(BookingID);
        }

        public EMPLOYEE getEmployeeType(string EmployeeID)
        {
            return db.getEmployeeType(EmployeeID);
        }

        public bool updateBooking(BOOKING bookingUpdate)
        {
            return db.updateBooking(bookingUpdate);
        }

        public bool updateUser(USER userUpdate)
        {
            return db.updateUser(userUpdate);
        }
        public bool BLL_CheckIn(BOOKING booking)
        {
            return db.CheckIn(booking);
        }
        public SP_GetAllofBookingDTL BLL_GetAllofBookingDTL(string bookingID, string customerID)
        {
            return db.GetAllofBookingDTL(bookingID, customerID);
        }
        public SP_GetBookingServiceDTL BLL_GetBookingServiceDTL(string bookingID, string customerID)
        {
            return db.GetBookingServiceDTL(bookingID, customerID);
        }
        public SP_ViewCustVisit BLL_ViewCustVisit(string customerID, string bookingID)
        {
            return db.ViewCustVisit(customerID, bookingID);
        }
        public bool BLL_UpdateCustVisit(CUST_VISIT visit, BOOKING b)
        {
            return db.UpdateCustVisit(visit,b);
        }
        public bool BLL_CreateCustVisit(CUST_VISIT cust_visit)
        {
            return db.CreateCustVisit(cust_visit);
        }

         public bool BLL_AddBooking(BOOKING addBooking)
        {
            return db.AddBooking(addBooking);
        }

        public List<SP_GetStylists> BLL_GetStylists()
        {
            return db.GetAllStylists();
        }
        public List<SP_GetServices> BLL_GetAllServices()
        {
            return db.GetAllServices();
        }
         public List<SP_GetSlotTimes> BLL_GetAllTimeSlots()
        {
            return db.GetAllTimeSlots();
        }
        public List<SP_GetBookedTimes> BLL_GetBookedStylistTimes(string stylistID, DateTime bookingDate)
        {
            return db.GetBookedStylistTimes(stylistID, bookingDate);
        }

        public List<SP_GetMyNextCustomer> BLL_GetMyNextCustomer(string employeeID, DateTime bookingDate)
        {
            return db.GetMyNextCustomer(employeeID, bookingDate);
        }

        public SP_GetCustomerBooking getBookingDetaisForCheckOut(string BookingID)
        {
            return db.getBookingDetaisForCheckOut(BookingID);
        }

        public bool createSalesRecord(string bookingID)
        {
            return db.createSalesRecord(bookingID);
        }

        public bool updateVatRate(string bussinesID, int vatRate)
        {
            return db.updateVatRate(bussinesID, vatRate);
        }

        public bool updateVatRegNo(string bussinesID, string vatRegNo)
        {
            return db.updateVatRegNo(bussinesID, vatRegNo);
        }

        public bool updateAddress(string bussinesID, string addresLine1, string addressLine2)
        {
            return db.updateAddress(bussinesID, addresLine1, addressLine2);
        }

        public bool updateWeekdayHours(string bussinesID, DateTime wDStart, DateTime wDEnd)
        {
            return db.updateWeekdayHours(bussinesID, wDStart, wDEnd);
        }

        public bool updateWeekendHours(string bussinesID, DateTime wEStart, DateTime wEEnd)
        {
            return db.updateWeekendHours(bussinesID, wEStart, wEEnd);
        }

        public bool updatePublicHolidayHours(string bussinesID, DateTime pHStart, DateTime pHEnd)
        {
            return db.updatePublicHolidayHours(bussinesID, pHStart, pHEnd);
        }

        public bool updatePhoneNumber(string bussinesID, string PhoneNumber)
        {
            return db.updatePhoneNumber(bussinesID, PhoneNumber);
        }

        public List<SP_ViewEmployee> viewAllEmployees()
        {
            return db.viewAllEmployees();
        }

        public List<SP_GetEmployeeTypes> getEmpTypes()
        {
            return db.getEmpTypes();
        }

        public List<PRODUCT> getAllProducts()
        {
            return db.getAllProducts();
        }
        

        public List<SP_GetProductTypes> getProductTypes()
        {
            return db.getProductTypes();
        }

        public List<SP_UserList> userList()
        {
            return db.userList();
        }

        public bool addEmployee(string empID, string bio, string ad1, string ad2, string suburb, string city, string firstname
                                , string lastname, string username, string email, string contactNo, string password,
                                string userimage, string passReset)
        {
            return db.addEmployee(empID,bio,ad1,ad2,suburb,city,firstname,lastname
                                    ,username,email,contactNo,password,userimage,passReset);
        }

        public bool updateEmployee(EMPLOYEE emp)
        {
            return db.updateEmployee(emp);
        }

        public List<SP_BookingsReportForHairstylist> getBookingsReportForHairstylist(string stylistID)
        {
            return db.getBookingsReportForHairstylist(stylistID);
        }

        public List<SP_BookingsReportForHairstylist> getBookingReportForHairstylistWithDateRange(string stylistID, DateTime startDate, DateTime endDate)
        {
          return db.getBookingReportForHairstylistWithDateRange(stylistID, startDate, endDate);

        }

        public List<SP_SaleOfHairstylist> getSaleOfHairstylist(string stylistID, DateTime startDate, DateTime endDate)
        {
            return db.getSaleOfHairstylist(stylistID, startDate,endDate);

        }
                public List<SP_GetStylists> BLL_GetAllStylists()
        {
            return db.GetAllStylists();
        }

        public List<SP_GetStylistBookings> getStylistPastBookings(string empID, string sortBy, string sortDir)
        {
            return db.getStylistPastBookings(empID,sortBy,sortDir);
        }

        public List<SP_GetStylistBookings> getStylistPastBookingsDateRange(string empID, DateTime startDate, DateTime endDate, string sortBy, string sortDir)
        {
            return db.getStylistPastBookingsDateRange(empID,startDate,endDate,sortBy,sortDir);
        }

        public List<SP_GetStylistBookings> getStylistUpcomingBookings(string empID, string sortBy, string sortDir)
        {
            return db.getStylistUpcomingBookings(empID,sortBy,sortDir);
        }

        public List<SP_AboutStylist> aboutStylist()
        {
            return db.aboutStylist();
        }

        public List<SP_GetStylistBookings> getStylistUpcomingBookingsDR(string empID, DateTime startDate, DateTime endDate, string sortBy, string sortDir)
        {
            return db.getStylistUpcomingBookingsDR(empID, startDate, endDate,sortBy,sortDir);
        }

        public List<SP_GetStylistBookings> getAllStylistsUpcomingBksForDate(DateTime bookingDate, string sortBy, string sortDir)
        {
            return db.getAllStylistsUpcomingBksForDate(bookingDate,sortBy,sortDir);
        }
       
        public List<SP_GetStylistBookings> getAllStylistsUpcomingBookings(string sortBy, string sortDir)
        {
            return db.getAllStylistsUpcomingBookings(sortBy,sortDir);
        }
        
        public List<SP_GetStylistBookings> getAllStylistsPastBookings(string sortBy, string sortDir)
        {
            return db.getAllStylistsPastBookings(sortBy,sortDir);
        }

        public List<SP_GetStylistBookings> getAllStylistsPastBksForDate(DateTime date, string sortBy, string sortDir)
        {
            return db.getAllStylistsPastBksForDate(date,sortBy,sortDir);
        }
        public List<SP_GetStylistBookings> getAllStylistsUpcomingBksDR(DateTime startDate, DateTime endDate, string sortBy, string sortDir)
        {
            return db.getAllStylistsUpcomingBksDR(startDate,endDate,sortBy,sortDir);
        }
        public List<SP_GetStylistBookings> getAllStylistsPastBookingsDateRange(DateTime startDate, DateTime endDate, string sortBy, string sortDir)
        {
            return db.getAllStylistsPastBookingsDateRange(startDate,endDate,sortBy,sortDir);
        }

        public List<SP_GetStylistBookings> getStylistPastBksForDate(string empID, DateTime day, string sortBy, string sortDir)
        {
            return db.getStylistPastBksForDate(empID, day,sortBy,sortDir);
        }
        public List<SP_GetStylistBookings> getStylistUpcomingBkForDate(string empID, DateTime day, string sortBy, string sortDir)
        {
            return db.getStylistUpcomingBkForDate(empID, day, sortBy, sortDir);
        }
        public bool BLL_AddToBookingService(BookingService bs)
        {
            return db.AddToBookingService(bs);
        }
        public bool updateService(PRODUCT p, SERVICE s)
        {
            return db.updateService(p, s);
        }
        public SERVICE BLL_GetSlotLength(string serviceID)
        {
            return db.GetSlotLength(serviceID);
        }
        public SP_GetEmployee_S_ getEmployee_S(string stylistID)
        {
            return db.getEmployee_S(stylistID);
        }
        public bool addSpecialisation(STYLIST_SERVICE ss)
        {
            return db.addSpecialisation(ss);
        }
        public SP_GetEmployee_S_ getBio(string id)
        {
            return db.getBio(id);
        }
         public List<SP_GetTopCustomerbyBooking> getTopCustomerByBookings(DateTime startDate, DateTime endDate)
        {
            return db.getTopCustomerByBookings(startDate, endDate);
        }
    }
}
