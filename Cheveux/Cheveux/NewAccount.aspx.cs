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
                Response.Redirect("Error.aspx");
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
            almostThere.Text = reg.Split('|')[2] + " We Are Almost There, Just One More Step To Complet Your Registration";
            userName.Attributes.Add("placeholder", (reg.Split('|')[1]).Split('@')[0]);
            Register.Visible = true;
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //check if the user has entered the nessary information
            if (userName.Text != "" && contactNumber.Text != "") {
                
                //get the user data from the cookie
                string reg = getRegCookie();
                //check if there was a error
                if (reg == "error")
                {
                    Response.Redirect("Error.aspx");
                }
                //create a new customer object
                reg = getRegCookie();
                CUSTOMER Cust = new CUSTOMER();
                string[] regArray = reg.Split('|');
                Cust.CustomerID = regArray[0];
                Cust.Email = regArray[1];
                Cust.FirstName = regArray[2];
                Cust.LastName = regArray[3];
                Cust.CustomerImage = regArray[4];
                Cust.UserName = userName.Text;
                Cust.ContactNo = contactNumber.Text;

                /*
                 * use the bll.NewUser to creat a new user
                 */
                bool result = auth.NewUser(Cust);

                if (result == true)
                {
                    //log the user in by creating a cookie to manage their state
                    HttpCookie cookie = new HttpCookie("CheveuxUserID");
                    // Set the user id in it.
                    cookie["ID"] = reg.Split('|')[0];
                    // Add it to the current web response.
                    Response.Cookies.Add(cookie);
                    //tell the user the registration was a success on the home page
                    Response.Redirect("Default.aspx?" + "NU=" + reg.Split('|')[2]);
                }
                else if (result == false)
                {
                    //open error page
                    Response.Redirect("Error.aspx");
                }
            } else
            {
                //let the user know they have to enter data
                    userNameErrorEmpty.Visible = true;
                    contactNumberErrorEmpty.Visible = true;
                RF.Visible = true;
            }
        }
    }
}