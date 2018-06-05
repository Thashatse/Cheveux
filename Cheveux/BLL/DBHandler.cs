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

        public SP_CheckForUserType BLL_CheckForUserType(string id)
        {
            return db.CheckForUserType(id);
        }

        public SP_AddUserGoogleAuth BLL_AddUser(USER user)
        {
            return db.AddUser(user);
        }

        public Tuple<List<SP_ProductSearchByTerm>, List<SP_SearchStylistsBySearchTerm>> UniversalSearch(string searchTerm)
        {
            return db.UniversalSearch(searchTerm);
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

        public List<SP_GetEmpAgenda> BLL_GetEmpAgenda(string employeeID)
        {
            return db.GetEmpAgenda(employeeID);
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
/*public bool AddBooking(BOOKING addBooking)
        {
            return db.AddBooking(addBooking);
        }
       
        public List<SP_GetStylists> GetHairstylists(string serviceID)
        {
            return db.GetStylistsForService(serviceID);
        }
        public List<SP_GetServices> GetServices()
        {
            return db.GetAllServices();
        }
        */
    }
}
