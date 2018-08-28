using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cheveux.Manager
{
    public partial class UpdateService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Access Control
            HttpCookie cookie = Request.Cookies["CheveuxUserID"];
            if (cookie == null)
            {
                Response.Redirect("../Manager/Service.aspx");
            }
            else if (cookie["UT"] != "M")
            {
                Response.Redirect("../Default.aspx");
            }
            else if (cookie["UT"] == "M")
            {
                //manager is allowed access
            }
            #endregion
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}