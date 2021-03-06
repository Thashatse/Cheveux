﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;
using System.Threading.Tasks;

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
            Parallel.Invoke(() => loadPage(sender, e), () => function.runAutoFunctions());
        }

        private void loadPage(object sender, EventArgs e)
        {
            //check if the user is loged out
            cookie = Request.Cookies["CheveuxUserID"];

            if (cookie == null)
            {
                //if the user is not loged in as a manager do not display Bussines setting
            }
            else if (cookie["UT"] != "M")
            {
                Response.Redirect("../Default.aspx");
            }
            else if (cookie["UT"] == "M")
            {
                #region stats 
                //load stats
                ManagerStats stats = handler.GetManagerStats();

                //display stats
                lStats1.Text = "R" + Math.Round(stats.sales, 2).ToString();
                lStats2.Text = stats.upcomingBookings.ToString();
                lStats3.Text = stats.totalBookings.ToString();
                lStats4.Text = stats.registeredCustomers.ToString();
                #endregion

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
                //load outstanding stook oders
                loadOutOrders();
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
                            + treat.ProductID.ToString()+"&Action=NewOrder" +
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

        private void addAlertToTable(string alertIcon, string alertType, string alertDescription)
        {
            alertsContainer.Visible = true;
            if (alertType != null
                && alertDescription != null)
            {
                TableRow newRow;
                //disply the table headers
                if (alertType == "Out Of Stock" & dashOutCount == 0 )
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
                            //Services
                            List<SP_GetBookingServices> bookingServiceList = null;
                            //get the booking services
                            try
                            {
                                bookingServiceList = handler.getBookingServices(booking.BookingID.ToString());
                            }
                            catch (Exception Err)
                            {
                                function.logAnError("Error Loading Booking Services in manager dashboard todays bookings (Upcoming) Error:" +
                                    Err.ToString());
                            }
                            newCell = new TableCell();
                            if (bookingServiceList.Count == 1)
                            {
                                newCell.Text = "<a href='ViewProduct.aspx?ProductID=" + bookingServiceList[0].ServiceID.Replace(" ", string.Empty) + "'>"
                                + bookingServiceList[0].ServiceName.ToString() + "</a>";
                            }
                            else if (bookingServiceList.Count > 1)
                            {
                                string dropDown = "<li style='list-style: none;' class='dropdown'>" +
                                    "<a class='dropdown-toggle' data-toggle='dropdown' href='#'>";
                                if (bookingServiceList.Count == 2)
                                {
                                    dropDown += bookingServiceList[0].ServiceName.ToString() +
                                    ", " + bookingServiceList[1].ServiceName.ToString();
                                }
                                else if (bookingServiceList.Count > 2)
                                {
                                    dropDown += " Multiple ";
                                }
                                dropDown += "<span class='caret'></span></a>" +
                                                "<ul class='dropdown-menu bg-dark text-white'>";
                                foreach (SP_GetBookingServices service in bookingServiceList)
                                {
                                    dropDown += "<li>&nbsp;<a href='/cheveux/services.aspx?ProductID=" + service.ServiceID.Replace(" ", string.Empty) + "'>" +
                                        " " + service.ServiceName.ToString() + " </a>&nbsp;</li>";
                                }
                                dropDown += "</ul></li>";

                                newCell.Text = dropDown;
                            }
                            tblBookings.Rows[bookingCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            if (function.GetFullArrivedStatus(booking.Arrived.ToString()[0]) == "No")
                            {
                                newCell.Text = "Not Arrived";
                            }
                            else if (function.GetFullArrivedStatus(booking.Arrived.ToString()[0]) == "Yes")
                            {
                                newCell.Text = "Arrived";
                            }
                            tblBookings.Rows[bookingCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = "<button type = 'button' class='btn btn-default'>" +
                            "<a href = '../ViewBooking.aspx?BookingID=" + booking.BookingID.ToString().Replace(" ", string.Empty) +
                            "&BookingType=Past" +
                            "&PreviousPage=../Manager/Dashboard.aspx'>View</a></button>";
                            tblBookings.Rows[bookingCount].Cells.Add(newCell);

                            //increment rowcounter
                            bookingCount++;
                        
                    }
                    
                    //set the bookings count
                    bookingsLable.Text = bookingCount-1 + " Bookings";
                }
                else if (todaysBookings.Count == 0)
                {
                    //set the bookings count
                    bookingsLable.Text = "No bookings today";
                    tblBookings.Visible = false;
                }
                else if (todaysBookings.Count < 0)
                {
                    //let the user know an error occoured
                    bookingsLable.Text = "An error occurred loading all bookings";
                    tblBookings.Visible = false;
                }

            }
            catch (Exception Err)
            {
                bookingsLable.Text = "An error occurred loading todays bookings";
                function.logAnError("unable to load todays bookings on Manager/Dashboard.aspx: " +
                    Err);
            }
        }

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
                        newCell.Text = "<a href='/Manager/Products.aspx?Action=Viewsup" +
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
                            "<a href='/Manager/Products.aspx?Action=ViewOrder&OrderID=" + outOrder.OrderID.ToString() +
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
    }
}