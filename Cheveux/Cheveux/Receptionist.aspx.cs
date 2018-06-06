using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;


namespace Cheveux
{
    public partial class Receptionist : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        String test = DateTime.Now.ToString("dddd d MMMM");
        List<SP_GetEmpNames> list = null;
        List<SP_GetEmpAgenda> agenda = null;
        BOOKING checkIn = null;

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
                        //allowed access to this page
                        //Response.Redirect("Receptionist.aspx");
                    }
                    else if (userType == "M")
                    {
                        //Manager
                        Response.Redirect("Manager.aspx");
                    }
                    else if (userType == "S")
                    {
                        //stylist
                        Response.Redirect("Stylist.aspx");
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
            list = handler.BLL_GetEmpNames();
            if (!Page.IsPostBack)
            {
                try
                {
                    foreach (SP_GetEmpNames emps in list)
                    {
                        //Load employee names into dropdownlist
                        drpEmpNames.DataSource = list;
                        //set the coloumn that will be displayed to the user
                        drpEmpNames.DataTextField = "Name";
                        //set the coloumn that will be used for the valuefield
                        drpEmpNames.DataValueField = "EmployeeID";
                        //bind the data
                        drpEmpNames.DataBind();
                        /*set the default (text (dropdownlist index[0]) that will first be displayed to the user.
                         * in this case the "instruction" to select the employee
                        */
                        drpEmpNames.Items.Insert(0, new ListItem("--Select Employee--", "-1"));
                    }
                }
                catch (ApplicationException Err)
                {
                    Response.Write("<script>alert('Could load data into dropdownlist.Problem communicating with the database.');</script>");
                    function.logAnError(Err.ToString());
                }

            }

            try
            {
                /*if the selected valus is not the "select employee" display the employee names
                 * if the selected value is the "select employee" nothing will be displayed or added to the table
                 * getAgenda method will not run
                 */ 
                if (drpEmpNames.SelectedValue != "-1")
                {
                    getAgenda(drpEmpNames.SelectedValue);
                }
            }
            catch (ApplicationException Err)
            {
                Response.Write("<script>alert('Could load data into the table.Problem communicating with the database.');</script>");
                function.logAnError(Err.ToString());
            }
        }
        public void getAgenda(string id)
        {
            Button btn;

            Button checkOut;
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

                //integer that will be incremented in the foreach loop to access the new row for every iteration of the foreach
                int i = 1;
                foreach(SP_GetEmpAgenda a in agenda)
                {
                    
                    //created cell for the record
                    TableRow r = new TableRow();
                    //add row to table
                    AgendaTable.Rows.Add(r);

                    //create start cell and add to row.. cell index: 0
                    TableCell start = new TableCell();
                    start.Text = a.StartTime.ToString();
                    AgendaTable.Rows[i].Cells.Add(start);

                    //create end cell and add to row.. cell index: 1
                    TableCell end = new TableCell();
                    end.Text = a.EndTime.ToString();
                    AgendaTable.Rows[i].Cells.Add(end);

                    //created customer name cell and add to row.. cell index: 2
                    TableCell c = new TableCell();
                    c.Text = a.CustomerFName.ToString();
                    AgendaTable.Rows[i].Cells.Add(c);

                    //create employee name cell and add to row.. cell index: 3
                    TableCell e = new TableCell();
                    e.Text = a.EmpFName.ToString();
                    AgendaTable.Rows[i].Cells.Add(e);

                    //create service name cell and add to row.. cell index: 4
                    TableCell s = new TableCell();
                    s.Text = a.ServiceName.ToString();
                    AgendaTable.Rows[i].Cells.Add(s);

                    //create arrival status cell and add to row.. cell index : 5
                    TableCell present = new TableCell();
                    present.Text = a.Arrived.ToString();
                    AgendaTable.Rows[i].Cells.Add(present);

                    //create cell that will be populated by the button and add to row.. cell index: 6
                    TableCell buttonCell = new TableCell();
                    buttonCell.Width = 200;
                    buttonCell.Height = 50;
                    
                    //create button
                    btn = new Button();
                    btn.Text = "Check-in";
                    btn.CssClass = "btn btn-outline-dark";
                    btn.Click += (ss, ee) => {
                        /*
                         *Check-in code here 
                         * After clicking the button arrived should change to Y
                         * and the button text should change to Check-out
                         * and code should cater for the change as the stored procedure to check out and generate invoice
                         * needs to be called
                         */
                        try
                        {
                            checkIn = new BOOKING();

                            checkIn.BookingID = a.BookingID.ToString();
                            checkIn.StylistID = drpEmpNames.SelectedValue.ToString();

                            if (handler.BLL_CheckIn(checkIn))
                            {
                                //if BLL_CheckIn successful and arrival status changed show user and refresh the page
                                Response.Write("<script>alert('Customer arrival status has been updated.');location.reload();</script>");
                            }
                            else
                            {
                                //if BLL_CheckIn unsuccessful and arrival status was not changed tell the user to try again or report to admin
                                Response.Write("<script>alert('Unsuccessful.Status was not changed.If problem persists report to admin.');</script>");
                            }

                        }
                        catch(ApplicationException err)
                        {
                            //Error handling
                            Response.Write("<script>alert('Our apologies. An error has occured. Please report to the administrator or try again later.')</script>");
                            //add error to the error log and then display response tab to say that an error has occured
                            function.logAnError(err.ToString());
                        }
                            

                    };
                    //add button to cell 
                    buttonCell.Controls.Add(btn);
                    //add cell to row
                    AgendaTable.Rows[i].Cells.Add(buttonCell);

                    //increment control variable
                    i++;


                    //create cell that will be populated by the button and add to row.. cell index: 6
                    TableCell checkCell = new TableCell();
                    checkCell.Width = 200;
                    checkCell.Height = 50;

                }
            }
            catch(ApplicationException E)
            {
                Response.Write("<script>alert('Trouble communicating with the database.Report to admin and try again later.');location.reload();</script>");
                function.logAnError(E.ToString());
            }
        }
    }
}
