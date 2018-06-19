﻿using System;
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
        public SP_GetCustomerBooking getBookingDetaisForCheckOut(string BookingID)
        {
            SP_GetCustomerBooking booking = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@bookingID", BookingID)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetBookingDetailsForCustVistRecord",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                            booking = new SP_GetCustomerBooking
                            {
                                serviceName = table.Rows[0]["Name"].ToString(),
                                serviceDescripion = table.Rows[0]["ProductDescription"].ToString(),
                                servicePrice = table.Rows[0]["Price"].ToString(),
                                stylistFirstName = table.Rows[0]["FirstName"].ToString(),
                                bookingDate = Convert.ToDateTime(table.Rows[0]["Date"].ToString()),
                                bookingStartTime = Convert.ToDateTime(table.Rows[0]["StartTime"].ToString()),
                                bookingID = table.Rows[0]["BookingID"].ToString(),
                                CustomerID = table.Rows[0]["BookingID"].ToString(),
                                serviceID = table.Rows[0]["ServiceID"].ToString()
                            };
                    }
                    return booking;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public string getSalePaymentType(string saleID)
        {
            string paymentType = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@SaleID", saleID)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetSalePaymentType",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        DataRow row = table.Rows[0];
                        paymentType = Convert.ToString(row["PaymentType"]);
                    }
                    return paymentType;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool createSalesDTLRecord(SALES_DTL detailLine)
        {

            SP_AddUserGoogleAuth TF = null;
            SqlParameter[] pars = new SqlParameter[]
            {
            new SqlParameter("@SaleID", detailLine.SaleID),
            new SqlParameter("@ProductID", detailLine.ProductID),
            new SqlParameter("@Qty", detailLine.Qty)
            };
            try
            {
                return DBHelper.NonQuery("SP_CreateSalesDTLRecord", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

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
            catch (Exception e)
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
            new SqlParameter("@UI", User.UserImage.ToString())
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
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public Tuple<List<SP_ProductSearchByTerm>, List<SP_SearchStylistsBySearchTerm>> UniversalSearch(string searchTerm)
        {
            List<SP_ProductSearchByTerm> ProductSearchResults = new List<SP_ProductSearchByTerm>();
            List<SP_SearchStylistsBySearchTerm> StylistSearchResults = new List<SP_SearchStylistsBySearchTerm>();
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
                            ProductSearchResults.Add(result);
                        }
                    }
                }

                pars = new SqlParameter[]
                {
                new SqlParameter("@searchTerm", searchTerm)
                };

                using (DataTable table = DBHelper.ParamSelect("SP_SearchStylistsBySearchTerm",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_SearchStylistsBySearchTerm result = new SP_SearchStylistsBySearchTerm
                            {
                                StylistID = row["UserID"].ToString(),
                                StylistFName = row["FirstName"].ToString(),
                                StylistLName = row["LastName"].ToString(),
                                StylistImage = row["UserImage"].ToString()
                            };
                            StylistSearchResults.Add(result);
                        }
                    }
                }
                return Tuple.Create(ProductSearchResults, StylistSearchResults);
            }
            catch (Exception e)
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
            catch (Exception e)
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
            catch (Exception e)
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
            catch (Exception e)
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
                            stylistEmployeeID = row["UserID"].ToString(),
                            stylistFirstName = row["FirstName"].ToString(),
                            bookingDate = Convert.ToDateTime(row["Date"].ToString()),
                            bookingStartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                            bookingID = row["BookingID"].ToString()

                        };
                    }

                    return booking;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool deleteBooking(string BookingID)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@BookingID", BookingID),
                };

                return DBHelper.NonQuery("SP_DeleteBooking", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
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
            catch (Exception e)
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
            catch (Exception e)
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
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public List<SP_GetEmpNames> GetEmpNames()
        {
            List<SP_GetEmpNames> list = new List<SP_GetEmpNames>();
            try
            {
                using (DataTable table = DBHelper.Select("SP_GetEmpNames", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetEmpNames emp = new SP_GetEmpNames();
                            emp.EmployeeID = row["EmployeeID"].ToString();
                            emp.Name = row["Name"].ToString();
                            list.Add(emp);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
            return list;
        }

        public List<SP_GetEmpAgenda> GetEmpAgenda(string employeeID, DateTime bookingDate)
        {
            SP_GetEmpAgenda emp = null;
            List<SP_GetEmpAgenda> agenda = new List<SP_GetEmpAgenda>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", employeeID),
                new SqlParameter("@Date", bookingDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetEmpAgenda",
                                            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            emp = new SP_GetEmpAgenda
                            {
                                BookingID = Convert.ToString(row["BookingID"]),
                                UserID = Convert.ToString(row["UserID"]),
                                StartTime = TimeSpan.Parse((row["StartTime"]).ToString()),
                                EndTime = TimeSpan.Parse((row["EndTime"]).ToString()),
                                CustomerFName = Convert.ToString(row["CustomerFName"]),
                                EmpFName = Convert.ToString(row["EmpFName"]),
                                ServiceName = Convert.ToString(row["ServiceName"]),
                                Arrived = Convert.ToString(row["Arrived"]),
                                Date = Convert.ToDateTime(row["Date"]),
                            };
                            agenda.Add(emp);
                        }
                    }
                }
                return agenda;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public EMPLOYEE getEmployeeType(string EmployeeID)
        {
            EMPLOYEE Emp = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@EmpID", EmployeeID)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetEmployeeType",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        Emp = new EMPLOYEE
                        {
                            Type = row["Type"].ToString(),
                        };
                    }
                    return Emp;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool updateBooking(BOOKING bookingUpdate)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@BookingID", bookingUpdate.BookingID.ToString()),
                new SqlParameter("@SlotNO", bookingUpdate.SlotNo.ToString()),
                new SqlParameter("@StylistID", bookingUpdate.StylistID.ToString()),
                new SqlParameter("@ServiceID", bookingUpdate.StylistID.ToString()),
                new SqlParameter("@Date", bookingUpdate.Date)
                };

                return DBHelper.NonQuery("SP_UpdateBooking", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool createSalesRecord(string bookingID)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@BookingID", bookingID)
                };

                return DBHelper.NonQuery("SP_CreateSalesRecord", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool CheckIn(BOOKING booking)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@BookingID", booking.BookingID.ToString() ),
                    new SqlParameter("@StylistID", booking.StylistID.ToString()),
                };

                return DBHelper.NonQuery("SP_CheckIn", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool updateUser(USER userUpdate)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@UserID", userUpdate.UserID.ToString()),
                new SqlParameter("@UserName", userUpdate.UserName.ToString()),
                new SqlParameter("@ContactNo", userUpdate.ContactNo.ToString()),
                };

                return DBHelper.NonQuery("SP_EditUser", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public SP_GetAllofBookingDTL GetAllofBookingDTL(string bookingID, string customerID)
        {
            SP_GetAllofBookingDTL bookingDTL = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@BookingID", bookingID),
                new SqlParameter("@CustomerID", customerID),
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetAllofBookingDTL",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        DataRow row = table.Rows[0];
                        bookingDTL = new SP_GetAllofBookingDTL
                        {
                            BookingID = Convert.ToString(row["BookingID"]),
                            CustomerID = Convert.ToString(row["CustomerID"]),
                            CustomerName = Convert.ToString(row["CustomerName"]),
                            ServiceName = Convert.ToString(row["ServiceName"]),
                            ServiceDescription = Convert.ToString(row["ServiceDescription"]),
                            Price = Convert.ToString(row["Price"]),
                            Date = Convert.ToDateTime(row["Date"]),
                            StartTime = TimeSpan.Parse((row["StartTime"]).ToString()),
                            EndTime = TimeSpan.Parse((row["EndTime"]).ToString())
                        };


                    }
                    return bookingDTL;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public SP_GetBookingServiceDTL GetBookingServiceDTL(string bookingID, string customerID)
        {
            SP_GetBookingServiceDTL serviceDTL = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                    new SqlParameter("@BookingID", bookingID),
                    new SqlParameter("@CustomerID", customerID),
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetBookingServiceDTL",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        DataRow row = table.Rows[0];
                        serviceDTL = new SP_GetBookingServiceDTL
                        {
                            BookingID = Convert.ToString(row["BookingID"]),
                            ServiceName = Convert.ToString(row["ServiceName"]),
                            ServiceDescription = Convert.ToString(row["ServiceDescription"])
                        };
                    }
                    return serviceDTL;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public SP_ViewCustVisit ViewCustVisit(string customerID, string bookingID)
        {
            SP_ViewCustVisit visit = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@CustomerID", customerID),
                new SqlParameter("@BookingID", bookingID),
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ViewCustVisit",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        DataRow row = table.Rows[0];
                        visit = new SP_ViewCustVisit
                        {
                            CustomerID = Convert.ToString(row["CustomerID"]),
                            Date = Convert.ToDateTime(row["Date"]),
                            BookingID = Convert.ToString(row["BookingID"]),
                            Description = Convert.ToString(row["Description"])
                        };
                    }
                    return visit;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool UpdateCustVisit(CUST_VISIT visit)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@CustomerID", visit.CustomerID.ToString()),
                    new SqlParameter("@BookingID", visit.BookingID.ToString()),
                    new SqlParameter("@Description", visit.Description.ToString())
                };
                return DBHelper.NonQuery("SP_UpdateCustVisit", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool CreateCustVisit(CUST_VISIT cust_visit)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@CustomerID",cust_visit.CustomerID.ToString()),
                    new SqlParameter("@Date", cust_visit.Date.ToString()),
                    new SqlParameter("@BookingID", cust_visit.BookingID.ToString()),
                    new SqlParameter("@Description", cust_visit.Description.ToString()),
                };

                return DBHelper.NonQuery("SP_CreateCustVisit", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool AddBooking(BOOKING addBooking)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                foreach (var prop in addBooking.GetType().GetProperties())
                {
                    if (prop.GetValue(addBooking) != null)
                    {
                        pars.Add(new SqlParameter("@" + prop.Name.ToString(), prop.GetValue(addBooking)));
                    }
                }
                return DBHelper.NonQuery("SP_AddBooking", CommandType.StoredProcedure, pars.ToArray());
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }

            /*
        public List<SP_GetServices> GetAllServices()
        {
            try
            {
                List<SP_GetServices> serviceList = new List<SP_GetServices>();
                using (DataTable table = DBHelper.Select("SP_GetServices", CommandType.StoredProcedure))
                {
                    if(table.Rows.Count>0)
                    {
                        foreach(DataRow row in table.Rows)
                        {
                            SP_GetServices services = new SP_GetServices
                            {
                                Name = Convert.ToString(row["Name"])
                            };
                            serviceList.Add(services);
                        }
                    }
                }
                return serviceList;
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetStylists> GetStylistsForService(string serviceID)
        {
            try
            {
                List<SP_GetStylists> stylistList = new List<SP_GetStylists>();
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@ServiceID", serviceID)
                };
                using(DataTable table = DBHelper.ParamSelect("SP_GetStylists", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count>0)
                    {
                        foreach(DataRow row in table.Rows)
                        {
                            SP_GetStylists stylists = new SP_GetStylists
                            {
                                FirstName = Convert.ToString(row["FirstName"])
                            };
                            stylistList.Add(stylists);
                        }
                    }
                }
                return stylistList;
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        */
        }

        public List<SP_GetMyNextCustomer> GetMyNextCustomer(string employeeID, DateTime bookingDate)
        {
            SP_GetMyNextCustomer emp = null;
            List<SP_GetMyNextCustomer> next = new List<SP_GetMyNextCustomer>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", employeeID),
                new SqlParameter("@Date", bookingDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetMyNextCustomer",
                                            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            emp = new SP_GetMyNextCustomer
                            {
                                BookingID = Convert.ToString(row["BookingID"]),
                                UserID = Convert.ToString(row["UserID"]),
                                StartTime = TimeSpan.Parse((row["StartTime"]).ToString()),
                                EndTime = TimeSpan.Parse((row["EndTime"]).ToString()),
                                CustomerFName = Convert.ToString(row["CustomerFName"]),
                                EmpFName = Convert.ToString(row["EmpFName"]),
                                ServiceName = Convert.ToString(row["ServiceName"]),
                                Arrived = Convert.ToString(row["Arrived"]),
                                Date = Convert.ToDateTime(row["Date"]),
                            };
                            next.Add(emp);
                        }
                    }
                }
                return next;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool addPaymentTypeToSalesRecord(string paymentType, string saleID)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@PaymentType", paymentType),
                    new SqlParameter("@SaleD", saleID),
                };

                return DBHelper.NonQuery("SP_AddPaymentTypeToSalesRecord", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
    }
}
