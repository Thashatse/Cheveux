using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;

namespace Cheveux.Manager
{
    public partial class Reports : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is loged out
            cookie = Request.Cookies["CheveuxUserID"];

            if (cookie == null)
            {
                //if the user is not loged in as a manager do not display Bussines setting
            }
            else if (cookie["UT"] != "M")
            {
                Response.Redirect("../Default.aspx");
            }
            else if (cookie["UT"] == "M")
            {
                //if the user is loged in as a manager display Bussines setting
                drpReport_SelectedIndexChanged1(sender, e);
                LogedIn.Visible = true;
                LogedOut.Visible = false;
            }
        }

        protected void drpReport_SelectedIndexChanged1(object sender, EventArgs e)
        {
            lError.Visible = false;
            if (drpReport.SelectedIndex == 0)
            {
                reportByContainer.Visible = false;
                reportDateRangeContainer.Visible = false;
                divReport.Visible = false;
            }
            else if (drpReport.SelectedIndex == 1)
            {
                reportByContainer.Visible = true;
                if (ddlReportFor.SelectedIndex == -1)
                {
                    try
                    {
                        List<SP_GetEmpNames> list = handler.BLL_GetEmpNames();
                        foreach (SP_GetEmpNames emps in list)
                        {
                            //Load employee names into dropdownlist
                            ddlReportFor.DataSource = list;
                            //set the coloumn that will be displayed to the user
                            ddlReportFor.DataTextField = "Name";
                            //set the coloumn that will be used for the valuefield
                            ddlReportFor.DataValueField = "EmployeeID";
                            //bind the data
                            ddlReportFor.DataBind();
                            /*set the default (text (dropdownlist index[0]) that will first be displayed to the user.
                             * in this case the "instruction" to select the employee
                            */
                            ddlReportFor.Items.Insert(0, new ListItem("Select Stylist", "-1"));
                        }
                    }
                    catch (ApplicationException Err)
                    {
                        function.logAnError(Err.ToString());
                        reportLable.Text = ("An error occurred communicating with the database. Please Try Again Later");
                    }
                }

                if (ddlReportFor.SelectedValue != "-1")
                {
                    reportByContainer.Visible = false;
                    reportDateRangeContainer.Visible = false;
                    divReport.Visible = true;
                    //display the sales report
                    getSalesReport();
                }
                
            }
            else if (drpReport.SelectedIndex == 2)
            {
                reportByContainer.Visible = true;
                if (ddlReportFor.SelectedIndex == -1)
                {
                    try
                    {
                        List<SP_GetEmpNames> list = handler.BLL_GetEmpNames();
                        foreach (SP_GetEmpNames emps in list)
                        {
                            //Load employee names into dropdownlist
                            ddlReportFor.DataSource = list;
                            //set the coloumn that will be displayed to the user
                            ddlReportFor.DataTextField = "Name";
                            //set the coloumn that will be used for the valuefield
                            ddlReportFor.DataValueField = "EmployeeID";
                            //bind the data
                            ddlReportFor.DataBind();
                            /*set the default (text (dropdownlist index[0]) that will first be displayed to the user.
                             * in this case the "instruction" to select the employee
                            */
                            ddlReportFor.Items.Insert(0, new ListItem("Select Stylist", "-1"));
                        }
                    }
                    catch (ApplicationException Err)
                    {
                        function.logAnError(Err.ToString());
                        reportLable.Text = ("An error occurred communicating with the database. Please Try Again Later");
                    }
                }

                if (ddlReportFor.SelectedValue != "-1")
                    {
                        reportDateRangeContainer.Visible = false;
                        divReport.Visible = true;
                        //display the sales report
                        getBookingForHairstylist();
                    }
                
            }
            else if (drpReport.SelectedIndex == 3)
            {
                reportByContainer.Visible = true;
                if (ddlReportFor.SelectedIndex == -1)
                {
                    try
                    {
                        List<SP_GetEmpNames> list = handler.BLL_GetEmpNames();
                        foreach (SP_GetEmpNames emps in list)
                        {
                            //Load employee names into dropdownlist
                            ddlReportFor.DataSource = list;
                            //set the coloumn that will be displayed to the user
                            ddlReportFor.DataTextField = "Name";
                            //set the coloumn that will be used for the valuefield
                            ddlReportFor.DataValueField = "EmployeeID";
                            //bind the data
                            ddlReportFor.DataBind();
                            /*set the default (text (dropdownlist index[0]) that will first be displayed to the user.
                             * in this case the "instruction" to select the employee
                            */
                            ddlReportFor.Items.Insert(0, new ListItem("Select Stylist", "-1"));
                        }
                    }
                    catch (ApplicationException Err)
                    {
                        function.logAnError(Err.ToString());
                        reportLable.Text = ("An error occurred communicating with the database. Please Try Again Later");
                    }
                }

                if (ddlReportFor.SelectedValue != "-1")
                {
                    reportDateRangeContainer.Visible = true;
                }

                if (ddlReportFor.SelectedValue != "-1" 
                    && CalendarDateStrart.SelectedDate.ToString() != "0001/01/01 00:00:00" 
                    && CalendarDateEnd.SelectedDate.ToString() != "0001/01/01 00:00:00")
                {
                    divReport.Visible = true;
                    //display the sales report
                    getBookingForHairstylistDateRange();
                }

            }
        }

        private void getSalesReport()
        {
            reportLable.Text = "Sales";
            reportByLable.Text = ddlReportFor.SelectedItem.Text.ToString();
            reportDateRangeLable.Text = "All Time";
            reportGenerateDateLable.Text = DateTime.Now.ToString("HH:mm dd MMM yyyy");
            try
            {
                List<SP_SaleOfHairstylist> report = handler.getSaleOfHairstylist(ddlReportFor.SelectedValue);
                //get the invoice dt lines
                if (report.Count != 0)
                {
                    //counter to keep track of rows in report
                    int reportRowCount = 0;
                    

                    //display each record
                    foreach (SP_SaleOfHairstylist Sales in report)
                    {
                        // create a new row in the results table and set the height
                        TableRow newRow = new TableRow();
                        newRow.Height = 50;
                        tblReport.Rows.Add(newRow);
                        //set the report headers
                        TableHeaderCell newHeaderCell = new TableHeaderCell();
                        newHeaderCell.Text = "Date";
                        newHeaderCell.Width = 100;
                        tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                        newHeaderCell = new TableHeaderCell();
                        newHeaderCell.Text = "Customer Name";
                        newHeaderCell.Width = 100;
                        tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                        //empty cell
                        newHeaderCell = new TableHeaderCell();
                        newHeaderCell.Width = 100;
                        tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                        reportRowCount++;

                        // create a new row in the results table and set the height
                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblReport.Rows.Add(newRow);
                        //fill the row with the data from the product results object
                        TableCell newCell = new TableCell();
                        newCell.Text = Sales.date.ToString("dd MMM yyy");
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = Sales.FullName.ToString();
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        //empty cell
                        newCell = new TableCell();
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        reportRowCount++;

                        #region Invoice
                        //diplay invoice
                        // create a new row in the results table and set the height
                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblReport.Rows.Add(newRow);
                        //get the invoice 
                        List<SP_getInvoiceDL> invoice = handler.getInvoiceDL(Sales.BookingID);
                        //set the Invoice header
                        newHeaderCell = new TableHeaderCell();
                        newHeaderCell.Text = "Invoice:";
                        tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                        //empty cell
                        newHeaderCell = new TableHeaderCell();
                        tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                        //empty cell
                        newHeaderCell = new TableHeaderCell();
                        tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                        reportRowCount++;
                        //calculate total price
                        double total = 0.0;

                        foreach (SP_getInvoiceDL item in invoice)
                        {
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblReport.Rows.Add(newRow);
                            //empty cell
                            newCell = new TableCell();
                            newRow.Cells.Add(newCell);
                            //fill in the item
                            newCell = new TableCell();
                            newCell.Text = item.Qty.ToString() + " " + item.itemName.ToString() + " @ R" + string.Format("{0:#.00}", item.price);
                            newRow.Cells.Add(newCell);
                            //fill in the Qty, unit price & TotalPrice
                            newCell = new TableCell();
                            newCell.HorizontalAlign = HorizontalAlign.Right;
                            newCell.Text = "R" + string.Format("{0:#.00}", item.price);
                            tblReport.Rows[reportRowCount].Cells.Add(newCell);
                            //increment final price
                            total = item.Qty * item.price;

                            //increment row count 
                            reportRowCount ++;
                        }

                        // get vat info
                        Tuple<double, double> vatInfo = function.getVat(total);

                        //display total including and Excluding VAT
                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblReport.Rows.Add(newRow);
                        //empty cell
                        newCell = new TableCell();
                        newRow.Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = "Total Ecluding VAT: ";
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        //fill in total Ecluding VAT
                        newCell = new TableCell();
                        newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = "R " + string.Format("{0:#.00}", vatInfo.Item1, 2);
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);

                        //increment row count 
                        reportRowCount ++;

                        //get the vat rate
                        double VATRate = -1;
                        try
                        {
                            VATRate = handler.GetVATRate().VATRate;
                        }
                        catch (ApplicationException Err)
                        {
                            function.logAnError(Err.ToString());
                        }

                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblReport.Rows.Add(newRow);
                        //empty cell
                        newCell = new TableCell();
                        newRow.Cells.Add(newCell);
                        //fill in total VAT due
                        newCell = new TableCell();
                        newCell.Text = "VAT @" + VATRate + "%";
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = "R " + string.Format("{0:#.00}", vatInfo.Item2, 2).ToString();
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);

                        //increment row count 
                        reportRowCount++;

                        //display the total due
                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblReport.Rows.Add(newRow);
                        //empty cell
                        newCell = new TableCell();
                        newRow.Cells.Add(newCell);
                        //fill in total
                        newCell = new TableCell();
                        newCell.Text = "Total Due: ";
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = "R " + string.Format("{0:#.00}", total).ToString();
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);

                        reportRowCount ++;
                        //empty row
                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblReport.Rows.Add(newRow);
                        reportRowCount++;
                        #endregion

                    }
                }


            }
            catch (ApplicationException Err)
            {
                function.logAnError("Error getting Sales Report " + Err.ToString());
                divReport.Visible = false;
                lError.Visible = true;
                lError.Text = "An error occurred generating the report, Try Again Later";
            }
        }

        private void getBookingForHairstylist()
        {
            reportLable.Text = "Bookings";
            reportByLable.Text = ddlReportFor.SelectedItem.Text.ToString();
            reportDateRangeLable.Text = "All Time";
            reportGenerateDateLable.Text = DateTime.Now.ToString("HH:mm dd MMM yyyy");
            try
            {
                //get the report data and pass the corect variables
                List <SP_BookingsReportForHairstylist> report = handler.getBookingsReportForHairstylist(ddlReportFor.SelectedValue);
                if(report.Count != 0)
                {
                    //counter to keep track of rows in report
                    int reportRowCount = 0;
                    // create a new row in the results table and set the height
                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblReport.Rows.Add(newRow);
                    //set the report headers
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Date";
                    newHeaderCell.Width = 100;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Start time";
                    newHeaderCell.Width = 100;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "End time";
                    newHeaderCell.Width = 100;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Customer Name";
                    newHeaderCell.Width = 300;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Service name";
                    newHeaderCell.Width = 500;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Arrived ";
                    newHeaderCell.Width = 50;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    reportRowCount++;

                    //display each record
                    foreach (SP_BookingsReportForHairstylist booking in report)
                    {
                        // create a new row in the results table and set the height
                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblReport.Rows.Add(newRow);
                        //fill the row with the data from the product results object
                        TableCell newCell = new TableCell();
                        newCell.Text = booking.Date.ToString("dd MMM yyy");
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = booking.StartTime.ToString("hh:mm");
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = booking.EndTime.ToString("hh:mm");
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = booking.FirstName.ToString() + " " + booking.LastName.ToString();
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = booking.Name.ToString();
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = function.GetFullArrivedStatus(booking.Arrived.ToString()[0]);
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        reportRowCount++;
                    }
                }
                else
                {
                    divReport.Visible = false;
                    lError.Visible = true;
                    lError.Text = "No bookings were found for hairstylist "+ddlReportFor.SelectedItem.Text.ToString();
                }
            }
            catch (ApplicationException Err)
            {
                function.logAnError("Error getting Booking for hairstylist Report "+Err.ToString() );
                divReport.Visible = false;
                lError.Visible = true;
                lError.Text = "An error occurred generating the report, Try Again Later";
            }
        }

        private void getBookingForHairstylistDateRange()
        {
            reportLable.Text = "Bookings";
            reportByLable.Text = ddlReportFor.SelectedItem.Text.ToString();
            //if(CalendarDateRage.SelectedDates.)
            reportDateRangeLable.Text = CalendarDateStrart.SelectedDate.ToString() +" - "+ CalendarDateEnd.SelectedDate.ToString();
            reportGenerateDateLable.Text = DateTime.Now.ToString("HH:mm dd MMM yyyy");
            try
            {
                //get the report data and pass the corect variables
                List<SP_BookingsReportForHairstylist> report = handler.getBookingReportForHairstylistWithDateRange(ddlReportFor.SelectedValue.ToString(), CalendarDateStrart.SelectedDate, CalendarDateEnd.SelectedDate);
                if (report.Count != 0)
                {
                    //counter to keep track of rows in report
                    int reportRowCount = 0;
                    // create a new row in the results table and set the height
                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblReport.Rows.Add(newRow);
                    //set the report headers
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Date";
                    newHeaderCell.Width = 100;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Start time";
                    newHeaderCell.Width = 100;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "End time";
                    newHeaderCell.Width = 100;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Customer Name";
                    newHeaderCell.Width = 300;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Service name";
                    newHeaderCell.Width = 500;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Arrived ";
                    newHeaderCell.Width = 50;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    reportRowCount++;

                    //display each record
                    foreach (SP_BookingsReportForHairstylist booking in report)
                    {
                        // create a new row in the results table and set the height
                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblReport.Rows.Add(newRow);
                        //fill the row with the data from the product results object
                        TableCell newCell = new TableCell();
                        newCell.Text = booking.Date.ToString("dd MMM yyy");
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = booking.StartTime.ToString("hh:mm");
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = booking.EndTime.ToString("hh:mm");
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = booking.FirstName.ToString() + " " + booking.LastName.ToString();
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = booking.Name.ToString();
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = function.GetFullArrivedStatus(booking.Arrived.ToString()[0]);
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        reportRowCount++;
                    }
                }
            }
            catch (ApplicationException Err)
            {
                function.logAnError("Error getting Booking for hairstylist Report Date Range " + Err.ToString());
                divReport.Visible = false;
                lError.Visible = true;
                lError.Text = "An error occurred generating the report, Try Again Later";
            }
        }
    }
}