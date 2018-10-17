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
        SP_GetBookingServices leaveServices = null;
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
            filterMonths();

            btnPrint.Visible = false;
            errorCssStyles();
            DateTime d = DateTime.Today;

            filterMonths();
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
                Header.Text = "View Schedules";
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
                Header.Text = "View Schedule";

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
                    Header.Text = "View Schedules";
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
                else if (action == "LeaveSchedule")
                {
                    Header.Text = "Leave Schedule";
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

                    if (!IsPostBack)
                    {
                        drpViewAppt.Items.Clear();
                        drpViewAppt.Items.Add(new ListItem("Upcoming Leave", "0"));
                        drpViewAppt.Items.Add(new ListItem("Past Leave", "1"));
                    }

                    DropDownChanges(cookie["UT"].ToString());
                }
            }
        }

        public void filterMonths()
        {
            DateTime now = DateTime.Now;
            int currentMonth = now.Month;
            if (drpViewAppt.SelectedValue == "0")//upcoming
            {
                foreach (ListItem li in drpStartMonth.Items)
                {
                    if (int.Parse(li.Value) < currentMonth)
                    {
                        li.Enabled = false;
                    }
                    else
                    {
                        li.Enabled = true;
                    }
                }

                foreach(ListItem li in drpEndMonth.Items)
                {
                    if (int.Parse(li.Value) < currentMonth)
                    {
                        li.Enabled = false;
                    }
                    else
                    {
                        li.Enabled = true;
                    }
                }
            }
            else if (drpViewAppt.SelectedValue == "1")//past
            {
                foreach(ListItem li in drpStartMonth.Items)
                {
                    if(int.Parse(li.Value) > currentMonth)
                    {
                        li.Enabled = false;
                    }
                    else
                    {
                        li.Enabled = true;
                    }
                }

                foreach (ListItem li in drpEndMonth.Items)
                {
                    if (int.Parse(li.Value) > currentMonth)
                    {
                        li.Enabled = false;
                    }
                    else
                    {
                        li.Enabled = true;
                    }
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

            if (userType == "R" || (userType =="M" && (action== "ViewAllSchedules" || action== "LeaveSchedule")))
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
                            drpStartMonth.Visible = false;
                            drpEndMonth.Visible = false;
                            lblStartM.Visible = false;
                            lblEndM.Visible = false;

                            getAllStylistsUpcomingBookings(drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                        else if (rdoDate.SelectedValue == "1")
                        {
                            //today
                            phBookingsErr.Visible = false;
                            phDay.Visible = false;
                            phDateRange.Visible = false;
                            drpStartMonth.Visible = false;
                            drpEndMonth.Visible = false;
                            lblStartM.Visible = false;
                            lblEndM.Visible = false;

                            getAllStylistsUpcomingBksForDate(DateTime.Parse(today), drpSortBy.SelectedItem.Text,
                                                            drpSortDir.SelectedItem.Text);
                        }
                        else if (rdoDate.SelectedValue == "2")
                        {
                            //specific day
                            phDay.Visible = true;
                            phDateRange.Visible = false;
                            drpStartMonth.Visible = true;
                            drpEndMonth.Visible = false;
                            lblStartM.Visible = true;
                            lblEndM.Visible = false;

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
                            drpEndMonth.Visible = true;
                            drpStartMonth.Visible = true;
                            lblStartM.Visible = true;
                            lblEndM.Visible = true;

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
                                drpStartMonth.Visible = false;
                                drpEndMonth.Visible = false;
                                lblStartM.Visible = false;
                                lblEndM.Visible = false;

                                getStylistUpcomingBookings(drpStylistNames.SelectedValue, drpSortBy.SelectedItem.Text,
                                                            drpSortDir.SelectedItem.Text);
                            }
                            else if (rdoDate.SelectedValue == "1")
                            {
                                //today
                                phBookingsErr.Visible = false;
                                phDateRange.Visible = false;
                                phDay.Visible = false;
                                drpStartMonth.Visible = false;
                                drpEndMonth.Visible = false;
                                lblStartM.Visible = false;
                                lblEndM.Visible = false;

                                getStylistUpcomingBksForDate(drpStylistNames.SelectedValue, DateTime.Parse(today),
                                                            drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                            }
                            else if (rdoDate.SelectedValue == "2")
                            {
                                //specific day 
                                phDay.Visible = true;
                                phDateRange.Visible = false;
                                drpStartMonth.Visible = true;
                                drpEndMonth.Visible = false;
                                lblStartM.Visible = true;
                                lblEnd.Visible = false;

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
                                drpStartMonth.Visible = true;
                                drpEndMonth.Visible = true;
                                lblStartM.Visible = true;
                                lblEndM.Visible = true;

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
                        drpStartMonth.Visible = false;
                        drpEndMonth.Visible = false;

                        if (rdoDate.SelectedValue == "0")
                        {
                            //all
                            phBookingsErr.Visible = false;
                            phDateRange.Visible = false;
                            phDay.Visible = false;
                            lblStartM.Visible = false;
                            lblEndM.Visible = false;

                            getAllStylistsPastBookings(drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                        else if (rdoDate.SelectedValue == "1")
                        {
                            //today
                            phBookingsErr.Visible = false;
                            phDay.Visible = false;
                            phDateRange.Visible = false;
                            drpStartMonth.Visible = false;
                            drpEndMonth.Visible = false;
                            lblStartM.Visible = false;
                            lblEndM.Visible = false;

                            getAllStylistsPastBksForDate(DateTime.Parse(today)
                                                        , drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                        }
                        else if (rdoDate.SelectedValue == "2")
                        {
                            //Specific Day
                            phDay.Visible = true;
                            phDateRange.Visible = false;
                            drpStartMonth.Visible = true;
                            drpEndMonth.Visible = false;
                            lblStartM.Visible = true;
                            lblEndM.Visible = false;

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
                            drpStartMonth.Visible = true;
                            drpEndMonth.Visible = true;
                            lblStartM.Visible = true;
                            lblEndM.Visible = true;

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
                                //all
                                phBookingsErr.Visible = false;
                                phDateRange.Visible = false;
                                phDay.Visible = false;
                                drpStartMonth.Visible = false;
                                drpEndMonth.Visible = false;
                                lblStartM.Visible = false;
                                lblEndM.Visible = false;

                                getStylistPastBookings(drpStylistNames.SelectedValue, drpSortBy.SelectedItem.Text,
                                                                    drpSortDir.SelectedItem.Text);
                            }
                            else if (rdoDate.SelectedValue == "1")
                            {
                                //today
                                phBookingsErr.Visible = false;
                                phDay.Visible = false;
                                phDateRange.Visible = false;
                                drpStartMonth.Visible = false;
                                drpEndMonth.Visible = false;
                                lblStartM.Visible = false;
                                lblEndM.Visible = false;

                                getStylistPastBksForDate(drpStylistNames.SelectedValue, DateTime.Parse(today)
                                                        , drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                            }
                            else if (rdoDate.SelectedValue == "2")
                            {
                                //Specific day
                                phDay.Visible = true;
                                phDateRange.Visible = false;
                                drpStartMonth.Visible = true;
                                drpEndMonth.Visible = false;
                                lblStartM.Visible = true;
                                lblEndM.Visible = false;

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
                                drpStartMonth.Visible = true;
                                drpEndMonth.Visible = true;
                                lblStartM.Visible = true;
                                lblEndM.Visible = true;

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
                        //all
                        phBookingsErr.Visible = false;
                        phDateRange.Visible = false;
                        phDay.Visible = false;
                        drpStartMonth.Visible = false;
                        drpEndMonth.Visible = false;
                        lblStartM.Visible = false;
                        lblEndM.Visible = false;

                        getStylistUpcomingBookings(cookie["ID"].ToString(), drpSortBy.SelectedItem.Text,
                                                    drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "1")
                    {
                        //today 
                        phBookingsErr.Visible = false;
                        phDateRange.Visible = false;
                        phDay.Visible = false;
                        drpStartMonth.Visible = false;
                        drpEndMonth.Visible = false;
                        lblStartM.Visible = false;
                        lblEndM.Visible = false;

                        getStylistUpcomingBksForDate(cookie["ID"].ToString(), DateTime.Parse(today),
                                                    drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "2")
                    {
                        //upcoming bookings for a specific day 
                        phDay.Visible = true;
                        phDateRange.Visible = false;
                        drpStartMonth.Visible = true;
                        drpEndMonth.Visible = false;
                        lblStartM.Visible = true;
                        lblEndM.Visible = false;

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
                        drpStartMonth.Visible = true;
                        drpEndMonth.Visible = true;
                        lblStartM.Visible = true;
                        lblEndM.Visible = true;

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
                        drpStartMonth.Visible = false;
                        drpEndMonth.Visible = false;
                        lblStartM.Visible = false;
                        lblEndM.Visible = false;

                        getStylistPastBookings(cookie["ID"].ToString(), drpSortBy.SelectedItem.Text,
                                                            drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "1")
                    {
                        //past bookings for today
                        phBookingsErr.Visible = false;
                        phDay.Visible = false;
                        phDateRange.Visible = false;
                        drpStartMonth.Visible = false;
                        drpEndMonth.Visible = false;
                        lblStartM.Visible = false;
                        lblEndM.Visible = false;

                        getStylistPastBksForDate(cookie["ID"].ToString(), DateTime.Parse(today)
                                                , drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "2")
                    {
                        //Past bookings for a Specific day
                        phDay.Visible = true;
                        phDateRange.Visible = false;
                        drpStartMonth.Visible = true;
                        drpEndMonth.Visible = false;
                        lblStartM.Visible = true;
                        lblEndM.Visible = false;

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
                        drpStartMonth.Visible = true;
                        drpEndMonth.Visible = true;
                        lblStartM.Visible = true;
                        lblEndM.Visible = true;

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
            else if(userType == "M" && (action == "ViewStylistSchedule"||action == "LeaveSchedule"))
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
                        drpStartMonth.Visible = false;
                        drpEndMonth.Visible = false;
                        lblStartM.Visible = false;
                        lblEndM.Visible = false;

                        getStylistUpcomingBookings(stylistID, drpSortBy.SelectedItem.Text,
                                                    drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "1")
                    {
                        //upcoming bookings for today 
                        phBookingsErr.Visible = false;
                        phDateRange.Visible = false;
                        phDay.Visible = false;
                        drpStartMonth.Visible = false;
                        drpEndMonth.Visible = false;
                        lblStartM.Visible = false;
                        lblEndM.Visible = false;

                        getStylistUpcomingBksForDate(stylistID, DateTime.Parse(today),
                                                    drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "2")
                    {
                        //upcoming bookings for a specific day 
                        phDay.Visible = true;
                        phDateRange.Visible = false;
                        drpStartMonth.Visible = true;
                        drpEndMonth.Visible = false;
                        lblStartM.Visible = true;
                        lblEndM.Visible = false;

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
                        drpStartMonth.Visible = true;
                        drpEndMonth.Visible = true;
                        lblStartM.Visible = true;
                        lblEndM.Visible = true;

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
                        drpStartMonth.Visible = false;
                        drpEndMonth.Visible = false;
                        lblStartM.Visible = false;
                        lblEndM.Visible = false;

                        getStylistPastBookings(stylistID, drpSortBy.SelectedItem.Text,
                                                            drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "1")
                    {
                        //past bookings for today
                        phBookingsErr.Visible = false;
                        phDay.Visible = false;
                        phDateRange.Visible = false;
                        drpStartMonth.Visible = false;
                        drpEndMonth.Visible = false;
                        lblStartM.Visible = false;
                        lblEndM.Visible = false;

                        getStylistPastBksForDate(stylistID, DateTime.Parse(today)
                                                , drpSortBy.SelectedItem.Text, drpSortDir.SelectedItem.Text);
                    }
                    else if (rdoDate.SelectedValue == "2")
                    {
                        //Past bookings for a Specific day
                        phDay.Visible = true;
                        phDateRange.Visible = false;
                        drpStartMonth.Visible = true;
                        drpEndMonth.Visible = false;
                        lblStartM.Visible = true;
                        lblEndM.Visible = false;

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
                        drpStartMonth.Visible = true;
                        drpEndMonth.Visible = true;
                        lblStartM.Visible = true;
                        lblEndM.Visible = true;

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
            cookie = Request.Cookies["CheveuxUserID"];
            string action = Request.QueryString["Action"];
            tblSchedule.CssClass = "table table-light table-hover";
            if (cookie["UT"] == "R" || action == "ViewAllSchedules" || action == "ViewStylistSchedule" || cookie["UT"] == "S")
            {
                try
                {
                    bList = handler.getStylistPastBookings(empID, sortBy, sortDir);

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
                    btnPrint.Visible = true;
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
            else if (cookie["UT"] == "M" && action == "LeaveSchedule")
            {
                try
                {
                    bList = handler.getStylistPastBookings(empID, sortBy, sortDir);

                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);

                    TableCell date = new TableCell();
                    date.Text = "Date<br/>(d/M/Y)";
                    date.Width = 200;
                    date.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(date);

                    TableCell reason = new TableCell();
                    reason.Text = "Reason";
                    reason.Width = 200;
                    reason.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(reason);

                    TableCell desc = new TableCell();
                    desc.Text = "Description";
                    desc.Width = 200;
                    desc.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(desc);

                    int rowCount = 1;
                    foreach (SP_GetStylistBookings b in bList)
                    {
                        TableRow r = new TableRow();
                        r.Height = 30;
                        tblSchedule.Rows.Add(r);

                        try
                        {
                            leaveServices = handler.getLeaveReason(b.BookingID.ToString());

                            if (leaveServices.type == "U")
                            {
                                TableCell dateCell = new TableCell();
                                dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                                tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                                TableCell servicesCell = new TableCell();
                                servicesCell.Text = "<a href='../cheveux/services.aspx?ProductID=" + leaveServices.ServiceID.Replace(" ", string.Empty) + "'>"
                                                        + leaveServices.ServiceName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);

                                servicesCell = new TableCell();
                                servicesCell.Text = leaveServices.serviceDescripion.ToString();
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);
                                rowCount++;
                            }
                            else
                            {
                                tblSchedule.Rows.RemoveAt(rowCount);
                            }
                        }
                        catch (Exception err)
                        {
                            function.logAnError("Couldn't get the leave reason and description. Error: " + err.ToString());
                        }

                    }
                    btnPrint.Visible = true;
                }
                catch (Exception err)
                {
                    tblSchedule.Rows.Clear();
                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);
                    TableCell newCell = new TableCell();
                    newCell.Text = "Stylist Leave currently unavailable.Please try again later.";
                    tblSchedule.Rows[0].Cells.Add(newCell);
                    function.logAnError("Error retreiving leave schedule." + err.ToString());
                }
            }

        }
        public void getStylistPastBookingsDateRange(string empID, DateTime startDate, DateTime endDate, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            cookie = Request.Cookies["CheveuxUserID"];
            string action = Request.QueryString["Action"];
            tblSchedule.CssClass = "table table-light table-hover";
            if (cookie["UT"] == "R" || action == "ViewAllSchedules" || action == "ViewStylistSchedule" || cookie["UT"] == "S")
            {
                try
                {
                    bList = handler.getStylistPastBookingsDateRange(empID, startDate, endDate, sortBy, sortDir);

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
            else if (cookie["UT"] == "M" && action == "LeaveSchedule")
            {
                try
                {
                    bList = handler.getStylistPastBookingsDateRange(empID, startDate, endDate, sortBy, sortDir);

                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);

                    TableCell date = new TableCell();
                    date.Text = "Date<br/>(d/M/Y)";
                    date.Width = 200;
                    date.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(date);

                    TableCell reason = new TableCell();
                    reason.Text = "Reason";
                    reason.Width = 200;
                    reason.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(reason);

                    TableCell desc = new TableCell();
                    desc.Text = "Description";
                    desc.Width = 200;
                    desc.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(desc);

                    int rowCount = 1;
                    foreach (SP_GetStylistBookings b in bList)
                    {
                        TableRow r = new TableRow();
                        r.Height = 30;
                        tblSchedule.Rows.Add(r);

                        try
                        {
                            leaveServices = handler.getLeaveReason(b.BookingID.ToString());

                            if (leaveServices.type == "U")
                            {
                                TableCell dateCell = new TableCell();
                                dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                                tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                                TableCell servicesCell = new TableCell();
                                servicesCell.Text = "<a href='../cheveux/services.aspx?ProductID=" + leaveServices.ServiceID.Replace(" ", string.Empty) + "'>"
                                                        + leaveServices.ServiceName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);

                                servicesCell = new TableCell();
                                servicesCell.Text = leaveServices.serviceDescripion.ToString();
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);
                                rowCount++;
                            }
                            else
                            {
                                tblSchedule.Rows.RemoveAt(rowCount);
                            }
                        }
                        catch (Exception err)
                        {
                            function.logAnError("Couldn't get the leave reason and description. Error: " + err.ToString());
                        }

                    }
                    btnPrint.Visible = true;
                }
                catch (Exception err)
                {
                    tblSchedule.Rows.Clear();
                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);
                    TableCell newCell = new TableCell();
                    newCell.Text = "Stylist Leave currently unavailable.Please try again later.";
                    tblSchedule.Rows[0].Cells.Add(newCell);
                    function.logAnError("Error retreiving leave schedule." + err.ToString());
                }
            }

        }
        public void getStylistPastBksForDate(string empID, DateTime day, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            cookie = Request.Cookies["CheveuxUserID"];
            string action = Request.QueryString["Action"];
            tblSchedule.CssClass = "table table-light table-hover";

            if (cookie["UT"] == "R" || action == "ViewAllSchedules" || action == "ViewStylistSchedule" || cookie["UT"] == "S")
            {
                try
                {
                    bList = handler.getStylistPastBksForDate(empID, day, sortBy, sortDir);

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
                    btnPrint.Visible = true;
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
            else if (cookie["UT"] == "M" && action == "LeaveSchedule")
            {
                try
                {
                    bList = handler.getStylistPastBksForDate(empID, day, sortBy, sortDir);

                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);

                    TableCell date = new TableCell();
                    date.Text = "Date<br/>(d/M/Y)";
                    date.Width = 200;
                    date.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(date);

                    TableCell reason = new TableCell();
                    reason.Text = "Reason";
                    reason.Width = 200;
                    reason.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(reason);

                    TableCell desc = new TableCell();
                    desc.Text = "Description";
                    desc.Width = 200;
                    desc.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(desc);

                    int rowCount = 1;
                    foreach (SP_GetStylistBookings b in bList)
                    {
                        TableRow r = new TableRow();
                        r.Height = 30;
                        tblSchedule.Rows.Add(r);

                        try
                        {
                            leaveServices = handler.getLeaveReason(b.BookingID.ToString());

                            if (leaveServices.type == "U")
                            {
                                TableCell dateCell = new TableCell();
                                dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                                tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                                TableCell servicesCell = new TableCell();
                                servicesCell.Text = "<a href='../cheveux/services.aspx?ProductID=" + leaveServices.ServiceID.Replace(" ", string.Empty) + "'>"
                                                        + leaveServices.ServiceName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);

                                servicesCell = new TableCell();
                                servicesCell.Text = leaveServices.serviceDescripion.ToString();
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);
                                rowCount++;
                            }
                            else
                            {
                                tblSchedule.Rows.RemoveAt(rowCount);
                            }
                        }
                        catch (Exception err)
                        {
                            function.logAnError("Couldn't get the leave reason and description. Error: " + err.ToString());
                        }

                    }
                    btnPrint.Visible = true;
                }
                catch (Exception err)
                {
                    tblSchedule.Rows.Clear();
                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);
                    TableCell newCell = new TableCell();
                    newCell.Text = "Stylist Leave currently unavailable.Please try again later.";
                    tblSchedule.Rows[0].Cells.Add(newCell);
                    function.logAnError("Error retreiving leave schedule." + err.ToString());
                }
            }

        }
        #endregion

        #region Upcoming
        public void getStylistUpcomingBookings(string empID, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            cookie = Request.Cookies["CheveuxUserID"];
            string action = Request.QueryString["Action"];
            tblSchedule.CssClass = "table table-light table-hover";
            if (cookie["UT"] == "R" || action == "ViewAllSchedules" || action == "ViewStylistSchedule" || cookie["UT"] == "S")
            {
                try
                {
                    ////phTable.Visible=true;

                    bList = handler.getStylistUpcomingBookings(empID, sortBy, sortDir);

                    tblSchedule.Visible = true;

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
                    btnPrint.Visible = true;
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
            else if (cookie["UT"] == "M" && action == "LeaveSchedule")
            {
                try
                {
                    bList = handler.getStylistUpcomingBookings(empID, sortBy, sortDir);

                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);

                    TableCell date = new TableCell();
                    date.Text = "Date<br/>(d/M/Y)";
                    date.Width = 200;
                    date.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(date);

                    TableCell reason = new TableCell();
                    reason.Text = "Reason";
                    reason.Width = 200;
                    reason.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(reason);

                    TableCell desc = new TableCell();
                    desc.Text = "Description";
                    desc.Width = 200;
                    desc.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(desc);

                    int rowCount = 1;
                    foreach (SP_GetStylistBookings b in bList)
                    {
                        TableRow r = new TableRow();
                        r.Height = 30 ;
                        tblSchedule.Rows.Add(r);

                        try
                        {
                            leaveServices = handler.getLeaveReason(b.BookingID.ToString());

                            if (leaveServices.type == "U")
                            {
                                TableCell dateCell = new TableCell();
                                dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                                tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                                TableCell servicesCell = new TableCell();
                                servicesCell.Text = "<a href='../cheveux/services.aspx?ProductID=" + leaveServices.ServiceID.Replace(" ", string.Empty) + "'>"
                                                        + leaveServices.ServiceName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);

                                servicesCell = new TableCell();
                                servicesCell.Text = leaveServices.serviceDescripion.ToString();
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);
                                rowCount++;
                            }
                            else
                            {
                                tblSchedule.Rows.RemoveAt(rowCount);
                            }
                        }
                        catch(Exception err)
                        {
                            function.logAnError("Couldn't get the leave reason and description. Error: " + err.ToString());
                        }
                        
                    }
                    btnPrint.Visible = true;
                }
                catch(Exception err)
                {
                    tblSchedule.Rows.Clear();
                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);
                    TableCell newCell = new TableCell();
                    newCell.Text = "Stylist Leave currently unavailable.Please try again later.";
                    tblSchedule.Rows[0].Cells.Add(newCell);
                    function.logAnError("Error retreiving leave schedule."+err.ToString());
                }
            }
            
        }

        public void getStylistUpcomingBksForDate(string id, DateTime bookingDate, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            cookie = Request.Cookies["CheveuxUserID"];
            string action = Request.QueryString["Action"];
            tblSchedule.CssClass = "table table-light table-hover";

            if (cookie["UT"] == "R" || action == "ViewAllSchedules" || action == "ViewStylistSchedule" || cookie["UT"] == "S")
            {
                try
                {
                    bList = handler.getStylistUpcomingBkForDate(id, bookingDate, sortBy, sortDir);

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
                    btnPrint.Visible = true;
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
            else if (cookie["UT"] == "M" && action == "LeaveSchedule")
            {
                try
                {
                    bList = handler.getStylistUpcomingBkForDate(id, bookingDate, sortBy, sortDir);

                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);

                    TableCell date = new TableCell();
                    date.Text = "Date<br/>(d/M/Y)";
                    date.Width = 200;
                    date.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(date);

                    TableCell reason = new TableCell();
                    reason.Text = "Reason";
                    reason.Width = 200;
                    reason.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(reason);

                    TableCell desc = new TableCell();
                    desc.Text = "Description";
                    desc.Width = 200;
                    desc.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(desc);

                    int rowCount = 1;
                    foreach (SP_GetStylistBookings b in bList)
                    {
                        TableRow r = new TableRow();
                        r.Height = 30;
                        tblSchedule.Rows.Add(r);

                        try
                        {
                            leaveServices = handler.getLeaveReason(b.BookingID.ToString());

                            if (leaveServices.type == "U")
                            {
                                TableCell dateCell = new TableCell();
                                dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                                tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                                TableCell servicesCell = new TableCell();
                                servicesCell.Text = "<a href='../cheveux/services.aspx?ProductID=" + leaveServices.ServiceID.Replace(" ", string.Empty) + "'>"
                                                        + leaveServices.ServiceName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);

                                servicesCell = new TableCell();
                                servicesCell.Text = leaveServices.serviceDescripion.ToString();
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);
                                rowCount++;
                            }
                            else
                            {
                                tblSchedule.Rows.RemoveAt(rowCount);
                            }
                        }
                        catch (Exception err)
                        {
                            function.logAnError("Couldn't get the leave reason and description. Error: " + err.ToString());
                        }

                    }
                    btnPrint.Visible = true;
                }
                catch (Exception err)
                {
                    tblSchedule.Rows.Clear();
                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);
                    TableCell newCell = new TableCell();
                    newCell.Text = "Upcoming stylist leave for selected date currently unavailable.Please try again later.";
                    tblSchedule.Rows[0].Cells.Add(newCell);
                    function.logAnError("Error retreiving leave schedule.(allUpcomingBookingsFrDate) Error:" + err.ToString());
                }
            }
 
        }

        public void getStylistUpcomingBookingsDR(string empID, DateTime startDate, DateTime endDate, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            cookie = Request.Cookies["CheveuxUserID"];
            string action = Request.QueryString["Action"];
            tblSchedule.CssClass = "table table-light table-hover";

            if (cookie["UT"] == "R" || action == "ViewAllSchedules" || action == "ViewStylistSchedule" || cookie["UT"] == "S")
            {
                try
                {
                    bList = handler.getStylistUpcomingBookingsDR(empID, startDate, endDate, sortBy, sortDir);

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
                    btnPrint.Visible = true;
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
            else if (cookie["UT"] == "M" && action == "LeaveSchedule")
            {
                
                try
                {
                    bList = handler.getStylistUpcomingBookingsDR(empID, startDate, endDate, sortBy, sortDir);

                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);

                    TableCell date = new TableCell();
                    date.Text = "Date<br/>(d/M/Y)";
                    date.Width = 200;
                    date.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(date);

                    TableCell reason = new TableCell();
                    reason.Text = "Reason";
                    reason.Width = 200;
                    reason.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(reason);

                    TableCell desc = new TableCell();
                    desc.Text = "Description";
                    desc.Width = 200;
                    desc.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(desc);

                    int rowCount = 1;
                    foreach (SP_GetStylistBookings b in bList)
                    {
                        TableRow r = new TableRow();
                        r.Height = 30;
                        tblSchedule.Rows.Add(r);

                        try
                        {
                            leaveServices = handler.getLeaveReason(b.BookingID.ToString());

                            if (leaveServices.type == "U")
                            {
                                TableCell dateCell = new TableCell();
                                dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                                tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                                TableCell servicesCell = new TableCell();
                                servicesCell.Text = "<a href='../cheveux/services.aspx?ProductID=" + leaveServices.ServiceID.Replace(" ", string.Empty) + "'>"
                                                        + leaveServices.ServiceName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);

                                servicesCell = new TableCell();
                                servicesCell.Text = leaveServices.serviceDescripion.ToString();
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);
                                rowCount++;
                            }
                            else
                            {
                                tblSchedule.Rows.RemoveAt(rowCount);
                            }
                        }
                        catch (Exception err)
                        {
                            function.logAnError("Couldn't get the leave reason and description. Error: " + err.ToString());
                        }

                    }
                    btnPrint.Visible = true;
                }
                catch (Exception err)
                {
                    tblSchedule.Rows.Clear();
                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);
                    TableCell newCell = new TableCell();
                    newCell.Text = "Upcoming stylist leave for selected date currently unavailable.Please try again later.";
                    tblSchedule.Rows[0].Cells.Add(newCell);
                    function.logAnError("Error retreiving leave schedule.(allUpcomingBookingsFrDate) Error:" + err.ToString());
                }
            }

        }
        #endregion

        #endregion


        #region All Stylists Bookings 

        #region Upcoming
        public void getAllStylistsUpcomingBksForDate(DateTime bookingDate, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            cookie = Request.Cookies["CheveuxUserID"];
            string action = Request.QueryString["Action"];
            tblSchedule.CssClass = "table table-light table-hover";

            if (cookie["UT"] == "R" || action == "ViewAllSchedules" || action == "ViewStylistSchedule")
            {
                try
                {
                    bList = handler.getAllStylistsUpcomingBksForDate(bookingDate, sortBy, sortDir);

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
                    btnPrint.Visible = true;
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
            else if(cookie["UT"] == "M" && action == "LeaveSchedule")
            {
                try
                {
                    bList = handler.getAllStylistsUpcomingBksForDate(bookingDate, sortBy, sortDir);

                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);

                    TableCell date = new TableCell();
                    date.Text = "Date<br/>(d/M/Y)";
                    date.Width = 200;
                    date.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(date);

                    TableCell stylist = new TableCell();
                    stylist.Text = "Employee";
                    stylist.Width = 200;
                    stylist.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(stylist);

                    TableCell reason = new TableCell();
                    reason.Text = "Reason";
                    reason.Width = 200;
                    reason.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(reason);

                    TableCell desc = new TableCell();
                    desc.Text = "Description";
                    desc.Width = 200;
                    desc.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(desc);

                    int rowCount = 1;
                    foreach (SP_GetStylistBookings b in bList)
                    {
                        TableRow r = new TableRow();
                        r.Height = 30;
                        tblSchedule.Rows.Add(r);

                        try
                        {
                            leaveServices = handler.getLeaveReason(b.BookingID.ToString());

                            if (leaveServices.type == "U")
                            {
                                TableCell dateCell = new TableCell();
                                dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                                tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                                TableCell stylistCell = new TableCell();
                                stylistCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.StylistID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.StylistName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(stylistCell);

                                TableCell servicesCell = new TableCell();
                                servicesCell.Text = "<a href='../cheveux/services.aspx?ProductID=" + leaveServices.ServiceID.Replace(" ", string.Empty) + "'>"
                                                        + leaveServices.ServiceName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);

                                servicesCell = new TableCell();
                                servicesCell.Text = leaveServices.serviceDescripion.ToString();
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);
                                rowCount++;
                            }
                            else
                            {
                                tblSchedule.Rows.RemoveAt(rowCount);
                            }
                        }
                        catch (Exception err)
                        {
                            function.logAnError("Couldn't get the leave reason and description. Error: " + err.ToString());
                        }

                    }
                    btnPrint.Visible = true;

                }
                catch (Exception err)
                {
                    tblSchedule.Rows.Clear();
                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);
                    TableCell newCell = new TableCell();
                    newCell.Text = "Upcoming stylist leave for selected date currently unavailable.Please try again later.";
                    tblSchedule.Rows[0].Cells.Add(newCell);
                    function.logAnError("Error retreiving leave schedule.(allUpcomingBookingsFrDate) Error:" + err.ToString());
                }
            }
            
        }

        public void getAllStylistsUpcomingBksDR(DateTime startDate, DateTime endDate, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            cookie = Request.Cookies["CheveuxUserID"];
            string action = Request.QueryString["Action"];
            tblSchedule.CssClass = "table table-light table-hover";

            if (cookie["UT"] == "R" || action == "ViewAllSchedules" || action == "ViewStylistSchedule")
            {
                try
                {
                    bList = handler.getAllStylistsUpcomingBksDR(startDate, endDate, sortBy, sortDir);

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
                    btnPrint.Visible = true;
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
            else if (cookie["UT"] == "M" && action == "LeaveSchedule")
            {
                try
                {
                    bList = handler.getAllStylistsUpcomingBksDR(startDate, endDate, sortBy, sortDir);

                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);

                    TableCell date = new TableCell();
                    date.Text = "Date<br/>(d/M/Y)";
                    date.Width = 200;
                    date.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(date);

                    TableCell stylistCell = new TableCell();
                    stylistCell.Text = "Employee";
                    stylistCell.Width = 200;
                    stylistCell.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(stylistCell);

                    TableCell reason = new TableCell();
                    reason.Text = "Reason";
                    reason.Width = 200;
                    reason.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(reason);

                    TableCell desc = new TableCell();
                    desc.Text = "Description";
                    desc.Width = 200;
                    desc.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(desc);

                    int rowCount = 1;
                    foreach (SP_GetStylistBookings b in bList)
                    {
                        TableRow r = new TableRow();
                        r.Height = 30;
                        tblSchedule.Rows.Add(r);

                        try
                        {
                            leaveServices = handler.getLeaveReason(b.BookingID.ToString());

                            if (leaveServices.type == "U")
                            {
                                TableCell dateCell = new TableCell();
                                dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                                tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                                TableCell stylist = new TableCell();
                                stylist.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.StylistID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.StylistName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(stylist);

                                TableCell servicesCell = new TableCell();
                                servicesCell.Text = "<a href='../cheveux/services.aspx?ProductID=" + leaveServices.ServiceID.Replace(" ", string.Empty) + "'>"
                                                        + leaveServices.ServiceName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);

                                servicesCell = new TableCell();
                                servicesCell.Text = leaveServices.serviceDescripion.ToString();
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);
                                rowCount++;
                            }
                            else
                            {
                                tblSchedule.Rows.RemoveAt(rowCount);
                            }
                        }
                        catch (Exception err)
                        {
                            function.logAnError("Couldn't get the leave reason and description. Error: " + err.ToString());
                        }

                    }
                    btnPrint.Visible = true;

                }
                catch (Exception err)
                {
                    tblSchedule.Rows.Clear();
                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);
                    TableCell newCell = new TableCell();
                    newCell.Text = "Upcoming stylist leave for date range currently unavailable.Please try again later.";
                    tblSchedule.Rows[0].Cells.Add(newCell);
                    function.logAnError("Error retreiving leave schedule.(allUpcomingBookingsFrDR) Error:" + err.ToString());
                }
            }
            
        }

        public void getAllStylistsUpcomingBookings(string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            cookie = Request.Cookies["CheveuxUserID"];
            string action = Request.QueryString["Action"];
            tblSchedule.CssClass = "table table-light table-hover";

            if (cookie["UT"] == "R" || action == "ViewAllSchedules" || action == "ViewStylistSchedule")
            {
                try
                {
                    bList = handler.getAllStylistsUpcomingBookings(sortBy, sortDir);

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
                    btnPrint.Visible = true;
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
            else if (cookie["UT"] == "M" && action == "LeaveSchedule")
            {
                try
                {
                    bList = handler.getAllStylistsUpcomingBookings(sortBy, sortDir);

                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);

                    TableCell date = new TableCell();
                    date.Text = "Date<br/>(d/M/Y)";
                    date.Width = 200;
                    date.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(date);

                    TableCell stylist = new TableCell();
                    stylist.Text = "Employee";
                    stylist.Width = 200;
                    stylist.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(stylist);

                    TableCell reason = new TableCell();
                    reason.Text = "Reason";
                    reason.Width = 200;
                    reason.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(reason);

                    TableCell desc = new TableCell();
                    desc.Text = "Description";
                    desc.Width = 200;
                    desc.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(desc);

                    int rowCount = 1;
                    foreach (SP_GetStylistBookings b in bList)
                    {
                        TableRow r = new TableRow();
                        r.Height = 30;
                        tblSchedule.Rows.Add(r);

                        try
                        {
                            leaveServices = handler.getLeaveReason(b.BookingID.ToString());

                            if (leaveServices.type == "U")
                            {
                                TableCell dateCell = new TableCell();
                                dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                                tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                                TableCell stylistCell = new TableCell();
                                stylistCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.StylistID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.StylistName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(stylistCell);

                                TableCell servicesCell = new TableCell();
                                servicesCell.Text = "<a href='../cheveux/services.aspx?ProductID=" + leaveServices.ServiceID.Replace(" ", string.Empty) + "'>"
                                                        + leaveServices.ServiceName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);

                                servicesCell = new TableCell();
                                servicesCell.Text = leaveServices.serviceDescripion.ToString();
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);
                                rowCount++;
                            }
                            else
                            {
                                tblSchedule.Rows.RemoveAt(rowCount);
                            }
                        }
                        catch (Exception err)
                        {
                            function.logAnError("Couldn't get the leave reason and description. Error: " + err.ToString());
                        }
                        
                    }
                    btnPrint.Visible = true;

                }
                catch (Exception err)
                {
                    tblSchedule.Rows.Clear();
                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);
                    TableCell newCell = new TableCell();
                    newCell.Text = "All upcoming stylist leave currently unavailable.Please try again later.";
                    tblSchedule.Rows[0].Cells.Add(newCell);
                    function.logAnError("Error retreiving leave schedule.(allUpcomingBookings) Error:" + err.ToString());
                }
            }
            
        }
        #endregion


        #region Past
        public void getAllStylistsPastBookings(string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            cookie = Request.Cookies["CheveuxUserID"];
            string action = Request.QueryString["Action"];
            tblSchedule.CssClass = "table table-light table-hover";

            if (cookie["UT"] == "R" || action == "ViewAllSchedules" || action == "ViewStylistSchedule")
            {
                try
                {
                    bList = handler.getAllStylistsPastBookings(sortBy, sortDir);

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
                    btnPrint.Visible = true;
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
            else if (cookie["UT"] == "M" && action == "LeaveSchedule")
            {
                try
                {
                    bList = handler.getAllStylistsPastBookings(sortBy, sortDir);

                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);

                    TableCell date = new TableCell();
                    date.Text = "Date<br/>(d/M/Y)";
                    date.Width = 200;
                    date.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(date);

                    TableCell stylist = new TableCell();
                    stylist.Text = "Employee";
                    stylist.Width = 200;
                    stylist.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(stylist);

                    TableCell reason = new TableCell();
                    reason.Text = "Reason";
                    reason.Width = 200;
                    reason.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(reason);

                    TableCell desc = new TableCell();
                    desc.Text = "Description";
                    desc.Width = 200;
                    desc.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(desc);

                    int rowCount = 1;
                    foreach (SP_GetStylistBookings b in bList)
                    {
                        TableRow r = new TableRow();
                        r.Height = 30;
                        tblSchedule.Rows.Add(r);

                        try
                        {
                            leaveServices = handler.getLeaveReason(b.BookingID.ToString());

                            if (leaveServices.type == "U")
                            {
                                TableCell dateCell = new TableCell();
                                dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                                tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                                TableCell stylistCell = new TableCell();
                                stylistCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.StylistID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.StylistName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(stylistCell);

                                TableCell servicesCell = new TableCell();
                                servicesCell.Text = "<a href='../cheveux/services.aspx?ProductID=" + leaveServices.ServiceID.Replace(" ", string.Empty) + "'>"
                                                        + leaveServices.ServiceName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);

                                servicesCell = new TableCell();
                                servicesCell.Text = leaveServices.serviceDescripion.ToString();
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);
                                rowCount++;
                            }
                            else
                            {
                                tblSchedule.Rows.RemoveAt(rowCount);
                            }
                        }
                        catch (Exception err)
                        {
                            function.logAnError("Couldn't get the leave reason and description. Error: " + err.ToString());
                        }

                    }
                    btnPrint.Visible = true;
                }
                catch (Exception err)
                {
                    tblSchedule.Rows.Clear();
                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);
                    TableCell newCell = new TableCell();
                    newCell.Text = "Stylist Leave currently unavailable.Please try again later.";
                    tblSchedule.Rows[0].Cells.Add(newCell);
                    function.logAnError("Error retreiving leave schedule." + err.ToString());
                }
            }

        }
        public void getAllStylistsPastBksForDate(DateTime bookingDate, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            cookie = Request.Cookies["CheveuxUserID"];
            string action = Request.QueryString["Action"];
            tblSchedule.CssClass = "table table-light table-hover";

            if (cookie["UT"] == "R" || action == "ViewAllSchedules" || action == "ViewStylistSchedule")
            {
                try
                {
                    bList = handler.getAllStylistsPastBksForDate(bookingDate, sortBy, sortDir);

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
                    btnPrint.Visible = true;
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
            else if (cookie["UT"] == "M" && action == "LeaveSchedule")
            {
                try
                {
                    bList = handler.getAllStylistsPastBksForDate(bookingDate, sortBy, sortDir);

                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);

                    TableCell date = new TableCell();
                    date.Text = "Date<br/>(d/M/Y)";
                    date.Width = 200;
                    date.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(date);

                    TableCell stylist = new TableCell();
                    stylist.Text = "Employee";
                    stylist.Width = 200;
                    stylist.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(stylist);

                    TableCell reason = new TableCell();
                    reason.Text = "Reason";
                    reason.Width = 200;
                    reason.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(reason);

                    TableCell desc = new TableCell();
                    desc.Text = "Description";
                    desc.Width = 200;
                    desc.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(desc);

                    int rowCount = 1;
                    foreach (SP_GetStylistBookings b in bList)
                    {
                        TableRow r = new TableRow();
                        r.Height = 30;
                        tblSchedule.Rows.Add(r);

                        try
                        {
                            leaveServices = handler.getLeaveReason(b.BookingID.ToString());

                            if (leaveServices.type == "U")
                            {
                                TableCell dateCell = new TableCell();
                                dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                                tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                                TableCell stylistCell = new TableCell();
                                stylistCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.StylistID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.StylistName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(stylistCell);

                                TableCell servicesCell = new TableCell();
                                servicesCell.Text = "<a href='../cheveux/services.aspx?ProductID=" + leaveServices.ServiceID.Replace(" ", string.Empty) + "'>"
                                                        + leaveServices.ServiceName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);

                                servicesCell = new TableCell();
                                servicesCell.Text = leaveServices.serviceDescripion.ToString();
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);
                                rowCount++;
                            }
                            else
                            {
                                tblSchedule.Rows.RemoveAt(rowCount);
                            }
                        }
                        catch (Exception err)
                        {
                            function.logAnError("Couldn't get the leave reason and description. Error: " + err.ToString());
                        }

                    }
                    btnPrint.Visible = true;
                }
                catch (Exception err)
                {
                    tblSchedule.Rows.Clear();
                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);
                    TableCell newCell = new TableCell();
                    newCell.Text = "Stylist Leave currently unavailable.Please try again later.";
                    tblSchedule.Rows[0].Cells.Add(newCell);
                    function.logAnError("Error retreiving leave schedule." + err.ToString());
                }
            }

        }
        public void getAllStylistsPastBookingsDateRange(DateTime startDate, DateTime endDate, string sortBy, string sortDir)
        {
            tblSchedule.Rows.Clear();
            cookie = Request.Cookies["CheveuxUserID"];
            string action = Request.QueryString["Action"];
            tblSchedule.CssClass = "table table-light table-hover";

            if (cookie["UT"] == "R" || action == "ViewAllSchedules" || action == "ViewStylistSchedule")
            {
                try
                {
                    bList = handler.getAllStylistsPastBookingsDateRange(startDate, endDate, sortBy, sortDir);

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
                    btnPrint.Visible = true;
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
            else if (cookie["UT"] == "M" && action == "LeaveSchedule")
            {
                try
                {
                    bList = handler.getAllStylistsPastBookingsDateRange(startDate, endDate, sortBy, sortDir);
                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);

                    TableCell date = new TableCell();
                    date.Text = "Date<br/>(d/M/Y)";
                    date.Width = 200;
                    date.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(date);

                    TableCell stylist = new TableCell();
                    stylist.Text = "Employee";
                    stylist.Width = 200;
                    stylist.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(stylist);

                    TableCell reason = new TableCell();
                    reason.Text = "Reason";
                    reason.Width = 200;
                    reason.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(reason);

                    TableCell desc = new TableCell();
                    desc.Text = "Description";
                    desc.Width = 200;
                    desc.Font.Bold = true;
                    tblSchedule.Rows[0].Cells.Add(desc);

                    int rowCount = 1;
                    foreach (SP_GetStylistBookings b in bList)
                    {
                        TableRow r = new TableRow();
                        r.Height = 30;
                        tblSchedule.Rows.Add(r);

                        try
                        {
                            leaveServices = handler.getLeaveReason(b.BookingID.ToString());

                            if (leaveServices.type == "U")
                            {
                                TableCell dateCell = new TableCell();
                                dateCell.Text = b.BookingDate.ToString("dd-MM-yyyy");
                                tblSchedule.Rows[rowCount].Cells.Add(dateCell);

                                TableCell stylistCell = new TableCell();
                                stylistCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + b.StylistID.ToString().Replace(" ", string.Empty) +
                                        "'>" + b.StylistName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(stylistCell);

                                TableCell servicesCell = new TableCell();
                                servicesCell.Text = "<a href='../cheveux/services.aspx?ProductID=" + leaveServices.ServiceID.Replace(" ", string.Empty) + "'>"
                                                        + leaveServices.ServiceName.ToString() + "</a>";
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);

                                servicesCell = new TableCell();
                                servicesCell.Text = leaveServices.serviceDescripion.ToString();
                                tblSchedule.Rows[rowCount].Cells.Add(servicesCell);
                                rowCount++;
                            }
                            else
                            {
                                tblSchedule.Rows.RemoveAt(rowCount);
                            }
                        }
                        catch (Exception err)
                        {
                            function.logAnError("Couldn't get the leave reason and description. Error: " + err.ToString());
                        }

                    }
                    btnPrint.Visible = true;
                }
                catch (Exception err)
                {
                    tblSchedule.Rows.Clear();
                    TableRow newRow = new TableRow();
                    tblSchedule.Rows.Add(newRow);
                    TableCell newCell = new TableCell();
                    newCell.Text = "Stylist Leave currently unavailable.Please try again later.";
                    tblSchedule.Rows[0].Cells.Add(newCell);
                    function.logAnError("Error retreiving leave schedule." + err.ToString());
                }
            }

        }
        #endregion

        #endregion

        public void getTimeCustomerServices(string aBookingID, string primaryBookingID, int i, SP_GetStylistBookings a)
        {
            string action = Request.QueryString["Action"];
            cookie = Request.Cookies["CheveuxUserID"];
            if (cookie["UT"] == "R" || action == "ViewAllSchedules" || action == "ViewStylistSchedule" || cookie["UT"] == "S")
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

                    if (bServices.Count > 0)
                    {
                        time = handler.getMultipleServicesTime(primaryBookingID);

                        start.Text = time.StartTime.ToString("HH:mm");
                        tblSchedule.Rows[i].Cells.Add(start);

                        end.Text = time.EndTime.ToString("HH:mm");
                        tblSchedule.Rows[i].Cells.Add(end);
                    }
                    btnPrint.Visible = true;
                }
                catch (Exception Err)
                {
                    //If time isn't retrieved (Error)
                    start.Text = "---";
                    tblSchedule.Rows[i].Cells.Add(start);
                    end.Text = "---";
                    tblSchedule.Rows[i].Cells.Add(end);
                    function.logAnError("Couldn't get the time (check db for 2nd bkID) [appointments.aspx "
                        + "{getTimeCustomerServices?getTime}] error:"
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
                    catch (Exception Err)
                    {
                        empCell.Text = "-------";
                        tblSchedule.Rows[i].Cells.Add(empCell);
                        function.logAnError("Couldnt get stylist name[appointments.aspx {getT/c/s method}]err:" + Err.ToString());
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
                catch (Exception Err)
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
                        services.Text = "<a href='../cheveux/services.aspx?ProductID=" + bServices[0].ServiceID.Replace(" ", string.Empty) + "'>"
                        + bServices[0].ServiceName.ToString() + "</a>";
                    }
                    else if (bServices.Count > 1)
                    {
                        string dropDown = "<li style='list-style: none;' class='dropdown'>" +
                            "<a class='dropdown-toggle' data-toggle='dropdown' href='#'>";
                        if (bServices.Count == 2)
                        {
                            dropDown += bServices[0].ServiceName.ToString() +
                            ", " + bServices[1].ServiceName.ToString();
                        }
                        else if (bServices.Count > 2)
                        {
                            dropDown += " Multiple ";
                        }
                        dropDown += "<span class='caret'></span></a>" +
                                        "<ul class='dropdown-menu bg-dark text-white'>";
                        foreach (SP_GetBookingServices service in bServices)
                        {
                            dropDown += "<li>&nbsp;<a href='../cheveux/services.aspx?ProductID=" + service.ServiceID.Replace(" ", string.Empty) + "'>" +
                                " " + service.ServiceName.ToString() + " </a>&nbsp;</li>";
                        }
                        dropDown += "</ul></li>";

                        services.Text = dropDown;
                    }
                    tblSchedule.Rows[i].Cells.Add(services);
                    btnPrint.Visible = true;
                }
                catch (Exception Err)
                {
                    //if theres an error or cant retrieve the services from the database 
                    services.Text = "Unable to retreive services";
                    tblSchedule.Rows[i].Cells.Add(services);
                    function.logAnError("Couldn't get the services [appointments.aspx "
                        + "{getTimeCustomerServices?getServices} ] error:" + Err.ToString());
                }


                #endregion
            }
        }

        #region Calendars
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
            if(drpViewAppt.SelectedValue == "0")
            {
                if (e.Day.Date.CompareTo(DateTime.Today) < 0)
                {
                    e.Day.IsSelectable = false;
                    e.Cell.BackColor = System.Drawing.Color.LightGray;
                }
            }
            else if(drpViewAppt.SelectedValue == "1")
            {
                if (e.Day.Date.CompareTo(DateTime.Today) > 0)
                {
                    e.Day.IsSelectable = false;
                    e.Cell.BackColor = System.Drawing.Color.LightGray;
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
            if (date1 == null || date2 == null)
            {
                valDate.Visible = false;
            }
            else if (date1 > date2)
            {
                valDate.ForeColor = System.Drawing.Color.Red;
                valDate.Text = "*Please ensure that Start Date is smaller than End Date";
                valDate.Visible = true;
            }
            else if (date1 == date2)
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
        protected void drpStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int month = Convert.ToInt16(drpStartMonth.SelectedValue);
            calStart.VisibleDate = new DateTime(2018,
                                    month,
                                    1);

            calDay.VisibleDate = new DateTime(2018, month,
                                    1);
        }
        protected void drpEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int month = Convert.ToInt16(drpEndMonth.SelectedValue);
            calEnd.VisibleDate = new DateTime(2018,
                                    month,
                                   1);
        }
        #endregion

        protected void drpViewAppt_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterMonths();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            divPrintHeader.Visible = true;

            phLogin.Visible = false;
            dontPrint.Visible = false;
            phScheduleErr.Visible = false;

            TableRow newRow = new TableRow();
            newRow.Height = 50;
            tblLogo.Rows.Add(newRow);
            TableCell newCell = new TableCell();
            newCell.Font.Bold = true;
            newCell.Font.Bold = true;
            newCell.Text = "<a class='navbar-brand js-scroll-trigger' href='#' onClick='window.print()'>Cheveux </a>";
            tblLogo.Rows[0].Cells.Add(newCell);

            newRow = new TableRow();
            newRow.Height = 50;
            tblSum.Rows.Add(newRow);

            newCell = new TableCell();
            newCell.Font.Bold = true;
            newCell.Text = "Booking Type: ";
            tblSum.Rows[0].Cells.Add(newCell);

            newCell = new TableCell();
            newCell.Text = drpViewAppt.SelectedItem.Text;
            tblSum.Rows[0].Cells.Add(newCell);

            newRow = new TableRow();
            newRow.Height = 50;
            tblSum.Rows.Add(newRow);

            newCell = new TableCell();
            newCell.Font.Bold = true;
            newCell.Text = "For: ";
            tblSum.Rows[1].Cells.Add(newCell);

            if(empSelectionType.SelectedValue == "0")
            {
                newCell = new TableCell();
                newCell.Text = empSelectionType.SelectedItem.Text;
                tblSum.Rows[1].Cells.Add(newCell);
            }
            else if(empSelectionType.SelectedValue == "1")
            {
                newCell = new TableCell();
                newCell.Text = drpStylistNames.SelectedItem.Text;
                tblSum.Rows[1].Cells.Add(newCell);
            }

            newRow = new TableRow();
            newRow.Height = 50;
            tblSum.Rows.Add(newRow);

            if (rdoDate.SelectedValue == "2")
            {
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "When: ";
                tblSum.Rows[2].Cells.Add(newCell);

                newCell = new TableCell();
                newCell.Text = calDay.SelectedDate.ToString("yyyy - MM - dd");
                tblSum.Rows[2].Cells.Add(newCell);
            }
            else if(rdoDate.SelectedValue == "3")
            {
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "From: ";
                tblSum.Rows[2].Cells.Add(newCell);

                newCell = new TableCell();
                newCell.Text = calStart.SelectedDate.ToString("yyyy - MM - dd");
                tblSum.Rows[2].Cells.Add(newCell);

                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "To: ";
                tblSum.Rows[2].Cells.Add(newCell);

                newCell = new TableCell();
                newCell.Text = calEnd.SelectedDate.ToString("yyyy - MM - dd");
                tblSum.Rows[2].Cells.Add(newCell);

            }
            else
            {
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "When: ";
                tblSum.Rows[2].Cells.Add(newCell);

                newCell = new TableCell();
                newCell.Text = rdoDate.SelectedItem.Text;
                tblSum.Rows[2].Cells.Add(newCell);
            }

            //print the report
            ClientScript.RegisterStartupScript(typeof(Page), "key", "<script type='text/javascript'>window.print();;</script>");

            divPrintHeader.Visible = true;
        }
    }
}
