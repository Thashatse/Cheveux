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
        List<SP_GetServices> serviceList = null;
        List<SP_GetStylists> stylistList = null;
        List<SP_GetBookedTimes> bookedList = null;
        List<SP_GetSlotTimes> slotList = null;
        BOOKING book = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if the user is logged 
            try
            {
            cookie = Request.Cookies["CheveuxUserID"];
            serviceList = handler.BLL_GetAllServices();
            stylistList = handler.BLL_GetAllStylists();
            slotList = handler.BLL_GetAllTimeSlots();
            }
            catch (Exception err)
            {
                function.logAnError("unable to comunicate with the database on Make A Booking page: " +
                    err);
                BookingSummary.Text = "Database connection failed. Please try again later";
                divServices.Visible = false;


            }

            try
            {
                foreach (SP_GetServices services in serviceList)
                {
                    drpPickAService.DataSource = serviceList;
                    drpPickAService.DataTextField = "Name";
                    drpPickAService.DataValueField = "ServiceID";
                    drpPickAService.DataBind();
                }


            }
            catch(Exception err)
            {
                function.logAnError(err.ToString());
            }

            /**try
            {
                foreach(SP_GetServices services in serviceList)
                {
                    cblPickAService.DataSource = serviceList;
                    cblPickAService.DataTextField = "Name";
                    cblPickAService.DataValueField = "ServiceID";
                    cblPickAService.DataBind();
                }

                
            }
            catch
            {

            }**/

        }
 

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if(btnNext.Text == "Choose Hairstylist")
            {
                foreach (SP_GetStylists stylists in stylistList)
                {
                    rblPickAStylist.DataSource = stylistList;
                    rblPickAStylist.DataTextField = "FirstName";
                    rblPickAStylist.DataValueField = "UserID";
                    rblPickAStylist.DataBind();
                }
                HttpCookie cookie = new HttpCookie("booking");
                cookie["service"] = drpPickAService.SelectedValue.ToString();
                /**string selectedService = "";
                foreach(ListItem item in cblPickAService.Items)
                {
                    if (item.Selected)
                    {
                        selectedService = item.Value;
                    }
                }
                cookie["service"] = selectedService;**/
                Response.Cookies.Add(cookie);
                divServices.Visible = false;
                divStylist.Visible = true;
                btnPrevious.Visible = true;
                btnPrevious.Text = "Choose Service(s)";
                btnNext.Text = "Choose Date & Time";
                
            }
            else if (btnNext.Text == "Choose Date & Time")
            {
                HttpCookie cookie = Request.Cookies["booking"];
                cookie["stylist"] = rblPickAStylist.SelectedValue.ToString();
                Response.Cookies.Add(cookie);
                AvailableTimes.Visible = true;
                bookedList = handler.BLL_GetBookedStylistTimes(rblPickAStylist.SelectedValue.ToString(), calBooking.SelectedDate);
                if(bookedList != null)
                {
                    foreach(SP_GetBookedTimes booked in bookedList)
                    {
                        foreach(SP_GetSlotTimes times in slotList)
                        {
                            if(booked.SlotNo != times.SlotNo)
                            {
                                drpAvailableTimes.DataSource = slotList;
                                drpAvailableTimes.DataTextField = "Time";
                                drpAvailableTimes.DataValueField = "SlotNo";
                                drpAvailableTimes.DataBind();
                            }
                        }
                    }
                }
                else
                {
                    drpAvailableTimes.DataSource = slotList;
                    drpAvailableTimes.DataTextField = "Time";
                    drpAvailableTimes.DataValueField = "SlotNo";
                    drpAvailableTimes.DataBind();
                }
                
                
                 
                
                divStylist.Visible = false;
                divDateTime.Visible = true;
                btnPrevious.Visible = true;
                btnPrevious.Text = "Choose Hairstylist";
                btnNext.Text = "Booking Summary";
            }
            else if (btnNext.Text == "Booking Summary")
            {
                HttpCookie cookie = Request.Cookies["booking"];
                cookie["date"] = calBooking.SelectedDate.ToString();
                cookie["time"] = drpAvailableTimes.SelectedValue.ToString();
                Response.Cookies.Add(cookie);
                divDateTime.Visible = false;
                divSummary.Visible = true;

                lblServices.Text = drpPickAService.SelectedItem.Text.ToString();
                lblStylist.Text = rblPickAStylist.SelectedItem.Text.ToString();
                lblDate.Text = calBooking.SelectedDate.ToString("dd MMM yyyy");
                lblTime.Text = Convert.ToDateTime(drpAvailableTimes.SelectedItem.Text).ToString("hh:mm");

                btnPrevious.Visible = true;
                btnPrevious.Text = "Choose Date & Time";
                btnNext.Text = "Submit";
            }
            else if (btnNext.Text == "Submit")
            {
                //Make Booking
                try
                {
                    book = new BOOKING();

                    book.BookingID = function.GenerateRandomBookingID();
                    book.SlotNo = drpAvailableTimes.SelectedValue;
                    book.Date = calBooking.SelectedDate;
                    book.CustomerID = cookie["ID"];
                    book.ServiceID = drpPickAService.SelectedValue;
                    book.StylistID = rblPickAStylist.SelectedValue;
                    book.Available = "N";

                    handler.BLL_AddBooking(book);
                    Response.Redirect("Default.aspx");
                }
                catch (Exception err)
                {
                    function.logAnError(err.ToString());
                }


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