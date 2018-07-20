using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;

namespace Cheveux
{
    public partial class Bookings : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;
        List<SP_GetCustomerBooking> bookingsList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //access control
            cookie = Request.Cookies["CheveuxUserID"];
            //send the user to the correct page based on their usertype
            if (cookie != null)
            {
                string userType = cookie["UT"].ToString();
                if ("R" == userType)
                {
                    //Receptionist
                    Response.Redirect("/Receptionist/Receptionist.aspx");
                }
                else if (userType == "M")
                {
                    //Manager
                    Response.Redirect("/Manager/dashboard.aspx");
                }
                else if (userType == "S")
                {
                    //stylist
                    Response.Redirect("/Stylist/Stylist.aspx");
                }
                else if (userType == "C")
                {
                    //customer
                    //allowed access to this page
                    //Response.Redirect("Default.aspx");

                    //if the user is loged in diplay and futer services
                    JumbotronLogedIn.Visible = true;
                    JumbotronLogedOut.Visible = false;

                    //load the users uppcoming bookings in top the upcoming bookins tab
                    displayUpcomingBookings();

                    //load the users past bookings in top the past bookins tab
                    displayPastBookings();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                    function.logAnError("Unknown user type found during Loading of default.aspx: " +
                        cookie["UT"].ToString());
                }
            }
            else
            {
                //ask the user to login first 
                //check if the user is loged in before displaying past and futcher bokings
                    //if the user is not loged in do not diplay and futer services
                    Tabs.Visible = false;
            }
        }

        public void displayUpcomingBookings()
        {
            //get the uppcoming bookins from the Data Base
            try
            {
                bookingsList = handler.getCustomerUpcomingBookings(cookie["ID"].ToString());
            }
            catch (ApplicationException Err)
            {
                function.logAnError(Err.ToString());
                upcomingBookingsLable.Text =
                    "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }
            //check if there are upcoming bookings
            if (bookingsList.Count > 0)
            {
                //if there are bookings desplay them
                //create a new row in the uppcoming bookings table and set the height
                TableRow newRow = new TableRow();
                newRow.Height = 50;
                upcomingBookings.Rows.Add(newRow);
                //create a header row and set cell withs
                TableHeaderCell newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Date";
                newHeaderCell.Width = 150;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Time";
                newHeaderCell.Width = 50;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Stylist";
                newHeaderCell.Width = 100;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Service Name";
                newHeaderCell.Width = 200;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Service Description";
                newHeaderCell.Width = 400;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Price";
                newHeaderCell.Width = 100;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Width = 300;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);

                //create a loop to display each result
                //creat a counter to keep track of the current row
                int rowCount = 1;
                foreach (SP_GetCustomerBooking bookings in bookingsList)
                {
                    // create a new row in the results table and set the height
                    newRow = new TableRow();
                    newRow.Height = 50;
                    upcomingBookings.Rows.Add(newRow);
                    //fill the row with the data from the results object
                    TableCell newCell = new TableCell();
                    newCell.Text = bookings.bookingDate.ToString("dd-MM-yyyy");
                    upcomingBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = bookings.bookingStartTime.ToString("HH:mm");
                    upcomingBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = bookings.stylistFirstName.ToString();
                    upcomingBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = bookings.serviceName.ToString();
                    upcomingBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = bookings.serviceDescripion.ToString();
                    upcomingBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "R "+ Math.Round(Convert.ToDouble(bookings.servicePrice), 2).ToString();
                    upcomingBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    //display cancel, edit and print buttons
                    newCell.Text =
                        "<a href = '../ViewBooking.aspx?Action=Cancel&BookingID=" +
                        bookings.bookingID.ToString().Replace(" ", string.Empty) +
                        "&PreviousPage=Bookings.aspx'>Cancel Booking   </a>   " +
                        
                        "<button type = 'button' class='btn btn-default'>" +
                        "<a href = '../ViewBooking.aspx?Action=Edit&BookingID=" +
                        bookings.bookingID.ToString().Replace(" ", string.Empty) +
                        "&PreviousPage=Bookings.aspx'>Edit Booking</a></button>";
                    upcomingBookings.Rows[rowCount].Cells.Add(newCell);
                    rowCount++;
                }
            }
            else
            {
                // if there aren't let the user know
                upcomingBookingsLable.Text =
                    "<p> No Upcoming Bookings </p>";
            }
        }

        public void displayPastBookings()
        {
            //get the Past bookins from the Data Base
            try
            {
                bookingsList = handler.getCustomerPastBookings(cookie["ID"].ToString());
            }
            catch (ApplicationException Err)
            {
                function.logAnError(Err.ToString()+"\n Getting Past Booking ob Bookings Page");
                pastBookingsLable.Text =
                    "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }
            //check if there are past bookings
            if (bookingsList.Count > 0)
            {
                //if there are bookings desplay them
                //create a new row in the past bookings table and set the height
                TableRow newRow = new TableRow();
                newRow.Height = 50;
                pastBookings.Rows.Add(newRow);
                //create a header row and set cell withs
                TableHeaderCell newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Date";
                newHeaderCell.Width = 150;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Time";
                newHeaderCell.Width = 50;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Stylist";
                newHeaderCell.Width = 100;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Service Name";
                newHeaderCell.Width = 200;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Service Description";
                newHeaderCell.Width = 400;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Price";
                newHeaderCell.Width = 100;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Width = 200;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);

                //create a loop to display each result
                //creat a counter to keep track of the current row
                int rowCount = 1;
                foreach (SP_GetCustomerBooking bookings in bookingsList)
                {
                    // create a new row in the results table and set the height
                    newRow = new TableRow();
                    newRow.Height = 50;
                    pastBookings.Rows.Add(newRow);
                    //fill the row with the data from the results object
                    TableCell newCell = new TableCell();
                    newCell.Text = bookings.bookingDate.ToString("dd-MM-yyyy");
                    pastBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = bookings.bookingStartTime.ToString("HH:mm");
                    pastBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = bookings.stylistFirstName.ToString();
                    pastBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = bookings.serviceName.ToString();
                    pastBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = bookings.serviceDescripion.ToString();
                    pastBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "R " + Math.Round(Convert.ToDouble(bookings.servicePrice), 2).ToString();
                    pastBookings.Rows[rowCount].Cells.Add(newCell);
                    if (bookings.arrived.ToString()[0] != 'N')
                    {
                        newCell = new TableCell();
                        newCell.Text =
                            "<button type = 'button' class='btn btn-default'>" +
                            "<a href = '../ViewBooking.aspx?BookingID=" + bookings.bookingID.ToString().Replace(" ", string.Empty) +
                            "&BookingType=Past" +
                            "&PreviousPage=Bookings.aspx'>View Invoice</a></button>";
                        pastBookings.Rows[rowCount].Cells.Add(newCell);
                    }
                    rowCount++;
                }
            }
            else
            {
                // if there aren't let the user know
                pastBookingsLable.Text =
                    "<p> No Past Bookings </p>";
            }
        }
    }
}