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
        string[,] availableTimes = new string[21,2];
        string bookedTime;
        string bookedSlot;
        string selectedServiceN = "";
        string selectedServiceA = "";
        string selectedServiceB = "";
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
                ListItem deselect = new ListItem("Clear Selection", "1");
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
                    else if(services.ServiceType == 'A')
                    {
                        ListItem item;
                        item = new ListItem(services.Name, services.ServiceID);
                        rblPickAServiceA.Items.Add(item);
                    }
                    else if(services.ServiceType == 'B')
                    {
                        ListItem item;
                        item = new ListItem(services.Name, services.ServiceID);
                        rblPickAServiceB.Items.Add(item);
                    }
                    
                }


            }
            catch(Exception err)
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

                foreach(ListItem item in cblPickAServiceN.Items)
                {
                    if (item.Selected)
                    {
                        selectedServiceN += item.Value;
                    }
                    else
                    {
                        selectedServiceN = "";
                    }
                }


                if ((selectedServiceN == "") && (selectedServiceA == "Clear Selection") && (selectedServiceB == "Clear Selection"))
                {
                    lblValidation.Visible = true;
                    lblValidation.Text = "Please select a service(s) before moving to the next step!";
                    divServices.Visible = true;
                }
                /**else if((selectedServiceA != "Clear Selection") && (selectedServiceB != "Clear Selection"))
                {
                    lblValidation.Visible = true;
                    lblValidation.Text = "Unable to select both an application and braiding service as they cannot be performed at the same time!";
                    divServices.Visible = true;
                }**/
                else
                {
                    divServices.Visible = false;
                    divStylist.Visible = true;
                    btnPrevious.Visible = true;
                    btnPrevious.Text = "Choose Service(s)";
                    btnNext.Text = "Choose Date & Time";
                }
               

            }
            else if (btnNext.Text == "Choose Date & Time")
            {
                BookingSummary.Text = "You have chosen: " + selectedServiceN + selectedServiceB + selectedServiceA + " as your service(s), with hairstylist: " + rblPickAStylist.SelectedValue.ToString();
                bookedList = handler.BLL_GetBookedStylistTimes(rblPickAStylist.SelectedValue.ToString(), calBooking.SelectedDate); 

                if(rblPickAStylist.SelectedValue.ToString() == "")
                {
                    lblValidation.Visible = true;
                    lblValidation.Text = "Please select a hairstylist before moving to the next step!";
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

                lblServices.Text = selectedServiceN + selectedServiceA + selectedServiceB;
                lblStylist.Text = rblPickAStylist.SelectedItem.Text.ToString();
                lblDate.Text = calBooking.SelectedDate.ToString("dd MMM yyyy");
                lblTime.Text = bookedTime;
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
            //access control
            else if (btnNext.Text == "Login")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/Authentication/Accounts.aspx?PreviousPage=MakeABooking','_newtab');", true);
                btnPrevious.Text = "Choose Date & Time";
                btnNext.Text = "Submit";
            }
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
                            book.SlotNo = bookedSlot;
                            book.Date = calBooking.SelectedDate;
                            book.CustomerID = cookie["ID"];
                            //book.ServiceID = drpPickAService.SelectedValue;
                            book.StylistID = rblPickAStylist.SelectedValue;
                            book.Available = "N";
                            handler.BLL_AddBooking(book);
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
            foreach (SP_GetSlotTimes times in slotList)
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
                                btnAfternoon11.Text = times.Time.ToString("hh:mm");
                                availableTimes[11, 0] = times.SlotNo;
                                availableTimes[11, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (afternoonButtonCount == 12)
                            {
                                btnAfternoon12.Visible = true;
                                btnAfternoon12.Text = times.Time.ToString("hh:mm");
                                availableTimes[12, 0] = times.SlotNo;
                                availableTimes[12, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (afternoonButtonCount == 13)
                            {
                                btnAfternoon13.Visible = true;
                                btnAfternoon13.Text = times.Time.ToString("hh:mm");
                                availableTimes[13, 0] = times.SlotNo;
                                availableTimes[13, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (afternoonButtonCount == 14)
                            {
                                btnAfternoon14.Visible = true;
                                btnAfternoon14.Text = times.Time.ToString("hh:mm");
                                availableTimes[14,0] = times.SlotNo;
                                availableTimes[14, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (afternoonButtonCount == 15)
                            {
                                btnAfternoon15.Visible = true;
                                btnAfternoon15.Text = times.Time.ToString("hh:mm");
                                availableTimes[15,0] = times.SlotNo;
                                availableTimes[15, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (afternoonButtonCount == 16)
                            {
                                btnAfternoon16.Visible = true;
                                btnAfternoon16.Text = times.Time.ToString("hh:mm");
                                availableTimes[16,0] = times.SlotNo;
                                availableTimes[16, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (afternoonButtonCount == 17)
                            {
                                btnAfternoon17.Visible = true;
                                btnAfternoon17.Text = times.Time.ToString("hh:mm");
                                availableTimes[17,0] = times.SlotNo;
                                availableTimes[17, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (afternoonButtonCount == 18)
                            {
                                btnAfternoon18.Visible = true;
                                btnAfternoon18.Text = times.Time.ToString("hh:mm");
                                availableTimes[18,0] = times.SlotNo;
                                availableTimes[18, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (afternoonButtonCount == 19)
                            {
                                btnAfternoon19.Visible = true;
                                btnAfternoon19.Text = times.Time.ToString("hh:mm");
                                availableTimes[19,0] = times.SlotNo;
                                availableTimes[19, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (afternoonButtonCount == 20)
                            {
                                btnAfternoon20.Visible = true;
                                btnAfternoon20.Text = times.Time.ToString("hh:mm");
                                availableTimes[20,0] = times.SlotNo;
                                availableTimes[20, 1] = times.Time.ToString("hh:mm");
                            }
                            afternoonButtonCount++;
                        }
                        else
                        {
                            if (morningButtonCount == 1)
                            {
                                btnMorning1.Visible = true;
                                btnMorning1.Text = times.Time.ToString("hh:mm");
                                availableTimes[1,0] = times.SlotNo;
                                availableTimes[1, 1] = times.Time.ToString("hh:mm");
                            }
                            else if(morningButtonCount == 2)
                            {
                                btnMorning2.Visible = true;
                                btnMorning2.Text = times.Time.ToString("hh:mm");
                                availableTimes[2,0] = times.SlotNo;
                                availableTimes[2, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (morningButtonCount == 3)
                            {
                                btnMorning3.Visible = true;
                                btnMorning3.Text = times.Time.ToString("hh:mm");
                                availableTimes[3,0] = times.SlotNo;
                                availableTimes[3, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (morningButtonCount == 4)
                            {
                                btnMorning4.Visible = true;
                                btnMorning4.Text = times.Time.ToString("hh:mm");
                                availableTimes[4,0] = times.SlotNo;
                                availableTimes[4, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (morningButtonCount == 5)
                            {
                                btnMorning5.Visible = true;
                                btnMorning5.Text = times.Time.ToString("hh:mm");
                                availableTimes[5,0] = times.SlotNo;
                                availableTimes[5, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (morningButtonCount == 6)
                            {
                                btnMorning6.Visible = true;
                                btnMorning6.Text = times.Time.ToString("hh:mm");
                                availableTimes[6,0] = times.SlotNo;
                                availableTimes[6, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (morningButtonCount == 7)
                            {
                                btnMorning7.Visible = true;
                                btnMorning7.Text = times.Time.ToString("hh:mm");
                                availableTimes[7,0] = times.SlotNo;
                                availableTimes[7, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (morningButtonCount == 8)
                            {
                                btnMorning8.Visible = true;
                                btnMorning8.Text = times.Time.ToString("hh:mm");
                                availableTimes[8,0] = times.SlotNo;
                                availableTimes[8, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (morningButtonCount == 9)
                            {
                                btnMorning9.Visible = true;
                                btnMorning9.Text = times.Time.ToString("hh:mm");
                                availableTimes[9,0] = times.SlotNo;
                                availableTimes[9, 1] = times.Time.ToString("hh:mm");
                            }
                            else if (morningButtonCount == 10)
                            {
                                btnMorning10.Visible = true;
                                btnMorning10.Text = times.Time.ToString("hh:mm");
                                availableTimes[10,0] = times.SlotNo;
                                availableTimes[10, 1] = times.Time.ToString("hh:mm");
                            }
                            morningButtonCount++;
                        }

                    }
                }
            }
        }

        protected void btnAfternoon11_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[11, 0];
            bookedTime = availableTimes[11, 1];
        }

        protected void btnMorning1_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[1, 0];
            bookedTime = availableTimes[1, 1];
        }

        protected void btnMorning2_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[2, 0];
            bookedTime = availableTimes[2, 1];
        }

        protected void btnMorning3_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[3, 0];
            bookedTime = availableTimes[3, 1];
        }

        protected void btnMorning4_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[4, 0];
            bookedTime = availableTimes[4, 1];
        }

        protected void btnMorning5_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[5, 0];
            bookedTime = availableTimes[5, 1];
        }

        protected void btnMorning6_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[6, 0];
            bookedTime = availableTimes[6, 1];
        }

        protected void btnMorning7_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[7, 0];
            bookedTime = availableTimes[7, 1];
        }

        protected void btnMorning8_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[8, 0];
            bookedTime = availableTimes[8, 1];
        }

        protected void btnMorning9_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[9, 0];
            bookedTime = availableTimes[9, 1];
        }

        protected void btnMorning10_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[10, 0];
            bookedTime = availableTimes[10, 1];
        }

        protected void btnAfternoon12_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[12, 0];
            bookedTime = availableTimes[12, 1];
        }

        protected void btnAfternoon13_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[13, 0];
            bookedTime = availableTimes[13, 1];
        }

        protected void btnAfternoon14_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[14, 0];
            bookedTime = availableTimes[14, 1];
        }

        protected void btnAfternoon15_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[15, 0];
            bookedTime = availableTimes[15, 1];
        }

        protected void btnAfternoon16_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[16, 0];
            bookedTime = availableTimes[16, 1];
        }

        protected void btnAfternoon17_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[17, 0];
            bookedTime = availableTimes[17, 1];
        }

        protected void btnAfternoon18_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[18, 0];
            bookedTime = availableTimes[18, 1];
        }

        protected void btnAfternoon19_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[19, 0];
            bookedTime = availableTimes[19, 1];
        }

        protected void btnAfternoon20_Click(object sender, EventArgs e)
        {
            bookedSlot = availableTimes[20, 0];
            bookedTime = availableTimes[20, 1];
        }

        protected void rblPickAServiceA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(rblPickAServiceA.SelectedValue.ToString() == "Clear Selection")
            {
                rblPickAServiceB.Enabled = true;
            }
            else
            {
                rblPickAServiceB.Enabled = false;
                selectedServiceB = "";
            }
            
        }

        protected void rblPickAServiceB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(rblPickAServiceB.SelectedValue.ToString() == "Clear Selection")
            {
                rblPickAServiceA.Enabled = true;
            }
            else
            {
                rblPickAServiceA.Enabled = false;
                selectedServiceA = "";
            }
            
        }
    }
}