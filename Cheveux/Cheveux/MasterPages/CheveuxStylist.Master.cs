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
    public partial class Site2 : System.Web.UI.MasterPage
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
                        ("<li class='dropdown'>" +
                            "<a class='dropdown-toggle' data-toggle='dropdown' href='#'>" +
                                "<img src=" + UserDetails.UserImage + "" +
                                " alt='" + UserDetails.UserName.ToString() +
                                " Profile Image' width='35' height='35' style='border-radius:50%;'/>" +
                                UserDetails.UserName.ToString() +
                                "<span class='caret'></span></a>" +
                                "<ul class='dropdown-menu bg-dark text-white'>" +
                                    "<li>&nbsp;<a href='../Profile.aspx?View=Profile'> Profile </a>&nbsp;</li> " +
                                    "<li>&nbsp;<a href='/Authentication/Accounts.aspx?action=Logout'> Logout </a>&nbsp;</li> " +
                                "</ul>" +
                        "</li> &nbsp; &nbsp;"));
                }
                else
                {
                    profile.Controls.Add(new LiteralControl
                        ("<a href='../Profile.aspx'> User Profile </a>"));
                }
                LogedOut.Visible = false;
            }
        }
    }
}
