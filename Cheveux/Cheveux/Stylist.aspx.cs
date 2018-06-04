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


                    TableRow r = new TableRow();
                    AgendaTable.Rows.Add(r);


                    TableCell start = new TableCell();
                    start.Text = a.StartTime.ToString();
                    AgendaTable.Rows[i].Cells.Add(start);

                    TableCell end = new TableCell();
                    end.Text = a.EndTime.ToString();
                    AgendaTable.Rows[i].Cells.Add(end);

                    TableCell c = new TableCell();
                    c.Text = a.CustomerFName.ToString();
                    AgendaTable.Rows[i].Cells.Add(c);

                    TableCell e = new TableCell();
                    e.Text = a.EmpFName.ToString();
                    AgendaTable.Rows[i].Cells.Add(e);

                    TableCell s = new TableCell();
                    s.Text = a.ServiceName.ToString();
                    AgendaTable.Rows[i].Cells.Add(s);

                    TableCell present = new TableCell();
                    present.Text = a.Arrived.ToString();
                    AgendaTable.Rows[i].Cells.Add(present);


                    TableCell buttonCell = new TableCell();
                    buttonCell.Width = 200;
                    buttonCell.Height = 50;
                    btn = new Button();
                    btn.Text = "Visit Record";
                    btn.CssClass = "btn btn-outline-dark";
                    btn.Click += (ss, ee) => { Response.Redirect("~/CustomerVisit.aspx"); };
                    buttonCell.Controls.Add(btn);
                    AgendaTable.Rows[i].Cells.Add(buttonCell);
                    i++;
                }
            }
            catch (ApplicationException E)
            {
                function.logAnError(E.ToString());
                Server.Transfer("~/Error.aspx");
            }
        }

    }
}