﻿using System;
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
    public partial class Stylist : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();

        CUST_VISIT cust_visit = null;
        HttpCookie cookie = null;
        SP_ViewCustVisit cv = null;
        List<SP_GetEmpAgenda> agenda = null;
        List<SP_GetBookingServices> bServices = null;
        SP_GetMultipleServicesTime time = null;

        String dayDate = DateTime.Today.ToString("D");
        String bookingDate = DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            errorHeader.Font.Bold = true;
            errorHeader.Font.Underline = true;
            errorHeader.Font.Size = 21;
            errorMessage.Font.Size = 14;

            //access control
            HttpCookie UserID = Request.Cookies["CheveuxUserID"];
            if (UserID == null)
            {
                LoggedOut.Visible = true;
                AgendaTable.Visible = false;
                LoggedIn.Visible = false;
            }
            else if (UserID["UT"] != "S")
            {
                Response.Redirect("../Default.aspx");
            }
            else if (UserID["UT"] == "S")
            {
                LoggedOut.Visible = false;
                LoggedIn.Visible = true;
                lblDate.Text = dayDate;

                cookie = Request.Cookies["CheveuxUserID"];
                getAgenda(cookie["ID"].ToString(), DateTime.Parse(bookingDate),null,null);

                //if theres no booking for the day dont display the tables headings
                if (AgendaTable.Rows.Count == 1)
                {
                    AgendaTable.Visible = false;
                    noBookingsPH.Visible = true;
                    lblNoBookings.Text = "You have no bookings for the day.<br/> Refresh page for update  <i class='fa fa-refresh' onClick='history.go(0)'></i>";
                }
                else
                {
                    //if there are bookings for the day display headings 
                    AgendaTable.Visible = true;
                }

                string action = Request.QueryString["Action"];
                string customerID = Request.QueryString["CustomerName"];
                if(action== "UpdateVisitRecord")
                {
                    phVisitSuccess.Visible = true;
                    lblSuccess.Text = customerID + "'s visit record successfully updated.";
                }
            }
        }

        public void getAgenda(string id, DateTime bookingDate,string sortBy,string sortDir)
        {
            Button btn;

            try
            {
                agenda = handler.BLL_GetEmpAgenda(id, bookingDate,sortBy,sortDir);

                AgendaTable.CssClass = "table table-light table-hover";

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
                startTime.Font.Bold = true;
                startTime.Width = 250;
                AgendaTable.Rows[0].Cells.Add(startTime);

                TableCell endTime = new TableCell();
                endTime.Text = "End Time";
                endTime.Font.Bold = true;
                endTime.Width = 250;
                AgendaTable.Rows[0].Cells.Add(endTime);

                TableCell cust = new TableCell();
                cust.Text = "Customer";
                cust.Font.Bold = true;
                cust.Width = 350;
                AgendaTable.Rows[0].Cells.Add(cust);

                TableCell service = new TableCell();
                service.Text = "Service";
                service.Font.Bold = true;
                service.Width = 350;
                AgendaTable.Rows[0].Cells.Add(service);

                TableCell comment = new TableCell();
                comment.Text = "Comment";
                comment.Font.Bold = true;
                comment.Width = 350;
                AgendaTable.Rows[0].Cells.Add(comment);

                TableCell arrived = new TableCell();
                arrived.Text = "Arrived";
                arrived.Font.Bold = true;
                arrived.Width = 100;
                AgendaTable.Rows[0].Cells.Add(arrived);

                TableCell visitRecord = new TableCell();
                visitRecord.Width = 400;
                AgendaTable.Rows[0].Cells.Add(visitRecord);

                //integer that will be appended in the foreach loop to access the new row for every iteration of the foreach
                int i = 1;
                foreach (SP_GetEmpAgenda a in agenda)
                {
                    try
                    {
                        cv = handler.BLL_ViewCustVisit(a.UserID, a.BookingID);
                    }
                    catch(Exception err)
                    {
                        function.logAnError("Unable to check if visit record exists[stylist.aspx] err:" + err.ToString());
                    }
                    //create row 
                    TableRow r = new TableRow();
                    AgendaTable.Rows.Add(r);

                    getTimeCustomerServices(a.BookingID, a.PrimaryID, i, a);

                    TableCell c = new TableCell();
                    c.Text = a.Comment.ToString();
                    AgendaTable.Rows[i].Cells.Add(c);

                    TableCell present = new TableCell();
                    present.Text = function.GetFullArrivedStatus(a.Arrived.ToString()[0]);
                    AgendaTable.Rows[i].Cells.Add(present);

                    if (function.GetFullArrivedStatus(a.Arrived.ToString()[0]) == "Yes")
                    {
                        TableCell buttonCell = new TableCell();
                        buttonCell.Width = 100;
                        buttonCell.Height = 50;

                        if (cv == null)
                        {   //if visit record doesn't exist show button

                            //create button 
                            btn = new Button();
                            btn.Text = "Create Visit Record";
                            btn.CssClass = "btn btn-primary";
                            //button's click event
                            btn.Click += (ss, ee) =>
                            {
                                try
                                {
                                    /* What does the button do:
                                     * =======================
                                     * button creates customer visit record in the CUST_VISIT table 
                                     *and redirects user to the customer visit page of the booking
                                     * 
                                     */
                                    cust_visit = new CUST_VISIT();

                                    cust_visit.CustomerID = a.UserID.ToString();
                                    cust_visit.Date = Convert.ToDateTime(a.Date);
                                    cust_visit.BookingID = a.PrimaryID.ToString();
                                    cust_visit.Description = "Pending";
                                    if (handler.BLL_CreateCustVisit(cust_visit))
                                    {
                                        Response.Redirect("../Stylist/CustomerVisit.aspx?Action=CreateRecord&bookingID=" + cust_visit.BookingID.ToString()
                                                          +"&customerID=" + cust_visit.CustomerID.ToString());
                                    }
                                    else
                                    {
                                        phVisitSuccess.Visible = false;
                                        //if the insert fails, display failed message
                                        phRecordErr.Visible = true;
                                        lblRecordErr.Text = "Error creating record<br/>Please try again later or report to admin.";
                                    }
                                }
                                catch (Exception err)
                                {
                                    phVisitSuccess.Visible = false;
                                    phVisitErr.Visible = true;
                                    lblVisitErr.Text = "Error:System is unable to create a visit record.br/>"
                                                          + "Please report to management. Sorry for the inconvenience.";
                                    //add error to the error log and then display response tab to say that an error has occured
                                    function.logAnError("Error creating visit record [stylist.aspx {btn}] err: " + err.ToString());
                                }
                            };
                            //add button control to the cell
                            buttonCell.Controls.Add(btn);
                        }
                        else if(cv != null)
                        {   
                            //if visit record already exists stylist should be able to update the visit
                            buttonCell.Text =   "<button type='button' class='btn btn-primary'>"
                                                + "<a href='../Stylist/CustomerVisit.aspx?Action=CreateRecord&bookingID="
                                                + a.PrimaryID.ToString().Replace(" ", string.Empty)
                                                + "&customerID="+a.UserID.ToString().Replace(" ", string.Empty)
                                                + "' style='color:White; text-decoration:none;' >Update</a>"
                                                + "</button>";
                        }


                        //add the cell to the row
                        AgendaTable.Rows[i].Cells.Add(buttonCell);
                    }
                    //increment i 
                    i++;
                }
            }
            catch (Exception E)
            {
                phVisitSuccess.Visible = false;
                phBookingsErr.Visible = true;
                errorHeader.Text = "Error getting employee agenda.";
                errorMessage.Text = "It seems there is a problem communicating with the database."
                                    + "Please report problem to admin or try again later.";
                //log error, display error message,redirect to the error which then takes user to the home page if they would like to
                function.logAnError("Error with getEmpAgenda [stylist.aspx {getAgenda}]. err: "+E.ToString());
            }
        }
        public void getTimeCustomerServices(string aBookingID, string primaryBookingID, int i, SP_GetEmpAgenda a)
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
                    function.logAnError("Error retreiving services [stylist.aspx {nested try in getTime method}] method err:" + serviceErr.ToString());
                }

                if (bServices.Count > 0)
                {
                    time = handler.getMultipleServicesTime(primaryBookingID);

                    start.Text = time.StartTime.ToString("HH:mm");
                    AgendaTable.Rows[i].Cells.Add(start);

                    end.Text = time.EndTime.ToString("HH:mm");
                    AgendaTable.Rows[i].Cells.Add(end);
                }

            }
            catch (Exception Err)
            {
                //If time isn't retrieved (Error)
                start.Text = "---";
                AgendaTable.Rows[i].Cells.Add(start);
                end.Text = "---";
                AgendaTable.Rows[i].Cells.Add(end);
                function.logAnError("Couldn't get the time (check db for 2nd bkID) [stylist.aspx {getTimeCustomerServices?getTime}] error:"
                                            + Err.ToString());
            }
            #endregion
            #region Customer
            TableCell c = new TableCell();
            c.Width = 300;
            c.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + a.UserID.ToString().Replace(" ", string.Empty) +
                            "'>" + a.CustomerFName.ToString() + "</a>";
            AgendaTable.Rows[i].Cells.Add(c);
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
                AgendaTable.Rows[i].Cells.Add(services);
            }
            catch (Exception Err)
            {
                //if theres an error or cant retrieve the services from the database 
                services.Text = "Unable to retreive services";
                AgendaTable.Rows[i].Cells.Add(services);
                function.logAnError("Couldn't get the services [stylis.aspx {getTimeCustomerServices?getServices}] error:" + Err.ToString());
            }
            #endregion
        }
    }
}
