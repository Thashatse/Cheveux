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

                //needs to be centered
                TableCell userImage = new TableCell();
                userImage.Width = 150;
                userImage.Text = "<img src=" + view.empImage
                                  + " alt='" + view.firstName + " " + view.lastName +
                                 " Profile Image' width='75' height='75'/>";
                tblUserImage.Rows[0].Cells.Add(userImage);

                TableCell username = new TableCell();
                username.Text = view.userName.ToString();
                username.Font.Bold = true;
                tblUserImage.Rows[0].Cells.Add(username);
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

                    //System.Threading.ThreadAbortException: Thread was being aborted error but updates on the database (fix) 

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
            Response.Redirect("../Manager/Dashboard.aspx");
        }
    }
}