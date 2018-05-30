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

        public List<SP_ProductSearchByTerm> UniversalSearch(string searchTerm)
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
    }
}
