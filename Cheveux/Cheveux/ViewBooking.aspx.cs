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
    public partial class ViewBooking : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;
        string PreviousPageAdress = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            PreviousPageAdress = Request.QueryString["PreviousPage"];
            //check if the user is loged out
            cookie = Request.Cookies["CheveuxUserID"];
            if (cookie == null)
            {
                //if the user is not loged in do not diplay bookings details
            }
            else if (cookie != null)
            {
                //if the user is loged in diplay bookings details
                //checked for the booking ID
                string BookingID = Request.QueryString["BookingID"];
                if (BookingID != null)
                {
                    LogedIn.Visible = true;
                    LogedOut.Visible = false;

                    //check the action
                    string action = Request.QueryString["Action"];
                    if (action == null)
                    {
                        //get the bookingID from the querystring
                        getBookingDeatails(BookingID);
                        //create a back button
                        //Set the page to redirect to the previous page in the querstring
                        if (PreviousPageAdress != null)
                        {
                            BackButton.Text =
                            "<button type = 'button' class='btn btn-default'>" +
                            "<a href = " + PreviousPageAdress + ">Return To Previous Page</a></button>";
                        }
                        else
                        {
                            BackButton.Text =
                            "<button type = 'button' class='btn btn-default'>" +
                            "<a href = 'Bookings.aspx'>Return To Bookings</a></button>";
                        }
                    }
                    else if (action == "Edit")
                    {
                        //edit the booking
                        editBooking(BookingID);
                    }
                    else if (action == "Cancel")
                    {
                        //confirm the delete the booking
                        confirmDeleteBooking(BookingID);
                    }
                    else if (action == "CancelConfirmed")
                    {
                        //delete the booking
                        deleteBooking(BookingID);
                        //Set the page to redirect to the previous page in the querstring
                        if (PreviousPageAdress != null)
                        {
                            BackButton.Text =
                            "<button type = 'button' class='btn btn-default'>" +
                            "<a href = " + PreviousPageAdress + ">Done</a></button>";
                        }
                        else
                        {
                            BackButton.Text =
                            "<button type = 'button' class='btn btn-default'>" +
                            "<a href = Bookings.aspx>Done</a></button>";
                        }
                    }
                }
                else
                {
                    //display an error message
                    BookingLable.Text = "<h2> An Error Occurred Retrieving Booking Details </h2>";
                    function.logAnError("An Error Occurred Retrieving Booking ID from Query String on Bookings Details Page");
                }
            }
            else
            {
                //display an appropriate error if booking id not found
                BookingLable.Text = "<h2> An Error Occurred Retrieving Booking Details </h2>";
                function.logAnError("An Error Occurred Retrieving User ID Cookie on Bookings Details Page");
            }
        }

        public void getBookingDeatails(string BookingID)
        {
            //display the booking
            //get the details from the db
            try
            {
                SP_GetCustomerBooking BookingDetails =
                    handler.getCustomerUpcomingBookingDetails(BookingID);

                //display a heading
                BookingLable.Text = "<h2> " + BookingDetails.serviceName.ToString() + " with " +
                    BookingDetails.stylistFirstName.ToString() + "</h2>";

                //create a new row in the table and set the height
                TableRow newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                TableCell newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Service Name:";
                newCell.Width = 300;
                BookingTable.Rows[0].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.serviceName.ToString();
                newCell.Width = 700;
                BookingTable.Rows[0].Cells.Add(newCell);

                newRow = new TableRow();
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Service Description:";
                BookingTable.Rows[1].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.serviceDescripion.ToString();
                BookingTable.Rows[1].Cells.Add(newCell);

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Price:";
                BookingTable.Rows[2].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.servicePrice.ToString();
                BookingTable.Rows[2].Cells.Add(newCell);

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Stylist:";
                BookingTable.Rows[3].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.stylistFirstName.ToString();
                BookingTable.Rows[3].Cells.Add(newCell);

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Time:";
                BookingTable.Rows[4].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.bookingStartTime.ToString("HH:mm");
                BookingTable.Rows[4].Cells.Add(newCell);

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Date:";
                BookingTable.Rows[5].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.bookingDate.ToString("dd-MM-yyyy");
                BookingTable.Rows[5].Cells.Add(newCell);

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                BookingTable.Rows[6].Cells.Add(newCell);
                newCell = new TableCell();
                if (PreviousPageAdress == null)
                { PreviousPageAdress = "Bookings.aspx"; }
                newCell.Text = "<a href = 'ViewBooking.aspx?Action=Cancel&BookingID=" +
                BookingDetails.bookingID.ToString().Replace(" ", string.Empty) +
                "&PreviousPage=" + PreviousPageAdress + "'>Cancel Booking   </a>   " +

                "<button type = 'button' class='btn btn-default'>" +
                "<a href = 'ViewBooking.aspx?Action=Edit&BookingID=" +
                BookingDetails.bookingID.ToString().Replace(" ", string.Empty) +
                "&PreviousPage=" + PreviousPageAdress + "'>Edit Booking</a></button>";
                BookingTable.Rows[6].Cells.Add(newCell);
            }
            catch (ApplicationException Err)
            {
                function.logAnError(Err.ToString() + "\n get booking method in viewbooking form");
                BookingLable.Text =
                        "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }
        }

        public void editBooking(string BookingID)
        {
            //display the booking edit form
            try
            {
                SP_GetCustomerBooking BookingDetails =
                    handler.getCustomerUpcomingBookingDetails(BookingID);

                //display a heading
                BookingLable.Text = "<h2> " + BookingDetails.serviceName.ToString() + " with " +
                    BookingDetails.stylistFirstName.ToString() + "</h2>";

                //create a new row in the table and set the height
                TableRow newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                TableCell newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Service Name:";
                newCell.Width = 300;
                BookingTable.Rows[0].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.serviceName.ToString();
                newCell.Width = 700;
                BookingTable.Rows[0].Cells.Add(newCell);

                newRow = new TableRow();
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Service Description:";
                BookingTable.Rows[1].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.serviceDescripion.ToString();
                BookingTable.Rows[1].Cells.Add(newCell);

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Price:";
                BookingTable.Rows[2].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.servicePrice.ToString();
                BookingTable.Rows[2].Cells.Add(newCell);

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Stylist:";
                BookingTable.Rows[3].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.stylistFirstName.ToString();
                BookingTable.Rows[3].Cells.Add(newCell);

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Time:";
                BookingTable.Rows[4].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.bookingStartTime.ToString("HH:mm");
                BookingTable.Rows[4].Cells.Add(newCell);

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Date:";
                BookingTable.Rows[5].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.bookingDate.ToString("dd-MM-yyyy");
                BookingTable.Rows[5].Cells.Add(newCell);

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                BookingTable.Rows[6].Cells.Add(newCell);
                newCell = new TableCell();
                if (PreviousPageAdress == null)
                { PreviousPageAdress = "Bookings.aspx"; }
                newCell.Text = "<a href='javascript:goBack()'>Cancel   </a>" +

                "<button type = 'button' class='btn btn-default'>" +
                "<a href = 'ViewBooking.aspx?Action=Edit&BookingID=" +
                BookingDetails.bookingID.ToString().Replace(" ", string.Empty) +
                "&PreviousPage=" + PreviousPageAdress + "'>Save</a></button>";
                BookingTable.Rows[6].Cells.Add(newCell);
            }
            catch (ApplicationException Err)
            {
                function.logAnError(Err.ToString() + "\n edit booking method in viewbooking form");
                BookingLable.Text =
                        "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }
        }

        public void confirmDeleteBooking(string BookingID)
        {
            //display booking detatils & ask if the user is sure
            //display the booking edit form
            try
            {
                SP_GetCustomerBooking BookingDetails =
                        handler.getCustomerUpcomingBookingDetails(BookingID);

                //display a heading
                BookingLable.Text = "<div class='jumbotron'> <h1> Are you sure you want to cancel booking, </h1> " +
                    " <p>" + BookingDetails.serviceName.ToString() +
                    " with " + BookingDetails.stylistFirstName.ToString() +
                    " on " + BookingDetails.bookingDate.ToString("dd-MM-yyyy") +
                    " at " + BookingDetails.bookingStartTime.ToString("HH:mm") + "? " +
                    "</p>  <button type = 'button' class='btn btn-default'>" +
                "<a href='javascript:goBack()'>No</a></button>  " +
               "<button type = 'button' class='btn btn-danger'>" +
                "<a href = ViewBooking.aspx?Action=CancelConfirmed&BookingID=" +
                BookingDetails.bookingID.ToString().Replace(" ", string.Empty) +
                "&PreviousPage=" + PreviousPageAdress + "'>Yes</a></button>" +
               "</div> ";
            }
            catch (ApplicationException Err)
            {
                function.logAnError(Err.ToString() + "\n confirm delete booking method in viewbooking form");
                BookingLable.Text =
                        "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }
        }

        public void deleteBooking(string BookingID)
        {
            try
            {
                bool success =
                        handler.deleteBooking(BookingID);
                //Let teh user know it was a success or not
                if (success == true)
                {
                    BookingLable.Text = "<div class='jumbotron'> <h1> The Booking was succefuly Canceled, </h1> " +
                   "</div> ";
                }
                else
                {
                    function.logAnError("\n an error occured deleting a booking from the database");
                    BookingLable.Text =
                            "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
                }
            }
            catch (ApplicationException Err)
            {
                function.logAnError(Err.ToString() + "\n an error occured deleting a booking from the database");
                BookingLable.Text =
                        "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }
        }
    }
}