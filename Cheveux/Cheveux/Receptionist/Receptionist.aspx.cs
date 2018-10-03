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

                if(alertCount == 0)
                {
                    alertsContainer.Visible = false;
                }
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
                        list = handler.BLL_GetEmpNames();
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
                        }
                    }
                    catch (Exception Err)
                    {
                        drpEmpNames.Items.Add("Stylist names unavailable");
                        function.logAnError(Err.ToString());
                    }
                }

                try
                {
                        getAgenda(drpEmpNames.SelectedValue, DateTime.Parse(bookingDate),null,null);

                        if (AgendaTable.Rows.Count == 1)
                        {
                            
                            if (drpEmpNames.Text== "Stylist names unavailable")
                            {
                            AgendaTable.Visible = false;
                            noBookingsPH.Visible = false;
                            }
                            else
                            {
                            AgendaTable.Visible = false;
                            noBookingsPH.Visible = true;
                            lblNoBookings.Text = drpEmpNames.SelectedItem.Text +
                                                                                " has not been appointed to any bookings today."
                                                                                + "<br/>Refresh for updated stylists appointments.";
                            }
                        }
                        else if(AgendaTable.Rows.Count > 1)
                        {
                            noBookingsPH.Visible = false;
                            AgendaTable.Visible = true;
                        }
                        
                }
                catch (Exception Err)
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
                    lblSuccess.Text = custName.ToString() + " has been checked in for their booking with "
                                    + stylistName.ToString();
                }
                #endregion

                loadOutOrders();
            }
        }
        
        #region Agenda
        protected void drpEmpNames_Changed(object sender, EventArgs e)
        {
            phCheckInSuccess.Visible = false;    
        }

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
                        TableCell buttonCell = new TableCell();
                        if ((a.StartTime.TimeOfDay >= DateTime.Now.TimeOfDay))
                        {
                            //edit
                            buttonCell.Text =
                                "<button type = 'button' class='btn btn-default'>" +
                            "<a href = '../ViewBooking.aspx?BookingID=" + a.BookingID.ToString().Replace(" ", string.Empty) +
                            "&Action=Edit'>Edit Booking</a></button>";
                            AgendaTable.Rows[i].Cells.Add(buttonCell);
                        }

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
                            catch (Exception err)
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
            catch(Exception E)
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
                catch(Exception serviceErr)
                {
                    function.logAnError("Error getting services [receptionist.aspx {tryCatch within getTime  method }]err:" + serviceErr.ToString());
                }    

                if (bServices.Count == 1)
                {
                    start.Text = a.StartTime.ToString("HH:mm");
                    AgendaTable.Rows[i].Cells.Add(start);

                    end.Text = a.EndTime.ToString("HH:mm");
                    AgendaTable.Rows[i].Cells.Add(end);
                }
                else if (bServices.Count >= 2)
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
                function.logAnError("Couldn't get the time (check db for 2nd bkID) [receptionist.aspx {getTimeCustomerServices?getTime}] error:"
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
            catch(Exception Err)
            {
                //if theres an error or cant retrieve the services from the database 
                services.Text = "Unable to retreive services";
                AgendaTable.Rows[i].Cells.Add(services);
                function.logAnError("Couldn't get the services [receptionist.aspx {getTimeCustomerServices?getServices} ] error:" + Err.ToString());
            }

            
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
                            "<a href='../Manager/Products.aspx?ProductID="
                            + treat.ProductID.ToString() + "&Action=NewOrder" +
                                            "'>" + treat.Name + "</a>");
                        dashOutCount++;
                    }
                    else if (treat.Qty <= 0 && dashOutCount > 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#10071;", "",
                            "<a href='../Manager/Products.aspx?ProductID="
                            + treat.ProductID.ToString() + "&Action=NewOrder" +
                                            "'>" + treat.Name + "</a>");
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
                            "<a href='../Manager/Products.aspx?ProductID="
                            + Access.ProductID.ToString() + "&Action=NewOrder" +
                                            "'>" + Access.Name + "</a>");
                        dashOutCount++;
                    }
                    else if (Access.Qty <= 0 && dashOutCount > 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#10071;", "",
                            "<a href='../Manager/Products.aspx?ProductID="
                            + Access.ProductID.ToString() + "&Action=NewOrder" +
                                            "'>" + Access.Name + "</a>");
                        dashOutCount++;
                    }
                }
                #endregion

                #region Low Stock
                int lowStock = handler.getStockSettings().LowStock;
                //check for low stock
                //check low stock treatments
                foreach (SP_GetAllTreatments treat in products.Item2)
                {
                    if (treat.Qty < lowStock && treat.Qty > 0 && dashLowCount == 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#9888;", "Low Stock",
                            "<a href='../Manager/Products.aspx?ProductID="
                            + treat.ProductID.ToString() + "&Action=NewOrder" +
                                            "'>" + treat.Name + "</a><br/> "
                            + treat.Qty + " Left in stock");
                        dashLowCount++;
                    }
                    else if (treat.Qty < lowStock && treat.Qty > 0 && dashLowCount > 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#9888;", "",
                            " <a href='../Manager/Products.aspx?ProductID="
                            + treat.ProductID.ToString() + "&Action=NewOrder" +
                                            "'>" + treat.Name + "</a><br/> "
                            + treat.Qty + " Left in stock");
                        dashLowCount++;
                    }
                }
                //check low stock accessories
                foreach (SP_GetAllAccessories Access in products.Item1)
                {
                    if (Access.Qty < lowStock && Access.Qty > 0 && dashLowCount == 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#9888;", "Low Stock",
                            "<a href='../Manager/Products.aspx?ProductID="
                            + Access.ProductID.ToString() + "&Action=NewOrder" +
                                            "'>" + Access.Name + "</a><br/>"
                            + Access.Qty + " Left in stock");
                        dashLowCount++;
                    }
                    else if (Access.Qty < lowStock && Access.Qty > 0 && dashLowCount > 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#9888;", "",
                            "<a href='../Manager/Products.aspx?ProductID="
                            + Access.ProductID.ToString() + "&Action=NewOrder" +
                                            "'>" + Access.Name + "</a><br/> "
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

        #region List Outstanding Orders
        private void loadOutOrders()
        {
            try
            {
                List<OrderViewModel> outOrders = handler.getOutStandingOrders();
                //check if there are outstanding orders
                if (outOrders.Count > 0)
                {
                    //if there are bookings desplay them
                    //create a new row in the uppcoming bookings table and set the height
                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblOutstandingOrders.Rows.Add(newRow);
                    //create a header row and set cell withs
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Date Orderd";
                    newHeaderCell.Width = 400;
                    tblOutstandingOrders.Rows[0].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Supplier";
                    newHeaderCell.Width = 800;
                    tblOutstandingOrders.Rows[0].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Items Out Standing";
                    newHeaderCell.Width = 400;
                    tblOutstandingOrders.Rows[0].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 400;
                    tblOutstandingOrders.Rows[0].Cells.Add(newHeaderCell);

                    //create a loop to display each result
                    //creat a counter to keep track of the current row
                    int rowCount = 1;
                    foreach (OrderViewModel outOrder in outOrders)
                    {
                        List<OrderViewModel> outOrderProducts = handler.getProductOrderDL(outOrder.OrderID.ToString());

                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblOutstandingOrders.Rows.Add(newRow);
                        //fill the row with the data from the results object
                        TableCell newCell = new TableCell();
                        newCell.Text = outOrder.orderDate.ToString("dd MMM yyyy");
                        tblOutstandingOrders.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = "<a href='../Manager/Products.aspx?Action=Viewsup" +
                                        "&supID=" + outOrder.supplierID.Replace(" ", string.Empty) +
                                        "'>" + outOrder.supplierName + "</a>";
                        tblOutstandingOrders.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        int outsandingItemCount = 0;
                        foreach (OrderViewModel outOrderDL in outOrderProducts)
                        {
                            outsandingItemCount += outOrderDL.Qty;
                        }
                        string dropDown = "<li style='list-style: none;' class='dropdown'>" +
                                "<a class='dropdown-toggle' data-toggle='dropdown' href='#'>";
                        dropDown += outsandingItemCount;
                        dropDown += "<span class='caret'></span></a>" +
                                            "<ul class='dropdown-menu bg-dark text-white'>";
                        foreach (OrderViewModel outOrderDL in outOrderProducts)
                        {
                            dropDown += "<li>&nbsp;<a href='/cheveux/products.aspx?ProductID=" + outOrderDL.ProductID.Replace(" ", string.Empty) + "'>" +
                                    " " + outOrderDL.Name.ToString() + " </a>&nbsp;</li>";
                        }
                        dropDown += "</ul></li>";
                        newCell.Text = dropDown;
                        tblOutstandingOrders.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = "<button type = 'button' class='btn btn-default'>" +
                            "<a href='../Manager/Products.aspx?Action=ViewOrder&OrderID=" + outOrder.OrderID.ToString() +
                            "'> View </a></button>";
                        tblOutstandingOrders.Rows[rowCount].Cells.Add(newCell);
                        rowCount++;
                    }

                    if (rowCount == 1)
                    {
                        // if there aren't let the user know
                        outstandingOrdersLable.Text =
                            "<p> No Outstanding Orders </p>";
                        tblOutstandingOrders.Visible = false;
                    }
                    else
                    {
                        // set the booking copunt
                        outstandingOrdersLable.Text =
                            "<p> " + (rowCount - 1) + " Outstanding Orders </p>";
                    }
                }
                else
                {
                    // if there aren't let the user know
                    outstandingOrdersLable.Text =
                        "<p> No outstanding Orders </p>";
                }
            }
            catch (Exception err)
            {
                function.logAnError("Error loading outstanding product orders on internal product page | Error: " + err.ToString());
                outstandingOrdersLable.Visible = true;
                tblOutstandingOrders.Visible = false;
                outstandingOrdersLable.Text =
                        "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }
        }
        #endregion

        #region New Sale
        protected void btnNewSale_Click(object sender, EventArgs e)
        {
            Response.Redirect("../ViewBooking.aspx?BookingType=CheckOut&CheckOutType=NewSale");
        }
        #endregion
    }
}
