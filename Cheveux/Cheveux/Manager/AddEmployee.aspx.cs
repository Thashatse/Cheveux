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
        EMPLOYEE emp = null;

        HttpCookie cookie = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            cookie = Request.Cookies["CheveuxUserID"];
            if(cookie == null)
            {
                phLogIn.Visible = true;
                phMain.Visible = false;
            }
            else if(cookie["UT"] != "M")
            {
                Response.Redirect("../Default.aspx");
            }
            else if(cookie["UT"] == "M")
            {
                phMain.Visible = true;
                phLogIn.Visible = false;
            }

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
                                    "width='80' height='80' />";
                    tblUsers.Rows[i].Cells.Add(uImage);

                    TableCell fName = new TableCell();
                    fName.Text = u.FullName.ToString();
                    fName.Width = 300;
                    fName.Font.Bold = true;
                    tblUsers.Rows[i].Cells.Add(fName);

                    TableCell uTypeCell = new TableCell();
                    uTypeCell.Height = 100;
                    RadioButtonList uTypeList = new RadioButtonList();
                    uTypeList.ClientIDMode = ClientIDMode.AutoID;
                    uTypeList.Items.Add("R");
                    uTypeList.Items.Add("S");
                    uTypeList.CellPadding = 10;
                    uTypeList.RepeatDirection = RepeatDirection.Horizontal;
                    uTypeList.DataBind();
                    uTypeCell.Controls.Add(uTypeList);
                    tblUsers.Rows[i].Cells.Add(uTypeCell);

                    TableCell a1 = new TableCell();
                    TextBox txtAddLine1 = new TextBox();
                    txtAddLine1.ClientIDMode = ClientIDMode.AutoID;
                    txtAddLine1.CssClass = "form-control";
                    a1.Controls.Add(txtAddLine1);
                    tblUsers.Rows[i].Cells.Add(a1);


                    TableCell a2 = new TableCell();
                    TextBox txtAddLine2 = new TextBox();
                    txtAddLine2.ClientIDMode = ClientIDMode.AutoID;
                    txtAddLine2.CssClass = "form-control";
                    a2.Controls.Add(txtAddLine2);
                    tblUsers.Rows[i].Cells.Add(a2);


                    TableCell buttonCell = new TableCell();
                    buttonCell.Width = 300;
                    buttonCell.Height = 100;
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

                            if ((txtAddLine1.Text.ToString() == string.Empty) || (txtAddLine2.Text.ToString() == string.Empty) || (uTypeList.SelectedValue.ToString() == string.Empty))
                            {
                                valLabel.Text = "<p style='font-size:14px;color:red;'>&nbsp;*Please enter all fields</p>";
                                valLabel.Visible = true;
                                buttonCell.Controls.Add(valLabel);
                            }

                            if (txtAddLine1.Text.ToString() != string.Empty && txtAddLine2.Text.ToString() != string.Empty && uTypeList.SelectedValue.ToString() != string.Empty)
                            {
                                if (handler.addEmployee(emp))
                                {
                                    Response.Write("<script>alert('Employee Added');</script>");
                                    Response.Redirect(Request.RawUrl);
                                }
                                else
                                {
                                    //Response.Write("<script>alert('Error. Please try again');</script>");
                                    phAddErr.Visible = true;
                                    lblAddErr.Text = "An error has occured.Please try again or report to management.<br/>"
                                                          + "Sorry for the inconvenience.";
                                }
                            }


                        }
                        catch (Exception err)
                        {
                            //Response.Write("<script>alert('Our apologies. An error has occured.')</script>");
                            phAddErr.Visible = true;
                            lblAddErr.Text = "An error has occured.We are unable to add the employee at this point in time.<br/>"
                                                  + "Sorry for the inconvenience."
                                                  + "<br/>Error: " + err.ToString();
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
            catch (ApplicationException E)
            {
                phUsersErr.Visible = true;
                errorHeader.Text = "Oh no!";
                errorMessage.Text = "It seems there is a communicating with the database."
                                    + "Please report problem to admin or try again later.";
                errorToReport.Text = "Error To report:" + E.ToString();

                function.logAnError(E.ToString());
            }
        }
    }
}

