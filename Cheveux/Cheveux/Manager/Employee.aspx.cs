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
    public partial class Employee : System.Web.UI.Page
    {

        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;
        List<SP_ViewEmployee> employees = null;
        List<SP_GetEmployeeTypes> empTypes = null;

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
                //if the user is loged in as a manager display the employe details and drpdownlist
                LogedIn.Visible = true;
                LogedOut.Visible = false;

                if (!Page.IsPostBack)
                {
                    drpEmpTyp.Items.Add(new ListItem("All", "A"));
                    try
                    {
                        empTypes = handler.getEmpTypes();
                        foreach (SP_GetEmployeeTypes empType in empTypes)
                        {
                            drpEmpTyp.Items.Add(new ListItem(
                                function.GetFullEmployeeTypeText(empType.Type.ToString()[0]), 
                                empType.Type.ToString()));
                        }
                    }
                    catch (Exception Err)
                    {
                        function.logAnError(Err.ToString() + "Unable to load drpEmpTyp on emplyee Page");
                    }
                    drpEmpTyp.Items.RemoveAt(0);
                    drpEmpTyp.SelectedIndex = 0;
                }
                //get the selected sort by and display the results
                loadEmployeeList(drpEmpTyp.SelectedValue.ToString()[0]);
            }
        }

        public void loadEmployeeList(char empType)
        {
            try
            {
                //load a list of all employees
                employees = handler.viewAllEmployees();
                //track row count
                int rowCount = 0;
                //count the number of employees found
                int empCount = 0;

                if (employees.Count > 0)
                {
                    //disply the table headers
                    //create a new row in the table and set the height
                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblEmployeeTable.Rows.Add(newRow);
                    //create a header row and set cell withs
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 100;
                    tblEmployeeTable.Rows[rowCount].Cells.Add(newHeaderCell);
                    //create a header row and set cell withs
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Full Name: ";
                    newHeaderCell.Width = 300;
                    tblEmployeeTable.Rows[rowCount].Cells.Add(newHeaderCell);
                    //create a header row and set cell withs
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Employee Type: ";
                    newHeaderCell.Width = 200;
                    tblEmployeeTable.Rows[rowCount].Cells.Add(newHeaderCell);
                    //create a header row and set cell withs
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 250;
                    tblEmployeeTable.Rows[rowCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 250;
                    tblEmployeeTable.Rows[rowCount].Cells.Add(newHeaderCell);

                    //increment rowcounter
                    rowCount++;

                    foreach (SP_ViewEmployee emp in employees)
                    {
                        //if the employe maches the selected type
                        if (emp.employeeType[0] == empType || empType == 'A')
                        {
                            //diplay the employee details
                            //add a new row to the table
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblEmployeeTable.Rows.Add(newRow);

                            //image
                            TableCell newCell = new TableCell();
                            newCell.Text = "<img src=" + emp.empImage
                                        + " alt='" + emp.firstName + " " + emp.lastName +
                                        " Profile Image' width='75' height='75'/>";
                            tblEmployeeTable.Rows[rowCount].Cells.Add(newCell);

                            //Full Name
                            newCell = new TableCell();
                            newCell.Text = emp.firstName + " " + emp.lastName;
                            tblEmployeeTable.Rows[rowCount].Cells.Add(newCell);

                            //Employee Type
                            newCell = new TableCell();
                            newCell.Text = function.GetFullEmployeeTypeText(emp.employeeType[0]);
                            tblEmployeeTable.Rows[rowCount].Cells.Add(newCell);

                            //Contact (Phone & Email)
                            newCell = new TableCell();
                            newCell.Text = "<button type = 'button' class='btn btn-default'>" +
                                "<a href = 'tel:" + emp.phoneNumber.ToString() +
                                "'>Phone    </a></button>          " +
                                "<button type = 'button' class='btn btn-default'>" +
                                "<a href = 'mailto:" + emp.email.ToString() +
                                "'>Email    </a></button>";
                            tblEmployeeTable.Rows[rowCount].Cells.Add(newCell);

                            //view & edit
                            newCell = new TableCell();
                            //Edit employee link to be added by sike
                            newCell.Text =
                                "<button type = 'button' class='btn btn-default'>" + 
                                "<a href = '#?" +
                                        "empID=" + emp.UserID.ToString().Replace(" ", string.Empty) +
                                        "&PreviousPage=../Manager/Employee.aspx'>Edit  </a></button>          " +

                                        "<button type = 'button' class='btn btn-default'>" +
                                        "<a href = '../Profile.aspx?Action=View" +
                                        "&empID=" + emp.UserID.ToString().Replace(" ", string.Empty) +
                                        "&PreviousPage=../Manager/Employee.aspx'>View   </a></button>";
                            tblEmployeeTable.Rows[rowCount].Cells.Add(newCell);

                            //increment rowcounter
                            rowCount++;
                            //increment count
                            empCount++;
                        }
                    }
                }

                if (empType == 'A')
                    {
                        employeeJumbotronLable.Text = empCount + " Employees";
                    }
                    else
                    {
                        employeeJumbotronLable.Text = empCount + " " + function.GetFullEmployeeTypeText(empType);
                    }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving list of emplyees for employee type: " 
                    + empType +" - "+function.GetFullEmployeeTypeText(empType)
                    + " in loadEmployeeList(char empType) method on Manager/Employee page");
                employeeJumbotronLable.Font.Size = 22;
                employeeJumbotronLable.Font.Bold = true;
                employeeJumbotronLable.Text = "An error occurred retrieving employee details";
            }
        }
    }
}