using System;
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

        //bookingID is going to go in here
        string bookingID;
        //customerID is going to go in here
        string customerID;

        CUST_VISIT visit;
        BOOKING b;
        protected void Page_Load(object sender, EventArgs e)
        {
            errorHeader.Font.Bold = true;
            errorHeader.Font.Underline = true;
            errorHeader.Font.Size = 21;
            errorMessage.Font.Size = 14;

            //access control
            HttpCookie UserID = Request.Cookies["CheveuxUserID"];
            //send the user to the correct page based on their usertype
            if (UserID != null)
            {
                string userType = UserID["UT"].ToString();
                if ("R" == userType)
                {
                    //Receptionist
                    Response.Redirect("../Receptionist/Receptionist.aspx");
                }
                else if (userType == "M")
                {
                    //Manager
                    //allowed access to this page
                    Response.Redirect("../Manager/Manager.aspx");
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
                    Response.Redirect("../Default.aspx");

                }
                else
                {
                    function.logAnError("Unknown user type found during Loading of default.aspx: " +
                        UserID["UT"].ToString());
                    Response.Redirect("../Default.aspx");
                }
            }
            else
            {
                //ask the user to login first 

                //temp fix redirect to home page
                Response.Redirect("../Default.aspx");
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
            //hide other placeholders headings and make the appropriate placeholder heading visible
            phServiceDetails.Visible = false;
            phBookingDetails.Visible = true;
            lblBookingDetailsHeading.Visible = true;
            lblServiceHeading.Visible = false;
            try
            {

                bDTL = handler.BLL_GetAllofBookingDTL(bookingID.Replace(" ", string.Empty), customerID);

                //create a variablew to track the row count
                int rCnt = 0;

                //create a new row in the table and set the height
                TableRow newRow = new TableRow();
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                TableCell newCell = new TableCell();
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
                newRow.Height = 50;
                allBookingTable.Rows.Add(newRow);
                newCell = new TableCell();
                newCell.Width = 700;
                Button btnUpdateVisit = new Button();
                btnUpdateVisit.Text = "Update Visit";
                btnUpdateVisit.CssClass = "btn btn-primary";
                btnUpdateVisit.Click += (ss, ee) => {
                    //hide other placeholders headings and make the appropriate placeholder heading visible
                    //takes user to content displaying the service details
                    phBookingDetails.Visible = false;
                    phServiceDetails.Visible = true;

                    lblBookingDetailsHeading.Visible = false;
                    lblServiceHeading.Visible = true;

                };
                newCell.Controls.Add(btnUpdateVisit);
                allBookingTable.Rows[rCnt].Cells.Add(newCell);

                TableCell noChangesCell = new TableCell();
                newCell.Width = 700;
                Button btnNoChanges = new Button();
                btnNoChanges.Text = "Complete Appointment";
                btnNoChanges.CssClass = "btn btn-primary";
                btnNoChanges.Click += (ss, ee) => {
                    try
                    {
                        /*If appointment went as scheduled and stylist does not need to update
                         * the customer vist record.
                         */

                        visit = new CUST_VISIT();

                        visit.CustomerID = Convert.ToString(customerID);
                        visit.BookingID = Convert.ToString(bookingID);
                        visit.Description = Convert.ToString(bDTL.ServiceDescription);

                        if (handler.BLL_UpdateCustVisit(visit, b))
                        {
                            Response.Write("<script>alert('Create Customer Visit Record Process Successful.');</script>");
                            Response.Redirect("../Stylist/Stylist.aspx");

                            btnNoChanges.Visible = false;
                            noChangesCell.Text = "<p><i>Appointment Completed</i></p>";
                        }
                        else
                        {
                            /*
                             * if the update fails, display failed message
                             * to alert that the update was not successful
                             * (user friendly action status response)
                             */
                            //Response.Write("<script>alert('Error.Please Try Again.');</script>");
                            phVisitErr.Visible = true;
                            lblVisitErr.Text = "System is unable to create a visit record at this point in time.<br/>"
                                                  + "Please report to management or try again later. Sorry for the inconvenience.";
                        }
                    }
                    catch (Exception err)
                    {
                        //Response.Write("<script>alert('Our apologies. An error has occured. Unable to update visit record.')</script>");
                        //add error to the error log and then display response tab to say that an error has occured 
                        phVisitErr.Visible = true;
                        lblVisitErr.Text = "An error has occured.System is unable to create a visit record at this point in time.<br/>"
                                              + "Please report to management or try again later. Sorry for the inconvenience.";
                        function.logAnError(err.ToString());
                    }
                };
                noChangesCell.Controls.Add(btnNoChanges);
                allBookingTable.Rows[rCnt].Cells.Add(noChangesCell);
                rCnt++;
            }
            catch(Exception Err)
            {
                phServiceDetails.Visible = false;
                phBookingDetails.Visible = false;
                lblBookingDetailsHeading.Visible = false;
                lblServiceHeading.Visible = false;

                phBookingsErr.Visible = true;
                errorHeader.Text = "Error.Cannot display booking details.";
                errorMessage.Text = "It seems there is a problem communicating with the database."
                                    + "Please report problem to admin or try again later.";
                //log error, display error message,redirect to the stylist page
                function.logAnError(Err.ToString());
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

                //create a new row and add it to the table
                newRow = new TableRow();
                newRow.Height = 50;
                serviceDetailsTable.Rows.Add(newRow);
                
                //create a cell for the service name and add it to the table
                TableCell newCell = new TableCell();
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
                newCell.Text = "Booking comment:";
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
                sbtnBack.CssClass = "btn btn-primary";
                sbtnBack.Click += (ss, ee) => {
                    //hide other placeholders headings and make the appropriate placeholder heading visible
                    //will go back to bookings details content
                    phBookingDetails.Visible = true;
                    phServiceDetails.Visible = false;
                    lblBookingDetailsHeading.Visible = true;
                    lblServiceHeading.Visible = false;
                };
                newCell.Controls.Add(sbtnBack);
                //add cell to the row
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);

                //create a cell for the update button and add the button control to the cell 
                newCell = new TableCell();
                newCell.Width = 700;
                Button sbtnUpdate = new Button();
                sbtnUpdate.Text = "Update Visit";
                sbtnUpdate.CssClass = "btn btn-primary";
                sbtnUpdate.Click += (ss, ee) => {

                    /* What this button does:
                     * =====================
                     * Updates customer visit record (service description column)
                     */
                    try
                    {
                        visit = new CUST_VISIT();

                        visit.CustomerID = Convert.ToString(customerID);
                        visit.BookingID = Convert.ToString(bookingID);
                        visit.Description = Convert.ToString(descBox.Text);

                        b = new BOOKING();
                        b.Comment = Convert.ToString(descBox.Text);

                        if (handler.BLL_UpdateCustVisit(visit,b))
                        {
                            Response.Write("<script>alert('Update Successful.');</script>");
                            Response.Redirect("../Stylist/Stylist.aspx");
                        }
                        else
                        {
                            /*
                             * if the update fails, display failed message
                             * to alert that the update was not successful
                             * (user friendly action status response)
                             */
                            //Response.Write("<script>alert('Unsuccessful. Customer visit record was not updated');</script>");
                            phVisitErr.Visible = true;
                            lblVisitErr.Text = "System is unable to create a visit record at this point in time.<br/>"
                                                  + "Please report to management or try again later. Sorry for the inconvenience.";
                        }
                    }
                    catch (Exception err)
                    {
                        //Response.Write("<script>alert('Our apologies. An error has occured. Unable to update visit record.')</script>");
                        phVisitErr.Visible = true;
                        lblVisitErr.Text = "An error has occured.System is unable to create a visit record at this point in time.<br/>"
                                              + "Please report to management or try again later. Sorry for the inconvenience.";
                        //add error to the error log and then display response tab to say that an error has occured
                        function.logAnError(err.ToString());
                    }
                    
                };
                newCell.Controls.Add(sbtnUpdate);
                //add the cell to the row
                serviceDetailsTable.Rows[rowCount].Cells.Add(newCell);
            }
            catch (Exception Err)
            {
                phServiceDetails.Visible = false;
                phBookingDetails.Visible = false;
                lblBookingDetailsHeading.Visible = false;
                lblServiceHeading.Visible = false;

                phBookingsErr.Visible = true;
                errorHeader.Text = "Error.Cannot display services details.";
                errorMessage.Text = "It seems there is a problem communicating with the database."
                                    + "Please report problem to admin or try again later.";
                function.logAnError(Err.ToString());
                
            }
        }
    }
}