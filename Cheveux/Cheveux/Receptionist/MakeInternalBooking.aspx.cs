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
        string style = null;
        DateTime date;
        string slotID = null;
        List<string> CustomerIDs = new List<string>();

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
                    updateCookie(null, null);
                }
                else
                {
                    cookie = Request.Cookies["CheveuxBooking"];
                    date = Convert.ToDateTime(cookie["date"].ToString());
                    slotID = cookie["SlotID"].ToString();
                    stylisID = cookie["StyID"].ToString();
                    if (cookie["CustID"].ToString() != "")
                    {
                        customerID = cookie["CustID"].ToString();
                    }
                    if (cookie["Style"].ToString() != "")
                    {
                        stylisID = cookie["Style"].ToString();
                    }
                }
            }
            else
            {
                Response.Redirect("Receptionist.aspx");
            }
            #endregion

            #region Load Summary
            loadSummary();
            #endregion

            #region load Customers
            if (!Page.IsPostBack)
            {
                loadCustomerList();
            }
            #endregion
        }

        #region booking Cookie
        private void updateCookie(string addcustID, string addServiceID)
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
            if (addServiceID != null)
            {
                cookie["Style"] = addServiceID;
            }
            else
            {
                cookie["Style"] = null;

            }
            if (addcustID != null)
            {
                cookie["CustID"] = addcustID;
            }
            else
            {
                cookie["CustID"] = null;

            }
            Response.Cookies.Add(cookie);
        }
        #endregion

        #region Views
        protected void btnSelectCustomer_Click(object sender, EventArgs e)
        {
            divSelectStyle.Visible = false;
            btnAddServiceToBooking.Visible = false;
            divSelectUser.Visible = true;
            btnAddCustomerToBooking.Visible = true;
        }

        protected void btnComfirmation_Click(object sender, EventArgs e)
        {
            divSelectUser.Visible = false;
            btnAddCustomerToBooking.Visible = false;
        }
        #endregion

        #region  booking
        protected void btnMakeBooking_Click(object sender, EventArgs e)
        {
            //make the booking
        }
        #endregion

        #region Summary
        public void loadSummary()
        {
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
        }
        #endregion

        #region Service
        protected void btnAddServiceToBooking_Click(object sender, EventArgs e)
        {
            //add the service
        }
        #endregion

        #region Customer
        //CustomerList
        private void loadCustomerList()
        {
            lbCustomers.Items.Clear();
            //add all customers to the list
            try
            {
                List<SP_UserList> customers = handler.userList();
                int customerCount = 0;
                if (customers.Count != 0)
                {
                    //sort the Customers by alphabetical oder
                    customers = customers.OrderBy(o => o.FullName).ToList();
                    //add customers
                    foreach (SP_UserList customer in customers)
                    {
                        //make sure there is stock
                        if (customer.userType == 'C'
                            && (function.compareToSearchTerm(customer.FullName, txtCustomerSearch.Text) == true
                            || function.compareToSearchTerm(customer.Email, txtCustomerSearch.Text) == true
                            || function.compareToSearchTerm(customer.ContactNo, txtCustomerSearch.Text) == true
                            || function.compareToSearchTerm(customer.UserName, txtCustomerSearch.Text) == true))
                        {
                            lbCustomers.Items.Add(customer.FullName.ToString());
                            customerCount++;
                        }
                    }

                    //if no products found matching the criteria
                    if (customerCount == 0)
                    {
                        lbCustomers.Items.Add("No Customers Found");
                    }
                }
            }
            catch (Exception err)
            {
                lbCustomers.Items.Clear();
                lbCustomers.Items.Add("Error Loading Customers, Try Again Later");
                function.logAnError("Error Loading Customer on MakeInternalBooking | Error: " + err);
            }
        }

        //productIDS
        private void loadCustomerID()
        {
            try
            {
                List<SP_UserList> customers = handler.userList();
                if (customers.Count != 0)
                {
                    //sort the Customers by alphabetical oder
                    customers = customers.OrderBy(o => o.FullName).ToList();
                    //add customers ids to array
                    foreach (SP_UserList customer in customers)
                    {
                        //make sure there is stock
                        if (customer.userType == 'C'
                            && (function.compareToSearchTerm(customer.FullName, txtCustomerSearch.Text) == true
                            || function.compareToSearchTerm(customer.Email, txtCustomerSearch.Text) == true
                            || function.compareToSearchTerm(customer.ContactNo, txtCustomerSearch.Text) == true
                            || function.compareToSearchTerm(customer.UserName, txtCustomerSearch.Text) == true))
                        {
                            CustomerIDs.Add(customer.UserID.ToString());
                        }
                    }
                }
            }
            catch (Exception err)
            {
                lbCustomers.Items.Clear();
                lbCustomers.Items.Add("Error adding customer, Try Again Later");
                function.logAnError("Error Loading Customer IDs on MakeInternalBooking | Error: " + err);
            }
        }

        //add customer to booking
        protected void lbCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //a product to ivoice
            if (lbCustomers.SelectedIndex >= 0)
            {
                //add the customer to the booking
                loadCustomerID();
                updateCookie(CustomerIDs[lbCustomers.SelectedIndex], serviceID);
                loadSummary();
            }
            else
            {
                loadCustomerList();
                loadSummary();
            }
        }

        protected void txtCustomerSearch_DataBinding(object sender, EventArgs e)
        {
            loadCustomerList();
        }
        #endregion

        protected void txtStyleSearch_DataBinding(object sender, EventArgs e)
        {
            //search for stylist
        }
    }
}