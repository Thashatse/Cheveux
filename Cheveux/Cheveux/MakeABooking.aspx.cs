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
    public partial class MakeABooking : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;
        List<SP_GetServices> getAllServices = null;
        List<SP_GetStylists> getStylists = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if the user is logged in
            cookie = Request.Cookies["CheveuxUserID"];

        }
        public void btnStylist(object sender, EventArgs e)
        {
           
        }
    }
}