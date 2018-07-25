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
            //set the create acount URL
            if (PreviousPage != null)
            {
                lCreateAccount.Text = "<a href='../Authentication/NewAccount.aspx?Type=Email&PreviousPage=" + PreviousPage;
            }
            lCreateAccount.Text = "<a href='../Authentication/NewAccount.aspx?Type=Email'>Create Acount</a>";
            //check if the user has requested a logout or login
            String action = Request.QueryString["action"];
            //login
            if (action == "Logout")
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

            //check if the user has requested to sign in with email
            string singInType = Request.QueryString["Type"];
            if (singInType == "Email")
            {
                //hide sign in with div and show sign in with email
                divAccountType.Visible = false;
                divEmailAcount.Visible = true;
                //check for any othe alerts
                string alert = Request.QueryString["Alert"];
                if(alert != null || alert != "")
                {
                    lError.Visible = true;
                    lError.Text = alert;
                }
            }
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
                    result = auth.AuthenticateGoogle(reg);
                }
                catch (Exception Err)
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
                    //get the user data from the cookie
                    reg = getRegCookie();
                        string[] regArray = reg.Split('|');
                    if (auth.checkForAccountEmail(regArray[1].ToString(), false) == false){
                        //Open the new account page, and set the page to redirect to as a querstring
                        String PreviousPage = Request.QueryString["PreviousPage"];
                        if (PreviousPage != null)
                        {
                            Response.Redirect("../Authentication/NewAccount.aspx?Type=Google&PreviousPage=" + PreviousPage);
                        }
                        Response.Redirect("../Authentication/NewAccount.aspx?Type=Google");
                    }
                    else
                    {
                        Response.Redirect("../Authentication/Accounts.aspx?Type=Email&Alert=This email address is already Registerd try loging in");
                    }
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
                        //go back to the previous page if there is one
                        goToPreviousPage();
                        Response.Redirect("../Default.aspx#page-top?" + "WB=" + reg.Split('|')[2]);
                    }
                    else if (result == "E")
                {
                    string EmpType = "";
                    try
                    {
                        EmpType = handler.getEmployeeType(reg.Split('|')[0]).Type.ToString().Replace(" ", string.Empty);
                    }
                    catch (NullReferenceException err)
                    {
                        function.logAnError("Error getting employe detais for user ID: "+ reg.Split('|')[0] +"\n "+err);
                        Response.Redirect("../Default.aspx?" + "WB=" + reg.Split('|')[2]);
                    }
                    if (EmpType == "R")
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

        private void goToPreviousPage()
        {
            //get the privious page to redirect to
            string PreviousPage = Request.QueryString["PreviousPage"];
            //go back to the previous page if there is one
            if (PreviousPage == "Help/CheveuxHelpCenter.aspx")
            {
                Response.Redirect("../Help/CheveuxHelpCenter.aspx#InternalHelp");
            }
            else if (PreviousPage == "BusinessSetting.aspx")
            {
                Response.Redirect("../Manager/BusinessSetting.aspx");
            }
            else if (PreviousPage == "Reports.aspx")
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
            else if (PreviousPage == "Bookings.aspx")
            {
                Response.Redirect("../Profile.aspx");
            }
            else if (PreviousPage == "MakeABooking")
            {
                Page.ClientScript.RegisterOnSubmitStatement(typeof(Page), "closePage", "window.onunload = CloseWindow();");
            }
        }

        protected void displayPassword(object sender, EventArgs e)
        {
            //check if the account exist
            try
            {
                if (auth.checkForAccountEmail(txtEmailUsername.Text.ToString().Replace(" ", string.Empty), false)
                    == true)
                {
                    lError.Visible = false;
                    //if the account exists hide the next button and show the password field and signin btn
                    divNext.Visible = false;
                    divPassword.Visible = true;
                    divSignIn.Visible = true;
                    txtEmailUsername.Attributes.Add("readonly", "readonly");
                }
                else
                {
                    //let the use know the account was not found
                    lError.Visible = true;
                    lError.Text = "Couldn't find your Cheveux Account";
                }
            }
            catch (Exception Err)
            {
                //let the use know an erorr ocoured
                lError.Visible = true;
                lError.Text = "An error occurred communicating with the Cheveux Server Please try again later";
                function.logAnError(Err.ToString());
            }
        }

        protected void signIn(object sender, EventArgs e)
        {
            //sign in the user
            //check if the credentials are correct
            string[] result = auth.AuthenticateEmail(txtEmailUsername.Text.ToString().Replace(" ", string.Empty),
                txtPassword.Text.ToString().Replace(" ", string.Empty));

            /*
                 * if the user deatails are incorect let the user know
                 */
            if (result[0].ToString().Replace(" ", string.Empty) == "Incorect")
            {
                //let the use know the account details were incorect
                lError.Visible = true;
                lError.Text = "Wrong password";
            }
            //if there was an error let the user know
            else if (result[0].ToString().Replace(" ", string.Empty) == "Error")
            {
                //let the use know ther was an error
                Response.Redirect("../Error.aspx?Error='A Error in when authenticating with the Cheveux sereve'");
            }
            //if the user details are coorect create a session cookie and return them to the previous or home page
            else if (result[1].ToString().Replace(" ", string.Empty) == "C" 
                || result[1].ToString().Replace(" ", string.Empty) == "E")
            {
                //remove the 'reg' cookie
                HttpCookie cookie = new HttpCookie("reg");
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
                //log the user in by creating a cookie to manage their state
                cookie = new HttpCookie("CheveuxUserID");
                // Set the user id in it.
                cookie["ID"] = result[0].ToString().Replace(" ", string.Empty);
                cookie["UT"] = result[1].ToString().Replace(" ", string.Empty);
                // Add it to the current web response.
                Response.Cookies.Add(cookie);

                //access control
                //send the user to the correct page based on their usertype
                if (result[1].Replace(" ", string.Empty) == "C")
                {
                    //go back to the previous page if there is one
                    goToPreviousPage();
                    Response.Redirect("../Default.aspx?" + "WB=" + result[2].ToString().Replace(" ", string.Empty));
                }
                else if (result[1].Replace(" ", string.Empty) == "E")
                {
                    string EmpType = handler.getEmployeeType(result[0].ToString().Replace(" ", string.Empty)).Type.Replace(" ", string.Empty);
                    if (EmpType == "R")
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
                        function.logAnError("Unknown user type found during login - User details (from Email Login):" +
                            result[1].ToString().Replace(" ", string.Empty));
                        Response.Redirect("../Default.aspx");
                    }
                }
                else
                {
                    Response.Redirect("../Default.aspx?" + "WB=" + result[2].Replace(" ", string.Empty));
                }
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            //get the privious page to redirect to
            string PreviousPage = Request.QueryString["PreviousPage"];
            if(PreviousPage != null)
            {
                Response.Redirect("../Authentication/Accounts.aspx?PreviousPage="+PreviousPage+"&Type=Email");
            }
            Response.Redirect("../Authentication/Accounts.aspx?Type=Email");
        }
    }
}