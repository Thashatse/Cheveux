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
    public partial class MakeInternalBooking : System.Web.UI.Page
    {
        HttpCookie cookie = null;
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        string customerID = null;
        string serviceID = null;
        string stylisID = null;
        DateTime date;
        string slotID = null;

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

            #region load Query string & Cookie
            stylisID = Request.QueryString["StyID"];
            date = Convert.ToDateTime(Request.QueryString["Date"]);
            slotID = Request.QueryString["Time"];
            if (stylisID != null &&
                date != null && 
                slotID != null)
            {
                if (!Page.IsPostBack)
                {
                    //save booking details in a cookie
                    //remove the old booking cookie
                    cookie = new HttpCookie("CheveuxBooking");
                    cookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(cookie);
                    //log the user in by creating a cookie to manage their state
                    cookie = new HttpCookie("CheveuxBooking");
                    // Set the user id in it.
                    cookie["StyID"] = stylisID;
                    cookie["date"] = date.ToString();
                    cookie["SlotID"] = slotID;
                    cookie["Style"] = null;
                    cookie["CustID"] = null;
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    cookie = Request.Cookies["CheveuxBooking"];
                    date = Convert.ToDateTime(cookie["date"].ToString());
                    slotID = cookie["SlotID"].ToString();
                    stylisID = cookie["Style"].ToString();
                    if (cookie["CustID"].ToString() != "")
                    {
                        customerID = cookie["CustID"].ToString();
                    }
                    if (cookie["StyID"].ToString() != "")
                    {
                        stylisID = cookie["StyID"].ToString();
                    }
                }
            }
            else
            {
                Response.Redirect("Receptionist.aspx");
            }
            #endregion

            #region Load Summary
            try
            {
                #region summury header
                if (stylisID != null)
                {
                    lSummary.Text = "Booking with " + handler.GetUserDetails(stylisID).FirstName;
                }

                if (customerID != null & stylisID != null)
                {
                    lSummary.Text = "Booking with " + handler.GetUserDetails(stylisID).FirstName
                        + ", For " + handler.GetUserDetails(customerID).FirstName;
                }
                #endregion

                #region service list box
                if (serviceID != null)
                {
                    string[] services = serviceID.Split('|');
                    foreach (string service in services)
                    {
                        //add services to list box
                        //lBservicestoBook.Items.Add();
                    }
                }
                #endregion

                #region date and time
                if (date != null && slotID != null)
                {
                    //get time slot
                    lDateandTime.Text = date.ToString("dd MMM yyyy");
                }
                #endregion
            }
            catch (Exception err)
            {
                function.logAnError("error loading summary on make a internal booking page error: " + err);
                lSummary.Text = "An Error Occurred Loading Summary";
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
        }

        protected void btnMakeBooking_Click(object sender, EventArgs e)
        {

        }

        protected void txtStyleSearch_DataBinding(object sender, EventArgs e)
        {
            //search for stylist
        }

        protected void txtCustomer_DataBinding(object sender, EventArgs e)
        {
            //search for customer
        }

        protected void btnAddServiceToBooking_Click(object sender, EventArgs e)
        {
            //add the service
        }
    }
}