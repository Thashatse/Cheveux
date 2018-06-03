using System;
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

        private string getRegCookie()
        {
            //get the user data from the cookie
            HttpCookie cookie = Request.Cookies["reg"];
            if (cookie == null)
            {
                //open error page
                Response.Redirect("Error.aspx?Error='A Error in when authenticating with google'");
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
            string reg = getRegCookie();
            almostThere.Text = reg.Split('|')[2] + " We Are Almost There, Just One More Step To Complete Your Registration";
            userName.Attributes.Add("placeholder", (reg.Split('|')[1]).Split('@')[0]);
            Register.Visible = true;
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
                //get the user data from the cookie
                string reg = getRegCookie();
                //check if there was a error
                if (reg == "error")
                {
                    Response.Redirect("Error.aspx");
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
                User.ContactNo = contactNumber.Text;

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
                    Response.Redirect("Error.aspx?Error='" + err + "'");
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
                    Response.Redirect("Default.aspx?" + "NU=" + reg.Split('|')[2]);
                }
                else if (result == false)
                {
                    //open error page
                    Response.Redirect("Error.aspx?Error='A Error in when authenticating with the Cheveux sereve'");
                }
        }
    }
}