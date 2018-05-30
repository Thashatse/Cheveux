using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DBAccess : IDBAccess
    {
        public SP_CheckForUserType CheckForUserType(string id)
        {
            SP_CheckForUserType TF = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@ID", id)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CheckForUserType",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        TF = new SP_CheckForUserType
                        {
                            userType = Convert.ToChar(row[0])
                        };

                    }
                    return TF;
                }
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public SP_AddUserGoogleAuth AddUser(USER User)
        {
            SP_AddUserGoogleAuth TF = null;
            SqlParameter[] pars = new SqlParameter[]
            {
            new SqlParameter("@ID", User.UserID),
            new SqlParameter("@FN", User.FirstName),
            new SqlParameter("@LN", User.LastName),
            new SqlParameter("@UN", User.UserName),
            new SqlParameter("@EM", User.Email),
            new SqlParameter("@CN", User.ContactNo),
            new SqlParameter("@UI", User.UserImage)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_AddUserGoogleAuth",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        TF = new SP_AddUserGoogleAuth
                        {
                            Result = Convert.ToInt16(row[0])
                        };

                    }
                    return TF;
                }
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public List<SP_ProductSearchByTerm> UniversalSearch(string searchTerm)
    {
        List<SP_ProductSearchByTerm> SearchResults = new List<SP_ProductSearchByTerm>();
        SqlParameter[] pars = new SqlParameter[]
        {
                new SqlParameter("@searchTerm", searchTerm)
        };

        try
        {
            using (DataTable table = DBHelper.ParamSelect("SP_ProductSearchByTerm",
        CommandType.StoredProcedure, pars))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        SP_ProductSearchByTerm result = new SP_ProductSearchByTerm
                        {
                            Name = row["Name"].ToString(),
                            ProductDescription = row["ProductDescription"].ToString(),
                            Price = row["Price"].ToString(),
                            ProductType = row["ProductType(T/A/S)"].ToString()[0],
                            ProductID = row["ProductID"].ToString()
                        };
                        SearchResults.Add(result);
                    }
                }
                return SearchResults;
            }
        }
        catch (ApplicationException e)
        {
            throw new ApplicationException(e.ToString());
        }
    }

        public USER GetUserDetails(string ID)
        {
            USER TF = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@ID", ID)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetUserDetails",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        TF = new USER
                        {
                            FirstName = row["FirstName"].ToString(),
                            LastName = row["LastName"].ToString(),
                            UserName = row["UserName"].ToString(),
                            Email = row["Email"].ToString(),
                            ContactNo = row["ContactNo"].ToString(),
                            UserType = Convert.ToChar(row["UserType"]),
                            UserImage = row["UserImage"].ToString()
                        };

                    }
                    return TF;
                }
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public SP_GetCurrentVATate GetVATRate()
        {
            SP_GetCurrentVATate VATRate = null;
            try
            {
                using (DataTable table = DBHelper.Select("SP_GetCurrentVATRate2",
            CommandType.StoredProcedure))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        VATRate = new SP_GetCurrentVATate
                        {
                            VATRate = Convert.ToChar(row[0])
                        };

                    }
                    return VATRate;
                }


            }
            catch (ApplicationException e)
            {
                throw new ApplicationException(e.ToString());
            } 
        }

        public List<SP_GetCustomerBooking> getCustomerUpcomingBookings(string CustomerID)
        {
            List<SP_GetCustomerBooking> customerBookings = new List<SP_GetCustomerBooking>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@CustID", CustomerID)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetCustomerUpcomingBookings",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetCustomerBooking booking = new SP_GetCustomerBooking
                            {
                                serviceName = row["Name"].ToString(),
                                serviceDescripion = row["ProductDescription"].ToString(),
                                servicePrice = row["Price"].ToString(),
                                stylistFirstName = row["FirstName"].ToString(),
                                bookingDate = Convert.ToDateTime(row["Date"].ToString()),
                                bookingStartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                bookingID = row["BookingID"].ToString()

                            };
                            customerBookings.Add(booking);
                        }
                    }
                    return customerBookings;
                }
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public SP_GetCustomerBooking getCustomerUpcomingBookingDetails(string BookingID)
        {
            SP_GetCustomerBooking booking = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@BookingID", BookingID)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetCustomerUpcomingBookingDetails",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        booking = new SP_GetCustomerBooking
                            {
                                serviceName = row["Name"].ToString(),
                                serviceDescripion = row["ProductDescription"].ToString(),
                                servicePrice = row["Price"].ToString(),
                                stylistFirstName = row["FirstName"].ToString(),
                                bookingDate = Convert.ToDateTime(row["Date"].ToString()),
                                bookingStartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                bookingID = row["BookingID"].ToString()

                            };
                        }
                    
                    return booking;
                }
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool deleteBooking(string BookingID)
        {
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@BookingID", BookingID),
            };

            return DBHelper.NonQuery("SP_DeleteBooking", CommandType.StoredProcedure, pars);
        }

        public List<SP_GetCustomerBooking> getCustomerPastBookings(string CustomerID)
        {
            List<SP_GetCustomerBooking> customerBookings = new List<SP_GetCustomerBooking>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@CustID", CustomerID)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetCustomerPastBooking",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetCustomerBooking booking = new SP_GetCustomerBooking
                            {
                                serviceName = row["Name"].ToString(),
                                serviceDescripion = row["ProductDescription"].ToString(),
                                servicePrice = row["Price"].ToString(),
                                stylistFirstName = row["FirstName"].ToString(),
                                bookingDate = Convert.ToDateTime(row["Date"].ToString()),
                                bookingStartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                bookingID = row["BookingID"].ToString(),
                                arrived = row["Arrived"].ToString()[0]
                            };
                            customerBookings.Add(booking);
                        }
                    }
                    return customerBookings;
                }
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public SP_GetCustomerBooking getCustomerPastBookingDetails(string BookingID)
        {
            SP_GetCustomerBooking booking = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@BookingID", BookingID)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetCustomerPastBookingDetail",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        booking = new SP_GetCustomerBooking
                        {
                            serviceName = row["Name"].ToString(),
                            serviceDescripion = row["ProductDescription"].ToString(),
                            servicePrice = row["Price"].ToString(),
                            stylistFirstName = row["FirstName"].ToString(),
                            bookingDate = Convert.ToDateTime(row["Date"].ToString()),
                            bookingStartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                            bookingID = row["BookingID"].ToString(),
                            arrived = row["Arrived"].ToString()[0]
                        };
                    }
                    return booking;
                }
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public List<SP_getInvoiceDL> getInvoiceDL(string BookingID)
        {
            List<SP_getInvoiceDL> InvoiceDetailIne = new List<SP_getInvoiceDL>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@BookingID", BookingID)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_getInvoiceDL",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_getInvoiceDL booking = new SP_getInvoiceDL
                            {
                                itemName = row["Name"].ToString(),
                                Qty = Convert.ToInt32(row["Qty"].ToString()),
                                price = Convert.ToDouble(row["Price"].ToString())
                            };
                            InvoiceDetailIne.Add(booking);
                        }
                    }
                    return InvoiceDetailIne;
                }
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
    }
}
   
