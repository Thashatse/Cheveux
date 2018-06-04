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
        HttpCookie cookie = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //access control
            //send the user to the correct page based on their usertype
            cookie = Request.Cookies["CheveuxUserID"];
            if (cookie != null)
            {
                string EmpType = cookie["UT"].ToString();
                if (EmpType == "R")
                {
                    //Receptionist
                    Response.Redirect("Receptionist.aspx");
                }
                else if (EmpType == "M")
                {
                    //Manager
                    Response.Redirect("Default.aspx");
                }
                else if (EmpType == "S")
                {
                    //stylist
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    function.logAnError("Unknown user type found during Loading of default.aspx: " +
                        cookie["UT"].ToString());
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