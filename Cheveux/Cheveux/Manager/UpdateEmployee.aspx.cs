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
        SP_GetEmployee_S_ s,bio = null;
        EMPLOYEE emp = null;
        HttpCookie cookie = null;
        List<SP_GetServices> specs = null;
        string userID;
        SP_ViewStylistSpecialisationAndBio viewSpec = null;
        STYLIST_SERVICE stylistService = null;
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

                userID = Request.QueryString["empID"];
                string type = Request.QueryString["Type"];
                string action = Request.QueryString["Action"];

                if(action== "UpdateStylist")
                {
                    if (userID != null)
                    {
                        getStylist(userID);
                        if (!Page.IsPostBack)
                        {
                            rdoType.SelectedValue = "S";
                            phStylist.Visible = true;
                        }
                    }
                }
                else if(action == "UpdateReceptionist")
                {
                    if(userID != null)
                    {
                        getUser(userID);
                        if (!Page.IsPostBack)
                        {
                            rdoType.SelectedValue = "R";

                        }
                    }
                }

                if(type == "NewEmp")
                {
                    getUser(userID);
                }
            }
            
        }
        public void getStylist(string stylistID)
        {
            try
            {
                s = handler.getEmployee_S(stylistID);
                TableRow row = new TableRow();
                tblUserImage.Rows.Add(row);
                TableCell userImage = new TableCell();
                userImage.Text = "<img src=" + s.UserImage
                                  + " alt='" + s.FirstName + " " + s.LastName +
                                 " Profile Image' width='125' height='125'/>";
                tblUserImage.Rows[0].Cells.Add(userImage);
                TableRow newRow = new TableRow();
                tblUserImage.Rows.Add(newRow);
                TableCell username = new TableCell();
                username.Text = "<p style='font-size:2em !important;'>" + s.Username.ToString() + "</p>";
                username.Font.Bold = true;
                tblUserImage.Rows[1].Cells.Add(username);

                if (!IsPostBack)
                {
                    try
                    {
                        specs = handler.BLL_GetAllServices();

                        foreach (SP_GetServices s in specs)
                        {
                            drpSpecs.DataSource = specs;
                            drpSpecs.DataTextField = "Name";
                            drpSpecs.DataValueField = "ServiceID";
                            drpSpecs.DataBind();
                        }
                        if (s.Specialisation != string.Empty || s.Specialisation != null)
                        {
                            drpSpecs.SelectedValue = s.ServiceID.ToString();
                        }
                    }
                    catch (Exception err)
                    {
                        drpSpecs.Text = "Services Unavailable";
                        function.logAnError("Error getting services for spec dropdown in updateEmp.aspx error:"
                                            + err.ToString());
                    }

                    if (s.Bio != string.Empty || s.Bio != null)
                    {
                        txtBio.InnerText = s.Bio.ToString();
                    }
                    if (s.ad1 != string.Empty || s.ad1 != null)
                    {
                        txtAddLine1.Text = s.ad1.ToString();
                    }
                    if (s.ad2 != string.Empty || s.ad2 != null)
                    {
                        txtAddLine2.Text = s.ad2.ToString();
                    }
                    if (s.Suburb != string.Empty || s.Suburb != null)
                    {
                        txtSuburb.Text = s.Suburb.ToString();
                    }
                    if (s.City != string.Empty || s.City != null)
                    {
                        txtCity.Text = s.City.ToString();
                    }
                }

            }
            catch (Exception Err)
            {
                getUser(userID);
                //phUsersErr.Visible = true;
                //phMain.Visible = false;
                //errorHeader.Text = "Error displaying user details.";
                //errorMessage.Text = "It seems there is a problem communicating with the database."
                //                    + "Please report problem to admin or try again later.";
                function.logAnError("Error get stylist details (the specialisation problem) [update.aspx {getStylist}] err:"+Err.ToString());
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

                if (!Page.IsPostBack)
                {
                    try
                    {
                        specs = handler.BLL_GetAllServices();

                        foreach (SP_GetServices s in specs)
                        {
                            drpSpecs.DataSource = specs;
                            drpSpecs.DataTextField = "Name";
                            drpSpecs.DataValueField = "ServiceID";
                            drpSpecs.DataBind();
                        }
                    }
                    catch (Exception err)
                    {
                        drpSpecs.Text = "Services Unavailable";
                        function.logAnError("Error getting services for spec dropdown in updateEmp.aspx error:"
                                            + err.ToString());
                    }
                    try
                    {
                        bio = handler.getBio(userID);
                        if (bio.Bio != string.Empty || bio.Bio != null)
                        {
                            txtBio.InnerText = bio.Bio.ToString();
                        }
                    }
                    catch(Exception err)
                    {
                        txtBio.Attributes.Add("placeholder", "Enter Employee Bio");
                        function.logAnError("couldn't get bio[getBio()] err: " + err.ToString());
                    }

                    if (view.addLine1 != string.Empty || view.addLine1 != null)
                    {
                        txtAddLine1.Text = view.addLine1.ToString();
                    }
                    if (view.addLine2 != string.Empty || view.addLine2 != null)
                    {
                        txtAddLine2.Text = view.addLine2.ToString();
                    }
                    if (view.suburb != string.Empty || view.suburb != null)
                    {
                        txtSuburb.Text = view.suburb.ToString();
                    }
                    if (view.city != string.Empty || view.city != null)
                    {
                        txtCity.Text = view.city.ToString();
                    }
                }
            }
            catch (Exception Err)
            {
                phUsersErr.Visible = true;
                phMain.Visible = false;
                errorHeader.Text = "Error displaying user details.";
                errorMessage.Text = "It seems there is a problem communicating with the database."
                                    + "Please report problem to admin or try again later.";
                function.logAnError("Error getting user details [update.aspx {getuser()}] err:" + Err.ToString());
            }
        }
        protected void rdoType_Change(object sender, EventArgs e)
        {
            if(rdoType.SelectedValue == "R")
            {
                phStylist.Visible = false;
            }
            else if(rdoType.SelectedValue == "S")
            {
                phStylist.Visible = true;
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

                emp.Bio = txtBio.InnerText.ToString();

                try
                {
                    viewSpec = handler.viewStylistSpecialisationAndBio(emp.EmployeeID);
                }
                catch(Exception Err)
                {
                    function.logAnError("Could not view stylist specialisation[btnUpdate of updateEmp] error:" +
                                          Err.ToString());
                }

                if(rdoType.SelectedValue == "S")
                {
                    if (viewSpec == null)
                    {
                        stylistService = new STYLIST_SERVICE();
                        stylistService.EmployeeID = userID;
                        stylistService.ServiceID = drpSpecs.SelectedValue;
                        handler.addSpecialisation(stylistService);
                    }

                    emp.Specialisation = drpSpecs.SelectedValue;
                }
                else
                {
                    emp.Specialisation = drpSpecs.Text;
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
                    lblUpdateErr.Text = "Unable to update the employees details.<br/>"+
                                         "<br/>Please report to management or the administrator.";
                }
            }
            catch (Exception Err)
            {
                phUpdateErr.Visible = true;
                lblUpdateErr.Text = "An error has occured.We are unable to update the employees details at this point in time.<br/>"
                                      + "Please report to management or the administrator.";
                function.logAnError("btnUpdateEmployee err:"+Err.ToString());
            }

        }
    }
}