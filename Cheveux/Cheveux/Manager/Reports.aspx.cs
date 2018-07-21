using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;

namespace Cheveux.Manager
{
    public partial class Reports : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is loged out
            cookie = Request.Cookies["CheveuxUserID"];

            if (cookie == null)
            {
                //if the user is not loged in as a manager do not display Bussines setting
            }
            else if (cookie["UT"] != "M")
            {
                Response.Redirect("../Default.aspx");
            }
            else if (cookie["UT"] == "M")
            {
                //if the user is loged in as a manager display Bussines setting
                drpReport_SelectedIndexChanged1(sender, e);
                LogedIn.Visible = true;
                LogedOut.Visible = false;
            }
        }

        protected void drpReport_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (drpReport.SelectedIndex == 0)
            {
                reportByContainer.Visible = false;
                reportDateRangeContainer.Visible = false;
                divReport.Visible = false;
            }
            else if (drpReport.SelectedIndex == 1)
            {
                reportByContainer.Visible = false;
                reportDateRangeContainer.Visible = false;
                divReport.Visible = true;
                //display the sales report
                getSalesReport();
            }
            else if (drpReport.SelectedIndex == 2)
            {
                reportByContainer.Visible = true;
                if (ddlReportFor.SelectedIndex == -1)
                {
                    try
                    {
                        List<SP_GetEmpNames> list = handler.BLL_GetEmpNames();
                        foreach (SP_GetEmpNames emps in list)
                        {
                            //Load employee names into dropdownlist
                            ddlReportFor.DataSource = list;
                            //set the coloumn that will be displayed to the user
                            ddlReportFor.DataTextField = "Name";
                            //set the coloumn that will be used for the valuefield
                            ddlReportFor.DataValueField = "EmployeeID";
                            //bind the data
                            ddlReportFor.DataBind();
                            /*set the default (text (dropdownlist index[0]) that will first be displayed to the user.
                             * in this case the "instruction" to select the employee
                            */
                            ddlReportFor.Items.Insert(0, new ListItem("Select Stylist", "-1"));
                        }
                    }
                    catch (ApplicationException Err)
                    {
                        function.logAnError(Err.ToString());
                        reportLable.Text = ("An error occurred communicating with the database. Please Try Again Later");
                    }
                }

                if (ddlReportFor.SelectedValue != "-1")
                    {
                        reportDateRangeContainer.Visible = false;
                        divReport.Visible = true;
                        //display the sales report
                        getBookingForHairstylist();
                    }
                
            }
            else if (drpReport.SelectedIndex == 3)
            {
                reportByContainer.Visible = true;
                if (ddlReportFor.SelectedIndex == -1)
                {
                    try
                    {
                        List<SP_GetEmpNames> list = handler.BLL_GetEmpNames();
                        foreach (SP_GetEmpNames emps in list)
                        {
                            //Load employee names into dropdownlist
                            ddlReportFor.DataSource = list;
                            //set the coloumn that will be displayed to the user
                            ddlReportFor.DataTextField = "Name";
                            //set the coloumn that will be used for the valuefield
                            ddlReportFor.DataValueField = "EmployeeID";
                            //bind the data
                            ddlReportFor.DataBind();
                            /*set the default (text (dropdownlist index[0]) that will first be displayed to the user.
                             * in this case the "instruction" to select the employee
                            */
                            ddlReportFor.Items.Insert(0, new ListItem("Select Stylist", "-1"));
                        }
                    }
                    catch (ApplicationException Err)
                    {
                        function.logAnError(Err.ToString());
                        reportLable.Text = ("An error occurred communicating with the database. Please Try Again Later");
                    }
                }

                if (ddlReportFor.SelectedValue != "-1")
                {
                    reportDateRangeContainer.Visible = true;
                }

                if (ddlReportFor.SelectedValue != "-1" && CalendarDateRage.SelectedDate.ToString() != "0001/01/01 00:00:00")
                {
                    divReport.Visible = true;
                    //display the sales report
                    getBookingForHairstylistDateRange();
                }

            }
        }

        private void getSalesReport()
        {
            reportLable.Text = "Sales";
            reportByLable.Text = "All Stylists";
            reportDateRangeLable.Text = "All Time";
            reportGenerateDateLable.Text = DateTime.Now.ToString("HH:mm dd MMM yyyy"); 

        }

        private void getBookingForHairstylist()
        {
            reportLable.Text = "Bookings";
            reportByLable.Text = ddlReportFor.SelectedItem.Text.ToString();
            reportDateRangeLable.Text = "All Time";
            reportGenerateDateLable.Text = DateTime.Now.ToString("HH:mm dd MMM yyyy");

        }

        private void getBookingForHairstylistDateRange()
        {
            reportLable.Text = "Bookings";
            reportByLable.Text = ddlReportFor.SelectedItem.Text.ToString();
            //if(CalendarDateRage.SelectedDates.)
            reportDateRangeLable.Text = CalendarDateRage.SelectedDates.ToString();
            reportGenerateDateLable.Text = DateTime.Now.ToString("HH:mm dd MMM yyyy");

        }
    }
}