﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;


namespace Cheveux
{
    public partial class Receptionist : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        String dayDate = DateTime.Today.ToString("D");
        String bookingDate = DateTime.Now.ToString("yyyy-MM-dd");
        List<SP_GetEmpNames> list = null;
        List<SP_GetEmpAgenda> agenda = null;
        BOOKING checkIn = null;
        HttpCookie cookie = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            errorHeader.Font.Bold = true;
            errorHeader.Font.Underline = true;
            errorHeader.Font.Size = 21;
            errorMessage.Font.Size = 14;
            errorToReport.Font.Size = 10;

            cookie = Request.Cookies["CheveuxUserID"];
            if(cookie == null)
            {
                LoggedOut.Visible = true;
                viewStylistHeader.Visible = false;
                drpEmpNames.Visible = false;
                LoggedIn.Visible = false;
            }
            else if(cookie["UT"] != "R")
            {
                Response.Redirect("../Default.aspx");
            }
            else if(cookie["UT"] == "R")
            {
                LoggedOut.Visible = false;
                viewStylistHeader.Visible = true;
                drpEmpNames.Visible = true;
                LoggedIn.Visible = true;

                lblDate.Text = dayDate;
                string wB = Request.QueryString["WB"];
                if (wB == "True")
                {
                    Welcome.Text = "Welcome Back " + handler.GetUserDetails(cookie["ID"]).FirstName;
                }
                list = handler.BLL_GetEmpNames();
                if (!Page.IsPostBack)
                {
                    try
                    {
                        foreach (SP_GetEmpNames emps in list)
                        {
                            //Load employee names into dropdownlist
                            drpEmpNames.DataSource = list;
                            //set the coloumn that will be displayed to the user
                            drpEmpNames.DataTextField = "Name";
                            //set the coloumn that will be used for the valuefield
                            drpEmpNames.DataValueField = "EmployeeID";
                            //bind the data
                            drpEmpNames.DataBind();
                            /*set the default (text (dropdownlist index[0]) that will first be displayed to the user.
                             * in this case the "instruction" to select the employee
                            */
                            drpEmpNames.Items.Insert(0, new ListItem("--Select Employee--", "-1"));
                        }
                    }
                    catch (ApplicationException Err)
                    {
                        drpEmpNames.Items.Insert(0, new ListItem("Error"));
                        phBookingsErr.Visible = true;
                        errorHeader.Text = "Oh no!";
                        errorMessage.Text = "It seems there is a problem retrieving data from the database"
                                            + "Please report problem or try again later.";
                        errorToReport.Text = "Error To report:" + Err.ToString();
                        function.logAnError(Err.ToString());
                    }

                }

                try
                {
                    /*if the selected valus is not the "select employee" display the employee names
                     * if the selected value is the "select employee" nothing will be displayed or added to the table
                     * getAgenda method will not run
                     */
                    if (drpEmpNames.SelectedValue != "-1")
                    {
                        getAgenda(drpEmpNames.SelectedValue, DateTime.Parse(bookingDate));

                        if(AgendaTable.Rows.Count == 1)
                        {
                            AgendaTable.Visible = false;
                            noBookingsPH.Visible = true;
                            lblNoBookings.Text = drpEmpNames.SelectedItem.Text + " has not been appointed to any bookings today."
                                                + "<br/>Refresh to check for updated stylists bookings appointments.";
                        }
                        else if(AgendaTable.Rows.Count > 1)
                        {
                            noBookingsPH.Visible = false;
                            AgendaTable.Visible = true;
                        }
                        
                    }
                }
                catch (ApplicationException Err)
                {
                    AgendaTable.Visible = false;
                    phBookingsErr.Visible = true;
                    errorHeader.Text = "Error";
                    errorMessage.Text = "It seems there is a problem connecting to the database.<br/>"
                                        + "Please report problem or try again later.";
                    errorToReport.Text = "Error To report:" + Err.ToString();
                    function.logAnError(Err.ToString());
                }
            }   
        }

        public void getAgenda(string id, DateTime bookingDate)
        {
            Button btn;

            try
            {
                agenda = handler.BLL_GetEmpAgenda(id, bookingDate);
                
                //create row for the table 
                TableRow row = new TableRow();
                row.Height = 50; 
                
                //add row to the table
                AgendaTable.Rows.Add(row);
                
                /* 
                 * create the cells for the row
                 * and their names
                 * the cells being created are for the first row of the table
                 * and their names are the column names
                 * Each cell is added to the table row
                 * .Rows[0] => refers to the first row of the table
                 * */
                TableCell startTime = new TableCell();
                startTime.Text = "Start Time";
                startTime.Width = 200;
                startTime.Font.Bold = true;
                AgendaTable.Rows[0].Cells.Add(startTime);

                TableCell endTime = new TableCell();
                endTime.Text = "End Time";
                endTime.Width = 200;
                endTime.Font.Bold = true;
                AgendaTable.Rows[0].Cells.Add(endTime);

                TableCell cust = new TableCell();
                cust.Text = "Customer Name";
                cust.Width = 300;
                cust.Font.Bold = true;
                AgendaTable.Rows[0].Cells.Add(cust);

                TableCell emp = new TableCell();
                emp.Text = "Employee Name";
                emp.Width = 300;
                emp.Font.Bold = true;
                AgendaTable.Rows[0].Cells.Add(emp);

                TableCell service = new TableCell();
                service.Text = "Service";
                service.Width = 300;
                service.Font.Bold = true;
                AgendaTable.Rows[0].Cells.Add(service);

                TableCell arrived = new TableCell();
                arrived.Text = "Arrived";
                arrived.Width = 100;
                arrived.Font.Bold = true;
                AgendaTable.Rows[0].Cells.Add(arrived);

                //integer that will be incremented in the foreach loop to access the new row for every iteration of the foreach
                int i = 1;
                foreach(SP_GetEmpAgenda a in agenda)
                {
                    
                    //created cell for the record
                    TableRow r = new TableRow();
                    //add row to table
                    AgendaTable.Rows.Add(r);

                    //create start cell and add to row.. cell index: 0
                    TableCell start = new TableCell();
                    start.Width = 200;
                    start.Text = a.StartTime.ToString();
                    AgendaTable.Rows[i].Cells.Add(start);

                    //create end cell and add to row.. cell index: 1
                    TableCell end = new TableCell();
                    end.Width = 200;
                    end.Text = a.EndTime.ToString();
                    AgendaTable.Rows[i].Cells.Add(end);

                    //created customer name cell and add to row.. cell index: 2
                    TableCell c = new TableCell();
                    c.Width = 300;
                    c.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + a.UserID.ToString().Replace(" ", string.Empty) +
                                    "'>" + a.CustomerFName.ToString() + "</a>";
                    AgendaTable.Rows[i].Cells.Add(c);

                    //create employee name cell and add to row.. cell index: 3
                    TableCell e = new TableCell();
                    e.Width = 300;
                    e.Text = "<a href = '../Profile.aspx?Action=View" +
                                        "&empID=" + a.empID.ToString().Replace(" ", string.Empty) +
                                        "'>" + a.EmpFName.ToString()+ "</a>";
                    AgendaTable.Rows[i].Cells.Add(e);

                    //create service name cell and add to row.. cell index: 4
                    TableCell s = new TableCell();
                    s.Width = 300;
                    s.Text = "<a href = 'ViewProduct.aspx?ProductID=" + a.ProductID.ToString().Replace(" ", string.Empty) +
                                    "'>" + a.ServiceName.ToString() + "</a>";
                    AgendaTable.Rows[i].Cells.Add(s);
                    
                    //create arrival status cell and add to row.. cell index : 5
                    TableCell present = new TableCell();
                    present.Width = 100;
                    present.Text = function.GetFullArrivedStatus(a.Arrived.ToString()[0]);
                    AgendaTable.Rows[i].Cells.Add(present);

                    //create cell that will be populated by the button and add to row.. cell index: 6
                    TableCell buttonCell = new TableCell();
                    buttonCell.Width = 200;
                    buttonCell.Height = 50;


                    if (function.GetFullArrivedStatus(a.Arrived.ToString()[0]) == "No")
                    {
                                                
                        //create button
                        btn = new Button();
                        btn.Text = "Check-in";
                        btn.CssClass = "btn btn-primary";
                        btn.Click += (ss, ee) => {
                            /*
                             * Check-in code here 
                             * After clicking the button arrived should change to Y
                             * and the button text should change to Check-out
                             * and code should cater for the change as the stored procedure to check out and generate invoice
                             * needs to be called
                             */
                            try
                            {
                                checkIn = new BOOKING();

                                checkIn.BookingID = a.BookingID.ToString();
                                checkIn.StylistID = drpEmpNames.SelectedValue.ToString();

                                if (handler.BLL_CheckIn(checkIn))
                                {
                                    //if BLL_CheckIn successful and arrival status changed show user and refresh the page
                                    Response.Write("<script>alert('Customer has been checked-in.');location.reload(true);</script>");
                                }
                                else
                                {
                                    //if BLL_CheckIn unsuccessful and arrival status was not changed tell the user to try again or report to admin
                                    //Response.Write("<script>alert('Unsuccessful.Status was not changed.If problem persists report to admin.');</script>");
                                    phCheckInErr.Visible = true;
                                    lblCheckinErr.Text = "An error has occured.We are unable to check-in the customer at this point in time.<br/>"
                                                          + "Please report to management. Sorry for the inconvenience.";
                                }

                            }
                            catch (ApplicationException err)
                            {
                                //Error handling
                                //Response.Write("<script>alert('Our apologies. An error has occured. Please report to the administrator or try again later.')</script>");
                                phCheckInErr.Visible = true;
                                lblCheckinErr.Text = "An error has occured during the check-in process.<br/>"
                                                      + "Please report to management or try again later. Sorry for the inconvenience.";
                                //add error to the error log and then display response tab to say that an error has occured
                                function.logAnError(err.ToString());
                            }


                        };
                        //add button to cell 
                        buttonCell.Controls.Add(btn);
                        //add cell to row
                        AgendaTable.Rows[i].Cells.Add(buttonCell);
                    }
                    else if(function.GetFullArrivedStatus(a.Arrived.ToString()[0]) == "Yes")
                    {
                        
                        //create button
                        TableCell newCell = new TableCell();
                        newCell.Text = "<button type = 'button' class='btn btn-primary'>" +
                            "<a href = '../ViewBooking.aspx?BookingID=" + a.BookingID.ToString().Replace(" ", string.Empty) +
                            "&BookingType=CheckOut" +
                            "&PreviousPage=Receptionist.aspx' style='color:White'>Check-out</a></button>";
                        AgendaTable.Rows[i].Cells.Add(newCell);
                    }
                    //increment control variable
                    i++;
                }
            }
            catch(ApplicationException E)
            {
                //Response.Write("<script>alert('Trouble communicating with the database.Report to admin and try again later.');location.reload();</script>");
                phBookingsErr.Visible = true;
                errorHeader.Text = "Error getting employee agenda.";
                errorMessage.Text = "It seems there is a problem communicating with the database."
                                    + "Please report problem to admin or try again later.";
                errorToReport.Text = "Error To report:" + E.ToString();

                function.logAnError(E.ToString());
            }
        }
    }
}
