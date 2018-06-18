using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;
using System.Data;

namespace Cheveux
{
    public partial class ViewBooking : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;
        string PreviousPageAdress = "";
        string BookingID;

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
                BookingID = Request.QueryString["BookingID"];
                if (BookingID != null)
                {
                    LogedIn.Visible = true;
                    LogedOut.Visible = false;

                    //check the action
                    string action = Request.QueryString["Action"];
                    if (action == null)
                    {
                        //check if its a past booking
                        string bookingType = Request.QueryString["BookingType"];
                        if (bookingType == "Past")
                        {
                            //get past booking details
                            getBookingDeatails(BookingID, true, false);
                        }
                        else if (bookingType == "CheckOut")
                        {
                            //get past booking details
                            getBookingDeatails(BookingID, false, true);
                        }
                        else if (bookingType == null)
                        {
                            //get upcoming booking details
                            getBookingDeatails(BookingID, false, false);
                        }
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
                            "<a href = '" + PreviousPageAdress + ">Done</a></button>";
                        }
                        else
                        {
                            BackButton.Text =
                            "<button type = 'button' class='btn btn-default'>" +
                            "<a href = 'Bookings.aspx'>Done</a></button>";
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

        public void getBookingDeatails(string BookingID, bool pastBooking, bool checkOut)
        {

            //display the booking
            //get the details from the db
            try
            {
                SP_GetCustomerBooking BookingDetails = null;
                List<SP_getInvoiceDL> invoicDetailLines = null;
                //check if this is a past or upcoming booking and display the details acordingly
                if (pastBooking == false && checkOut == false)
                {
                    BookingDetails =
                        handler.getCustomerUpcomingBookingDetails(BookingID);
                }
                else if (pastBooking == true)
                {
                    //get booking deatils
                    BookingDetails = handler.getCustomerPastBookingDetails(BookingID);
                    //get the invoice 
                    invoicDetailLines = handler.getInvoiceDL(BookingID);
                    //get the review
                    
                }
                else if (checkOut == true)
                {
                    //get booking deatils
                    BookingDetails = handler.getBookingDetaisForCheckOut(BookingID);
                    //create sales record
                    handler.createSalesRecord(BookingID);
                    //add booking to invoice
                    SALES_DTL detailLine = new SALES_DTL();
                    detailLine.ProductID = BookingDetails.serviceID;
                    detailLine.SaleID = BookingID;
                    detailLine.Qty = 1;
                    handler.createSalesDTLRecord(detailLine);
                    //get the invoice 
                    invoicDetailLines = handler.getInvoiceDL(BookingID);
                }

                if (checkOut == false)
                {
                    //display a heading
                    BookingLable.Text = "<h2> " + BookingDetails.serviceName.ToString() + " with " +
                    BookingDetails.stylistFirstName.ToString() + "</h2>";
                }
                else
                {
                    //display a heading
                    BookingLable.Text = "<h2> Booking Summary </h2>";
                }

                //create a variablew to track the row count
                int rowCount = 0;

                //create a new row in the table and set the height
                TableRow newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                TableCell newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Service Name:";
                newCell.Width = 300;
                BookingTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.serviceName.ToString();
                newCell.Width = 700;
                BookingTable.Rows[rowCount].Cells.Add(newCell);

                //increment row count 
                rowCount++;

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Service Description:";
                BookingTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.serviceDescripion.ToString();
                BookingTable.Rows[rowCount].Cells.Add(newCell);

                //increment row count 
                rowCount++;

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Price:";
                BookingTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.servicePrice.ToString();
                BookingTable.Rows[rowCount].Cells.Add(newCell);

                //increment row count 
                rowCount++;

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Stylist:";
                BookingTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.stylistFirstName.ToString();
                BookingTable.Rows[rowCount].Cells.Add(newCell);

                //increment row count 
                rowCount++;

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Time:";
                BookingTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.bookingStartTime.ToString("HH:mm");
                BookingTable.Rows[rowCount].Cells.Add(newCell);

                //increment row count 
                rowCount++;

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Date:";
                BookingTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.bookingDate.ToString("dd-MM-yyyy");
                BookingTable.Rows[rowCount].Cells.Add(newCell);

                //increment row count 
                rowCount++;

                //only display arrived stataus for past bookings
                if (pastBooking == true || checkOut == true)
                {

                    //diplay invoice
                    //get invoice details
                    List<SP_getInvoiceDL> invoice = handler.getInvoiceDL(BookingID);

                    //diaplay a heading
                    newRow = new TableRow();
                    newRow.Height = 50;
                    BookingTable.Rows.Add(newRow);
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Invoice:";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);

                    //increment row count 
                    rowCount++;

                    //calculate total price
                    double total = 0.0;

                    foreach (SP_getInvoiceDL item in invoice)
                    {
                        newRow = new TableRow();
                        newRow.Height = 50;
                        BookingTable.Rows.Add(newRow);
                        //fill in the item
                        newCell = new TableCell();
                        newCell.Text = item.Qty.ToString() +" "+item.itemName.ToString() + " @ R" + item.price.ToString();
                        newRow.Cells.Add(newCell);
                        //fill in the Qty, unit price & TotalPrice
                        newCell = new TableCell();
                        newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = "R" + Math.Round((item.Qty * item.price), 2).ToString();
                        BookingTable.Rows[rowCount].Cells.Add(newCell);
                        //increment final price
                        total = item.Qty * item.price;

                        //increment row count 
                        rowCount++;

                    }

                    // get vat info
                    Tuple<double, double> vatInfo = function.getVat(total);

                    //display total including and Excluding VAT
                    newRow = new TableRow();
                    newRow.Height = 50;
                    BookingTable.Rows.Add(newRow);
                    newCell = new TableCell();
                    newCell.Text = "Total Ecluding VAT: ";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);
                    //fill in total Ecluding VAT
                    newCell = new TableCell();
                    newCell.HorizontalAlign = HorizontalAlign.Right;
                    newCell.Text = "R " + Math.Round(vatInfo.Item1, 2).ToString();
                    BookingTable.Rows[rowCount].Cells.Add(newCell);

                    //increment row count 
                    rowCount++;

                    //get the vat rate
                    double VATRate = -1;
                    try
                    {
                        VATRate = handler.GetVATRate().VATRate;
                    }
                    catch (ApplicationException Err)
                    {
                        function.logAnError(Err.ToString());
                    }

                    newRow = new TableRow();
                    newRow.Height = 50;
                    BookingTable.Rows.Add(newRow);
                    //fill in total VAT due
                    newCell = new TableCell();
                    newCell.Text = "VAT @"+ VATRate + "%";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.HorizontalAlign = HorizontalAlign.Right;
                    newCell.Text = "R " + Math.Round(vatInfo.Item2, 2).ToString();
                    BookingTable.Rows[rowCount].Cells.Add(newCell);

                    //increment row count 
                    rowCount++;

                    //display the total due
                    newRow = new TableRow();
                    newRow.Height = 50;
                    BookingTable.Rows.Add(newRow);
                    //fill in total
                    newCell = new TableCell();
                    newCell.Text = "Total Due: ";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);
					newCell = new TableCell();
                    newCell.HorizontalAlign = HorizontalAlign.Right;
                    newCell.Text = "R " + total.ToString();
                    BookingTable.Rows[rowCount].Cells.Add(newCell);

                    //increment row count 
                    rowCount++;

                    //display review
                    //diaplay a heading
                    newRow = new TableRow();
                    newRow.Height = 50;
                    BookingTable.Rows.Add(newRow);
                    newCell = new TableCell();
                    BookingTable.Rows[rowCount].Cells.Add(newCell);
					
					//increment row count 
                    rowCount++;

                    //where arrived status used. extra cell in table to be removed
                    newRow = new TableRow();
                    BookingTable.Rows.Add(newRow);
                    newCell = new TableCell();
                    newCell.Text = "";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);

                    //increment row count 
                    rowCount++;
                }

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                BookingTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();

                //check for reivious page
                if (PreviousPageAdress == null)
                { PreviousPageAdress = "Bookings.aspx"; }

                //display the buttons bassed on if this is a past booking or not
                if (pastBooking == true)
                {
                    //print booking summary
                    newCell.Text = "<a href='#' onClick='window.print()' >Print This Page  </a>";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);
                }
                else if(checkOut == true)
                {
                    /* the payment type should be recived and commited ti the database here
                     * for puropse of presentation this was left out
                     * 
                    //get Payment Type
                    string paymentType = handler.getSalePaymentType(BookingID);
                    if(paymentType == null || paymentType == "NULL" || paymentType == "")
                    {
                        newCell.Text = "Cash <input type='checkbox' name='paymentType' value='Cash'/>          "+
                            "       Credit <input type='checkbox' id='Credit' name='paymentType' value='Credit'/>   " +
                            " < a href = '#' > Save Payment Type </ a > ";
                       BookingTable.Rows[rowCount].Cells.Add(newCell);
                    }
                    else
                    {
                        newCell.Text = "<a href='#'> Print   </a>" +

                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = 'ViewBooking.aspx?BookingID=" + BookingID.ToString().Replace(" ", string.Empty) +
                            "&BookingType=CheckOut" +
                            "&PreviousPage=" + PreviousPageAdress + "' style='color:White'>Check-out</a></button>";
                        BookingTable.Rows[rowCount].Cells.Add(newCell);
                    }
                    *
                    * the remaining part of the if statmnt is for presentaion preposes only
                    */
                    newCell.Text = "<a href='#' onClick='window.print()' >Print This Page  </a>";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);
                }
                else
                {
                    newCell.Text = "<a href = 'ViewBooking.aspx?Action=Cancel&BookingID=" +
                    BookingDetails.bookingID.ToString().Replace(" ", string.Empty) +
                    "&PreviousPage=" + PreviousPageAdress + "'>Cancel Booking   </a>   " +

                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = 'ViewBooking.aspx?Action=Edit&BookingID=" +
                    BookingDetails.bookingID.ToString().Replace(" ", string.Empty) +
                    "&PreviousPage=" + PreviousPageAdress + "'>Edit Booking</a></button>";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString() + "\n get getBookingDeatails method in viewbooking form");
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

                //show and fill the Table
                Edit.Visible = true;

                editBookingTable.Rows[0].Cells[0].Text = "Service Name:";

                editBookingTable.Rows[1].Cells[0].Text = "Service Description:";

                editBookingTable.Rows[2].Cells[0].Text = "Price:";

                editBookingTable.Rows[3].Cells[0].Text = "Stylist:";

                //creat a drop down list of stylists
                //get hairstylist info
                List<SP_GetEmpNames> Stylist = handler.BLL_GetEmpNames();
                //bind the data to a list
                DropDownList dropDownStylists = new DropDownList();
                dropDownStylists.ID = "Stylist";
                foreach (SP_GetEmpNames emps in Stylist)
                {
                    dropDownStylists.Items.Add(new ListItem(emps.Name.ToString(), emps.EmployeeID.ToString()));
                    dropDownStylists.DataBind();
                }
                dropDownStylists.Items.FindByValue(BookingDetails.stylistEmployeeID.ToString()).Selected = true;

                editBookingTable.Rows[5].Cells[0].Text = "Time:";

                editBookingTable.Rows[4].Cells[0].Text = "Date:";
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
                "&PreviousPage=" + PreviousPageAdress + ">Yes</a></button>" +
               "</div> ";
            }
            catch (Exception Err)
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
                    Response.Redirect(PreviousPageAdress);
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

        protected void Save_Click(object sender, EventArgs e)
        {
            confirm.Visible = true;
            LogedIn.Visible = false;
            LogedOut.Visible = false;
            confirmHeaderPlaceHolder.Text = "";
            confirmPlaceHolder.Text = "";
        }

        //show edit
        public void showEdit(object sender, EventArgs e)
        {
            confirm.Visible = false;
            Edit.Visible = true;
            LogedIn.Visible = true;
        }

        public void commitEdit(object sender, EventArgs e)
        {
            LogedIn.Visible = false;
            LogedOut.Visible = false;
            BOOKING updatedBooking = new BOOKING();
            //fill the booking variable 


            bool check = false;
            try
            {
                //edit booking
            }
            catch (ApplicationException Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred editing booking id: " + BookingID);
            }
            if (check == true)
            {
                confirmHeaderPlaceHolder.Text = "<h1> Your Booking Been Updated </h1>";
                confirmPlaceHolder.Text = "";
            }
            else if (check == false)
            {
                confirmHeaderPlaceHolder.Text = "<h1> An error occurred updating your Booking </h1>";
                confirmPlaceHolder.Text = "Please try again later";
            }
            yes.Visible = false;
            no.Visible = false;
            OK.Visible = true;
        }

        protected void OK_Click(object sender, EventArgs e)
        {
            Response.Redirect("Bookings.aspx");
        }
    }
}