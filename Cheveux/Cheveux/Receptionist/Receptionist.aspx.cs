using System;
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
using System.Drawing;

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
        List<SP_GetBookingServices> bServices = null;
        SP_GetMultipleServicesTime time = null;
        BOOKING checkIn = null;
        HttpCookie cookie = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Error
            errorHeader.Font.Bold = true;
            errorHeader.Font.Underline = true;
            errorHeader.Font.Size = 21;
            errorMessage.Font.Size = 14;
            #endregion

            #region Access Control
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
                sidebar.Visible = true;
                #endregion

                #region Alerts
                //Check For Low Stock
                checkForLowStock();
                #endregion

                #region Header
                //date
                lblDate.Text = dayDate;

                //welcom back
                string wB = Request.QueryString["WB"];
                if (wB == "True")
                {
                    Welcome.Text = "Welcome Back " + handler.GetUserDetails(cookie["ID"]).FirstName;
                }
                #endregion

                #region Agenda
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
                        errorHeader.Text = "Error retrieving employee names";
                        errorMessage.Text = "It seems there is a problem retrieving data from the database"
                                            + "Please report problem or try again later.";
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
                        getAgenda(drpEmpNames.SelectedValue, DateTime.Parse(bookingDate),null,null);

                        if (AgendaTable.Rows.Count == 1)
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
                    errorMessage.Text = "It seems there is a problem retrieving employees bookings.<br/>"
                                        + "Please report problem or try again later.";
                    function.logAnError(Err.ToString());
                }

                string action = Request.QueryString["Action"];
                string custName = Request.QueryString["CustomerName"];
                string stylistName = Request.QueryString["StylistName"];

                if (action == "CheckedIn" && custName != null && stylistName != null)
                {
                    phCheckInSuccess.Visible = true;
                    lblSuccess.Text = custName.ToString() + " has been checked for their booking with "
                                    + stylistName.ToString();
                }
                #endregion
            }
        }
        
        protected void drpEmpNames_Changed(object sender, EventArgs e)
        {
            phCheckInSuccess.Visible = false;    
        }

        #region Agenda
        public void getAgenda(string id, DateTime bookingDate,string sortBy, string sortDir)
        {
            Button btnCheckin;
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
                startTime.Width = 200;
                startTime.Font.Bold = true;
                AgendaTable.Rows[0].Cells.Add(startTime);

                TableCell endTime = new TableCell();
                endTime.Text = "End Time";
                endTime.Width = 200;
                endTime.Font.Bold = true;
                AgendaTable.Rows[0].Cells.Add(endTime);

                TableCell cust = new TableCell();
                cust.Text = "Customer";
                cust.Width = 300;
                cust.Font.Bold = true;
                AgendaTable.Rows[0].Cells.Add(cust);

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

                TableCell edit = new TableCell();
                edit.Width = 200;
                AgendaTable.Rows[0].Cells.Add(edit);

                TableCell checkin = new TableCell();
                checkin.Width = 200;
                AgendaTable.Rows[0].Cells.Add(checkin);

                //integer that will be incremented in the foreach loop to access the new row for every iteration of the foreach
                int i = 1;
                foreach(SP_GetEmpAgenda a in agenda)
                {
                    TableRow r = new TableRow();
                    AgendaTable.Rows.Add(r);

                    getTimeCustomerServices(a.BookingID, a.PrimaryID, i, a);

                    TableCell present = new TableCell();
                    present.Width = 100;
                    present.Text = function.GetFullArrivedStatus(a.Arrived.ToString()[0]);
                    AgendaTable.Rows[i].Cells.Add(present);
                    
                    //check in BTN
                    if (function.GetFullArrivedStatus(a.Arrived.ToString()[0]) == "No")
                    {
                        //edit
                        TableCell buttonCell = new TableCell();
                        buttonCell.Text =
                            "<button type = 'button' class='btn btn-default'>" +
                        "<a href = '../ViewBooking.aspx?BookingID=" + a.BookingID.ToString().Replace(" ", string.Empty) +
                        "&Action=Edit'>Edit Booking</a></button>";
                        AgendaTable.Rows[i].Cells.Add(buttonCell);

                        //create cell that will be populated by the button and add to row.. cell index: 6
                        buttonCell = new TableCell();
                        buttonCell.Width = 200;
                        buttonCell.Height = 50;

                        //create button
                        btnCheckin = new Button();
                        btnCheckin.Text = "Check-in";
                        btnCheckin.CssClass = "btn btn-primary";
                        btnCheckin.Click += (ss, ee) => {
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

                                if (handler.BLL_CheckIn(checkIn))
                                {
                                    //if BLL_CheckIn successful and arrival status changed show user and refresh the page
                                    //Response.Write("<script>alert('Customer has been checked-in.');location.reload();</script>");
                                    Response.Redirect("../Receptionist/Receptionist.aspx?Action=CheckedIn&CustomerName="+
                                                        a.CustomerFName.ToString().Replace(" ", string.Empty)
                                                        +"&StylistName=" + drpEmpNames.SelectedItem.Text);
                                }
                                else
                                {
                                    //if BLL_CheckIn unsuccessful and arrival status was not changed tell the user to try again or report to admin
                                    phCheckInErr.Visible = true;
                                    lblCheckinErr.Text = "We are unable to check-in customer.<br/>"
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
                        buttonCell.Controls.Add(btnCheckin);
                        //add cell to row
                        AgendaTable.Rows[i].Cells.Add(buttonCell);
                    }
                    //check Out BTN
                    else if(function.GetFullArrivedStatus(a.Arrived.ToString()[0]) == "Yes")
                    {
                        //edit
                        TableCell emptybuttonCell = new TableCell();
                        emptybuttonCell.Text = "";
                        AgendaTable.Rows[i].Cells.Add(emptybuttonCell);

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
                function.logAnError(E.ToString());
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
                catch(ApplicationException serviceErr)
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
            catch(ApplicationException Err)
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
        #endregion

        #region Alerts
        Tuple<List<SP_GetAllAccessories>, List<SP_GetAllTreatments>> products = null;
        int alertCount = 0;
        int dashOutCount = 0;
        int dashLowCount = 0;

        private void addAlertToTable(string alertIcon, string alertType, string alertDescription)
        {
            alertsContainer.Visible = true;
            if (alertType != null
                && alertDescription != null)
            {
                TableRow newRow;
                //disply the table headers
                if (alertType == "Out Of Stock" & dashOutCount == 0)
                {
                    //create a new row in the table and set the height
                    newRow = new TableRow();
                    newRow.Height = 50;
                    tblAlerts.Rows.Add(newRow);
                    //create a header row and set cell withs
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 50;
                    tblAlerts.Rows[alertCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Out Of Stock";
                    newHeaderCell.Width = 950;
                    tblAlerts.Rows[alertCount].Cells.Add(newHeaderCell);

                    //increment rowcounter
                    alertCount++;
                }
                else if (alertType == "Low Stock" & dashLowCount == 0)
                {
                    //create a new row in the table and set the height
                    newRow = new TableRow();
                    newRow.Height = 50;
                    tblAlerts.Rows.Add(newRow);
                    //create a header row and set cell withs
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 50;
                    tblAlerts.Rows[alertCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Low Stock";
                    newHeaderCell.Width = 950;
                    tblAlerts.Rows[alertCount].Cells.Add(newHeaderCell);

                    //increment rowcounter
                    alertCount++;
                }

                //add the alert to the table
                //create a new row in the table and set the height
                newRow = new TableRow();
                newRow.Height = 50;
                tblAlerts.Rows.Add(newRow);
                //add the alert to the table
                TableCell newCell = new TableCell();
                newCell.Text = alertIcon;
                tblAlerts.Rows[alertCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = alertDescription;
                tblAlerts.Rows[alertCount].Cells.Add(newCell);

                //increment rowcounter
                alertCount++;
            }
        }

        private void checkForLowStock()
        {
            try
            {
                //load a list of all products
                products = handler.getAllProductsAndDetails();
                //sort the products by stock count
                products = Tuple.Create(products.Item1.OrderBy(o => o.Qty).ToList(),
                    products.Item2.OrderBy(o => o.Qty).ToList());

                #region out of stock
                //check for out of stock products
                //check out of stock treatments
                foreach (SP_GetAllTreatments treat in products.Item2)
                {
                    if (treat.Qty <= 0 && dashOutCount == 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#10071;", "Out Of Stock",
                            "<a  href='../Manager/Products.aspx?Action=NewOrder&" +
                                            "ProductID="
                            + treat.ProductID.ToString().Replace(" ", string.Empty) +
                            "&PreviousPage=Receptionist.aspx'>"
                             + treat.Name + "</a>");
                        dashOutCount++;
                    }
                    else if (treat.Qty <= 0 && dashOutCount > 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#10071;", "",
                            "<a href='../Manager/Products.aspx?Action=NewOrder&" +
                                            "ProductID="
                            + treat.ProductID.ToString().Replace(" ", string.Empty) +
                            "&PreviousPage=Receptionist.aspx'>"  + treat.Name + "</a>");
                        dashOutCount++;
                    }
                }
                //check out of stock accessories
                foreach (SP_GetAllAccessories Access in products.Item1)
                {
                    if (Access.Qty <= 0 && dashOutCount == 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#10071;", "Out Of Stock",
                            "<a  href='../Manager/Products.aspx?Action=NewOrder&" +
                                            "ProductID="
                            + Access.ProductID.ToString().Replace(" ", string.Empty) +
                            "&PreviousPage=Receptionist.aspx'>" +
                            "" + Access.Name + "</a>");
                        dashOutCount++;
                    }
                    else if (Access.Qty <= 0 && dashOutCount > 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#10071;", "",
                            "<a href='../Manager/Products.aspx?Action=NewOrder&" +
                                            "ProductID="
                            + Access.ProductID.ToString().Replace(" ", string.Empty) +
                            "&PreviousPage=Receptionist.aspx'>"+
                            "" + Access.Name + "</a>");
                        dashOutCount++;
                    }
                }
                #endregion
                #region Low Stock
                //check for low stock
                //check low stock treatments
                foreach (SP_GetAllTreatments treat in products.Item2)
                {
                    if (treat.Qty < 10 && treat.Qty > 0 && dashLowCount == 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#9888;", "Low Stock",
                            "<a  href='../Manager/Products.aspx?Action=NewOrder&" +
                                            "ProductID="
                            + treat.ProductID.ToString().Replace(" ", string.Empty) +
                            "&PreviousPage=Receptionist.aspx'>"+
                            "" + treat.Name + "</a><br/> "
                            + treat.Qty + " Left in stock");
                        dashLowCount++;
                    }
                    else if (treat.Qty < 10 && treat.Qty > 0 && dashLowCount > 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#9888;", "",
                            " <a  href='../Manager/Products.aspx?Action=NewOrder&" +
                                            "ProductID="
                            + treat.ProductID.ToString().Replace(" ", string.Empty) +
                            "&PreviousPage=Receptionist.aspx'>" +
                            "" + treat.Name + "</a><br/> "
                            + treat.Qty + " Left in stock");
                        dashLowCount++;
                    }
                }
                //check low stock accessories
                foreach (SP_GetAllAccessories Access in products.Item1)
                {
                    if (Access.Qty < 10 && Access.Qty > 0 && dashLowCount == 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#9888;", "Low Stock",
                            "<a  href='../Manager/Products.aspx?Action=NewOrder&" +
                                            "ProductID="
                            + Access.ProductID.ToString().Replace(" ", string.Empty) +
                            "&PreviousPage=Receptionist.aspx'>" +
                            "" + Access.Name + "</a><br/>"
                            + Access.Qty + " Left in stock");
                        dashLowCount++;
                    }
                    else if (Access.Qty < 10 && Access.Qty > 0 && dashLowCount > 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#9888;", "",
                            "<a  href='../Manager/Products.aspx?Action=NewOrder&" +
                                            "ProductID="
                            + Access.ProductID.ToString().Replace(" ", string.Empty) +
                            "&PreviousPage=Receptionist.aspx'>" +
                            "" + Access.Name + "</a><br/> "
                            + Access.Qty + " Left in stock");
                        dashLowCount++;
                    }
                }
                #endregion
            }
            catch (Exception Err)
            {
                addAlertToTable("", "Error", "An error occurred loading all alerts");
                function.logAnError("unable to load alerts on Receptionist/Dashboard.aspx: " +
                    Err);
            }
        }
        #endregion

        #region Make internal booking
        protected void btnMakeInternalBooking_Click(object sender, EventArgs e)
        {
            Response.Redirect("../MakeABooking.aspx?Type=Internal");
        }
        #endregion

        #region Register New Customer
        protected void btnNewCust_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Authentication/NewAccount.aspx?Type=NewCust");
        }
        #endregion
    }
}
