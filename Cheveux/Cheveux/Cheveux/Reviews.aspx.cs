using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;

namespace Cheveux
{
    public partial class Reviews : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;
        string action = null;
        protected void Page_PreInit(Object sender, EventArgs e)
        {
            //check the cheveux user id cookie for the user
            HttpCookie cookie = Request.Cookies["CheveuxUserID"];
            char userType;
            //check if the cookie is empty or not
            if (cookie != null)
            {
                //store the user Type in a variable and display the appropriate master page for the user
                userType = cookie["UT"].ToString()[0];
                //if customer
                if (userType == 'C')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/Cheveux.Master";
                }
                //if receptionist
                else if (userType == 'R')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/CheveuxReceptionist.Master";
                }
                //if stylist
                else if (userType == 'S')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/CheveuxStylist.Master";
                }
                //if Manager
                else if (userType == 'M')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/CheveuxManager.Master";
                }
                //default
                else
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/CheveuxManager.Master";
                }
            }
            //set the default master page if the cookie is empty
            else
            {
                //set the master page
                this.MasterPageFile = "~/MasterPages/CheveuxManager.Master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            cookie = Request.Cookies["CheveuxUserID"];
            action = Request.QueryString["Action"];
            if (cookie == null)
            {
                if(action== "MyReviews" || action == "ReviewBooking" ||
                    action == "ReviewStylist")
                {
                    phLogin.Visible = true;
                    phMain.Visible = false;
                }
                else
                {
                    phMain.Visible = true;
                    phLogin.Visible = false;
                }
            }
            else if (cookie["UT"] == "C")
            {
                phMain.Visible = true;
                phLogin.Visible = false;

                if(action == "MyReviews")
                {
                    OpeningHeader.Text = "My Reviews";
                }
                else if (action == "ReadReviews")
                {
                    OpeningHeader.Text = "Behind every review is an experience that matters";
                }
                else if (action == "ReviewBooking")
                {
                    OpeningHeader.Text = "Review Booking";
                }
                else if (action == "ReviewStylist")
                {
                    OpeningHeader.Text = "Review Stylist";
                }
                
            }
            else if (cookie["UT"] == "M")
            {
                phMain.Visible = true;
                phLogin.Visible = false;
                if (action == "ReadReviews")
                {
                    OpeningHeader.Text = "Read Customer Reviews";
                }
            }
        }
    }
}