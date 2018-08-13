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
        BOOKING checkIn = null;
        HttpCookie cookie = null;
        string currentBookingStylistID = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            errorHeader.Font.Bold = true;
            errorHeader.Font.Underline = true;
            errorHeader.Font.Size = 21;
            errorMessage.Font.Size = 14;

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

                #region Alerts
                //Check For Low Stock
                checkForLowStock();
                #endregion

                lblDate.Text = dayDate;
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
                        #region Make internal booking`
                        displayMAB(drpEmpNames.SelectedValue);
                        #endregion

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
                    errorMessage.Text = "It seems there is a problem retrieving employees bookings.<br/>"
                                        + "Please report problem or try again later.";
                    function.logAnError(Err.ToString());
                }
            }   
        }

        #region Agenda
        public void getAgenda(string id, DateTime bookingDate)
        {
            Button btn;

            try
            {
                agenda = handler.BLL_GetEmpAgenda(id, bookingDate);

                AgendaTable.CssClass = "table table-light table-hover table-bordered";
                
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

                TableCell checkin = new TableCell();
                checkin.Width = 200;
                AgendaTable.Rows[0].Cells.Add(checkin);

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
                    start.Text = a.StartTime.ToString("HH:mm");
                    AgendaTable.Rows[i].Cells.Add(start);

                    //create end cell and add to row.. cell index: 1
                    TableCell end = new TableCell();
                    end.Width = 200;
                    end.Text = a.EndTime.ToString("HH:mm");
                    AgendaTable.Rows[i].Cells.Add(end);

                    //created customer name cell and add to row.. cell index: 2
                    TableCell c = new TableCell();
                    c.Width = 300;
                    c.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + a.UserID.ToString().Replace(" ", string.Empty) +
                                    "'>" + a.CustomerFName.ToString() + "</a>";
                    AgendaTable.Rows[i].Cells.Add(c);

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
                function.logAnError(E.ToString());
            }
        }
        #endregion

        #region Alerts
        Tuple<List<SP_GetAllAccessories>, List<SP_GetAllTreatments>> products = null;
        int alertCount = 0;
        int dashOutCount = 0;
        int dashLowCount = 0;

        private void addAlertToTable(string alertIcon, string alertType, string alertDescription)
        {
            tblAlerts.CssClass = "table table-dark table-hover table-bordered";

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
                            "<a href = '#?ProductID="
                            + treat.ProductID.ToString().Replace(" ", string.Empty) +
                            "'>"
                             + treat.Name + "</a>");
                        dashOutCount++;
                    }
                    else if (treat.Qty <= 0 && dashOutCount > 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#10071;", "",
                            "<a href = '#?ProductID="
                            + treat.ProductID.ToString().Replace(" ", string.Empty) +
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
                            "<a href = '#?ProductID="
                            + Access.ProductID.ToString().Replace(" ", string.Empty) +
                            "&PreviousPage=../Manager/Dashboard.aspx'>" +
                            "" + Access.Name + "</a>");
                        dashOutCount++;
                    }
                    else if (Access.Qty <= 0 && dashOutCount > 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#10071;", "",
                            "<a href = '#?ProductID="
                            + Access.ProductID.ToString().Replace(" ", string.Empty) +
                            "'>" +
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
                            " <a href = '#?ProductID="
                            + treat.ProductID.ToString().Replace(" ", string.Empty) +
                            "'>" +
                            "" + treat.Name + "</a><br/> "
                            + treat.Qty + " Left in stock");
                        dashLowCount++;
                    }
                    else if (treat.Qty < 10 && treat.Qty > 0 && dashLowCount > 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#9888;", "",
                            " <a href = '#?ProductID="
                            + treat.ProductID.ToString().Replace(" ", string.Empty) +
                            "'>" +
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
                            "<a href = '#?ProductID="
                            + Access.ProductID.ToString().Replace(" ", string.Empty) +
                            "'>" +
                            "" + Access.Name + "</a><br/>"
                            + Access.Qty + " Left in stock");
                        dashLowCount++;
                    }
                    else if (Access.Qty < 10 && Access.Qty > 0 && dashLowCount > 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#9888;", "",
                            "<a href = '#?ProductID="
                            + Access.ProductID.ToString().Replace(" ", string.Empty) +
                            "'>" +
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
                function.logAnError("unable to load alerts on Manager/Dashboard.aspx: " +
                    Err);
            }
        }
        #endregion

        #region Make internal booking
        public void displayMAB(string stylistID)
        {
            //display the make a booking panel
            try
            {
                USER stylist = handler.GetUserDetails(stylistID);
                lMABforStylist.Text = "with " + stylist.FirstName + " " + stylist.LastName;
                makeABookingContainer.Visible = true;
            }
            catch (Exception err)
            {
                calMAB.Visible = false;
                btnMakeInternalBooking.Visible = false;
                lMABforStylist.Text = "Make A Booking Unavailable, Try Again Later";
                makeABookingContainer.Visible = true;
                function.logAnError("Error loading make a booking panel on receptionist page for stylist id: " +
                    stylistID + " | Error: " + err);
            }
        }

        protected void prossesBooking(object sender, EventArgs e)
        {
            try
            {
                if (calMAB.SelectedDate.ToString() != "0001/01/01 00:00:00" 
                    && drpAvailableTimes.Visible == false)
                {
                    drpAvailableTimes.Visible = true;
                }

                //if time selected redirect to service & customer slection
                if (drpAvailableTimes.SelectedValue != "-1" 
                    && btnMakeInternalBooking.Visible == false)
                {
                    btnMakeInternalBooking.Visible = true;
                }
            }
            catch (Exception err)
            {
                calMAB.Visible = false;
                btnMakeInternalBooking.Visible = false;
                drpAvailableTimes.Visible = false;
                lMABforStylist.Text = "Make A Booking Unavailable, Try Again Later";
                makeABookingContainer.Visible = true;
                function.logAnError("Error loading avalible times and redirecting to make a booking page on receptionist page for stylist id: " +
                    drpEmpNames.SelectedValue.Replace(" ", string.Empty) +
                    "Date: " + calMAB.SelectedDate +
                    "Time (SlotNo.): " + drpAvailableTimes.SelectedValue +
                    " | Error: " + err);
            }
        }
        
        //remove dates befor today
        protected void calMAB_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date <= DateTime.Now)
            {
                e.Cell.BackColor = ColorTranslator.FromHtml("#a9a9a9");
                e.Day.IsSelectable = false;
            }
        }

        protected void btnMakeInternalBooking_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Receptionist/MakeInternalBooking.aspx"
                        + "?StyID=" + drpEmpNames.SelectedValue.Replace(" ", string.Empty) +
                        "&Date=" + calMAB.SelectedDate.ToString("yyyy-MM-dd") +
                        "&Time=" + drpAvailableTimes.SelectedValue.Replace(" ", string.Empty));
        }
        
        //cleares booking panel when new stylist selected
        protected void clearBooking(object sender, EventArgs e)
        {
            btnMakeInternalBooking.Visible = false;
            drpAvailableTimes.Visible = false;
            calMAB.SelectedDate = Convert.ToDateTime("0001/01/01 00:00:00");
        }
        
        protected void calMAB_SelectionChanged(object sender, EventArgs e)
        {
            #region Avalible Times
            List<SP_GetBookedTimes> bookedList = handler.BLL_GetBookedStylistTimes(
                drpEmpNames.SelectedValue.Replace(" ", string.Empty), calMAB.SelectedDate);
            List<SP_GetSlotTimes> slotList = handler.BLL_GetAllTimeSlots();
            slotList = slotList.OrderBy(o => o.Time).ToList();
            drpAvailableTimes.Items.Clear();
            if (bookedList.Count != 0)
            {
                foreach (SP_GetBookedTimes booked in bookedList)
                {
                    foreach (SP_GetSlotTimes times in slotList)
                    {
                        if (booked.SlotNo != times.SlotNo)
                        {
                            drpAvailableTimes.Items.Add(new ListItem(times.Time.ToString("hh:mm"),
                            times.SlotNo));
                        }
                    }
                }
            }
            else
            {
                foreach (SP_GetSlotTimes times in slotList)
                {
                    drpAvailableTimes.Items.Add(new ListItem(times.Time.ToString("hh:mm"),
                    times.SlotNo));
                }
            }
            drpAvailableTimes.Items.Insert(0, new ListItem("--Select Time--", "-1"));
            #endregion

            prossesBooking(sender, e);
        }
        #endregion
    }
}
