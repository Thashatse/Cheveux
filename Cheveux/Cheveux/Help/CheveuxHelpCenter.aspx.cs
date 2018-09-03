using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cheveux.Help
{
    public partial class CheveuxHelpCenter : System.Web.UI.Page
    {
        HttpCookie cookie = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //access control
            cookie = Request.Cookies["CheveuxUserID"];
            //display internal help based on user type
            if (cookie != null)
            {
                string userType = cookie["UT"].ToString();
                if("R" == userType || "M" == userType || "S" == userType)
                {
                    LogedIn.Visible = true;
                    liInternalHelp.Visible = true;
                }
            }
        }
    }
}