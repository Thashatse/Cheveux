using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.Models;

namespace Cheveux
{
    public partial class Accounts : System.Web.UI.Page
    {
        Authentication auth = new Authentication();
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        USER restPassAccount;
        string code = null;

        protected void Page_Load(object sender, EventArgs e)
        {
                Parallel.Invoke(() => loadPage(), () => function.sendOGBkngNoti());
        }

        private void loadPage()
        {
            lHeader.Text = "<h2>Get started with<b> Cheveux</b></h2>";
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
            else if (action == "Reset")
            {
                lHeader.Text = "<h2><b> Cheveux</b></h2>";
                //check if the user has requested to reset thier passowrd or request a reset code
                code = Request.QueryString["code"];
                if (code == null)
                {
                    //Display the rest email Form
                    divEmailAcount.Visible = false;
                    divAccountType.Visible = false;
                    divGetRestCode.Visible = true;
                    divGetEmailToReset.Visible = true;
                }
                else
                {
                    //Display the rest Pasword Form
                    string[] codeArray = code.Split('|');
                    DateTime codeGenerate = DateTime.Now.AddYears(-10);
                    try
                    {
                        codeGenerate = Convert.ToDateTime(codeArray[0].ToString() + " " + codeArray[1].ToString());
                    }
                    catch (Exception err)
                    {
                        function.logAnError("Bad Pass word reset code: "+ code +". Error: "+err);
                        //if the code has expierd
                        //Display the rest email Form
                        lEamailResetError.Text = "The Rest Request Has Expired, Try Again.";
                        lEamailResetError.Visible = true;
                        divEmailAcount.Visible = false;
                        divAccountType.Visible = false;
                        divGetRestCode.Visible = true;
                        divGetEmailToReset.Visible = true;
                    }
                        if (codeGenerate.AddMinutes(10) >= DateTime.Now)
                    {
                        //check if the account exist and the code is valid
                        restPassAccount = handler.GetAccountForRestCode(code);
                        if (restPassAccount != null)
                        {
                            divEmailAcount.Visible = false;
                        divAccountType.Visible = false;
                        divResetPasword.Visible = true;
                        divResetPaswordtxtPass.Visible = true;
                            //get the user name and display it in the label
                            lPaswordResetUsernameLable.Text = restPassAccount.UserName.ToString();
                        }
                        else
                        {
                            //if the code has expierd
                            //Display the rest email Form
                            lEamailResetError.Text = "The Rest Request Has Expired, Try Again.";
                            lEamailResetError.Visible = true;
                            divEmailAcount.Visible = false;
                            divAccountType.Visible = false;
                            divGetRestCode.Visible = true;
                            divGetEmailToReset.Visible = true;
                        }
                    }
                    else
                    {
                        //if the code has expierd
                        //Display the rest email Form
                        lEamailResetError.Text = "The Rest Request Has Expired, Try Again.";
                        lEamailResetError.Visible = true;
                        divEmailAcount.Visible = false;
                        divAccountType.Visible = false;
                        divGetRestCode.Visible = true;
                        divGetEmailToReset.Visible = true;
                    }
                }
            }
            else if (action == "ChangePass")
            {
                lHeader.Text = "<h2> Change Password </h2>";
                try
                {
                    HttpCookie UserID = Request.Cookies["CheveuxUserID"];
                    divEmailAcount.Visible = false;
                    divAccountType.Visible = false;
                    divexistingPass.Visible = true;
                    divResetPasword.Visible = true;
                    divResetPaswordtxtPass.Visible = true;
                    //get the user name and display it in the label
                    lPaswordResetUsernameLable.Text = handler.GetUserDetails(UserID["ID"]).UserName.ToString();
                }
                catch (Exception err)
                {
                    Response.Redirect("Accounts.aspx?PreviousPage=Profile.aspx");
                }
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
                if (alert != null || alert != "")
                {
                    lError.Visible = true;
                    lError.Text = alert;
                }
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

        #region Google Auth
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
                            Response.Redirect("../Receptionist/Receptionist.aspx?WB=True");
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
                            Response.Redirect("../Stylist/Stylist.aspx?WB=True");
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
        #endregion

        #region Email Auth
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
            if (result[0].ToString().Replace(" ", string.Empty) == "Error")
            {
                //let the use know the account details were incorect
                lError.Visible = true;
                lError.Text = "Wrong password";
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
                        Response.Redirect("../Receptionist/Receptionist.aspx?WB=True");
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
                        Response.Redirect("../Stylist/Stylist.aspx?WB=True");
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
        #endregion

        #region Email Pass Change/Rest
        protected void btnRestPassword_Click(object sender, EventArgs e)
        {
            if (btnRestPassword.Text == "Rest Pasword")
            {
                //check if the account exist
                try
                {
                    if (auth.checkForAccountEmail(txtEmailToReset.Text.ToString().Replace(" ", string.Empty), false)
                        == true)
                    {
                        lEamailResetError.Visible = false;
                        //Create Reset Code and email to user
                        string code = DateTime.Now.ToString("yyyy/MM/dd|HH:mm") + "|"+auth.generatePassRestCode();
                        bool success = handler.createRestCode(txtEmailToReset.Text.ToString(), code);
                        if (success == true)
                        {
                            var body = new System.Text.StringBuilder();
                            body.AppendFormat("Hello User,");
                            body.AppendLine(@"");
                            body.AppendLine(@"To rest your password follow the link --> http://sict-iis.nmmu.ac.za/beauxdebut/Authentication/Accounts.aspx?action=Reset&code="+code);
                            body.AppendLine(@"");
                            body.AppendLine(@"Regards,");
                            body.AppendLine(@"The Cheveux Team");
                            success = function.sendEmailAlert(txtEmailToReset.Text.ToString(), "Cheveux User",
                                "Password Rest",
                                body.ToString(),
                                "Accounts Cheveux");
                            //let the user know the password was succefuly rest
                            lPaswordRestCodeFeedback.Visible = true;
                            lPaswordRestCodeFeedback.Text = "A Password Rest Link Has Been Sent To Your Email";
                            divGetEmailToReset.Visible = false;
                            btnRestPassword.Text = "Done";
                            if (success == false)
                            {
                                //let the use know the proccess fails
                                lEamailResetError.Visible = true;
                                lEamailResetError.Text = "An Error Occurred, Please Try Again Later";
                            }
                        }
                        else if(success == false)
                        {
                            //let the use know the proccess fails
                            lEamailResetError.Visible = true;
                            lEamailResetError.Text = "An Error Occurred, Please Try Again Later";
                        }
                    }
                    else
                    {
                        //let the use know the account was not found
                        lEamailResetError.Visible = true;
                        lEamailResetError.Text = "Couldn't find your Cheveux Account";
                    }
                }
                catch (Exception Err)
                {
                    //let the use know an erorr ocoured
                    lPaswordRestCodeFeedback.Visible = true;
                    lPaswordRestCodeFeedback.Text = "An error occurred communicating with the Cheveux Server Please try again later";
                    function.logAnError("Error checking for account when requestin rest password on accounts page" +
                        Err.ToString());
                }
            }
            else if (btnRestPassword.Text == "Done")
            {
                Response.Redirect("../Default.aspx");
            }
        }

        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            if (btnChangePass.Text == "Change Pasword" 
                && divexistingPass.Visible == false)
            {
                    try
                    {
                    //re set the password
                    bool success = handler.updateUserAccountPassword(txtNewPasword.Text.ToString(), restPassAccount.UserID.ToString());
                    if (success == true)
                    {
                        //send confermation email
                        var body = new System.Text.StringBuilder();
                        body.AppendFormat("Hello User,");
                        body.AppendLine(@"");
                        body.AppendLine(@"Your Password Was Succesfuly Rest.");
                        body.AppendLine(@"");
                        body.AppendLine(@"Make a Booking Now --> http://sict-iis.nmmu.ac.za/beauxdebut/MakeABooking.aspx.");
                        body.AppendLine(@"");
                        body.AppendLine(@"Regards,");
                        body.AppendLine(@"The Cheveux Team");
                        success = function.sendEmailAlert(handler.GetUserDetails(restPassAccount.UserID.ToString()).Email.ToString(), "Cheveux User",
                            "Password Rest Succesful",
                            body.ToString(),
                            "Accounts Cheveux");
                        //let the user know the password was succefuly rest
                        lPaswordResetUsernameLable.Visible = true;
                        lPaswordResetUsernameLable.Text = "Your Password Has Successfully Been Reset";
                        divResetPaswordtxtPass.Visible = false;
                        btnChangePass.Text = "Done";
                    }
                    else
                    {
                        //let the user know the password was succefuly rest
                        function.logAnError("Error reseting password on accounts page for reset code: " + code);
                        lPaswordResetUsernameLable.Visible = true;
                        lPaswordResetUsernameLable.Text = "An error occurred, Please try again later.";
                        divResetPaswordtxtPass.Visible = false;
                        btnChangePass.Text = "Done";
                    }
                    }
                    catch (Exception Err)
                    {
                        //let the use know an erorr ocoured
                        lPaswordResetUsernameLable.Visible = true;
                        lPaswordResetUsernameLable.Text = "An error occurred communicating with the Cheveux Server, Please try again later.";
                        function.logAnError("Error reseting password on accounts page for reset code: " + code +
                            Err.ToString());
                    }
            }
            else if (btnChangePass.Text == "Change Pasword"
                && divexistingPass.Visible == true)
                {
                HttpCookie UserID = Request.Cookies["CheveuxUserID"];
                USER user = handler.GetUserDetails(UserID["ID"]);
                try
                    {
                    //check if the credentials are correct
                    string[] result = auth.AuthenticateEmail(user.UserName.ToString().Replace(" ", string.Empty),
                        txtExistingPassword.Text.ToString().Replace(" ", string.Empty));

                    /*
                         * if the user deatails are incorect let the user know
                         */
                    if (result[0].ToString().Replace(" ", string.Empty) == "Error")
                    {
                        //let the use know the account details were incorect
                        wrongExsistingPass.Visible = true;
                        wrongExsistingPass.Text = "Wrong password";
                    }
                    //if the user details are corect change the password
                    else if (result[1].ToString().Replace(" ", string.Empty) == "C"
                        || result[1].ToString().Replace(" ", string.Empty) == "E")
                    {
                        //re set the password
                        bool success = handler.updateUserAccountPassword(txtNewPasword.Text.ToString().Replace(" ", string.Empty),
                            UserID["ID"].ToString().Replace(" ", string.Empty));
                        if (success == true)
                        {
                            //send confermation email
                            var body = new System.Text.StringBuilder();
                            body.AppendFormat("Hello" + user.FirstName+",");
                            body.AppendLine(@"");
                            body.AppendLine(@"Your Password Was Succesfuly Changed.");
                            body.AppendLine(@"");
                            body.AppendLine(@"Make a Booking Now --> http://sict-iis.nmmu.ac.za/beauxdebut/MakeABooking.aspx.");
                            body.AppendLine(@"");
                            body.AppendLine(@"Regards,");
                            body.AppendLine(@"The Cheveux Team");
                            success = function.sendEmailAlert(user.Email.ToString(), "Cheveux User",
                                "Password Changed",
                                body.ToString(),
                                "Accounts Cheveux");
                            //let the user know the password was succefuly rest
                            lPaswordResetUsernameLable.Visible = true;
                            lPaswordResetUsernameLable.Text = "Your Password Has Successfully Been Changed";
                            divResetPaswordtxtPass.Visible = false;
                            divexistingPass.Visible = false;
                            btnChangePass.Text = "Done";
                        }
                        else
                        {
                            //let the user know the password was succefuly rest
                            function.logAnError("Error changeing password for username: " + user.UserName.ToString());
                            lPaswordResetUsernameLable.Visible = true;
                            lPaswordResetUsernameLable.Text = "An error occurred, Please try again later.";
                            divResetPaswordtxtPass.Visible = false;
                            divexistingPass.Visible = false;
                            btnChangePass.Text = "Done";
                        }
                    }
                    }
                    catch (Exception Err)
                    {
                        //let the use know an erorr ocoured
                        lPaswordResetUsernameLable.Visible = true;
                        lPaswordResetUsernameLable.Text = "An error occurred communicating with the Cheveux Server, Please try again later.";
                    function.logAnError("Error changeing password for username: " + user.UserName.ToString() +
                    Err.ToString());
                    }
                }
                else if (btnChangePass.Text == "Done" && txtExistingPassword.Text == null)
            {
                Response.Redirect("../Authentication/Accounts.aspx?Type=Email");
            }
            else if (btnChangePass.Text == "Done" && txtExistingPassword.Text != null)
            {
                Response.Redirect("../Profile.aspx");
            }
        }
        #endregion
    }
}