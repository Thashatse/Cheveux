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
        List<string> productIDs = new List<string>();

        #region Master Page
        //set the master page based on the user type
        protected void Page_PreInit(Object sender, EventArgs e)
        {
            //check the cheveux user id cookie for the user
            HttpCookie cookie = Request.Cookies["CheveuxUserID"];
            char userType;
            //check if the cookie is empty or not
            if (cookie != null)
            {
                //store the user Type in a variable and display the appropriate master page for the user
                userType = cookie["UT"].ToString()[0];
                //if customer
                if (userType == 'C')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/Cheveux.Master";
                }
                //if receptionist
                else if (userType == 'R')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/CheveuxReceptionist.Master";
                }
                //if stylist
                else if (userType == 'S')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/CheveuxStylist.Master";
                }
                //if Manager
                else if (userType == 'M')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/CheveuxManager.Master";
                }
                //default
                else
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/Cheveux.Master";
                }
            }
            //set the default master page if the cookie is empty
            else
            {
                //set the master page
                this.MasterPageFile = "~/MasterPages/Cheveux.Master";
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            PreviousPageAdress = Request.QueryString["PreviousPage"];
            //check if the user is loged out
            cookie = Request.Cookies["CheveuxUserID"];
            if (cookie == null)
            {
                //if the user is not loged in do not diplay bookings details
                Response.Redirect("Profile.aspx");
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
                            checkOut(BookingID);
                        }
                        else if (bookingType == null)
                        {
                            //get upcoming booking details
                            getBookingDeatails(BookingID, false, false);
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
                if (pastBooking == false)
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
               
                    //display a heading
                    BookingLable.Text = "<h2> " + BookingDetails.serviceName.ToString() + " with " +
                    BookingDetails.stylistFirstName.ToString() + "</h2>";

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
                newCell.Text = "<a href='ViewProduct.aspx?ProductID=" + BookingDetails.serviceID.Replace(" ", string.Empty) + "'>" + 
                    BookingDetails.serviceName.ToString() + "</a>";
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
                newCell.Text = "<a href='ViewProduct.aspx?ProductID=" + BookingDetails.serviceID.Replace(" ", string.Empty) + "'>"+
                    BookingDetails.serviceDescripion.ToString() + "</a>";
                BookingTable.Rows[rowCount].Cells.Add(newCell);
                
                //increment row count 
                rowCount++;

                newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                if (cookie["UT"] == "C")
                {
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Stylist:";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "<a href='Profile.aspx?Action=View" +
                                "&empID=" + BookingDetails.stylistEmployeeID.ToString().Replace(" ", string.Empty) +
                                "'>" + BookingDetails.stylistFirstName.ToString() + "</a>";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);
                }
                else if(cookie["UT"] == "S")
                {
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Customer:";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "<a href='Profile.aspx?Action=View" +
                                "&UserID=" + BookingDetails.CustomerID.ToString().Replace(" ", string.Empty) +
                                "'>" + BookingDetails.CustFullName.ToString() + "</a>";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);
                }
                else
                {
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Stylist:";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "<a href='Profile.aspx?Action=View" +
                                "&empID=" + BookingDetails.stylistEmployeeID.ToString().Replace(" ", string.Empty) +
                                "'>" + BookingDetails.stylistFirstName.ToString() + "</a>";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);

                    //increment row count 
                    rowCount++;

                    newRow = new TableRow();
                    newRow.Height = 50;
                    BookingTable.Rows.Add(newRow);

                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Customer:";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "<a href='Profile.aspx?Action=View" +
                                "&UserID=" + BookingDetails.CustomerID.ToString().Replace(" ", string.Empty) +
                                "'>" + BookingDetails.CustFullName.ToString() + "</a>";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);
                }
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
                if (pastBooking == true)
                {

                    //diplay invoice
                    //get invoice details
                    List<SP_getInvoiceDL> invoice = handler.getInvoiceDL(BookingID);

                    if (invoicDetailLines.Count != 0)
                    {

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
                            newCell.Text = item.Qty.ToString() + " " + item.itemName.ToString() + " @ R" + string.Format("{0:#.00}", item.price);
                            newRow.Cells.Add(newCell);
                            //fill in the Qty, unit price & TotalPrice
                            newCell = new TableCell();
                            newCell.HorizontalAlign = HorizontalAlign.Right;
                            newCell.Text = "R" + string.Format("{0:#.00}", item.price);
                            BookingTable.Rows[rowCount].Cells.Add(newCell);
                            //increment final price
                            total += item.Qty * item.price;

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
                        newCell.Text = "<br/> Total Ecluding VAT: ";
                        BookingTable.Rows[rowCount].Cells.Add(newCell);
                        //fill in total Ecluding VAT
                        newCell = new TableCell();
                        newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = " <br/> R " + string.Format("{0:#.00}", vatInfo.Item1, 2);
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
                        newCell.Text = "VAT @" + VATRate + "%";
                        BookingTable.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = "R " + string.Format("{0:#.00}", vatInfo.Item2, 2).ToString();
                        BookingTable.Rows[rowCount].Cells.Add(newCell);

                        //increment row count 
                        rowCount++;

                        //display the total due
                        newRow = new TableRow();
                        newRow.Height = 50;
                        BookingTable.Rows.Add(newRow);
                        //fill in total
                        newCell = new TableCell();
                        newCell.Text = "<br/> Total Due: ";
                        BookingTable.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = "<br/> R " + string.Format("{0:#.00}", total).ToString();
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

        #region Edit Booking
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
        #endregion
        
        #region Delete
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
        #endregion

        protected void Save_Click(object sender, EventArgs e)
        {
            confirm.Visible = true;
            LogedIn.Visible = false;
            LogedOut.Visible = false;
            confirmHeaderPlaceHolder.Text = "";
            confirmPlaceHolder.Text = "";
        }
        
        protected void OK_Click(object sender, EventArgs e)
        {
            Response.Redirect("Bookings.aspx");
        }

        #region Check Out
        Tuple<List<SP_GetAllAccessories>, List<SP_GetAllTreatments>> products = null;

        public void checkOut(string BookingID)
        {
            //display the booking detail
            try
            {
                //get the details from the db
                SP_GetCustomerBooking BookingDetails = null;
                List<SP_getInvoiceDL> invoicDetailLines = null;
                //get booking deatils
                BookingDetails = handler.getBookingDetaisForCheckOut(BookingID);

                //check if sales record exists
                //if sales record dose not exist make a new one
                if (handler.getInvoiceDL(BookingID).Count == 0)
                {
                    //create sales record
                    handler.createSalesRecord(BookingID);
                    //add booking to invoice
                    SALES_DTL detailLine = new SALES_DTL();
                    detailLine.ProductID = BookingDetails.serviceID;
                    detailLine.SaleID = BookingID;
                    detailLine.Qty = 1;
                    handler.createSalesDTLRecord(detailLine);
                }

                //get the invoice 
                invoicDetailLines = handler.getInvoiceDL(BookingID);
                
                //un-hide the checkout table 
                divCheckOut.Visible = true;

                //display a heading
                BookingLable.Text = "<h2> Booking Summary </h2>";

                //create a variable to track the row count
                int rowCount = 0;

                //add booking details to the table

                //service name
                tblCheckOut.Rows[rowCount].Cells[1].Text = BookingDetails.serviceName.ToString();

                //increment row count 
                rowCount++;

                //Service description
                tblCheckOut.Rows[rowCount].Cells[1].Text = BookingDetails.serviceDescripion.ToString();

                //increment row count 
                rowCount++;

                //stylist
                tblCheckOut.Rows[rowCount].Cells[1].Text = BookingDetails.stylistFirstName.ToString();

                //increment row count 
                rowCount++;

                //Date
                tblCheckOut.Rows[rowCount].Cells[1].Text = BookingDetails.bookingDate.ToString("dd-MM-yyyy");

                //increment row count 
                rowCount++;

                //Time
                tblCheckOut.Rows[rowCount].Cells[1].Text = BookingDetails.bookingStartTime.ToString("HH:mm");

                //increment row count 
                rowCount++;

                //invoice header here (Already in Table)
                //increment row count
                rowCount++;

                //diplay invoice
                #region invoice
                //get invoice details
                List<SP_getInvoiceDL> invoice = handler.getInvoiceDL(BookingID);

                //create a table for the invoice (To be added to tblCheckOut cell)
                string tblInvoice = "<table>";

                //calculate total price
                double total = 0.0;

                foreach (SP_getInvoiceDL item in invoice)
                {
                    //new row
                    tblInvoice += "<tr>";
                    //add a new cell to the row
                    //fill in the item
                    tblInvoice += "<td  Width='250'>" + item.Qty.ToString() + " " + item.itemName.ToString() + " @ R" + item.price.ToString() + "</td>";

                    //add a new cell to the row
                    //fill in the Qty, unit price & TotalPrice
                    tblInvoice += "<td align='right' Width='250'> R" + Math.Round((item.Qty * item.price), 2).ToString() + "</td>";
                    tblInvoice += "</tr>";

                    //increment final price
                    total += item.Qty * item.price;
                }

                // get vat info
                Tuple<double, double> vatInfo = function.getVat(total);

                //display total including and Excluding VAT
                //new row
                tblInvoice += "<tr>";

                tblInvoice += "<td> <br/> Total Ecluding VAT: </td>";

                //fill in total Ecluding VAT

                tblInvoice += "<td align='right'> <br/> R" + Math.Round(vatInfo.Item1, 2).ToString() + "</td>";
                tblInvoice += "</tr>";

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

                //new row
                tblInvoice += "<tr>";

                //fill in total VAT due
                tblInvoice += "<td> VAT @" + VATRate + "% </td>";

                tblInvoice += "<td align='right'> R" + Math.Round(vatInfo.Item2, 2).ToString() + "</td>";

                //display the total due//new row
                tblInvoice += "</tr><tr>";

                //fill in total
                tblInvoice += "<td> <br/> Total Due: </td>";

                tblInvoice += "<td align='right'> <br/> R" + total.ToString() + "</td>";
                tblInvoice += "</tr>";

                tblInvoice += "</table>";

                //add the invoice to the table
                tblCheckOut.Rows[rowCount].Cells[1].Text = tblInvoice;

                //increment row count 
                rowCount++;
                rowCount++;
                rowCount++;
                #endregion

                //check if paymentType Exists
                string paymentType = handler.getSalePaymentType(BookingID);
                if (paymentType != "")
                {
                    //hide payment type busttons
                    tblCheckOut.Rows[rowCount].Cells[0].Text = "";
                    tblCheckOut.Rows[rowCount].Cells[1].Text = "";
                    //add print page
                    tblCheckOut.Rows[rowCount + 1].Cells[0].Text = "<a href = '#' onClick = 'window.print()'> Print This Page </a>";
                    //show payment type
                    tblCheckOut.Rows[rowCount - 1].Cells[1].Text = paymentType;
                    //hide add product button
                    tblCheckOut.Rows[rowCount - 2].Cells[1].Text = "";
                }
                else
                {
                    tblCheckOut.Rows[rowCount + 1].Cells[0].Text = "";
                    tblCheckOut.Rows[rowCount + 1].Cells[1].Text = "";
                }

            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString() + "\n get getBookingDeatails method in viewbooking form");
                BookingLable.Text =
                        "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }
        }

        protected void btnSavePaymentType_Click(object sender, EventArgs e)
        {
            string pT = PaymentType.SelectedValue.ToString();
            try
            {
                //add the pyment tye
                handler.addPaymentTypeToSalesRecord(pT.Replace(" ", string.Empty), BookingID);
                //refresh the page to reflect tha changes
                checkOut(BookingID);
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString() + "\n An error ocoured updating payment type during checkout process");
                Response.Write("<script>alert('An error ocoured updating payment type.');window.location='ViewBooking.aspx';</script>");
                Response.Redirect(PreviousPageAdress);
            }
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            //add Products to the list
            lbProducts.Items.Clear();
            try
            {
                //load a list of all products
                products = handler.getAllProductsAndDetails();
                if (products.Item1.Count != 0 && products.Item2.Count != 0)
                {
                    //sort the products by alphabetical oder
                    products = Tuple.Create(products.Item1.OrderBy(o => o.Name).ToList(),
                        products.Item2.OrderBy(o => o.Name).ToList());
                    //add treatments
                    foreach (SP_GetAllTreatments treat in products.Item2)
                    {
                        //make sure there is stock
                        if(treat.Qty > 0
                            && (compareToSearchTerm(treat.Name) == true 
                            || compareToSearchTerm(treat.ProductDescription) == true
                            || compareToSearchTerm(treat.Brandname) == true))
                        {
                            lbProducts.Items.Add(treat.Name.ToString());
                        }
                    }

                    //add accessories
                    foreach (SP_GetAllAccessories Access in products.Item1)
                    {
                        //make sure there is stock
                        if (Access.Qty > 0
                            && (compareToSearchTerm(Access.Name) == true
                            || compareToSearchTerm(Access.ProductDescription) == true
                            || compareToSearchTerm(Access.Brandname) == true))
                        {
                            lbProducts.Items.Add(Access.Name.ToString());
                        }
                    }

                    addRemoveProductInvoice();

                        //show the add product to sale view
                    divCheckOut.Visible = false;
                        divAddProducts.Visible = true;
                }
                else
                {
                    Response.Write("<script>alert('An error occoured loading products, Please try again later.');location.reload(true);</script>");
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving list of products" 
                    + " in btnAddProduct_Click(object sender, EventArgs e) method on viewBookings Page");
                Response.Write("<script>alert('An error occoured loading products, Please try again later.');location.reload(true);</script>");
            }
        }

        protected void loadproductIDs()
        {
            if (btnAddProductToSale.Text == "Add Product")
            {
                //load the product ids
                products = handler.getAllProductsAndDetails();
                if (products.Item1.Count != 0 && products.Item2.Count != 0)
                {
                    //sort the products by alphabetical oder
                    products = Tuple.Create(products.Item1.OrderBy(o => o.Name).ToList(),
                        products.Item2.OrderBy(o => o.Name).ToList());
                    //add treatments
                    foreach (SP_GetAllTreatments treat in products.Item2)
                    {
                        //make sure there is stock
                        if (treat.Qty > 0
                            && (compareToSearchTerm(treat.Name) == true
                            || compareToSearchTerm(treat.ProductDescription) == true
                            || compareToSearchTerm(treat.Brandname) == true))
                        {
                            productIDs.Add(treat.ProductID.ToString());
                        }
                    }

                    //add accessories
                    foreach (SP_GetAllAccessories Access in products.Item1)
                    {
                        //make sure there is stock
                        if (Access.Qty > 0
                            && (compareToSearchTerm(Access.Name) == true
                            || compareToSearchTerm(Access.ProductDescription) == true
                            || compareToSearchTerm(Access.Brandname) == true))
                        {
                            productIDs.Add(Access.ProductID.ToString());
                        }
                    }
                }
            }
            else if (btnAddProductToSale.Text == "Remove Product")
            {
                //load the product ids
                List<SP_getInvoiceDL> invoice = handler.getInvoiceDL(BookingID);
                if (invoice.Count != 0)
                {
                    //add treatments
                    foreach (SP_getInvoiceDL item in invoice)
                    {
                        if (item.itemType != "S")
                        {
                            productIDs.Add(item.itemID.ToString());
                        }
                    }
                }
            }
        }
        
        protected void btnAddProductToSale_Click(object sender, EventArgs e)
        {
            if (btnAddProductToSale.Text == "Add Product")
            {
                //a product to ivoice
                if (lbProducts.SelectedIndex >= 0)
                {
                    try
                    {
                        //load the product ids
                        loadproductIDs();
                        //add the selectedproduct to the sale
                        SALES_DTL newItem = new SALES_DTL();
                        newItem.SaleID = BookingID;
                        newItem.ProductID = productIDs[lbProducts.SelectedIndex];
                        newItem.Qty = 1;
                        handler.createProductSalesDTLRecord(newItem);
                        btnAddProduct_Click(sender, e);
                    }
                    catch (Exception Err)
                    {
                        function.logAnError(" An error occurred adding product to sales record"
                            + " in btnAddProductToSale_Click(object sender, EventArgs e) method on viewBookings Page: " + Err.ToString());
                        Response.Write("<script>alert('An error occoured adding the product, Please try again later.');location.reload(false);</script>");
                    }
                }
                else
                {
                    btnAddProduct_Click(sender, e);
                }
            }
            else if (btnAddProductToSale.Text == "Remove Product")
            {
                //remove product from ivoice
                if (lbProducts.SelectedIndex >= 0)
                {
                    try
                    {
                        //load the product ids
                        loadproductIDs();
                        //remove the selected product to the sale
                        SALES_DTL removeItem = new SALES_DTL();
                        removeItem.SaleID = BookingID;
                        removeItem.ProductID = productIDs[lbProducts.SelectedIndex];
                        handler.removeProductSalesDTLRecord(removeItem);
                        removeProducts();
                    }
                    catch (Exception Err)
                    {
                        function.logAnError(" An error occurred adding product to sales record"
                            + " in btnAddProductToSale_Click(object sender, EventArgs e) method on viewBookings Page: " + Err.ToString());
                        Response.Write("<script>alert('An error occoured adding the product, Please try again later.');location.reload(false);</script>");
                    }
                }
                else
                {
                    removeProducts();
                }
            }
        }

        protected void btnRemoveProductFromSale_Click(object sender, EventArgs e)
        {
            //remove the product from the sale
            if (btnRemoveProductFromSale.Text == "Remove Product(S)")
            {
                txtProductSearch.Visible = false;
                btnSearchProduct.Visible = false;
                btnRemoveProductFromSale.Text = "Add Product(s)";
                btnAddProductToSale.Text = "Remove Product";
                //load products to remove
                removeProducts();
            }
            else if (btnRemoveProductFromSale.Text == "Add Product(s)")
            {
                txtProductSearch.Visible = true;
                btnSearchProduct.Visible = true;
                btnRemoveProductFromSale.Text = "Remove Product(s)";
                btnAddProductToSale.Text = "Add Product";
                //load products to add
                btnAddProduct_Click(sender, e);
            }
        }

        protected void removeProducts()
        {
            //add Products to the list
            lbProducts.Items.Clear();
            try
            {
                //get invoice details and the current products
                List<SP_getInvoiceDL> invoice = handler.getInvoiceDL(BookingID);
                if (invoice.Count != 0)
                {
                    //add 
                    foreach (SP_getInvoiceDL item in invoice)
                    {
                        if (item.itemType != "S")
                        {
                            lbProducts.Items.Add(item.itemName.ToString());
                        }
                    }

                    //refresh the invoice
                    addRemoveProductInvoice();

                    //show the add product to sale view
                    divCheckOut.Visible = false;
                    divAddProducts.Visible = true;
                }
                else
                {
                    Response.Write("<script>alert('An error occoured loading products, Please try again later.');location.reload(true);</script>");
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving list of products"
                    + " in btnAddProduct_Click(object sender, EventArgs e) method on viewBookings Page");
                Response.Write("<script>alert('An error occoured loading products, Please try again later.');location.reload(true);</script>");
            }
        }

        protected void addRemoveProductInvoice()
        {
            #region invoice
            //get invoice details
            List<SP_getInvoiceDL> invoice = handler.getInvoiceDL(BookingID);

            //create a table for the invoice (To be added to tblCheckOut cell)
            string tblInvoice = "<table>";

            //calculate total price
            double total = 0.0;

            foreach (SP_getInvoiceDL item in invoice)
            {
                //new row
                tblInvoice += "<tr>";
                //add a new cell to the row
                //fill in the item
                tblInvoice += "<td  Width='250'>" + item.Qty.ToString() + " " + item.itemName.ToString() + " @ R" + item.price.ToString() + "</td>";

                //add a new cell to the row
                //fill in the Qty, unit price & TotalPrice
                tblInvoice += "<td align='right' Width='250'> R" + Math.Round((item.Qty * item.price), 2).ToString() + "</td>";
                tblInvoice += "</tr>";

                //increment final price
                total += item.Qty * item.price;
            }

            // get vat info
            Tuple<double, double> vatInfo = function.getVat(total);

            //display total including and Excluding VAT
            //new row
            tblInvoice += "<tr>";

            tblInvoice += "<td> <br/> Total Ecluding VAT: </td>";

            //fill in total Ecluding VAT

            tblInvoice += "<td align='right'> <br/> R" + Math.Round(vatInfo.Item1, 2).ToString() + "</td>";
            tblInvoice += "</tr>";

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

            //new row
            tblInvoice += "<tr>";

            //fill in total VAT due
            tblInvoice += "<td> VAT @" + VATRate + "% </td>";

            tblInvoice += "<td align='right'> R" + Math.Round(vatInfo.Item2, 2).ToString() + "</td>";

            //display the total due//new row
            tblInvoice += "</tr><tr>";

            //fill in total
            tblInvoice += "<td> <br/> Total Due: </td>";

            tblInvoice += "<td align='right'> <br/> R" + total.ToString() + "</td>";
            tblInvoice += "</tr>";

            tblInvoice += "</table>";

            //add the invoice to the table
            tblSale.Rows[0].Cells[0].Text = tblInvoice;
            #endregion
        }

        protected void btnSaveSale_Click(object sender, EventArgs e)
        {
            //Save The Sale
            divAddProducts.Visible = false;
            BookingID = Request.QueryString["BookingID"];
            checkOut(BookingID);
            divCheckOut.Visible = true;
        }

        public bool compareToSearchTerm(string toBeCompared)
        {
            bool result = false;
            if (txtProductSearch.Text != null)
            {
                toBeCompared = toBeCompared.ToLower();
                string searcTearm = txtProductSearch.Text.ToLower();
                if (toBeCompared.Contains(searcTearm))
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }
            return result;
        }
        #endregion
    }
}