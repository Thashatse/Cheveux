﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;
namespace Cheveux
{
    public partial class CustomerVisit : System.Web.UI.Page
    {
        String test = DateTime.Now.ToString("dddd d MMMM");
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();

        //get a descriptive detail of the booking
        SP_GetAllofBookingDTL bDTL = null;
        
        //get bookings service details
        SP_GetBookingServiceDTL sDTL = null;

        //SP_ViewCustVisit viewCustomerVisit = null;

        //bookingID is going to go in here
        string bookingID;
        //customerID is going to go in here
        string customerID;

        CUST_VISIT visit;

        protected void Page_Load(object sender, EventArgs e)
        {
            //access control
            HttpCookie UserID = Request.Cookies["CheveuxUserID"];
            //send the user to the correct page based on their usertype
            if (UserID != null)
            {
                string userType = UserID["UT"].ToString();
                if ("R" == userType)
                {
                    //Receptionist
                    Response.Redirect("Receptionist.aspx");
                }
                else if (userType == "M")
                {
                    //Manager
                    //allowed access to this page
                    Response.Redirect("Manager.aspx");
                }
                else if (userType == "S")
                {
                    //stylist
                    //allowed access to this page
                    //Response.Redirect("Stylist.aspx");
                }
                else if (userType == "C")
                {
                    //customer
                    Response.Redirect("Default.aspx");

                }
                else
                {
                    Response.Redirect("Default.aspx");
                    function.logAnError("Unknown user type found during Loading of default.aspx: " +
                        UserID["UT"].ToString());
                }
            }
            else
            {
                //ask the user to login first 

                //temp fix redirect to home page
                Response.Redirect("Default.aspx");
            }
            
            theDate.InnerHtml = test;
            
            //set bookingID param to bookingID variable so it can be used in the methods
            bookingID = Request.QueryString["bookingID"];
            //set customerID param to customerID variable so it can be used in the methods
            customerID = Request.QueryString["customerID"];
            if (bookingID != null && customerID != null)
            {
                DisplayBookingDetails(bookingID, customerID);
                DisplayServiceDetails(bookingID, customerID);
                //DisplayConfirmVisit(customerID, bookingID);
            }
            else
            {
                Response.Write("<script>alert('ID's not passed.');window.location='Stylist.aspx';</script>");
            }
 
        }

        public void DisplayBookingDetails(string bookingID, string customerID)
        {
            try
            {

                bDTL = handler.BLL_GetAllofBookingDTL(bookingID.Replace(" ", string.Empty), customerID);

                //create a variablew to track the row count
                int rCnt = 0;
                //create a new row in the table and set the height
                TableRow newRow = new TableRow();
                newRow.Height = 50;
                //add the row to the table
                allBookingTable.Rows.Add(newRow);
                //create a new cell in that row and set the width
                TableCell newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "BookingID:";
                newCell.Width = 300;
                //add the cell to the tablerow in the table
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                //add a new cell that will display the data from the database
                newCell = new TableCell();
                newCell.Text = bDTL.BookingID.ToString();
                newCell.Width = 700;
                //add the cell to the row in the table
                allBookingTable.Rows[rCnt].Cells.Add(newCell);

                //increment row count 
                rCnt++;


                newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "CustomerID:";
                newCell.Width = 300;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.CustomerID.ToString();
                newCell.Width = 700;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                rCnt++;


                newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Customer Name:";
                newCell.Width = 300;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.CustomerName.ToString();
                newCell.Width = 700;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                rCnt++;


                newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Service Name:";
                newCell.Width = 300;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.ServiceName.ToString();
                newCell.Width = 700;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                rCnt++;


                newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Service Description:";
                newCell.Width = 300;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.ServiceDescription.ToString();
                newCell.Width = 700;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                rCnt++;


                newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Price:";
                newCell.Width = 300;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.Price.ToString();
                newCell.Width = 700;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                rCnt++;


                newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Date:";
                newCell.Width = 300;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.Date.ToString();
                newCell.Width = 700;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                rCnt++;


                newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Start Time - End Time:";
                newCell.Width = 300;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.StartTime.ToString() + " - " + bDTL.EndTime.ToString();
                newCell.Width = 700;
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                rCnt++;


                newRow = new TableRow();
                newRow.Height = 250;
                allBookingTable.Rows.Add(newRow);

                newCell = new TableCell();
                newCell.Width = 700;
                Button btnUpdateVisit = new Button();
                btnUpdateVisit.Text = "Update Visit";
                btnUpdateVisit.CssClass = "btn btn-outline-dark";
                btnUpdateVisit.Click += (ss, ee) => {
                    //hide other placeholders headings and make the appropriate placeholder heading visible
                    //takes user to content displaying the service details
                    phBookingDetails.Visible = false;
                    phServiceDetails.Visible = true;
                    //phConfirmVisit.Visible = false;
                    lblBookingDetailsHeading.Visible = false;
                    lblServiceHeading.Visible = true;
                    //lblConfirmUpdateHeading.Visible = false;
                };
                newCell.Controls.Add(btnUpdateVisit);
                allBookingTable.Rows[rCnt].Cells.Add(newCell);
                rCnt++;

                //hide other placeholders headings and make the appropriate placeholder heading visible
                phServiceDetails.Visible = false;
                //phConfirmVisit.Visible = false;
                lblBookingDetailsHeading.Visible = true;
                lblServiceHeading.Visible = false;
                //lblConfirmUpdateHeading.Visible = false;
            }
            catch(Exception Err)
            {
                //log error, display error message,redirect to the stylist page
                function.logAnError(Err.ToString());
                Response.Write("<script>alert('An error has occured.Unable to display required data.');window.location='Stylist.aspx';</script>");
            }
        }
        
        public void DisplayServiceDetails(string bookingID,string customerID)
        {
            try
            {
                sDTL = handler.BLL_GetBookingServiceDTL(bookingID, customerID);

                //create a variablew to track the row count
                int rowCount = 0;
                //create a new row in the table and set the height
                TableRow newRow = new TableRow();
                newRow.Height = 50;
                serviceDetailsTable.Rows.Add(newRow);
                //create cell for bookingID and it to the row
                TableCell newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "BookingID:";
                newCell.Width = 300;
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);
                
                //create a cell that displays the bookingID and add it to the row
                newCell = new TableCell();
                newCell.Text = bDTL.BookingID.ToString();
                newCell.Width = 700;
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);
                rowCount++;

                //create a new row and add it to the table
                newRow = new TableRow();
                newRow.Height = 50;
                serviceDetailsTable.Rows.Add(newRow);
                
                //create a cell for the service name and add it to the table
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Service Name:";
                newCell.Width = 300;
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);

                //create a cell that will display the service name and add it to the row. 
                newCell = new TableCell();
                newCell.Text = bDTL.ServiceName.ToString();
                newCell.Width = 700;
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);
                //increment rowCount
                rowCount++;

                //create a new row and add it to the table
                newRow = new TableRow();
                newRow.Height = 50;
                serviceDetailsTable.Rows.Add(newRow);

                //create a cell for the service description and add it to the row
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Service Description:";
                newCell.Width = 300;
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);

                //create a cell that will be populated by the text box
                newCell = new TableCell();
                //newCell.Text = bDTL.ServiceDescription.ToString();
                newCell.Width = 700;
                TextBox descBox = new TextBox();
                descBox.ID = "service_description";
                descBox.CssClass = "form-control";
                //add control to cell
                newCell.Controls.Add(descBox);
                //add cell to row
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);
                //increment rowCount
                rowCount++;

                //create a new row and add it to the table
                newRow = new TableRow();
                newRow.Height = 250; 
                serviceDetailsTable.Rows.Add(newRow);

                //create a cell for the back button and add the button control to the cell
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Width = 300;
                Button sbtnBack = new Button();
                sbtnBack.Text = "Back";
                sbtnBack.CssClass = "btn btn-outline-dark";
                sbtnBack.Click += (ss, ee) => {
                    //hide other placeholders headings and make the appropriate placeholder heading visible
                    //will go back to bookings details content
                    phBookingDetails.Visible = true;
                    phServiceDetails.Visible = false;
                    phConfirmVisit.Visible = false;
                    lblBookingDetailsHeading.Visible = true;
                    lblServiceHeading.Visible = false;
                    lblConfirmUpdateHeading.Visible = false;
                };
                newCell.Controls.Add(sbtnBack);
                //add cell to the row
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);

                //create a cell for the update button and add the button control to the cell 
                newCell = new TableCell();
                newCell.Width = 700;
                Button sbtnUpdate = new Button();
                sbtnUpdate.Text = "Update Visit";
                sbtnUpdate.CssClass = "btn btn-outline-dark";
                sbtnUpdate.Click += (ss, ee) => {

                    /* What this button does:
                     * =====================
                     * 
                     * User would click on button 
                     * Button would call update procedure
                     * Update the necessary data    (Create the update stored procedure)
                     * And then reload the page 
                     * And show the updated version of the data
                     * code still to be added 
                     * 
                     */
                    try
                    {
                        visit = new CUST_VISIT();

                        visit.CustomerID = Convert.ToString(customerID);
                        visit.BookingID = Convert.ToString(bookingID);
                        visit.Description = Convert.ToString(descBox.Text);

                        if (handler.BLL_UpdateCustVisit(visit))
                        {
                            Response.Write("<script>alert('Update Successful.');</script>");
                            Response.Redirect("Stylist.aspx");
                        }
                        else
                        {
                            /*
                             * if the update fails, display failed message
                             * to alert that the update was not successful
                             * (user friendly action status response)
                             */
                            Response.Write("<script>alert('Unsuccessful. Customer visit record was not updated');</script>");
                        }
                    }
                    catch (ApplicationException err)
                    {
                        Response.Write("<script>alert('Our apologies. An error has occured. Unable to update visit record.')</script>");
                        //add error to the error log and then display response tab to say that an error has occured
                        function.logAnError(err.ToString());
                    }
                    


                    /*hide other placeholders headings and make the appropriate placeholder heading visible
                     *will show user the customer visit content
                     *
                    phBookingDetails.Visible = false;
                    phServiceDetails.Visible = false;
                    phConfirmVisit.Visible = true;
                    lblBookingDetailsHeading.Visible = false;
                    lblServiceHeading.Visible = false;
                    lblConfirmUpdateHeading.Visible = true;*/
                };
                newCell.Controls.Add(sbtnUpdate);
                //add the cell to the row
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);
            }
            catch (ApplicationException Err)
            {
                //log error, display error message,redirect to the stylist page
                function.logAnError(Err.ToString());
                Response.Write("<script>alert('An error has occured.');window.location='Stylist.aspx';</script>");
            }
        }
        
       /* public void DisplayConfirmVisit(string customerID,string bookingID)
        {
            /*Method displays the customer visit to the user and the user 
                  updates the customer visit record and the receptionist can then generate the 
                  invoice
            

            try
            {
                    viewCustomerVisit = handler.BLL_ViewCustVisit(customerID, bookingID);

                    //create a variablew to track the row count
                    int count = 0;
                    //create a new row in the table and set the height
                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    //add the row to the table
                    confirmVisitTable.Rows.Add(newRow);
                    //create a new cell in that row and set the width
                    TableCell newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "CustomerID:";
                    newCell.Width = 300;
                    //add the cell to the tablerow in the table
                    confirmVisitTable.Rows[count].Cells.Add(newCell);
                    //add a new cell that will display the data from the database
                    newCell = new TableCell();
                    newCell.Text = viewCustomerVisit.CustomerID.ToString();
                    newCell.Width = 700;
                    //add the cell to the row in the table
                    confirmVisitTable.Rows[count].Cells.Add(newCell);

                    //increment row count 
                    count++;


                    newRow = new TableRow();
                    newRow.Height = 50;
                    confirmVisitTable.Rows.Add(newRow);
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Date :";
                    newCell.Width = 300;
                    confirmVisitTable.Rows[count].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = viewCustomerVisit.Date.ToString();
                    newCell.Width = 700;
                    confirmVisitTable.Rows[count].Cells.Add(newCell);
                    count++;


                    newRow = new TableRow();
                    newRow.Height = 50;
                    confirmVisitTable.Rows.Add(newRow);
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "BookingID :";
                    newCell.Width = 300;
                    confirmVisitTable.Rows[count].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = viewCustomerVisit.BookingID.ToString();
                    newCell.Width = 700;
                    confirmVisitTable.Rows[count].Cells.Add(newCell);
                    count++;

                    newRow = new TableRow();
                    newRow.Height = 50;
                    confirmVisitTable.Rows.Add(newRow);
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Description :";
                    newCell.Width = 300;
                    confirmVisitTable.Rows[count].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = viewCustomerVisit.Description.ToString();
                    newCell.Width = 700;
                    confirmVisitTable.Rows[count].Cells.Add(newCell);
                    count++;

                    newRow = new TableRow();
                    newRow.Height = 250;
                    confirmVisitTable.Rows.Add(newRow);
                    newCell = new TableCell();
                    newCell.Width = 300;
                    Button buttonBack = new Button();
                    buttonBack.Text = "Back";
                    buttonBack.CssClass = "btn btn-outline-dark";
                    buttonBack.Click += (ss, ee) =>
                    {
                        //hide other placeholders headings and make the appropriate placeholder heading visible
                        //will take user back to service details 
                        phBookingDetails.Visible = false;
                        phServiceDetails.Visible = true;
                        phConfirmVisit.Visible = false;
                        lblBookingDetailsHeading.Visible = false;
                        lblServiceHeading.Visible = true;
                        lblConfirmUpdateHeading.Visible = false;
                    };
                    newCell.Controls.Add(buttonBack);
                    confirmVisitTable.Rows[count].Cells.Add(newCell);



                    newCell = new TableCell();
                    newCell.Width = 300;
                    Button buttonNext = new Button();
                    buttonNext.Text = "Confirm";
                    buttonNext.CssClass = "btn btn-outline-dark";
                    buttonNext.Click += (ss, ee) =>
                    {
                        Response.Redirect("~/Stylist.aspx");
                    };
                    newCell.Controls.Add(buttonNext);
                    confirmVisitTable.Rows[count].Cells.Add(newCell);
                    //rowCount++;


                
            }
            catch (ApplicationException err)
            {
                //log error, display error message,redirect to the stylist page
                function.logAnError(err.ToString());
                Response.Write("<script>alert('An error has occured while communicating with the database.');window.location='Stylist.aspx';</script>");
            }
        }*/
                
        
    }
}