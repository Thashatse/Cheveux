using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;

namespace Cheveux
{
    public partial class CheveuxReceptionist : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBHandler handler = new DBHandler();
            Functions function = new Functions();

            HttpCookie UserID = Request.Cookies["CheveuxUserID"];
            if (UserID == null)
            {
                // display sign in buton & Hide The Accounts Button
                LogedIn.Visible = false;

            }
            else
            {
                // display the user profile & hide the sign in button
                //get the user details to display the username & image on the profile button
                USER UserDetails = null;
                try
                {
                    UserDetails = handler.GetUserDetails(UserID["ID"]);
                }
                catch (Exception err)
                {
                    function.logAnError("Error geting user details for profile buton in master page Error: " + err);
                }
                if (UserDetails != null)
                {
                    profile.Controls.Add(new LiteralControl
                        ("<a class='nav-link js-scroll-trigger dropdown-toggle' data-toggle='dropdown' href='../Profile.aspx?View=Profile' style='text-decoration: none;'>" +
                                "<img src=" + UserDetails.UserImage + "" +
                                " width='35' height='35' style='border-radius:50%;'/> " +
                                UserDetails.UserName.ToString() + "</a>" +
                                "<div class='dropdown-menu'>" +
                                    "<a class='dropdown-item' href='../Profile.aspx?View=Profile'> Profile </a>" +
                                    "<a class='dropdown-item' href='/Authentication/Accounts.aspx?action=Logout'> Logout </a>" +
                                "</div>"));
                }
                else
                {

                    profile.Controls.Add(new LiteralControl
                        ("<a href='../Profile.aspx' style='text-decoration: none;'> User Profile </a>"));
                }
                LogedOut.Visible = false;
            }
        }
    }
}
