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
            if (cookie != null)
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
                Response.Redirect("Error.aspx?Error='A Error in when authenticating with google'");
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
            catch (ApplicationException)
            {
                throw;
                //HttpCookie ErrorCookie = new HttpCookie("Err");
                //ErrorCookie["Err1"] = err.ToString();
                //Response.Cookies.Add(ErrorCookie);
                //Response.Redirect("Error.aspx");
            }
            /*
             * if the user is unregistered get the info requered and create a new user, 
             * using the new account page
             */
            if (result == "unRegUser")
            {
                //Open the new account page
                Response.Redirect("NewAccount.aspx");
            }
            //if the user exists create a session cookie and return them to the previous or home page
            else if (result == "C" || result == "E")
            {
                HttpCookie cookie = Request.Cookies["reg"];
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
                //log the user in by creating a cookie to manage their state
                cookie = new HttpCookie("CheveuxUserID");
                // Set the user id in it.
                cookie["ID"] = reg.Split('|')[0];
                cookie["UT"] = result.ToString();
                // Add it to the current web response.
                Response.Cookies.Add(cookie);
                Response.Redirect("Default.aspx?"+"WB="+reg.Split('|')[2]);
            }else if (result == "Error")
            {
                Response.Redirect("Error.aspx?Error='A Error in when authenticating with the Cheveux sereve'");
            }
        }

  

        }
}