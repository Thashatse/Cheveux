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

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private string getRegCookie()
        {
            //get the user data from the cookie
            HttpCookie cookie = Request.Cookies["reg"];
            string reg = "error";
            if (reg != null)
            {
                reg = cookie.Value;
            }
            return reg;
        }

        protected void btnAuthenticate_Click(object sender, EventArgs e)
        {
            //get the user data from the cookie
            string reg = getRegCookie();
            //check if there was a error
            if(reg == "error")
            {
                //open error page
            }

            /*
             * use the bll.authenticate class to see if the user exist are ready or needs to register 
             * as a new user
             */
            string result = auth.Authenticate(reg);
            /*
             * if the user is unregistered get the info requered and create a new user, 
             * using the bll.authenticate class
             */
            if (result == "unRegUser")
            {
                //hide the login form
                Login.Visible = false;
                //show the register form
                almostThere.Text = reg.Split('|')[2] + " We Are Almost There, Just One More Step To Complet Your Registration";
                userName.Attributes.Add("placeholder", (reg.Split('|')[1]).Split('@')[0]);
                Register.Visible = true;
            }
            //if the user exists create a session cookie and return them to the previous or home page
            else if (result == "RegUser")
            {
                //log the user in by creating a cookie to manage their state
                HttpCookie cookie = new HttpCookie("CheveuxUserID");
                // Set the user id in it.
                cookie["ID"] = reg.Split('|')[0];
                // Add it to the current web response.
                Response.Cookies.Add(cookie);
                Response.Redirect("Default.aspx?" + reg.Split('|')[4]);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //get the user data from the cookie
            string reg = getRegCookie()+"|"+userName.Text+"|"+contactNumber.Text;
            //check if there was a error
            if (reg == "error")
            {
                //open error page
            }
            /*
             * use the bll.NewUser to creat a new user
             */
            bool result = auth.NewUser(reg);
            if (result == true)
            {
                //tell the user the registration was a success
                //"Congradulations "+ reg.Split('|')[2] +"  You Are Now Register With Cheveux"
                
                //log the user in by creating a cookie to manage their state
                HttpCookie cookie = new HttpCookie("CheveuxUserID");
                // Set the user id in it.
                cookie["ID"] = reg.Split('|')[0];
                // Add it to the current web response.
                Response.Cookies.Add(cookie);
                Response.Redirect("Default.aspx");
            }
            else if (result == false)
            {
                //open error page
            }
        }

        }
}