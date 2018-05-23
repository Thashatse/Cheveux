using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cheveux
{
    public partial class Bookings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is loged in and display past and futcher bokings
            HttpCookie cookie = Request.Cookies["CheveuxUserID"];
            if (cookie == null)
            {
                //if the user is not loged in do not diplay and futer services
                Tabs.Visible = false;
            }
            if (cookie != null)
            {
                //if the user is loged in diplay and futer services
                JumbotronLogedIn.Visible = true;
                JumbotronLogedOut.Visible = false;
            }
        }
    }
}