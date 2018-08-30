using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.Models;

namespace Cheveux.Authentication
{
    public partial class CloseWindow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBHandler handler = new DBHandler();
            HttpCookie UserID = Request.Cookies["CheveuxUserID"];
            if (UserID == null)
            {
                // redirect to log in
                Response.Redirect("Accounts.aspx");

            }
            else
            {
                // display the user profile
                //get the user details to display the username & image on the profile button
                USER UserDetails = handler.GetUserDetails(UserID["ID"]);
                if (UserDetails != null)
                {
                    profile.Controls.Add(new LiteralControl
                        ("<img src=" + UserDetails.UserImage + "" +
                        " alt='" + UserDetails.UserName.ToString() +
                        " Profile Image' width='50' height='50'/>" 
                        + UserDetails.FirstName.ToString()
                        + " "+ UserDetails.LastName.ToString()));
                }
                else
                {
                    // redirect to error page
                    Response.Redirect("../Error.aspx");
                }
            }
        }
    }
}