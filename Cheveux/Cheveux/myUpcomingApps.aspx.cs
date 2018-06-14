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
    public partial class todaysSchedule : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        String test = DateTime.Now.ToString("dddd d MMMM");
        List<SP_GetEmpAgenda> today = null;

        HttpCookie cookie = null;
        String bookingDate = DateTime.Now.ToString("yyyy-MM-dd");

        //string sample = "118233419479102946333";
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
            cookie = Request.Cookies["CheveuxUserID"];

            getTodaySchedule(cookie["ID"].ToString(), DateTime.Parse(bookingDate));
        }
        public void getTodaySchedule(string id, DateTime bookingDate)
        {

            try
            {
                today = handler.BLL_GetEmpAgenda(id, bookingDate);

                //create row for the table 
                TableRow row = new TableRow();
                row.Height = 50;

                //add row to the table
                myScheduleToday.Rows.Add(row);

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
                startTime.Font.Bold = true;
                myScheduleToday.Rows[0].Cells.Add(startTime);

                TableCell endTime = new TableCell();
                endTime.Text = "End Time";
                endTime.Width = 300;
                endTime.Font.Bold = true;
                myScheduleToday.Rows[0].Cells.Add(endTime);

                TableCell cust = new TableCell();
                cust.Text = "Customer Name";
                cust.Width = 300;
                cust.Font.Bold = true;
                myScheduleToday.Rows[0].Cells.Add(cust);

                TableCell emp = new TableCell();
                emp.Text = "Employee Name";
                emp.Width = 300;
                emp.Font.Bold = true;
                myScheduleToday.Rows[0].Cells.Add(emp);

                TableCell service = new TableCell();
                service.Text = "Service";
                service.Width = 300;
                service.Font.Bold = true;
                myScheduleToday.Rows[0].Cells.Add(service);

                TableCell arrived = new TableCell();
                arrived.Text = "Arrived";
                arrived.Width = 300;
                arrived.Font.Bold = true;
                myScheduleToday.Rows[0].Cells.Add(arrived);

                int i = 1;
                foreach (SP_GetEmpAgenda a in today)
                {

                    //created cell for the record
                    TableRow r = new TableRow();
                    //add row to table
                    myScheduleToday.Rows.Add(r);

                    //create start cell and add to row.. cell index: 0
                    TableCell start = new TableCell();
                    start.Text = a.StartTime.ToString();
                    myScheduleToday.Rows[i].Cells.Add(start);

                    //create end cell and add to row.. cell index: 1
                    TableCell end = new TableCell();
                    end.Text = a.EndTime.ToString();
                    myScheduleToday.Rows[i].Cells.Add(end);

                    //created customer name cell and add to row.. cell index: 2
                    TableCell c = new TableCell();
                    c.Text = a.CustomerFName.ToString();
                    myScheduleToday.Rows[i].Cells.Add(c);

                    //create employee name cell and add to row.. cell index: 3
                    TableCell e = new TableCell();
                    e.Text = a.EmpFName.ToString();
                    myScheduleToday.Rows[i].Cells.Add(e);

                    //create service name cell and add to row.. cell index: 4
                    TableCell s = new TableCell();
                    s.Text = a.ServiceName.ToString();
                    myScheduleToday.Rows[i].Cells.Add(s);

                    //create arrival status cell and add to row.. cell index : 5
                    TableCell present = new TableCell();
                    present.Text = a.Arrived.ToString();
                    myScheduleToday.Rows[i].Cells.Add(present);
                    i++;
                }
            }
            catch (ApplicationException E)
            {
                Response.Write("<script>alert('Trouble communicating with the database.Report to admin and try again later.');</script>");
                Response.Write("<script>window.location='Stylist.aspx';</script>");
                function.logAnError(E.ToString());
                //Server.Transfer("Error.aspx");
            }
        }
    }
}