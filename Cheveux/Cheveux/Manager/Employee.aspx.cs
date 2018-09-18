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

        SP_ViewEmployee emp = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                tblEmployeeTable.Rows.Clear();
            }

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

                //upon successful employee update
                string employeeID = Request.QueryString["EmployeeID"];
                if(employeeID != null)
                {
                    try
                    {
                        //get the employees name
                        emp = handler.viewEmployee(employeeID);
                        phNotif.Visible = true;
                        lblNotif.Text = "Update successful for "
                                        + "<a href='../Profile.aspx?Action=View&empID="
                                        + emp.UserID.ToString().Replace(" ", string.Empty)
                                        + "'>"
                                        + emp.firstName.ToString() + ' '
                                        +emp.lastName.ToString()
                                         + "</a>";
                    }
                    catch (Exception Err)
                    {
                        phNotif.Visible = false;
                        function.logAnError(Err.ToString());
                    }
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
                    newHeaderCell.Width = 250;
                    tblEmployeeTable.Rows[rowCount].Cells.Add(newHeaderCell);
                    //create a header row and set cell withs
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Name: ";
                    newHeaderCell.Width = 500;
                    tblEmployeeTable.Rows[rowCount].Cells.Add(newHeaderCell);
                    //create a header row and set cell withs
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 250;
                    tblEmployeeTable.Rows[rowCount].Cells.Add(newHeaderCell);

                    //increment rowcounter
                    rowCount++;

                    char type = 'X';
                    foreach (SP_ViewEmployee emp in employees)
                    {
                        //if the employe maches the selected type
                        if ((emp.employeeType[0] == empType || empType == 'A') &&
                            (compareToSearchTerm(emp.firstName) == true ||
                            compareToSearchTerm(emp.firstName) == true ||
                            compareToSearchTerm(emp.email) == true ||
                            compareToSearchTerm(emp.phoneNumber) == true))
                        {
                            if (type != emp.employeeType[0])
                            {
                                //add a new row to the table
                                newRow = new TableRow();
                                newRow.Height = 50;
                                tblEmployeeTable.Rows.Add(newRow);
                                //employee Type
                                newHeaderCell = new TableHeaderCell();
                                newHeaderCell.Text = function.GetFullEmployeeTypeText(emp.employeeType[0])+"('s): ";
                                tblEmployeeTable.Rows[rowCount].Cells.Add(newHeaderCell);
                                //create a new row in the table and set the height
                                newRow = new TableRow();
                                newRow.Height = 50;
                                tblEmployeeTable.Rows.Add(newRow);
                                type = emp.employeeType[0];
                                //increment rowcounter
                                rowCount++;
                            }

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
                            newCell.Text = "<a class='btn btn-default' href = '../Profile.aspx?Action=View" +
                                        "&empID=" + emp.UserID.ToString().Replace(" ", string.Empty) +
                                        "'>"+emp.firstName + " " + emp.lastName+ "</a>";
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

        public bool compareToSearchTerm(string toBeCompared)
        {
            bool result = false;
            if (txtProductSearchTerm.Text != null)
            {
                toBeCompared = toBeCompared.ToLower();
                string searcTearm = txtProductSearchTerm.Text.ToLower();
                if (toBeCompared.Contains(searcTearm))
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }
            return result;
        }
    }
}