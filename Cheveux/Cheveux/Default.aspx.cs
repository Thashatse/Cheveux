using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Cheveux
{
    public partial class Default2 : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();

        protected void Page_Load(object sender, EventArgs e)
        {
            //access control
            HttpCookie UserID = Request.Cookies["CheveuxUserID"];
            //send the user to the correct page based on their usertype
            if (UserID != null)
            {
                string userType = UserID["UT"].ToString();
                if ("R" == userType)
                {
                    //Receptionist
                    Response.Redirect("Receptionist.aspx");
                }
                else if (userType == "M")
                {
                    //Manager
                    Response.Redirect("Manager.aspx");
                }
                else if (userType == "S")
                {
                    //stylist
                    Response.Redirect("Stylist.aspx");
                }
                else if (userType == "C")
                {
                    //customer
                    //allowed access to this page
                    //Response.Redirect("Default.aspx");

                }
                else
                {
                    Response.Redirect("Default.aspx");
                    function.logAnError("Unknown user type found during Loading of default.aspx: " +
                        UserID["UT"].ToString());
                }
            }

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
                        + "  You Are Now Register With Cheveux";
                    }
                }
            }
    }
    }