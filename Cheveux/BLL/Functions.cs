using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLibrary.ViewModels;
using System.Data;
using System.Web.UI.WebControls;

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
    }
}