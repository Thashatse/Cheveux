using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;

namespace Cheveux
{
    public partial class Appointments : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;
        List<SP_GetEmpAgenda> agenda = null;
        List<SP_GetStylistBookings> bList = null;
        List<SP_GetEmpNames> list = null;

        string today = DateTime.Now.ToString("yyyy-MM-dd");
        protected void Page_Load(object sender, EventArgs e)
        {
            errorCssStyles();

            cookie = Request.Cookies["CheveuxUserID"];

            if (cookie == null)
            {
                phLogin.Visible = true;
                phMain.Visible = false;
            }
            else if (cookie["UT"] != "R")
            {
                Response.Redirect("../Default.aspx");
            }
            else if (cookie["UT"] == "R")
            {
                phLogin.Visible = false;
                phMain.Visible = true;
                if (!IsPostBack)
                {
                    //load stylists names into 'names' dropdownlist
                    try
                    {
                        list = handler.BLL_GetEmpNames();
                        foreach (SP_GetEmpNames emps in list)
                        {
                            drpStylistNames.DataSource = list;
                            drpStylistNames.DataTextField = "Name";
                            drpStylistNames.DataValueField = "EmployeeID";
                            drpStylistNames.DataBind();
                        }
                    }
                    catch (ApplicationException Err)
                    {
                        drpStylistNames.Text = "Cannot retrieve names";
                        phBookingsErr.Visible = true;
                        errorHeader.Text = "Error retrieving employee names";
                        errorMessage.Text = "It seems there is a problem retrieving data from the database"
                                            + "Please report problem or try again later.";
                        function.logAnError(Err.ToString());
                    }
                }
                DropDownChanges();
            }
        }

        public void errorCssStyles()
        {
            errorHeader.Font.Bold = true;
            errorHeader.Font.Underline = true;
            errorHeader.Font.Size = 21;
            errorMessage.Font.Size = 14;
        }
        public void DropDownChanges()
        {
            if (drpViewAppt.SelectedValue == "0")
            {
                //upcoming
                phNames.Visible = false;
                phCalendars.Visible = false;

                if (empSelectionType.SelectedValue == "0")
                {
                    phNames.Visible = false;
                    phCalendars.Visible = true;
                    phDay.Visible = false;
                    phDateRange.Visible = false;

                    if (rdoDate.SelectedValue == "0")
                    {
                        //all
                        phBookingsErr.Visible = false;
                        phDateRange.Visible = false;
                        phDay.Visible = false;
                        getAllStylistsUpcomingBookings(drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "1")
                    {
                        //today
                        phBookingsErr.Visible = false;
                        phDay.Visible = false;
                        phDateRange.Visible = false;
                        getAllStylistsUpcomingBksForDate(DateTime.Parse(today), drpSortBy.SelectedItem.Text, 
                                                        drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "2")
                    {
                        //specific day
                        phDay.Visible = true;
                        phDateRange.Visible = false;

                        if(lblDay.Text=="Label1")
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a date.";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getAllStylistsUpcomingBksForDate(calDay.SelectedDate, drpSortBy.SelectedItem.Text, 
                                                            drpSortDir.SelectedItem.Text);
                        }
                    }
                    else if (rdoDate.SelectedValue == "3")
                    {
                        //date range
                        phDay.Visible = false;
                        phDateRange.Visible = true;

                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getAllStylistsUpcomingBksDR(calStart.SelectedDate, calEnd.SelectedDate,
                                                    drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                        
                    }
                }
                else if (empSelectionType.SelectedValue == "1")
                {
                    
                    //stylist
                    phNames.Visible = true;
                    phCalendars.Visible = false;
                    if (drpStylistNames.SelectedValue != "-1")
                    {
                        //name is chosen
                        phCalendars.Visible = true;
                        if (rdoDate.SelectedValue == "0")
                        {
                            //all
                            phBookingsErr.Visible = false;
                            phDateRange.Visible = false;
                            phDay.Visible = false;
                            getStylistUpcomingBookings(drpStylistNames.SelectedValue, drpSortBy.SelectedItem.Text, 
                                                        drpSortDir.SelectedItem.Text);
                        }
                        else if (rdoDate.SelectedValue == "1")
                        {
                            //today
                            phBookingsErr.Visible = false;
                            phDateRange.Visible = false;
                            phDay.Visible = false;
                            getStylistUpcomingBksForDate(drpStylistNames.SelectedValue, DateTime.Parse(today), 
                                                        drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                        else if (rdoDate.SelectedValue == "2")
                        {
                            //specific day 
                            phDay.Visible = true;
                            phDateRange.Visible = false;

                            if (lblDay.Text == "Label1")
                            {
                                phBookingsErr.Visible = true;
                                lblBookingsErr.Text = "Please select a date.";
                            }
                            else
                            {
                                phBookingsErr.Visible = false;
                                getStylistUpcomingBksForDate(drpStylistNames.SelectedValue, calDay.SelectedDate, drpSortBy.SelectedItem.Text, 
                                                                drpSortDir.SelectedItem.Text);
                            }

                        }
                        else if (rdoDate.SelectedValue == "3")
                        {
                            //date range
                            phDay.Visible = false;
                            phDateRange.Visible = true;

                            if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                            {
                                phBookingsErr.Visible = true;
                                lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                    + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
                            }
                            else
                            {
                                phBookingsErr.Visible = false;
                                getStylistUpcomingBookingsDR(drpStylistNames.SelectedValue, calStart.SelectedDate, calEnd.SelectedDate, 
                                                            drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                            }
                        }
                    }
                }
            }
            else if (drpViewAppt.SelectedValue == "1")
            {
                //Past
                phNames.Visible = false;
                phCalendars.Visible = false;
                if (empSelectionType.SelectedValue == "0")
                {
                    //all
                    phNames.Visible = false;
                    phCalendars.Visible = true;
                    phDay.Visible = false;
                    phDateRange.Visible = false;

                    if (rdoDate.SelectedValue == "0")
                    {
                        //all
                        phBookingsErr.Visible = false;
                        phDateRange.Visible = false;
                        phDay.Visible = false;

                        getAllStylistsPastBookings(drpSortBy.SelectedItem.Text,drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "1")
                    {
                        //today
                        phBookingsErr.Visible = false;
                        phDay.Visible = false;
                        phDateRange.Visible = false;

                        getAllStylistsPastBksForDate(DateTime.Parse(today)
                                                    ,drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "2")
                    {
                        //Specific Day
                        phDay.Visible = true;
                        phDateRange.Visible=false;

                        if (lblDay.Text == "Label1")
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a date.";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getAllStylistsPastBksForDate(calDay.SelectedDate, drpSortBy.SelectedItem.Text,
                                                        drpSortDir.SelectedItem.Text);
                        }

                    }
                    else if(rdoDate.SelectedValue == "3")
                    {
                        //Date Range
                        phDay.Visible = false;
                        phDateRange.Visible = true;

                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getAllStylistsPastBookingsDateRange(calStart.SelectedDate, calEnd.SelectedDate
                                                        , drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }

                    }

                }
                else if (empSelectionType.SelectedValue == "1")
                {
                    //stylist
                    phNames.Visible = true;
                    phCalendars.Visible = false;
                    if (drpStylistNames.SelectedValue != "-1")
                    {
                        //name is chosen
                        phCalendars.Visible = true;
                        if (rdoDate.SelectedValue == "0")
                        {
                            //today
                            phBookingsErr.Visible = false;
                            phDateRange.Visible = false;
                            phDay.Visible = false;

                            getStylistPastBookings(drpStylistNames.SelectedValue, drpSortBy.SelectedItem.Text, 
                                                                drpSortDir.SelectedItem.Text);
                        }
                        else if (rdoDate.SelectedValue == "1")
                        {
                            //today
                            phBookingsErr.Visible = false;
                            phDay.Visible = false;
                            phDateRange.Visible = false;

                            getStylistPastBksForDate(drpStylistNames.SelectedValue, DateTime.Parse(today)
                                                    ,drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                        else if (rdoDate.SelectedValue == "2")
                        {
                            //Specific day
                            phDay.Visible = true;
                            phDateRange.Visible = false;

                            if (lblDay.Text == "Label1")
                            {
                                phBookingsErr.Visible = true;
                                lblBookingsErr.Text = "Please select a date.";
                            }
                            else
                            {
                                phBookingsErr.Visible = false;
                                getStylistPastBksForDate(drpStylistNames.SelectedValue, calDay.SelectedDate,
                                                        drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                            } 
                        }
                        else if(rdoDate.SelectedValue == "3")
                        {
                            //date range
                            phDay.Visible = false;
                            phDateRange.Visible = true;

                            if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                            {
                                phBookingsErr.Visible = true;
                                lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                    + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
                            }
                            else
                            {
                                phBookingsErr.Visible = false;
                                getStylistPastBookingsDateRange(drpStylistNames.SelectedValue, calStart.SelectedDate, calEnd.SelectedDate
                                                            ,drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                            }
                        }
                    }
                }
            }
        }

        #region Stylists bookings

        #region Past
        public void getStylistPastBookings(string empID, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            try
            {
                bList = handler.getStylistPastBookings(empID,sortBy,sortDir);

                ////phTable.Visible=true;
                tblSchedule.CssClass = "table table-light table-hover table-bordered";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 240;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 240;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 240;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 200;
                tblSchedule.Rows[0].Cells.Add(empty);

                int rowCount = 1;
                foreach (SP_GetStylistBookings b in bList)
                {
                    TableRow r = new TableRow();
                    r.Height = 50;
                    tblSchedule.Rows.Add(r);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                    tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                    TableCell timeCell = new TableCell();
                    timeCell.Text = b.StartTime.ToString("HH:mm");
                    tblSchedule.Rows[rowCount].Cells.Add(timeCell);

                    TableCell customerCell = new TableCell();
                    customerCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.CustomerID.ToString().Replace(" ", string.Empty) +
                                    "'>" + b.FullName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(customerCell);

                    TableCell servNameCell = new TableCell();
                    servNameCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                + b.ServiceName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past" +
                    "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);
                    rowCount++;
                }
            }
            catch (ApplicationException Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting past bookings for stylist.";
                errorMessage.Text = "It seems there is a problem communicating with the database."
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        public void getStylistPastBookingsDateRange(string empID, DateTime startDate, DateTime endDate, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            try
            {
                bList = handler.getStylistPastBookingsDateRange(empID, startDate, endDate,sortBy,sortDir);

                ////phTable.Visible=true;
                tblSchedule.CssClass = "table table-light table-hover table-bordered";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 240;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 240;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 240;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 200;
                tblSchedule.Rows[0].Cells.Add(empty);

                int rowCount = 1;
                foreach (SP_GetStylistBookings b in bList)
                {
                    TableRow r = new TableRow();
                    r.Height = 50;
                    tblSchedule.Rows.Add(r);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                    tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                    TableCell timeCell = new TableCell();
                    timeCell.Text = b.StartTime.ToString("HH:mm");
                    tblSchedule.Rows[rowCount].Cells.Add(timeCell);

                    TableCell customerCell = new TableCell();
                    customerCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.CustomerID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.FullName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(customerCell);

                    TableCell servNameCell = new TableCell();
                    servNameCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                                        + b.ServiceName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past" +
                    "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);
                    rowCount++;
                }
            }
            catch (ApplicationException Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting past bookings within required date range for stylist.";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        public void getStylistPastBksForDate(string empID, DateTime day, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            try
            {
                bList = handler.getStylistPastBksForDate(empID, day,sortBy,sortDir);

                ////phTable.Visible=true;
                tblSchedule.CssClass = "table table-light table-hover table-bordered";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 240;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 240;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 240;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 200;
                tblSchedule.Rows[0].Cells.Add(empty);

                int rowCount = 1;
                foreach (SP_GetStylistBookings b in bList)
                {
                    TableRow r = new TableRow();
                    r.Height = 50;
                    tblSchedule.Rows.Add(r);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                    tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                    TableCell timeCell = new TableCell();
                    timeCell.Text = b.StartTime.ToString("HH:mm");
                    tblSchedule.Rows[rowCount].Cells.Add(timeCell);

                    TableCell customerCell = new TableCell();
                    customerCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.CustomerID.ToString().Replace(" ", string.Empty) +
                                    "'>" + b.FullName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(customerCell);

                    TableCell servNameCell = new TableCell();
                    servNameCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                + b.ServiceName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past" +
                    "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);

                    rowCount++;
                }
            }
            catch (ApplicationException Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting past bookings for stylist for required day.";
                errorMessage.Text = "It seems there is a problem communicating with the database."
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        #endregion

        #region Upcoming
        public void getStylistUpcomingBookings(string empID, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            try
            {
                ////phTable.Visible=true;

                bList = handler.getStylistUpcomingBookings(empID,sortBy,sortDir);

                tblSchedule.Visible = true;
                tblSchedule.CssClass = "table table-light table-hover table-bordered";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 240;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 240;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 240;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 200;
                tblSchedule.Rows[0].Cells.Add(empty);

                int rowCount = 1;
                foreach (SP_GetStylistBookings b in bList)
                {
                    TableRow r = new TableRow();
                    r.Height = 50;
                    tblSchedule.Rows.Add(r);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                    tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                    TableCell timeCell = new TableCell();
                    timeCell.Text = b.StartTime.ToString("HH:mm");
                    tblSchedule.Rows[rowCount].Cells.Add(timeCell);

                    TableCell customerCell = new TableCell();
                    customerCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.CustomerID.ToString().Replace(" ", string.Empty) +
                                    "'>" + b.FullName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(customerCell);

                    TableCell servNameCell = new TableCell();
                    servNameCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                                        + b.ServiceName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past" +
                    "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);
                    rowCount++;

                    rowCount++;
                }
            }
            catch (ApplicationException Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting upcoming bookings for stylist.";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        public void getStylistUpcomingBksForDate(string id, DateTime bookingDate, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            try
            {
                agenda = handler.BLL_GetEmpAgenda(id, bookingDate,sortBy,sortDir);

                tblSchedule.CssClass = "table table-light table-hover table-bordered";

                //create row for the table 
                TableRow row = new TableRow();
                row.Height = 50;

                //add row to the table
                tblSchedule.Rows.Add(row);

                /* 
                 * create the cells for the row
                 * and their names
                 * the cells being created are for the first row of the table
                 * and their names are the column names
                 * Each cell is added to the table row
                 * .Rows[0] => refers to the first row of the table
                 * */
                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 240;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 240;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 240;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 200;
                tblSchedule.Rows[0].Cells.Add(empty);

                //integer that will be incremented in the foreach loop to access the new row for every iteration of the foreach
                int rowCount = 1;
                foreach (SP_GetEmpAgenda b in agenda)
                {

                    TableRow r = new TableRow();
                    r.Height = 50;
                    tblSchedule.Rows.Add(r);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = b.Date.ToString("dd-MM-yyyy");
                    tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                    TableCell timeCell = new TableCell();
                    timeCell.Text = b.StartTime.ToString("HH:mm");
                    tblSchedule.Rows[rowCount].Cells.Add(timeCell);

                    TableCell customerCell = new TableCell();
                    customerCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.UserID.ToString().Replace(" ", string.Empty) +
                                    "'>" + b.CustomerFName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(customerCell);

                    TableCell servNameCell = new TableCell();
                    servNameCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ProductID.Replace(" ", string.Empty) + "'>"
                                        + b.ServiceName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past" +
                    "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);

                    rowCount++;
                }
            }
            catch (ApplicationException E)
            {
                //Response.Write("<script>alert('Trouble communicating with the database.Report to admin and try again later.');location.reload();</script>");
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting stylists upcoming bookings for day.";
                errorMessage.Text = "It seems there is a problem communicating with the database."
                                    + "Please report problem to admin or try again later.";
                function.logAnError(E.ToString());
            }
        }
        public void getStylistUpcomingBookingsDR(string empID, DateTime startDate, DateTime endDate, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            try
            {
                bList = handler.getStylistUpcomingBookingsDR(empID, startDate, endDate,sortBy,sortDir);

                ////phTable.Visible=true;
                tblSchedule.CssClass = "table table-light table-hover table-bordered";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 240;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 240;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 240;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);


                TableCell empty = new TableCell();
                empty.Width = 200;
                tblSchedule.Rows[0].Cells.Add(empty);

                int rowCount = 1;
                foreach (SP_GetStylistBookings b in bList)
                {
                    TableRow r = new TableRow();
                    r.Height = 50;
                    tblSchedule.Rows.Add(r);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                    tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                    TableCell timeCell = new TableCell();
                    timeCell.Text = b.StartTime.ToString("HH:mm");
                    tblSchedule.Rows[rowCount].Cells.Add(timeCell);

                    TableCell customerCell = new TableCell();
                    customerCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.CustomerID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.FullName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(customerCell);

                    TableCell servNameCell = new TableCell();
                    servNameCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                                        + b.ServiceName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past" +
                    "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);
                    rowCount++;
                }
            }
            catch (ApplicationException Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting upcoming bookings for stylist for required date range.";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        #endregion

        #endregion

        #region All Stylists 

        #region Upcoming
        public void getAllStylistsUpcomingBksForDate(DateTime bookingDate, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            try
            {
                bList = handler.getAllStylistsUpcomingBksForDate(bookingDate,sortBy,sortDir);

                ////phTable.Visible=true;
                tblSchedule.CssClass = "table table-light table-hover table-bordered";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 240;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell emp = new TableCell();
                emp.Text = "Employee";
                emp.Width = 50;
                emp.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(emp);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 240;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 240;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 200;
                tblSchedule.Rows[0].Cells.Add(empty);

                int rowCount = 1;
                foreach (SP_GetStylistBookings b in bList)
                {
                    TableRow r = new TableRow();
                    r.Height = 50;
                    tblSchedule.Rows.Add(r);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                    tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                    TableCell timeCell = new TableCell();
                    timeCell.Text = b.StartTime.ToString("HH:mm");
                    tblSchedule.Rows[rowCount].Cells.Add(timeCell);

                    TableCell empCell = new TableCell();
                    empCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.StylistID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.StylistName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(empCell);

                    TableCell customerCell = new TableCell();
                    customerCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.CustomerID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.FullName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(customerCell);

                    TableCell servNameCell = new TableCell();
                    servNameCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                                        + b.ServiceName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past" +
                    "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);

                    rowCount++;
                }
            }
            catch (ApplicationException Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting upcoming bookings for required day.";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        public void getAllStylistsUpcomingBksDR(DateTime startDate, DateTime endDate, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            try
            {
                bList = handler.getAllStylistsUpcomingBksDR(startDate, endDate, sortBy, sortDir);

                ////phTable.Visible=true;
                tblSchedule.CssClass = "table table-light table-hover table-bordered";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 240;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell emp = new TableCell();
                emp.Text = "Employee";
                emp.Width = 50;
                emp.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(emp);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 240;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 240;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 200;
                tblSchedule.Rows[0].Cells.Add(empty);

                int rowCount = 1;
                foreach (SP_GetStylistBookings b in bList)
                {
                    TableRow r = new TableRow();
                    r.Height = 50;
                    tblSchedule.Rows.Add(r);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                    tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                    TableCell timeCell = new TableCell();
                    timeCell.Text = b.StartTime.ToString("HH:mm");
                    tblSchedule.Rows[rowCount].Cells.Add(timeCell);

                    TableCell empCell = new TableCell();
                    empCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.StylistID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.StylistName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(empCell);

                    TableCell customerCell = new TableCell();
                    customerCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.CustomerID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.FullName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(customerCell);

                    TableCell servNameCell = new TableCell();
                    servNameCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                                        + b.ServiceName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past" +
                    "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);
                    rowCount++;
                }
            }
            catch (ApplicationException Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting all stylists upcoming bookings for date range.";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        public void getAllStylistsUpcomingBookings(string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            try
            {
                bList = handler.getAllStylistsUpcomingBookings(sortBy, sortDir);

                ////phTable.Visible=true;
                tblSchedule.CssClass = "table table-light table-hover table-bordered";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 240;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell emp = new TableCell();
                emp.Text = "Employee";
                emp.Width = 50;
                emp.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(emp);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 240;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 240;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 200;
                tblSchedule.Rows[0].Cells.Add(empty);

                int rowCount = 1;
                foreach (SP_GetStylistBookings b in bList)
                {
                    TableRow r = new TableRow();
                    r.Height = 50;
                    tblSchedule.Rows.Add(r);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                    tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                    TableCell timeCell = new TableCell();
                    timeCell.Text = b.StartTime.ToString("HH:mm");
                    tblSchedule.Rows[rowCount].Cells.Add(timeCell);

                    TableCell empCell = new TableCell();
                    empCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.StylistID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.StylistName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(empCell);

                    TableCell customerCell = new TableCell();
                    customerCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.CustomerID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.FullName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(customerCell);

                    TableCell servNameCell = new TableCell();
                    servNameCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                                        + b.ServiceName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past" +
                    "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);

                    rowCount++;
                }
            }
            catch (ApplicationException Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting all stylists upcoming bookings.";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        #endregion

        #region Past
        public void getAllStylistsPastBookings(string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            try
            {
                bList = handler.getAllStylistsPastBookings(sortBy,sortDir);

                //phTable.Visible=true;
                tblSchedule.CssClass = "table table-light table-hover table-bordered";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 240;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell emp = new TableCell();
                emp.Text = "Employee";
                emp.Width = 50;
                emp.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(emp);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 240;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 240;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 200;
                tblSchedule.Rows[0].Cells.Add(empty);

                int rowCount = 1;
                foreach (SP_GetStylistBookings b in bList)
                {
                    TableRow r = new TableRow();
                    r.Height = 50;
                    tblSchedule.Rows.Add(r);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                    tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                    TableCell timeCell = new TableCell();
                    timeCell.Text = b.StartTime.ToString("HH:mm");
                    tblSchedule.Rows[rowCount].Cells.Add(timeCell);

                    TableCell empCell = new TableCell();
                    empCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.StylistID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.StylistName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(empCell);

                    TableCell customerCell = new TableCell();
                    customerCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.CustomerID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.FullName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(customerCell);

                    TableCell servNameCell = new TableCell();
                    servNameCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                                        + b.ServiceName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past" +
                    "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);
                    rowCount++;
                }
            }
            catch (ApplicationException Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting all stylists past bookings";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        public void getAllStylistsPastBksForDate(DateTime date, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            try
            {
                bList = handler.getAllStylistsPastBksForDate(date, sortBy, sortDir);

                //phTable.Visible=true;
                tblSchedule.CssClass = "table table-light table-hover table-bordered";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell dateC = new TableCell();
                dateC.Text = "Date";
                dateC.Width = 240;
                dateC.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(dateC);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell emp = new TableCell();
                emp.Text = "Employee";
                emp.Width = 50;
                emp.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(emp);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 240;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 240;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 200;
                tblSchedule.Rows[0].Cells.Add(empty);

                int rowCount = 1;
                foreach (SP_GetStylistBookings b in bList)
                {
                    TableRow r = new TableRow();
                    r.Height = 50;
                    tblSchedule.Rows.Add(r);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                    tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                    TableCell timeCell = new TableCell();
                    timeCell.Text = b.StartTime.ToString("HH:mm");
                    tblSchedule.Rows[rowCount].Cells.Add(timeCell);

                    TableCell empCell = new TableCell();
                    empCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.StylistID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.StylistName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(empCell);

                    TableCell customerCell = new TableCell();
                    customerCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.CustomerID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.FullName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(customerCell);

                    TableCell servNameCell = new TableCell();
                    servNameCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                                        + b.ServiceName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past" +
                    "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);

                    rowCount++;
                }
            }
            catch (ApplicationException Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting all past bookings for required day.";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        public void getAllStylistsPastBookingsDateRange(DateTime startDate, DateTime endDate, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            try
            {
                bList = handler.getAllStylistsPastBookingsDateRange(startDate,endDate, sortBy, sortDir);

                //phTable.Visible=true;
                tblSchedule.CssClass = "table table-light table-hover table-bordered";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell dateC = new TableCell();
                dateC.Text = "Date";
                dateC.Width = 240;
                dateC.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(dateC);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell emp = new TableCell();
                emp.Text = "Employee";
                emp.Width = 50;
                emp.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(emp);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 240;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 240;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 200;
                tblSchedule.Rows[0].Cells.Add(empty);

                int rowCount = 1;
                foreach (SP_GetStylistBookings b in bList)
                {
                    TableRow r = new TableRow();
                    r.Height = 50;
                    tblSchedule.Rows.Add(r);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                    tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                    TableCell timeCell = new TableCell();
                    timeCell.Text = b.StartTime.ToString("HH:mm");
                    tblSchedule.Rows[rowCount].Cells.Add(timeCell);

                    TableCell empCell = new TableCell();
                    empCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.StylistID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.StylistName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(empCell);

                    TableCell customerCell = new TableCell();
                    customerCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.CustomerID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.FullName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(customerCell);

                    TableCell servNameCell = new TableCell();
                    servNameCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                                        + b.ServiceName.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past" +
                    "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);

                    rowCount++;
                }
            }
            catch (ApplicationException Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting all past bookings for date range";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        #endregion

        #endregion

        protected void calDay_SelectionChanged(object sender, EventArgs e)
        {
            lblDay.Text = calDay.SelectedDate.ToString("dd-MM-yyyy");
            if (drpViewAppt.SelectedValue == "0")//upcoming bookings 
            {
                if (empSelectionType.SelectedValue == "0")//all stylists
                {
                    if (lblDay.Text == string.Empty)
                    {
                        phBookingsErr.Visible = true;
                        lblBookingsErr.Text = "Please select a date.";
                    }
                    else
                    {
                        phBookingsErr.Visible = false;
                        getAllStylistsUpcomingBksForDate(calDay.SelectedDate, drpSortBy.SelectedItem.Text,
                                                        drpSortDir.SelectedItem.Text);
                    }
                }
                else if(empSelectionType.SelectedValue == "1")//stylist
                {
                    if (lblDay.Text == string.Empty)
                    {
                        phBookingsErr.Visible = true;
                        lblBookingsErr.Text = "Please select a date.";
                    }
                    else
                    {
                        phBookingsErr.Visible = false;
                        getStylistUpcomingBksForDate(drpStylistNames.SelectedValue, calDay.SelectedDate, 
                                                    drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                }
            }
            else if (drpViewAppt.SelectedValue == "1")//Past bookings 
            {
                if (empSelectionType.SelectedValue == "0")//all stylists
                {
                    if (lblDay.Text == string.Empty)
                    {
                        phBookingsErr.Visible = true;
                        lblBookingsErr.Text = "Please select a date.";
                    }
                    else
                    {
                        phBookingsErr.Visible = false;
                        getAllStylistsPastBksForDate(calDay.SelectedDate, drpSortBy.SelectedItem.Text,
                                                    drpSortDir.SelectedItem.Text);
                    }
                }
                else if (empSelectionType.SelectedValue == "1")//a stylist
                {
                    if (lblDay.Text == string.Empty)
                    {
                        phBookingsErr.Visible = true;
                        lblBookingsErr.Text = "Please select a date.";
                    }
                    else
                    {
                        phBookingsErr.Visible = false;
                        getStylistPastBksForDate(drpStylistNames.SelectedValue,calDay.SelectedDate,
                                                drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                }
            }
        }
        protected void calStart_SelectionChanged(object sender, EventArgs e)
        {
            lblStart.Text = calStart.SelectedDate.ToString("dd-MM-yyyy");
            //lblEnd.Text = calEnd.SelectedDate.ToString("dd-MM-yyyy");

            if (drpViewAppt.SelectedValue == "0")//upcoming bookings
            {
                if (empSelectionType.SelectedValue == "0")//all
                {
                    if (rdoDate.SelectedValue == "3")//date range
                    {
                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getAllStylistsUpcomingBksDR(calStart.SelectedDate, calEnd.SelectedDate, drpSortBy.SelectedItem.Text, 
                                                        drpSortDir.SelectedItem.Text);
                        }
                    }
                }
                else if (empSelectionType.SelectedValue == "1")//stylist
                {
                    if (rdoDate.SelectedValue == "3")
                    {
                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getStylistUpcomingBookingsDR(drpStylistNames.SelectedValue, calStart.SelectedDate, calEnd.SelectedDate, 
                                                        drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }

                }
            }
            else if (drpViewAppt.SelectedValue == "1")//past bookings
            {
                if (empSelectionType.SelectedValue == "0")//all
                {
                    if (rdoDate.SelectedValue == "3")//date range
                    {
                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getAllStylistsPastBookingsDateRange(calStart.SelectedDate, calEnd.SelectedDate, 
                                                            drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }
                }
                else if (empSelectionType.SelectedValue == "1")//stylist
                {
                    if (rdoDate.SelectedValue == "3")
                    {
                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getStylistPastBookingsDateRange(drpStylistNames.SelectedValue, calStart.SelectedDate, calEnd.SelectedDate, 
                                                        drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }

                }
            }
        }
        protected void calEnd_SelectionChanged(object sender, EventArgs e)
        {
            //lblStart.Text = calStart.SelectedDate.ToString("dd-MM-yyyy");
            lblEnd.Text = calEnd.SelectedDate.ToString("dd-MM-yyyy");

            if (drpViewAppt.SelectedValue == "0")//upcoming bookings
            {
                if (empSelectionType.SelectedValue == "0")//all
                {
                    if (rdoDate.SelectedValue == "3")//date range
                    {
                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getAllStylistsUpcomingBksDR(calStart.SelectedDate, calEnd.SelectedDate,
                                                    drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }
                }
                else if (empSelectionType.SelectedValue == "1")//stylist
                {
                    if (rdoDate.SelectedValue == "3")
                    {
                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getStylistUpcomingBookingsDR(drpStylistNames.SelectedValue, calStart.SelectedDate, calEnd.SelectedDate,
                                                    drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }

                }
            }
            else if (drpViewAppt.SelectedValue == "1")//past bookings
            {
                if (empSelectionType.SelectedValue == "0")//all
                {
                    if (rdoDate.SelectedValue == "3")//date range
                    {
                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getAllStylistsPastBookingsDateRange(calStart.SelectedDate, calEnd.SelectedDate, 
                                                            drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }
                }
                else if (empSelectionType.SelectedValue == "1")//stylist
                {
                    if (rdoDate.SelectedValue == "3")
                    {
                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getStylistPastBookingsDateRange(drpStylistNames.SelectedValue, calStart.SelectedDate, calEnd.SelectedDate, 
                                                        drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }

                }
            }
        }
        protected void scheduleCalender_DayRender(object sender, DayRenderEventArgs e)
        {
            if (drpViewAppt.SelectedValue == "0")
            {
                if(e.Day.Date < DateTime.Today)
                {
                    e.Day.IsSelectable = false;
                }
            }
            else if (drpViewAppt.SelectedValue == "1")
            {
                if(e.Day.Date > DateTime.Today)
                {
                    e.Day.IsSelectable = false;
                }
            }
        }
    }
}
