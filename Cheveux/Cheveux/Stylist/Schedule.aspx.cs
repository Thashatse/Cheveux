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
    public partial class Schedule : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;
        List<SP_GetStylistBookings> bList = null;
        SP_ViewEmployee viewEmp = null;

        protected void Page_PreInit(Object sender, EventArgs e)
        {
            //check the cheveux user id cookie for the user
            HttpCookie cookie = Request.Cookies["CheveuxUserID"];
            char userType;
            //check if the cookie is empty or not
            if (cookie != null)
            {
                //store the user Type in a variable and display the appropriate master page for the user
                userType = cookie["UT"].ToString()[0];
                //if customer
                if (userType == 'C')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/Cheveux.Master";
                }
                //if receptionist
                else if (userType == 'R')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/CheveuxReceptionist.Master";
                }
                //if stylist
                else if (userType == 'S')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/CheveuxStylist.Master";
                }
                //if Manager
                else if (userType == 'M')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/CheveuxManager.Master";
                }
                //default
                else
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/Cheveux.Master";
                }
            }
            //set the default master page if the cookie is empty
            else
            {
                //set the master page
                this.MasterPageFile = "~/MasterPages/Cheveux.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            errorCssStyles();

            cookie = Request.Cookies["CheveuxUserID"];
            string empID = Request.QueryString["empID"];
            string action = Request.QueryString["Action"];

            if (cookie == null)
            {
                phLogIn.Visible = true;
                phMain.Visible = false;
            }
            else if(cookie["UT"] == "S")
            {
                phLogIn.Visible = false;
                phMain.Visible = true;

                try
                {
                    if(drpViewAppt.SelectedValue ==  "0")//upcoming
                    {
                        //phCheckBox.Visible = false;
                        phCal.Visible = false;

                        if (bxAll.Checked)
                        {
                            phBookingsErr.Visible = false;
                            phCal.Visible = false;
                            getUpcomingBookings(cookie["ID"].ToString(), 
                                        drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                        else if (!bxAll.Checked)
                        {
                            phCal.Visible = true;
                        }
                    }
                    else if(drpViewAppt.SelectedValue == "1")//past
                    {
                        //phCheckBox.Visible = true;
                        if (bxAll.Checked)
                        {
                            phBookingsErr.Visible = false;
                            phCal.Visible = false;

                            getPastBookings(cookie["ID"].ToString(), drpSortBy.SelectedItem.Text, 
                                                    drpSortDir.SelectedItem.Text);
                        }
                        else if (!bxAll.Checked)
                        {
                            phCal.Visible = true;
                        }
                    }
                }
                catch (Exception Err)
                {
                    phScheduleErr.Visible = true;
                    errorHeader.Text = "Error";
                    errorMessage.Text = "It seems there is a problem connecting to the database.<br/>"
                                        + "Please report problem or try again later.";
                    function.logAnError(Err.ToString());
                }
            }
            else if(cookie["UT"] == "M")
            {
                phLogIn.Visible = false;
                phMain.Visible = true;
                viewEmp = handler.viewEmployee(empID);
                if (empID != null)
                {
                    if (action == "ViewSchedule" && empID != null)
                    {
                        Header.Visible = false;
                        managerview.Visible = true;
                        viewEmployee(empID);
                        EmployeeHead.Text = "Viewing Schedule of "
                                           + viewEmp.firstName.ToString() + ' ' + viewEmp.lastName.ToString();
                        try
                        {
                            if (drpViewAppt.SelectedValue == "0")//upcoming
                            {
                                //phCheckBox.Visible = false;
                                phCal.Visible = false;

                                if (bxAll.Checked)
                                {
                                    phBookingsErr.Visible = false;
                                    phCal.Visible = false;
                                    getUpcomingBookings(empID, drpSortBy.SelectedItem.Text,
                                                drpSortDir.SelectedItem.Text);
                                }
                                else if (!bxAll.Checked)
                                {
                                    phCal.Visible = true;
                                }
                            }
                            else if (drpViewAppt.SelectedValue == "1")//past
                            {
                                //phCheckBox.Visible = true;
                                if (bxAll.Checked)
                                {
                                    phBookingsErr.Visible = false;
                                    phCal.Visible = false;

                                    getPastBookings(empID, drpSortBy.SelectedItem.Text,
                                                        drpSortDir.SelectedItem.Text);
                                }
                                else if (!bxAll.Checked)
                                {
                                    phCal.Visible = true;
                                }
                            }
                        }
                        catch (Exception Err)
                        {
                            phScheduleErr.Visible = true;
                            errorHeader.Text = "Error";
                            errorMessage.Text = "It seems there is a problem connecting to the database.<br/>"
                                                + "Please report problem or try again later.";
                            function.logAnError(Err.ToString());
                        }
                    }
                    else
                    {
                        Response.Redirect("../Manager/Employee.aspx");
                    }
                }
                else
                {
                    phScheduleErr.Visible = true;
                    errorHeader.Text = "Error displaying employee schedule page.";
                    errorMessage.Text = "No employeeID passed through.";
                }
            }  
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }

        public void errorCssStyles()
        {
            errorHeader.Font.Bold = true;
            errorHeader.Font.Underline = true;
            errorHeader.Font.Size = 21;
            errorMessage.Font.Size = 14;
        }

        public void viewEmployee(string empID)
        {
            try
            {
                viewEmp = handler.viewEmployee(empID);
                TableRow row = new TableRow();
                tblEmployee.Rows.Add(row);
                TableCell userImage = new TableCell();
                userImage.Text = "<img src=" + viewEmp.empImage
                                  + " alt='" + viewEmp.firstName + " " + viewEmp.lastName +
                                 " Profile Image' width='125' height='125'/>";
                tblEmployee.Rows[0].Cells.Add(userImage);
                TableRow newRow = new TableRow();
                tblEmployee.Rows.Add(newRow);
                //TableCell username = new TableCell();
                //username.Text = "<p style='font-size:2em !important;'>" + viewEmp.userName.ToString() + "</p>";
                //username.Font.Bold = true;
                //tblEmployee.Rows[1].Cells.Add(username);
            }
            catch (Exception Err)
            {
                phScheduleErr.Visible = true;
                phMain.Visible = false;
                errorHeader.Text = "Error displaying user details.";
                errorMessage.Text = "It seems there is a problem communicating with the database."
                                    + "Please report problem to admin or try again later.";

                function.logAnError(Err.ToString());
            }
        }

        #region PAST
        public void getPastBookings(string empID,string sortBy,string sortDir)
        {
            try
            {
                bList = handler.getStylistPastBookings(empID,sortBy,sortDir);

                phPast.Visible = true;
                tblPast.CssClass= "table table-light table-hover";

                TableRow row = new TableRow();
                tblPast.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 220;
                date.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 120;
                time.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 150;
                customer.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 150;
                svName.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 200;
                tblPast.Rows[0].Cells.Add(empty);

                int rowCount = 1;
                foreach(SP_GetStylistBookings b in bList)
                {
                    TableRow r = new TableRow();
                    r.Height = 50;
                    tblPast.Rows.Add(r);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                    tblPast.Rows[rowCount].Cells.Add(dateCell);

                    TableCell timeCell = new TableCell();
                    timeCell.Text = b.StartTime.ToString("HH:mm");
                    tblPast.Rows[rowCount].Cells.Add(timeCell);

                    TableCell customerCell = new TableCell();
                    customerCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.CustomerID.ToString().Replace(" ", string.Empty) +
                                    "'>" + b.FullName.ToString() + "</a>";
                    tblPast.Rows[rowCount].Cells.Add(customerCell);

                //    TableCell servNameCell = new TableCell();
                //    servNameCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                //+ b.ServiceName.ToString() + "</a>";
                //    tblPast.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past" +
                    "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
                    tblPast.Rows[rowCount].Cells.Add(buttonCell);
                    rowCount++;
                }
            }
            catch (Exception Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting past bookings.";
                errorMessage.Text = "It seems there is a problem communicating with the database."
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        public void pastDateRange(string empID, DateTime startDate, DateTime endDate,string sortBy,string sortDir)
        {
            try
            {
                bList = handler.getStylistPastBookingsDateRange(empID, startDate, endDate,sortBy, sortDir);

                phPast.Visible = true;
                tblPast.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblPast.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 220;
                date.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 120;
                time.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 150;
                customer.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 150;
                svName.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 200;
                tblPast.Rows[0].Cells.Add(empty);

                int rowCount = 1;
                foreach (SP_GetStylistBookings b in bList)
                {
                    TableRow r = new TableRow();
                    r.Height = 50;
                    tblPast.Rows.Add(r);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                    tblPast.Rows[rowCount].Cells.Add(dateCell);

                    TableCell timeCell = new TableCell();
                    timeCell.Text = b.StartTime.ToString("HH:mm");
                    tblPast.Rows[rowCount].Cells.Add(timeCell);

                    TableCell customerCell = new TableCell();
                    customerCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.CustomerID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.FullName.ToString() + "</a>";
                    tblPast.Rows[rowCount].Cells.Add(customerCell);

                    //TableCell servNameCell = new TableCell();
                    //servNameCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                    //                    + b.ServiceName.ToString() + "</a>";
                    //tblPast.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past" +
                    "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
                    tblPast.Rows[rowCount].Cells.Add(buttonCell);
                    rowCount++;
                }
            }
            catch (Exception Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting past bookings within required date range.";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }



        }
        #endregion

        #region UPCOMING
        public void getUpcomingBookings(string empID,string sortBy,string sortDir)
        {
            try
            {
                phUpcoming.Visible = true;

                bList = handler.getStylistUpcomingBookings(empID,sortBy,sortDir);

                tblUpcoming.Visible = true;
                tblUpcoming.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblUpcoming.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 220;
                date.Font.Bold = true;
                tblUpcoming.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 120;
                time.Font.Bold = true;
                tblUpcoming.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 150;
                customer.Font.Bold = true;
                tblUpcoming.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 150;
                svName.Font.Bold = true;
                tblUpcoming.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 200;
                tblUpcoming.Rows[0].Cells.Add(empty);

                int rowCount = 1;
                foreach (SP_GetStylistBookings b in bList)
                {
                    TableRow r = new TableRow();
                    r.Height = 50;
                    tblUpcoming.Rows.Add(r);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                    tblUpcoming.Rows[rowCount].Cells.Add(dateCell);

                    TableCell timeCell = new TableCell();
                    timeCell.Text = b.StartTime.ToString("HH:mm");
                    tblUpcoming.Rows[rowCount].Cells.Add(timeCell);

                    TableCell customerCell = new TableCell();
                    customerCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.CustomerID.ToString().Replace(" ", string.Empty) +
                                    "'>" + b.FullName.ToString() + "</a>";
                    tblUpcoming.Rows[rowCount].Cells.Add(customerCell);

                //    TableCell servNameCell = new TableCell();
                //    servNameCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                //+ b.ServiceName.ToString() + "</a>";
                //    tblUpcoming.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past" +
                    "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
                    tblUpcoming.Rows[rowCount].Cells.Add(buttonCell);

                    rowCount++;
                }
            }
            catch (Exception Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting upcoming bookings";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        public void getStylistUpcomingBookingsDR(string empID, DateTime startDate, DateTime endDate,string sortBy,string sortDir)
        {
            tblUpcoming.Rows.Clear();
            try
            {
                bList = handler.getStylistUpcomingBookingsDR(empID, startDate, endDate,sortBy,sortDir);

                ////phTable.Visible=true;
                tblUpcoming.CssClass = "table table-light table-hover table-bordered";

                TableRow row = new TableRow();
                tblUpcoming.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 220;
                date.Font.Bold = true;
                tblUpcoming.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 120;
                time.Font.Bold = true;
                tblUpcoming.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 150;
                customer.Font.Bold = true;
                tblUpcoming.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 150;
                svName.Font.Bold = true;
                tblUpcoming.Rows[0].Cells.Add(svName);


                TableCell empty = new TableCell();
                empty.Width = 200;
                tblUpcoming.Rows[0].Cells.Add(empty);

                int rowCount = 1;
                foreach (SP_GetStylistBookings b in bList)
                {
                    TableRow r = new TableRow();
                    r.Height = 50;
                    tblUpcoming.Rows.Add(r);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                    tblUpcoming.Rows[rowCount].Cells.Add(dateCell);

                    TableCell timeCell = new TableCell();
                    timeCell.Text = b.StartTime.ToString("HH:mm");
                    tblUpcoming.Rows[rowCount].Cells.Add(timeCell);

                    TableCell customerCell = new TableCell();
                    customerCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.CustomerID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.FullName.ToString() + "</a>";
                    tblUpcoming.Rows[rowCount].Cells.Add(customerCell);

                    //TableCell servNameCell = new TableCell();
                    //servNameCell.Text = "<a href='ViewProduct.aspx?ProductID=" + b.ServiceID.Replace(" ", string.Empty) + "'>"
                    //                    + b.ServiceName.ToString() + "</a>";
                    //tblUpcoming.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past" +
                    "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
                    tblUpcoming.Rows[rowCount].Cells.Add(buttonCell);
                    rowCount++;
                }
            }
            catch (Exception Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting upcoming bookings for stylist for required date range.";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        #endregion

        protected void calStart_SelectionChanged(object sender,EventArgs e)
        {
            string empID = Request.QueryString["empID"];
            if (cookie["UT"] == "S")
            {
                lblDate1.Text = calStart.SelectedDate.ToString("dd-MM-yyyy");
                if (drpViewAppt.SelectedValue == "0")
                {
                    if (lblDate1.Text == string.Empty || lblDate2.Text == string.Empty)
                    {
                        phBookingsErr.Visible = true;
                        lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                            + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
                    }
                    else
                    {
                        phBookingsErr.Visible = false;
                        getStylistUpcomingBookingsDR(cookie["ID"].ToString(), calStart.SelectedDate, calEnd.SelectedDate,
                                            drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                }
                else if (drpViewAppt.SelectedValue == "1")
                {
                    if (lblDate1.Text == string.Empty || lblDate2.Text == string.Empty)
                    {
                        phBookingsErr.Visible = true;
                        lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                            + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
                    }
                    else
                    {
                        phBookingsErr.Visible = false;
                        pastDateRange(cookie["ID"].ToString(), calStart.SelectedDate, calEnd.SelectedDate, 
                                        drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                }
            }
            else if(cookie["UT"] == "M")
            {
                lblDate1.Text = calStart.SelectedDate.ToString("dd-MM-yyyy");
                if (drpViewAppt.SelectedValue == "0")
                {
                    if (lblDate1.Text == string.Empty || lblDate2.Text == string.Empty)
                    {
                        phBookingsErr.Visible = true;
                        lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                            + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
                    }
                    else
                    {
                        phBookingsErr.Visible = false;
                        getStylistUpcomingBookingsDR(empID, calStart.SelectedDate, calEnd.SelectedDate,
                                                drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                }
                else if (drpViewAppt.SelectedValue == "1")
                {
                    if (lblDate1.Text == string.Empty || lblDate2.Text == string.Empty)
                    {
                        phBookingsErr.Visible = true;
                        lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                            + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
                    }
                    else
                    {
                        phBookingsErr.Visible = false;
                        pastDateRange(empID, calStart.SelectedDate, calEnd.SelectedDate
                                , drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                }
            }
        }

        protected void calEnd_SelectionChanged(object sender, EventArgs e)
        {
            string empID = Request.QueryString["empID"];
            if(cookie["UT"] == "S")
            {
                lblDate2.Text = calEnd.SelectedDate.ToString("dd-MM-yyyy");

                if (drpViewAppt.SelectedValue == "0")//upcoming
                {
                    if (lblDate1.Text == string.Empty || lblDate2.Text == string.Empty)
                    {
                        phBookingsErr.Visible = true;
                        lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                            + "Hint: View bookings between 1/1/19(start date) and 12/12/19(end date)";
                    }
                    else
                    {
                        phBookingsErr.Visible = false;
                        getStylistUpcomingBookingsDR(cookie["ID"].ToString(), calStart.SelectedDate, calEnd.SelectedDate,
                                                     drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                }
                else if (drpViewAppt.SelectedValue == "1")//past
                {
                    if (lblDate1.Text == string.Empty || lblDate2.Text == string.Empty)
                    {
                        phBookingsErr.Visible = true;
                        lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                            + "Hint: View bookings between 1/1/19(start date) and 12/12/19(end date)";
                    }
                    else
                    {
                        phBookingsErr.Visible = false;
                        pastDateRange(cookie["ID"].ToString(), calStart.SelectedDate, calEnd.SelectedDate, 
                                                drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                }
            }
            else if(cookie["UT"] == "M")
            {
                lblDate2.Text = calEnd.SelectedDate.ToString("dd-MM-yyyy");

                if (drpViewAppt.SelectedValue == "0")//upcoming
                {
                    if (lblDate1.Text == string.Empty || lblDate2.Text == string.Empty)
                    {
                        phBookingsErr.Visible = true;
                        lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                            + "Hint: View bookings between 1/1/19(start date) and 12/12/19(end date)";
                    }
                    else
                    {
                        phBookingsErr.Visible = false;
                        getStylistUpcomingBookingsDR(empID, calStart.SelectedDate, calEnd.SelectedDate, 
                                                drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                }
                else if (drpViewAppt.SelectedValue == "1")//past
                {
                    if (lblDate1.Text == string.Empty || lblDate2.Text == string.Empty)
                    {
                        phBookingsErr.Visible = true;
                        lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                            + "Hint: View bookings between 1/1/19(start date) and 12/12/19(end date)";
                    }
                    else
                    {
                        phBookingsErr.Visible = false;
                        pastDateRange(empID, calStart.SelectedDate, calEnd.SelectedDate, 
                                    drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                }
            }

            
        }

        protected void cal_DayRender(object sender, DayRenderEventArgs e)
        {
            if (drpViewAppt.SelectedValue == "0")
            {
                if (e.Day.Date < DateTime.Today)
                {
                    e.Day.IsSelectable = false;
                }
            }
            else if (drpViewAppt.SelectedValue == "1")
            {
                if (e.Day.Date > DateTime.Today)
                {
                    e.Day.IsSelectable = false;
                }
            }
        }
    }
}