using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;

namespace Cheveux.Manager
{
    public partial class AddEmpolyee : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        List<SP_UserList> userList = null;
        List<SP_SearchForUser> searchForUser = null;
        EMPLOYEE emp = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            displayUsers();
        }
        public void displayUsers()
        {
            Button btnAdd;
            try
            {
                userList = handler.userList();

                
                TableRow row = new TableRow();
                row.Height = 50;
                tblUsers.Rows.Add(row);

                TableCell userImage = new TableCell();
                userImage.Width = 300;
                tblUsers.Rows[0].Cells.Add(userImage);

                TableCell fullName = new TableCell();
                fullName.Text = "Full Name";
                fullName.Width = 300;
                fullName.Font.Bold = true;
                tblUsers.Rows[0].Cells.Add(fullName);

                TableCell userType = new TableCell();
                userType.Text = "User Type";
                userType.Width = 200;
                userType.Font.Bold = true;
                tblUsers.Rows[0].Cells.Add(userType);

                TableCell adLine1 = new TableCell();
                adLine1.Text = "AddressLine 1";
                adLine1.Width = 200;
                adLine1.Font.Bold = true;
                tblUsers.Rows[0].Cells.Add(adLine1);

                TableCell adLine2 = new TableCell();
                adLine2.Text = "AddressLine 2";
                adLine2.Width = 200;
                adLine2.Font.Bold = true;
                tblUsers.Rows[0].Cells.Add(adLine2);

                int i = 1;

                foreach (SP_UserList u in userList)
                {
                    TableRow r = new TableRow();
                    tblUsers.Rows.Add(r);

                    TableCell uImage = new TableCell();
                    uImage.Width = 150;
                    uImage.Text = "<img src=" + u.UserImage +
                                    " alt='User Profile Picture' " + 
                                    "width='80' height='80' />" ;
                    tblUsers.Rows[i].Cells.Add(uImage);

                    TableCell fName = new TableCell();
                    fName.Text = u.FullName.ToString();
                    fName.Width = 300;
                    fName.Font.Bold = true;
                    tblUsers.Rows[i].Cells.Add(fName);

                    TableCell uTypeCell = new TableCell();
                    uTypeCell.Height = 100;
                    RadioButtonList uTypeList = new RadioButtonList();
                    uTypeList.Items.Add("R");
                    uTypeList.Items.Add("S");
                    uTypeList.CellPadding = 10;
                    uTypeList.RepeatDirection = RepeatDirection.Horizontal;
                    uTypeList.DataBind();
                    uTypeCell.Controls.Add(uTypeList);
                    tblUsers.Rows[i].Cells.Add(uTypeCell);

                    TableCell a1 = new TableCell();
                    TextBox txtAddLine1 = new TextBox();
                    txtAddLine1.CssClass = "form-control";
                    a1.Controls.Add(txtAddLine1);
                    tblUsers.Rows[i].Cells.Add(a1);


                    TableCell a2 = new TableCell();
                    TextBox txtAddLine2 = new TextBox();
                    txtAddLine2.CssClass = "form-control";
                    a2.Controls.Add(txtAddLine2);
                    tblUsers.Rows[i].Cells.Add(a2);


                    TableCell buttonCell = new TableCell();
                    buttonCell.Width = 200;
                    buttonCell.Height = 50;
                    btnAdd = new Button();
                    btnAdd.Text = "Add";
                    btnAdd.CssClass = "btn btn-primary";
                    btnAdd.Click += (ss, ee) =>
                    {
                        try
                        {
                            emp = new EMPLOYEE();

                            emp.EmployeeID = u.UserID.ToString();
                            emp.AddressLine1 = txtAddLine1.Text;
                            emp.AddressLine2 = txtAddLine2.Text;
                            emp.Type = uTypeList.SelectedValue.ToString();

                            if (handler.addEmployee(emp))
                            {
                                Response.Write("<script>alert('Employee Added');</script>");
                                Response.Redirect(Request.RawUrl);
                            }
                            else
                            {
                                Response.Write("<script>alert('Error. Please try again');</script>");
                            }
                        }
                        catch (Exception err)
                        {
                            Response.Write("<script>alert('Our apologies. An error has occured.')</script>");
                            //add error to the error log and then display response tab to say that an error has occured
                            function.logAnError(err.ToString());
                        }
                        

                    };
                    //add button to cell 
                    buttonCell.Controls.Add(btnAdd);
                    //add cell to row
                    tblUsers.Rows[i].Cells.Add(buttonCell);

                    i++;
                }
            }
            catch(ApplicationException E)
            {
                //dispaly error message below//

                function.logAnError(E.ToString());
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            phUsers.Visible = false;
            phSearchedUsers.Visible = true;
            Button btnAdd;
            try
            {
                searchForUser = handler.searchForUser(txtSearch.Text.ToString());
                TableRow row = new TableRow();
                row.Height = 50;
                tblSearchedUsers.Rows.Add(row);

                TableCell userImage = new TableCell();
                userImage.Width = 300;
                tblSearchedUsers.Rows[0].Cells.Add(userImage);

                TableCell fullName = new TableCell();
                fullName.Text = "Full Name";
                fullName.Width = 300;
                fullName.Font.Bold = true;
                tblSearchedUsers.Rows[0].Cells.Add(fullName);

                TableCell userType = new TableCell();
                userType.Text = "User Type";
                userType.Width = 200;
                userType.Font.Bold = true;
                tblSearchedUsers.Rows[0].Cells.Add(userType);

                TableCell adLine1 = new TableCell();
                adLine1.Text = "AddressLine 1";
                adLine1.Width = 200;
                adLine1.Font.Bold = true;
                tblSearchedUsers.Rows[0].Cells.Add(adLine1);

                TableCell adLine2 = new TableCell();
                adLine2.Text = "AddressLine 2";
                adLine2.Width = 200;
                adLine2.Font.Bold = true;
                tblSearchedUsers.Rows[0].Cells.Add(adLine2);
                int i = 1;

                foreach (SP_SearchForUser u in searchForUser)
                {
                    TableRow r = new TableRow();
                    tblSearchedUsers.Rows.Add(r);

                    TableCell uImage = new TableCell();
                    uImage.Width = 150;
                    uImage.Text = "<img src=" + u.UserImage +
                                    " alt='User Profile Picture' " +
                                    "width='80' height='80' />";
                    tblSearchedUsers.Rows[i].Cells.Add(uImage);

                    TableCell fName = new TableCell();
                    fName.Text = u.FullName.ToString();
                    fName.Width = 300;
                    fName.Font.Bold = true;
                    tblSearchedUsers.Rows[i].Cells.Add(fName);

                    TableCell uTypeCell = new TableCell();
                    uTypeCell.Height = 100;
                    RadioButtonList uTypeList = new RadioButtonList();
                    uTypeList.Items.Add("R");
                    uTypeList.Items.Add("S");
                    uTypeList.CellPadding = 10;
                    uTypeList.RepeatDirection = RepeatDirection.Horizontal; 
                    uTypeList.DataBind();
                    uTypeCell.Controls.Add(uTypeList);
                    tblSearchedUsers.Rows[i].Cells.Add(uTypeCell);

                    TableCell a1 = new TableCell();
                    TextBox txtAddLine1 = new TextBox();
                    txtAddLine1.CssClass = "form-control";
                    a1.Controls.Add(txtAddLine1);
                    tblSearchedUsers.Rows[i].Cells.Add(a1);


                    TableCell a2 = new TableCell();
                    TextBox txtAddLine2 = new TextBox();
                    txtAddLine2.CssClass = "form-control";
                    a2.Controls.Add(txtAddLine2);
                    tblSearchedUsers.Rows[i].Cells.Add(a2);

                    TableCell buttonCell = new TableCell();
                    buttonCell.Width = 200;
                    buttonCell.Height = 50;
                    btnAdd = new Button();
                    btnAdd.Text = "Add";
                    btnAdd.CssClass = "btn btn-primary";
                    btnAdd.Click += (ss, ee) =>
                    {
                        try
                        {
                            emp = new EMPLOYEE();

                            emp.EmployeeID = u.UserID.ToString();
                            emp.AddressLine1 = txtAddLine1.Text;
                            emp.AddressLine2 = txtAddLine2.Text;
                            emp.Type = uTypeList.SelectedValue.ToString(); 

                            if (handler.addEmployee(emp))
                            {
                                Response.Write("<script>alert('Employee Added');</script>");
                                Response.Redirect(Request.RawUrl);
                            }
                            else
                            {
                                Response.Write("<script>alert('Error. Please try again');</script>");
                            }
                        }
                        catch (Exception err)
                        {
                            Response.Write("<script>alert('Our apologies. An error has occured.')</script>");
                            //add error to the error log and then display response tab to say that an error has occured
                            function.logAnError(err.ToString());
                        }


                    };
                    //add button to cell 
                    buttonCell.Controls.Add(btnAdd);
                    //add cell to row
                    tblSearchedUsers.Rows[i].Cells.Add(buttonCell);

                    i++;
                }
                if (tblSearchedUsers.Rows.Count > 1)
                {
                    resultsHeading.InnerText = "Search results for " + "'"+
                                                txtSearch.Text + "'";
                    resultsHeading.Visible = true;
                }
                else if(tblSearchedUsers.Rows.Count == 1)
                {
                    resultsHeading.InnerText = "No results were found";
                    resultsHeading.Visible = true;
                }

            }
            catch (ApplicationException E)
            {
                //dispaly error message below//

                function.logAnError(E.ToString());
            }
        }

    }
}