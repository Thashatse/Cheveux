﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLibrary.ViewModels;
using System.Data;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using TypeLibrary.Models;

namespace BLL
{
    public class Functions
    {
        DBHandler Handler = new DBHandler();

        public void runAutoFunctions()
        {
            sendOGBkngNoti();
            autoPurchOrders();
        }

        public Tuple<double, double> getVat(double VATIncluded)
        {
            /*
            * Given an VAT included price, 
            * this class will return the vat and orriganal price
            * Returns -1 when error Occurs
            */
            double VATExcluded = -1;
            double VAT = -1;
            double VATRate = -1;
            try
            {
                VATRate = (Handler.GetVATRate().VATRate / 100) + 1;
            }
            catch (ApplicationException Err)
            {
                logAnError(Err.ToString());
            }
            if (VATRate > 0)
            {
                VATExcluded = VATIncluded / VATRate;
                VAT = VATExcluded - VATIncluded;
            }
            return Tuple.Create(VATExcluded, (VAT * -1));
        }

        public void logAnError(string Err)
        {
            /*
            * Logs Error Details in a text File
            */
                try
                {
                    using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(@"" + AppDomain.CurrentDomain.BaseDirectory + "CheveuxErrorLog.txt", true))
                    {
                        file.WriteLine();
                        file.WriteLine("TimeStamp: " + DateTime.Now);
                        file.WriteLine("Machine Name: " + Environment.MachineName);
                        file.WriteLine("OS Version: " + Environment.OSVersion);
                        file.WriteLine("Curent User: " + Environment.UserName);
                        file.WriteLine("User Domain: " + Environment.UserDomainName);
                        file.WriteLine("Curent Directory: " + Environment.CurrentDirectory);
                        file.WriteLine("Error: ");
                        file.WriteLine(Err);
                    }
                }
                catch
                {

                }
        }

        public string goToPreviousPage(string PreviousPage)
        {
            //return the page to redirect to if there is one
            if (PreviousPage == "Help/CheveuxHelpCenter.aspx")
            {
                return ("../Help/CheveuxHelpCenter.aspx#InternalHelp");
            }
            else if (PreviousPage == "BusinessSetting.aspx")
            {
                return ("../Manager/BusinessSetting.aspx");
            }
            else if (PreviousPage == "Reports.aspx")
            {
                return ("../Manager/Reports.aspx");
            }
            else if (PreviousPage == "Manager.aspx")
            {
                return ("../Manager/Dashboard.aspx");
            }
            else if (PreviousPage == "Employee.aspx")
            {
                return ("../Manager/Employee.aspx");
            }
            else if (PreviousPage == "Products.aspx")
            {
                return ("../Manager/Products.aspx");
            }
            else if (PreviousPage == "Service.aspx")
            {
                return ("../Manager/Service.aspx");
            }
            else if (PreviousPage == "Profile.aspx")
            {
                return "Profile.aspx";
            }
            else
            {
                return null;
            }
        }

        public bool compareToSearchTerm(string toBeCompared, string comparedTo)
        {
            //check if toBeCompared is contained in comparedTo 
            bool result = false;
            if (comparedTo != null)
            {
                toBeCompared = toBeCompared.ToLower();
                string searcTearm = comparedTo.ToLower();
                if (toBeCompared.Contains(searcTearm))
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }
            return result;
        }

        #region Generate IDs
        public string GenerateRandomBookingID()
        {
            try
            {
                string result;
            do
            {
                int[] id = new int[9];
                Random rn = new Random();
                for (int i = 0; i < id.Length; i++)
                {
                    id[i] = rn.Next(0, 9);
                }
                result = string.Join("", id);
            } while (Handler.getCustomerUpcomingBookingDetails(result) != null && 
                    Handler.getSale(result) != null );
            return result;
        }
            catch
            {
                throw new Exception();
    }
}

        public string GenerateRandomReviewID()
        {
            try
            {
                string result;
                do
                {
                    int[] id = new int[9];
                    Random rn = new Random();
                    for (int i = 0; i < id.Length; i++)
                    {
                        id[i] = rn.Next(0, 9);
                    }
                    result = string.Join("", id);
                } while (Handler.CheckForReview(result) != null);
                return result;
            }
            catch
            {
                throw new Exception();
            }
        }

        public string GenerateRandomProductID()
        {
            try
            {
                string result;
                do
                {
                    int[] id = new int[3];
                    Random rn = new Random();
                    for (int i = 0; i < id.Length; i++)
                    {
                        id[i] = rn.Next(0, 9);
                    }
                    result = "Pr" + string.Join("", id);
                } while (Handler.CheckForProduct(result) != null);
                return result;
            }
            catch
            {
                throw new Exception();
            }
        }

        public string GenerateRandomOrderID()
        {
            try
            {
                string result;
                do
                {
                    int[] id = new int[9];
                    Random rn = new Random();
                    for (int i = 0; i < id.Length; i++)
                    {
                        id[i] = rn.Next(0, 9);
                    }
                    result = string.Join("", id);
                } while (Handler.CheckForOrder(result) != null);
                return result;
            }
            catch
            {
                throw new Exception();
            }
        }

        public string GenerateRandomBrandID()
        {
            try
            {
                string result;
                do
                {
                    int[] id = new int[9];
                    Random rn = new Random();
                    for (int i = 0; i < id.Length; i++)
                    {
                        id[i] = rn.Next(0, 9);
                    }
                    result = string.Join("", id);
                } while (Handler.CheckForBrand(result) != null);
                return result;
            }
            catch
            {
                throw new Exception();
            }
        }

        public string GenerateRandomSupplierID()
        {
            try
            {
                string result;
                do
                {
                    int[] id = new int[9];
                    Random rn = new Random();
                    for (int i = 0; i < id.Length; i++)
                    {
                        id[i] = rn.Next(0, 9);
                    }
                    result = string.Join("", id);
                } while (Handler.CheckForSupplier(result) != null);
                return result;
            }
            catch
            {
                throw new Exception();
            }
        }

        public string GenerateRandomServiceTypeID()
        {
            try
            {
                string result;
                bool add = true;
                do
                {
                    int[] id = new int[3];
                    Random rn = new Random();
                    for (int i = 0; i < id.Length; i++)
                    {
                        id[i] = rn.Next(0, 9);
                    }
                    result = string.Join("", id);

                    List<ProductType> types = Handler.getProductTypes();
                    foreach (ProductType type in types)
                    {
                        if (type.typeID.Replace(" ", string.Empty) == result)
                        {
                            add = false;
                        }
                    }

                } while (add == false);
                return result;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region Emails
        public bool sendEmailAlert(string receverAddress, string reciverName, string subject, string body, string senderName)
        {
            bool success = false;
            try
            {
                var fromAddress = new MailAddress("BookingsCheveux2@gmail.com", senderName);
                var toAddress = new MailAddress(receverAddress, reciverName);
                const string fromPassword = "W$fu7a61k";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                success = true;
            }
            catch (Exception err)
            {
                logAnError("Error sending email To: "+ receverAddress 
                    +"Subject: "+ subject
                    +" Error:"+ err);
                success = false;
            }
            return success;
        }

        public void sendOGBkngNoti()
        {
            try
            {
                List<OGBkngNoti> oGBkngNotiList = Handler.GetOGBkngNotis();
                foreach (OGBkngNoti oGBkngNoti in oGBkngNotiList)
                {
                    List<SP_GetBookingServices> bookingServiceList = Handler.getBookingServices(oGBkngNoti.BookingID.ToString());
                    //check preferd means on comunication
                    if (oGBkngNoti.PreferredCommunication == 'E')
                    {
                        //send an email notification
                        var body = new System.Text.StringBuilder();

                        //construct the email boady
                        if (bookingServiceList.Count == 1)
                        {
                            body.AppendFormat("Hello, " + oGBkngNoti.FirstName.ToString() + ",");
                            body.AppendLine(@"");
                            body.AppendLine(@"You have a booking with " + oGBkngNoti.stylistFirstName.ToString() +
                                " Tomorow (" + oGBkngNoti.Date.ToString("dd MMM yyyy") + ") at " +
                                oGBkngNoti.StartTime.ToString("HH:mm") + ".");
                            body.AppendLine(@"You booking is for " + bookingServiceList[0].ServiceName.ToString() +
                                " at a cost of R" + string.Format("{0:#.00}", bookingServiceList[0].Price.ToString()) + ".");                           body.AppendLine(@"");
                            body.AppendLine(@"View or change your booking details here: "+
                                "http://sict-iis.nmmu.ac.za/beauxdebut/ViewBooking.aspx?BookingID=" + 
                                oGBkngNoti.BookingID.ToString().Replace(" ", string.Empty) + ".");
                            body.AppendLine(@"See you tomorow at " + oGBkngNoti.StartTime.ToString("HH:mm") + ".");
                            body.AppendLine(@"");
                            body.AppendLine(@"Regards,");
                            body.AppendLine(@"");
                            body.AppendLine(@"The Cheveux Team");
                        }
                        else if (bookingServiceList.Count == 2)
                        {
                            //calculate Price
                            double price = 0;
                            foreach (SP_GetBookingServices servicePrice in bookingServiceList)
                            {
                                price += servicePrice.Price;
                            }
                            body.AppendFormat("Hello, " + oGBkngNoti.FirstName.ToString() + ",");
                            body.AppendLine(@"");
                            body.AppendLine(@"You have a booking with " + oGBkngNoti.stylistFirstName.ToString() +
                                " Tomorow (" + oGBkngNoti.Date.ToString("dd MMM yyyy") + ") at " +
                                oGBkngNoti.StartTime.ToString("HH:mm") + ".");
                            body.AppendLine(@"You booking is for " + bookingServiceList[0].ServiceName.ToString() +
                                " & " + bookingServiceList[1].ServiceName.ToString() + " at a cost of R" +
                                string.Format("{0:#.00}", price) + "."); body.AppendLine(@"");
                            body.AppendLine(@"View or change your booking details here: " +
                                "http://sict-iis.nmmu.ac.za/beauxdebut/ViewBooking.aspx?BookingID=" +
                                oGBkngNoti.BookingID.ToString().Replace(" ", string.Empty) + ".");
                            body.AppendLine(@"See you tomorow at " + oGBkngNoti.StartTime.ToString("HH:mm") + ".");
                            body.AppendLine(@"");
                            body.AppendLine(@"Regards,");
                            body.AppendLine(@"");
                            body.AppendLine(@"The Cheveux Team");
                        }
                        else if (bookingServiceList.Count > 2)
                        {
                            //calculate Price
                            double price = 0;
                            foreach (SP_GetBookingServices servicePrice in bookingServiceList)
                            {
                                price += servicePrice.Price;
                            }
                            body.AppendFormat("Hello, " + oGBkngNoti.FirstName.ToString() + ",");
                            body.AppendLine(@"");
                            body.AppendLine(@"You have a booking with " + oGBkngNoti.stylistFirstName.ToString() + 
                                " Tomorow (" + oGBkngNoti.Date.ToString("dd MMM yyyy") + ") at " + 
                                oGBkngNoti.StartTime.ToString("HH:mm") + ".");
                            body.AppendLine(@"You booking will cost of R" +
                                string.Format("{0:#.00}", price) + "."); body.AppendLine(@"");
                            body.AppendLine(@"View or change your booking details here: "+
                                "http://sict-iis.nmmu.ac.za/beauxdebut/ViewBooking.aspx?BookingID=" + 
                                oGBkngNoti.BookingID.ToString().Replace(" ", string.Empty) + ".");
                            body.AppendLine(@"See you tomorow at " + oGBkngNoti.StartTime.ToString("HH:mm") + ".");
                            body.AppendLine(@"");
                            body.AppendLine(@"Regards,");
                            body.AppendLine(@"");
                            body.AppendLine(@"The Cheveux Team");
                        }
                        
                        //send the email
                        sendEmailAlert(oGBkngNoti.Email.ToString(), oGBkngNoti.FirstName.ToString() + " " + oGBkngNoti.lastName.ToString(),
                            "Don't Forget Your Booking Tommorow",
                            body.ToString(),
                            "Bookings Cheveux");
                        Handler.updateNotiStatus(oGBkngNoti.BookingID.ToString(), true);
                    }
                    else if (oGBkngNoti.PreferredCommunication == 'S')
                    {
                        //send an SMS notification
                    }
                }
            }
            catch (Exception err)
            {
                    logAnError("Error sending out going booking notifications: " + err);
            }
        }
        #endregion

        #region Purchase Orders
        public string newPurchaseOrder(Order order, List<Order_DTL> orderDTLs)
        {
                bool success = false;
                string orderID = "";

                try
                {
                    Order newOrder = new Order();
                    newOrder.OrderID = GenerateRandomOrderID();
                newOrder.supplierID = order.supplierID;
                    success = Handler.newProductOrder(newOrder);

                    if (success != false)
                    {
                        foreach (Order_DTL prod in orderDTLs)
                        {
                            Order_DTL newOrderDL = new Order_DTL();
                            newOrderDL.OrderID = newOrder.OrderID;
                            newOrderDL.ProductID = prod.ProductID;
                            newOrderDL.Qty = prod.Qty;
                            success = Handler.newProductOrderDL(newOrderDL);
                        }
                    }

                    orderID = newOrder.OrderID;
                }
                catch (Exception err)
                {
                    logAnError("Error making new product order | Error: " + err);
                return "Err";
                }

                if (success == true)
                {
                    //email to supplier
                    Supplier supp = Handler.getSupplier(order.supplierID);
                    //send an email notification
                    var body = new System.Text.StringBuilder();
                    body.AppendFormat("Hello " + supp.contactName.ToString() + ",");
                    body.AppendLine(@"");
                    body.AppendLine(@"");
                    body.AppendLine(@"Please review the purchase order request at the link below");
                    body.AppendLine(@"");
                    body.AppendLine(@"http://sict-iis.nmmu.ac.za/beauxdebut/Manager/Products.aspx?Action=ViewOrder&OrderID=" + orderID);
                    body.AppendLine(@"");
                    body.AppendLine(@"Regards,");
                    body.AppendLine(@"");
                    body.AppendLine(@"The Cheveux Team");
                    sendEmailAlert(supp.contactEmail, supp.contactName,
                        "Purchase Order Request",
                        body.ToString(),
                        "Cheveux");

                //show order details to user
                return orderID;
                }
                else if (success == false)
                {
                    logAnError("Error making new product order");
                return "Err";
            }
            return "Err";
        }

        public DateTime updateAutoOrderDate(string frequency)
        {
            DateTime NxtOrderdDate = DateTime.Now.AddDays(-1);
            if (frequency == "Asn")
            {
                NxtOrderdDate = DateTime.Now.AddDays(1);
            }
            else if (frequency == "Ewe")
            {
                NxtOrderdDate = DateTime.Now.AddDays(7);
            }
            else if (frequency == "E2w")
            {
                NxtOrderdDate = DateTime.Now.AddDays(14);
            }
            else if (frequency == "Emo")
            {
                NxtOrderdDate = DateTime.Now.AddDays(28);
            }
            return NxtOrderdDate;
        }

        public void autoPurchOrders()
        {
            Stock_Management stockSettings = new Stock_Management();
            List<SP_GetAuto_Purchase_Products> AutoPurchProds = new List<SP_GetAuto_Purchase_Products>();
            List<PRODUCT> allProducts = null;
            string success = "Err";
            try
            {
                stockSettings = Handler.getStockSettings();
                if (stockSettings.AutoPurchase == true)
                {
                    if (stockSettings.NxtOrderdDate < DateTime.Now)
                    {
                        if(stockSettings.AutoPurchaseProducts == true)
                        {
                            AutoPurchProds = Handler.getAutoPurchOrdProds();
                        }
                        else
                        {
                            allProducts = Handler.getAllProducts();
                            foreach(PRODUCT prod in allProducts)
                            {
                                SP_GetAuto_Purchase_Products purchProd = new SP_GetAuto_Purchase_Products();
                                purchProd.ProductID = prod.ProductID;
                                purchProd.Name = prod.Name;
                                purchProd.ProductType = prod.ProductType;
                                purchProd.Qty = stockSettings.PurchaseQty;
                                AutoPurchProds.Add(purchProd);
                            }
                        }

                        List<Supplier> supps = Handler.getSuppliers();
                        foreach (Supplier sup in supps)
                        {
                            Order order = new Order();
                            order.supplierID = sup.supplierID;
                            List<Order_DTL> orderDTLs = new List<Order_DTL>();
                            List<OrderViewModel> outOrders = Handler.getOutStandingOrders();

                            foreach (SP_GetAuto_Purchase_Products prod in AutoPurchProds)
                            {
                                bool add = true;

                                //check if the product is not alread on order 
                                foreach (OrderViewModel outOrder in outOrders)
                                {
                                    List<OrderViewModel> outOrderProducts = Handler.getProductOrderDL(outOrder.OrderID.ToString());
                                    foreach (OrderViewModel outOrderDL in outOrderProducts)
                                    {
                                        if (outOrderDL.ProductID == prod.ProductID)
                                        {
                                            add = false;
                                        }
                                    }
                                }

                                if (add == true)
                                {
                                    if (prod.ProductType == "A")
                                    {
                                        SP_GetAllAccessories prodDetails = Handler.selectAccessory(prod.ProductID);
                                        if (prodDetails.supplierID == sup.supplierID
                                            && prodDetails.Qty < stockSettings.LowStock)
                                        {
                                            Order_DTL newOrderProduct = new Order_DTL();
                                            newOrderProduct.Qty = prod.Qty;
                                            newOrderProduct.ProductID = prod.ProductID;
                                            orderDTLs.Add(newOrderProduct);
                                        }
                                    }
                                    else if (prod.ProductType == "T")
                                    {
                                        SP_GetAllTreatments prodDetails = Handler.selectTreatment(prod.ProductID);
                                        if (prodDetails.supplierID == sup.supplierID
                                            && prodDetails.Qty < stockSettings.LowStock)
                                        {
                                            Order_DTL newOrderProduct = new Order_DTL();
                                            newOrderProduct.Qty = prod.Qty;
                                            newOrderProduct.ProductID = prod.ProductID;
                                            orderDTLs.Add(newOrderProduct);
                                        }
                                    }
                                }
                            }

                            if (orderDTLs.Count > 0)
                            {
                                success = newPurchaseOrder(order, orderDTLs);

                                if (success != "Err")
                                {
                                    stockSettings.NxtOrderdDate = updateAutoOrderDate(stockSettings.AutoPurchaseFrequency);
                                    Handler.updateStockSettings(stockSettings);

                                    //send manager confirmation email
                                    Supplier supp = Handler.getSupplier(order.supplierID);
                                    //send an email notification
                                    var body = new System.Text.StringBuilder();
                                    body.AppendFormat("Hello Mnager,");
                                    body.AppendLine(@"");
                                    body.AppendLine(@"Please review the auto purchase order request sent to " + sup.supplierName + " at the link below");
                                    body.AppendLine(@"");
                                    body.AppendLine(@"http://sict-iis.nmmu.ac.za/beauxdebut/Manager/Products.aspx?Action=ViewOrder&OrderID=" + success);
                                    body.AppendLine(@"");
                                    body.AppendLine(@"The Next Auto Purchase request will occur on " + stockSettings.NxtOrderdDate.ToString("dd MMM yyyy"));
                                    body.AppendLine(@"");
                                    body.AppendLine(@"Regards,");
                                    body.AppendLine(@"");
                                    body.AppendLine(@"The Cheveux Team");
                                    sendEmailAlert(Handler.getManagerContact().Email, "Manager",
                                        "New Auto Purchase Order Request",
                                        body.ToString(),
                                        "Cheveux");
                                }
                                else
                                {
                                    stockSettings.NxtOrderdDate = updateAutoOrderDate(stockSettings.AutoPurchaseFrequency);
                                    Handler.updateStockSettings(stockSettings);

                                    //send manager error email
                                    Supplier supp = Handler.getSupplier(order.supplierID);
                                    //send an email notification
                                    var body = new System.Text.StringBuilder();
                                    body.AppendFormat("Hello Mnager,");
                                    body.AppendLine(@"");
                                    body.AppendLine(@"A failed  auto purchase order request for supplier " + sup.supplierName + " occurred at " + DateTime.Now.ToString("HH:mm dd MMM yyyy"));
                                    body.AppendLine(@"");
                                    body.AppendLine(@"Please contact admin");
                                    body.AppendLine(@"");
                                    body.AppendLine(@"The Next Auto Purchase request will occur on " + stockSettings.NxtOrderdDate.ToString("dd MMM yyyy"));
                                    body.AppendLine(@"");
                                    body.AppendLine(@"Regards,");
                                    body.AppendLine(@"");
                                    body.AppendLine(@"The Cheveux Team");
                                    sendEmailAlert(Handler.getManagerContact().Email, "Manager",
                                        "New Auto Purchase Order Request",
                                        body.ToString(),
                                        "Cheveux");

                                    logAnError("Error making Auto Purchse Order");

                                    success = "";
                                }
                            }
                        }
                    }
                    else
                    {
                        success = "";
                    }
                }
                else
                {
                    success = "";
                }
            }
            catch (Exception err)
            {
                logAnError("Error making Auto Purchse Order: " + err);
            }

            if (success == "Err")
            {
                logAnError("Error making Auto Purchse Order");
                //send an email notification
                var body = new System.Text.StringBuilder();
                body.AppendFormat("Hello Mnager,");
                body.AppendLine(@"");
                body.AppendLine(@"A faild auto purchase order request ocourde at " + DateTime.Now.ToString("HH:mm dd MMM yyyy"));
                body.AppendLine(@"");
                body.AppendLine(@"Please contact admin");
                body.AppendLine(@"");
                body.AppendLine(@"Regards,");
                body.AppendLine(@"");
                body.AppendLine(@"The Cheveux Team");
                sendEmailAlert(Handler.getManagerContact().Email, "Manager",
                    "New Auto Purchase Order Request",
                    body.ToString(),
                    "Cheveux");
            }
        }
        #endregion

        #region Abreviation Full Text
        public string GetFullProductTypeText(string ProductType)
        {
            /*
             * Given abbreviated char that database returns for product type, 
             * this class will return the full text of that product type
             */
            try
            {
                List<ProductType> productTypes = Handler.getProductTypes();
                foreach(ProductType type in productTypes)
                {
                        if (ProductType == type.typeID.Replace(" ", string.Empty))
                        {
                            return type.name;
                        }
                }

                logAnError("Unknown Product Type given to GetFullProductTypeText method in functions");
                return "Error";
            }
            catch (Exception Err)
            {
                logAnError("error ing GetFullProductTypeText method in functions | " + Err.ToString());
                return "Error";
            }
        }

        public string GetFullEmployeeTypeText(char empType)
        {
            /*
             * Given abbreviated char that database returns for product type, 
             * this class will return the full text of that product type
             */
            if (empType == 'S')
            {
                return "Stylist";
            }
            else if (empType == 'R')
            {
                return "Receptionist";
            }
            else if (empType == 'M')
            {
                return "Manager";
            }
            else if (empType == 'A')
            {
                return "Manager";
            }
            else
            {
                logAnError("Unknown Employee Type given to GetFullEmployeeTypeText in functions");
                return "error";
            }
        }

        public string GetFullActiveTypeText(char activeType)
        {
            /*
             * Given abbreviated char that database returns for Active, 
             * this class will return the full text of that Active type
             */
            if (activeType == 'T')
            {
                return "True";
            }
            else if (activeType == 'F')
            {
                return "Fasle";
            }
            else
            {
                logAnError("Unknown Active Type given to GetFullActiveTypeText in functions: " + activeType);
                return "error";
            }
        }

        public string GetFullArrivedStatus(char ArrivedStatus)
        {
            /*
             * Given abbreviated char that database returns for Arrived, 
             * this class will return the full text of that Arrival status
             */
            if (ArrivedStatus == 'Y')
            {
                return "Yes";
            }
            else if (ArrivedStatus == 'N')
            {
                return "No";
            }
            else
            {
                return "error";
            }
        }
        #endregion
    }
}
