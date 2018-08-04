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
        protected void Page_Load(object sender, EventArgs e)
        {
            errorCssStyles();

            cookie = Request.Cookies["CheveuxUserID"];

            if (cookie == null)
            {
                phLogIn.Visible = true;
                phMain.Visible = false;
            }
            else if(cookie["UT"] != "S")
            {
                Response.Redirect("../Default.aspx");
            }
            else if(cookie["UT"] == "S")
            {
                phLogIn.Visible = false;
                phMain.Visible = true;

                try
                {
                    if(drpViewAppt.SelectedValue ==  "0")
                    {
                        phCheckBox.Visible = false;
                        phCal.Visible = false;

                        getUpcomingBookings(cookie["ID"].ToString());
                    }
                    else if(drpViewAppt.SelectedValue == "1")
                    {
                        phCheckBox.Visible = true;
                        if (bxAll.Checked)
                        {
                            phBookingsErr.Visible = false;
                            phCal.Visible = false;

                            getPastBookings(cookie["ID"].ToString());
                        }
                        else if (!bxAll.Checked)
                        {
                            phCal.Visible = true;
                        }
                    }
                }
                catch (ApplicationException Err)
                {
                    phScheduleErr.Visible = true;
                    errorHeader.Text = "Error";
                    errorMessage.Text = "It seems there is a problem connecting to the database.<br/>"
                                        + "Please report problem or try again later.";
                    function.logAnError(Err.ToString());
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
        public void getPastBookings(string empID)
        {
            Button btn;
            try
            {
                bList = handler.getStylistPastBookings(empID);

                phPast.Visible = true;
                tblPast.CssClass= "table table-light table-hover";

                TableRow row = new TableRow();
                tblPast.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 200;
                date.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 50;
                time.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 100;
                customer.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 200;
                svName.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(svName);

                TableCell svDesc = new TableCell();
                svDesc.Text = "Description";
                svDesc.Width = 400;
                svDesc.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(svDesc);

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
                    customerCell.Text = b.FullName.ToString();
                    tblPast.Rows[rowCount].Cells.Add(customerCell);

                    TableCell servNameCell = new TableCell();
                    servNameCell.Text = b.ServiceName.ToString();
                    tblPast.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell servDescCell = new TableCell();
                    servDescCell.Text = b.ServiceDescription.ToString();
                    tblPast.Rows[rowCount].Cells.Add(servDescCell);

                    TableCell buttonCell = new TableCell();
                    btn = new Button();
                    btn.Text = "Click";
                    btn.CssClass = "btn btn-default";
                    btn.Click += (a, o) =>
                    {
                        //
                    };
                    buttonCell.Controls.Add(btn);
                    tblPast.Rows[rowCount].Cells.Add(buttonCell);
                    rowCount++;
                }
            }
            catch (ApplicationException Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting past bookings.";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        public void getUpcomingBookings(string empID)
        {
            Button btn;
            try
            {
                phUpcoming.Visible = true;

                bList = handler.getStylistUpcomingBookings(empID);

                tblUpcoming.Visible = true;
                tblUpcoming.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblUpcoming.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 200;
                date.Font.Bold = true;
                tblUpcoming.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 50;
                time.Font.Bold = true;
                tblUpcoming.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 200;
                customer.Font.Bold = true;
                tblUpcoming.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 200;
                svName.Font.Bold = true;
                tblUpcoming.Rows[0].Cells.Add(svName);

                TableCell svDesc = new TableCell();
                svDesc.Text = "Description";
                svDesc.Width = 400;
                svDesc.Font.Bold = true;
                tblUpcoming.Rows[0].Cells.Add(svDesc);

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
                    customerCell.Text = b.FullName.ToString();
                    tblUpcoming.Rows[rowCount].Cells.Add(customerCell);

                    TableCell servNameCell = new TableCell();
                    servNameCell.Text = b.ServiceName.ToString();
                    tblUpcoming.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell servDescCell = new TableCell();
                    servDescCell.Text = b.ServiceDescription.ToString();
                    tblUpcoming.Rows[rowCount].Cells.Add(servDescCell);

                    TableCell buttonCell = new TableCell();
                    btn = new Button();
                    btn.Text = "Click";
                    btn.CssClass = "btn btn-default";
                    btn.Click += (a, o) =>
                    {
                        //
                    };
                    buttonCell.Controls.Add(btn);
                    tblUpcoming.Rows[rowCount].Cells.Add(buttonCell);
                    rowCount++;
                }
            }
            catch (ApplicationException Err)
            {
                phScheduleErr.Visible = true;
                errorHeader.Text = "Error getting upcoming bookings";
                errorMessage.Text = "It seems there is a problem communicating with the database.<br/>"
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
            }
        }
        public void pastDateRange(string empID, DateTime startDate, DateTime endDate)
        {
            Button btn;
            try
            {
                bList = handler.getStylistPastBookingsDateRange(empID, startDate, endDate);

                phPast.Visible = true;
                tblPast.CssClass = "table table-light table-hover";

                TableRow row = new TableRow();
                tblPast.Rows.Add(row);

                TableCell date = new TableCell();
                date.Text = "Date";
                date.Width = 200;
                date.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(date);

                TableCell time = new TableCell();
                time.Text = "Time";
                time.Width = 50;
                time.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(time);

                TableCell customer = new TableCell();
                customer.Text = "Customer";
                customer.Width = 100;
                customer.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(customer);

                TableCell svName = new TableCell();
                svName.Text = "Service";
                svName.Width = 200;
                svName.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(svName);

                TableCell svDesc = new TableCell();
                svDesc.Text = "Description";
                svDesc.Width = 400;
                svDesc.Font.Bold = true;
                tblPast.Rows[0].Cells.Add(svDesc);

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
                    customerCell.Text = b.FullName.ToString();
                    tblPast.Rows[rowCount].Cells.Add(customerCell);

                    TableCell servNameCell = new TableCell();
                    servNameCell.Text = b.ServiceName.ToString();
                    tblPast.Rows[rowCount].Cells.Add(servNameCell);

                    TableCell servDescCell = new TableCell();
                    servDescCell.Text = b.ServiceDescription.ToString();
                    tblPast.Rows[rowCount].Cells.Add(servDescCell);

                    TableCell buttonCell = new TableCell();
                    btn = new Button();
                    btn.Text = "Click";
                    btn.CssClass = "btn btn-default";
                    btn.Click += (a, o) =>
                    {
                        //
                    };
                    buttonCell.Controls.Add(btn);
                    tblPast.Rows[rowCount].Cells.Add(buttonCell);
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
        protected void calStart_SelectionChanged(object sender,EventArgs e)
        {
            lblDate1.Text = calStart.SelectedDate.ToString("dd-MM-yyyy");
            if (lblDate1.Text==string.Empty || lblDate2.Text==string.Empty)
            {
                phBookingsErr.Visible = true;
                lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                    + "Hint: View bookings between 1/1/19(start date) and 12/06/19(end date)";
            }
            else
            {
                phBookingsErr.Visible = false;
                pastDateRange(cookie["ID"].ToString(), calStart.SelectedDate, calEnd.SelectedDate);
            }
            
        }
        protected void calEnd_SelectionChanged(object sender, EventArgs e)
        {
            lblDate2.Text = calEnd.SelectedDate.ToString("dd-MM-yyyy");
            if (lblDate1.Text==string.Empty || lblDate2.Text==string.Empty)
            {
                phBookingsErr.Visible = true;
                lblBookingsErr.Text = "Please select a start and end date.<br/>"
                                    + "Hint: View bookings between 1/1/19(start date) and 12/12/19(end date)";
            }
            else
            {
                phBookingsErr.Visible = false;
                pastDateRange(cookie["ID"].ToString(), calStart.SelectedDate, calEnd.SelectedDate);
            }
        }
        protected void cal_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date > DateTime.Today)
            {
                e.Day.IsSelectable = false;
            }
        }
    }
}