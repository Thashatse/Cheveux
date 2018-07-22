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

        protected void Page_Load(object sender, EventArgs e)
        {

            userID = Request.QueryString["empID"];
            if(userID != null)
            {
                getUser(userID);
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
            }
            catch(Exception Err)
            {
                function.logAnError(Err.ToString());
            }

        }
       
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                emp = new EMPLOYEE();

                emp.EmployeeID = userID;
                emp.AddressLine1 = txtAddLine1.Text.ToString();
                emp.AddressLine2 = txtAddLine2.Text.ToString();
                emp.Type = rdoType.SelectedValue.ToString();

                if (handler.updateEmployee(emp))
                {
                    Response.Write("<script>alert('Successful Update.');</script>");
                    Response.Redirect("../Manager/Employee.aspx",false);
                }
                else
                {
                    Response.Write("<script>alert('Update Failed.Please Try Again.');</script>");
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString());
                Response.Redirect("../Error.aspx");
            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Manager/Employee.aspx");
        }
    }
}