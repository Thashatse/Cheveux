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

        //temporary...used just for testing to see if code functions properly
        string bookingID ;

        //temporary...used just for testing to see if code functions properly
        string customerID;


        protected void Page_Load(object sender, EventArgs e)
        {
            theDate.InnerHtml = test;
            
            bookingID = Request.QueryString["bookingID"];
            customerID = Request.QueryString["customerID"];
            if (bookingID != null && customerID != null)
            {
                DisplayBookingDetails(bookingID, customerID);
                DisplayServiceDetails(bookingID, customerID);
                DisplayConfirmVisit();
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

                bDTL = handler.BLL_GetAllofBookingDTL(bookingID, customerID);

                //create a variablew to track the row count
                int rowCount = 0;
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
                allBookingTable.Rows[rowCount].Cells.Add(newCell);
                //add a new cell that will display the data from the database
                newCell = new TableCell();
                newCell.Text = bDTL.BookingID.ToString();
                newCell.Width = 700;
                //add the cell to the row in the table
                allBookingTable.Rows[rowCount].Cells.Add(newCell);

                //increment row count 
                rowCount++;


                newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "CustomerID:";
                newCell.Width = 300;
                allBookingTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.CustomerID.ToString();
                newCell.Width = 700;
                allBookingTable.Rows[rowCount].Cells.Add(newCell);
                rowCount++;


                newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Customer Name:";
                newCell.Width = 300;
                allBookingTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.CustomerName.ToString();
                newCell.Width = 700;
                allBookingTable.Rows[rowCount].Cells.Add(newCell);
                rowCount++;


                newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Service Name:";
                newCell.Width = 300;
                allBookingTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.ServiceName.ToString();
                newCell.Width = 700;
                allBookingTable.Rows[rowCount].Cells.Add(newCell);
                rowCount++;


                newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Service Description:";
                newCell.Width = 300;
                allBookingTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.ServiceDescription.ToString();
                newCell.Width = 700;
                allBookingTable.Rows[rowCount].Cells.Add(newCell);
                rowCount++;


                newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Price:";
                newCell.Width = 300;
                allBookingTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.Price.ToString();
                newCell.Width = 700;
                allBookingTable.Rows[rowCount].Cells.Add(newCell);
                rowCount++;


                newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Date:";
                newCell.Width = 300;
                allBookingTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.Date.ToString();
                newCell.Width = 700;
                allBookingTable.Rows[rowCount].Cells.Add(newCell);
                rowCount++;


                newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Start Time - End Time:";
                newCell.Width = 300;
                allBookingTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.StartTime.ToString() + " - " + bDTL.EndTime.ToString();
                newCell.Width = 700;
                allBookingTable.Rows[rowCount].Cells.Add(newCell);
                rowCount++;


                newRow = new TableRow();
                newRow.Height = 250;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Width = 300;
                Button btnCreateVisit = new Button();
                btnCreateVisit.Text = "Create Visit";
                btnCreateVisit.CssClass = "btn btn-outline-dark";
                btnCreateVisit.Click += (ss, ee) => {
                    //hide other placeholders headings and make the appropriate placeholder heading visible
                    phBookingDetails.Visible = false;
                    phServiceDetails.Visible = false;
                    phConfirmVisit.Visible = true;
                    lblBookingDetailsHeading.Visible = false;
                    lblServiceHeading.Visible = false;
                    lblConfirmUpdateHeading.Visible = true;
                };
                newCell.Controls.Add(btnCreateVisit);
                allBookingTable.Rows[rowCount].Cells.Add(newCell);


                newCell = new TableCell();
                newCell.Width = 700;
                Button btnUpdateVisit = new Button();
                btnUpdateVisit.Text = "Update";
                btnUpdateVisit.CssClass = "btn btn-outline-dark";
                btnUpdateVisit.Click += (ss, ee) => {
                    //hide other placeholders headings and make the appropriate placeholder heading visible
                    //takes user to content displaying the service details
                    phBookingDetails.Visible = false;
                    phServiceDetails.Visible = true;
                    phConfirmVisit.Visible = false;
                    lblBookingDetailsHeading.Visible = false;
                    lblServiceHeading.Visible = true;
                    lblConfirmUpdateHeading.Visible = false;
                };
                newCell.Controls.Add(btnUpdateVisit);
                allBookingTable.Rows[rowCount].Cells.Add(newCell);
                rowCount++;

                //hide other placeholders headings and make the appropriate placeholder heading visible
                phServiceDetails.Visible = false;
                phConfirmVisit.Visible = false;
                lblBookingDetailsHeading.Visible = true;
                lblServiceHeading.Visible = false;
                lblConfirmUpdateHeading.Visible = false;
            }
            catch(ApplicationException Err)
            {
                function.logAnError(Err.ToString());
                Server.Transfer("~/Error.aspx");
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
                TableCell newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "BookingID:";
                newCell.Width = 300;
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.BookingID.ToString();
                newCell.Width = 700;
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Width = 700;
                Button btnEdit = new Button();
                btnEdit.Text = "Edit Details";
                btnEdit.CssClass = "btn";
                btnEdit.Click += (ss, ee) => {
                    /*
                     *Click button and and a textbox is created
                     * User can then add onto the service name
                     * 
                     */
                };
                newCell.Controls.Add(btnEdit);
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);
                //increment row count 
                rowCount++;


                newRow = new TableRow();
                newRow.Height = 50;
                serviceDetailsTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Service Name:";
                newCell.Width = 300;
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.ServiceName.ToString();
                newCell.Width = 700;
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);
                rowCount++;


                newRow = new TableRow();
                newRow.Height = 50;
                serviceDetailsTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Service Description";
                newCell.Width = 300;
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = bDTL.ServiceDescription.ToString();
                newCell.Width = 700;
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);
                rowCount++;


                newRow = new TableRow();
                newRow.Height = 250; 
                serviceDetailsTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Width = 300;
                Button btnBack = new Button();
                btnBack.Text = "Back";
                btnBack.CssClass = "btn btn-outline-dark";
                btnBack.Click += (ss, ee) => {
                    //hide other placeholders headings and make the appropriate placeholder heading visible
                    //will go back to bookings details content
                    phBookingDetails.Visible = true;
                    phServiceDetails.Visible = false;
                    phConfirmVisit.Visible = false;
                    lblBookingDetailsHeading.Visible = true;
                    lblServiceHeading.Visible = false;
                    lblConfirmUpdateHeading.Visible = false;
                };
                newCell.Controls.Add(btnBack);
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);


                newCell = new TableCell();
                newCell.Width = 700;
                Button btnUpdate = new Button();
                btnUpdate.Text = "Update";
                btnUpdate.CssClass = "btn btn-outline-dark";
                btnUpdate.Click += (ss, ee) => {


                    /*
                     *User would click on button 
                     *Button would call update procedure
                     * 
                     * Update the necessary data    (Create the update stored procedure)
                     * 
                     * And then reload the page 
                     * And show the updated version of the data
                     * 
                     * code still to be added 
                     * 
                     */

                    //hide other placeholders headings and make the appropriate placeholder heading visible
                    phBookingDetails.Visible = false;
                    phServiceDetails.Visible = true;
                    phConfirmVisit.Visible = false;
                    lblBookingDetailsHeading.Visible = false;
                    lblServiceHeading.Visible = true;
                    lblConfirmUpdateHeading.Visible = false;
                };
                newCell.Controls.Add(btnUpdate);
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);


                newCell = new TableCell();
                newCell.Width = 700;
                Button btnNext = new Button();
                btnNext.Text = "Next";
                btnNext.CssClass = "btn btn-outline-dark";
                btnNext.Click += (ss, ee) => {
                    //hide other placeholders headings and make the appropriate placeholder heading visible
                    //will show user the customer visit content
                    phBookingDetails.Visible = false;
                    phServiceDetails.Visible = false;
                    phConfirmVisit.Visible = true;
                    lblBookingDetailsHeading.Visible = false;
                    lblServiceHeading.Visible = false;
                    lblConfirmUpdateHeading.Visible = true;
                };
                newCell.Controls.Add(btnNext);
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);
                rowCount++;

            }
            catch (ApplicationException Err)
            {
                function.logAnError(Err.ToString());
                Server.Transfer("~/Error.aspx");
            }
        }
        
        public void DisplayConfirmVisit()
        {
            try
            {
                TableRow newRow = new TableRow();
                newRow.Height = 250;
                confirmVisitTable.Rows.Add(newRow);
                TableCell newCell = new TableCell();
                newCell.Width = 300;
                Button btnBack = new Button();
                btnBack.Text = "Back";
                btnBack.CssClass = "btn btn-outline-dark";
                btnBack.Click += (ss, ee) => {
                    //hide other placeholders headings and make the appropriate placeholder heading visible
                    //will take user back to service details 
                    phBookingDetails.Visible = false;
                    phServiceDetails.Visible = true;
                    phConfirmVisit.Visible = false;
                    lblBookingDetailsHeading.Visible = false;
                    lblServiceHeading.Visible = true;
                    lblConfirmUpdateHeading.Visible = false;
                };
                newCell.Controls.Add(btnBack);
                confirmVisitTable.Rows[0].Cells.Add(newCell);



                newCell = new TableCell();
                newCell.Width = 300;
                Button btnNext= new Button();
                btnNext.Text = "Confirm";
                btnNext.CssClass = "btn btn-outline-dark";
                btnNext.Click += (ss, ee) => {
                    //hide other placeholders headings and make the appropriate placeholder heading visible
                    /*
                     * 
                     * Creates the Customer visit record and takes away the appointment from the stylist agenda
                     * 
                     * Code to still be added
                     * 
                     * 
                     * 
                    phBookingDetails.Visible = false;
                    phServiceDetails.Visible = false;
                    phConfirmVisit.Visible = false;
                    lblBookingDetailsHeading.Visible = false;
                    lblServiceHeading.Visible = false;
                    lblConfirmUpdateHeading.Visible = false;*/
                    Response.Redirect("~/Stylist.aspx");
                };
                newCell.Controls.Add(btnNext);
                confirmVisitTable.Rows[0].Cells.Add(newCell);



            }
            catch(ApplicationException err)
            {
                function.logAnError(err.ToString());
                Server.Transfer("~/Error.aspx");
            }
        }
    }
}