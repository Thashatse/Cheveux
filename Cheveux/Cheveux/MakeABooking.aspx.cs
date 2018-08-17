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
        HttpCookie bookingTime = null;
        List<SP_GetServices> serviceList = null;
        List<SP_GetStylists> stylistList = null;
        List<SP_GetBookedTimes> bookedList = null;
        List<SP_GetSlotTimes> slotList = null;
        BOOKING book = null;
        string[,] availableTimes = new string[21,2];
        List<string> pickedServiceName = null;
        List<string> pickedServiceID = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            #region access control
            HttpCookie Authcookie = Request.Cookies["CheveuxUserID"];
            //access control
            if (Authcookie != null)
            {
                if(Authcookie["UT"].ToString()[0] == 'C')
                {
                    //customer is aloud on mage
                }
                else if (Authcookie["UT"].ToString()[0] == 'R')
                {
                    //Redirect the user as they are not aloud to make bookings
                    Response.Redirect("Error.aspx?Error='Receptionists are not able to make bookings, Login as a customer and try again.'");
                }
                //if stylist
                else if (Authcookie["UT"].ToString()[0] == 'S')
                {
                    //Redirect the user as they are not aloud to make bookings
                    Response.Redirect("Error.aspx?Error='Stylist are not able to make bookings, Login as a customer and try again.'");
                }
                //if Manager
                else if (Authcookie["UT"].ToString()[0] == 'M')
                {
                    //Redirect the user as they are not aloud to make bookings
                    Response.Redirect("Error.aspx?Error='Managers are not able to make bookings, Login as a customer and try again.'");
                }
                //default
                else
                {
                    //Redirect the user as they are not aloud to make bookings
                    Response.Redirect("Error.aspx?Error='An error occurred, please try again later.'");
                }
            }
            #endregion

            bookingTime = new HttpCookie("BookTime");

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
                lblBookingSummary.Text = "Database connection failed. Please try again later";
                divServices.Visible = false;


            }

            try
            {
                if (!Page.IsPostBack)
                {
                    ListItem deselect = new ListItem("None", "0");
                    deselect.Selected = true;
                    rblPickAServiceA.Items.Add(deselect);
                    rblPickAServiceB.Items.Add(deselect);

                    foreach (SP_GetServices services in serviceList)
                    {

                        if (services.ServiceType == 'N')
                        {
                            ListItem item;
                            item = new ListItem(services.Name, services.ServiceID);
                            cblPickAServiceN.Items.Add(item);
                        }
                        else if (services.ServiceType == 'A')
                        {
                            ListItem item;
                            item = new ListItem(services.Name, services.ServiceID);
                            rblPickAServiceA.Items.Add(item);
                        }
                        else if (services.ServiceType == 'B')
                        {
                            ListItem item;
                            item = new ListItem(services.Name, services.ServiceID);
                            rblPickAServiceB.Items.Add(item);
                        }

                    }

                }
            }
            catch (Exception err)
            {
                function.logAnError(err.ToString());
            }

        }
 
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (btnNext.Text == "Choose Hairstylist")
            {
                foreach (SP_GetStylists stylists in stylistList)
                {
                    rblPickAStylist.DataSource = stylistList;
                    rblPickAStylist.DataTextField = "FirstName";
                    rblPickAStylist.DataValueField = "UserID";
                    rblPickAStylist.DataBind();
                }


                
                if ((cblPickAServiceN.SelectedValue.ToString() == "") && (rblPickAServiceA.SelectedValue.ToString() == "0") && (rblPickAServiceB.SelectedValue.ToString() == "0"))
                {
                    lblBookingSummary.Visible = true;
                    lblBookingSummary.Text = "Please select a service(s) before moving to the next step!";
                    divServices.Visible = true;
                }
                else
                {
                    lblBookingSummary.Visible = false;
                    divServices.Visible = false;
                    divStylist.Visible = true;
                    btnPrevious.Visible = true;
                    btnPrevious.Text = "Choose Service(s)";
                    btnNext.Text = "Choose Date & Time";
                }
            }
            else if (btnNext.Text == "Choose Date & Time")
            {           
                if(rblPickAStylist.SelectedValue.ToString() == "")
                {
                    lblBookingSummary.Visible = true;
                    lblBookingSummary.Text = "Please select a hairstylist before moving to the next step!";
                    divStylist.Visible = true;
                }
                else 
                {
                    divStylist.Visible = false;
                    divDateTime.Visible = true;
                    btnPrevious.Visible = true;
                    btnPrevious.Text = "Choose Hairstylist";
                    btnNext.Text = "Booking Summary";
                }
                
            }
            else if (btnNext.Text == "Booking Summary")
            {
                //BookingSummary.Text = BookingSummary.Text + " for: " + calBooking.SelectedDate.ToString() + " " + bookedTime;
                divDateTime.Visible = false;
                divSummary.Visible = true;

                if(cblPickAServiceN.SelectedValue != null)
                {
                    lblServices.Text = cblPickAServiceN.SelectedItem.ToString();
                }
                else if(rblPickAServiceB.SelectedValue != null)
                {
                    lblServices.Text = rblPickAServiceB.SelectedItem.ToString();
                }
                else if(rblPickAServiceA.SelectedValue != null)
                {
                    lblServices.Text = rblPickAServiceA.SelectedItem.ToString();
                }
                lblStylist.Text = rblPickAStylist.SelectedItem.Text.ToString();
                lblDate.Text = calBooking.SelectedDate.ToString("dd MMM yyyy");
                HttpCookie bookingTime = Request.Cookies["BookTime"];
                lblTime.Text = bookingTime["Time"];
                btnPrevious.Visible = true;

                #region access control
                HttpCookie Authcookie = Request.Cookies["CheveuxUserID"];
                if (Authcookie != null)
                {
                    if (Authcookie["UT"].ToString()[0] == 'C')
                    {
                        btnPrevious.Text = "Choose Date & Time";
                        btnNext.Text = "Submit";
                    }
                    else
                    {
                        btnPrevious.Text = "Choose Date & Time";
                        btnNext.Text = "Login";
                    }
                }
                else
                {
                    btnPrevious.Text = "Choose Date & Time";
                    btnNext.Text = "Login";
                }
                #endregion
            }
            #region access control
            else if (btnNext.Text == "Login")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/Authentication/Accounts.aspx?PreviousPage=MakeABooking','_newtab');", true);
                btnPrevious.Text = "Choose Date & Time";
                btnNext.Text = "Submit";
            }
            #endregion
            else if (btnNext.Text == "Submit")
            {
                btnPrevious.Visible = true;
                //access control
                HttpCookie Authcookie = Request.Cookies["CheveuxUserID"];
                if (Authcookie != null)
                {
                    if (Authcookie["UT"].ToString()[0] == 'C') {
                        //Make Booking
                        try
                        {
                           
                            book = new BOOKING();
                            book.BookingID = function.GenerateRandomBookingID();
                            HttpCookie bookingTime = Request.Cookies["BookTime"];
                            book.SlotNo = bookingTime["TimeSlot"];
                            book.Date = calBooking.SelectedDate;
                            book.CustomerID = cookie["ID"];
                            if(cblPickAServiceN.SelectedValue != null)
                            {
                                book.ServiceID = cblPickAServiceN.SelectedValue.ToString();
                            }
                            else if(rblPickAServiceB.SelectedValue != null)
                            {
                                book.ServiceID = rblPickAServiceB.SelectedValue.ToString();
                            }
                            else
                            {
                                book.ServiceID = rblPickAServiceA.SelectedValue.ToString();
                            }
                            book.StylistID = rblPickAStylist.SelectedValue;
                            book.Available = "N";
                            handler.BLL_AddBooking(book);
                            USER user = handler.GetUserDetails(cookie["ID"]);
                            //send an email notification
                            var body = new System.Text.StringBuilder();
                            body.AppendFormat("Hello " + user.FirstName.ToString() + ",");
                            body.AppendLine(@"");
                            body.AppendLine(@"Your booking is with " + rblPickAStylist.SelectedItem.Text.ToString() + " on " + calBooking.SelectedDate.ToString("dd MMM yyyy") + " at " + Convert.ToDateTime(bookingTime["Time"]).ToString("HH:mm") + ".");
                            body.AppendLine(@"Your booking is for " + lblServices.Text.ToString() + ".");
                            body.AppendLine(@"");
                            body.AppendLine(@"View or change your booking details here: http://sict-iis.nmmu.ac.za/beauxdebut/Profile.aspx.");
                            body.AppendLine(@"");
                            body.AppendLine(@"Regards,");
                            body.AppendLine(@"");
                            body.AppendLine(@"The Cheveux Team");
                            function.sendEmailAlert(user.Email, user.FirstName +" " +user.LastName,
                                "Booking Confirmation",
                                body.ToString(),
                                "Bookings Cheveux");
                            //redirect and confirm booking
                            Response.Redirect("Default.aspx?BS=True&Sty="+book.StylistID+"&D="+book.Date+"&T="+book.SlotNo);
                        }
                        catch (Exception err)
                        {
                            function.logAnError(err.ToString());
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/Authentication/Accounts.aspx?PreviousPage=MakeABooking','_newtab');", true);
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

        protected void calBooking_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date.CompareTo(DateTime.Today) < 0)
            {
                e.Day.IsSelectable = false;
            }
        }

        protected void calBooking_SelectionChanged(object sender, EventArgs e)
        {
            int morningButtonCount = 1;
            int afternoonButtonCount = 11;
            bookedList = handler.BLL_GetBookedStylistTimes(rblPickAStylist.SelectedValue.ToString(), calBooking.SelectedDate);
            HideButtons();
            foreach (SP_GetSlotTimes times in slotList)
            {
                if (bookedList.Count != 0)
                {
                    foreach (SP_GetBookedTimes booked in bookedList)
                    {
                        if (booked.SlotNo != times.SlotNo)
                        {
                            if (times.Time > Convert.ToDateTime("12:00"))
                            {
                                if (afternoonButtonCount == 11)
                                {
                                    btnAfternoon11.Visible = true;
                                    btnAfternoon11.Text = times.Time.ToString("HH:mm");
                                    availableTimes[11, 0] = times.SlotNo;
                                    availableTimes[11, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (afternoonButtonCount == 12)
                                {
                                    btnAfternoon12.Visible = true;
                                    btnAfternoon12.Text = times.Time.ToString("HH:mm");
                                    availableTimes[12, 0] = times.SlotNo;
                                    availableTimes[12, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (afternoonButtonCount == 13)
                                {
                                    btnAfternoon13.Visible = true;
                                    btnAfternoon13.Text = times.Time.ToString("HH:mm");
                                    availableTimes[13, 0] = times.SlotNo;
                                    availableTimes[13, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (afternoonButtonCount == 14)
                                {
                                    btnAfternoon14.Visible = true;
                                    btnAfternoon14.Text = times.Time.ToString("HH:mm");
                                    availableTimes[14, 0] = times.SlotNo;
                                    availableTimes[14, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (afternoonButtonCount == 15)
                                {
                                    btnAfternoon15.Visible = true;
                                    btnAfternoon15.Text = times.Time.ToString("HH:mm");
                                    availableTimes[15, 0] = times.SlotNo;
                                    availableTimes[15, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (afternoonButtonCount == 16)
                                {
                                    btnAfternoon16.Visible = true;
                                    btnAfternoon16.Text = times.Time.ToString("HH:mm");
                                    availableTimes[16, 0] = times.SlotNo;
                                    availableTimes[16, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (afternoonButtonCount == 17)
                                {
                                    btnAfternoon17.Visible = true;
                                    btnAfternoon17.Text = times.Time.ToString("HH:mm");
                                    availableTimes[17, 0] = times.SlotNo;
                                    availableTimes[17, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (afternoonButtonCount == 18)
                                {
                                    btnAfternoon18.Visible = true;
                                    btnAfternoon18.Text = times.Time.ToString("HH:mm");
                                    availableTimes[18, 0] = times.SlotNo;
                                    availableTimes[18, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (afternoonButtonCount == 19)
                                {
                                    btnAfternoon19.Visible = true;
                                    btnAfternoon19.Text = times.Time.ToString("HH:mm");
                                    availableTimes[19, 0] = times.SlotNo;
                                    availableTimes[19, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (afternoonButtonCount == 20)
                                {
                                    btnAfternoon20.Visible = true;
                                    btnAfternoon20.Text = times.Time.ToString("HH:mm");
                                    availableTimes[20, 0] = times.SlotNo;
                                    availableTimes[20, 1] = times.Time.ToString("HH:mm");
                                }
                                afternoonButtonCount++;
                            }
                            else
                            {
                                if (morningButtonCount == 1)
                                {
                                    btnMorning1.Visible = true;
                                    btnMorning1.Text = times.Time.ToString("HH:mm");
                                    availableTimes[1, 0] = times.SlotNo;
                                    availableTimes[1, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (morningButtonCount == 2)
                                {
                                    btnMorning2.Visible = true;
                                    btnMorning2.Text = times.Time.ToString("HH:mm");
                                    availableTimes[2, 0] = times.SlotNo;
                                    availableTimes[2, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (morningButtonCount == 3)
                                {
                                    btnMorning3.Visible = true;
                                    btnMorning3.Text = times.Time.ToString("HH:mm");
                                    availableTimes[3, 0] = times.SlotNo;
                                    availableTimes[3, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (morningButtonCount == 4)
                                {
                                    btnMorning4.Visible = true;
                                    btnMorning4.Text = times.Time.ToString("HH:mm");
                                    availableTimes[4, 0] = times.SlotNo;
                                    availableTimes[4, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (morningButtonCount == 5)
                                {
                                    btnMorning5.Visible = true;
                                    btnMorning5.Text = times.Time.ToString("HH:mm");
                                    availableTimes[5, 0] = times.SlotNo;
                                    availableTimes[5, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (morningButtonCount == 6)
                                {
                                    btnMorning6.Visible = true;
                                    btnMorning6.Text = times.Time.ToString("HH:mm");
                                    availableTimes[6, 0] = times.SlotNo;
                                    availableTimes[6, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (morningButtonCount == 7)
                                {
                                    btnMorning7.Visible = true;
                                    btnMorning7.Text = times.Time.ToString("HH:mm");
                                    availableTimes[7, 0] = times.SlotNo;
                                    availableTimes[7, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (morningButtonCount == 8)
                                {
                                    btnMorning8.Visible = true;
                                    btnMorning8.Text = times.Time.ToString("HH:mm");
                                    availableTimes[8, 0] = times.SlotNo;
                                    availableTimes[8, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (morningButtonCount == 9)
                                {
                                    btnMorning9.Visible = true;
                                    btnMorning9.Text = times.Time.ToString("HH:mm");
                                    availableTimes[9, 0] = times.SlotNo;
                                    availableTimes[9, 1] = times.Time.ToString("HH:mm");
                                }
                                else if (morningButtonCount == 10)
                                {
                                    btnMorning10.Visible = true;
                                    btnMorning10.Text = times.Time.ToString("HH:mm");
                                    availableTimes[10, 0] = times.SlotNo;
                                    availableTimes[10, 1] = times.Time.ToString("HH:mm");
                                }
                                morningButtonCount++;
                            }

                        }
                    }
                }
                else
                {
                    if (times.Time > Convert.ToDateTime("12:00"))
                    {
                        if (afternoonButtonCount == 11)
                        {
                            btnAfternoon11.Visible = true;
                            btnAfternoon11.Text = times.Time.ToString("HH:mm");
                            availableTimes[11, 0] = times.SlotNo;
                            availableTimes[11, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (afternoonButtonCount == 12)
                        {
                            btnAfternoon12.Visible = true;
                            btnAfternoon12.Text = times.Time.ToString("HH:mm");
                            availableTimes[12, 0] = times.SlotNo;
                            availableTimes[12, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (afternoonButtonCount == 13)
                        {
                            btnAfternoon13.Visible = true;
                            btnAfternoon13.Text = times.Time.ToString("HH:mm");
                            availableTimes[13, 0] = times.SlotNo;
                            availableTimes[13, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (afternoonButtonCount == 14)
                        {
                            btnAfternoon14.Visible = true;
                            btnAfternoon14.Text = times.Time.ToString("HH:mm");
                            availableTimes[14, 0] = times.SlotNo;
                            availableTimes[14, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (afternoonButtonCount == 15)
                        {
                            btnAfternoon15.Visible = true;
                            btnAfternoon15.Text = times.Time.ToString("HH:mm");
                            availableTimes[15, 0] = times.SlotNo;
                            availableTimes[15, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (afternoonButtonCount == 16)
                        {
                            btnAfternoon16.Visible = true;
                            btnAfternoon16.Text = times.Time.ToString("HH:mm");
                            availableTimes[16, 0] = times.SlotNo;
                            availableTimes[16, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (afternoonButtonCount == 17)
                        {
                            btnAfternoon17.Visible = true;
                            btnAfternoon17.Text = times.Time.ToString("HH:mm");
                            availableTimes[17, 0] = times.SlotNo;
                            availableTimes[17, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (afternoonButtonCount == 18)
                        {
                            btnAfternoon18.Visible = true;
                            btnAfternoon18.Text = times.Time.ToString("HH:mm");
                            availableTimes[18, 0] = times.SlotNo;
                            availableTimes[18, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (afternoonButtonCount == 19)
                        {
                            btnAfternoon19.Visible = true;
                            btnAfternoon19.Text = times.Time.ToString("HH:mm");
                            availableTimes[19, 0] = times.SlotNo;
                            availableTimes[19, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (afternoonButtonCount == 20)
                        {
                            btnAfternoon20.Visible = true;
                            btnAfternoon20.Text = times.Time.ToString("HH:mm");
                            availableTimes[20, 0] = times.SlotNo;
                            availableTimes[20, 1] = times.Time.ToString("HH:mm");
                        }
                        afternoonButtonCount++;
                    }
                    else
                    {
                        if (morningButtonCount == 1)
                        {
                            btnMorning1.Visible = true;
                            btnMorning1.Text = times.Time.ToString("HH:mm");
                            availableTimes[1, 0] = times.SlotNo;
                            availableTimes[1, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (morningButtonCount == 2)
                        {
                            btnMorning2.Visible = true;
                            btnMorning2.Text = times.Time.ToString("HH:mm");
                            availableTimes[2, 0] = times.SlotNo;
                            availableTimes[2, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (morningButtonCount == 3)
                        {
                            btnMorning3.Visible = true;
                            btnMorning3.Text = times.Time.ToString("HH:mm");
                            availableTimes[3, 0] = times.SlotNo;
                            availableTimes[3, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (morningButtonCount == 4)
                        {
                            btnMorning4.Visible = true;
                            btnMorning4.Text = times.Time.ToString("HH:mm");
                            availableTimes[4, 0] = times.SlotNo;
                            availableTimes[4, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (morningButtonCount == 5)
                        {
                            btnMorning5.Visible = true;
                            btnMorning5.Text = times.Time.ToString("HH:mm");
                            availableTimes[5, 0] = times.SlotNo;
                            availableTimes[5, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (morningButtonCount == 6)
                        {
                            btnMorning6.Visible = true;
                            btnMorning6.Text = times.Time.ToString("HH:mm");
                            availableTimes[6, 0] = times.SlotNo;
                            availableTimes[6, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (morningButtonCount == 7)
                        {
                            btnMorning7.Visible = true;
                            btnMorning7.Text = times.Time.ToString("HH:mm");
                            availableTimes[7, 0] = times.SlotNo;
                            availableTimes[7, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (morningButtonCount == 8)
                        {
                            btnMorning8.Visible = true;
                            btnMorning8.Text = times.Time.ToString("HH:mm");
                            availableTimes[8, 0] = times.SlotNo;
                            availableTimes[8, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (morningButtonCount == 9)
                        {
                            btnMorning9.Visible = true;
                            btnMorning9.Text = times.Time.ToString("HH:mm");
                            availableTimes[9, 0] = times.SlotNo;
                            availableTimes[9, 1] = times.Time.ToString("HH:mm");
                        }
                        else if (morningButtonCount == 10)
                        {
                            btnMorning10.Visible = true;
                            btnMorning10.Text = times.Time.ToString("HH:mm");
                            availableTimes[10, 0] = times.SlotNo;
                            availableTimes[10, 1] = times.Time.ToString("HH:mm");
                        }
                        morningButtonCount++;
                    }
                }
            }
        }
        private void deselectButton()
        {
            btnAfternoon11.CssClass = "btn btn-light";
            btnAfternoon12.CssClass = "btn btn-light";
            btnAfternoon13.CssClass = "btn btn-light";
            btnAfternoon14.CssClass = "btn btn-light";
            btnAfternoon15.CssClass = "btn btn-light";
            btnAfternoon16.CssClass = "btn btn-light";
            btnAfternoon17.CssClass = "btn btn-light";
            btnAfternoon18.CssClass = "btn btn-light";
            btnAfternoon19.CssClass = "btn btn-light";
            btnAfternoon20.CssClass = "btn btn-light";
            btnMorning1.CssClass = "btn btn-light";
            btnMorning2.CssClass = "btn btn-light";
            btnMorning3.CssClass = "btn btn-light";
            btnMorning4.CssClass = "btn btn-light";
            btnMorning5.CssClass = "btn btn-light";
            btnMorning6.CssClass = "btn btn-light";
            btnMorning7.CssClass = "btn btn-light";
            btnMorning8.CssClass = "btn btn-light";
            btnMorning9.CssClass = "btn btn-light";
            btnMorning10.CssClass = "btn btn-light";
        }
        private void HideButtons()
        {
            btnAfternoon11.Visible = false;
            btnAfternoon12.Visible = false;
            btnAfternoon13.Visible = false;
            btnAfternoon14.Visible = false;
            btnAfternoon15.Visible = false;
            btnAfternoon16.Visible = false;
            btnAfternoon17.Visible = false;
            btnAfternoon18.Visible = false;
            btnAfternoon19.Visible = false;
            btnAfternoon20.Visible = false;
            btnMorning1.Visible = false;
            btnMorning2.Visible = false;
            btnMorning3.Visible = false;
            btnMorning4.Visible = false;
            btnMorning5.Visible = false;
            btnMorning6.Visible = false;
            btnMorning7.Visible = false;
            btnMorning8.Visible = false;
            btnMorning9.Visible = false;
            btnMorning10.Visible = false;
        }
        protected void btnAfternoon11_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[11, 0];
            bookingTime["Time"] = availableTimes[11, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon11.CssClass = "btn btn-primary";
        }

        protected void btnMorning1_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[1, 0];
            bookingTime["Time"] = availableTimes[1, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning1.CssClass = "btn btn-primary";
        }

        protected void btnMorning2_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[2, 0];
            bookingTime["Time"] = availableTimes[2, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning2.CssClass = "btn btn-primary";
        }

        protected void btnMorning3_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[3, 0];
            bookingTime["Time"] = availableTimes[3, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning3.CssClass = "btn btn-primary";
        }

        protected void btnMorning4_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[4, 0];
            bookingTime["Time"] = availableTimes[4, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning4.CssClass = "btn btn-primary";
        }

        protected void btnMorning5_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[5, 0];
            bookingTime["Time"] = availableTimes[5, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning5.CssClass = "btn btn-primary";
        }

        protected void btnMorning6_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[6, 0];
            bookingTime["Time"] = availableTimes[6, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning6.CssClass = "btn btn-primary";
        }

        protected void btnMorning7_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[7, 0];
            bookingTime["Time"] = availableTimes[7, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning7.CssClass = "btn btn-primary";
        }

        protected void btnMorning8_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[8, 0];
            bookingTime["Time"] = availableTimes[8, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning8.CssClass = "btn btn-primary";
        }

        protected void btnMorning9_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[9, 0];
            bookingTime["Time"] = availableTimes[9, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning9.CssClass = "btn btn-primary";
        }

        protected void btnMorning10_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[10, 0];
            bookingTime["Time"] = availableTimes[10, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnMorning10.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon12_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[12, 0];
            bookingTime["Time"] = availableTimes[12, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon12.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon13_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[13, 0];
            bookingTime["Time"] = availableTimes[13, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon13.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon14_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[14, 0];
            bookingTime["Time"] = availableTimes[14, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon14.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon15_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[15, 0];
            bookingTime["Time"] = availableTimes[15, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon15.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon16_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[16, 0];
            bookingTime["Time"] = availableTimes[16, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon16.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon17_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[17, 0];
            bookingTime["Time"] = availableTimes[17, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon17.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon18_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[18, 0];
            bookingTime["Time"] = availableTimes[18, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon18.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon19_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[19, 0];
            bookingTime["Time"] = availableTimes[19, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon19.CssClass = "btn btn-primary";
        }

        protected void btnAfternoon20_Click(object sender, EventArgs e)
        {
            calBooking_SelectionChanged(sender, e);
            bookingTime["TimeSlot"] = availableTimes[20, 0];
            bookingTime["Time"] = availableTimes[20, 1];
            Response.Cookies.Add(bookingTime);
            deselectButton();
            btnAfternoon20.CssClass = "btn btn-primary";
        }

        protected void rblPickAServiceA_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rblPickAServiceA.SelectedValue != "0")
            {             
                rblPickAServiceB.Enabled = false;
                pickedServiceID.Add(rblPickAServiceA.SelectedValue);
                pickedServiceName.Add(rblPickAServiceA.Text);

            }
            else
            {
                rblPickAServiceB.Enabled = true;
            }
            
        }

        protected void rblPickAServiceB_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rblPickAServiceB.SelectedValue != "0")
            {
                rblPickAServiceA.Enabled = false;
                pickedServiceID.Add(rblPickAServiceB.SelectedValue);
                pickedServiceName.Add(rblPickAServiceB.Text);
            }
            else
            {
                rblPickAServiceA.Enabled = true;
            }
            
        }

        protected void cblPickAServiceN_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach(ListItem item in cblPickAServiceN.Items)
               {
                 if (item.Selected)
                 {
                    try
                    {
                        pickedServiceID.Add(item.Value);
                        pickedServiceName.Add(item.Text);
                    }
                    catch
                    {

                    }
                 }

            }
        }
    }
}
