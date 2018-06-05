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
        String test = DateTime.Now.ToString("dddd d MMMM");
        List<SP_GetEmpAgenda> agenda = null;
        CUST_VISIT cust_visit;
        
        string sample = "118233419479102946333"; //Temporary..used just for testing to see if code functions properly

        protected void Page_Load(object sender, EventArgs e)
		{
            theDate.InnerHtml = test;
            getAgenda(sample);
        }

        public void getAgenda(string id)
        {
            Button btn;

            try
            {
                agenda = handler.BLL_GetEmpAgenda(id);

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
                startTime.Width = 300;
                AgendaTable.Rows[0].Cells.Add(startTime);

                TableCell endTime = new TableCell();
                endTime.Text = "End Time";
                endTime.Width = 300;
                AgendaTable.Rows[0].Cells.Add(endTime);

                TableCell cust = new TableCell();
                cust.Text = "Customer Name";
                cust.Width = 300;
                AgendaTable.Rows[0].Cells.Add(cust);

                TableCell emp = new TableCell();
                emp.Text = "Employee Name";
                emp.Width = 300;
                AgendaTable.Rows[0].Cells.Add(emp);

                TableCell service = new TableCell();
                service.Text = "Service";
                service.Width = 300;
                AgendaTable.Rows[0].Cells.Add(service);

                TableCell arrived = new TableCell();
                arrived.Text = "Arrived";
                arrived.Width = 300;
                AgendaTable.Rows[0].Cells.Add(arrived);

                //integer that will be appended in the foreach loop to access the new row for every iteration of the foreach
                int i = 1;
                foreach (SP_GetEmpAgenda a in agenda)
                {

                    //create row 
                    TableRow r = new TableRow();
                    AgendaTable.Rows.Add(r);

                    //create cell for the start time and add to row (cell : 0)
                    TableCell start = new TableCell();
                    start.Text = a.StartTime.ToString();
                    AgendaTable.Rows[i].Cells.Add(start);

                    //create cell for the end time and add to row (cell : 1)
                    TableCell end = new TableCell();
                    end.Text = a.EndTime.ToString();
                    AgendaTable.Rows[i].Cells.Add(end);

                    //create cell for the customer first name and add to row (cell : 2)
                    TableCell c = new TableCell();
                    c.Text = a.CustomerFName.ToString();
                    AgendaTable.Rows[i].Cells.Add(c);

                    //create cell for the stylists name and add to row (cell : 3)
                    TableCell e = new TableCell();
                    e.Text = a.EmpFName.ToString();
                    AgendaTable.Rows[i].Cells.Add(e);

                    //create cell for the service name and add to row (cell : 4)
                    TableCell s = new TableCell();
                    s.Text = a.ServiceName.ToString();
                    AgendaTable.Rows[i].Cells.Add(s);

                    //create cell for the arrived status and add to row (cell : 5)
                    TableCell present = new TableCell();
                    present.Text = a.Arrived.ToString();
                    AgendaTable.Rows[i].Cells.Add(present);

                    //create cell which the button populates and add to row (cell : 6)
                    TableCell buttonCell = new TableCell();
                    buttonCell.Width = 200;
                    buttonCell.Height = 50;
                    //create button 
                    btn = new Button();
                    btn.Text = "Visit Record";
                    btn.CssClass = "btn btn-outline-dark";
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

                            //add customerID parameter from booking/bookingAgenda
                            cust_visit.CustomerID = Convert.ToString(a.UserID);
                            //add bookingID parameter from booking/bookingAgenda
                            cust_visit.BookingID = Convert.ToString(a.BookingID);
                            //this would be the "ServiceName" from the booking/bookingAgenda
                            cust_visit.Description = Convert.ToString(a.ServiceName);

                            if (handler.BLL_CreateCustVisit(cust_visit))
                            {
                                /*
                                 * alert user that customer visit record has been created
                                 * after the user clicks okay on the alert window, the page redirects 
                                 *  to the customer visit process.
                                 *
                                 */

                                Response.Write("<script>alert('Customer visit record for the visit has been created.You will now be taken to customer visit process.');"
                                    + "window.location = 'CustomerVisit.aspx?bookingID=" + cust_visit.BookingID.ToString()
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
                                Response.Write("<script>alert('Unsuccessful. Customer visit record not created');</script>");
                            }
                        }
                        catch(ApplicationException err)
                        {
                            //Response.Write("<script>alert('Error: '" + err.ToString()+ "\")</script>");
                            Response.Write("<script>alert('Our apologies. An error has occured. Please report to the administrator or try again later.')</script>");
                            //add error to the error log and then display response tab to say that an error has occured
                            function.logAnError(err.ToString());
                        }

                    };
                    //add button control to the cell
                    buttonCell.Controls.Add(btn);
                    //add the cell to the row
                    AgendaTable.Rows[i].Cells.Add(buttonCell);
                    
                    //increment i 
                    i++;
                }
            }
            catch (ApplicationException E)
            {
                //log error, display error message,redirect to the error which then takes user to the home page if they would like to
                function.logAnError(E.ToString());
                Response.Write("<script>alert('An erro has occured.Unable to display required data.');</script>");
                Server.Transfer("~/Error.aspx");
            }
        }

    }
}