using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Cheveux 
{
    public partial class Accounts : System.Web.UI.Page
    {
        Authentication auth = new Authentication();
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private string getRegCookie()
        {
            //get the user data from the cookie
            HttpCookie cookie = Request.Cookies["reg"];
            string reg = "error";
            if (cookie != null)
            {
                reg = cookie.Value;
            }
            return reg;
        }

        protected void btnAuthenticate_Click(object sender, EventArgs e)
        {
            //check if the user has requested a logout or login
            String action = Request.QueryString["action"];
            //login
            if (action != "Logout")
            {
                //log in
                //get the user data from the cookie
                string reg = getRegCookie();
                //check if there was a error
                if (reg == "error")
                {
                    //open error page
                    Response.Redirect("../Error.aspx?Error='A Error in when authenticating with google'");
                }

                /*
                 * use the bll.authenticate class to see if the user exist are ready or needs to register 
                 * as a new user
                 */
                string result = "";
                try
                {
                    result = auth.Authenticate(reg);
                }
                catch (ApplicationException Err)
                {
                    function.logAnError(Err.ToString());
                    Response.Redirect("../Error.aspx?Error='A Error in when authenticating with the Cheveux server'");
                }
                /*
                 * if the user is unregistered get the info requered and create a new user, 
                 * using the new account page
                 */
                if (result == "unRegUser")
                {
                    //Open the new account page, and set the page to redirect to as a querstring
                    String PreviousPage = Request.QueryString["PreviousPage"];
                    if (PreviousPage != null)
                    {
                        Response.Redirect("NewAccount.aspx?PreviousPage=" + PreviousPage);
                    }
                    Response.Redirect("NewAccount.aspx");
                }
                //if the user exists create a session cookie and return them to the previous or home page
                else if (result == "C" || result == "E")
                {
                    //remove the 'reg' cookie
                    HttpCookie cookie = new HttpCookie("reg");
                    cookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(cookie);
                    //log the user in by creating a cookie to manage their state
                    cookie = new HttpCookie("CheveuxUserID");
                    // Set the user id in it.
                    cookie["ID"] = reg.Split('|')[0];
                    cookie["UT"] = result.ToString();
                    // Add it to the current web response.
                    Response.Cookies.Add(cookie);
                    
                    //access control
                    //send the user to the correct page based on their usertype
                    if (result == "C")
                    {
                        Response.Redirect("../Default.aspx?" + "WB=" + reg.Split('|')[2]);
                    }else if (result == "E")
                    {
                        string EmpType = handler.getEmployeeType(reg.Split('|')[0]).Type.ToString().Replace(" ", string.Empty);
                        if(EmpType == "R")
                        {
                            //Receptionist
                            cookie["UT"] = "R";
                            goToPreviousPage();
                            Response.Redirect("../Receptionist/Receptionist.aspx");
                        }
                        else if (EmpType == "M")
                        {
                            //Manager
                            cookie["UT"] = "M";
                            goToPreviousPage();
                            Response.Redirect("../Manager/Dashboard.aspx?WB=True");
                        }
                        else if (EmpType == "S")
                        {
                            //stylist
                            cookie["UT"] = "S";
                            //go back to the previous page if there is one
                            goToPreviousPage();
                            Response.Redirect("../Stylist/Stylist.aspx");
                        }
                        else
                        {
                            function.logAnError("Unknown user type found during login - User details (from Google Server):" +
                                reg.ToString());
                            Response.Redirect("../Default.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("../Default.aspx?" + "WB=" + reg.Split('|')[2]);
                    }
                }
                else if (result == "Error")
                {
                    Response.Redirect("../Error.aspx?Error='A Error in when authenticating with the Cheveux sereve'");
                }
            }
            //logout
            else
            {
                //log out
                //log the user out on googles servers
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "signOut()", true);
                //remove the 'reg' cookie
                HttpCookie cookie = new HttpCookie("reg");
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
                //remove the 'CheveuxUserID' cookie
                cookie = new HttpCookie("CheveuxUserID");
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
                //retun the usere to the home page
                Response.Redirect("../Default.aspx");
            }
        }

        private void goToPreviousPage()
        {
            //get the privious page to redirect to
            String PreviousPage = Request.QueryString["PreviousPage"];
            //go back to the previous page if there is one
            if (PreviousPage == "Help/CheveuxHelpCenter.aspx")
            {
                Response.Redirect("../Help/CheveuxHelpCenter.aspx#InternalHelp");
            }else if(PreviousPage == "BusinessSetting.aspx")
            {
                Response.Redirect("../Manager/BusinessSetting.aspx");
            }else if(PreviousPage == "Reports.aspx")
            {
                Response.Redirect("../Manager/Reports.aspx");
            }
            else if (PreviousPage == "Manager.aspx")
            {
                Response.Redirect("../Manager/Dashboard.aspx?WB=True");
            }
            else if (PreviousPage == "Employee.aspx")
            {
                Response.Redirect("../Manager/Employee.aspx");
            }
            else if (PreviousPage == "Products.aspx")
            {
                Response.Redirect("../Manager/Products.aspx");
            }
            else if (PreviousPage == "Service.aspx")
            {
                Response.Redirect("../Manager/Service.aspx");
            }
            else if (PreviousPage == "Profile.aspx")
            {
                Response.Redirect("../Profile.aspx");
            }
        }
    }
}