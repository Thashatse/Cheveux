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
    public partial class CustomerVisit : System.Web.UI.Page
    {
        String dayDate = DateTime.Today.ToString("D");

        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        SP_GetAllofBookingDTL bDTL = null;
        
        CUST_VISIT visit = null;
        BOOKING b=null;
        List<SP_GetBookingServices> bServices = null;
        SP_GetMultipleServicesTime time = null;
        SP_ReturnBooking rb = null;
        SP_ReturnBooking rbNext = null;
        List<SP_ReturnAvailServices> ras = null;

        string bookingID;
        string customerID;
        string action;
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
                Response.Redirect("../Stylist/Stylist.aspx");
            }
            else if (UserID["UT"] != "S")
            {
                Response.Redirect("../Default.aspx");
            }
            else if (UserID["UT"] == "S")
            {
                LoggedOut.Visible = false;
                LoggedIn.Visible = true;

                action = Request.QueryString["Action"];

                if(action == "CreateRecord")
                {
                    jheader.InnerText = "Create Customer Visit Record";
                    bookingID = Request.QueryString["bookingID"];
                    customerID = Request.QueryString["customerID"];

                    if (bookingID != null && customerID != null)
                    {
                        getDetailsAndCreateRecord(bookingID, customerID);
                    }
                    else
                    {
                        phBookingsErr.Visible = true;
                        errorHeader.Text = "Fail.";
                        errorMessage.Text = "Unable to display visit details.<br/>"
                                            + "Rest assured, customer visit record was created.<br/>"
                                            + "Please report this fault to the administrator so we can make your life"
                                            + "a bit easier and fix it as soon as possible.";
                    }
                
                }
                else
                {
                    jheader.InnerText = "Create Customer Visit Record";
                }
            }
        }

        public void getDetailsAndCreateRecord(string bookingID, string customerID)
        {
            try
            {
                bDTL = handler.BLL_GetAllofBookingDTL(bookingID.Replace(" ", string.Empty), customerID);

                //create a variablew to track the row count
                int rCnt = 0;

                TableRow newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                TableCell newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Customer Name:";
                newCell.Width = 300;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);

                getTimeCustomerServices(bDTL.BookingID, bDTL.BookingID, rCnt, bDTL);

                newRow = new TableRow();
                newRow.Height = 50;
                tblOther.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Date:";
                newCell.Width = 300;
                tblOther.Rows[0].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.Date.ToString("dd-MM-yyy");
                newCell.Width = 300;
                tblOther.Rows[0].Cells.Add(newCell);

                newRow = new TableRow();
                newRow.Height = 50;
                tblOther.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Booking comment:";
                newCell.Width = 300;
                tblOther.Rows[1].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Width = 300;
                TextBox descBox = new TextBox();
                descBox.ID = "bookingComment";
                if(bDTL.Comment != string.Empty)
                {
                    descBox.Text =bDTL.Comment.ToString();
                }
                descBox.CssClass = "form-control";
                //descBox.MaxLength = 50;
                newCell.Controls.Add(descBox);
                tblOther.Rows[1].Cells.Add(newCell);

                newRow = new TableRow();
                tblOther.Rows.Add(newRow);
                TableCell len = new TableCell();
                len.Text = "*Please keep the comment short and descriptive";
                len.ForeColor = System.Drawing.Color.Red;
                len.Visible = false;
                tblOther.Rows[2].Cells.Add(len);

                newRow = new TableRow();
                tblOther.Rows.Add(newRow);
                TableCell val = new TableCell();
                val.Text = "*Please enter comment";
                val.ForeColor = System.Drawing.Color.Red;
                val.Visible = false;
                tblOther.Rows[3].Cells.Add(val);

                newRow = new TableRow();
                tblOther.Rows.Add(newRow);
                TableCell createVisit = new TableCell();
                createVisit.Width = 300;
                Button btnVisit = new Button();
                btnVisit.Text = "Update";
                btnVisit.CssClass = "btn btn-primary";
                btnVisit.Click += (ss, ee) =>
                {
                    try
                    {
                        visit = new CUST_VISIT();
                        
                        visit.CustomerID = customerID.ToString();
                        visit.BookingID =  bookingID.ToString();
                        visit.Description = Convert.ToString(descBox.Text);

                        b = new BOOKING();
                        b.Comment =Convert.ToString( descBox.Text);

                        if (b.Comment == string.Empty || b.Comment == null)
                        {
                            val.Visible = true;
                        }
                        if(b.Comment.Length > 50)
                        {
                            len.Visible = true;   
                        }
                        else
                        {
                            if (handler.BLL_UpdateCustVisit(visit, b))
                            {
                                Response.Redirect("../Stylist/Stylist.aspx?Action=UpdateVisitRecord&CustomerName="
                                                    + bDTL.CustomerName.ToString().Replace(" ", string.Empty));
                            }
                            else
                            {
                                phVisitErr.Visible = true;
                                lblVisitErr.Text = "Unable to update visit record.<br/>"
                                                      + "Please report to management or try again later.";
                            }
                        }

                    }
                    catch (Exception err)
                    {
                        phVisitErr.Visible = true;
                        lblVisitErr.Text = "System is unable to create a visit record.<br/>"
                                              + "Please report to management or try again later.";
                        function.logAnError("Error updating visit [customervisit.aspx btnVisit] err:"+err.ToString());
                    }
                };
                createVisit.Controls.Add(btnVisit);
                tblOther.Rows[4].Cells.Add(createVisit);
            }
            catch(Exception Err)
            {
                phBookingsErr.Visible = true;
                errorHeader.Text = "Error.Cannot display booking details.";
                errorMessage.Text = "It seems there is a problem communicating with the database."
                                    + "Please report problem to admin or try again later.";

                function.logAnError("Couldn't display bookingDetails to create visit record"
                                     + " [customerVisit.aspx getDetailsAndCreateRecord method ]"
                                     +" err:"+ Err.ToString());
            }
        }
        public void getTimeCustomerServices(string aBookingID, string primaryBookingID, int rCnt, SP_GetAllofBookingDTL a)
        {
            #region Customer
            TableCell c = new TableCell();
            c.Width = 300;
            c.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + a.CustomerID.ToString().Replace(" ", string.Empty) +
                            "'>" + a.CustomerName.ToString() + "</a>";
            allBookingTable.Rows[rCnt].Cells.Add(c);
            rCnt++;
            #endregion
            #region Services

            TableRow newRow = new TableRow();
            newRow.Height = 50;
            allBookingTable.Rows.Add(newRow);
            TableCell newCell = new TableCell();
            newCell.Font.Bold = true;
            newCell.Text = "Service :";
            newCell.Width = 300;
            allBookingTable.Rows[rCnt].Cells.Add(newCell);
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
                allBookingTable.Rows[rCnt].Cells.Add(services);
                rCnt++;
            }
            catch (Exception Err)
            {
                //if theres an error or cant retrieve the services from the database 
                services.Text = "Unable to retreive services";
                allBookingTable.Rows[rCnt].Cells.Add(services);
                rCnt++;
                function.logAnError("Couldn't get the services [stylist.aspx {getTimeCustomerServices?getServices} ] error:" + Err.ToString());
            }
            #endregion
            #region Time

            newRow = new TableRow();
            newRow.Height = 50;
            allBookingTable.Rows.Add(newRow);
            newCell = new TableCell();
            newCell.Font.Bold = true;
            newCell.Text = "Start Time - End Time:";
            newCell.Width = 300;
            allBookingTable.Rows[rCnt].Cells.Add(newCell);
            newCell = new TableCell();
            newCell.Width = 300;
            try
            {
                try
                {
                    bServices = handler.getBookingServices(a.BookingID.ToString());
                }
                catch (Exception serviceErr)
                {
                    function.logAnError("Error retreiving services [receptionist.aspx] getTimeAndServices method err:" + serviceErr.ToString());
                }
                if (bServices.Count > 0)
                {
                    time = handler.getMultipleServicesTime(primaryBookingID);

                    newCell.Text = time.StartTime.ToString("HH:mm") + " - " + time.EndTime.ToString("HH:mm");

                    allBookingTable.Rows[rCnt].Cells.Add(newCell);
                    rCnt++;
                }
            }
            catch (Exception Err)
            {
                newCell.Text = "Unable to retrieve time";
                function.logAnError("Couldn't get the time [customerVisit.aspx, getT&C&S method] Error:" + Err.ToString());
            }

            //if (bServices.Count < 2)
            //{
            //    newCell.Text = a.StartTime.ToString("HH:mm") + " - " + a.EndTime.ToString("HH:mm");
            //}
            //else if (bServices.Count >= 2)
            //{
            //    newCell.Text = time.StartTime.ToString("HH:mm") + " - " + time.EndTime.ToString("HH:mm");
            //}
            #endregion
        }
    }
}