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
                    }
                    else if (action == "")
                    {
                        //edit the booking
                    }
                    else if (action == "")
                    {
                        //delete the booking
                    }
                }
                else
                {
                    //display an error message
                    BookingLable.Text = "<h2> An Error Occurred Retrieving Booking Details </h2>";
                    function.logAnError("An Error Occurred Retrieving Booking ID from Query String on Bookings Details Page");
                }
            }else
                {
                    //display an appropriate error if booking id not found
                    BookingLable.Text = "<h2> An Error Occurred Retrieving Booking Details </h2>";
                    function.logAnError("An Error Occurred Retrieving User ID Cookie on Bookings Details Page");
                }

                //create a back button
                //Set the page to redirect to the previous page in the querstring
                if (PreviousPageAdress != null)
                {
                    BackButton.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = "+ PreviousPageAdress + ">Return To Previous Page</a></button>";
                }
                else
                {
                    BackButton.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = 'Bookings.aspx'>Return To Bookings</a></button>";
                }
            }

        public void getBookingDeatails(string BookingID)
        {
                //display the booking
                //get the details from the db

                try
                {
                    SP_GetCustomerUpcomingBooking BookingDetails = 
                        handler.getCustomerUpcomingBookingDetails(BookingID);

                //display a heading
                BookingLable.Text = "<h2> "+BookingDetails.serviceName.ToString()+" with "+
                    BookingDetails.stylistFirstName.ToString() +"</h2>";

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
                newCell.Text = "<button type = 'button' class='btn btn-default'>" +
                "<a href = 'ViewBooking.aspx?Action=Cancel&BookingID=" + 
                BookingDetails.bookingID.ToString().Replace(" ", string.Empty) +
                "&PreviousPage=" + PreviousPageAdress + "'>Cancel Booking</a></button>   " +
                
                "<button type = 'button' class='btn btn-default'>" +
                "<a href = 'ViewBooking.aspx?Action=Edit&BookingID=" + 
                BookingDetails.bookingID.ToString().Replace(" ", string.Empty) +
                "&PreviousPage=" + PreviousPageAdress + "'>Edit Booking</a></button>";
                BookingTable.Rows[6].Cells.Add(newCell);
            }
                catch (ApplicationException Err)
                {
                    function.logAnError(Err.ToString());
                BookingLable.Text =
                        "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
                }
        }
    }
}