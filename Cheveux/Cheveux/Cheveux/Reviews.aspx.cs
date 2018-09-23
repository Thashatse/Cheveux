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
                this.MasterPageFile = "~/MasterPages/Cheveux.Master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            cookie = Request.Cookies["CheveuxUserID"];
            action = Request.QueryString["Action"];
            if (cookie == null)
            {
                if(action == "MakeAreview")
                {
                    phLogin.Visible = true;
                    phMain.Visible = false;
                }
                else
                {
                    phLogin.Visible = false;
                    phMain.Visible = true;
                    
                    OpeningHeader.Text = "Behind every review is an experience that matters";
                    btnRev.Visible = true;
                    readReviews.Visible = true;
                    makeAreview.Visible = false;
                }
            }
            else if (cookie["UT"] == "C")
            {
                phMain.Visible = true;
                phLogin.Visible = false;

                if (action == "ReadReviews")
                {
                    OpeningHeader.Text = "Reviews";
                    btnRev.Visible = true;
                    readReviews.Visible = true;
                    makeAreview.Visible = false;
                }
                else if (action == "MakeAreview")
                {
                    OpeningHeader.Text = "Write A Review";
                    readReviews.Visible = false;
                    btnRev.Visible = false;
                    makeAreview.Visible = true;
                }
                
            }
            else if (cookie["UT"] == "M")
            {
                phMain.Visible = true;
                phLogin.Visible = false;
                if (action == "ReadReviews")
                {
                    OpeningHeader.Text = "Customer Reviews";
                    readReviews.Visible = true;
                    btnRev.Visible = false;
                    makeAreview.Visible = false;
                }
            }
        }
        public void displayPastBookings(DateTime day,string customerID)
        {
            //////
        }
        #region Events
        protected void calDay_SelectionChanged(object sender, EventArgs e)
        {
            lsBksHeader.InnerText = "Your Booking(s) on "
                                + calDay.SelectedDate.ToString("dd-MM-yyyy");
        }
        protected void drpReadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpReadType.SelectedValue == "1")//view my reviews
            {
                cookie = Request.Cookies["CheveuxUserID"];
                if (cookie == null)
                {
                    phLogin.Visible = true;
                    phMain.Visible = false;
                }
            }
        }

        protected void drpReviewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpReviewType.SelectedValue == "0")//review booking
            {
                datepick.Visible = true;
            }
            else if (drpReviewType.SelectedValue == "1")//review stylist
            {
                datepick.Visible = false;
            }
        }
        protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int month = Convert.ToInt16(drpMonth.SelectedValue);
            calDay.VisibleDate = new DateTime(2018, month,
                                    1);
        }
        protected void cal_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date > DateTime.Today)
            {
                e.Day.IsSelectable = false;
            }
            if (e.Day.Date.CompareTo(DateTime.Today) > 0)
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = System.Drawing.Color.LightGray;
            }
        }
        #endregion


    }
}