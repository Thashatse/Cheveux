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
                    catch (Exception Err)
                    {
                        function.logAnError(Err.ToString());
                        reportLable.Text = ("An error occurred communicating with the database. Please Try Again Later");
                    }
                }

                if (ddlReportFor.SelectedValue != "-1"
                    && CalendarDateStrart.SelectedDate.ToString() == "0001/01/01 00:00:00"
                    && CalendarDateEnd.SelectedDate.ToString() == "0001/01/01 00:00:00")
                {
                    reportByContainer.Visible = true;
                    reportDateRangeContainer.Visible = true;
                    divReport.Visible = true;
                    //display the sales report
                    getSalesReport(true);
                }
                else if (ddlReportFor.SelectedValue != "-1"
                    && CalendarDateStrart.SelectedDate.ToString() != "0001/01/01 00:00:00"
                    && CalendarDateEnd.SelectedDate.ToString() != "0001/01/01 00:00:00")
                {
                    reportDateRangeContainer.Visible = true;
                    reportByContainer.Visible = true;
                    divReport.Visible = true;
                    //display the sales report
                    getSalesReport(false);
                }

            }
            else if (drpReport.SelectedIndex == 1)
            {
                Response.Redirect("../Receptionist/Appointments.aspx?Action=ViewAllSchedules");
            }
        }

        private void getSalesReport(bool defaultDateRange)
        {
            //clear the table
            tblReport.Rows.Clear();

            reportLable.Text = "Sales Report";
            reportByLable.Text = "For: "+ ddlReportFor.SelectedItem.Text.ToString();
            reportGenerateDateLable.Text = "Generated: "+DateTime.Now.ToString("HH:mm dd MMM yyyy");
            try
            {
                List<SP_SaleOfHairstylist> report = null;
                if (defaultDateRange == true)
                {
                    reportDateRangeLable.Text = new DateTime(DateTime.Now.Year, 1, 1).ToString("dd MMM yyyy") + " - " +
                        DateTime.Today.ToString("dd MMM yyyy");
                    report = handler.getSaleOfHairstylist(ddlReportFor.SelectedValue, new DateTime(DateTime.Now.Year, 1, 1), DateTime.Today) ;
                }
                else if (defaultDateRange == false)
                {
                    reportDateRangeLable.Text = CalendarDateStrart.SelectedDate.ToString("dd MMM yyyy") + " - " +
                        CalendarDateEnd.SelectedDate.ToString("dd MMM yyyy");
                    report = handler.getSaleOfHairstylist(ddlReportFor.SelectedValue, CalendarDateStrart.SelectedDate, CalendarDateEnd.SelectedDate);
                }

                if (drpType.SelectedValue == "0")
                {
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
                            newHeaderCell.Width = 300;
                            tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                            newHeaderCell = new TableHeaderCell();
                            newHeaderCell.Text = "Customer Name";
                            newHeaderCell.Width = 300;
                            tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                            //empty cell
                            newHeaderCell = new TableHeaderCell();
                            newHeaderCell.Width = 300;
                            newHeaderCell.Text = "Total spent";
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

                            #region Total
                            //get the invoice 
                            List<SP_getInvoiceDL> invoice = handler.getInvoiceDL(Sales.BookingID);
                            //calculate total price
                            double total = 0.0;

                            foreach (SP_getInvoiceDL item in invoice)
                            {
                                //increment final price
                                total = item.Qty * item.price;
                            }

                            //fill in total
                            newCell = new TableCell();
                            newCell.Text = "R " + string.Format("{0:#.00}", total).ToString();
                            tblReport.Rows[reportRowCount].Cells.Add(newCell);

                            reportRowCount++;
                            //empty row
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblReport.Rows.Add(newRow);
                            reportRowCount++;
                            #endregion
                        }
                    }
                }
                else if (drpType.SelectedValue == "1")
                {
                    //load the ones with type that is cash
                    //get the invoice dt lines
                    if (report.Count != 0)
                    {
                        //counter to keep track of rows in report
                        int reportRowCount = 0;

                        //display each record
                        foreach (SP_SaleOfHairstylist Sales in report)
                        {
                            if (Sales.PaymentType.ToString() == drpType.SelectedItem.Text)
                            {
                                // create a new row in the results table and set the height
                                TableRow newRow = new TableRow();
                                newRow.Height = 50;
                                tblReport.Rows.Add(newRow);
                                //set the report headers
                                TableHeaderCell newHeaderCell = new TableHeaderCell();
                                newHeaderCell.Text = "Date";
                                newHeaderCell.Width = 300;
                                tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                                newHeaderCell = new TableHeaderCell();
                                newHeaderCell.Text = "Customer Name";
                                newHeaderCell.Width = 300;
                                tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                                //empty cell
                                newHeaderCell = new TableHeaderCell();
                                newHeaderCell.Width = 300;
                                newHeaderCell.Text = "Total spent";
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

                                #region Total
                                //get the invoice 
                                List<SP_getInvoiceDL> invoice = handler.getInvoiceDL(Sales.BookingID);
                                //calculate total price
                                double total = 0.0;

                                foreach (SP_getInvoiceDL item in invoice)
                                {
                                    //increment final price
                                    total = item.Qty * item.price;
                                }

                                //fill in total
                                newCell = new TableCell();
                                newCell.Text = "R " + string.Format("{0:#.00}", total).ToString();
                                tblReport.Rows[reportRowCount].Cells.Add(newCell);

                                reportRowCount++;
                                //empty row
                                newRow = new TableRow();
                                newRow.Height = 50;
                                tblReport.Rows.Add(newRow);
                                reportRowCount++;
                                #endregion
                            }
                        }
                    }
                }
                else if (drpType.SelectedValue == "2")
                {
                    //load the ones with type that is credit
                    if (report.Count != 0)
                    {
                        //counter to keep track of rows in report
                        int reportRowCount = 0;

                        //display each record
                        foreach (SP_SaleOfHairstylist Sales in report)
                        {
                            if (Sales.PaymentType == drpType.SelectedItem.Text)
                            {
                                // create a new row in the results table and set the height
                                TableRow newRow = new TableRow();
                                newRow.Height = 50;
                                tblReport.Rows.Add(newRow);
                                //set the report headers
                                TableHeaderCell newHeaderCell = new TableHeaderCell();
                                newHeaderCell.Text = "Date";
                                newHeaderCell.Width = 300;
                                tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                                newHeaderCell = new TableHeaderCell();
                                newHeaderCell.Text = "Customer Name";
                                newHeaderCell.Width = 300;
                                tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                                //empty cell
                                newHeaderCell = new TableHeaderCell();
                                newHeaderCell.Width = 300;
                                newHeaderCell.Text = "Total spent";
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

                                #region Total
                                //get the invoice 
                                List<SP_getInvoiceDL> invoice = handler.getInvoiceDL(Sales.BookingID);
                                //calculate total price
                                double total = 0.0;

                                foreach (SP_getInvoiceDL item in invoice)
                                {
                                    //increment final price
                                    total = item.Qty * item.price;
                                }

                                //fill in total
                                newCell = new TableCell();
                                newCell.Text = "R " + string.Format("{0:#.00}", total).ToString();
                                tblReport.Rows[reportRowCount].Cells.Add(newCell);

                                reportRowCount++;
                                //empty row
                                newRow = new TableRow();
                                newRow.Height = 50;
                                tblReport.Rows.Add(newRow);
                                reportRowCount++;
                                #endregion
                            }
                        }
                    }
                }
            }
            catch (Exception Err)
            {
                function.logAnError("Error getting Sales Report " + Err.ToString());
                divReport.Visible = false;
                lError.Visible = true;
                lError.Text = "An error occurred generating the report, Try Again Later";
            }
        }

        private void getBookingForHairstylistDateRange(bool defaultDateRange)
        {
            //clear the table
            tblReport.Rows.Clear();

            //set the headers
            reportLable.Text = "BookingsReport";
            reportByLable.Text = "For: " + ddlReportFor.SelectedItem.Text.ToString();
            reportGenerateDateLable.Text = "Generated: " + DateTime.Now.ToString("HH:mm dd MMM yyyy");
            try
            {
                List<SP_BookingsReportForHairstylist> report = null;
                if (defaultDateRange == true)
                {
                    reportDateRangeLable.Text = new DateTime(DateTime.Now.Year, 1, 1).ToString("dd MMM yyyy") + " - " +
                        DateTime.Today.ToString("dd MMM yyyy");
                    //get the report data and pass the corect variables
                    report = handler.getBookingReportForHairstylistWithDateRange(
                        ddlReportFor.SelectedValue.ToString(), new DateTime(DateTime.Now.Year, 1, 1), DateTime.Today);
                }else if (defaultDateRange == false)
                {
                    reportDateRangeLable.Text = CalendarDateStrart.SelectedDate.ToString("dd MMM yyyy") + " - " + 
                        CalendarDateEnd.SelectedDate.ToString("dd MMM yyyy");
                    //get the report data and pass the corect variables
                    report = handler.getBookingReportForHairstylistWithDateRange(
                        ddlReportFor.SelectedValue.ToString(), CalendarDateStrart.SelectedDate, CalendarDateEnd.SelectedDate);
                }

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
                    newHeaderCell.Text = "Time";
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
            catch (Exception Err)
            {
                function.logAnError("Error getting Booking for hairstylist Report Date Range " + Err.ToString());
                divReport.Visible = false;
                lError.Visible = true;
                lError.Text = "An error occurred generating the report, Try Again Later";
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            drpReport_SelectedIndexChanged1(sender, e);
        }
    }
}