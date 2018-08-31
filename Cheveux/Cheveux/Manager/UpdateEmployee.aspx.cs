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
    public partial class UpdateEmployee : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        SP_ViewEmployee view = null;
        EMPLOYEE emp = null;
        string userID;
        HttpCookie cookie = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            errorHeader.Font.Bold = true;
            errorHeader.Font.Underline = true;
            errorHeader.Font.Size = 21;
            errorMessage.Font.Size = 14;
            errorToReport.Font.Size = 10;

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
            userID = Request.QueryString["empID"];
            if(userID != null)
            {
                getUser(userID);
            }
            if (rdoType.SelectedValue == "R")
            {
                divBio.Visible = false;
            }
            else if(rdoType.SelectedValue == "S")
            {
                divBio.Visible = true;
            }
        }
        public void getUser(string userID)
        {
            try
            {
                view = handler.viewEmployee(userID);
                TableRow row = new TableRow();
                tblUserImage.Rows.Add(row);
                TableCell userImage = new TableCell();
                userImage.Text = "<img src=" + view.empImage
                                  + " alt='" + view.firstName + " " + view.lastName +
                                 " Profile Image' width='125' height='125'/>";
                tblUserImage.Rows[0].Cells.Add(userImage);
                TableRow newRow = new TableRow();
                tblUserImage.Rows.Add(newRow);
                TableCell username = new TableCell();
                username.Text = "<p style='font-size:2em !important;'>" + view.userName.ToString() + "</p>";
                username.Font.Bold = true;
                tblUserImage.Rows[1].Cells.Add(username);

                if(view.employeeType.Replace(" ", string.Empty) == "S")
                {
                    txtBio.InnerText = view.bio.ToString();   
                }
                
            }
            catch (Exception Err)
            {
                phUsersErr.Visible = true;
                phMain.Visible = false;
                errorHeader.Text = "Error displaying user details.";
                errorMessage.Text = "It seems there is a problem communicating with the database."
                                    + "Please report problem to admin or try again later.";
                errorToReport.Text = "Error To report:" + Err.ToString();

                function.logAnError(Err.ToString());
            }

        }
       
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string ad2;
            try
            {
                emp = new EMPLOYEE();

                emp.EmployeeID = userID;
                emp.Type = rdoType.SelectedValue.ToString();

                if(emp.Type.Replace(" ", string.Empty) == "R")
                {
                    emp.Bio = "";
                }
                else
                {
                    emp.Bio = txtBio.InnerText.ToString();
                }

                emp.AddressLine1 = txtAddLine1.Text;

                if (txtAddLine2.Text == null)
                {
                    ad2 = "";
                }
                else
                {
                    ad2 = txtAddLine2.Text;
                }
                emp.AddressLine2 = ad2;
                emp.Suburb = txtSuburb.Text;
                emp.City = txtCity.Text;

                if (handler.updateEmployee(emp))
                {
                    Response.Redirect("../Manager/Employee.aspx?EmployeeID="+emp.EmployeeID.ToString().Replace(" ",string.Empty),false);
                }
                else
                {
                    phUpdateErr.Visible = true;
                    lblUpdateErr.Text = "Unable to update the employees details.<br/>"
                                          + "Sorry for the inconvenience." +
                                          "<br/>Please report to management or the administrator.";
                }
            }
            catch (Exception Err)
            {
                phUpdateErr.Visible = true;
                lblUpdateErr.Text = "An error has occured.We are unable to update the employees details at this point in time.<br/>"
                                      + "Sorry for the inconvenience.Please report to management or the administrator."
                                      + "<br/>Error: " + Err.ToString();
                function.logAnError(Err.ToString());
            }

        }
    }
}