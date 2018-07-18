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
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if the user is logged in
            cookie = Request.Cookies["CheveuxUserID"];
        }
 

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if(btnNext.Text == "Choose Hairstylist")
            {
                divServices.Visible = false;
                divStylist.Visible = true;
                btnPrevious.Visible = true;
                btnPrevious.Text = "Choose Service(s)";
                btnNext.Text = "Choose Date & Time";
            }
            else if (btnNext.Text == "Choose Date & Time")
            {
                divStylist.Visible = false;
                divDateTime.Visible = true;
               
                btnPrevious.Visible = true;
                btnPrevious.Text = "Choose Hairstylist";
                btnNext.Text = "Booking Summary";
            }
            else if (btnNext.Text == "Booking Summary")
            {
                divDateTime.Visible = false;
                divSummary.Visible = true;
                
                btnPrevious.Visible = true;
                btnPrevious.Text = "Choose Date & Time";
                btnNext.Text = "Submit";
            }
            else if (btnNext.Text == "Submit")
            {
                //Make Booking
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            if (btnPrevious.Text == "Choose Service(s)")
            {
                divServices.Visible = true;
                divStylist.Visible = false;
                btnPrevious.Visible = false;
                btnNext.Text = "Choose Hairstylist";
            }
            else if (btnPrevious.Text == "Choose Hairstylist")
            {
                divStylist.Visible = true;
                divDateTime.Visible = false;

                btnPrevious.Visible = true;
                btnPrevious.Text = "Choose Service(s)";
                btnNext.Text = "Choose Date & Time";
            }
            else if (btnPrevious.Text == "Choose Date & Time")
            {
                divDateTime.Visible = true;
                divSummary.Visible = false;

                btnPrevious.Visible = true;
                btnPrevious.Text = "Choose Hairstylist";
                btnNext.Text = "Choose Date & Time";
            }
      
        }
    }
}