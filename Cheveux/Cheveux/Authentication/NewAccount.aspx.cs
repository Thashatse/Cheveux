﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.Models;

//create a new user, using the bll.authenticate class
namespace Cheveux
{
    public partial class NewAccount : System.Web.UI.Page
    {
        Authentication auth = new Authentication();
        Functions function = new Functions();

        private string getRegCookie()
        {
            //get the user data from the cookie
            HttpCookie cookie = Request.Cookies["reg"];
            if (cookie == null)
            {
                //open error page
                Response.Redirect("../Error.aspx?Error='A Error in when authenticating with google'");
            }
            string reg = "error";
            if (reg != null)
            {
                reg = cookie.Value;
            }
            return reg;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //check what type of registration needs to take place
            string type = Request.QueryString["Type"];
            if (type == null || type == "" || type == string.Empty)
            {
                Response.Redirect("../Authentication/Accounts.aspx");
            }
            else if (type == "Google")
            {
                try
                {
                    string reg = getRegCookie();
                    almostThere.Text = "Hey "+reg.Split('|')[2] + " you are new here, just one more step to complete your registration";
                    userName.Attributes.Add("placeholder", (reg.Split('|')[1]).Split('@')[0]);
                    RegisterGoogleUser.Visible = true;
                }
                catch (Exception Err)
                {
                    function.logAnError("Error occoured geting RegCookie in Page_Load(object sender, EventArgs e) on NewAccounts page" + Err.ToString());
                    //open error page
                    Response.Redirect("../Error.aspx?Error='A Error in when authenticating with google'");
                }
            }
            else if (type == "Email")
            {
                registeEmailUser.Visible = true;
            }
        }
        
        protected void btnSubmitGoogle_Click(object sender, EventArgs e)
        {
            if (auth.checkForAccountEmail(userName.Text.ToString().Replace(" ", string.Empty), true)
                    == false)
            {
                //get the user data from the cookie
                string reg = getRegCookie();
                //check if there was a error
                if (reg == "error")
                {
                    Response.Redirect("../Error.aspx");
                }
                //create a new user object
                reg = getRegCookie();
                USER User = new USER();
                string[] regArray = reg.Split('|');
                User.UserID = regArray[0];
                User.Email = regArray[1];
                User.FirstName = regArray[2];
                User.LastName = regArray[3];
                User.UserImage = regArray[4];
                User.UserName = userName.Text;
                if (txtContactNumber.Text.ToString() != "")
                {
                    User.ContactNo = txtContactNumber.Text.ToString().Replace(" ", string.Empty);
                }
                else
                {
                    User.ContactNo = null;
                }
                User.AccountType = regArray[5];
                User.Password = null;

                /*
                 * use the bll.NewUser to creat a new user
                 */
                bool result = false;
                try
                {
                    result = auth.NewUser(User);
                }
                catch (ApplicationException err)
                {
                    Response.Redirect("../Error.aspx?Error='" + err + "'");
                }

                if (result == true)
                {
                    //log the user in by creating a cookie to manage their state
                    HttpCookie cookie = new HttpCookie("CheveuxUserID");
                    // Set the user id in it.
                    cookie["ID"] = reg.Split('|')[0];
                    cookie["UT"] = "C";
                    // Add it to the current web response.
                    Response.Cookies.Add(cookie);
                    //go back to the previous page or the home page by default
                    String PreviousPage = Request.QueryString["PreviousPage"];
                    if (PreviousPage != null)
                    {
                        Response.Redirect(PreviousPage);
                    }
                    //tell the user the registration was a success on the home page
                    Response.Redirect("../Default.aspx?" + "NU=" + reg.Split('|')[2]);
                }
                else if (result == false)
                {
                    //open error page
                    Response.Redirect("../Error.aspx?Error='A Error in when authenticating with the Cheveux sereve'");
                }
            }
        }

        protected void btnSubmitEmail_Click(object sender, EventArgs e)
        {
            if (auth.checkForAccountEmail(txtUsername.Text.ToString().Replace(" ", string.Empty), true)
                    == false && auth.checkForAccountEmail(txtEmailAddress.Text.ToString().Replace(" ", string.Empty), true)
                    == false)
            {

                USER User = new USER();
                string userID = auth.GenerateRandomUserID();
                User.UserID = userID.ToString().Replace(" ", string.Empty);
                User.Email = txtEmailAddress.Text.ToString().Replace(" ", string.Empty);
                User.FirstName = txtFirstName.Text.ToString().Replace(" ", string.Empty);
                User.LastName = txtLastName.Text.ToString().Replace(" ", string.Empty);
                User.UserImage = null;
                User.UserName = txtUsername.Text.ToString().Replace(" ", string.Empty);
                if (txtContactNumber.Text.ToString() != "")
                {
                    User.ContactNo = txtContactNumber.Text.ToString().Replace(" ", string.Empty);
                }
                else
                {
                    User.ContactNo = null;
                }
                User.AccountType = "Email";
                User.Password = txtPassword.Text.ToString().Replace(" ", string.Empty);
                User.UserImage = null;

                /*
                 * use the bll.NewUser to creat a new user
                */

                bool result = false;
                try
                {
                    result = auth.NewUser(User);
                }
                catch (ApplicationException err)
                {
                    Response.Redirect("../Error.aspx?Error='" + err + "'");
                }

                if (result == true)
                {
                    //log the user in by creating a cookie to manage their state
                    HttpCookie cookie = new HttpCookie("CheveuxUserID");
                    // Set the user id in it.
                    cookie["ID"] = userID;
                    cookie["UT"] = "C";
                    // Add it to the current web response.
                    Response.Cookies.Add(cookie);
                    //go back to the previous page or the home page by default
                    String PreviousPage = Request.QueryString["PreviousPage"];
                    if (PreviousPage != null)
                    {
                        Response.Redirect(PreviousPage);
                    }
                    //tell the user the registration was a success on the home page
                    Response.Redirect("../Default.aspx?" + "NU=" + txtFirstName.Text.ToString().Replace(" ", string.Empty));
                }
                else if (result == false)
                {
                    //open error page
                    Response.Redirect("../Error.aspx?Error='A Error in when authenticating with the Cheveux sereve'");
                }
            }
            else
            {
                txtUsername_TextChanged(sender, e);
                txtEmailAddress_TextChanged(sender, e);
            }
        }

        protected void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (auth.checkForAccountEmail(txtUsername.Text.ToString().Replace(" ", string.Empty), true)
                    == true)
            {
                //if the account exists tell the user to try a difreting username
                LUsernmaeExists.Visible = true;
                LUsernmaeExists.Text = "Sorry, That Username is taken. Try Another one";
            }
            else
            {
                LUsernmaeExists.Visible = false;
            }
        }

        protected void txtEmailAddress_TextChanged(object sender, EventArgs e)
        {
            if (auth.checkForAccountEmail(txtEmailAddress.Text.ToString().Replace(" ", string.Empty), true)
                    == true)
            {
                //if the account exists tell the user to try a difreting username
                LEmailExists.Visible = true;
                LEmailExists.Text = "This email address is already registerd, <a href='../Authentication/Accounts.aspx'>Try Loging in?</a>";
            }
            else
            {
                LEmailExists.Visible = false;
            }
        }

        protected void userName_TextChanged(object sender, EventArgs e)
        {
            if (auth.checkForAccountEmail(userName.Text.ToString().Replace(" ", string.Empty), true)
                    == true)
            {
                //if the account exists tell the user to try a difreting username
               LGoogleUsernameExists.Visible = true;
                LGoogleUsernameExists.Text = "Sorry, That Username is taken. Try Another one";
            }
            else
            {
                LGoogleUsernameExists.Visible = false;
            }
        }
    }
}