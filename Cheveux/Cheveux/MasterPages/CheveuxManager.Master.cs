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
    public partial class CheveuxManager : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBHandler handler = new DBHandler();

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
                USER UserDetails = handler.GetUserDetails(UserID["ID"]);
                if (UserDetails != null)
                {
                    profile.Controls.Add(new LiteralControl
                        ("<img src=" + UserDetails.UserImage + "" +
                        " alt='" + UserDetails.UserName.ToString() +
                        " Profile Image' width='75' height='75'/>" +
                        "   <a href='Profile.aspx'>" + UserDetails.UserName.ToString() + "</a>"));
                }
                else
                {
                    profile.Controls.Add(new LiteralControl
                        ("<a href='Profile.aspx'> User Profile </a>"));
                }
                LogedOut.Visible = false;
            }
        }

        public void btnSearchClickMasterPage(object sender, EventArgs e)
        {
            Response.Redirect("Search.aspx?ST=" + SearchBoxOnMasterPage.Text);
        }
    }
}