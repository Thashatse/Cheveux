using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;

namespace Cheveux.Manager
{
    public partial class Manager : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;
        Tuple<List<SP_GetAllAccessories>, List<SP_GetAllTreatments>> products = null;
        int alertCount = 0;
        List<SP_GetTodaysBookings> todaysBookings = null;
        int bookingCount = 0;
        int dashOutCount = 0;
        int dashLowCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is loged out
            cookie = Request.Cookies["CheveuxUserID"];

            if (cookie == null)
            {
                //if the user is not loged in as a manager do not display Bussines setting
            }else if(cookie["UT"] != "M")
            {
                Response.Redirect("../Default.aspx");
            }
            else if (cookie["UT"] == "M")
            {
                //if the user is loged in as a manager display the dashboard
                LogedIn.Visible = true;
                LogedOut.Visible = false;
                //display the headings
                string wB = Request.QueryString["WB"];
                if (wB == "True")
                {
                    Welcome.Text = "Welcome Back " + handler.GetUserDetails(cookie["ID"]).FirstName;
                }
                lJumbotronDate.Text = DateTime.Today.ToString("D");
                //check for any alerts
                //low stock alert
                checkForLowStock();
                //load to days bookings 
                loadTodaysBookings();
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
                            "&PreviousPage=../Manager/Dashboard.aspx'>" +
                            "The Treatment '" + treat.Name + "' is currently out off stock</a>");
                        dashOutCount++;
                    }
                    else if (treat.Qty <= 0 && dashOutCount > 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#10071;", "",
                            "<a href = '#?ProductID="
                            + treat.ProductID.ToString().Replace(" ", string.Empty) +
                            "&PreviousPage=../Manager/Dashboard.aspx'>" +
                            "The Treatment '" + treat.Name + "' is currently out off stock</a>");
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
                            "The Accessory '" + Access.Name + "' is currently out of stock</a>");
                        dashOutCount++;
                    }
                    else if (Access.Qty <= 0 && dashOutCount > 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#10071;", "",
                            "<a href = '#?ProductID="
                            + Access.ProductID.ToString().Replace(" ", string.Empty) +
                            "&PreviousPage=../Manager/Dashboard.aspx'>" +
                            "The Accessory '" + Access.Name + "' is currently out of stock</a>");
                        dashOutCount++;
                    }
                }
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
                            "&PreviousPage=../Manager/Dashboard.aspx'>" +
                            "The Treatment '" + treat.Name + "' is currently runing low on stock with "
                            + treat.Qty + " Left in stock </a>");
                        dashLowCount++;
                    }
                    else if (treat.Qty < 10 && treat.Qty > 0 && dashLowCount > 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#9888;", "",
                            " <a href = '#?ProductID="
                            + treat.ProductID.ToString().Replace(" ", string.Empty) +
                            "&PreviousPage=../Manager/Dashboard.aspx'>" +
                            "The Treatment '" + treat.Name + "' is currently runing low on stock with "
                            + treat.Qty + " Left in stock </a>");
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
                            "&PreviousPage=../Manager/Dashboard.aspx'>" +
                            "The Accessory '" + Access.Name + "' is currently runing low on stock with "
                            + Access.Qty + " Left in stock</a>");
                        dashLowCount++;
                    }
                    else if (Access.Qty < 10 && Access.Qty > 0 && dashLowCount > 0)
                    {
                        //if the accessory is low and stock add an alert to the alert table
                        addAlertToTable("&#9888;", "",
                            "<a href = '#?ProductID="
                            + Access.ProductID.ToString().Replace(" ", string.Empty) +
                            "&PreviousPage=../Manager/Dashboard.aspx'>" +
                            "The Accessory '" + Access.Name + "' is currently runing low on stock with "
                            + Access.Qty + " Left in stock</a>");
                        dashLowCount++;
                    }
                }
            }
            catch (Exception Err)
            {
                addAlertToTable("", "Error", "An error occurred loading all alerts");
                function.logAnError("unable to load alerts on Manager/Dashboard.aspx: " +
                    Err);
            }
        }

        private void addAlertToTable(string alertIcon, string alertType, string alertDescription)
        {
            alertsContainer.Visible = true;
            if (alertType != null
                && alertDescription != null)
            {
                TableRow newRow;
                //disply the table headers
                if (alertCount == 0)
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
                    newHeaderCell.Text = "Type: ";
                    newHeaderCell.Width = 200;
                    tblAlerts.Rows[alertCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Description: ";
                    newHeaderCell.Width = 750;
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
                newCell.Text = alertType;
                tblAlerts.Rows[alertCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = alertDescription;
                tblAlerts.Rows[alertCount].Cells.Add(newCell);

                //increment rowcounter
                alertCount++;
            }
        }

        private void loadTodaysBookings()
        {
            //load todays bookings from the database and diplays them in the bookings table
            try
            {
                //load todays bookings
                todaysBookings = handler.getTodaysBookings();
                //check if the are any bookings today
                if (todaysBookings.Count > 0)
                {
                    //set the table headings
                    //create a new row in the table and set the height
                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblBookings.Rows.Add(newRow);
                    //create a header row and set cell withs
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Time: ";
                    newHeaderCell.Width = 100;
                    tblBookings.Rows[bookingCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Customer: ";
                    newHeaderCell.Width = 250;
                    tblBookings.Rows[bookingCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Description: ";
                    newHeaderCell.Width = 300;
                    tblBookings.Rows[bookingCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Status: ";
                    newHeaderCell.Width = 250;
                    tblBookings.Rows[bookingCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    //view booking details
                    newHeaderCell.Width = 100;
                    tblBookings.Rows[bookingCount].Cells.Add(newHeaderCell);

                    //increment rowcounter
                    bookingCount++;

                    //fill the table with bookings
                    //upcoming bookings 
                    foreach(SP_GetTodaysBookings booking in todaysBookings)
                    {
                        if (booking.StartTime < DateTime.Now)
                        {
                            //create a new row in the table and set the height
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblBookings.Rows.Add(newRow);
                            //fill the wrow with data
                            TableCell newCell = new TableCell();
                            newCell.Text = booking.StartTime.ToString("HH:mm");
                            tblBookings.Rows[bookingCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + booking.CustomerID.ToString().Replace(" ", string.Empty) +
                                    "&PreviousPage=Dashboard.aspx'>" + booking.CustomerFirstName + " " + booking.CustomerLastName + "</a>";
                            tblBookings.Rows[bookingCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = "<a href = 'ViewProduct.aspx?ProductID=" + booking.ServiceID.ToString().Replace(" ", string.Empty) +
                                    "&PreviousPage=Dashboard.aspx'>"+ booking.ServiceName + " with " + 
                                    handler.viewEmployee(booking.StylistID).firstName+"</a>";
                            tblBookings.Rows[bookingCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            if (function.GetFullArrivedStatus(booking.Arrived.ToString()[0]) == "No")
                            {
                                newCell.Text = "Customer Has Not Arived";
                            }
                            else if (function.GetFullArrivedStatus(booking.Arrived.ToString()[0]) == "Yes")
                            {
                                newCell.Text = "Customer Has Arived";
                            }
                            tblBookings.Rows[bookingCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = "<button type = 'button' class='btn btn-default'>" +
                            "<a href = '../ViewBooking.aspx?BookingID=" + booking.BookingID.ToString().Replace(" ", string.Empty) +
                            "&BookingType=Past" +
                            "&PreviousPage=../Manager/Dashboard.aspx'>View Details</a></button>";
                            tblBookings.Rows[bookingCount].Cells.Add(newCell);

                            //increment rowcounter
                            bookingCount++;
                        }
                    }

                    //past bookings 
                    foreach (SP_GetTodaysBookings booking in todaysBookings)
                    {
                        if (booking.StartTime > DateTime.Now)
                        {
                            //create a new row in the table and set the height
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblBookings.Rows.Add(newRow);
                            //fill the wrow with data
                            TableCell newCell = new TableCell();
                            newCell.Text = booking.StartTime.ToString("HH:mm");
                            tblBookings.Rows[bookingCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + booking.CustomerID.ToString().Replace(" ", string.Empty) +
                                    "&PreviousPage=Dashboard.aspx'>" + booking.CustomerFirstName + " " + booking.CustomerLastName + "</a>";
                            tblBookings.Rows[bookingCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = "<a href = 'ViewProduct.aspx?ProductID=" + booking.ServiceID.ToString().Replace(" ", string.Empty) +
                                    "&PreviousPage=Dashboard.aspx'>" + booking.ServiceName + " with " +
                                    handler.viewEmployee(booking.StylistID).firstName + "</a>";
                            tblBookings.Rows[bookingCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            if (function.GetFullArrivedStatus(booking.Arrived.ToString()[0]) == "No")
                            {
                                newCell.Text = "Customer Has Not Arived";
                            }
                            else if (function.GetFullArrivedStatus(booking.Arrived.ToString()[0]) == "Yes")
                            {
                                newCell.Text = "Customer Has Arived";
                            }
                            tblBookings.Rows[bookingCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = "<button type = 'button' class='btn btn-default'>" +
                            "<a href = '../ViewBooking.aspx?BookingID=" + booking.BookingID.ToString().Replace(" ", string.Empty) +
                            "&BookingType=Past" +
                            "&PreviousPage=../Manager/Dashboard.aspx'>View Details</a></button>";
                            tblBookings.Rows[bookingCount].Cells.Add(newCell);

                            //increment rowcounter
                            bookingCount++;
                        }
                    }

                    //set the bookings count
                    bookingsLable.Text = bookingCount-1 + " Bookings";
                }
                else if (todaysBookings.Count == 0)
                {
                    //set the bookings count
                    bookingsLable.Text = "No bookings today";
                }
                else if (todaysBookings.Count < 0)
                {
                    //let the user know an error occoured
                    bookingsLable.Text = "An error occurred loading all bookings";
                }

            }
            catch (Exception Err)
            {
                bookingsLable.Text = "An error occurred loading all bookings";
                function.logAnError("unable to load topdays bookings on Manager/Dashboard.aspx: " +
                    Err);
            }
        }

    }
}