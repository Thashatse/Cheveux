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
        string bookingID;
        string customerID;
        CUST_VISIT visit = null;
        BOOKING b=null;
        List<SP_GetBookingServices> bServices = null;
        SP_GetMultipleServicesTime time = null;
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
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Date:";
                newCell.Width = 300;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.Date.ToString("dd-MM-yyy");
                newCell.Width = 300;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                rCnt++;

                newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Booking comment:";
                newCell.Width = 300;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Width = 300;
                TextBox descBox = new TextBox();
                descBox.ID = "bookingComment";
                descBox.CssClass = "form-control";
                newCell.Controls.Add(descBox);
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                rCnt++;

                newRow = new TableRow();
                allBookingTable.Rows.Add(newRow);
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

                        if (handler.BLL_UpdateCustVisit(visit, b))
                        {
                            Response.Redirect("../Stylist/Stylist.aspx?Action=UpdateVisitRecord&CustomerName="
                                                +bDTL.CustomerName.ToString().Replace(" ", string.Empty));
                        }
                        else
                        {
                            phVisitErr.Visible = true;
                            lblVisitErr.Text = "Unable to update visit record.<br/>"
                                                  + "Please report to management or try again later.";
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
                allBookingTable.Rows[rCnt].Cells.Add(createVisit);
                rCnt++;
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
            }
            catch (Exception Err)
            {
                services.Text = "Unable to retreive service";
                function.logAnError("Couldn't get the services [customervisit.aspx-getT&C&S method] error:" + Err.ToString());
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
            allBookingTable.Rows[rCnt].Cells.Add(services);
            rCnt++;
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
                time = handler.getMultipleServicesTime(primaryBookingID);

            }
            catch (Exception Err)
            {
                newCell.Text = "Unable to retrieve time";
                //start.Text = "---";
                //end.Text = "---";
                function.logAnError("Couldn't get the time [customerVisit.aspx, etT&C&S method] error:" + Err.ToString());
            }

            if (bServices.Count < 2)
            {
                newCell.Text = a.StartTime.ToString("HH:mm") + " - " + a.EndTime.ToString("HH:mm");
            }
            else if (bServices.Count >= 2)
            {
                newCell.Text = time.StartTime.ToString("HH:mm") + " - " + time.EndTime.ToString("HH:mm");
            }
            allBookingTable.Rows[rCnt].Cells.Add(newCell);
            rCnt++;
            #endregion
        }

    }
}