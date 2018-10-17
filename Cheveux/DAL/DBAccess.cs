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
        #region Home Page Features
        public List<HomePageFeatures> GetHomePageFeatures()
        {
            List<HomePageFeatures> homePageFeatures = new List<HomePageFeatures>();

            try
            {
                //hairstyls and products
                using (DataTable table = DBHelper.Select("SP_FeaturedProductsAndHairStyles",
            CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            HomePageFeatures feature = new HomePageFeatures
                            {
                                FeatureID = row["FeatureID"].ToString(),
                                ItemID = row["ItemID"].ToString(),
                                ImageURL = row["ImageURL"].ToString(),
                                Name = row["Name"].ToString(),
                                description = row["ProductDescription"].ToString(),
                                price = Math.Round(Convert.ToDecimal(row["Price"]), 2).ToString()
                            };
                            homePageFeatures.Add(feature);
                        }
                    }
                }

                //Contact us & stylists
                using (DataTable table = DBHelper.Select("SP_FeaturedProductsAndContact",
            CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            HomePageFeatures feature = new HomePageFeatures
                            {
                                FeatureID = row["FeatureID"].ToString(),
                                ItemID = row["ItemID"].ToString(),
                                ImageURL = row["ImageURL"].ToString(),
                                firstName = row["FirstName"].ToString(),
                                contactNo = row["ContactNo"].ToString(),
                                email = row["Email"].ToString(),
                            };
                            homePageFeatures.Add(feature);
                        }
                    }
                }
                return homePageFeatures;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool UpdatedHomePageFeatures(Home_Page UpdateFeature)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@FeatureID", UpdateFeature.FeatureID),
                    new SqlParameter("@ItemID", UpdateFeature.ItemID)
                };

                if (UpdateFeature.FeatureID == "Ser01" || UpdateFeature.FeatureID == "Ser02" || UpdateFeature.FeatureID == "Ser03" || UpdateFeature.FeatureID == "Ser04")
                {
                    return DBHelper.NonQuery("SP_UpdateFeaturedService", CommandType.StoredProcedure, pars);
                }
                else if (UpdateFeature.FeatureID == "Pro01" || UpdateFeature.FeatureID == "Pro02" || UpdateFeature.FeatureID == "Pro03")
                {
                    return DBHelper.NonQuery("SP_UpdateFeaturedProduct", CommandType.StoredProcedure, pars);
                }
                else if (UpdateFeature.FeatureID == "Sty01" || UpdateFeature.FeatureID == "Sty02" || UpdateFeature.FeatureID == "Sty03")
                {
                    return DBHelper.NonQuery("SP_UpdateFeaturedStylist", CommandType.StoredProcedure, pars);
                }
                else if (UpdateFeature.FeatureID == "CwuPno" || UpdateFeature.FeatureID == "CwuEma")
                {
                    return DBHelper.NonQuery("SP_UpdateFeaturedContact", CommandType.StoredProcedure, pars);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        #endregion

        #region Email/SMS Notifications
        public List<OGBkngNoti> GetOGBkngNotis()
        {
            List<OGBkngNoti> oGBkngNotiList = new List<OGBkngNoti>();
            OGBkngNoti oGBkngNoti = null;
            try
            {
                using (DataTable table = DBHelper.Select("SP_GetOGBkngNoti",
            CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            oGBkngNoti = new OGBkngNoti
                            {
                                BookingID = row["BookingID"].ToString(),
                                SlotNo = row["SlotNo"].ToString(),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                CustomerID = row["CustomerID"].ToString(),
                                FirstName = row["FirstName"].ToString(),
                                lastName = row["LastName"].ToString(),
                                UserName = row["UserName"].ToString(),
                                PreferredCommunication = row["PreferredCommunication"].ToString()[0],
                                Email = row["Email"].ToString(),
                                ContactNo = row["ContactNo"].ToString(),
                                stylistFirstName = row["StylistName"].ToString(),
                                Date = Convert.ToDateTime(row["Date"].ToString()),
                                NotificationReminder = Convert.ToBoolean(row["NotificationReminder"]),
                                StylistID = row["StylistID"].ToString()
                            };
                            oGBkngNotiList.Add(oGBkngNoti);
                        }
                    }
                    return oGBkngNotiList;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool updateNotiStatus(string bookingID, bool notiStatus)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@BookingID", bookingID),
                new SqlParameter("@NotiStatus", notiStatus)
                };

                return DBHelper.NonQuery("SP_UpdateNotiStatus", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        #endregion

        #region Invoice/Sale
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
                                itemID = row["ProductID"].ToString(),
                                itemType = row["ProductType(T/A/S)"].ToString(),
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

        public SALE getSale(string SaleID)
        {
            SALE sale = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@saleID", SaleID)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetSale",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        sale = new SALE
                        {
                            SaleID = table.Rows[0][0].ToString(),
                            Date = Convert.ToDateTime(table.Rows[0][1].ToString()),
                            CustID = table.Rows[0][2].ToString(),
                            PaymentType = table.Rows[0][3].ToString(),
                            BookingID = table.Rows[0][4].ToString(),
                        };
                    }
                    return sale;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool createProductSalesDTLRecord(SALES_DTL Sale)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@SaleID", Sale.SaleID.ToString()),
                new SqlParameter("@ProductID", Sale.ProductID.ToString()),
                new SqlParameter("@Qty", Sale.Qty)
                };

                return DBHelper.NonQuery("SP_CreateProductSalesDTLRecord", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool removeProductSalesDTLRecord(SALES_DTL Sale)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@SaleID", Sale.SaleID.ToString()),
                new SqlParameter("@ProductID", Sale.ProductID.ToString()),
                new SqlParameter("@Qty", Sale.Qty)
                };

                return DBHelper.NonQuery("SP_RemoveProductSalesDTLRecord", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool UpdateProductSalesDTLRecordQty(SALES_DTL Sale)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@SaleID", Sale.SaleID.ToString()),
                new SqlParameter("@ProductID", Sale.ProductID.ToString()),
                new SqlParameter("@Qty", Sale.Qty)
                };

                return DBHelper.NonQuery("SP_UpdateProductSalesDTLRecordQty", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool createSalesRecord(SALE newSale)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@SaleID", newSale.SaleID),
                    new SqlParameter("@CustID", newSale.CustID)
                };

                return DBHelper.NonQuery("SP_CreateSalesRecord", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        #endregion

        #region Bookings
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
                                stylistFirstName = row["FirstName"].ToString(),
                                bookingDate = Convert.ToDateTime(row["Date"].ToString()),
                                bookingStartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                bookingID = row["BookingID"].ToString(),
                                stylistEmployeeID = row["StylistID"].ToString()
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
                            stylistEmployeeID = row["UserID"].ToString(),
                            stylistFirstName = row["FirstName"].ToString(),
                            bookingDate = Convert.ToDateTime(row["Date"].ToString()),
                            bookingStartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                            slotNo = row["SlotNo"].ToString(),
                            bookingID = row["BookingID"].ToString(),
                            CustFullName = row["CustFullName"].ToString(),
                            CustomerID = row["CustomerID"].ToString(),
                            BookingComment = row["Comment"].ToString()
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

        public List<SP_GetBookingServices> getBookingServices(string BookingID)
        {
            List<SP_GetBookingServices> bookingServices = new List<SP_GetBookingServices>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@BookingID", BookingID)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetBookingServices",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetBookingServices service = new SP_GetBookingServices
                            {
                                BookingID = row["BookingID"].ToString(),
                                ServiceID = row["ServiceID"].ToString(),
                                ServiceName = row["Name"].ToString(),
                                serviceDescripion = row["ProductDescription"].ToString(),
                                Price = Convert.ToDouble(row["Price"])
                            };
                            bookingServices.Add(service);
                        }
                    }
                    return bookingServices;
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

        public bool deleteBookingService(string BookingID, string ServiceID)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@BookingID", BookingID),
                new SqlParameter("@ServiceID", ServiceID)
                };

                return DBHelper.NonQuery("SP_DeleteBookingService", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool deleteSecondaryBooking(string BookingID)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@PrimaryBookingID", BookingID),
                };

                return DBHelper.NonQuery("SP_DeleteSecondaryBookings", CommandType.StoredProcedure, pars);
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
                                stylistFirstName = row["FirstName"].ToString(),
                                bookingDate = Convert.ToDateTime(row["Date"].ToString()),
                                bookingStartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                bookingID = row["BookingID"].ToString(),
                                arrived = row["Arrived"].ToString()[0],
                                stylistEmployeeID = row["StylistID"].ToString()
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
                            stylistFirstName = row["FirstName"].ToString(),
                            bookingDate = Convert.ToDateTime(row["Date"].ToString()),
                            bookingStartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                            bookingID = row["BookingID"].ToString(),
                            arrived = row["Arrived"].ToString()[0],
                            stylistEmployeeID = row["StylistID"].ToString(),
                            CustFullName = row["CustFullName"].ToString(),
                            CustomerID = row["CustomerID"].ToString()
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

        public bool updateBooking(BOOKING bookingUpdate)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@BookingID", bookingUpdate.BookingID.ToString()),
                new SqlParameter("@SlotNO", bookingUpdate.SlotNo.ToString()),
                new SqlParameter("@StylistID", bookingUpdate.StylistID.ToString()),
                new SqlParameter("@Date", bookingUpdate.Date),
                new SqlParameter("@Comment", bookingUpdate.Comment.ToString())
                };

                return DBHelper.NonQuery("SP_UpdateBooking", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public List<SP_GetBookedTimes> GetBookedStylistTimes(string stylistID, DateTime bookingDate)
        {
            List<SP_GetBookedTimes> bookings = new List<SP_GetBookedTimes>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@UserID", stylistID),
                new SqlParameter("@Date", bookingDate)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetBookedTimes",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetBookedTimes times = new SP_GetBookedTimes
                            {
                                SlotNo = Convert.ToString(row["SlotNo"]),
                            };
                            bookings.Add(times);
                        }
                    }

                }
                return bookings;
            }
            catch
            {

            }
            return null;
        }

        public bool AddBooking(BOOKING addBooking)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@BookingID", addBooking.BookingID.ToString()),
                    new SqlParameter("@Slot", addBooking.SlotNo.ToString()),
                    new SqlParameter("@CustomerID", addBooking.CustomerID.ToString()),
                    new SqlParameter("@StylistID", addBooking.StylistID.ToString()),
                    new SqlParameter("@Date", addBooking.Date.ToString()),
                    new SqlParameter("@primaryBookingID", addBooking.primaryBookingID.ToString()),
                    new SqlParameter("@Comment", addBooking.Comment.ToString()),
                    new SqlParameter("@Arrived", addBooking.Arrived.ToString())
                };
                return DBHelper.NonQuery("SP_AddBooking", CommandType.StoredProcedure, pars.ToArray());
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }

        }

        public List<SP_GetTodaysBookings> getTodaysBookings()
        {
            SP_GetTodaysBookings booking = null;
            List<SP_GetTodaysBookings> bookings = new List<SP_GetTodaysBookings>();
            try
            {
                using (DataTable table = DBHelper.Select("SP_GetTodaysBookings",
            CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            booking = new SP_GetTodaysBookings
                            {
                                BookingID = row["BookingID"].ToString(),
                                SlotNo = row["SlotNo"].ToString(),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                EndTime = Convert.ToDateTime(row["EndTime"].ToString()),
                                CustomerID = row["CustomerID"].ToString(),
                                CustomerFirstName = row["FirstName"].ToString(),
                                CustomerLastName = row["LastName"].ToString(),
                                Date = Convert.ToDateTime(row["Date"].ToString()),
                                Available = row["Available"].ToString(),
                                Arrived = row["Arrived"].ToString(),
                                Comment = row["Comment"].ToString()
                            };
                            bookings.Add(booking);
                        }
                    }
                    return bookings;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }

        }

        public SP_GetMultipleServicesTime getMultipleServicesTime(string primaryBookingID)
        {
            SP_GetMultipleServicesTime time = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@primaryBookingID", primaryBookingID)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetMultipleServicesTime",
                                                CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        time = new SP_GetMultipleServicesTime
                        {
                            StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                            EndTime = Convert.ToDateTime(row["EndTime"].ToString()),
                        };
                    }
                }
                return time;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        #endregion

        #region CheckIN CheckOut Cust Vist
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
                    if (table.Rows.Count == 1)
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

        public bool UpdateCustVisit(CUST_VISIT visit, BOOKING b)
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
                    new SqlParameter("@PrimaryBookingID", cust_visit.BookingID.ToString()),
                    new SqlParameter("@Description", cust_visit.Description.ToString()),
                };

                return DBHelper.NonQuery("SP_CreateCustVisit", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

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
                            stylistFirstName = table.Rows[0]["FirstName"].ToString(),
                            bookingDate = Convert.ToDateTime(table.Rows[0]["Date"].ToString()),
                            bookingStartTime = Convert.ToDateTime(table.Rows[0]["StartTime"].ToString()),
                            bookingID = table.Rows[0]["BookingID"].ToString(),
                            CustomerID = table.Rows[0]["CustomerID"].ToString(),
                            CustFullName = table.Rows[0]["custFullName"].ToString()
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

        public bool createSalesRecordForBooking(string bookingID)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@BookingID", bookingID)
                };

                return DBHelper.NonQuery("SP_CreateSalesRecordForBooking", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool createSalesDTLRecord(SALES_DTL detailLine)
        {
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

        public bool CheckIn(BOOKING booking)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@BookingID", booking.BookingID.ToString())
                };
                return DBHelper.NonQuery("SP_CheckIn", CommandType.StoredProcedure, pars);
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
                            Date = Convert.ToDateTime(row["Date"]),
                            StartTime = Convert.ToDateTime((row["StartTime"]).ToString()),
                            EndTime = Convert.ToDateTime((row["EndTime"]).ToString()),
                            Comment = Convert.ToString(row["Comment"])
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
        #endregion

        #region Authentication
        public USER logInEmail(string identifier, string password)
        {
            USER AT = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@identifier", identifier),
                new SqlParameter("@password", password)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_LogInEmail",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        AT = new USER
                        {
                            UserID = Convert.ToString(row[0]),
                            UserType = Convert.ToString(row[1])[0],
                            FirstName = Convert.ToString(row[2])
                        };
                    }
                    return AT;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public USER getPasHash(string identifier)
        {
            USER AT = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@identifier", identifier)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetPasHash",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        AT = new USER
                        {
                            Password = Convert.ToString(row[0])
                        };
                    }
                    return AT;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        #endregion

        #region User Accounts
        public bool updateStylistBio(EMPLOYEE bioUpdate)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@empID", bioUpdate.EmployeeID.ToString()),
                    new SqlParameter("@bioUpdate", bioUpdate.Bio.ToString())
                };
                return DBHelper.NonQuery("SP_UpdateStylistBio", CommandType.StoredProcedure, pars);
            }
            catch (Exception E)
            {
                throw new ApplicationException(E.ToString());
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

        public SP_AddUser AddUser(USER User)
        {
            SP_AddUser TF = null;
            SqlParameter[] pars = new SqlParameter[]
                {
            new SqlParameter("@ID", User.UserID),
            new SqlParameter("@FN", User.FirstName),
            new SqlParameter("@LN", User.LastName),
            new SqlParameter("@UN", User.UserName),
            new SqlParameter("@EM", User.Email),
            new SqlParameter("@CN", User.ContactNo),
            new SqlParameter("@UI", User.UserImage),
            new SqlParameter("@AT", User.AccountType),
            new SqlParameter("@UT", User.UserType),
            new SqlParameter("@Pass", User.Password)
                };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_AddNewUser",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        TF = new SP_AddUser
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

        public bool updateUser(USER userUpdate)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@UserID", userUpdate.UserID.ToString()),
                new SqlParameter("@UserName", userUpdate.UserName.ToString()),
                new SqlParameter("@ContactNo", userUpdate.ContactNo.ToString()),
                new SqlParameter("@Name", userUpdate.FirstName.ToString()),
                new SqlParameter("@LName", userUpdate.LastName.ToString()),
                new SqlParameter("@Email", userUpdate.Email.ToString()),
                };

                return DBHelper.NonQuery("SP_EditUser", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool deactivateUser(string userID)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@UserID", userID.ToString())
                };

                return DBHelper.NonQuery("SP_DeactivateUser", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool createRestCode(string emailOrUsername, string restCode)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@restCode", restCode),
                new SqlParameter("@Email", emailOrUsername)
                };

                return DBHelper.NonQuery("SP_CreateRestCode", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool updateUserAccountPassword(string password, string userID)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@Password", password),
                new SqlParameter("@UserID", userID)
                };

                return DBHelper.NonQuery("SP_UpdateUserAccountPassword", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public USER GetAccountForRestCode(string code)
        {
            USER TF = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@Code", code)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetAccountForRestCode",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        TF = new USER
                        {
                            UserID = row["UserID"].ToString(),
                            UserName = row["UserName"].ToString()
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
                            UserImage = row["UserImage"].ToString(),
                            AccountType = row["AccountType"].ToString()
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

        public List<SP_UserList> userList()
        {
            SP_UserList userList = null;
            List<SP_UserList> list = new List<SP_UserList>();
            try
            {
                using (DataTable table = DBHelper.Select("SP_UserList", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            userList = new SP_UserList
                            {
                                UserImage = Convert.ToString(row["UserImage"]),
                                UserID = Convert.ToString(row["UserID"]),
                                FullName = Convert.ToString(row["FullName"]),
                                UserName = Convert.ToString(row["UserName"]),
                                Email = Convert.ToString(row["Email"]),
                                ContactNo = Convert.ToString(row["ContactNo"]),
                                userType = Convert.ToChar(row["UserType"])
                            };
                            list.Add(userList);
                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        #endregion

        #region Products
        public PRODUCT CheckForProduct(string id)
        {
            PRODUCT TF = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@ProductID", id)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CheckForProduct",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        TF = new PRODUCT
                        {
                            ProductID = row[0].ToString()
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

        public Tuple<List<SP_GetAllAccessories>, List<SP_GetAllTreatments>> getAllProductsAndDetails()
        {
            SP_GetAllAccessories accessory = null;
            List<SP_GetAllAccessories> accessories = new List<SP_GetAllAccessories>();
            SP_GetAllTreatments treatment = null;
            List<SP_GetAllTreatments> treatments = new List<SP_GetAllTreatments>();
            try
            {
                //get accessories	
                using (DataTable table = DBHelper.Select("SP_GetAllAccessories",
            CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            accessory = new SP_GetAllAccessories
                            {
                                ProductID = row["ProductID"].ToString(),
                                Name = row["Name"].ToString(),
                                ProductDescription = row["ProductDescription"].ToString(),
                                Price = Convert.ToDecimal(row["Price"].ToString()),
                                ProductType = row["ProductType(T/A/S)"].ToString(),
                                Active = row["Active"].ToString(),
                                //ProductImage = row["ProductImage"]	
                                Colour = row["Colour"].ToString(),
                                Qty = Convert.ToInt32(row["Qty"].ToString()),
                                BrandID = row["BrandID"].ToString(),
                                Brandname = row[11].ToString(),
                                brandType = row["Type(T/A)"].ToString(),
                                supplierID = row["SupplierID"].ToString(),
                                supplierName = row["SupplierName"].ToString(),
                                contactName = row["ContactName"].ToString(),
                                contactEmail = row["ContactEmail"].ToString(),
                                contactNo = row["ContactNo"].ToString()
                            };
                            accessories.Add(accessory);
                        }
                    }
                }
                //get Treatments	
                using (DataTable table = DBHelper.Select("SP_GetAllTreatments",
            CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            treatment = new SP_GetAllTreatments
                            {
                                ProductID = row["ProductID"].ToString(),
                                Name = row["Name"].ToString(),
                                ProductDescription = row["ProductDescription"].ToString(),
                                Price = Convert.ToDecimal(row["Price"].ToString()),
                                ProductType = row["ProductType(T/A/S)"].ToString(),
                                Active = row["Active"].ToString(),
                                //ProductImage = row["ProductImage"]	
                                TreatmentType = row["TreatmentType"].ToString(),
                                Qty = Convert.ToInt32(row["Qty"].ToString()),
                                BrandID = row["BrandID"].ToString(),
                                Brandname = row[11].ToString(),
                                brandType = row["Type(T/A)"].ToString(),
                                supplierID = row["SupplierID"].ToString(),
                                supplierName = row["SupplierName"].ToString(),
                                contactName = row["ContactName"].ToString(),
                                contactEmail = row["ContactEmail"].ToString(),
                                contactNo = row["ContactNo"].ToString()
                            };
                            treatments.Add(treatment);
                        }
                    }
                }
                return Tuple.Create(accessories, treatments);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        //select accessory
        public SP_GetAllAccessories selectAccessory(string accessoryID)
        {
            SP_GetAllAccessories accessory = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ProductID", accessoryID)

            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_SelectAccessory", CommandType.StoredProcedure, parameters))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        accessory = new SP_GetAllAccessories();
                        accessory.Name = Convert.ToString(row["Name"]);
                        accessory.ProductDescription = Convert.ToString(row["ProductDescription"]);
                        accessory.Price = Convert.ToInt32(row["Price"]);
                        accessory.ProductType = Convert.ToString(row["ProductType(T/A/S)"]);
                        accessory.BrandID = Convert.ToString(row["BrandID"]);
                        accessory.Qty = Convert.ToInt16(row["Qty"]);
                        accessory.Brandname = Convert.ToString(row["BrandName"]);
                        accessory.supplierID= Convert.ToString(row["SupplierID"]);
                        accessory.supplierName = Convert.ToString(row["SupplierName"]);
                        accessory.Colour = Convert.ToString(row["Colour"]);
                    }
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
            return accessory;
        }

        //select treatment
        public SP_GetAllTreatments selectTreatment(string treatmentID)
        {
            SP_GetAllTreatments treatment = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ProductID", treatmentID)

            };

            try
            { using (DataTable table = DBHelper.ParamSelect("SP_SelectTreatment", CommandType.StoredProcedure, parameters))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        treatment = new SP_GetAllTreatments();
                        treatment.Name = Convert.ToString(row["Name"]);
                        treatment.ProductDescription = Convert.ToString(row["ProductDescription"]);
                        treatment.Price = Convert.ToInt32(row["Price"]);
                        treatment.ProductType = Convert.ToString(row["ProductType(T/A/S)"]);
                        treatment.Active = Convert.ToString(row["Active"]);
                        treatment.BrandID = Convert.ToString("BrandID");
                        treatment.Qty = Convert.ToInt16(row["Qty"]);
                        treatment.Brandname = Convert.ToString(row["BrandName"]);
                        treatment.supplierID = Convert.ToString(row["SupplierID"]);
                        treatment.supplierName = Convert.ToString(row["SupplierName"]);
                        treatment.TreatmentType = Convert.ToString(row["TreatmentType"]);
                    }
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
            return treatment;
        }

        //AddProduct
        public bool addProduct(PRODUCT addProduct)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@ProductID", addProduct.ProductID),
                    new SqlParameter("@Name", addProduct.Name),
                    new SqlParameter("@ProductDescription", addProduct.ProductDescription),
                    new SqlParameter("@Price", addProduct.Price),
                    new SqlParameter("@productType", addProduct.ProductType),
                    new SqlParameter("@Active", addProduct.Active)

                };
                return DBHelper.NonQuery("SP_AddProduct", CommandType.StoredProcedure, pars);
            }
            catch (Exception E)
            {
                throw new ApplicationException(E.ToString());
            }
        }
                
        public List<PRODUCT> getAllProducts()
        {
            PRODUCT product = null;
            List<PRODUCT> products = new List<PRODUCT>();
            try
            {
                using (DataTable table = DBHelper.Select("SP_GetAllProducts",
            CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            product = new PRODUCT
                            {
                                ProductID = row["ProductID"].ToString(),
                                Name = row["Name"].ToString(),
                                ProductDescription = row["ProductDescription"].ToString(),
                                Price = Convert.ToDecimal(row["Price"].ToString()),
                                ProductType = row["ProductType(T/A/S)"].ToString(),
                                Active = row["Active"].ToString()//,
                                //ProductImage = row["ProductImage"]
                            };
                            products.Add(product);
                        }
                    }
                    return products;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

       public bool addAccessories(ACCESSORY a, PRODUCT p)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                    {
                     new SqlParameter("@accessoryID", a.TreatmentID.ToString()),
                    new SqlParameter("@colour", a.Colour.ToString()),
                    new SqlParameter("@qty", a.Qty.ToString()),
                    new SqlParameter("@BrandID", a.BrandID.ToString()),
                    new SqlParameter("@name",p.Name.ToString()),
                    new SqlParameter("@productDescription",p.ProductDescription.ToString()),
                    new SqlParameter("@price", p.Price.ToString()),
                    new SqlParameter("@productType", p.ProductType.ToString()),
                    new SqlParameter("@SupplierID", a.supplierID.ToString())
                    };
                return DBHelper.NonQuery("SP_AddAccessory", CommandType.StoredProcedure, pars.ToArray());
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }

        }

        //addTreatments
        public bool addTreatments(TREATMENT t, PRODUCT p)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                    {
                    new SqlParameter("@treatmentID",t.TreatmentID.ToString()),
                    new SqlParameter("@qty", t.Qty.ToString()),
                    new SqlParameter("@treatmentType", t.TreatmentType.ToString()),
                    new SqlParameter("@BrandID", t.BrandID.ToString()),
                    new SqlParameter("@name",p.Name.ToString()),
                    new SqlParameter("@productDescription",p.ProductDescription.ToString()),
                    new SqlParameter("@price", p.Price.ToString()),
                    new SqlParameter("@productType", p.ProductType.ToString()),
                    new SqlParameter("@SupplierID", t.supplierID.ToString())
                    };
                return DBHelper.NonQuery("SP_AddTreatment", CommandType.StoredProcedure, pars.ToArray());
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }

        }

        public bool updateAccessories(ACCESSORY a, PRODUCT p)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                    {
                     new SqlParameter("@accessoryID", a.TreatmentID.ToString()),
                    new SqlParameter("@name",p.Name.ToString()),
                    new SqlParameter("@productDescription",p.ProductDescription.ToString()),
                    new SqlParameter("@price", p.Price.ToString()),
                    new SqlParameter("@SupplierID", a.supplierID.ToString())
                    };
                return DBHelper.NonQuery("SP_UpdateAccessory", CommandType.StoredProcedure, pars.ToArray());
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }

        }

        //addTreatments
        public bool updateTreatments(TREATMENT t, PRODUCT p)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                    {
                    new SqlParameter("@treatmentID",t.TreatmentID.ToString()),
                    new SqlParameter("@name",p.Name.ToString()),
                    new SqlParameter("@productDescription",p.ProductDescription.ToString()),
                    new SqlParameter("@price", p.Price.ToString()),
                    new SqlParameter("@SupplierID", t.supplierID.ToString())
                    };
                return DBHelper.NonQuery("SP_UpdateTreatment", CommandType.StoredProcedure, pars.ToArray());
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }

        }
        #endregion

        #region ProductTypes
        public List<ProductType> getProductTypes()
        {
            ProductType productType = null;
            List<ProductType> productTypes = new List<ProductType>();
            try
            {
                using (DataTable table = DBHelper.Select("SP_GetProductTypes",
            CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productType = new ProductType
                            {
                                typeID = row["TypeID"].ToString(),
                                name = row["Name"].ToString(),
                                ProductOrService = row["Product/Service"].ToString()[0],
                                PrimaryService = Convert.ToBoolean(row["PrimaryService"])
                            };
                            productTypes.Add(productType);
                        }
                    }
                    return productTypes;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool addProductType(ProductType newType)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@typeID", newType.typeID),
                new SqlParameter("@typeName", newType.name),
                new SqlParameter("@ProdOrSer", newType.ProductOrService)
                };

                return DBHelper.NonQuery("SP_NewProductType", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool editProductType(ProductType updateType)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@typeID", updateType.typeID),
                new SqlParameter("@typeName", updateType.name),
                new SqlParameter("@ProdOrSer", updateType.ProductOrService)
                };

                return DBHelper.NonQuery("SP_EditProductType", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        #endregion

        #region Product Orders
        public OrderViewModel getOrder(string orderID)
        {
            OrderViewModel listItem = new OrderViewModel();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@OrderID", orderID)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetProductOrder", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            listItem.OrderID = row["OrderID"].ToString();
                            listItem.orderDate = Convert.ToDateTime(row["OrderDate"].ToString());
                            listItem.Received = Convert.ToBoolean(row["Received"].ToString());
                            if (row["DateReceived"].ToString() != "")
                            {
                                listItem.dateReceived = Convert.ToDateTime(row["DateReceived"].ToString());
                            }
                            listItem.supplierID = row["SupplierID"].ToString();
                            listItem.supplierName = row["SupplierName"].ToString();
                            listItem.contactName = row["ContactName"].ToString();
                            listItem.contactNo = row["ContactNo"].ToString();
                            listItem.AddressLine1 = row["AddressLine1"].ToString();
                            listItem.AddressLine2 = row["AddressLine2"].ToString();
                            listItem.Suburb = row["Suburb"].ToString();
                            listItem.City = row["City"].ToString();
                            listItem.contactEmail = row["ContactEmail"].ToString();
                        }
                    }
                    return listItem;
                }
            }
            catch (Exception E)
            {
                throw new ApplicationException(E.ToString());
            }
        }

        public List<OrderViewModel> getOutStandingOrders()
        {
            List<OrderViewModel> list = new List<OrderViewModel>();
            try
            {
                using (DataTable table = DBHelper.Select("SP_GetOutstandingProductOrders", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            OrderViewModel listItem = new OrderViewModel();
                            listItem.OrderID = row["OrderID"].ToString();
                            listItem.orderDate = Convert.ToDateTime(row["OrderDate"].ToString());
                            listItem.Received = Convert.ToBoolean(row["Received"].ToString());
                            if (row["DateReceived"].ToString() != "")
                            {
                                listItem.dateReceived = Convert.ToDateTime(row["DateReceived"].ToString());
                            }
                            listItem.supplierID = row["SupplierID"].ToString();
                            listItem.supplierName = row["SupplierName"].ToString();
                            listItem.contactName = row["ContactName"].ToString();
                            listItem.contactNo = row["ContactNo"].ToString();
                            listItem.AddressLine1 = row["AddressLine1"].ToString();
                            listItem.AddressLine2 = row["AddressLine2"].ToString();
                            listItem.Suburb = row["Suburb"].ToString();
                            listItem.City = row["City"].ToString();
                            listItem.contactEmail = row["ContactEmail"].ToString();
                            list.Add(listItem);
                        }
                    }
                    return list;
                }
            }
            catch (Exception E)
            {
                throw new ApplicationException(E.ToString());
            }
        }

        public List<OrderViewModel> getPastOrders()
        {
            List<OrderViewModel> list = new List<OrderViewModel>();
            try
            {
                using (DataTable table = DBHelper.Select("SP_GetPastProductOrders", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            OrderViewModel listItem = new OrderViewModel();
                            listItem.OrderID = row["OrderID"].ToString();
                            listItem.orderDate = Convert.ToDateTime(row["OrderDate"].ToString());
                            listItem.Received = Convert.ToBoolean(row["Received"].ToString());
                            listItem.dateReceived = Convert.ToDateTime(row["DateReceived"].ToString());
                            listItem.supplierID = row["SupplierID"].ToString();
                            listItem.supplierName = row["SupplierName"].ToString();
                            listItem.contactName = row["ContactName"].ToString();
                            listItem.contactNo = row["ContactNo"].ToString();
                            listItem.AddressLine1 = row["AddressLine1"].ToString();
                            listItem.AddressLine2 = row["AddressLine2"].ToString();
                            listItem.Suburb = row["Suburb"].ToString();
                            listItem.City = row["City"].ToString();
                            listItem.contactEmail = row["ContactEmail"].ToString();
                            list.Add(listItem);
                        }
                    }
                    return list;
                }
            }
            catch (Exception E)
            {
                throw new ApplicationException(E.ToString());
            }
        }

        public List<OrderViewModel> getProductOrderDL(string orderID)
        {
            List<OrderViewModel> list = new List<OrderViewModel>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@OrderID", orderID)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetProductOrderDetailLines", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            OrderViewModel listItem = new OrderViewModel();
                            listItem.OrderID = row["OrderID"].ToString();
                            listItem.orderDate = Convert.ToDateTime(row["OrderDate"].ToString());
                            listItem.Received = Convert.ToBoolean(row["Received"].ToString());
                            if (row["DateReceived"].ToString() != "")
                            {
                                listItem.dateReceived = Convert.ToDateTime(row["DateReceived"].ToString());
                            }
                            listItem.supplierID = row["SupplierID"].ToString();
                            listItem.supplierName = row["SupplierName"].ToString();
                            listItem.contactName = row["ContactName"].ToString();
                            listItem.contactNo = row["ContactNo"].ToString();
                            listItem.AddressLine1 = row["AddressLine1"].ToString();
                            listItem.AddressLine2 = row["AddressLine2"].ToString();
                            listItem.Suburb = row["Suburb"].ToString();
                            listItem.City = row["City"].ToString();
                            listItem.contactEmail = row["ContactEmail"].ToString();
                            listItem.ProductID = row["ProductID"].ToString();
                            listItem.Name = row["Name"].ToString();
                            listItem.ProductDescription = row["ProductDescription"].ToString();
                            listItem.Price = Convert.ToDouble(row["Price"].ToString());
                            listItem.ProductType = row["ProductType"].ToString();
                            listItem.Active = row["Active"].ToString();
                            listItem.Qty = Convert.ToInt32(row["Qty"].ToString());
                            list.Add(listItem);
                        }
                    }
                    return list;
                }
            }
            catch (Exception E)
            {
                throw new ApplicationException(E.ToString());
            }
        }
       
        public bool newProductOrder(Order newOrder)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@OrderID", newOrder.OrderID),
                new SqlParameter("@SuppID", newOrder.supplierID)
                };

                return DBHelper.NonQuery("SP_NewOrder", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool newProductOrderDL(Order_DTL newOrderDL)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@OrderID", newOrderDL.OrderID),
                new SqlParameter("@ProdID", newOrderDL.ProductID),
                new SqlParameter("@Qty", newOrderDL.Qty)
                };

                return DBHelper.NonQuery("SP_NewOrderDL", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public Order CheckForOrder(string id)
        {
            Order TF = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@OrderID", id)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CheckForOrder",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        TF = new Order
                        {
                            OrderID = row[0].ToString()
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
        #endregion

        #region Auto Product Orders
        public List<SP_GetAuto_Purchase_Products> getAutoPurchOrdProds()
        {
            List<SP_GetAuto_Purchase_Products> list = new List<SP_GetAuto_Purchase_Products>();
            try
            {
                using (DataTable table = DBHelper.Select("SP_GetAuto_Purchase_Products", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetAuto_Purchase_Products listItem = new SP_GetAuto_Purchase_Products();
                            listItem.Name = row["Name"].ToString();
                            listItem.ProductID = row["ProductID"].ToString();
                            listItem.ProductType = row["ProductType(T/A/S)"].ToString();
                            listItem.Qty = int.Parse(row["Qty"].ToString());
                            list.Add(listItem);
                        }
                    }
                    return list;
                }
            }
            catch (Exception E)
            {
                throw new ApplicationException(E.ToString());
            }
        }

        public bool newAutoPurchProd(Auto_Purchase_Products newProduct)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@ProductID", newProduct.ProductID),
                    new SqlParameter("@QTY", newProduct.Qty)
                };

                return DBHelper.NonQuery("SP_AddProdToAutoPurch", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool deleteAutoPurchProd(Auto_Purchase_Products product)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@ProductID", product.ProductID)
                };

                return DBHelper.NonQuery("SP_DeleteProdFromAutoPurch", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        #endregion

        #region Stock Managment Settings
        public Stock_Management getStockSettings()
        {
            Stock_Management StockSettings = null;

            try
            {
                using (DataTable table = DBHelper.Select("SP_GetStockManagementSettings", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        StockSettings = new Stock_Management
                        {
                            BusinessID = row["BusinessID"].ToString(),
                            LowStock = int.Parse(row["LowStock"].ToString()),
                            PurchaseQty = int.Parse(row["PurchaseQty"].ToString()),
                            AutoPurchase = Convert.ToBoolean(row["AutoPurchase"].ToString()),
                            AutoPurchaseFrequency = row["AutoPurchaseFrequency"].ToString(),
                            AutoPurchaseProducts = Convert.ToBoolean(row["AutoPurchaseProducts"].ToString()),
                            NxtOrderdDate = Convert.ToDateTime(row["NxtOrderDate"].ToString())
                        };
                    }
                    return StockSettings;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool updateStockSettings(Stock_Management Update)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@BusinessID", Update.BusinessID),
                    new SqlParameter("@LowStock", Update.LowStock),
                    new SqlParameter("@PurchaseQty", Update.PurchaseQty),
                    new SqlParameter("@AutoPurchase", Update.AutoPurchase),
                    new SqlParameter("@AutoPurchaseFrequency", Update.AutoPurchaseFrequency),
                    new SqlParameter("@AutoPurchaseProducts", Update.AutoPurchaseProducts),
                    new SqlParameter("@nextDate", Update.NxtOrderdDate)
                };
                return DBHelper.NonQuery("SP_UpdateStockManagementSettings", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        #endregion

        #region Manager Dash Board
        public ManagerStats GetManagerStats()
        {
            ManagerStats stats = new ManagerStats();

            try
            {
                //todaysSales
                using (DataTable table = DBHelper.Select("SP_GetTodaysTotalSales",
            CommandType.StoredProcedure))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        if (row[0].ToString() != "")
                        {
                           stats.sales = Convert.ToDecimal(row[0].ToString());
                        }
                        else
                        {
                            stats.sales = 0;
                        }
                    }
                }

                //UpcomingBookings
                using (DataTable table = DBHelper.Select("SP_TotalUpcomingBookings",
            CommandType.StoredProcedure))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        if (row[0].ToString() != "")
                        {
                            stats.upcomingBookings = Convert.ToInt16(row[0].ToString());
                    }
                    else
                    {
                        stats.upcomingBookings = 0;
                    }
                }
                }

                //registered customers
                using (DataTable table = DBHelper.Select("SP_GetTotalCusts",
            CommandType.StoredProcedure))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        if (row[0].ToString() != "")
                        {
                            stats.registeredCustomers = Convert.ToInt16(row[0].ToString());
                    }
                    else
                    {
                        stats.registeredCustomers = 0;
                    }
                }
                }

                //tottal alltime bookings
                using (DataTable table = DBHelper.Select("SP_TotalBookings",
            CommandType.StoredProcedure))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                    if (row[0].ToString() != "")
                    {
                        stats.totalBookings = Convert.ToInt16(row[0].ToString());
                    }
                    else
                    {
                        stats.totalBookings= 0;
                    }
                }
                }

                return stats;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        #endregion

        #region Services
        public bool updateService(PRODUCT p, SERVICE s)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@ServiceID", p.ProductID.ToString()),
                    new SqlParameter("@Price", p.Price.ToString()),
                    new SqlParameter("@Slots", s.NoOfSlots.ToString())
                };
                return DBHelper.NonQuery("SP_UpdateService", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }

        }
        #endregion

        #region Brands
        public List<SP_GetBrandsForProductType> getBrandsForProductType(char type)
        {
            List<SP_GetBrandsForProductType> list = new List<SP_GetBrandsForProductType>();
            SP_GetBrandsForProductType b = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@productType",type)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetBrandsForProductType", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            b = new SP_GetBrandsForProductType();
                            b.BrandID = row["BrandID"].ToString();
                            b.Name = row["Name"].ToString();
                            b.Type = row["Type(T/A)"].ToString();
                            list.Add(b);
                        }
                    }
                    return list;
                }
            }
            catch (Exception E)
            {
                throw new ApplicationException(E.ToString());
            }

        }

        public List<BRAND> getAllBrands()
        {
            List<BRAND> brandList = new List<BRAND>();
            try
            {
                using (DataTable table = DBHelper.Select("SP_GetAllBrands", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            BRAND brand = new BRAND();
                            brand.BrandID = row["BrandID"].ToString();
                            brand.Name = row["Name"].ToString();
                            brand.Type = row["Type(T/A)"].ToString();
                            brandList.Add(brand);
                        }
                    }
                    return brandList;
                }
            }
            catch (Exception E)
            {
                throw new ApplicationException(E.ToString());
            }

        }

        public BRAND getBrand(string BrandID)
        {
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@BrandID", BrandID)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetBrand", CommandType.StoredProcedure, pars))
                {
                    BRAND brand = new BRAND();
                    if (table.Rows.Count == 1)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            brand.BrandID = row["BrandID"].ToString();
                            brand.Name = row["Name"].ToString();
                            brand.Type = row["Type(T/A)"].ToString();
                        }
                    }
                    return brand;
                }
            }
            catch (Exception E)
            {
                throw new ApplicationException(E.ToString());
            }

        }

        public bool newBrand (BRAND newBrand)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@BrandID", newBrand.BrandID),
                new SqlParameter("@BrandName", newBrand.Name),
                new SqlParameter("@Type", newBrand.Type)
                };

                return DBHelper.NonQuery("SP_NewBrand", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool editBrand (BRAND brandUpdate)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@BrandID", brandUpdate.BrandID),
                new SqlParameter("@BrandName", brandUpdate.Name),
                new SqlParameter("@Type", brandUpdate.Type)
                };

                return DBHelper.NonQuery("SP_EditBrand", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public BRAND CheckForBrand(string id)
        {
            BRAND TF = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@BrandID", id)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CheckForBrand",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        TF = new BRAND
                        {
                            BrandID = row[0].ToString()
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
        #endregion

        #region Supplier
        public Supplier getSupplier(string suppID)
        {
            Supplier supplier = new Supplier();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@SuppID", suppID)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("getSupplierDetails", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            supplier.supplierID = row["SupplierID"].ToString();
                            supplier.supplierName = row["SupplierName"].ToString();
                            supplier.contactName = row["ContactName"].ToString();
                            supplier.contactNo = row["ContactNo"].ToString();
                            supplier.AddressLine1 = row["AddressLine1"].ToString();
                            supplier.AddressLine2 = row["AddressLine2"].ToString();
                            supplier.Suburb = row["Suburb"].ToString();
                            supplier.City = row["City"].ToString();
                            supplier.contactEmail = row["ContactEmail"].ToString();
                        }
                    }
                    return supplier;
                }
            }
            catch (Exception E)
            {
                throw new ApplicationException(E.ToString());
            }
        }

        public List<Supplier> getSuppliers()
        {
            List<Supplier> list = new List<Supplier>();
            try
            {
                using (DataTable table = DBHelper.Select("SP_Get_Supplier", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            Supplier supplier = new Supplier();
                            supplier.supplierID = row["SupplierID"].ToString();
                            supplier.supplierName = row["SupplierName"].ToString();
                            supplier.contactName = row["ContactName"].ToString();
                            supplier.contactNo = row["ContactNo"].ToString();
                            supplier.AddressLine1 = row["AddressLine1"].ToString();
                            supplier.AddressLine2 = row["AddressLine2"].ToString();
                            supplier.Suburb = row["Suburb"].ToString();
                            supplier.City = row["City"].ToString();
                            supplier.contactEmail = row["ContactEmail"].ToString();
                            list.Add(supplier);
                        }
                    }
                    return list;
                }
            }
            catch (Exception E)
            {
                throw new ApplicationException(E.ToString());
            }
        }

        public bool newSupplier(Supplier newSupp)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@SuppID", newSupp.supplierID),
                new SqlParameter("@SuppName", newSupp.supplierName),
                new SqlParameter("@ContactName", newSupp.contactName),
                new SqlParameter("@ContactNo", newSupp.contactNo),
                new SqlParameter("@AddressL1", newSupp.AddressLine1),
                new SqlParameter("@AddressL2", newSupp.AddressLine2),
                new SqlParameter("@Suburb", newSupp.Suburb),
                new SqlParameter("@City", newSupp.City),
                new SqlParameter("@ContactEmail", newSupp.contactEmail),
                };

                return DBHelper.NonQuery("SP_NewSupplier", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool editSupplier(Supplier suppUpdate)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                new SqlParameter("@SuppID", suppUpdate.supplierID),
                new SqlParameter("@SuppName", suppUpdate.supplierName),
                new SqlParameter("@ContactName", suppUpdate.contactName),
                new SqlParameter("@ContactNo", suppUpdate.contactNo),
                new SqlParameter("@AddressL1", suppUpdate.AddressLine1),
                new SqlParameter("@AddressL2", suppUpdate.AddressLine2),
                new SqlParameter("@Suburb", suppUpdate.Suburb),
                new SqlParameter("@City", suppUpdate.City),
                new SqlParameter("@ContactEmail", suppUpdate.contactEmail),
                };

                return DBHelper.NonQuery("SP_EditSupplier", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public Supplier CheckForSupplier(string id)
        {
            Supplier TF = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@SuppID", id)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CheckForSupplier",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        TF = new Supplier
                        {
                            supplierID = row[0].ToString()
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
        #endregion

        #region Bussines Table
        public BUSINESS getBusinessTable()
        {
            BUSINESS businessDetails = null;

            try
            {
                using (DataTable table = DBHelper.Select("SP_getBusinessTable", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        businessDetails = new BUSINESS
                        {
                            BusinessID = row[0].ToString(),
                            Vat = int.Parse(row[1].ToString()),
                            VatRegNo = row[2].ToString(),
                            AddressLine1 = row[3].ToString(),
                            AddressLine2 = row[4].ToString(),
                            Phone = row[5].ToString(),
                            WeekdayStart = DateTime.Parse(row[6].ToString()),
                            WeekdayEnd = DateTime.Parse(row[7].ToString()),
                            WeekendStart = DateTime.Parse(row[8].ToString()),
                            WeekendEnd = DateTime.Parse(row[9].ToString()),
                            PublicHolEnd = DateTime.Parse(row[9].ToString()),
                            PublicHolStart = DateTime.Parse(row[10].ToString())
                        };
                    }
                    return businessDetails;
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

        public bool updateVatRate(string bussinesID, int vatRate)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@VatRat", vatRate),
                    new SqlParameter("@BusinessID", bussinesID)
                };
                return DBHelper.NonQuery("SP_UpdateVateRate", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool updateVatRegNo(string bussinesID, string vatRegNo)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@VatRegno", vatRegNo),
                    new SqlParameter("@BusinessID", bussinesID)
                };
                return DBHelper.NonQuery("SP_UpdateVateRegNo", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool updateAddress(string bussinesID, string addressLine1, string addressLine2)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@addline1", addressLine1),
                    new SqlParameter("@addline2", addressLine2),
                    new SqlParameter("@BusinessID", bussinesID)
                };
                return DBHelper.NonQuery("SP_UpdateAddress", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool updateWeekdayHours(string bussinesID, DateTime wDStart, DateTime wDEnd)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@start", wDStart.ToString("HH:mm")),
                    new SqlParameter("@end", wDEnd.ToString("HH:mm")),
                    new SqlParameter("@BusinessID", bussinesID)
                };
                return DBHelper.NonQuery("SP_UpdateWeekdayHours", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool updateWeekendHours(string bussinesID, DateTime wEStart, DateTime wEEnd)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@start", wEStart.ToString("HH:mm")),
                    new SqlParameter("@end", wEEnd.ToString("HH:mm")),
                    new SqlParameter("@BusinessID", bussinesID)
                };
                return DBHelper.NonQuery("SP_UpdateWeekendHours", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool updatePublicHolidayHours(string bussinesID, DateTime pHStart, DateTime pHEnd)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@start", pHStart.ToString("HH:mm")),
                    new SqlParameter("@end", pHEnd.ToString("HH:mm")),
                    new SqlParameter("@BusinessID", bussinesID)
                };
                return DBHelper.NonQuery("SP_UpdatePublicHolidayHours", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public bool updatePhoneNumber(string bussinesID, string PhoneNumber)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@PhoneNumber", PhoneNumber),
                    new SqlParameter("@BusinessID", bussinesID)
                };
                return DBHelper.NonQuery("SP_UpdatePhoneNumber", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        #endregion

        #region Search
        public Tuple<List<SP_ProductSearchByTerm>, List<SP_SearchStylistsBySearchTerm>> UniversalSearch(string searchTerm)
        {
            List<SP_ProductSearchByTerm> ProductSearchResults = new List<SP_ProductSearchByTerm>();
            List<SP_SearchStylistsBySearchTerm> StylistSearchResults = new List<SP_SearchStylistsBySearchTerm>();

            //search treatments
            SqlParameter[] pars = new SqlParameter[]
        {
                new SqlParameter("@searchTerm", searchTerm)
        };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_TreatmentSearchByTerm",
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
                                Price = Math.Round(Convert.ToDecimal(row["Price"]), 2).ToString(),
                                ProductType = row["ProductType(T/A/S)"].ToString()[0],
                                ProductID = row["ProductID"].ToString()
                            };
                            ProductSearchResults.Add(result);
                        }
                    }
                }

                //search Acsoris
                pars = new SqlParameter[]
                {
                new SqlParameter("@searchTerm", searchTerm)
                };

                using (DataTable table = DBHelper.ParamSelect("SP_AccessorySearchByTerm",
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
                                Price = Math.Round(Convert.ToDecimal(row["Price"]), 2).ToString(),
                                ProductType = row["ProductType(T/A/S)"].ToString()[0],
                                ProductID = row["ProductID"].ToString()
                            };
                            ProductSearchResults.Add(result);
                        }
                    }
                }

                //search Services
                pars = new SqlParameter[]
                {
                new SqlParameter("@searchTerm", searchTerm)
                };

                using (DataTable table = DBHelper.ParamSelect("SP_ServiceSearchByTerm",
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
                                Price = Math.Round(Convert.ToDecimal(row["Price"]), 2).ToString(),
                                ProductType = row["ProductType(T/A/S)"].ToString()[0],
                                ProductID = row["ProductID"].ToString()
                            };
                            ProductSearchResults.Add(result);
                        }
                    }
                }

                //search Braid Services
                pars = new SqlParameter[]
                {
                new SqlParameter("@searchTerm", searchTerm)
                };

                using (DataTable table = DBHelper.ParamSelect("SP_BraidServiceSearchByTerm",
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
                                Price = Math.Round(Convert.ToDecimal(row["Price"]), 2).ToString(),
                                ProductType = row["ProductType(T/A/S)"].ToString()[0],
                                ProductID = row["ProductID"].ToString()
                            };
                            ProductSearchResults.Add(result);
                        }
                    }
                }

                //search stylists
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

        public List<SP_GetCustomerBooking> searchBookings(DateTime startDate, DateTime endDate)
        {
            List<SP_GetCustomerBooking> bookings = new List<SP_GetCustomerBooking>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_SearchBookings",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetCustomerBooking booking = new SP_GetCustomerBooking
                            {
                                stylistFirstName = row["StylistFirstName"].ToString(),
                                bookingDate = Convert.ToDateTime(row["Date"].ToString()),
                                bookingStartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                bookingID = row["BookingID"].ToString(),
                                arrived = row["Arrived"].ToString()[0],
                                stylistEmployeeID = row["StylistID"].ToString(),
                                BookingComment = row["Comment"].ToString(),
                                CustFullName = row["CustFirstName"].ToString(),
                                CustomerID = row["CustomerID"].ToString()
                            };
                            bookings.Add(booking);
                        }
                    }
                    return bookings;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        #endregion

        #region Reviews
        public List<SP_GetReviews> getAllBookingReviews()
        {
            List<SP_GetReviews> list = new List<SP_GetReviews>();
            try
            {
                using(DataTable table = DBHelper.Select("SP_GetAllBookingReviews", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetReviews emp = new SP_GetReviews()
                            {
                                ReviewID = row["ReviewID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                EmployeeID = row["EmployeeID"].ToString(),
                                PrimaryBookingID = row["primaryBookingID"].ToString(),
                                Date = Convert.ToDateTime(row["Date"].ToString()),
                                Time = Convert.ToDateTime(row["Time"].ToString()),
                                Rating = Convert.ToInt32(row["Rating"].ToString()),
                                Comment = row["Comment"].ToString(),
                                StylistImage = row["StylistImage"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                CustomerImage = row["CustomerImage"].ToString(),
                                CustomerName = row["CustomerName"].ToString()
                            };
                            list.Add(emp);
                        }
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetReviews> getAllStylistReviews()
        {
            List<SP_GetReviews> list = new List<SP_GetReviews>();
            try
            {
                using (DataTable table = DBHelper.Select("SP_GetAllStylistReviews", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetReviews emp = new SP_GetReviews()
                            {
                                ReviewID = row["ReviewID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                EmployeeID = row["EmployeeID"].ToString(),
                                PrimaryBookingID = row["primaryBookingID"].ToString(),
                                Date = Convert.ToDateTime(row["Date"].ToString()),
                                Time = Convert.ToDateTime(row["Time"].ToString()),
                                Rating = Convert.ToInt32(row["Rating"].ToString()),
                                Comment = row["Comment"].ToString(),
                                StylistImage = row["StylistImage"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                CustomerImage = row["CustomerImage"].ToString(),
                                CustomerName = row["CustomerName"].ToString()
                            };
                            list.Add(emp);
                        }
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetReviews> getCustomersReviews(string customerID)
        {
            List<SP_GetReviews> list = new List<SP_GetReviews>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@customerID", customerID)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetCustomersReviews", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetReviews emp = new SP_GetReviews()
                            {
                                ReviewID = row["ReviewID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                EmployeeID = row["EmployeeID"].ToString(),
                                PrimaryBookingID = row["primaryBookingID"].ToString(),
                                Date = Convert.ToDateTime(row["Date"].ToString()),
                                Time = Convert.ToDateTime(row["Time"].ToString()),
                                Rating = Convert.ToInt32(row["Rating"].ToString()),
                                Comment = row["Comment"].ToString(),
                                StylistImage = row["StylistImage"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                CustomerImage = row["CustomerImage"].ToString(),
                                CustomerName = row["CustomerName"].ToString()
                            };
                            list.Add(emp);
                        }
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetReviews> getReviewsOfStylist(string stylistID)
        {
            List<SP_GetReviews> list = new List<SP_GetReviews>();
            SqlParameter[] pars = new SqlParameter[] 
            {
                new SqlParameter("@stylistID", stylistID)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetReviewsOfStylist", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetReviews emp = new SP_GetReviews()
                            {
                                ReviewID = row["ReviewID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                EmployeeID = row["EmployeeID"].ToString(),
                                PrimaryBookingID = row["primaryBookingID"].ToString(),
                                Date = Convert.ToDateTime(row["Date"].ToString()),
                                Time = Convert.ToDateTime(row["Time"].ToString()),
                                Rating = Convert.ToInt32(row["Rating"].ToString()),
                                Comment = row["Comment"].ToString(),
                                CustomerImage = row["CustomerImage"].ToString(),
                                CustomerName = row["CustomerName"].ToString()
                            };
                            list.Add(emp);
                        }
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public bool reviewBooking(REVIEW r)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@reviewID",r.ReviewID),
                    new SqlParameter("@customerID",r.CustomerID),
                    new SqlParameter("@employeeID", r.EmployeeID),
                    new SqlParameter("@primaryBookingID",r.PrimaryBookingID),
                    new SqlParameter("@date",r.Date),
                    new SqlParameter("@time",r.Time),
                    new SqlParameter("@rating",r.Rating),
                    new SqlParameter("@comment",r.Comment)
                };
                return DBHelper.NonQuery("SP_ReviewBooking", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public bool reviewStylist(REVIEW r)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@reviewID",r.ReviewID),
                    new SqlParameter("@customerID",r.CustomerID),
                    new SqlParameter("@employeeID", r.EmployeeID),
                    new SqlParameter("@date",r.Date),
                    new SqlParameter("@time",r.Time),
                    new SqlParameter("@rating",r.Rating),
                    new SqlParameter("@comment",r.Comment)
                };
                return DBHelper.NonQuery("SP_ReviewStylist", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public bool updateStylistReview(REVIEW r)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@reviewID",r.ReviewID),
                    new SqlParameter("@employeeID", r.EmployeeID),
                    new SqlParameter("@date",r.Date),
                    new SqlParameter("@time",r.Time),
                    new SqlParameter("@rating",r.Rating),
                    new SqlParameter("@comment",r.Comment)
                };
                return DBHelper.NonQuery("SP_UpdateStylistReview", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public bool updateBookingReview(REVIEW r)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@reviewID",r.ReviewID),
                    new SqlParameter("@bookingID", r.PrimaryBookingID),
                    new SqlParameter("@date",r.Date),
                    new SqlParameter("@time",r.Time),
                    new SqlParameter("@rating",r.Rating),
                    new SqlParameter("@comment",r.Comment)
                };
                return DBHelper.NonQuery("SP_UpdateBookingReview", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public REVIEW CheckForReview(string reviewID)
        {
            REVIEW rev = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@reviewID", reviewID)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CheckForReview",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        rev = new REVIEW
                        {
                            ReviewID = row[0].ToString()
                        };
                    }
                    return rev;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public REVIEW customersReviewForStylist(string customerID,string stylistID)
        {
            REVIEW rev = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@customerID", customerID),
                new SqlParameter("@stylistID", stylistID)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomersReviewForStylist",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        rev = new REVIEW
                        {
                            ReviewID = row["ReviewID"].ToString(),
                            CustomerID = row["CustomerID"].ToString(),
                            EmployeeID = row["EmployeeID"].ToString(),
                            PrimaryBookingID = row["primaryBookingID"].ToString(),
                            Date = Convert.ToDateTime(row["Date"].ToString()),
                            Time = Convert.ToDateTime(row["Time"].ToString()),
                            Rating = Convert.ToInt32(row["Rating"].ToString()),
                            Comment = row["Comment"].ToString()
                        };
                    }
                    return rev;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public REVIEW customersReviewForBooking(string customerID, string bookingID)
        {
            REVIEW rev = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@customerID", customerID),
                new SqlParameter("@bookingID", bookingID)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomersReviewForBooking",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        rev = new REVIEW
                        {
                            ReviewID = row["ReviewID"].ToString(),
                            CustomerID = row["CustomerID"].ToString(),
                            EmployeeID = row["EmployeeID"].ToString(),
                            PrimaryBookingID = row["primaryBookingID"].ToString(),
                            Date = Convert.ToDateTime(row["Date"].ToString()),
                            Time = Convert.ToDateTime(row["Time"].ToString()),
                            Rating = Convert.ToInt32(row["Rating"].ToString()),
                            Comment = row["Comment"].ToString()
                        };
                    }
                    return rev;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_ReturnStylistNamesForReview> returnStylistNamesForReview(string customerID)
        {
            List<SP_ReturnStylistNamesForReview> list = new List<SP_ReturnStylistNamesForReview>();
            SqlParameter[] pars = new SqlParameter[] {
                new SqlParameter("@customerID" , customerID)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ReturnStylistNamesForReview", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_ReturnStylistNamesForReview emp = new SP_ReturnStylistNamesForReview()
                            {
                                StylistID = row["StylistID"].ToString(),
                                StylistName = row["StylistName"].ToString()
                            };
                            list.Add(emp);
                        }
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public REVIEW getStylistRating(string stylistID)
        {
            REVIEW rev = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@stylistID", stylistID)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetStylistRating",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        rev = new REVIEW
                        {
                            Rating = Convert.ToInt32(row["AverageRating"].ToString())
                        };
                    }
                    return rev;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetCustomerBooking> getCustRecentBookings(string CustomerID)
        {
            List<SP_GetCustomerBooking> customerBookings = new List<SP_GetCustomerBooking>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@CustID", CustomerID)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustRecentBookings",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetCustomerBooking booking = new SP_GetCustomerBooking
                            {
                                stylistFirstName = row["FirstName"].ToString(),
                                bookingDate = Convert.ToDateTime(row["Date"].ToString()),
                                bookingStartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                bookingID = row["BookingID"].ToString(),
                                arrived = row["Arrived"].ToString()[0],
                                stylistEmployeeID = row["StylistID"].ToString()
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
        #endregion

        #region Employees
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

        public USER getManagerContact()
        {
            try
            {
                using (DataTable table = DBHelper.Select("SP_GetManagerContact",
            CommandType.StoredProcedure))
                {
                    USER ManagerContact = null;
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        ManagerContact = new USER
                        {
                            Email = row["Email"].ToString(),
                            ContactNo = row["ContactNo"].ToString()
                        };
                    }
                    return ManagerContact;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        #endregion

        #region Reports
        public List<SP_BookingsReportForHairstylist> getBookingsReportForHairstylist(string stylistID)
        {
            SP_BookingsReportForHairstylist hairstylistBookingReportrecord = null;
            List<SP_BookingsReportForHairstylist> list = new List<SP_BookingsReportForHairstylist>();

            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@stylistID", stylistID)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_BookingsReportForHairstylist", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            hairstylistBookingReportrecord = new SP_BookingsReportForHairstylist
                            {
                                BookingID = Convert.ToString(row["BookingID"]),
                                slotNo = Convert.ToString(row["slotNo"]),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                EndTime = Convert.ToDateTime(row["EndTime"].ToString()),
                                CustomerID = Convert.ToString(row["CustomerID"]),
                                FirstName = Convert.ToString(row["FirstName"]),
                                LastName = Convert.ToString(row["LastName"]),
                                stylistID = Convert.ToString(row["StylistID"]),
                                serviceID = Convert.ToString(row["ServiceID"]),
                                Name = Convert.ToString(row["Name"]),
                                Date = Convert.ToDateTime(row["Date"]),
                                Available = Convert.ToString(row["Available"]),
                                Arrived = Convert.ToString(row["Arrived"]),
                                Comment = Convert.ToString(row["Comment"]),
                            };
                            list.Add(hairstylistBookingReportrecord);
                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public List<SP_BookingsReportForHairstylist> getBookingReportForHairstylistWithDateRange(string stylistID, DateTime startDate, DateTime endDate)
        {

            SP_BookingsReportForHairstylist hairstylistBookingReportrecord = null;
            List<SP_BookingsReportForHairstylist> list = new List<SP_BookingsReportForHairstylist>();

            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@stylistID", stylistID),
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_BookingsReportForHairstylistwithDateRange", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {

                            hairstylistBookingReportrecord = new SP_BookingsReportForHairstylist
                            {
                                BookingID = Convert.ToString(row["BookingID"]),
                                slotNo = Convert.ToString(row["slotNo"]),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                EndTime = Convert.ToDateTime(row["EndTime"].ToString()),
                                CustomerID = Convert.ToString(row["CustomerID"]),
                                FirstName = Convert.ToString(row["FirstName"]),
                                LastName = Convert.ToString(row["LastName"]),
                                stylistID = Convert.ToString(row["StylistID"]),
                                serviceID = Convert.ToString(row["ServiceID"]),
                                Name = Convert.ToString(row["Name"]),
                                Date = Convert.ToDateTime(row["Date"]),
                                Available = Convert.ToString(row["Available"]),
                                Arrived = Convert.ToString(row["Arrived"]),
                                Comment = Convert.ToString(row["Comment"]),
                            };
                            list.Add(hairstylistBookingReportrecord);
                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }

        }

        public List<SP_SaleOfHairstylist> getSaleOfHairstylist(string stylistID, DateTime startDate, DateTime endDate)
        {
            SP_SaleOfHairstylist saleOfHairstylistrecord = null;
            List<SP_SaleOfHairstylist> list = new List<SP_SaleOfHairstylist>();

            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@stylistID", stylistID),
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_SaleOfHairstylist", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            saleOfHairstylistrecord = new SP_SaleOfHairstylist
                            {
                                SaleID = Convert.ToString(row["SaleID"]),
                                date = Convert.ToDateTime(row["Date"]),
                                CustomerID = Convert.ToString(row["CustomerID"]),
                                FullName = Convert.ToString(row["FullName"]),
                                BookingID = Convert.ToString(row["BookingID"]),
                                PaymentType = Convert.ToString(row["PaymentType"])
                            };
                            list.Add(saleOfHairstylistrecord);

                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        #region Top Product
        public List<productSalesReport> getProductSalesVolumeAll(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ProductSalesReportByVolume", 
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Product"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getProductSalesVolumeCredit(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();

            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ProductSalesReportByVolumeCredit",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Product"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getProductSalesVolumeCash(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ProductSalesReportByVolumeCash", 
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Product"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getProductSalesValueCredit(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ProductSalesReportByValueCredit", 
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Product"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getProductSalesValueCash(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ProductSalesReportByValueCash", 
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Product"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getProductSalesValueAll(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ProductSalesReportByValue", 
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Product"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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
        #endregion

        #region Top Service
        public List<productSalesReport> getServiceSalesVolumeAll(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ServiceSalesReportByVolume",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Product"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getServiceSalesVolumeCredit(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();

            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ServiceSalesReportByVolumeCredit",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Product"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getServiceSalesVolumeCash(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ServiceSalesReportByVolumeCash",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Product"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getServiceSalesValueCredit(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ServiceSalesReportByValueCredit",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Product"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getServiceSalesValueCash(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ServiceSalesReportByValueCash",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Product"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getServiceSalesValueAll(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ServiceSalesReportByValue",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Product"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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
        #endregion

        #region Top Customer
        public List<productSalesReport> getCustomerSalesVolumeAll(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerSalesReportByVolume",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getCustomerSalesVolumeCredit(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();

            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerSalesReportByVolumeCredit",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getCustomerSalesVolumeCash(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerSalesReportByVolumeCash",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getCustomerSalesValueCredit(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerSalesReportByValueCredit",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getCustomerSalesValueCash(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerSalesReportByValueCash",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getCustomerSalesValueAll(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerSalesReportByValue",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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
        #endregion

        #region Top Customer Service
        public List<productSalesReport> getCustomerServiceSalesVolumeAll(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerServiceSalesReportByVolume",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getCustomerServiceSalesVolumeCredit(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();

            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerServiceSalesReportByVolumeCredit",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getCustomerServiceSalesVolumeCash(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerServiceSalesReportByVolumeCash",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getCustomerServiceSalesValueCredit(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerServiceSalesReportByValueCredit",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getCustomerServiceSalesValueCash(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerServiceSalesReportByValueCash",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getCustomerServiceSalesValueAll(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerServiceSalesReportByValue",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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
        #endregion

        #region Top Customer Product
        public List<productSalesReport> getCustomerProductSalesVolumeAll(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerProductSalesReportByVolume",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getCustomerProductSalesVolumeCredit(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();

            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerProductSalesReportByVolumeCredit",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getCustomerProductSalesVolumeCash(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerProductSalesReportByVolumeCash",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getCustomerProductSalesValueCredit(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerProductSalesReportByValueCredit",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getCustomerProductSalesValueCash(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerProductSalesReportByValueCash",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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

        public List<productSalesReport> getCustomerProductSalesValueAll(DateTime startDate, DateTime endDate)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerProductSalesReportByValue",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = row["Customer"].ToString();
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
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
        #endregion

        public List<productSalesReport> getSalesGauge(string ProductID)
        {
            List<productSalesReport> list = new List<productSalesReport>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@prodID", ProductID)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_SalesGaugeFroProduct",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            productSalesReport prod = new productSalesReport();
                            prod.product = "1";
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                            list.Add(prod);
                        }
                    }
                }

                using (DataTable table = DBHelper.Select("SP_SalesGaugeTotals",
                    CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        productSalesReport prod = new productSalesReport();
                        foreach (DataRow row in table.Rows)
                        {
                            prod.product = "2";
                            prod.volume = int.Parse(row["Volume"].ToString());
                            prod.value = Convert.ToDouble(row["Value"].ToString());
                        }
                        list.Add(prod);
                    }
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
            return list;
        }

        public List<SP_TotalBksMissedByCustomers> returnTotalbksMissedbyCustomers(DateTime startDate, DateTime endDate)
        {
            List<SP_TotalBksMissedByCustomers> list = new List<SP_TotalBksMissedByCustomers>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_TotalBksMissedByCustomers",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_TotalBksMissedByCustomers cust = new SP_TotalBksMissedByCustomers()
                            {
                                customerID = row["CustomerID"].ToString(),
                                customerName = row["CustomerName"].ToString(),
                                missed =Convert.ToInt32(row["BookingsMissed"])
                            };
                            list.Add(cust);
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

        public List<SP_GetReviews> mostPopularStylist(DateTime startDate, DateTime endDate)
        {
            List<SP_GetReviews> list = new List<SP_GetReviews>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_MostPopularStylist",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetReviews rev = new SP_GetReviews()
                            {
                                EmployeeID = row["EmployeeID"].ToString(),
                                StylistName = row["EmployeeName"].ToString(),
                                Rating = Convert.ToInt32(row["Rating"])
                            };
                            list.Add(rev);
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

        public List<SP_GetReviews> customerSatistfaction(DateTime startDate, DateTime endDate)
        {
            List<SP_GetReviews> list = new List<SP_GetReviews>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerSatisfaction",
                    CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetReviews rev = new SP_GetReviews()
                            {
                                CustomerID = row["CustomerID"].ToString(),
                                CustomerName = row["CustomerName"].ToString(),
                                Rating = Convert.ToInt32(row["Rating"]),
                                noOfReviews = Convert.ToInt32(row["NoOfReviews"])
                            };
                            list.Add(rev);
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
        #endregion

        public List<SP_GetServices> GetAllServices()
        {
            try
            {
                List<SP_GetServices> serviceList = new List<SP_GetServices>();
                using (DataTable table = DBHelper.Select("SP_GetServices", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetServices services = new SP_GetServices
                            {
                                ServiceID = Convert.ToString(row["ProductID"]),
                                Name = Convert.ToString(row["Name"]),
                                ServiceType = Convert.ToChar(row["ServiceType"]),
                                Price = Convert.ToDecimal(row["Price"]),
                                Description = Convert.ToString(row["ProductDescription"]),
                                Active = Convert.ToChar(row["Active"])
                            };
                            serviceList.Add(services);
                        }
                    }
                }
                return serviceList;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public List<SP_GetStylists> GetStylists()
        {
            try
            {
                List<SP_GetStylists> stylistList = new List<SP_GetStylists>();

                using (DataTable table = DBHelper.Select("SP_GetStylist", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetStylists stylists = new SP_GetStylists
                            {
                                UserID = Convert.ToString(row["UserID"]),
                                FirstName = Convert.ToString(row["FirstName"]),
                                ServiceID = Convert.ToString(row["ServiceID"])
                            };
                            stylistList.Add(stylists);
                        }
                    }
                }
                return stylistList;

            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
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
        
        public SP_ViewEmployee viewEmployee(string empID)
        {
            SP_ViewEmployee viewEmployee = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", empID)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ViewEmployee",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        viewEmployee = new SP_ViewEmployee
                        {
                            UserID = Convert.ToString(row["UserID"]),
                            firstName = Convert.ToString(row["FirstName"]),
                            lastName = Convert.ToString(row["LastName"]),
                            userName = Convert.ToString(row["UserName"]),
                            email = Convert.ToString(row["Email"]),
                            phoneNumber = Convert.ToString(row["ContactNo"]),
                            employeeType = Convert.ToString(row["Type"]),
                            empImage = Convert.ToString(row["UserImage"]),
                            active = Convert.ToChar(row["Active"].ToString()[0]),
                            addLine1 = Convert.ToString(row["AddressLine1"]),
                            addLine2 = Convert.ToString(row["AddressLine2"]),
                            suburb = Convert.ToString(row["Suburb"]),
                            city = Convert.ToString(row["City"])
                        };
                    }
                    return viewEmployee;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public SP_ViewStylistSpecialisationAndBio viewStylistSpecialisationAndBio(string empID)
        {
            SP_ViewStylistSpecialisationAndBio stylistSpecialisationAndBio = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", empID)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ViewStylistSpecialisation",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        stylistSpecialisationAndBio = new SP_ViewStylistSpecialisationAndBio
                        {
                            EmployeeID = Convert.ToString(row[0]),
                            serviceID = Convert.ToString(row[1]),
                            serviceName = Convert.ToString(row[2]),
                            serviceDescription = Convert.ToString(row[3]),
                            servicePrice = Convert.ToDecimal(row[4].ToString()),
                            serviceType = Convert.ToChar(row[5].ToString()[0]),
                            Stylistbio = row["StylistBio"].ToString()
                        };
                    }
                    return stylistSpecialisationAndBio;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public List<SP_ViewEmployee> viewAllEmployees()
        {
            SP_ViewEmployee employee = null;
            List<SP_ViewEmployee> employees = new List<SP_ViewEmployee>();
            try
            {
                using (DataTable table = DBHelper.Select("SP_ViewAllEmployee",
            CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            employee = new SP_ViewEmployee
                            {
                                UserID = Convert.ToString(row["UserID"]),
                                firstName = Convert.ToString(row["FirstName"]),
                                lastName = Convert.ToString(row["LastName"]),
                                userName = Convert.ToString(row["UserName"]),
                                email = Convert.ToString(row["Email"]),
                                phoneNumber = Convert.ToString(row["ContactNo"]),
                                employeeType = Convert.ToString(row["Type"]),
                                empImage = Convert.ToString(row["UserImage"]),
                                active = Convert.ToChar(row["Active"])
                            };
                            employees.Add(employee);
                        }
                    }
                    return employees;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public List<SP_GetEmployeeTypes> getEmpTypes()
        {
            SP_GetEmployeeTypes employeeType = null;
            List<SP_GetEmployeeTypes> employeeTypes = new List<SP_GetEmployeeTypes>();
            try
            {
                using (DataTable table = DBHelper.Select("SP_GetEmployeeTypes",
            CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            employeeType = new SP_GetEmployeeTypes
                            {
                                Type = Convert.ToChar(row[0].ToString()[0])
                            };
                            employeeTypes.Add(employeeType);
                        }
                    }
                    return employeeTypes;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
                
        public USER checkForAccountTypeEmail(string identifier)
        {
            USER AT = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@identifier", identifier)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CheckForAccountTypeEmail",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        AT = new USER
                        {
                            AccountType = Convert.ToString(row[0])
                        };
                    }
                    return AT;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        
        public bool addEmployee(string empID, string bio, string ad1, string ad2, string suburb, string city, string firstname
                                , string lastname, string username, string email, string contactNo, string password,
                                string userimage, string passReset)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@employeeID", empID),
                    new SqlParameter("@bio", bio),
                    new SqlParameter("@AddressLine1",ad1),
                    new SqlParameter("@AddressLine2",ad2 ),
                    new SqlParameter("@suburb", suburb),
                    new SqlParameter("@city", city),
                    new SqlParameter("@firstname", firstname),
                    new SqlParameter("@lastname", lastname),
                    new SqlParameter("@username", username),
                    new SqlParameter("@email", email),
                    new SqlParameter("@contactNo", contactNo),
                    new SqlParameter("@password", password),
                    new SqlParameter("@userimage", userimage),
                    new SqlParameter("@passReset", passReset),
                };
                return DBHelper.NonQuery("SP_AddEmployee", CommandType.StoredProcedure, pars);
            }
            catch (Exception E)
            {
                throw new ApplicationException(E.ToString());
            }
        }

        public bool updateEmployee(EMPLOYEE emp)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@empID", emp.EmployeeID.ToString()),
                    new SqlParameter("@type", emp.Type.ToString()),
                    new SqlParameter("@bio",emp.Bio.ToString()),
                    new SqlParameter("@Specialisation", emp.Specialisation.ToString()),
                    new SqlParameter("@addLine1", emp.AddressLine1.ToString()),
                    new SqlParameter("@addLine2", emp.AddressLine2.ToString()),
                    new SqlParameter("@suburb", emp.Suburb.ToString()),
                    new SqlParameter("@city", emp.City.ToString())
                };
                return DBHelper.NonQuery("SP_UpdateEmployee", CommandType.StoredProcedure, pars);
            }
            catch (Exception E)
            {
                throw new ApplicationException(E.ToString());
            }
        }

        public List<SP_GetSlotTimes> GetAllTimeSlots()
        {
            List<SP_GetSlotTimes> list = new List<SP_GetSlotTimes>();
            try
            {
                using (DataTable table = DBHelper.Select("SP_GetTimeSlots", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetSlotTimes slots = new SP_GetSlotTimes
                            {
                                SlotNo = Convert.ToString(row["SlotNo"]),
                                Time = Convert.ToDateTime(row["StartTime"].ToString())
                            };
                            list.Add(slots);
                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public List<SP_GetStylists> GetAllStylists()
        {
            try
            {
                List<SP_GetStylists> stylistList = new List<SP_GetStylists>();
                using (DataTable table = DBHelper.Select("SP_GetStylist", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetStylists stylists = new SP_GetStylists
                            {
                                UserID = Convert.ToString(row["UserID"]),
                                FirstName = Convert.ToString(row["FirstName"]),
                                ServiceID = Convert.ToString(row["ServiceID"]),
                                ServiceName = Convert.ToString(row["Name"])
                            };
                            stylistList.Add(stylists);
                        }
                    }
                }
                return stylistList;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());

            }
        }
        #region Schedule
        #region Specific Stylist
        public List<SP_GetEmpAgenda> GetEmpAgenda(string employeeID, DateTime bookingDate, string sortBy, string sortDir)
        {
            SP_GetEmpAgenda emp = null;
            List<SP_GetEmpAgenda> agenda = new List<SP_GetEmpAgenda>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", employeeID),
                new SqlParameter("@Date", bookingDate),
                new SqlParameter("@sortBy", sortBy),
                new SqlParameter("@sortDir", sortDir)
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
                                PrimaryID = Convert.ToString(row["PrimaryID"]),
                                UserID = Convert.ToString(row["UserID"]),
                                StartTime = Convert.ToDateTime((row["StartTime"]).ToString()),
                                EndTime = Convert.ToDateTime((row["EndTime"]).ToString()),
                                CustomerFName = Convert.ToString(row["CustomerFName"]),
                                EmpFName = Convert.ToString(row["EmpFName"]),
                                Arrived = Convert.ToString(row["Arrived"]),
                                Date = Convert.ToDateTime(row["Date"]),
                                empID = Convert.ToString(row["EmpID"]),
                                Comment = Convert.ToString(row["Comment"])
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
        public List<SP_GetStylistBookings> getStylistUpcomingBkForDate(string empID, DateTime day, string sortBy, string sortDir)
        {
            SP_GetStylistBookings s = null;
            List<SP_GetStylistBookings> bookings = new List<SP_GetStylistBookings>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@stylistID", empID),
                new SqlParameter("@day", day),
                new SqlParameter("@sortBy", sortBy),
                new SqlParameter("@sortDir", sortDir)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_StylistUpcomingBkForDate", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            s = new SP_GetStylistBookings
                            {
                                BookingID = row["BookingID"].ToString(),
                                PrimaryID = row["PrimaryID"].ToString(),
                                StylistID = row["StylistID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                FullName = row["FullName"].ToString(),
                                BookingDate = Convert.ToDateTime(row["Date"]),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                EndTime = Convert.ToDateTime(row["EndTime"].ToString()),
                                //ServiceID = row["ProductID"].ToString(),
                                //ServiceName = row["Name"].ToString(),
                                //ServiceDescription = row["ProductDescription"].ToString(),
                                Arrived = row["Arrived"].ToString()
                            };
                            bookings.Add(s);
                        }
                    }
                }
                return bookings;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetStylistBookings> getStylistPastBookings(string empID, string sortBy, string sortDir)
        {
            SP_GetStylistBookings s = null;
            List<SP_GetStylistBookings> bookings = new List<SP_GetStylistBookings>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@stylistID", empID),
                new SqlParameter("@sortBy", sortBy),
                new SqlParameter("@sortDir", sortDir)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_StylistPastBookings", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            s = new SP_GetStylistBookings
                            {
                                BookingID = row["BookingID"].ToString(),
                                PrimaryID = row["PrimaryID"].ToString(),
                                StylistID = row["StylistID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                FullName = row["FullName"].ToString(),
                                BookingDate = Convert.ToDateTime(row["Date"]),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                EndTime = Convert.ToDateTime(row["EndTime"].ToString()),
                                //ServiceID = row["ProductID"].ToString(),
                                //ServiceName = row["Name"].ToString(),
                                //ServiceDescription = row["ProductDescription"].ToString(),
                                Arrived = row["Arrived"].ToString()
                            };
                            bookings.Add(s);
                        }
                    }
                }
                return bookings;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetStylistBookings> getStylistPastBookingsDateRange(string empID, DateTime startDate, DateTime endDate, string sortBy, string sortDir)
        {
            SP_GetStylistBookings s = null;
            List<SP_GetStylistBookings> bookings = new List<SP_GetStylistBookings>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@stylistID", empID),
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate),
                new SqlParameter("@sortBy",sortBy),
                new SqlParameter("@sortDir", sortDir)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_StylistPastBookingsDateRange", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            s = new SP_GetStylistBookings
                            {
                                BookingID = row["BookingID"].ToString(),
                                PrimaryID = row["PrimaryID"].ToString(),
                                StylistID = row["StylistID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                FullName = row["FullName"].ToString(),
                                BookingDate = Convert.ToDateTime(row["Date"]),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                EndTime = Convert.ToDateTime(row["EndTime"].ToString()),
                                //ServiceID = row["ProductID"].ToString(),
                                //ServiceName = row["Name"].ToString(),
                                //ServiceDescription = row["ProductDescription"].ToString(),
                                Arrived = row["Arrived"].ToString()
                            };
                            bookings.Add(s);
                        }
                    }
                }
                return bookings;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetStylistBookings> getStylistUpcomingBookings(string empID, string sortBy, string sortDir)
        {
            SP_GetStylistBookings s = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@stylistID", empID),
                new SqlParameter("@sortBy", sortBy),
                new SqlParameter("@sortDir", sortDir)
            };
            List<SP_GetStylistBookings> bookings = new List<SP_GetStylistBookings>();
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_StylistUpcomingBookings", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            s = new SP_GetStylistBookings
                            {
                                BookingID = row["BookingID"].ToString(),
                                PrimaryID = row["PrimaryID"].ToString(),
                                StylistID = row["StylistID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                FullName = row["FullName"].ToString(),
                                BookingDate = Convert.ToDateTime(row["Date"]),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                EndTime = Convert.ToDateTime(row["EndTime"].ToString()),
                                //ServiceID = row["ProductID"].ToString(),
                                //ServiceName = row["Name"].ToString(),
                                //ServiceDescription = row["ProductDescription"].ToString(),
                                Arrived = row["Arrived"].ToString()
                            };
                            bookings.Add(s);
                        }
                    }
                }
                return bookings;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetStylistBookings> getStylistUpcomingBookingsDR(string empID, DateTime startDate, DateTime endDate, string sortBy, string sortDir)
        {
            SP_GetStylistBookings s = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@stylistID", empID),
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate",endDate),
                new SqlParameter("@sortBy", sortBy),
                new SqlParameter("@sortDir", sortDir)
            };
            List<SP_GetStylistBookings> bookings = new List<SP_GetStylistBookings>();
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_StylistUpcomingBookingsDR", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            s = new SP_GetStylistBookings
                            {
                                BookingID = row["BookingID"].ToString(),
                                PrimaryID = row["PrimaryID"].ToString(),
                                StylistID = row["StylistID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                FullName = row["FullName"].ToString(),
                                BookingDate = Convert.ToDateTime(row["Date"]),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                EndTime = Convert.ToDateTime(row["EndTime"].ToString()),
                                //ServiceID = row["ProductID"].ToString(),
                                //ServiceName = row["Name"].ToString(),
                                //ServiceDescription = row["ProductDescription"].ToString(),
                                Arrived = row["Arrived"].ToString()
                            };
                            bookings.Add(s);
                        }
                    }
                }
                return bookings;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        #endregion
        #region All Stylists
        public List<SP_GetStylistBookings> getAllStylistsUpcomingBksForDate(DateTime bookingDate, string sortBy, string sortDir)
        {
            SP_GetStylistBookings s = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@bookingDate", bookingDate),
                new SqlParameter("@sortBy", sortBy),
                new SqlParameter("@sortDir", sortDir)
            };
            List<SP_GetStylistBookings> bookings = new List<SP_GetStylistBookings>();
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_AllStylistsUpcomingBksForDate", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            s = new SP_GetStylistBookings
                            {
                                BookingID = row["BookingID"].ToString(),
                                PrimaryID = row["PrimaryID"].ToString(),
                                StylistID = row["StylistID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                FullName = row["FullName"].ToString(),
                                BookingDate = Convert.ToDateTime(row["Date"]),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                EndTime = Convert.ToDateTime(row["EndTime"].ToString()),
                                //ServiceID = row["ProductID"].ToString(),
                                //ServiceName = row["Name"].ToString(),
                                //ServiceDescription = row["ProductDescription"].ToString(),
                                Arrived = row["Arrived"].ToString()
                            };
                            bookings.Add(s);
                        }
                    }
                }
                return bookings;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetStylistBookings> getAllStylistsUpcomingBksDR(DateTime startDate, DateTime endDate, string sortBy, string sortDir)
        {
            SP_GetStylistBookings s = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate),
                new SqlParameter("@sortBy",sortBy),
                new SqlParameter("@sortDir", sortDir)
            };
            List<SP_GetStylistBookings> bookings = new List<SP_GetStylistBookings>();
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_AllStylistsUpcomingBksDR", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            s = new SP_GetStylistBookings
                            {
                                BookingID = row["BookingID"].ToString(),
                                PrimaryID = row["PrimaryID"].ToString(),
                                StylistID = row["StylistID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                FullName = row["FullName"].ToString(),
                                BookingDate = Convert.ToDateTime(row["Date"]),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                EndTime = Convert.ToDateTime(row["EndTime"].ToString()),
                                //ServiceID = row["ProductID"].ToString(),
                                //ServiceName = row["Name"].ToString(),
                                //ServiceDescription = row["ProductDescription"].ToString(),
                                Arrived = row["Arrived"].ToString()
                            };
                            bookings.Add(s);
                        }
                    }
                }
                return bookings;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetStylistBookings> getStylistPastBksForDate(string empID, DateTime day, string sortBy, string sortDir)
        {
            SP_GetStylistBookings s = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@stylistID", empID),
                new SqlParameter("@day", day),
                new SqlParameter("@sortBy",sortBy),
                new SqlParameter("@sortDir",sortDir)
            };
            List<SP_GetStylistBookings> bookings = new List<SP_GetStylistBookings>();
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_StylistPastBksForDate", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            s = new SP_GetStylistBookings
                            {
                                BookingID = row["BookingID"].ToString(),
                                PrimaryID = row["PrimaryID"].ToString(),
                                StylistID = row["StylistID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                FullName = row["FullName"].ToString(),
                                BookingDate = Convert.ToDateTime(row["Date"]),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                EndTime = Convert.ToDateTime(row["EndTime"].ToString()),
                                //ServiceID = row["ProductID"].ToString(),
                                //ServiceName = row["Name"].ToString(),
                                //ServiceDescription = row["ProductDescription"].ToString(),
                                Arrived = row["Arrived"].ToString()
                            };
                            bookings.Add(s);
                        }
                    }
                }
                return bookings;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetStylistBookings> getAllStylistsUpcomingBookings(string sortBy, string sortDir)
        {
            SP_GetStylistBookings s = null;
            List<SP_GetStylistBookings> bookings = new List<SP_GetStylistBookings>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@sortBy", sortBy),
                new SqlParameter("@sortDir", sortDir)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_AllStylistsUpcomingBookings", CommandType.StoredProcedure,pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            s = new SP_GetStylistBookings
                            {
                                BookingID = row["BookingID"].ToString(),
                                PrimaryID = row["PrimaryID"].ToString(),
                                StylistID = row["StylistID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                FullName = row["FullName"].ToString(),
                                BookingDate = Convert.ToDateTime(row["Date"]),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                EndTime = Convert.ToDateTime(row["EndTime"].ToString()),
                                //ServiceID = row["ProductID"].ToString(),
                                //ServiceName = row["Name"].ToString(),
                                //ServiceDescription = row["ProductDescription"].ToString(),
                                Arrived = row["Arrived"].ToString()
                            };
                            bookings.Add(s);
                        }
                    }
                }
                return bookings;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetStylistBookings> getAllStylistsPastBookings(string sortBy, string sortDir)
        {
            SP_GetStylistBookings s = null;
            List<SP_GetStylistBookings> bookings = new List<SP_GetStylistBookings>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@sortBy", sortBy),
                new SqlParameter("@sortDir", sortDir)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_AllStylistsPastBookings", CommandType.StoredProcedure,pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            s = new SP_GetStylistBookings
                            {
                                BookingID = row["BookingID"].ToString(),
                                PrimaryID = row["PrimaryID"].ToString(),
                                StylistID = row["StylistID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                FullName = row["FullName"].ToString(),
                                BookingDate = Convert.ToDateTime(row["Date"]),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                EndTime = Convert.ToDateTime(row["EndTime"].ToString()),
                                //ServiceID = row["ProductID"].ToString(),
                                //ServiceName = row["Name"].ToString(),
                                //ServiceDescription = row["ProductDescription"].ToString(),
                                Arrived = row["Arrived"].ToString()
                            };
                            bookings.Add(s);
                        }
                    }
                }
                return bookings;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetStylistBookings> getAllStylistsPastBksForDate(DateTime date, string sortBy, string sortDir)
        {
            SP_GetStylistBookings s = null;
            List<SP_GetStylistBookings> bookings = new List<SP_GetStylistBookings>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@day", date),
                new SqlParameter("@sortBy",sortBy),
                new SqlParameter("@sortDir",sortDir)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_AllStylistPastBksForDate", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            s = new SP_GetStylistBookings
                            {
                                BookingID = row["BookingID"].ToString(),
                                PrimaryID = row["PrimaryID"].ToString(),
                                StylistID = row["StylistID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                FullName = row["FullName"].ToString(),
                                BookingDate = Convert.ToDateTime(row["Date"]),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                EndTime = Convert.ToDateTime(row["EndTime"].ToString()),
                                //ServiceID = row["ProductID"].ToString(),
                                //ServiceName = row["Name"].ToString(),
                                //ServiceDescription = row["ProductDescription"].ToString(),
                                Arrived = row["Arrived"].ToString()
                            };
                            bookings.Add(s);
                        }
                    }
                }
                return bookings;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetStylistBookings> getAllStylistsPastBookingsDateRange(DateTime startDate, DateTime endDate, string sortBy, string sortDir)
        {
            SP_GetStylistBookings s = null;
            List<SP_GetStylistBookings> bookings = new List<SP_GetStylistBookings>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate),
                new SqlParameter("@sortBy", sortBy),
                new SqlParameter("@sortDir", sortDir)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_AllStylistsPastBksDR", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            s = new SP_GetStylistBookings
                            {
                                BookingID = row["BookingID"].ToString(),
                                PrimaryID = row["PrimaryID"].ToString(),
                                StylistID = row["StylistID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                FullName = row["FullName"].ToString(),
                                BookingDate = Convert.ToDateTime(row["Date"]),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                EndTime = Convert.ToDateTime(row["EndTime"].ToString()),
                                //ServiceID = row["ProductID"].ToString(),
                                //ServiceName = row["Name"].ToString(),
                                //ServiceDescription = row["ProductDescription"].ToString(),
                                Arrived = row["Arrived"].ToString()
                            };
                            bookings.Add(s);
                        }
                    }
                }
                return bookings;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        #endregion
        #endregion
        public List<SP_AboutStylist> aboutStylist()
        {
            SP_AboutStylist s = null;
            List<SP_AboutStylist> stylistsList = new List<SP_AboutStylist>();
            try
            {
                using (DataTable table = DBHelper.Select("SP_AboutStylist", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            s = new SP_AboutStylist
                            {
                                UserImage = row["UserImage"].ToString(),
                                EmployeeID = row["EmployeeID"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                Type = row["Type"].ToString(),
                                ServiceID = row["ServiceID"].ToString(),
                                Specialisation = row["Specialisation"].ToString(),
                                SpecDesc = row["SpecialisationDescription"].ToString(),
                                Bio = row["Bio"].ToString(),
                            };
                            stylistsList.Add(s);
                        }
                    }
                }
                return stylistsList;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        
        public bool AddService(PRODUCT p, SERVICE s)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@ProductID", p.ProductID.ToString()),
                    new SqlParameter("@Name", p.Name.ToString()),
                    new SqlParameter("@Description", p.ProductDescription.ToString()),
                    new SqlParameter("@Price", p.Price.ToString()),
                    new SqlParameter("@Slots", s.NoOfSlots.ToString()),
                    new SqlParameter("@Type", s.Type.ToString()),

                };
                return DBHelper.NonQuery("SP_AddService", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }


        public List<SP_GetWidth> GetWidths()
        {
            try
            {
                List<SP_GetWidth> widthList = new List<SP_GetWidth>();
                using (DataTable table = DBHelper.Select("SP_GetWidths", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetWidth width = new SP_GetWidth
                            {
                                WidthID = Convert.ToString(row["WidthID"]),
                                Description = Convert.ToString(row["Description"])
                            };
                            widthList.Add(width);
                        }
                    }
                }
                return widthList;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());

            }


        }


        public List<SP_GetStyles> GetStyles()
        {
            try
            {
                List<SP_GetStyles> styleList = new List<SP_GetStyles>();
                using (DataTable table = DBHelper.Select("SP_GetStyles", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetStyles styles = new SP_GetStyles
                            {
                                StyleID = Convert.ToString(row["StyleID"]),
                                Description = Convert.ToString(row["Description"])
                            };
                            styleList.Add(styles);
                        }
                    }
                }
                return styleList;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());

            }


        }
        public List<SP_GetLength> GetLengths()
        {
            try
            {
                List<SP_GetLength> lengthList = new List<SP_GetLength>();
                using (DataTable table = DBHelper.Select("SP_GetLengths", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetLength length = new SP_GetLength
                            {
                                LengthID = Convert.ToString(row["LengthID"]),
                                Description = Convert.ToString(row["Description"])
                            };
                            lengthList.Add(length);
                        }
                    }
                }
                return lengthList;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());

            }


        }
        public bool AddBraidService(BRAID_SERVICE bs)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@ProductID", bs.ProductID.ToString()),
                    new SqlParameter("@StyleID", bs.StyleID.ToString()),
                    new SqlParameter("@LengthID", bs.LengthID.ToString()),
                    new SqlParameter("@WidthID", bs.WidthID.ToString())
                };
                return DBHelper.NonQuery("SP_AddBraidService", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public bool AddToBookingService(BookingService bs)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@BookingID", bs.BookingID.ToString()),
                    new SqlParameter("@ServiceID", bs.ServiceID.ToString())
                };
                return DBHelper.NonQuery("SP_AddToBookingService", CommandType.StoredProcedure, pars.ToArray());
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }

        }
        public bool UpdateService(PRODUCT p, SERVICE s)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@ServiceID", p.ProductID.ToString()),
                    new SqlParameter("@Price", p.Price.ToString()),
                    new SqlParameter("@Slots", s.NoOfSlots.ToString())
                };
                return DBHelper.NonQuery("SP_UpdateService", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }

        }
         public SERVICE GetSlotLength(string serviceID)
        {
            SERVICE service = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@ServiceID", serviceID)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetSlotLength",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        service = new SERVICE
                        {
                            NoOfSlots = Convert.ToInt32(table.Rows[0]["NoOfSlots"].ToString())

                        };
                    }
                    return service;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public SP_GetEmployee_S_ getEmployee_S(string stylistID)
        {
            SP_GetEmployee_S_ stylist = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", stylistID)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetEmployee",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        stylist = new SP_GetEmployee_S_
                        {
                            UserImage = Convert.ToString(row["UserImage"]),
                            StylistID = Convert.ToString(row["UserID"]),
                            FirstName = Convert.ToString(row["FirstName"]),
                            LastName = Convert.ToString(row["LastName"]),
                            Username = Convert.ToString(row["UserName"]),
                            Email = Convert.ToString(row["Email"]),
                            ContactNo = Convert.ToString(row["ContactNo"]),
                            Type = Convert.ToString(row["Type"]),
                            Active = Convert.ToString(row["Active"]),
                            ad1 = Convert.ToString(row["AddressLine1"]),
                            ad2 = Convert.ToString(row["AddressLine2"]),
                            Suburb = Convert.ToString(row["Suburb"]),
                            City = Convert.ToString(row["City"]),
                            Bio = row["Bio"].ToString(),
                            ServiceID = row["ServiceID"].ToString(),
                            Specialisation = row["Specialisation"].ToString(),
                            SpecDesc = row["SpecialisationDescription"].ToString(),
                            
                        };
                    }
                    return stylist;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public bool addSpecialisation(STYLIST_SERVICE ss)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@employeeID", ss.EmployeeID.ToString()),
                    new SqlParameter("@serviceID", ss.ServiceID.ToString())
                };
                return DBHelper.NonQuery("SP_AddSpecialisation", CommandType.StoredProcedure, pars);
            }
            catch (Exception E)
            {
                throw new ApplicationException(E.ToString());
            }
        }

        public SP_GetEmployee_S_ getBio(string id)
        {
            SP_GetEmployee_S_ stylist = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", id)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetBio",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        stylist = new SP_GetEmployee_S_
                        {
                            Bio = row["Bio"].ToString(),
                        };
                    }
                    return stylist;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
         public SP_GetService GetServiceFromID(string id)
        {
            SP_GetService service = null;

            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@ServiceID", id)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetService",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        service = new SP_GetService
                        {
                            ServiceName = row["Name"].ToString(),
                            Description = row["ProductDescription"].ToString(),
                            Price = Convert.ToDecimal(row["Price"].ToString()),
                            NoOfSlots = Convert.ToInt32(row["NoOfSlots"].ToString()),
                            ServiceType = Convert.ToChar(row["ServiceType"].ToString())
                        };
                    }
                    return service;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }

        }
        public SP_GetBraidService GetBraidServiceFromID(string id)
        {
            SP_GetBraidService service = null;

            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@ServiceID", id)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetBraidService",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        service = new SP_GetBraidService
                        {
                            StyleDesc = row["styleDesc"].ToString(),
                            LengthDesc = row["lengthDesc"].ToString(),
                            WidthDesc = row["widthDesc"].ToString()
                        };
                    }
                    return service;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }

        }
        
        public List<SP_GetTopCustomerbyBooking> getTopCustomerByBookings(DateTime startDate, DateTime endDate)
        {
            SP_GetTopCustomerbyBooking customer = null;
            List<SP_GetTopCustomerbyBooking> list = new List<SP_GetTopCustomerbyBooking>();

            SqlParameter[] pars = new SqlParameter[]

            {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
            };


            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetTopCustomers", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            customer = new SP_GetTopCustomerbyBooking
                            {
                                noOfBookings = int.Parse(row[0].ToString()),
                                CustomerID = Convert.ToString(row["CustomerID"]),
                                CustomerName = Convert.ToString(row["custFullName"])
                            };
                            list.Add(customer);
                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());

            }
        }
        public List<SP_GetStylistBookings> getCustomerPastBookingsForDate(string customerID, DateTime day)
        {
            SP_GetStylistBookings s = null;
            List<SP_GetStylistBookings> bookings = new List<SP_GetStylistBookings>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@customerID", customerID),
                new SqlParameter("@date",day)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CustomerPastBookingsForDate", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            s = new SP_GetStylistBookings
                            {
                                BookingID = row["BookingID"].ToString(),
                                PrimaryID = row["PrimaryID"].ToString(),
                                StylistID = row["StylistID"].ToString(),
                                CustomerID = row["CustomerID"].ToString(),
                                StylistName = row["StylistName"].ToString(),
                                FullName = row["CustomerFullName"].ToString(),
                                BookingDate = Convert.ToDateTime(row["Date"]),
                                StartTime = Convert.ToDateTime(row["StartTime"].ToString()),
                                Arrived = row["Arrived"].ToString()
                            };
                            bookings.Add(s);
                        }
                    }
                }
                return bookings;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetLeaveServices> GetLeaveServices()
        {
            SP_GetLeaveServices leaveServicesList = null;
            List<SP_GetLeaveServices> leaveService = new List<SP_GetLeaveServices>();
            try
            {
                using (DataTable table = DBHelper.Select("SP_GetLeaveService",
            CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            leaveServicesList = new SP_GetLeaveServices
                            {
                                ProductID = row["ProductID"].ToString(),
                                Name = row["Name"].ToString(),
                                NoOfSlots = Convert.ToInt32(row["NoOfSlots"].ToString()),
                            };
                            leaveService.Add(leaveServicesList);
                        }
                    }
                    return leaveService;
                }
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public bool UpdateOrder(string orderID, DateTime dateReceived, bool received)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@OrderID", orderID),
                    new SqlParameter("@DateReceived", dateReceived),
                    new SqlParameter("@Received", received)
                };

                return DBHelper.NonQuery("SP_UpdateOrder", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public bool UpdateQtyOnHand(string prodID, int qty)
        {
            try
            {
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@ProductID", prodID),
                    new SqlParameter("@Qty", qty)
                };

                return DBHelper.NonQuery("SP_UpdateQtyOnHand", CommandType.StoredProcedure, pars);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public SP_ReturnBooking returnNextBooking(string startTime, string bookingID, string stylistID, DateTime date)
        {
            SP_ReturnBooking rb = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@startTime", startTime.ToString()),
                new SqlParameter("@bookingID", bookingID),
                new SqlParameter("@stylistID", stylistID),
                new SqlParameter("@date", date)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ReturnNextBooking",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        rb = new SP_ReturnBooking
                        {
                            bookingID = row["BookingID"].ToString(),
                            slotNo = row["SlotNo"].ToString(),
                            startTime = row["StartTime"].ToString()
                        };
                    }
                    return rb;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public SP_ReturnBooking returnBooking(string bookingID, string customerID, string stylistID, DateTime date)
        {
            SP_ReturnBooking rb = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@bookingID", bookingID),
                new SqlParameter("@customerID", customerID),
                new SqlParameter("@stylistID", stylistID),
                new SqlParameter("@date", date)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ReturnBooking",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        rb = new SP_ReturnBooking
                        {
                            bookingID = row["BookingID"].ToString(),
                            customerID = row["CustomerID"].ToString(),
                            stylistID = row["StylistID"].ToString(),
                            slotNo = row["SlotNo"].ToString(),
                            startTime = row["StartTime"].ToString(),
                            date = Convert.ToDateTime(row["Date"].ToString())
                        };
                    }
                    return rb;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_ReturnAvailServices> returnAvailServices(int num)
        {
            List<SP_ReturnAvailServices> list = new List<SP_ReturnAvailServices>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@slots", num)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_ReturnAvailServices", CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_ReturnAvailServices serv = new SP_ReturnAvailServices()
                            {
                                serviceID = row["ServiceID"].ToString(),
                                name = row["Name"].ToString(),
                                productDescription = row["ProductDescription"].ToString(),
                                price= Convert.ToDecimal(row["Price"]),
                                noSlots = Convert.ToInt32(row["NoOfSlots"].ToString()),
                                type = row["Type(A/N/B)"].ToString()
                            };
                            list.Add(serv);
                        }
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public SP_GetBookingServices getLeaveReason(string BookingID)
        {
            SP_GetBookingServices bookingServices = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@BookingID", BookingID)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetBookingServices",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];

                        bookingServices = new SP_GetBookingServices
                        {
                            BookingID = row["BookingID"].ToString(),
                            ServiceID = row["ServiceID"].ToString(),
                            ServiceName = row["Name"].ToString(),
                            serviceDescripion = row["ProductDescription"].ToString(),
                            Price = Convert.ToDouble(row["Price"]),
                            type = row["ProductType(T/A/S)"].ToString()
                        };
                        
                    }
                    return bookingServices;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
    }
}                  
