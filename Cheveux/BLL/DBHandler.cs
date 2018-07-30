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

        #region Home Page Features
        public List<HomePageFeatures> GetHomePageFeatures()
        {
            return db.GetHomePageFeatures();
        }
        #endregion

        public bool deactivateUser(string userID)
        {
            return db.deactivateUser(userID);
        }

        public DBHandler()
        {
            db = new DBAccess();
        }

        public USER logInEmail(string identifier, string password)
        {
            return db.logInEmail(identifier, password);
        }

        public USER checkForAccountTypeEmail(string identifier)
        {
            return db.checkForAccountTypeEmail(identifier);
        }

        public List<SP_GetTodaysBookings> getTodaysBookings()
        {
            return db.getTodaysBookings();
        }

        public SP_ViewStylistSpecialisation viewStylistSpecialisation(string empID)
        {
            return db.viewStylistSpecialisation(empID);
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

        public Tuple<List<SP_ProductSearchByTerm>, List<SP_SearchStylistsBySearchTerm>> UniversalSearch(string searchTerm)
        {
            return db.UniversalSearch(searchTerm);
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

        public List<SP_GetEmpAgenda> BLL_GetEmpAgenda(string employeeID, DateTime bookingDate)
        {
            return db.GetEmpAgenda(employeeID, bookingDate);
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
        public bool BLL_UpdateCustVisit(CUST_VISIT visit)
        {
            return db.UpdateCustVisit(visit);
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
        public bool addEmployee(EMPLOYEE e)
        {
            return db.addEmployee(e);
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
    }
}
