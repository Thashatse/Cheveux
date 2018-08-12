using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cheveux
{
    public partial class MakeInternalBooking : System.Web.UI.Page
    {
        HttpCookie cookie = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Access Control
            cookie = Request.Cookies["CheveuxUserID"];
            if (cookie == null)
            {
                Response.Redirect("../Receptionist/Receptionist.aspx");
            }
            else if (cookie["UT"] != "R")
            {
                Response.Redirect("../Receptionist/Receptionist.aspx");
            }
            #endregion
        }

        protected void btnSelectCustomer_Click(object sender, EventArgs e)
        {
            divSelectStyle.Visible = false;
            divSelectUser.Visible = true;
        }

        protected void btnComfirmation_Click(object sender, EventArgs e)
        {
            divSelectUser.Visible = false;
            divSummary.Visible = true;
        }

        protected void btnMakeBooking_Click(object sender, EventArgs e)
        {

        }
    }
}