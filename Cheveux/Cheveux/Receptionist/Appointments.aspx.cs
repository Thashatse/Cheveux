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
        //List<SP_GetEmpAgenda> agenda = null;
        List<SP_GetStylistBookings> bList = null;
        List<SP_GetEmpNames> list = null;
        SP_ViewEmployee viewEmp = null;
        List<SP_GetBookingServices> bServices = null;
        SP_GetMultipleServicesTime time = null;
        string today = DateTime.Now.ToString("yyyy-MM-dd");

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
                    this.MasterPageFile = "~/MasterPages/CheveuxManager.Master";
                }
            }
            //set the default master page if the cookie is empty
            else
            {
                //set the master page
                this.MasterPageFile = "~/MasterPages/CheveuxManager.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            errorCssStyles();
            cookie = Request.Cookies["CheveuxUserID"];

            if (cookie == null)
            {
                phLogin.Visible = true;
                phMain.Visible = false;
            }
            else if (cookie["UT"] != "R" && cookie["UT"] != "S" && cookie["UT"] != "M")
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
                    catch (Exception Err)
                    {
                        drpStylistNames.Items.Add("Stylist names unavailable");
                        function.logAnError(Err.ToString());
                    }
                }
                DropDownChanges(cookie["UT"].ToString());
            }
            else if (cookie["UT"] == "S")
            {
                phLogin.Visible = false;
                phMain.Visible = true;

                phStylists.Visible = false;
                phNames.Visible = false;

                DropDownChanges(cookie["UT"].ToString());
            }
            else if (cookie["UT"] == "M")
            {
                phLogin.Visible = false;
                phMain.Visible = true;

                string stylistID = Request.QueryString["empID"];
                string action = Request.QueryString["Action"];

                if(action == "ViewStylistSchedule")
                {
                    phStylists.Visible = false;
                    phNames.Visible = false;

                    if (stylistID != null)
                    {
                        viewEmp = handler.viewEmployee(stylistID);

                        Header.Visible = false;
                        managerview.Visible = true;
                        viewEmployee(stylistID);
                        EmployeeHead.Text = "Viewing Schedule of "
                                            + viewEmp.firstName.ToString() + ' ' + viewEmp.lastName.ToString();

                        DropDownChanges(cookie["UT"].ToString());
                    }
                    else if (stylistID == null)
                    {
                        phWhen.Visible = false;
                        phStylists.Visible = false;
                        phNames.Visible = false;
                        phCalendars.Visible = false;
                        phScheduleErr.Visible = true;
                        errorHeader.Text = "Error retrieving stylistID";
                        errorMessage.Text = "Please report problem to admin or try again later.";
                    }
                }
                else if(action == "ViewAllSchedules")
                {
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
                        catch (Exception Err)
                        {
                            drpStylistNames.Text = "Cannot retrieve names";
                            phScheduleErr.Visible = true;
                            errorHeader.Text = "Error retrieving employee names";
                            errorMessage.Text = "It seems there is a problem retrieving data from the database"
                                                + "Please report problem or try again later.";
                            function.logAnError(Err.ToString());
                        }
                    }
                    DropDownChanges(cookie["UT"].ToString());
                }
            }
        }

        public void errorCssStyles()
        {
            errorHeader.Font.Bold = true;
            errorHeader.Font.Underline = true;
            errorHeader.Font.Size = 21;
            errorMessage.Font.Size = 14;
        }

        public void DropDownChanges(string userType)
        {
            string action = Request.QueryString["Action"];

            if (userType == "R" || (userType =="M" && action== "ViewAllSchedules"))
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

                            if (lblDay.Text == "Label1")
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
                                                    + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
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
                                                        + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
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

                            getAllStylistsPastBookings(drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                        else if (rdoDate.SelectedValue == "1")
                        {
                            //today
                            phBookingsErr.Visible = false;
                            phDay.Visible = false;
                            phDateRange.Visible = false;

                            getAllStylistsPastBksForDate(DateTime.Parse(today)
                                                        , drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                        else if (rdoDate.SelectedValue == "2")
                        {
                            //Specific Day
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
                                getAllStylistsPastBksForDate(calDay.SelectedDate, drpSortBy.SelectedItem.Text,
                                                            drpSortDir.SelectedItem.Text);
                            }

                        }
                        else if (rdoDate.SelectedValue == "3")
                        {
                            //Date Range
                            phDay.Visible = false;
                            phDateRange.Visible = true;

                            if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                            {
                                phBookingsErr.Visible = true;
                                lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                    + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
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
                                                        , drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
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
                            else if (rdoDate.SelectedValue == "3")
                            {
                                //date range
                                phDay.Visible = false;
                                phDateRange.Visible = true;

                                if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                                {
                                    phBookingsErr.Visible = true;
                                    lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                        + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
                                }
                                else
                                {
                                    phBookingsErr.Visible = false;
                                    getStylistPastBookingsDateRange(drpStylistNames.SelectedValue, calStart.SelectedDate, calEnd.SelectedDate
                                                                , drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                                }
                            }
                        }
                    }
                }
            }
            else if (userType == "S")
            {
                phCalendars.Visible = true;
                empSelectionType.SelectedValue = "1";
                drpSortBy.Items.RemoveAt(1);
                drpSortBy.Items.Add(new ListItem("Customer", "1"));

                if (drpViewAppt.SelectedValue == "0")//upcoming
                {
                    if (rdoDate.SelectedValue == "0")
                    {
                        //all upcoming bookings
                        phBookingsErr.Visible = false;
                        phDateRange.Visible = false;
                        phDay.Visible = false;
                        getStylistUpcomingBookings(cookie["ID"].ToString(), drpSortBy.SelectedItem.Text,
                                                    drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "1")
                    {
                        //upcoming bookings for today 
                        phBookingsErr.Visible = false;
                        phDateRange.Visible = false;
                        phDay.Visible = false;
                        getStylistUpcomingBksForDate(cookie["ID"].ToString(), DateTime.Parse(today),
                                                    drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "2")
                    {
                        //upcoming bookings for a specific day 
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
                            getStylistUpcomingBksForDate(cookie["ID"].ToString(), calDay.SelectedDate, drpSortBy.SelectedItem.Text,
                                                            drpSortDir.SelectedItem.Text);
                        }

                    }
                    else if (rdoDate.SelectedValue == "3")
                    {
                        //upcoming bookings for a date range 
                        phDay.Visible = false;
                        phDateRange.Visible = true;

                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getStylistUpcomingBookingsDR(cookie["ID"].ToString(), calStart.SelectedDate, calEnd.SelectedDate,
                                                        drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }
                }
                else if(drpViewAppt.SelectedValue == "1")//past
                {
                    if (rdoDate.SelectedValue == "0")
                    {
                        //all past bookings
                        phBookingsErr.Visible = false;
                        phDateRange.Visible = false;
                        phDay.Visible = false;

                        getStylistPastBookings(cookie["ID"].ToString(), drpSortBy.SelectedItem.Text,
                                                            drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "1")
                    {
                        //past bookings for today
                        phBookingsErr.Visible = false;
                        phDay.Visible = false;
                        phDateRange.Visible = false;

                        getStylistPastBksForDate(cookie["ID"].ToString(), DateTime.Parse(today)
                                                , drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "2")
                    {
                        //Past bookings for a Specific day
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
                            getStylistPastBksForDate(cookie["ID"].ToString(), calDay.SelectedDate,
                                                    drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }
                    else if (rdoDate.SelectedValue == "3")
                    {
                        //Past bookings for a date range
                        phDay.Visible = false;
                        phDateRange.Visible = true;

                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getStylistPastBookingsDateRange(cookie["ID"].ToString(), calStart.SelectedDate, calEnd.SelectedDate,
                                                        drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }
                }
            }
            else if(userType == "M" && action == "ViewStylistSchedule")
            {
                string stylistID = Request.QueryString["empID"];
                
                phCalendars.Visible = true;

                if (drpViewAppt.SelectedValue == "0")//upcoming
                {
                    if (rdoDate.SelectedValue == "0")
                    {
                        //all upcoming bookings
                        phBookingsErr.Visible = false;
                        phDateRange.Visible = false;
                        phDay.Visible = false;
                        getStylistUpcomingBookings(stylistID, drpSortBy.SelectedItem.Text,
                                                    drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "1")
                    {
                        //upcoming bookings for today 
                        phBookingsErr.Visible = false;
                        phDateRange.Visible = false;
                        phDay.Visible = false;
                        getStylistUpcomingBksForDate(stylistID, DateTime.Parse(today),
                                                    drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "2")
                    {
                        //upcoming bookings for a specific day 
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
                            getStylistUpcomingBksForDate(stylistID, calDay.SelectedDate, drpSortBy.SelectedItem.Text,
                                                            drpSortDir.SelectedItem.Text);
                        }

                    }
                    else if (rdoDate.SelectedValue == "3")
                    {
                        //upcoming bookings for a date range 
                        phDay.Visible = false;
                        phDateRange.Visible = true;

                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getStylistUpcomingBookingsDR(stylistID, calStart.SelectedDate, calEnd.SelectedDate,
                                                        drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }
                }
                else if (drpViewAppt.SelectedValue == "1")//past
                {
                    if (rdoDate.SelectedValue == "0")
                    {
                        //all past bookings
                        phBookingsErr.Visible = false;
                        phDateRange.Visible = false;
                        phDay.Visible = false;

                        getStylistPastBookings(stylistID, drpSortBy.SelectedItem.Text,
                                                            drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "1")
                    {
                        //past bookings for today
                        phBookingsErr.Visible = false;
                        phDay.Visible = false;
                        phDateRange.Visible = false;

                        getStylistPastBksForDate(stylistID, DateTime.Parse(today)
                                                , drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "2")
                    {
                        //Past bookings for a Specific day
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
                            getStylistPastBksForDate(stylistID, calDay.SelectedDate,
                                                    drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }
                    else if (rdoDate.SelectedValue == "3")
                    {
                        //Past bookings for a date range
                        phDay.Visible = false;
                        phDateRange.Visible = true;

                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getStylistPastBookingsDateRange(stylistID, calStart.SelectedDate, calEnd.SelectedDate,
                                                        drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }
                }
            }     
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
                                 " Profile Image' width='150' height='170'/>";
                tblEmployee.Rows[0].Cells.Add(userImage);
                TableRow newRow = new TableRow();
                tblEmployee.Rows.Add(newRow);
            }
            catch (Exception Err)
            {
                TableRow row = new TableRow();
                tblEmployee.Rows.Add(row);
                TableCell userImage = new TableCell();
                userImage.Text = "Error displaying user image";
                tblEmployee.Rows[0].Cells.Add(userImage);

                function.logAnError("Couldn't display user image in apts.aspx err:"+Err.ToString());
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
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date<br/>(d/M/Y)";
                date.Width = 300;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Start Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                time = new TableCell();
                time.Text = "End Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 300;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 300;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 150;
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

                    getTimeCustomerServices(b.BookingID, b.PrimaryID, rowCount, b);

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
            catch (Exception Err)
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
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date<br/>(d/M/Y)";
                date.Width = 300;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Start Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                time = new TableCell();
                time.Text = "End Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 300;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 300;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 150;
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

                    getTimeCustomerServices(b.BookingID, b.PrimaryID, rowCount, b);

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
            catch (Exception Err)
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
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date<br/>(d/M/Y)";
                date.Width = 300;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Start Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                time = new TableCell();
                time.Text = "End Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 300;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 300;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 150;
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

                    getTimeCustomerServices(b.BookingID, b.PrimaryID, rowCount, b);

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
            catch (Exception Err)
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
            cookie = Request.Cookies["CheveuxUserID"];

            tblSchedule.Rows.Clear();
            try
            {
                ////phTable.Visible=true;

                bList = handler.getStylistUpcomingBookings(empID,sortBy,sortDir);

                tblSchedule.Visible = true;
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date<br/>(d/M/Y)";
                date.Width = 300;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Start Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                time = new TableCell();
                time.Text = "End Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 300;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 300;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                if (cookie["UT"] != "S")
                {
                    TableCell edit = new TableCell();
                    edit.Width = 200;
                    tblSchedule.Rows[0].Cells.Add(edit);
                }

                TableCell empty = new TableCell();
                empty.Width = 150;
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

                    getTimeCustomerServices(b.BookingID, b.PrimaryID, rowCount, b);

                    if (cookie["UT"] != "S")
                    {
                        //edit
                        TableCell editButton = new TableCell();
                        editButton.Text =
                            "<button type = 'button' class='btn btn-default'>" +
                        "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                        "&Action=Edit'>Edit Booking</a></button>";
                        tblSchedule.Rows[rowCount].Cells.Add(editButton);
                    }

                    //view booking
                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);
                    
                    rowCount++;
                }
            }
            catch (Exception Err)
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
            cookie = Request.Cookies["CheveuxUserID"];

            tblSchedule.Rows.Clear();
            try
            {
                bList = handler.getStylistUpcomingBkForDate(id, bookingDate,sortBy,sortDir);

                tblSchedule.CssClass = "table table-light table-hover";

                //create row for the table 
                TableRow row = new TableRow();
                row.Height = 50;

                //add row to the table
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date<br/>(d/M/Y)";
                date.Width = 300;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Start Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                time = new TableCell();
                time.Text = "End Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 300;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 300;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                if (cookie["UT"] != "S")
                {
                    TableCell edit = new TableCell();
                    edit.Width = 200;
                    tblSchedule.Rows[0].Cells.Add(edit);
                }

                TableCell empty = new TableCell();
                empty.Width = 150;
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

                    getTimeCustomerServices(b.BookingID, b.PrimaryID, rowCount, b);


                    if (cookie["UT"] != "S")
                    {
                        //edit
                        TableCell editButton = new TableCell();
                        editButton.Text =
                            "<button type = 'button' class='btn btn-default'>" +
                        "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                        "&Action=Edit'>Edit Booking</a></button>";
                        tblSchedule.Rows[rowCount].Cells.Add(editButton);
                    }

                    //view booking
                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);

                    rowCount++;
                }
            }
            catch (Exception E)
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
            cookie = Request.Cookies["CheveuxUserID"];

            tblSchedule.Rows.Clear();
            try
            {
                bList = handler.getStylistUpcomingBookingsDR(empID, startDate, endDate,sortBy,sortDir);

                ////phTable.Visible=true;
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date<br/>(d/M/Y)";
                date.Width = 300;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Start Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                time = new TableCell();
                time.Text = "End Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 300;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 300;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);


                if (cookie["UT"] != "S")
                {
                    TableCell edit = new TableCell();
                    edit.Width = 200;
                    tblSchedule.Rows[0].Cells.Add(edit);
                }

                TableCell empty = new TableCell();
                empty.Width = 150;
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

                    getTimeCustomerServices(b.BookingID, b.PrimaryID, rowCount, b);

                    if (cookie["UT"] != "S")
                    {
                        //edit
                        TableCell editButton = new TableCell();
                        editButton.Text =
                            "<button type = 'button' class='btn btn-default'>" +
                        "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                        "&Action=Edit'>Edit Booking</a></button>";
                        tblSchedule.Rows[rowCount].Cells.Add(editButton);
                    }

                    //view booking
                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);

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

        #endregion

        #region All Stylists Bookings 

        #region Upcoming
        public void getAllStylistsUpcomingBksForDate(DateTime bookingDate, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            try
            {
                bList = handler.getAllStylistsUpcomingBksForDate(bookingDate,sortBy,sortDir);

                ////phTable.Visible=true;
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date<br/>(d/M/Y)";
                date.Width = 300;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Start Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                time = new TableCell();
                time.Text = "End Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell emp = new TableCell();
                emp.Text = "Employee";
                emp.Width = 280;
                emp.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(emp);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 300;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 300;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 150;
                tblSchedule.Rows[0].Cells.Add(empty);

                empty = new TableCell();
                empty.Width = 150;
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

                    getTimeCustomerServices(b.BookingID, b.PrimaryID, rowCount, b);

                    //edit
                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                        "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&Action=Edit'>Edit Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);

                    //view booking
                    buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);

                    rowCount++;
                }
            }
            catch (Exception Err)
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
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date<br/>(d/M/Y)";
                date.Width = 300;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Start Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                time = new TableCell();
                time.Text = "End Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell emp = new TableCell();
                emp.Text = "Employee";
                emp.Width = 280;
                emp.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(emp);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 300;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 300;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 150;
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

                    getTimeCustomerServices(b.BookingID, b.PrimaryID, rowCount, b);

                    //edit
                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                        "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&Action=Edit'>Edit Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);

                    //view booking
                    buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);
                    rowCount++;
                }
            }
            catch (Exception Err)
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
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date<br/>(d/M/Y)";
                date.Width = 300;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Start Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                time = new TableCell();
                time.Text = "End Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell emp = new TableCell();
                emp.Text = "Employee";
                emp.Width = 280;
                emp.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(emp);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 300;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 300;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 150;
                tblSchedule.Rows[0].Cells.Add(empty);

                empty = new TableCell();
                empty.Width = 150;
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

                    getTimeCustomerServices(b.BookingID, b.PrimaryID, rowCount, b);

                    //edit
                    TableCell buttonCell = new TableCell();
                    buttonCell.Text =
                        "<button type='button' class='btn btn-default'>" +
                    "<a href='../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "&Action=Edit'>Edit Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);

                    //view booking
                    buttonCell = new TableCell();
                    buttonCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + b.BookingID.ToString().Replace(" ", string.Empty) +
                    "'>View Booking</a></button>";
                    tblSchedule.Rows[rowCount].Cells.Add(buttonCell);

                    rowCount++;
                }
            }
            catch (Exception Err)
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
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date<br/>(d/M/Y) ";
                date.Width = 300;
                date.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Start Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                time = new TableCell();
                time.Text = "End Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell emp = new TableCell();
                emp.Text = "Employee";
                emp.Width = 280;
                emp.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(emp);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 300;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 300;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 150;
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

                    getTimeCustomerServices(b.BookingID, b.PrimaryID, rowCount, b);

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
            catch (Exception Err)
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
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell dateC = new TableCell();
                dateC.Text = "Date<br/>(d/M/Y)";
                dateC.Width = 240;
                dateC.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(dateC);

                TableCell time = new TableCell();
                time.Text = "Start Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                time = new TableCell();
                time.Text = "End Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell emp = new TableCell();
                emp.Text = "Employee";
                emp.Width = 280;
                emp.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(emp);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 300;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 300;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 150;
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

                    getTimeCustomerServices(b.BookingID, b.PrimaryID, rowCount, b);

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
            catch (Exception Err)
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
                tblSchedule.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblSchedule.Rows.Add(row);

                TableCell dateC = new TableCell();
                dateC.Text = "Date<br/>(d/M/Y)";
                dateC.Width = 240;
                dateC.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(dateC);

                TableCell time = new TableCell();
                time.Text = "Start Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                time = new TableCell();
                time.Text = "End Time";
                time.Width = 90;
                time.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(time);

                TableCell emp = new TableCell();
                emp.Text = "Employee";
                emp.Width = 280;
                emp.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(emp);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 300;
                customer.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 300;
                svName.Font.Bold = true;
                tblSchedule.Rows[0].Cells.Add(svName);

                TableCell empty = new TableCell();
                empty.Width = 150;
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

                    getTimeCustomerServices(b.BookingID, b.PrimaryID, rowCount, b);

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
            catch (Exception Err)
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
        public void getTimeCustomerServices(string aBookingID, string primaryBookingID, int i, SP_GetStylistBookings a)
        {
            #region Time

            TableCell start = new TableCell();
            start.Width = 200;
            TableCell end = new TableCell();
            end.Width = 200;

            try
            {
                try
                {
                    bServices = handler.getBookingServices(a.BookingID.ToString());
                }
                catch (Exception serviceErr)
                {
                    function.logAnError("Error getting services [appointments.aspx {tryCatch within getTime  method }]err:" + serviceErr.ToString());
                }

                if (bServices.Count == 1)
                {
                    start.Text = a.StartTime.ToString("HH:mm");
                    tblSchedule.Rows[i].Cells.Add(start);

                    end.Text = a.EndTime.ToString("HH:mm");
                    tblSchedule.Rows[i].Cells.Add(end);
                }
                else if (bServices.Count >= 2)
                {
                    time = handler.getMultipleServicesTime(primaryBookingID);

                    start.Text = time.StartTime.ToString("HH:mm");
                    tblSchedule.Rows[i].Cells.Add(start);

                    end.Text = time.EndTime.ToString("HH:mm");
                    tblSchedule.Rows[i].Cells.Add(end);
                }

            }
            catch (Exception Err)
            {
                //If time isn't retrieved (Error)
                start.Text = "---";
                tblSchedule.Rows[i].Cells.Add(start);
                end.Text = "---";
                tblSchedule.Rows[i].Cells.Add(end);
                function.logAnError("Couldn't get the time (check db for 2nd bkID) [appointments.aspx "
                    +"{getTimeCustomerServices?getTime}] error:"
                                            + Err.ToString());
            }
            #endregion
            #region Stylist
            if (empSelectionType.SelectedValue == "0")
            {
                TableCell empCell = new TableCell();
                try
                {
                    empCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + a.StylistID.ToString().Replace(" ", string.Empty) +
                                    "'>" + a.StylistName.ToString() + "</a>";
                    tblSchedule.Rows[i].Cells.Add(empCell);
                }
                catch(Exception Err)
                {
                    empCell.Text = "-------";
                    tblSchedule.Rows[i].Cells.Add(empCell);
                    function.logAnError("Couldnt get stylist name[appointments.aspx {getT/c/s method}]err:"+Err.ToString());
                }
            }
            #endregion
            #region Customer
            TableCell c = new TableCell();
            try
            {
                c.Width = 300;
                c.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + a.CustomerID.ToString().Replace(" ", string.Empty) +
                                "'>" + a.FullName.ToString() + "</a>";
                tblSchedule.Rows[i].Cells.Add(c);
            }
            catch(Exception Err)
            {
                c.Width = 300;
                c.Text = "----------";
                tblSchedule.Rows[i].Cells.Add(c);
                function.logAnError("Couldnt get customer name[appointments.aspx {getT/c/s method}]err:" + Err.ToString());
            }
            #endregion
            #region Services

            TableCell services = new TableCell();
            services.Width = 300;

            try
            {
                bServices = handler.getBookingServices(a.BookingID.ToString());
                if (bServices.Count == 1)
                {
                    services.Text = "<a href='ViewProduct.aspx?ProductID=" + bServices[0].ServiceID.Replace(" ", string.Empty) + "'>"
                    + bServices[0].ServiceName.ToString() + "</a>";
                }
                else if (bServices.Count == 2)
                {
                    services.Text = "<a href='../ViewBooking.aspx?BookingID=" + a.BookingID.ToString().Replace(" ", string.Empty) +
                        "'>" + bServices[0].ServiceName.ToString() +
                        ", " + bServices[1].ServiceName.ToString() + "</a>";
                }
                else if (bServices.Count > 2)
                {
                    string toolTip = "";
                    int toolTipCount = 0;
                    foreach (SP_GetBookingServices toolTipDTL in bServices)
                    {
                        if (toolTipCount == 0)
                        {
                            toolTip = toolTipDTL.ServiceName;
                            toolTipCount++;
                        }
                        else
                        {
                            toolTip += ", " + toolTipDTL.ServiceName;
                        }
                    }
                    services.Text = "<a title='" + toolTip + "'" +
                        "href='../ViewBooking.aspx?BookingID=" + a.BookingID.ToString().Replace(" ", string.Empty) +
                        "'> Multiple Services </a>";
                }
                tblSchedule.Rows[i].Cells.Add(services);
            }
            catch (Exception Err)
            {
                //if theres an error or cant retrieve the services from the database 
                services.Text = "Unable to retreive services";
                tblSchedule.Rows[i].Cells.Add(services);
                function.logAnError("Couldn't get the services [appointments.aspx "
                    +"{getTimeCustomerServices?getServices} ] error:" + Err.ToString());
            }


            #endregion
        }
        protected void calDay_SelectionChanged(object sender, EventArgs e)
        {
            lblDay.Text = calDay.SelectedDate.ToString("dd-MM-yyyy");
            string action = Request.QueryString["Action"];
            if (cookie["UT"] == "R" || (cookie["UT"] == "M" && action == "ViewAllSchedules"))
            {
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
                    else if (empSelectionType.SelectedValue == "1")//stylist
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
                    else if (empSelectionType.SelectedValue == "1")//stylist
                    {
                        if (lblDay.Text == string.Empty)
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
                }
            }
            else if (cookie["UT"] == "S")
            {
                string stylistID = cookie["ID"];

                if(drpViewAppt.SelectedValue == "0")//upcoming bookings
                {
                    if (lblDay.Text == string.Empty)
                    {
                        phBookingsErr.Visible = true;
                        lblBookingsErr.Text = "Please select a date.";
                    }
                    else
                    {
                        phBookingsErr.Visible = false;
                        getStylistUpcomingBksForDate(stylistID, calDay.SelectedDate,
                                                    drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                }
                else if(drpViewAppt.SelectedValue == "1")//past bookings
                {
                    if (lblDay.Text == string.Empty)
                    {
                        phBookingsErr.Visible = true;
                        lblBookingsErr.Text = "Please select a date.";
                    }
                    else
                    {
                        phBookingsErr.Visible = false;
                        getStylistPastBksForDate(stylistID, calDay.SelectedDate,
                                                drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                }
            }
            else if(cookie["UT"] == "M" && action == "ViewStylistSchedule")
            {
                string stylistID = Request.QueryString["empID"];
                if (drpViewAppt.SelectedValue == "0")//upcoming bookings
                {
                    if (lblDay.Text == string.Empty)
                    {
                        phBookingsErr.Visible = true;
                        lblBookingsErr.Text = "Please select a date.";
                    }
                    else
                    {
                        phBookingsErr.Visible = false;
                        getStylistUpcomingBksForDate(stylistID, calDay.SelectedDate,
                                                    drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                }
                else if (drpViewAppt.SelectedValue == "1")//past bookings
                {
                    if (lblDay.Text == string.Empty)
                    {
                        phBookingsErr.Visible = true;
                        lblBookingsErr.Text = "Please select a date.";
                    }
                    else
                    {
                        phBookingsErr.Visible = false;
                        getStylistPastBksForDate(stylistID, calDay.SelectedDate,
                                                drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                }
            }
        }

        protected void calStart_SelectionChanged(object sender, EventArgs e)
        {
            lblStart.Text = calStart.SelectedDate.ToString("dd-MM-yyyy");

            string action = Request.QueryString["Action"];

            checkValidDate(calStart.SelectedDate, calEnd.SelectedDate);


            if (cookie["UT"] == "R" || (cookie["UT"] == "M" && action == "ViewAllSchedules"))
            {
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
                                                    + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
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
                                                    + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
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
                                                    + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
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
                                                    + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
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
            else if(cookie["UT"] == "S")
            {
                string stylistID = cookie["ID"];
                if (drpViewAppt.SelectedValue == "0")//upcoming bookings
                {
                    if (rdoDate.SelectedValue == "3")//date range
                    {
                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getStylistUpcomingBookingsDR(stylistID, calStart.SelectedDate, calEnd.SelectedDate,
                                                        drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }
                }
                else if(drpViewAppt.SelectedValue == "1")//past bookings
                {
                    if (rdoDate.SelectedValue == "3")
                    {
                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getStylistPastBookingsDateRange(stylistID, calStart.SelectedDate, calEnd.SelectedDate,
                                                        drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }
                }
            }
            else if(cookie["UT"] == "M" && action == "ViewStylistSchedule")
            {
                string stylistID = Request.QueryString["empID"];
                if (drpViewAppt.SelectedValue == "0")//upcoming bookings
                {
                    if (rdoDate.SelectedValue == "3")//date range
                    {
                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getStylistUpcomingBookingsDR(stylistID, calStart.SelectedDate, calEnd.SelectedDate,
                                                        drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }
                }
                else if (drpViewAppt.SelectedValue == "1")//past bookings
                {
                    if (rdoDate.SelectedValue == "3")
                    {
                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getStylistPastBookingsDateRange(stylistID, calStart.SelectedDate, calEnd.SelectedDate,
                                                        drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }
                }
            }

            
        }

        protected void calEnd_SelectionChanged(object sender, EventArgs e)
        {
            lblEnd.Text = calEnd.SelectedDate.ToString("dd-MM-yyyy");

            string action = Request.QueryString["Action"];

            checkValidDate(calStart.SelectedDate,calEnd.SelectedDate);

            if (cookie["UT"] == "R" || (cookie["UT"] == "M" && action == "ViewAllSchedules"))
            {
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
                                                    + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
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
                                                    + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
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
                                                    + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
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
                                                    + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
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
            else if(cookie["UT"] == "S")
            {
                string stylistID = cookie["ID"];
                if (drpViewAppt.SelectedValue == "0")//upcoming bookings
                {
                    if (rdoDate.SelectedValue == "3")
                    {
                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getStylistUpcomingBookingsDR(stylistID, calStart.SelectedDate, calEnd.SelectedDate,
                                                    drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }
                }
                else if (drpViewAppt.SelectedValue == "1")//past bookings
                {
                    if (rdoDate.SelectedValue == "3")
                    {
                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getStylistPastBookingsDateRange(stylistID, calStart.SelectedDate, calEnd.SelectedDate,
                                                        drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }
                }
            }
            else if(cookie["UT"] == "M" && action == "ViewStylistSchedule")
            {
                string stylistID = cookie["ID"];
                if (drpViewAppt.SelectedValue == "0")//upcoming bookings
                {
                    if (rdoDate.SelectedValue == "3")
                    {
                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getStylistUpcomingBookingsDR(stylistID, calStart.SelectedDate, calEnd.SelectedDate,
                                                    drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                    }
                }
                else if (drpViewAppt.SelectedValue == "1")//past bookings
                {
                    if (rdoDate.SelectedValue == "3")
                    {
                        if (lblStart.Text == string.Empty || lblEnd.Text == string.Empty)
                        {
                            phBookingsErr.Visible = true;
                            lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                                + "e.g. View bookings between 1/1/19(start date) and 12/06/19(end date)";
                        }
                        else
                        {
                            phBookingsErr.Visible = false;
                            getStylistPastBookingsDateRange(stylistID, calStart.SelectedDate, calEnd.SelectedDate,
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

        protected void empSelectionType_Changed(object sender, EventArgs e)
        {
            if (empSelectionType.SelectedValue == "0")
            {
                drpSortBy.Items.RemoveAt(1);
                drpSortBy.Items.Add("Stylist");
            }
            else if (empSelectionType.SelectedValue == "1")
            {
                drpSortBy.Items.RemoveAt(1);
                drpSortBy.Items.Add("Customer");
            }
        }
        public void checkValidDate(DateTime date1, DateTime date2)
        {
            if( date1 == null || date2 == null)
            {
                valDate.Visible = false;
            }
            else if(date1 > date2)
            {
                valDate.ForeColor = System.Drawing.Color.Red;
                valDate.Text = "*Please ensure that Start Date is smaller than End Date";
                valDate.Visible = true;
            }
            else if(date1 == date2)
            {
                valDate.ForeColor = System.Drawing.Color.Red;
                valDate.Visible = true;
                valDate.Text = "(Tip: In the future select 'Specific day' radio button for bookings of a specific days bookings)";
            }
            else
            {
                valDate.Visible = false;
            }
        }
    }
}
