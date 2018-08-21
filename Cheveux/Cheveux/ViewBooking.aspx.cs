﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;
using System.Data;
using System.Drawing;


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
        List<int> productSaleQty = new List<int>();
        

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

        #region View Booking
        public void getBookingDeatails(string BookingID, bool pastBooking, bool checkOut)
        {
            //display the booking
            //get the details from the db
            try
            {
                SP_GetCustomerBooking BookingDetails = null;
                List<SP_getInvoiceDL> invoicDetailLines = null;
                List<SP_GetBookingServices> bookingServiceList = null;
                //check if this is a past or upcoming booking and display the details acordingly
                if (pastBooking == false)
                {
                    BookingDetails =
                        handler.getCustomerUpcomingBookingDetails(BookingID);
                    //get the services
                    bookingServiceList = handler.getBookingServices(BookingID);
                }
                else if (pastBooking == true)
                {
                    //get booking deatils
                    BookingDetails = handler.getCustomerPastBookingDetails(BookingID);
                    //get the invoice 
                    invoicDetailLines = handler.getInvoiceDL(BookingID);
                    //get the services
                    bookingServiceList = handler.getBookingServices(BookingID);
                    //get the review

                }

                #region Heading
                if (bookingServiceList.Count == 1)
                {
                    //display a heading
                    BookingLable.Text = "<h2> " + bookingServiceList[0].ServiceName.ToString() + " with " +
                    BookingDetails.stylistFirstName.ToString() + "</h2>";
                }
                else if (bookingServiceList.Count == 2)
                {
                    //display a heading
                    BookingLable.Text = "<h2> " + bookingServiceList[0].ServiceName.ToString() +
                    " & " +  bookingServiceList[1].ServiceName.ToString() + " with " +
                    BookingDetails.stylistFirstName.ToString() + "</h2>";
                }
                else if (bookingServiceList.Count > 2)
                {
                    //display a heading
                    BookingLable.Text = "<h2> Booking with " +
                    BookingDetails.stylistFirstName.ToString() + "</h2>";
                }
                #endregion

                #region Booking Details
                //create a variablew to track the row count
                int rowCount = 0;

                //create a new row in the table and set the height
                TableRow newRow = new TableRow();
                newRow.Height = 50;
                BookingTable.Rows.Add(newRow);
                TableCell newCell = new TableCell();
                newCell.Font.Bold = true;
                if (bookingServiceList.Count == 1)
                {
                    newCell.Text = "Service:";
                }
                else if (bookingServiceList.Count > 1)
                {
                    newCell.Text = "Services:";
                }
                newCell.Width = 300;
                BookingTable.Rows[rowCount].Cells.Add(newCell);
                
                if (bookingServiceList.Count == 1)
                {
                    newCell = new TableCell();
                    newCell.Text = "<a href='ViewProduct.aspx?ProductID=" + bookingServiceList[0].ServiceID.Replace(" ", string.Empty) + "'>"
                    + bookingServiceList[0].ServiceName.ToString() + "</a>";
                    newCell.Width = 700;
                    BookingTable.Rows[rowCount].Cells.Add(newCell);

                    //increment row count 
                    rowCount++;
                }
                else if (bookingServiceList.Count > 1)
                {
                    int i = 0;
                    foreach (SP_GetBookingServices service in bookingServiceList)
                    {
                        if(i > 0)
                        {
                            newRow = new TableRow();
                            newRow.Height = 50;
                            BookingTable.Rows.Add(newRow);
                            newCell = new TableCell();
                            newCell.Width = 300;
                            BookingTable.Rows[rowCount].Cells.Add(newCell);
                        }

                        newCell = new TableCell();
                        newCell.Text = "<a href='ViewProduct.aspx?ProductID=" + bookingServiceList[0].ServiceID.Replace(" ", string.Empty) + "'>"
                        + service.ServiceName.ToString() + "</a>";
                        newCell.Width = 700;
                        BookingTable.Rows[rowCount].Cells.Add(newCell);

                        //increment row count 
                        rowCount++;
                        i++;
                    }
                }

                if (bookingServiceList.Count == 1)
                {
                    newRow = new TableRow();
                    newRow.Height = 50;
                    BookingTable.Rows.Add(newRow);
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Service Description:";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "<a href='ViewProduct.aspx?ProductID=" + bookingServiceList[0].ServiceID.Replace(" ", string.Empty) + "'>" +
                    bookingServiceList[0].serviceDescripion.ToString() + "</a>";
                    BookingTable.Rows[rowCount].Cells.Add(newCell);

                    //increment row count 
                    rowCount++;
                }
                
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
                newCell.Text = "When:";
                BookingTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = BookingDetails.bookingStartTime.ToString("HH:mm") + " " + BookingDetails.bookingDate.ToString("dd MMM yyyy");
                BookingTable.Rows[rowCount].Cells.Add(newCell);

                //increment row count 
                rowCount++;
                #endregion

                #region invoice
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

                        //Billed to
                        newRow = new TableRow();
                        newRow.Height = 50;
                        BookingTable.Rows.Add(newRow);
                        newCell = new TableCell();
                        newCell.Text = "Billed To:";
                        BookingTable.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = BookingDetails.CustFullName.ToString();
                        BookingTable.Rows[rowCount].Cells.Add(newCell);

                        //increment row count 
                        rowCount++;

                        if (cookie["UT"] == "C")
                        {
                            //invoice from
                            newRow = new TableRow();
                            newRow.Height = 50;
                            BookingTable.Rows.Add(newRow);
                            newCell = new TableCell();
                            newCell.Text = "From:";
                            BookingTable.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = "Cheveux";
                            BookingTable.Rows[rowCount].Cells.Add(newCell);

                            //increment row count 
                            rowCount++;

                            //from address
                            BUSINESS bUSINESS = handler.getBusinessTable();
                            newRow = new TableRow();
                            newRow.Height = 50;
                            BookingTable.Rows.Add(newRow);
                            newCell = new TableCell();
                            BookingTable.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = bUSINESS.AddressLine1;
                            BookingTable.Rows[rowCount].Cells.Add(newCell);

                            //increment row count 
                            rowCount++;

                            newRow = new TableRow();
                            newRow.Height = 50;
                            BookingTable.Rows.Add(newRow);
                            newCell = new TableCell();
                            BookingTable.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = bUSINESS.AddressLine2;
                            BookingTable.Rows[rowCount].Cells.Add(newCell);

                            //increment row count 
                            rowCount++;
                        }

                        //calculate total price
                        double total = 0.0;

                        foreach (SP_getInvoiceDL item in invoice)
                        {
                            newRow = new TableRow();
                            newRow.Height = 50;
                            BookingTable.Rows.Add(newRow);
                            //fill in the item
                            newCell = new TableCell();
                            newCell.Text = item.Qty.ToString() + " " + item.itemName.ToString() + " @ R" + string.Format("{0:#.00}", item.price)
                            + " &nbsp;";
                            newRow.Cells.Add(newCell);
                            //fill in the Qty, unit price & TotalPrice
                            newCell = new TableCell();
                            newCell.HorizontalAlign = HorizontalAlign.Left;
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
                        newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = "<br/> Total Ecluding VAT: &nbsp; ";
                        BookingTable.Rows[rowCount].Cells.Add(newCell);
                        //fill in total Ecluding VAT
                        newCell = new TableCell();
                        newCell.HorizontalAlign = HorizontalAlign.Left;
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
                        newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = "VAT @" + VATRate + "% &nbsp; ";
                        BookingTable.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.HorizontalAlign = HorizontalAlign.Left;
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
                        newCell.HorizontalAlign = HorizontalAlign.Right;
                        newCell.Text = "<br/> Total Due: &nbsp; ";
                        BookingTable.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.HorizontalAlign = HorizontalAlign.Left;
                        newCell.Text = "<br/> R " + string.Format("{0:#.00}", total).ToString();
                        BookingTable.Rows[rowCount].Cells.Add(newCell);

                        //increment row count 
                        rowCount++;
                        #endregion

                        #region Review
                        //display review
                        //diaplay a heading
                        newRow = new TableRow();
                        newRow.Height = 50;
                        BookingTable.Rows.Add(newRow);
                        newCell = new TableCell();
                        BookingTable.Rows[rowCount].Cells.Add(newCell);

                        //increment row count 
                        rowCount++;
                        #endregion
                    }
                }

                #region Buttons
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
                    newCell.Text = "<a class='btn btn-primary' href='#' onClick='window.print()' >Print This Page  </a>";
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
                #endregion
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString() + "\n get getBookingDeatails method in viewbooking form");
                BookingLable.Text =
                        "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }
        }
        #endregion

        #region Edit Booking
        public void editBooking(string BookingID)
        {
            //display the booking edit form
            try
            {
                SP_GetCustomerBooking BookingDetails = handler.getCustomerUpcomingBookingDetails(BookingID);
                List<SP_GetBookingServices> bookingServiceList = handler.getBookingServices(BookingID);
                
                #region Heading
                if (bookingServiceList.Count == 1)
                {
                    //display a heading
                    BookingLable.Text = "<h2> Edit " + bookingServiceList[0].ServiceName.ToString() + " with " +
                    BookingDetails.stylistFirstName.ToString() + "</h2>";
                }
                else if (bookingServiceList.Count == 2)
                {
                    //display a heading
                    BookingLable.Text = "<h2> Edit " + bookingServiceList[0].ServiceName.ToString() +
                        " & " + bookingServiceList[1].ServiceName.ToString() + " with " +
                    BookingDetails.stylistFirstName.ToString() + "</h2>";
                }
                else if (bookingServiceList.Count > 2)
                {
                    //display a heading
                    BookingLable.Text = "<h2> Edit booking with " +
                    BookingDetails.stylistFirstName.ToString() + "</h2>";
                }
                #endregion

                //show the edit div
                Edit.Visible = true;

                #region Summary
                //row counter
                int rowCount = 0;

                //new row
                TableRow newRow = new TableRow();
                newRow.Height = 50;
                tblEditSummary.Rows.Add(newRow);

                //date and time
                TableCell newCell = new TableCell();
                newCell.Width = 150;
                newCell.Font.Bold = true;
                newCell.Text = "When";
                tblEditSummary.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Width = 150;
                newCell.Text = "<a href='ViewBooking.aspx?Action=Edit&BookingID="+ BookingDetails.bookingID+ "&EditType=DateTime'> "+
                    BookingDetails.bookingStartTime.ToString("HH:mm") + " " + BookingDetails.bookingDate.ToString("dd MMM yyyy") 
                    + " </a>";
                tblEditSummary.Rows[rowCount].Cells.Add(newCell);
                
                //increment Row Count 
                rowCount++;

                //new row
                newRow = new TableRow();
                newRow.Height = 50;
                tblEditSummary.Rows.Add(newRow);

                //stylist
                newCell = new TableCell();
                newCell.Width = 150;
                newCell.Font.Bold = true;
                newCell.Text = "With";
                tblEditSummary.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Width = 150;
                newCell.Text = "<a href='ViewBooking.aspx?Action=Edit&BookingID=" + BookingDetails.bookingID + "&EditType=Stylist'> " + 
                    BookingDetails.stylistFirstName.ToString()
                +" </a>";
                tblEditSummary.Rows[rowCount].Cells.Add(newCell);

                //increment Row Count 
                rowCount++;

                //new row
                newRow = new TableRow();
                newRow.Height = 50;
                tblEditSummary.Rows.Add(newRow);

                //services
                newCell = new TableCell();
                newCell.Width = 300;
                newCell.Font.Bold = true;
                if (bookingServiceList.Count == 1)
                {
                    newCell.Text = "Service:";
                }
                else if (bookingServiceList.Count > 1)
                {
                    newCell.Text = "Services:";
                }
                tblEditSummary.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Width = 300;
                if (bookingServiceList.Count == 1)
                {
                    newCell = new TableCell();
                    newCell.Text = "<a href='ViewBooking.aspx?Action=Edit&BookingID=" + BookingDetails.bookingID + "&EditType=Service'> " +
                     bookingServiceList[0].ServiceName.ToString() + "</a>";
                    newCell.Width = 700;
                    tblEditSummary.Rows[rowCount].Cells.Add(newCell);

                    //increment row count 
                    rowCount++;
                }
                else if (bookingServiceList.Count > 1)
                {
                    int i = 0;
                    foreach (SP_GetBookingServices service in bookingServiceList)
                    {
                        if (i > 0)
                        {
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblEditSummary.Rows.Add(newRow);
                            newCell = new TableCell();
                            newCell.Width = 300;
                            tblEditSummary.Rows[rowCount].Cells.Add(newCell);
                        }

                        newCell = new TableCell();
                        newCell.Text = "<a href='ViewBooking.aspx?Action=Edit&BookingID=" + BookingDetails.bookingID + "&EditType=Service'> " +
                        service.ServiceName.ToString() + "</a>";
                        newCell.Width = 700;
                        tblEditSummary.Rows[rowCount].Cells.Add(newCell);

                        //increment row count 
                        rowCount++;
                        i++;
                    }
                }

                if (bookingServiceList.Count == 1)
                {
                    newRow = new TableRow();
                    newRow.Height = 50;
                    tblEditSummary.Rows.Add(newRow);
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Service Description:";
                    tblEditSummary.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = bookingServiceList[0].serviceDescripion.ToString();
                    tblEditSummary.Rows[rowCount].Cells.Add(newCell);

                    //increment row count 
                    rowCount++;
                }

                //increment Row Count 
                rowCount++;
                #endregion

                #region Display Edit Div
                string editType = Request.QueryString["EditType"];
                if(editType == null)
                {
                    divEditNone.Visible = true;
                }
                else if (editType == "DateTime")
                {
                    divEditDateTime.Visible = true;
                    if (!IsPostBack)
                    {
                        calMAB.SelectedDate = BookingDetails.bookingDate;
                    }
                    loadEditDateAndTime();
                }
                else if (editType == "Stylist")
                {
                    loadEditStylist();
                    diveditStylist.Visible = true;
                }
                else if (editType == "Service")
                {
                    loadEditServices();
                    divEditService.Visible = true;
                }
                #endregion
            }
            catch (ApplicationException Err)
            {
                function.logAnError(Err.ToString() + "\n edit booking method in viewbooking form");
                BookingLable.Text =
                        "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }
        }

        #region keep track of selected time when editing date and time
        HttpCookie bookingTime = new HttpCookie("BookTime");
        #endregion

        public bool saveEdit(bool stylist, bool dateAndTime, bool service)
        {
            bool result = false;
            try
            {
                //get current booking details 
                SP_GetCustomerBooking BookingDetails = handler.getCustomerUpcomingBookingDetails(BookingID);
                List<SP_GetBookingServices> bookingServiceList = handler.getBookingServices(BookingID);

                //fill updated booking
                BOOKING updatedBooking = new BOOKING();
                updatedBooking.BookingID = BookingID;
                //date and time
                if(dateAndTime == false)
                {
                    //if unchanged
                    updatedBooking.SlotNo = BookingDetails.slotNo;
                    updatedBooking.Date = BookingDetails.bookingDate;
                }
                else
                {
                    //if changed
                    updatedBooking.Date = calMAB.SelectedDate;
                    HttpCookie bookingTime = Request.Cookies["BookTime"];
                    updatedBooking.SlotNo = bookingTime["TimeSlot"];
                }
                //services
                if (service == false)
                {
                    //if unchanged

                }
                else
                {
                    //if changed

                }
                //stylist
                if (stylist == false)
                {
                    //if unchanged
                    updatedBooking.StylistID = BookingDetails.stylistEmployeeID;
                }
                else
                {
                    //if changed
                    updatedBooking.StylistID = rblPickAStylist.SelectedValue;
                }
                //commit
                result = handler.updateBooking(updatedBooking);
            }
            catch (Exception err)
            {
                function.logAnError("Error commining booking edit to Db in saveEdit(Stylist:"+stylist
                    +" dateAndTime: "+dateAndTime+" service: "+service+") | bookingID: " + BookingID + 
                    " | Error:" + err);
                result = false;
            }
            return result;
        }

        #region load div content
        //Edit
        public void loadEditError()
        {
            diveditStylist.Visible = false;
            divEditNone.Visible = false;
            divEditDateTime.Visible = false;
            divEditService.Visible = false;
            divEditError.Visible = true;
        }

        //Stylist
        public void loadEditStylist()
        {
            try
            {
                int selctedStylistIndex = -1;
                List<int> disabledStylisIndex = new List<int>();
                int stylistIndexCount = 0;
                SP_GetCustomerBooking BookingDetails = handler.getCustomerUpcomingBookingDetails(BookingID);
                List<SP_GetStylists> stylistList = handler.BLL_GetAllStylists();
                foreach (SP_GetStylists stylist in stylistList)
                {
                    //define variable to store the value and text
                    string value = null; 
                    string text = "";

                    //create the item
                    ListItem item = new ListItem(text, value);

                    if (BookingDetails.stylistEmployeeID == stylist.UserID)
                    {
                        //the stylist is curently selected
                        text = stylist.FirstName + " - Specializes in " + stylist.ServiceName;
                        value = stylist.UserID;
                        item.Selected = true;
                        selctedStylistIndex = stylistIndexCount;
                    }
                    else
                    {
                        //check is stylist is avalible at the time
                        List<SP_GetBookedTimes> stylistBookedSlots = handler.BLL_GetBookedStylistTimes(
                            stylist.UserID, BookingDetails.bookingDate);
                        if (stylistBookedSlots.Count != 0)
                        {
                            foreach (SP_GetBookedTimes stylistBookedSlot in stylistBookedSlots)
                            {
                                if (stylistBookedSlot.SlotNo == BookingDetails.slotNo)
                                {
                                    //if stylist is unavalable 
                                    text = stylist.FirstName + " - (Unavalible at the selected time)";
                                    value = "";
                                    disabledStylisIndex.Add(stylistIndexCount);
                                }
                                else if (stylistBookedSlot.SlotNo != BookingDetails.slotNo)
                                {
                                    //if stylit is avalabile
                                    text = stylist.FirstName + " - Specializes in " + stylist.ServiceName;
                                    value = stylist.UserID;
                                }
                            }
                        }
                        else
                        {
                            //stylist is avalible all day
                            text = stylist.FirstName + " - Specializes in " + stylist.ServiceName;
                            value = stylist.UserID;
                        }
                    }
                    
                    //fill the item
                    item = new ListItem(text, value);
                    //add to the list
                    rblPickAStylist.Items.Add(item);

                    //increment count
                    stylistIndexCount++;
                }

                //curent stylist (Fix Needed)
                //rblPickAStylist.Items[selctedStylistIndex].Selected = true;

                //unavalible stylists
                if(disabledStylisIndex.Count != 0)
                {
                    foreach(int index in disabledStylisIndex)
                    {
                        rblPickAStylist.Items[index].Enabled = false;
                    }
                }
            }
            catch (Exception err)
            {
                function.logAnError("Error loading stylist in loadEditStylist() in viewbooking for edit . BookingID: " + BookingID + " | "
                    + err);
                loadEditError();
            }
        }

        //Service
        public void loadEditServices()
        {
            try
            {
                
            }
            catch (Exception err)
            {
                function.logAnError("Error loading service Selector in loadEditStylist() in viewbooking for edit. BookingID: " + BookingID + " | "
                    + err);
                loadEditError();
            }
        }

        #region date and time
        //array to keep track of dynamic time button values
        string[] availableTimes = new string[21];

        //date and time
        public void loadEditDateAndTime()
        {
            try
            {
                bookingTime["TimeSlot"] = "";
                Response.Cookies.Add(bookingTime);
                //load booking detais
                SP_GetCustomerBooking BookingDetails = handler.getCustomerUpcomingBookingDetails(BookingID);
                //set button counters
                int morningButtonCount = 1;
                int afternoonButtonCount = 11;
                //get stylist booked times
                List<SP_GetBookedTimes> bookedList = handler.BLL_GetBookedStylistTimes(BookingDetails.stylistEmployeeID, calMAB.SelectedDate);
                //get all slot times
                List<SP_GetSlotTimes> slotList = handler.BLL_GetAllTimeSlots();
                //hide all the buttons 
                HideButtons();
                //load time buttons
                foreach (SP_GetSlotTimes times in slotList)
                {
                    if (bookedList.Count != 0)
                    {
                        foreach (SP_GetBookedTimes booked in bookedList)
                        {
                            if (booked.SlotNo != times.SlotNo)
                            {
                                if (times.Time > Convert.ToDateTime("12:00"))
                                {
                                    if (afternoonButtonCount == 11)
                                    {
                                        btnAfternoon11.Visible = true;
                                        btnAfternoon11.Text = times.Time.ToString("HH:mm");
                                        availableTimes[11] = times.SlotNo;
                                    }
                                    else if (afternoonButtonCount == 12)
                                    {
                                        btnAfternoon12.Visible = true;
                                        btnAfternoon12.Text = times.Time.ToString("HH:mm");
                                        availableTimes[12] = times.SlotNo;
                                    }
                                    else if (afternoonButtonCount == 13)
                                    {
                                        btnAfternoon13.Visible = true;
                                        btnAfternoon13.Text = times.Time.ToString("HH:mm");
                                        availableTimes[13] = times.SlotNo;
                                    }
                                    else if (afternoonButtonCount == 14)
                                    {
                                        btnAfternoon14.Visible = true;
                                        btnAfternoon14.Text = times.Time.ToString("HH:mm");
                                        availableTimes[14] = times.SlotNo;
                                    }
                                    else if (afternoonButtonCount == 15)
                                    {
                                        btnAfternoon15.Visible = true;
                                        btnAfternoon15.Text = times.Time.ToString("HH:mm");
                                        availableTimes[15] = times.SlotNo;
                                    }
                                    else if (afternoonButtonCount == 16)
                                    {
                                        btnAfternoon16.Visible = true;
                                        btnAfternoon16.Text = times.Time.ToString("HH:mm");
                                        availableTimes[16] = times.SlotNo;
                                    }
                                    else if (afternoonButtonCount == 17)
                                    {
                                        btnAfternoon17.Visible = true;
                                        btnAfternoon17.Text = times.Time.ToString("HH:mm");
                                        availableTimes[17] = times.SlotNo;
                                    }
                                    else if (afternoonButtonCount == 18)
                                    {
                                        btnAfternoon18.Visible = true;
                                        btnAfternoon18.Text = times.Time.ToString("HH:mm");
                                        availableTimes[18] = times.SlotNo;
                                    }
                                    else if (afternoonButtonCount == 19)
                                    {
                                        btnAfternoon19.Visible = true;
                                        btnAfternoon19.Text = times.Time.ToString("HH:mm");
                                        availableTimes[19] = times.SlotNo;
                                    }
                                    else if (afternoonButtonCount == 20)
                                    {
                                        btnAfternoon20.Visible = true;
                                        btnAfternoon20.Text = times.Time.ToString("HH:mm");
                                        availableTimes[20] = times.SlotNo;
                                    }
                                    afternoonButtonCount++;
                                }
                                else
                                {
                                    if (morningButtonCount == 1)
                                    {
                                        btnMorning1.Visible = true;
                                        btnMorning1.Text = times.Time.ToString("HH:mm");
                                        availableTimes[1] = times.SlotNo;
                                    }
                                    else if (morningButtonCount == 2)
                                    {
                                        btnMorning2.Visible = true;
                                        btnMorning2.Text = times.Time.ToString("HH:mm");
                                        availableTimes[2] = times.SlotNo;
                                    }
                                    else if (morningButtonCount == 3)
                                    {
                                        btnMorning3.Visible = true;
                                        btnMorning3.Text = times.Time.ToString("HH:mm");
                                        availableTimes[3] = times.SlotNo;
                                    }
                                    else if (morningButtonCount == 4)
                                    {
                                        btnMorning4.Visible = true;
                                        btnMorning4.Text = times.Time.ToString("HH:mm");
                                        availableTimes[4] = times.SlotNo;
                                    }
                                    else if (morningButtonCount == 5)
                                    {
                                        btnMorning5.Visible = true;
                                        btnMorning5.Text = times.Time.ToString("HH:mm");
                                        availableTimes[5] = times.SlotNo;
                                    }
                                    else if (morningButtonCount == 6)
                                    {
                                        btnMorning6.Visible = true;
                                        btnMorning6.Text = times.Time.ToString("HH:mm");
                                        availableTimes[6] = times.SlotNo;
                                    }
                                    else if (morningButtonCount == 7)
                                    {
                                        btnMorning7.Visible = true;
                                        btnMorning7.Text = times.Time.ToString("HH:mm");
                                        availableTimes[7] = times.SlotNo;
                                    }
                                    else if (morningButtonCount == 8)
                                    {
                                        btnMorning8.Visible = true;
                                        btnMorning8.Text = times.Time.ToString("HH:mm");
                                        availableTimes[8] = times.SlotNo;
                                    }
                                    else if (morningButtonCount == 9)
                                    {
                                        btnMorning9.Visible = true;
                                        btnMorning9.Text = times.Time.ToString("HH:mm");
                                        availableTimes[9] = times.SlotNo;
                                    }
                                    else if (morningButtonCount == 10)
                                    {
                                        btnMorning10.Visible = true;
                                        btnMorning10.Text = times.Time.ToString("HH:mm");
                                        availableTimes[10] = times.SlotNo;
                                    }
                                    morningButtonCount++;
                                }

                            }
                        }
                    }
                    else
                    {
                        if (times.Time > Convert.ToDateTime("12:00"))
                        {
                            if (afternoonButtonCount == 11)
                            {
                                btnAfternoon11.Visible = true;
                                btnAfternoon11.Text = times.Time.ToString("HH:mm");
                                availableTimes[11] = times.SlotNo;
                            }
                            else if (afternoonButtonCount == 12)
                            {
                                btnAfternoon12.Visible = true;
                                btnAfternoon12.Text = times.Time.ToString("HH:mm");
                                availableTimes[12] = times.SlotNo;
                            }
                            else if (afternoonButtonCount == 13)
                            {
                                btnAfternoon13.Visible = true;
                                btnAfternoon13.Text = times.Time.ToString("HH:mm");
                                availableTimes[13] = times.SlotNo;
                            }
                            else if (afternoonButtonCount == 14)
                            {
                                btnAfternoon14.Visible = true;
                                btnAfternoon14.Text = times.Time.ToString("HH:mm");
                                availableTimes[14] = times.SlotNo;
                            }
                            else if (afternoonButtonCount == 15)
                            {
                                btnAfternoon15.Visible = true;
                                btnAfternoon15.Text = times.Time.ToString("HH:mm");
                                availableTimes[15] = times.SlotNo;
                            }
                            else if (afternoonButtonCount == 16)
                            {
                                btnAfternoon16.Visible = true;
                                btnAfternoon16.Text = times.Time.ToString("HH:mm");
                                availableTimes[16] = times.SlotNo;
                            }
                            else if (afternoonButtonCount == 17)
                            {
                                btnAfternoon17.Visible = true;
                                btnAfternoon17.Text = times.Time.ToString("HH:mm");
                                availableTimes[17] = times.SlotNo;
                            }
                            else if (afternoonButtonCount == 18)
                            {
                                btnAfternoon18.Visible = true;
                                btnAfternoon18.Text = times.Time.ToString("HH:mm");
                                availableTimes[18] = times.SlotNo;
                            }
                            else if (afternoonButtonCount == 19)
                            {
                                btnAfternoon19.Visible = true;
                                btnAfternoon19.Text = times.Time.ToString("HH:mm");
                                availableTimes[19] = times.SlotNo;
                            }
                            else if (afternoonButtonCount == 20)
                            {
                                btnAfternoon20.Visible = true;
                                btnAfternoon20.Text = times.Time.ToString("HH:mm");
                                availableTimes[20] = times.SlotNo;
                            }
                            afternoonButtonCount++;
                        }
                        else
                        {
                            if (morningButtonCount == 1)
                            {
                                btnMorning1.Visible = true;
                                btnMorning1.Text = times.Time.ToString("HH:mm");
                                availableTimes[1] = times.SlotNo;
                            }
                            else if (morningButtonCount == 2)
                            {
                                btnMorning2.Visible = true;
                                btnMorning2.Text = times.Time.ToString("HH:mm");
                                availableTimes[2] = times.SlotNo;
                            }
                            else if (morningButtonCount == 3)
                            {
                                btnMorning3.Visible = true;
                                btnMorning3.Text = times.Time.ToString("HH:mm");
                                availableTimes[3] = times.SlotNo;
                            }
                            else if (morningButtonCount == 4)
                            {
                                btnMorning4.Visible = true;
                                btnMorning4.Text = times.Time.ToString("HH:mm");
                                availableTimes[4] = times.SlotNo;
                            }
                            else if (morningButtonCount == 5)
                            {
                                btnMorning5.Visible = true;
                                btnMorning5.Text = times.Time.ToString("HH:mm");
                                availableTimes[5] = times.SlotNo;
                            }
                            else if (morningButtonCount == 6)
                            {
                                btnMorning6.Visible = true;
                                btnMorning6.Text = times.Time.ToString("HH:mm");
                                availableTimes[6] = times.SlotNo;
                            }
                            else if (morningButtonCount == 7)
                            {
                                btnMorning7.Visible = true;
                                btnMorning7.Text = times.Time.ToString("HH:mm");
                                availableTimes[7] = times.SlotNo;
                            }
                            else if (morningButtonCount == 8)
                            {
                                btnMorning8.Visible = true;
                                btnMorning8.Text = times.Time.ToString("HH:mm");
                                availableTimes[8] = times.SlotNo;
                            }
                            else if (morningButtonCount == 9)
                            {
                                btnMorning9.Visible = true;
                                btnMorning9.Text = times.Time.ToString("HH:mm");
                                availableTimes[9] = times.SlotNo;
                            }
                            else if (morningButtonCount == 10)
                            {
                                btnMorning10.Visible = true;
                                btnMorning10.Text = times.Time.ToString("HH:mm");
                                availableTimes[10] = times.SlotNo;
                            }
                            morningButtonCount++;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                function.logAnError("Error loading Date Time Selector in loadEditStylist() in viewbooking for edit. BookingID: "+BookingID+" | "
                    + err);
                loadEditError();
            }
        }

        //remove dates befor today
        protected void calMAB_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date <= DateTime.Now.AddDays(1))
            {
                e.Cell.BackColor = ColorTranslator.FromHtml("#a9a9a9");
                e.Day.IsSelectable = false;
            }
        }

        #region Time Buttons Functions
        private void HideButtons()
        {
            btnAfternoon11.Visible = false;
            btnAfternoon12.Visible = false;
            btnAfternoon13.Visible = false;
            btnAfternoon14.Visible = false;
            btnAfternoon15.Visible = false;
            btnAfternoon16.Visible = false;
            btnAfternoon17.Visible = false;
            btnAfternoon18.Visible = false;
            btnAfternoon19.Visible = false;
            btnAfternoon20.Visible = false;
            btnMorning1.Visible = false;
            btnMorning2.Visible = false;
            btnMorning3.Visible = false;
            btnMorning4.Visible = false;
            btnMorning5.Visible = false;
            btnMorning6.Visible = false;
            btnMorning7.Visible = false;
            btnMorning8.Visible = false;
            btnMorning9.Visible = false;
            btnMorning10.Visible = false;
        }

        private void deselectButton()
        {
            btnAfternoon11.CssClass = "btn btn-light";
            btnAfternoon12.CssClass = "btn btn-light";
            btnAfternoon13.CssClass = "btn btn-light";
            btnAfternoon14.CssClass = "btn btn-light";
            btnAfternoon15.CssClass = "btn btn-light";
            btnAfternoon16.CssClass = "btn btn-light";
            btnAfternoon17.CssClass = "btn btn-light";
            btnAfternoon18.CssClass = "btn btn-light";
            btnAfternoon19.CssClass = "btn btn-light";
            btnAfternoon20.CssClass = "btn btn-light";
            btnMorning1.CssClass = "btn btn-light";
            btnMorning2.CssClass = "btn btn-light";
            btnMorning3.CssClass = "btn btn-light";
            btnMorning4.CssClass = "btn btn-light";
            btnMorning5.CssClass = "btn btn-light";
            btnMorning6.CssClass = "btn btn-light";
            btnMorning7.CssClass = "btn btn-light";
            btnMorning8.CssClass = "btn btn-light";
            btnMorning9.CssClass = "btn btn-light";
            btnMorning10.CssClass = "btn btn-light";
        }
        
        #region monrning
        protected void btnMorning1_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning1.CssClass = "btn btn-primary";
        }

        protected void btnMorning2_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[2];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning2.CssClass = "btn btn-primary";
        }

        protected void btnMorning3_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[3];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning3.CssClass = "btn btn-primary";
        }

        protected void btnMorning4_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[4];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning4.CssClass = "btn btn-primary";
        }

        protected void btnMorning5_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[5];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning5.CssClass = "btn btn-primary";
        }

        protected void btnMorning6_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[6];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning6.CssClass = "btn btn-primary";
        }

        protected void btnMorning7_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[7];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning7.CssClass = "btn btn-primary";
        }

        protected void btnMorning8_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[8];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning8.CssClass = "btn btn-primary";
        }

        protected void btnMorning9_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[9];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning9.CssClass = "btn btn-primary";
        }
        #endregion

        #region afternoon
        protected void btnMorning10_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[10];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning10.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon11_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[11];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon11.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon12_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[12];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon12.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon13_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[13];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon13.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon14_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[14];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon14.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon15_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[15];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon15.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon16_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[16];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon16.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon17_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[17];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon17.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon18_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[18];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon18.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon19_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[19];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon19.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon20_Click(object sender, EventArgs e)
        {
            loadEditDateAndTime();
            bookingTime["TimeSlot"] = availableTimes[20];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon20.CssClass = "btn btn-primary";
        }
        #endregion
        #endregion
        #endregion
        #endregion

        #region btn functions / cal functions
        //redirect back to previous page
        protected void btnDoneEdit_Click(object sender, EventArgs e)
        {
            if (PreviousPageAdress == null)
            {
                Response.Redirect("Profile.aspx");
            }
            else if (PreviousPageAdress == "Receptionist")
            {
                Response.Redirect("../Receptionist/Receptionist.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewBooking.aspx?Action=Edit&BookingID=" + BookingID);
        }

        #region Date & Time
        protected void btnSaveEditDateAndTime_Click(object sender, EventArgs e)
        {
            HttpCookie bookingTime = Request.Cookies["BookTime"];
            string SlotNo = bookingTime["TimeSlot"];
            if (SlotNo != "")
            {
                //save edit
                bool result = saveEdit(false, true, false);

                if (result == true)
                {
                    //return to edit page
                    btnCancel_Click(sender, e);
                }
                else
                {
                    loadEditError();
                }
            }
            else
            {
                //return to edit page
                btnCancel_Click(sender, e);
            }
        }

        protected void calMAB_SelectionChanged(object sender, EventArgs e)
        {
            loadEditDateAndTime();
        }
        #endregion

        #region Stylist
        protected void btnSaveEditStylist_Click(object sender, EventArgs e)
        {
            if (rblPickAStylist.SelectedValue != null
                && rblPickAStylist.SelectedValue != "")
            {
                //save edit
                bool result = saveEdit(true, false, false);

                if (result == true)
                {
                    //return to edit page
                    btnCancel_Click(sender, e);
                }
                else
                {
                    loadEditError();
                }
            }
            else
            {
                //return to edit page
                btnCancel_Click(sender, e);
            }
        }
        #endregion

        #region Service
        protected void btnSaveEditService_Click(object sender, EventArgs e)
        {
            //save edit
            bool result = saveEdit(false, false, true);

            if (result == true)
            {
                //return to edit page
                btnCancel_Click(sender, e);
            }
            else
            {
                loadEditError();
            }
        }
        #endregion
        #endregion
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
        
        #region Check Out
        Tuple<List<SP_GetAllAccessories>, List<SP_GetAllTreatments>> products = null;
        //calculate total price
        double total = 0.0;
        SP_GetCustomerBooking BookingDetails = null;

        public void checkOut(string BookingID)
        {            
            //display the booking detail
            try
            {
                //get the details from the db
                List<SP_getInvoiceDL> invoicDetailLines = null;
                List<SP_GetBookingServices> bookingServiceList = null;
                //get booking deatils
                BookingDetails = handler.getBookingDetaisForCheckOut(BookingID);
                //get the services to add to the sale
                bookingServiceList = handler.getBookingServices(BookingID);

                //check if sales record exists
                //if sales record dose not exist make a new one
                if (handler.getInvoiceDL(BookingID).Count == 0)
                {
                    //create sales record
                    handler.createSalesRecord(BookingID);
                    //add booking to invoice for each product
                    foreach (SP_GetBookingServices service in bookingServiceList)
                    {
                        SALES_DTL detailLine = new SALES_DTL();
                        detailLine.ProductID = service.ServiceID;
                        detailLine.SaleID = BookingID;
                        detailLine.Qty = 1;
                        handler.createSalesDTLRecord(detailLine);
                    }
                }

                //get the invoice 
                invoicDetailLines = handler.getInvoiceDL(BookingID);
                
                //un-hide the checkout table 
                divCheckOut.Visible = true;

                #region Booking Details
                //display a heading
                BookingLable.Text = "<h2> Check Out </h2>";

                //create a variable to track the row count
                int rowCount = 0;

                //add booking details to the table

                //Billed To
                tblCheckOut.Rows[rowCount].Cells[1].Text = BookingDetails.CustFullName.ToString();

                //increment row count 
                rowCount++;

                //Date & Time
                tblCheckOut.Rows[rowCount].Cells[1].Text = BookingDetails.bookingStartTime.ToString("HH:mm")
                    + " " + BookingDetails.bookingDate.ToString("dd-MMM-yyyy");

                //increment row count 
                rowCount++;

                //service name
                int i = 0;
                if (bookingServiceList.Count == 1)
                {
                    tblCheckOut.Rows[rowCount].Cells[1].Text = bookingServiceList[0].ServiceName.ToString();
                }
                else if (bookingServiceList.Count > 1)
                {
                    string services = "";
                    foreach (SP_GetBookingServices service in bookingServiceList)
                    {
                        if (i == 0)
                        {
                            services += " " + service.ServiceName.ToString();
                            i++;
                        }
                        else
                        {
                            services += ", " + service.ServiceName.ToString();
                        }
                    }
                    tblCheckOut.Rows[rowCount].Cells[1].Text = services;
                }

                //increment row count 
                rowCount++;

                //stylist
                tblCheckOut.Rows[rowCount].Cells[1].Text = BookingDetails.stylistFirstName.ToString();

                //increment row count 
                rowCount++;
                rowCount++;
                #endregion

                //diplay invoice
                #region invoice
                //get invoice details
                List<SP_getInvoiceDL> invoice = handler.getInvoiceDL(BookingID);

                //create a table for the invoice (To be added to tblCheckOut cell)
                string tblInvoice = "<table>";
                
                foreach (SP_getInvoiceDL item in invoice)
                {
                    //new row
                    tblInvoice += "<tr>";
                    //add a new cell to the row
                    //fill in the item
                    tblInvoice += "<td  Width='250'>" + item.Qty.ToString() + " " + item.itemName.ToString() + " @ R" + item.price.ToString() + "&#09; </td>";

                    //add a new cell to the row
                    //fill in the Qty, unit price & TotalPrice
                    tblInvoice += "<td align='left' Width='250'> R" + Math.Round((item.Qty * item.price), 2).ToString() + "</td>";
                    tblInvoice += "</tr>";

                    //increment final price
                    total += item.Qty * item.price;
                }

                // get vat info
                Tuple<double, double> vatInfo = function.getVat(total);

                //display total including and Excluding VAT
                //new row
                tblInvoice += "<tr>";

                tblInvoice += "<td align='right'> <br/> Total Ecluding VAT: &nbsp; </td>";

                //fill in total Ecluding VAT

                tblInvoice += "<td align='left'> <br/> R" + Math.Round(vatInfo.Item1, 2).ToString() + "</td>";
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
                tblInvoice += "<td align='right'> VAT @" + VATRate + "% &nbsp; </td>";

                tblInvoice += "<td align='left'> R" + Math.Round(vatInfo.Item2, 2).ToString() + "</td>";

                //display the total due//new row
                tblInvoice += "</tr><tr>";

                //fill in total
                tblInvoice += "<td align='right'> <br/> Total Due: &nbsp; </td>";

                tblInvoice += "<td align='left'> <br/>R" + total.ToString() + "</td>";
                tblInvoice += "</tr>";

                tblInvoice += "</table>";

                //add the invoice to the table
                tblCheckOut.Rows[rowCount].Cells[0].Text = tblInvoice;

                //increment row count 
                rowCount++;
                rowCount++;
                rowCount++;
                #endregion

                #region PaymentType
                //check if paymentType Exists
                string paymentType = handler.getSalePaymentType(BookingID);
                if (paymentType != "")
                {
                    divPamentType.Visible = false;
                    //show payment type
                    tblCheckOut.Rows[4].Width = 50;
                    tblCheckOut.Rows[4].Cells[0].Text = "Payment Type:";
                    tblCheckOut.Rows[4].Cells[1].Text = paymentType;
                    //hide add product button
                    tblCheckOut.Rows[6].Cells[1].Text = "";
                    //add Print invoice button
                    tblCheckOut.Rows[7].Width = 50;
                    tblCheckOut.Rows[7].Cells[1].Text = "<a class='btn btn-primary' href='#' onClick='window.print()' >Print Invoice  </a>";
                }
                else
                {
                    tblCheckOut.Rows[4].Width = 0;
                    tblCheckOut.Rows[4].Cells[0].Text = "";
                    tblCheckOut.Rows[4].Cells[1].Text = "";
                    //hide print button
                    tblCheckOut.Rows[7].Width = 0;
                    tblCheckOut.Rows[7].Cells[1].Text = "";
                }
                #endregion
                divCheckOutInvoice.Visible = true;
            }
            catch (Exception Err)
            {
                divCheckOutInvoice.Visible = false;
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
                total = 0.0;
                checkOut(BookingID);
                //send booking completion email
                USER user = handler.GetUserDetails(BookingDetails.CustomerID);
                //send an email notification
                var body = new System.Text.StringBuilder();
                body.AppendFormat("Hello " + user.FirstName.ToString() + ",");
                body.AppendLine(@"");
                body.AppendLine(@"Thank you for choosing Cheveux,");
                body.AppendLine(@"view your invoice here --> http://sict-iis.nmmu.ac.za/beauxdebut/ViewBooking.aspx?BookingType=Past&BookingID=" + BookingDetails.bookingID.ToString().Replace(" ", string.Empty)+".");
                body.AppendLine(@"");
                body.AppendLine(@"Make your next booking now --> http://sict-iis.nmmu.ac.za/beauxdebut/MakeABooking.aspx.");
                body.AppendLine(@"");
                body.AppendLine(@"Regards,");
                body.AppendLine(@"");
                body.AppendLine(@"The Cheveux Team");
                function.sendEmailAlert(user.Email, user.FirstName + " " + user.LastName,
                    "Booking Invoice",
                    body.ToString(),
                    "Bookings Cheveux");
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
                    int prodCount = 0;
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
                            prodCount++;
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
                            prodCount++;
                        }
                    }

                    //if no products found matching the criteria
                    if (prodCount == 0)
                    {
                        lbProducts.Items.Add("No Products Found");
                    }

                        //show the add product to sale view
                    divCheckOut.Visible = false;
                        divAddProducts.Visible = true;

                    //load products currently on the invoice
                    removeProducts();
                }
                else
                {
                    Response.Write("<script>alert('An error occoured loading products, Please try again later.');location.reload(true);</script>");
                    //return to check out
                    divAddProducts.Visible = false;
                    BookingID = Request.QueryString["BookingID"];
                    checkOut(BookingID);
                    divCheckOut.Visible = true;
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving list of products" 
                    + " in btnAddProduct_Click(object sender, EventArgs e) method on viewBookings Page");
                Response.Write("<script>alert('An error occoured loading products, Please try again later.');location.reload(true);</script>");
                //return to check out
                divAddProducts.Visible = false;
                BookingID = Request.QueryString["BookingID"];
                checkOut(BookingID);
                divCheckOut.Visible = true;
            }
        }

        protected void loadproductIDs(char type)
        {
            //add items
            if (type == 'A')
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
            //remove items
            else if (type == 'R')
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
                            productSaleQty.Add(item.Qty);
                        }
                    }
                }
            }
        }
        
        protected void btnAddProductToSale_Click(object sender, EventArgs e)
        {
                //a product to ivoice
                if (lbProducts.SelectedIndex >= 0)
                {
                    try
                    {
                        //load the product ids
                        loadproductIDs('A');
                        //add the selectedproduct to the sale
                        SALES_DTL newItem = new SALES_DTL();
                        newItem.SaleID = BookingID;
                        newItem.ProductID = productIDs[lbProducts.SelectedIndex];
                        newItem.Qty = int.Parse(Qty.SelectedValue);
                        handler.createProductSalesDTLRecord(newItem);
                        btnAddProduct_Click(sender, e);
                    Qty.SelectedIndex = 0;
                    }
                    catch (Exception Err)
                    {
                        function.logAnError(" An error occurred adding product to sales record"
                            + " in btnAddProductToSale_Click(object sender, EventArgs e) method on viewBookings Page: " + Err.ToString());
                        Response.Write("<script>alert('An error occoured adding the product, Please try again later.');location.reload(false);</script>");
                    //return to check out divAddProducts.Visible = false;
                    divAddProducts.Visible = false;
                    BookingID = Request.QueryString["BookingID"];
                    checkOut(BookingID);
                    divCheckOut.Visible = true;
                    }
                }
                else
                {
                    btnAddProduct_Click(sender, e);
                }
        }

        protected void btnRemoveProductFromSale_Click(object sender, EventArgs e)
        {
            //remove product from ivoice
            if (lProductsOnSale.SelectedIndex >= 0)
            {
                try
                {
                    //load the product ids
                    loadproductIDs('R');
                    //remove the selected product to the sale
                    SALES_DTL removeItem = new SALES_DTL();
                    removeItem.SaleID = BookingID;
                    removeItem.ProductID = productIDs[lProductsOnSale.SelectedIndex];
                    if (int.Parse(Qty.SelectedValue) < productSaleQty[lProductsOnSale.SelectedIndex])
                    {
                        removeItem.Qty = int.Parse(Qty.SelectedValue);
                        handler.UpdateProductSalesDTLRecordQty(removeItem);
                    }
                    else
                    {
                        removeItem.Qty = productSaleQty[lProductsOnSale.SelectedIndex];
                        handler.removeProductSalesDTLRecord(removeItem);
                    }
                    removeProducts();
                    Qty.SelectedIndex = 0;
                }
                catch (Exception Err)
                {
                    function.logAnError(" An error occurred removing product from sales record"
                        + " in btnRemoveProductFromSale_Click(object sender, EventArgs e) method on viewBookings Page: " + Err.ToString());
                    Response.Write("<script>alert('An error occoured removing the product, Please try again later.');location.reload(true);</script>");
                    //return to check out divAddProducts.Visible = false;
                    divAddProducts.Visible = false;
                    BookingID = Request.QueryString["BookingID"];
                    checkOut(BookingID);
                    divCheckOut.Visible = true;
                }
            }
            else
            {
                btnAddProduct_Click(sender, e);
            }
        }

        protected void removeProducts()
        {
            //add Products to the list
            lProductsOnSale.Items.Clear();
            try
            {
                //track the number of products
                int count = 0;
                //get invoice details and the current products
                List<SP_getInvoiceDL> invoice = handler.getInvoiceDL(BookingID);
                if (invoice.Count != 0)
                {
                    //add 
                    foreach (SP_getInvoiceDL item in invoice)
                    {
                        if (item.itemType != "S")
                        {
                            lProductsOnSale.Items.Add(item.Qty.ToString()+"x "+
                                item.itemName.ToString());
                            count++;
                        }
                    }
                    
                    //if there are no products let the user know
                    if(count == 0)
                    {
                        lProductsOnSale.Items.Add("No products added");
                    }

                    //show the add product to sale view
                    divCheckOut.Visible = false;
                    divAddProducts.Visible = true;
                }
                else
                {
                    Response.Write("<script>alert('An error occoured loading products, Please try again later.');location.reload(true);</script>");
                    //return to check out
                    divAddProducts.Visible = false;
                    BookingID = Request.QueryString["BookingID"];
                    checkOut(BookingID);
                    divCheckOut.Visible = true;
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving list of products"
                    + " in btnAddProduct_Click(object sender, EventArgs e) method on viewBookings Page");
                Response.Write("<script>alert('An error occoured loading products, Please try again later.');location.reload(true);</script>");
                //return to check out
                divAddProducts.Visible = false;
                BookingID = Request.QueryString["BookingID"];
                checkOut(BookingID);
                divCheckOut.Visible = true;
            }
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
        
        protected void PaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(PaymentType.SelectedIndex == 0)
            {
                divCalcuateChange.Visible = true;
                lAmountDue.Text = "R" + total.ToString();
            }
            else
            {
                divCalcuateChange.Visible = false;
            }
        }

        protected void txtAmounTenderd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double change = Convert.ToDouble(txtAmounTenderd.Text.ToString()) - total;
                if (change < 0)
                {
                    lChangeDue.Text = "Insufficient funds";
                }
                else if (change == 0)
                {
                    lChangeDue.Text = "No Change";
                }
                else
                {
                    lChangeDue.Text = "R" + Math.Round(change, 2).ToString();
                }
            }
            catch (Exception err)
            {
                function.logAnError("invalid Amount entered when caluclation chage on check out on " +
                    "txtAmounTenderd_TextChanged(object sender, EventArgs e) of view booking. Amount: " 
                    + txtAmounTenderd.Text.ToString() + " Error: " + err);
                lChangeDue.Text = "Invalid value for amount tendered";
            }
        }
        #endregion
    }
}