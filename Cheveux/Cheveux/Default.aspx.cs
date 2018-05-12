using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cheveux
{
    public partial class Default2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //welcome back existing users
            String name = Request.QueryString["WB"];
            if (name != null)
            {
                Welcome.Text = "Welcom Back To Cheveux " + name;
            }
            else
            {
                //welcome new customers
                name = Request.QueryString["NU"];
                if (name != null)
                {
                    Welcome.Text = "Congradulations " + name
                    +"  You Are Now Register With Cheveux";
                }
            }
        }
    }
}