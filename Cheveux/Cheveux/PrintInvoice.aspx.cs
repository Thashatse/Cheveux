using BLL;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;

namespace Cheveux
{
    public partial class PrintInvoice : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;
        string SaleID = null;
        string autoPrint = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is loged out
            cookie = Request.Cookies["CheveuxUserID"];
            if (cookie == null)
            {
                //if the user is not loged in do not diplay invoice details
                Response.Redirect("Default.aspx");
            }
            else if (cookie != null)
            {
                //checked for the booking ID
                SaleID = Request.QueryString["SaleID"];
                if (SaleID != null)
                {
                    //if loged in load invoice
                    loadInvoice();

                    //open the print dialog box if requested
                    autoPrint = Request.QueryString["AutoPrint"];
                    if (autoPrint != "False" || autoPrint == null)
                    {
                        //print the invoice
                        ClientScript.RegisterStartupScript(typeof(Page), "key", "<script type='text/javascript'>window.print();;</script>");
                    }
                }
            }

            //if the user is loged in and there is no sale id redirect
            if(cookie != null
                && SaleID == null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        private void loadInvoice()
        {
            //display the invoice
            int rowCount = 0;
            //get the details from the db
            try
            {
                //get booking deatils
                SP_GetCustomerBooking BookingDetails = handler.getCustomerPastBookingDetails(SaleID);
                SALE invoice = handler.getSale(SaleID);
                List<SP_getInvoiceDL> invoicDetailLines = handler.getInvoiceDL(SaleID);

                    if (invoicDetailLines.Count != 0)
                    {
                    #region Company Details / Invoice Date
                    //diaplay a heading
                    TableRow newRow = new TableRow();
                        newRow.Height = 50;
                        tblInvoice.Rows.Add(newRow);
                        TableCell newCell = new TableCell();
                        newCell.Font.Bold = true;
                    newCell.Font.Bold = true;
                    newCell.Text = "<a class='navbar-brand js-scroll-trigger' href='#' onClick='window.print()'>Cheveux </a>";
                        tblInvoice.Rows[rowCount].Cells.Add(newCell);
                    #region EmptyCells
                    newCell = new TableCell();
                    newCell.Text = "";
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "";
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);
                    #endregion
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Invoice";
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);

                    #region Break Line
                    newRow = new TableRow();
                    newRow.Height = 50;
                    tblInvoice.Rows.Add(newRow);
                    newCell = new TableCell();
                    newCell.Text = "<br/>";
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "<br/>";
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);

                    //increment row count 
                    rowCount++;
                    #endregion

                    //from address
                    BUSINESS bUSINESS = handler.getBusinessTable();
                    newRow = new TableRow();
                    newRow.Height = 50;
                    tblInvoice.Rows.Add(newRow);
                    newCell = new TableCell();
                    newCell.Text = bUSINESS.AddressLine1 + "<br/>"+bUSINESS.AddressLine2;
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);

                    #region EmptyCells
                    newCell = new TableCell();
                    newCell.Text = "";
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "";
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);
                    #endregion

                    #region  date
                    newCell = new TableCell();

                    newCell.Text = invoice.Date.ToString("HH:mm dd MMM yyyy");
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);
                    #endregion

                    //increment row count 
                    rowCount++;
                    #endregion

                    #region Billed To
                    //Billed to
                    newRow = new TableRow();
                        newRow.Height = 50;
                        tblInvoice.Rows.Add(newRow);
                        newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Billed To:";
                        tblInvoice.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                        newCell.Text = handler.GetUserDetails(invoice.CustID).FirstName.ToString() +" "+
                        handler.GetUserDetails(invoice.CustID).LastName.ToString();
                        tblInvoice.Rows[rowCount].Cells.Add(newCell);

                        //increment row count 
                        rowCount++;
                    #endregion

                    #region Stylist
                    if (BookingDetails != null)
                    {
                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblInvoice.Rows.Add(newRow);
                        newCell = new TableCell();
                        newCell.Font.Bold = true;
                        newCell.Text = "Services Renderd By:";
                        tblInvoice.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = BookingDetails.stylistFirstName;
                        tblInvoice.Rows[rowCount].Cells.Add(newCell);

                        //increment row count 
                        rowCount++;
                    }
                    #endregion
                    
                    //calculate total price
                    double total = 0.0;

                    #region Items
                    newRow = new TableRow();
                    newRow.Height = 50;
                    tblInvoice.Rows.Add(newRow);

                    #region header
                    //fill in the item
                    newCell = new TableCell();
                    newCell.Text = "Item";
                    newCell.Font.Bold = true;
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);

                    newCell = new TableCell();
                    newCell.Text = "Qty";
                    newCell.Font.Bold = true;
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);
                    
                    newCell = new TableCell();
                    newCell.Text = "Unit Price";
                    newCell.Font.Bold = true;
                    newCell.HorizontalAlign = HorizontalAlign.Right;
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);

                    //fill in the Qty, unit price & TotalPrice
                    newCell = new TableCell();
                    newCell.Text = "Total";
                    newCell.Font.Bold = true;
                    newCell.HorizontalAlign = HorizontalAlign.Right;
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);

                    //increment row count 
                    rowCount++;
                    #endregion

                    foreach (SP_getInvoiceDL item in invoicDetailLines)
                        {
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblInvoice.Rows.Add(newRow);

                            //fill in the item
                        newCell = new TableCell();
                        newCell.Text = item.itemName.ToString();
                        tblInvoice.Rows[rowCount].Cells.Add(newCell);

                        newCell = new TableCell();
                        newCell.Text = item.Qty.ToString();
                        tblInvoice.Rows[rowCount].Cells.Add(newCell);

                        newCell = new TableCell();
                        newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = "R" + string.Format("{0:#.00}", item.price);
                        tblInvoice.Rows[rowCount].Cells.Add(newCell);

                        //fill in the Qty, unit price & TotalPrice
                        newCell = new TableCell();
                            newCell.HorizontalAlign = HorizontalAlign.Right;
                            newCell.Text = "R" + string.Format("{0:#.00}", item.Qty * item.price);
                            tblInvoice.Rows[rowCount].Cells.Add(newCell);

                            //increment final price
                            total += item.Qty * item.price;

                            //increment row count 
                            rowCount++;
                        }
                    #endregion

                    #region Excluding Vat Info
                    // get vat info
                    Tuple<double, double> vatInfo = function.getVat(total);

                        //display total including and Excluding VAT
                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblInvoice.Rows.Add(newRow);

                    newCell = new TableCell();
                    newCell.Text = "";
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);

                    newCell = new TableCell();
                    newCell.Text = "";
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);

                    newCell = new TableCell();
                        newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = "<br/> Excluding VAT: &nbsp; ";
                    newCell.Font.Bold = true;
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);
                        //fill in total ExcludingVAT
                        newCell = new TableCell();
                        newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = " <br/> R " + string.Format("{0:#.00}", vatInfo.Item1, 2);
                        tblInvoice.Rows[rowCount].Cells.Add(newCell);

                        //increment row count 
                        rowCount++;
#endregion

                    #region VAT RAte
                    //get the vat rate
                    double VATRate = -1;
                        try
                        {
                            VATRate = handler.GetVATRate().VATRate;
                        }
                        catch (Exception Err)
                        {
                            function.logAnError(Err.ToString());
                        }
                    
                    newRow = new TableRow();
                        newRow.Height = 50;
                        tblInvoice.Rows.Add(newRow);

                    newCell = new TableCell();
                    newCell.Text = "";
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);

                    newCell = new TableCell();
                    newCell.Text = "";
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);

                    //fill in total VAT due
                    newCell = new TableCell();
                        newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = "VAT @" + VATRate + "% &nbsp; ";
                    newCell.Font.Bold = true;
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                    newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = "R " + string.Format("{0:#.00}", vatInfo.Item2, 2).ToString();
                        tblInvoice.Rows[rowCount].Cells.Add(newCell);

                        //increment row count 
                        rowCount++;
                    #endregion

                    #region Total
                    //display the total due
                    newRow = new TableRow();
                        newRow.Height = 50;
                        tblInvoice.Rows.Add(newRow);

                    newCell = new TableCell();
                    newCell.Text = "";
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);

                    newCell = new TableCell();
                    newCell.Text = "";
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);

                    //fill in total
                    newCell = new TableCell();
                        newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = "<br/> Total Due: &nbsp; ";
                    newCell.Font.Bold = true;
                    tblInvoice.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                    newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = "<br/> R " + string.Format("{0:#.00}", total).ToString();
                        tblInvoice.Rows[rowCount].Cells.Add(newCell);
                    #endregion
                }
                else
                {
                    function.logAnError("Error Loading Print Invoice ");
                    Response.Redirect("Error.aspx?Error='An Error Occured Communicating With The Data Base, Try Again Later'");
                }
            }
            catch (Exception Err)
            {
                function.logAnError("Error Loading Print Invoice | Error: "+Err.ToString());
                Response.Redirect("Error.aspx?Error='An Error Occured Communicating With The Data Base, Try Again Later'");
            }

        }
    }
}