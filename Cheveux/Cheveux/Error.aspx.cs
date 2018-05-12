using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cheveux
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //format the page
            ErrorHeader.Font.Bold = true;
            ErrorHeader.Font.Underline = true;
            ErrorHeader.Font.Size = 21;
            Error1.Font.Size = 14;

            //chech if and error detail has been sent by querystring
            String error = Request.QueryString["Error"];
            HttpCookie ErrorCookie = Request.Cookies["Err"];
            if (error != null)
            {
                //else dislay the error header and errod detail
                ErrorHeader.Text = "We Are Sorry, The Following Error Ocoured";
                Error1.Text = error;
            } else if (ErrorCookie != null)
            { 
                ErrorHeader.Text = "We Are Sorry, The Following Error Ocoured";
                Error1.Text = ErrorCookie["Err1"];
            }
            else
            {
                //if not display generic error page
                ErrorHeader.Text = "We Are Sorry, An Unknown Error Ocoured";
            }
        }
    }
}