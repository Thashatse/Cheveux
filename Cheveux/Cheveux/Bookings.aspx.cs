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
        List<SP_GetCustomerUpcomingBooking> upcomingBookingsList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is loged in and display past and futcher bokings
            cookie = Request.Cookies["CheveuxUserID"];
            if (cookie == null)
            {
                //if the user is not loged in do not diplay and futer services
                Tabs.Visible = false;
            }
            if (cookie != null)
            {
                //if the user is loged in diplay and futer services
                JumbotronLogedIn.Visible = true;
                JumbotronLogedOut.Visible = false;

                //load the users uppcoming bookings in top the upcoming bookins tab
                displayUpcomingBookings();

                //load the users past bookings in top the past bookins tab
                displayPastBookings();
            }
        }

        public void displayUpcomingBookings()
        {
            //get the uppcoming bookins from the Data Base
            try
            {
                upcomingBookingsList = handler.getCustomerUpcomingBookings(cookie["ID"].ToString());
            }
            catch (ApplicationException Err)
            {
                function.logAnError(Err.ToString());
                upcomingBookingsLable.Text =
                    "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }
            //check if there are upcoming bookings
            if (upcomingBookings.Rows.Count > -1)
            {
                //if there are bookings desplay them
                //create a new row in the uppcoming bookings table and set the height
                TableRow newRow = new TableRow();
                newRow.Height = 50;
                upcomingBookings.Rows.Add(newRow);
                //create a header row and set cell withs
                TableHeaderCell newHeaderCell = new TableHeaderCell();
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
                newHeaderCell.Text = "Stylist";
                newHeaderCell.Width = 100;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Time";
                newHeaderCell.Width = 50;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Date";
                newHeaderCell.Width = 150;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Width = 150;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);

                //create a loop to display each result
                //creat a counter to keep track of the current row
                int rowCount = 1;
                foreach (SP_GetCustomerUpcomingBooking bookings in upcomingBookingsList)
                {
                    // create a new row in the results table and set the height
                    newRow = new TableRow();
                    newRow.Height = 50;
                    upcomingBookings.Rows.Add(newRow);
                    //fill the row with the data from the results object
                    TableCell newCell = new TableCell();
                    newCell.Text = bookings.serviceName.ToString();
                    upcomingBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = bookings.serviceDescripion.ToString();
                    upcomingBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = bookings.servicePrice.ToString();
                    upcomingBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = bookings.stylistFirstName.ToString();
                    upcomingBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = bookings.bookingStartTime.ToString("HH:mm");
                    upcomingBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = bookings.bookingDate.ToString("dd-MM-yyyy");
                    upcomingBookings.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text =
                        "<button type = 'button' class='btn btn-default'>" +
                        "<a href = 'ViewBooking.aspx?BookingID=" + bookings.bookingID.ToString().Replace(" ", string.Empty) + 
                        "&PreviousPage=Bookings.aspx'>View Booking</a></button>";
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
                    // List<SP_GetCustomerPastBookings> pastBookings =
                    //handler.getCustomerPastBookings(cookie["ID"].ToString());
                }
                catch (ApplicationException Err)
                {
                    function.logAnError(Err.ToString());
                    pastBookingsLable.Text =
                        "<h2> An Error Occurred Communicating With The Data Base, Try Again Later. </h2>";
                }
                //check if there are past bookings
                if (-1 > 0)
                {
                //if there are bookings desplay them
                //create a header row and set cell withs
                TableHeaderCell newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Service Name";
                newHeaderCell.Width = 200;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell.Text = "Service Description";
                newHeaderCell.Width = 600;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell.Text = "Price";
                newHeaderCell.Width = 50;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell.Text = "Stylist";
                newHeaderCell.Width = 100;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell.Text = "Time";
                newHeaderCell.Width = 50;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell.Text = "Date";
                newHeaderCell.Width = 50;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell.Width = 50;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
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