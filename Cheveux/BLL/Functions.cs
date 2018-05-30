using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                VATRate = (Handler.GetVATRate().VATRate/100)+1;
            }catch (ApplicationException Err)
            {
                logAnError(Err.ToString());
            }
            if (VATRate > 0)
            {
                VATExcluded = VATIncluded/VATRate;
                VAT = VATExcluded - VATIncluded;
            }
            return Tuple.Create(VATExcluded, (VAT*-1));
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
            new System.IO.StreamWriter(@""+ AppDomain.CurrentDomain.BaseDirectory + "CheveuxErrorLog.txt", true))
            {
                file.WriteLine();
                file.WriteLine("TimeStamp: "+DateTime.Now);
                file.WriteLine("Machine Name: " + Environment.MachineName);
                file.WriteLine("OS Version: " + Environment.OSVersion);
                file.WriteLine("Curent User: " + Environment.UserName);
                file.WriteLine("User Domain: " + Environment.UserDomainName);
                file.WriteLine("Curent Directory: " + Environment.CurrentDirectory);
                file.WriteLine("Error: ");
                file.WriteLine(Err);
            }
        }
    }
}