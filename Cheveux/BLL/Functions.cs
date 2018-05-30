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
        /*
        public DataTable createInvoiceTable(List<SP_getInvoiceDL> invoiceDetailLines)
        {
            
             * Given an invoice Detail Line List, 
             * this class will return a DATA Table of the Invoice formated corectly
             

        //crate a new table
        DataTable invoice = null;

            //diaplay a heading
            TableRow newRow = new TableRow();
            newRow.Height = 50;

            TableCell newCell = new TableCell();
            newCell.Width = 600;
            newCell.Font.Bold = true;
            newCell.Text = "Invoice:";
            newRow.Cells.Add(newCell);
            newCell = new TableCell();
            newCell.Width = 200;
            newRow.Cells.Add(newCell);
            newCell = new TableCell();
            newCell.Width = 200;
            newRow.Cells.Add(newCell);
            invoice.Rows.InsertAt(newRow, );

            //calculate total price
            double total = 0.0;

            foreach (SP_getInvoiceDL item in invoiceDetailLines)
            {
                newRow = new TableRow();
                newRow.Height = 50;
                //fill in the item
                newCell = new TableCell();
                newCell.Text = item.itemName.ToString();
                newRow.Cells.Add(newCell);
                //fill in the Qty & unit price
                newCell = new TableCell();
                newCell.Text = item.Qty.ToString()+" @ R"+ item.price.ToString();
                newRow.Cells.Add(newCell);
                //fill in the total cost
                newCell = new TableCell();
                newCell.Text = "R"+(item.Qty*item.price).ToString();
                newRow.Cells.Add(newCell);
                //increment final price
                total = item.Qty * item.price;
                invoice.Rows.Add(newRow);
            }

            // get vat info
            Tuple<double, double> vatInfo = getVat(total);

            //display total including and Excluding VAT
            newRow = new TableRow();
            newRow.Height = 50;
            newCell = new TableCell();
            newRow.Cells.Add(newCell);
            //fill in total Ecluding VAT
            newCell = new TableCell();
            newCell.Text = "Ecluding VAT: ";
            newRow.Cells.Add(newCell);
            newCell = new TableCell();
            newCell.Text = "R" + vatInfo.Item1.ToString();
            newRow.Cells.Add(newCell);
            invoice.Rows.Add(newRow);

            //get the vat rate
            double VATRate = -1;
            try
            {
                VATRate = (Handler.GetVATRate().VATRate / 100) + 1;
            }
            catch (ApplicationException Err)
            {
                logAnError(Err.ToString());
            }

            newRow = new TableRow();
            newRow.Height = 50;
            newCell = new TableCell();
            newRow.Cells.Add(newCell);
            //fill in total VAT due
            newCell = new TableCell();

            newCell.Text = "VAT @ "+ VATRate+"%";
            newRow.Cells.Add(newCell);
            newCell = new TableCell();
            newCell.Text = "R" + vatInfo.Item1.ToString();
            newRow.Cells.Add(newCell);
            invoice.Rows.Add(newRow);

            //display the total due
            newRow = new TableRow();
            newRow.Height = 50;
            newCell = new TableCell();
            newRow.Cells.Add(newCell);
            //fill in total Ecluding VAT
            newCell = new TableCell();
            newCell.Text = "Total Due: ";
            newRow.Cells.Add(newCell);
            newCell = new TableCell();
            newCell.Text = "R"+total.ToString();
            newRow.Cells.Add(newCell);
            invoice.Rows.Add(newRow);

            return invoice;
        }

        public string ExportDatatableToHtml(DataTable dt)
        {
            //method taken from https://www.c-sharpcorner.com/UploadFile/deveshomar/export-datatable-to-html-in-C-Sharp/
            //accessed 2018/05/09

            StringBuilder strHTMLBuilder = new StringBuilder();
            strHTMLBuilder.Append("<html >");
            strHTMLBuilder.Append("<head>");
            strHTMLBuilder.Append("</head>");
            strHTMLBuilder.Append("<body>");
            strHTMLBuilder.Append("<table border='1px' cellpadding='1' cellspacing='1' bgcolor='lightyellow' style='font-family:Garamond; font-size:smaller'>");

            strHTMLBuilder.Append("<tr >");
            foreach (DataColumn myColumn in dt.Columns)
            {
                strHTMLBuilder.Append("<td >");
                strHTMLBuilder.Append(myColumn.ColumnName);
                strHTMLBuilder.Append("</td>");

            }
            strHTMLBuilder.Append("</tr>");


            foreach (DataRow myRow in dt.Rows)
            {

                strHTMLBuilder.Append("<tr >");
                foreach (DataColumn myColumn in dt.Columns)
                {
                    strHTMLBuilder.Append("<td >");
                    strHTMLBuilder.Append(myRow[myColumn.ColumnName].ToString());
                    strHTMLBuilder.Append("</td>");

                }
                strHTMLBuilder.Append("</tr>");
            }

            //Close tags.  
            strHTMLBuilder.Append("</table>");
            strHTMLBuilder.Append("</body>");
            strHTMLBuilder.Append("</html>");

            string Htmltext = strHTMLBuilder.ToString();

            return Htmltext;

        }
        */
    }
}