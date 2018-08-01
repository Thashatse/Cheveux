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
	public partial class Stylist : System.Web.UI.Page
	{
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        String dayDate = DateTime.Now.ToString("dddd d MMMM");
        //List<SP_GetMyNextCustomer> next = null;
        CUST_VISIT cust_visit;
        HttpCookie cookie = null;

        List<SP_GetEmpAgenda> agenda = null;

        String bookingDate = DateTime.Now.ToString("yyyy-MM-dd");
        protected void Page_Load(object sender, EventArgs e)
		{
            errorHeader.Font.Bold = true;
            errorHeader.Font.Underline = true;
            errorHeader.Font.Size = 21;
            errorMessage.Font.Size = 14;
            errorToReport.Font.Size = 10;

            //access control
            HttpCookie UserID = Request.Cookies["CheveuxUserID"];

            if(UserID == null)
            {
                LoggedOut.Visible = true;
                AgendaTable.Visible = false;
                LoggedIn.Visible = false;
            }
            else if(UserID["UT"] != "S")
            {
                Response.Redirect("../Default.aspx");
            }
            else if(UserID["UT"] == "S")
            {
                LoggedOut.Visible =false;
                LoggedIn.Visible = true;

                lblDate.Text = dayDate;
                string wB = Request.QueryString["WB"];
                if (wB == "True")
                {
                    lblWelcome.Text = "Welcome Back " + handler.GetUserDetails(UserID["ID"]).FirstName;
                }

                cookie = Request.Cookies["CheveuxUserID"];
                getAgenda(cookie["ID"].ToString(), DateTime.Parse(bookingDate));

                //if theres no booking for the day dont display the tables headings
                if(AgendaTable.Rows.Count == 1)
                {
                    AgendaTable.Visible = false;
                    noBookingsPH.Visible = true;
                    lblNoBookings.Text = "You have no bookings for the day.<br/> Refresh page for update";
                }
                else
                {
                    //if there are bookings for the day display headings 
                    AgendaTable.Visible = true;
                }
            }            
        }

        public void getAgenda(string id, DateTime bookingDate)
        {
            Button btn;

            try
            {
                agenda = handler.BLL_GetEmpAgenda(id, bookingDate);

                AgendaTable.CssClass = "table table-light table-hover";

                //create row for the table 
                TableRow row = new TableRow();
                row.Height = 50;

                //add row to the table
                AgendaTable.Rows.Add(row);

                /* 
                 * create the cells for the row
                 * and their names
                 * the cells being created are for the first row of the table
                 * and their names are the column names
                 * Each cell is added to the table row
                 * .Rows[0] => refers to the first row of the table
                 * */
                TableCell startTime = new TableCell();
                startTime.Text = "Start Time";
                startTime.Font.Bold = true;
                startTime.Width = 400;
                AgendaTable.Rows[0].Cells.Add(startTime);

                TableCell endTime = new TableCell();
                endTime.Text = "End Time";
                endTime.Font.Bold = true;
                endTime.Width = 400;
                AgendaTable.Rows[0].Cells.Add(endTime);

                TableCell cust = new TableCell();
                cust.Text = "Customer";
                cust.Font.Bold = true;
                cust.Width = 400;
                AgendaTable.Rows[0].Cells.Add(cust);

                TableCell emp = new TableCell();
                emp.Text = "Employee";
                emp.Font.Bold = true;
                emp.Width = 400;
                AgendaTable.Rows[0].Cells.Add(emp);

                TableCell service = new TableCell();
                service.Text = "Service";
                service.Font.Bold = true;
                service.Width = 450;
                AgendaTable.Rows[0].Cells.Add(service);

                TableCell arrived = new TableCell();
                arrived.Text = "Arrived";
                arrived.Font.Bold = true;
                arrived.Width = 200;
                AgendaTable.Rows[0].Cells.Add(arrived);

                //integer that will be appended in the foreach loop to access the new row for every iteration of the foreach
                int i = 1;
                foreach (SP_GetEmpAgenda n in agenda)
                {

                    //create row 
                    TableRow r = new TableRow();
                    AgendaTable.Rows.Add(r);

                    //create cell for the start time and add to row (cell : 0)
                    TableCell start = new TableCell();
                    start.Text = n.StartTime.ToString();
                    AgendaTable.Rows[i].Cells.Add(start);

                    //create cell for the end time and add to row (cell : 1)
                    TableCell end = new TableCell();
                    end.Text = n.EndTime.ToString();
                    AgendaTable.Rows[i].Cells.Add(end);

                    //create cell for the customer first name and add to row (cell : 2)
                    TableCell c = new TableCell();
                    c.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + n.UserID.ToString().Replace(" ", string.Empty) +
                                    "'>" + n.CustomerFName.ToString() + "</a>";
                    AgendaTable.Rows[i].Cells.Add(c);

                    //create cell for the stylists name and add to row (cell : 3)
                    TableCell e = new TableCell();
                    e.Text = "<a href = '../Profile.aspx?Action=View" +
                                        "&empID=" + n.empID.ToString().Replace(" ", string.Empty) +
                                        "'>" + n.EmpFName.ToString() + "</a>";
                    AgendaTable.Rows[i].Cells.Add(e);

                    //create cell for the service name and add to row (cell : 4)
                    TableCell s = new TableCell();
                    s.Text = "<a href = 'ViewProduct.aspx?ProductID=" + n.ProductID.ToString().Replace(" ", string.Empty) +
                                    "'>" + n.ServiceName.ToString() + "</a>";
                    AgendaTable.Rows[i].Cells.Add(s);

                    //create cell for the arrived status and add to row (cell : 5)
                    TableCell present = new TableCell();
                    present.Text = function.GetFullArrivedStatus(n.Arrived.ToString()[0]);
                    AgendaTable.Rows[i].Cells.Add(present);

                        //create cell which the button populates and add to row (cell : 6)
                        TableCell buttonCell = new TableCell();
                        buttonCell.Width = 200;
                        buttonCell.Height = 50;
                    if(function.GetFullArrivedStatus(n.Arrived.ToString()[0]) == "Yes")
                    {
                        //create button 
                        btn = new Button();
                        btn.Text = "Create Visit Record";
                        btn.CssClass = "btn btn-primary";
                        //button's click event
                        btn.Click += (ss, ee) => {
                            try
                            {
                                /* What does the button do:
                                 * =======================
                                 * button creates customer visit record in the CUST_VISIT table 
                                 *and redirects user to the customer visit page of the booking
                                 * 
                                 */

                                cust_visit = new CUST_VISIT();

                                cust_visit.CustomerID = Convert.ToString(n.UserID);
                                cust_visit.Date = Convert.ToDateTime(n.Date);
                                cust_visit.BookingID = Convert.ToString(n.BookingID);
                                cust_visit.Description = Convert.ToString(n.ServiceName);

                                if (handler.BLL_CreateCustVisit(cust_visit))
                                {
                                    /*
                                     * alert user that customer visit record has been created
                                     * after the user clicks okay on the alert window, the page redirects 
                                     *  to the customer visit process.
                                     *
                                     */

                                    Response.Write("<script>alert('Customer visit record for the visit has been created.You will now be taken to customer visit process.');"
                                        + "window.location = '../Stylist/CustomerVisit.aspx?bookingID=" + cust_visit.BookingID.ToString()
                                        + "&customerID=" + cust_visit.CustomerID.ToString()
                                        + "';</script>");
                                }
                                else
                                {
                                    /*
                                     * if the insert fails, display failed message
                                     *to alert that the insert was not successful
                                     * and that the customer visit record was not created
                                     * (user friendly action status response)
                                     */
                                    //Response.Write("<script>alert('Unsuccessful. Customer visit record not created');</script>");
                                    phVisitErr.Visible = true;
                                    lblVisitErr.Text = "An error has occured.System is unable to create a visit record for the customer at this point in time.<br/>"
                                                          + "Please report to management. Sorry for the inconvenience."+
                                                          "<br/>Possible Error: Visit Record already exists. System does not allow double visit records.";
                                }
                            }
                            catch (ApplicationException err)
                            {
                                //Response.Write("<script>alert('Our apologies. An error has occured. Please report to the administrator or try again later.')</script>");
                                phVisitErr.Visible = true;
                                lblVisitErr.Text = "An error has occured.<br/>"
                                                      + "Please report to management. Sorry for the inconvenience." +
                                                      "<br/>Error: " + err.ToString();
                                //add error to the error log and then display response tab to say that an error has occured
                                function.logAnError(err.ToString());
                            }

                        };
                        //add button control to the cell
                        buttonCell.Controls.Add(btn);
                        //add the cell to the row
                        AgendaTable.Rows[i].Cells.Add(buttonCell);
                    }
                    else if(function.GetFullArrivedStatus(n.Arrived.ToString()[0]) == "No")
                    {
                        buttonCell.Text = "<p><i>Customer has not arrived</i></p>";
                        AgendaTable.Rows[i].Cells.Add(buttonCell);
                    }

                    //increment i 
                    i++;
                }
            }
            catch (ApplicationException E)
            {
                //Response.Write("<script>alert('An error has occured.Having trouble connecting to the database. Unable to display required data.');</script>");
                //Response.Redirect(Request.RawUrl);

                phBookingsErr.Visible = true;
                errorHeader.Text = "Oh no!";
                errorMessage.Text = "It seems there is a problem communicating with the database."
                                    + "Please report problem to admin or try again later.";
                errorToReport.Text = "Error To report:" + E.ToString();

                //log error, display error message,redirect to the error which then takes user to the home page if they would like to
                function.logAnError(E.ToString());
                
            }
        }

    }
}