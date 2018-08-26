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
    public partial class Stylist : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        String dayDate = DateTime.Today.ToString("D");
        CUST_VISIT cust_visit = null;
        HttpCookie cookie = null;
        SP_ViewCustVisit cv = null;
        List<SP_GetEmpAgenda> agenda = null;
        List<SP_GetBookingServices> bServices = null;
        SP_GetMultipleServicesTime time = null;
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
                    lblNoBookings.Text = "You have no bookings for the day.<br/> Refresh page for update";
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
                    catch(ApplicationException err)
                    {
                        function.logAnError("Unable to check if visit record exists[stylist.aspx] err:" + err.ToString());
                    }

                    //create row 
                    TableRow r = new TableRow();
                    AgendaTable.Rows.Add(r);

                    getTimeCustomerServices(a.BookingID, a.PrimaryID, i, a);

                    TableCell present = new TableCell();
                    present.Text = function.GetFullArrivedStatus(a.Arrived.ToString()[0]);
                    AgendaTable.Rows[i].Cells.Add(present);

                    if (function.GetFullArrivedStatus(a.Arrived.ToString()[0]) == "Yes")
                    {
                        TableCell buttonCell = new TableCell();
                        buttonCell.Width = 200;
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
                                        Response.Redirect("../Stylist/CustomerVisit.aspx?bookingID=" + cust_visit.BookingID.ToString()
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
                                catch (ApplicationException err)
                                {
                                    phVisitSuccess.Visible = false;
                                    phVisitErr.Visible = true;
                                    lblVisitErr.Text = "Error:System is unable to create a visit record.br/>"
                                                          + "Please report to management. Sorry for the inconvenience.";
                                    //add error to the error log and then display response tab to say that an error has occured
                                    function.logAnError("Error creating visit record [stylist.aspx] err: " + err.ToString());
                                }

                            };
                            //add button control to the cell
                            buttonCell.Controls.Add(btn);
                        }
                        else if(cv != null)
                        {   
                            //if visit record already exists stylist should be able to update the visit
                            buttonCell.Text =   "<button type='button' class='btn btn-primary'>"
                                                +"<a href='../Stylist/CustomerVisit.aspx?bookingID="
                                                +a.PrimaryID.ToString().Replace(" ", string.Empty)
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
            catch (ApplicationException E)
            {
                phVisitSuccess.Visible = false;
                phBookingsErr.Visible = true;
                errorHeader.Text = "Error getting employee agenda.";
                errorMessage.Text = "It seems there is a problem communicating with the database."
                                    + "Please report problem to admin or try again later.";
                //log error, display error message,redirect to the error which then takes user to the home page if they would like to
                function.logAnError("Error with getEmpAgenda [stylist.aspx]. err: "+E.ToString());

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
                catch (ApplicationException serviceErr)
                {
                    function.logAnError("Error retreiving services [receptionist.aspx] getTimeAndServices method err:" + serviceErr.ToString());
                }
                time = handler.getMultipleServicesTime(primaryBookingID);

            }
            catch (ApplicationException Err)
            {
                start.Text = "---";
                end.Text = "---";
                function.logAnError("Couldn't get the time [receptionist.aspx] error:" + Err.ToString());
            }

            if (bServices.Count < 2)
            {
                start.Text = a.StartTime.ToString("HH:mm");
                AgendaTable.Rows[i].Cells.Add(start);

                end.Text = a.EndTime.ToString("HH:mm");
                AgendaTable.Rows[i].Cells.Add(end);
            }
            else if (bServices.Count >= 2)
            {
                start.Text = time.StartTime.ToString("HH:mm");
                AgendaTable.Rows[i].Cells.Add(start);

                end.Text = time.EndTime.ToString("HH:mm");
                AgendaTable.Rows[i].Cells.Add(end);
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
            }
            catch (ApplicationException Err)
            {
                services.Text = "Unable to retreive data";
                function.logAnError("Couldn't get the services [receptionist.aspx] error:" + Err.ToString());
            }

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
            AgendaTable.Rows[i].Cells.Add(services);
            #endregion
        }
    }
}