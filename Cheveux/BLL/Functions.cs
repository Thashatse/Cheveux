using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLibrary.ViewModels;
using System.Data;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;

namespace BLL
{
    public class Functions
    {
        DBHandler Handler = new DBHandler();

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

        public string GetFullProductTypeText(char ProductType)
        {
            /*
             * Given abbreviated char that database returns for product type, 
             * this class will return the full text of that product type
             */
            if (ProductType == 'S')
            {
                return "Service";
            }
            else if (ProductType == 'T')
            {
                return "Treatment";
            }
            else if (ProductType == 'A')
            {
                return "Application Service";
            }
            else if (ProductType == 'X')
            {
                return "ALL";
            }
            else
            {
                logAnError("Unknown Product Type given to GetFullProductTypeText method in functions");
                return "error";
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
                logAnError("Unknown Active Type given to GetFullActiveTypeText in functions: "+activeType);
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
            }catch(Exception err)
            {
                throw new Exception(err.ToString());
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

        public string GenerateRandomBookingID()
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
            } while (Handler.getCustomerUpcomingBookingDetails(result) != null);
            return result;
        }

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
                    //check preferd means on comunication
                    if (oGBkngNoti.PreferredCommunication == 'E')
                    {
                        //send an email notification
                        var body = new System.Text.StringBuilder();
                        body.AppendFormat("Hello, " + oGBkngNoti.FirstName.ToString() + ",");
                        body.AppendLine(@"");
                        body.AppendLine(@"You have a booking with "+oGBkngNoti.stylistFirstName.ToString() + " Tomorow ("+oGBkngNoti.Date.ToString("dd MMM yyyy")+") at "+oGBkngNoti.StartTime.ToString("hh:mm")+".");
                        body.AppendLine(@"You booking is for "+oGBkngNoti.serviceName.ToString() + " at a cost of R"+ string.Format("{0:#.00}", oGBkngNoti.Price.ToString()) +".");
                        body.AppendLine(@"");
                        body.AppendLine(@"View or change your booking details here: http://sict-iis.nmmu.ac.za/beauxdebut/Profile.aspx.");
                        body.AppendLine(@"See you tomorow at "+oGBkngNoti.StartTime.ToString("hh:mm") + ".");
                        body.AppendLine(@"");
                        body.AppendLine(@"Regards,");
                        body.AppendLine(@"");
                        body.AppendLine(@"The Cheveux Team");
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
    }
}