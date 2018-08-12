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
        //incomplete.. edits pending 

        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;

        List<SP_GetStylistBookings> bList= null;
        List<SP_GetEmpNames> list = null;
        protected void Page_Load(object sender, EventArgs e)
        {
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

                //load stylists names into 'names' dropdownlist
                try
                {
                    list = handler.BLL_GetEmpNames();
                    foreach(SP_GetEmpNames emps in list)
                    {
                        drpStylistNames.DataSource = list;
                        drpStylistNames.DataTextField = "Name";
                        drpStylistNames.DataValueField = "EmployeeID";
                        drpStylistNames.DataBind();
                    }
                }
                catch (ApplicationException Err)
                {
                    drpStylistNames.Items.Insert(0, new ListItem("Error"));
                    phBookingsErr.Visible = true;
                    errorHeader.Text = "Error retrieving employee names";
                    errorMessage.Text = "It seems there is a problem retrieving data from the database"
                                        + "Please report problem or try again later.";
                    function.logAnError(Err.ToString());
                }

                DropDownChanges();

            }
        }

        public void DropDownChanges()
        {
            if (drpViewAppt.SelectedValue == "1")
            {
                //upcoming

                phNames.Visible = false;
                phCalendars.Visible = false;

                if (empSelectionType.SelectedValue == "1")
                {
                    //all
                    phNames.Visible = false;
                    phCalendars.Visible = true;
                    phDay.Visible = false;
                    phDateRange.Visible = false;

                    if (rdoDate.SelectedValue == "0")
                    {
                        //today
                        phDateRange.Visible = false;
                        phDay.Visible = false;
                        getAllStylistsUpcomingBookings();
                    }
                    else if (rdoDate.SelectedValue == "1")
                    {
                        //Specific Day
                        phDay.Visible = true;
                        phDateRange.Visible = false;
                    }
                    else if (rdoDate.SelectedValue == "2")
                    {
                        //date range
                        phDay.Visible = false;
                        phDateRange.Visible = true;
                    }

                }
                else if (empSelectionType.SelectedValue == "2")
                {
                    //stylist
                    phNames.Visible = true;
                    phCalendars.Visible = false;
                    if (drpStylistNames.SelectedValue != "0")
                    {
                        //name is chosen
                        phCalendars.Visible = true;
                        if (rdoDate.SelectedValue == "0")
                        {
                            //today
                            phDateRange.Visible = false;
                            phDay.Visible = false;

                        }
                        else if (rdoDate.SelectedValue == "1")
                        {
                            //specific day
                            phDay.Visible = true;
                            phDateRange.Visible = false;
                        }
                        else if (rdoDate.SelectedValue == "2")
                        {
                            //date range
                            phDay.Visible = false;
                            phDateRange.Visible = true;
                        }
                    }
                }
            }
            else if (drpViewAppt.SelectedValue == "2")
            {
                //Past
                phNames.Visible = false;
                phCalendars.Visible = false;
                if (empSelectionType.SelectedValue == "1")
                {
                    //all
                    phNames.Visible = false;
                    phCalendars.Visible = true;
                    phDay.Visible = false;
                    phDateRange.Visible = false;

                    if (rdoDate.SelectedValue == "0")
                    {
                        //today
                        phDateRange.Visible = false;
                        phDay.Visible = false;

                        
                    }
                    else if (rdoDate.SelectedValue == "1")
                    {
                        //Specific Day
                        phDay.Visible = true;
                        phDateRange.Visible = false;

                    }
                    else if (rdoDate.SelectedValue == "2")
                    {
                        //date range
                        phDay.Visible = false;
                        phDateRange.Visible = true;                        
                    }

                }
                else if (empSelectionType.SelectedValue == "2")
                {
                    //stylist
                    phNames.Visible = true;
                    phCalendars.Visible = false;
                    if (drpStylistNames.SelectedValue != "0")
                    {
                        //name is chosen
                        phCalendars.Visible = true;
                        if (rdoDate.SelectedValue == "0")
                        {
                            //today
                            phDateRange.Visible = false;
                            phDay.Visible = false;

                        }
                        else if (rdoDate.SelectedValue == "1")
                        {
                            //specific day
                            phDay.Visible = true;
                            phDateRange.Visible = false;
                        }
                        else if (rdoDate.SelectedValue == "2")
                        {
                            //date range
                            phDay.Visible = false;
                            phDateRange.Visible = true;
                        }
                    }
                }

            }
        }

        public void getStylistPastBookings(string empID)
        {
            try
            {
                bList = handler.getStylistPastBookings(empID);

                phTable.Visible = true;
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 200;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 50;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 100;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 200;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell svDesc = new TableCell();
                svDesc.Text = "Description";
                svDesc.Width = 400;
                svDesc.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svDesc);

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

                    TableCell servDescCell = new TableCell();
                    servDescCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                + b.ServiceDescription.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servDescCell);

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
        public void getStylistPastBookingsDateRange(string empID, DateTime startDate, DateTime endDate)
        {
            try
            {
                bList = handler.getStylistPastBookingsDateRange(empID, startDate, endDate);

                phTable.Visible = true;
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 200;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 50;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 100;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 200;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell svDesc = new TableCell();
                svDesc.Text = "Description";
                svDesc.Width = 400;
                svDesc.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svDesc);

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

                    TableCell servDescCell = new TableCell();
                    servDescCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                                        + b.ServiceDescription.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servDescCell);

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
        public void getStylistUpcomingBookings(string empID)
        {
            try
            {
                phTable.Visible = true;

                bList = handler.getStylistUpcomingBookings(empID);

                tblSchedule.Visible = true;
                tblSchedule.CssClass = "table table-light table-hover";

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

                TableCell svDesc = new TableCell();
                svDesc.Text = "Description";
                svDesc.Width = 440;
                svDesc.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svDesc);

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

                    TableCell servDescCell = new TableCell();
                    servDescCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                                        + b.ServiceDescription.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servDescCell);

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
        public void getStylistUpcomingBookingsDR(string empID, DateTime startDate, DateTime endDate)
        {
            try
            {
                bList = handler.getStylistUpcomingBookingsDR(empID, startDate, endDate);

                phTable.Visible = true;
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 200;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 50;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 100;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 200;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell svDesc = new TableCell();
                svDesc.Text = "Description";
                svDesc.Width = 400;
                svDesc.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svDesc);

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

                    TableCell servDescCell = new TableCell();
                    servDescCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                                        + b.ServiceDescription.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servDescCell);

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
        public void getAllStylistsUpcomingBksForDate(DateTime bookingDate)
        {
            try
            {
                bList = handler.getAllStylistsUpcomingBksForDate(bookingDate);

                phTable.Visible = true;
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 200;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 50;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell emp = new TableCell();
                emp.Text = "Employee";
                emp.Width = 50;
                emp.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(emp);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 100;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 200;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell svDesc = new TableCell();
                svDesc.Text = "Description";
                svDesc.Width = 400;
                svDesc.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svDesc);

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

                    TableCell servDescCell = new TableCell();
                    servDescCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                                        + b.ServiceDescription.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servDescCell);

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
                errorHeader.Text = "Error getting past bookings within required date range (day).";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        public void getAllStylistsUpcomingBksDR(DateTime startDate, DateTime endDate)
        {
            try
            {
                bList = handler.getAllStylistsUpcomingBksDR(startDate, endDate);

                phTable.Visible = true;
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 200;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 50;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell emp = new TableCell();
                emp.Text = "Employee";
                emp.Width = 50;
                emp.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(emp);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 100;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 200;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell svDesc = new TableCell();
                svDesc.Text = "Description";
                svDesc.Width = 400;
                svDesc.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svDesc);

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

                    TableCell servDescCell = new TableCell();
                    servDescCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                                        + b.ServiceDescription.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servDescCell);

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
                errorHeader.Text = "Error getting past bookings within required date range.";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        public void getAllStylistsUpcomingBookings()
        {
            try
            {
                bList = handler.getAllStylistsUpcomingBookings();

                phTable.Visible = true;
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 200;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 50;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell emp = new TableCell();
                emp.Text = "Employee";
                emp.Width = 50;
                emp.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(emp);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 100;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 200;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell svDesc = new TableCell();
                svDesc.Text = "Description";
                svDesc.Width = 400;
                svDesc.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svDesc);

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

                    TableCell servDescCell = new TableCell();
                    servDescCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                                        + b.ServiceDescription.ToString() + "</a>";
                    tblSchedule.Rows[rowCount].Cells.Add(servDescCell);

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
                errorHeader.Text = "Error getting past bookings for stylists.";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }

    }
}