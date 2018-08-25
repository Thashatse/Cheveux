using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;
using System.Drawing;

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
        SERVICE service = null;
        BookingService bookService = null;
        int count = 0;
        string[,] availableTimes = new string[21,2];
        List<string> pickedServiceName;
        List<string> pickedServiceID;
        string bookingType;
        //internal booking variable
        List<string> CustomerIDs = new List<string>();

        #region set the master page based on the user type
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
                //default
                else
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/Cheveux.Master";
                }
            }
            //set the default master page if the cookie is empty
            else
            {
                //set the master page
                this.MasterPageFile = "~/MasterPages/Cheveux.Master";
            }
        }
        #endregion

        #region page load
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
                    //Redirect is aloud to make booings
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

            #region Internal Booking
            //load booking type 
            bookingType = Request.QueryString["Type"];
            if (!Page.IsPostBack)
            {
                loadCustomerList();
            }
            #endregion

            bookingTime = new HttpCookie("BookTime");
            lblChoose.Text = "Choose A Service to begin booking process...";
            lblChoose.Font.Size = 18;
            lblChoose.ForeColor = Color.Gray;
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
                lblErrorSummary.Text = "Database connection failed. Please try again later";
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
        #endregion

        #region View
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (btnNext.Text == "Choose Hairstylist")
            {
                rblPickAStylist.Items.Clear();
                foreach (SP_GetStylists stylists in stylistList)
                {
                    ListItem item = new ListItem(stylists.FirstName + " - Specializes in " + stylists.ServiceName, stylists.UserID);
                    rblPickAStylist.Items.Add(item);
                }

                if ((cblPickAServiceN.SelectedValue.ToString() == "") && (rblPickAServiceA.SelectedValue.ToString() == "0") && (rblPickAServiceB.SelectedValue.ToString() == "0"))
                {
                    lblErrorSummary.Visible = true;
                    lblErrorSummary.Text = "Please select a service(s) before moving to the next step!";
                    divServices.Visible = true;
                }
                else
                {
                    lblErrorSummary.Visible = false;
                    divServices.Visible = false;
                    divLabels.Visible = false;
                    divStylist.Visible = true;
                    btnPrevious.Visible = true;
                    btnPrevious.Text = "Choose Service(s)";
                    btnNext.Text = "Choose Date & Time";
                }
            }
            else if (btnNext.Text == "Choose Date & Time")
            {
                divLabels.Visible = false;
                if (rblPickAStylist.SelectedValue.ToString() == "")
                {
                    lblErrorSummary.Visible = true;
                    lblErrorSummary.Text = "Please select a hairstylist before moving to the next step!";
                    divStylist.Visible = true;
                }
                else
                {
                    lblErrorSummary.Visible = false;
                    divStylist.Visible = false;
                    divDateTime.Visible = true;
                    btnPrevious.Visible = true;
                    btnPrevious.Text = "Choose Hairstylist";
                    //load booking type 
                    bookingType = Request.QueryString["Type"];
                    if (bookingType == "Internal")
                    {
                        //if internal booking
                        btnNext.Text = "Select Customer";
                    }
                    else
                    {
                        //if external booking
                        btnNext.Text = "Booking Summary";
                    }
                }

            }
            #region Internal Booking
            else if (btnNext.Text == "Select Customer")
            {
                loadCustomerList();
                //if the booking is being made internaly i.e: by Receptionist
                #region set the buttons
                btnPrevious.Text = "Choose Date & Time";
                btnNext.Text = "Booking Summary";
                divDateTime.Visible = false;
                divSelectUser.Visible = true;
                btnPrevious.Visible = true;
                #endregion
            }
            #endregion
            else if (btnNext.Text == "Booking Summary")
            {
                divLabels.Visible = false;
                #region Internal Booking
                //load booking type 
                bookingType = Request.QueryString["Type"];
                if (lbCustomers.SelectedIndex == -1 && bookingType == "Internal")
                {
                    lblErrorSummary.Visible = true;
                    lblErrorSummary.Text = "Please select a customer before moving to the next step!";
                    divSelectUser.Visible = true;
                }
                #endregion
                else
                {

                    //BookingSummary.Text = BookingSummary.Text + " for: " + calBooking.SelectedDate.ToString() + " " + bookedTime;
                    rblPickAServiceA_SelectedIndexChanged(sender, e);
                    rblPickAServiceB_SelectedIndexChanged(sender, e);
                    cblPickAServiceN_SelectedIndexChanged(sender, e);
                    HttpCookie bookingTime = Request.Cookies["BookTime"];
                    if(calBooking.SelectedDate.ToString() == "0001/01/01 00:00:00")
                    {
                        lblErrorSummary.Visible = true;
                        lblErrorSummary.Text = "Please select a date before moving to the next step!";
                        divDateTime.Visible = true;
                    }
                    else if(bookingTime == null)
                    { 
                        lblErrorSummary.Visible = true;
                        lblErrorSummary.Text = "Please select a time before moving to the next step!";
                        divDateTime.Visible = true;
                    }
                    else
                    {
                        divDateTime.Visible = false;
                        divSelectUser.Visible = false;
                        lblErrorSummary.Visible = false;

                        if (pickedServiceID != null)
                        {
                            int serviceCount = 0;
                            foreach (string name in pickedServiceName)
                            {
                                if (serviceCount == 0)
                                {
                                    lblServices.Text = name;
                                    serviceCount++;
                                }
                                else
                                {
                                    lblServices.Text += ", " + name;
                                }

                            }
                        }
                        else
                        {
                            lblErrorSummary.Visible = true;
                            lblErrorSummary.Text = "There was trouble retrieving the services";
                        }

                        lblStylist.Text = rblPickAStylist.SelectedItem.Text;
                        lblDate.Text = calBooking.SelectedDate.ToString("dd MMM yyyy");
                        HttpCookie time = Request.Cookies["BookTime"];
                        lblTime.Text = time["Time"];
                        btnPrevious.Visible = true;


                        if (bookingType == "Internal")
                        {
                            btnPrevious.Text = "Select Customer";
                        }
                        else
                        {
                            btnPrevious.Text = "Choose Date & Time";
                        }

                        #region access control
                        HttpCookie Authcookie = Request.Cookies["CheveuxUserID"];
                        if (Authcookie != null)
                        {
                            if (Authcookie["UT"].ToString()[0] == 'C' || Authcookie["UT"].ToString()[0] == 'R')
                            {
                                btnNext.Text = "Submit";
                            }
                            else
                            {
                                btnNext.Text = "Login";
                            }
                        }
                        else
                        {
                            btnNext.Text = "Login";
                        }
                        #endregion
                    }


                }
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
                try
                {
                    btnPrevious.Visible = true;
                    //initialize booking variable
                    book = new BOOKING();
                    //build service list
                    rblPickAServiceA_SelectedIndexChanged(sender, e);
                    rblPickAServiceB_SelectedIndexChanged(sender, e);
                    cblPickAServiceN_SelectedIndexChanged(sender, e);
                    int length;
                   
                    //access control
                    HttpCookie Authcookie = Request.Cookies["CheveuxUserID"];
                    if (Authcookie != null)
                    {
                        #region external Booking (Customer)
                        if (Authcookie["UT"].ToString()[0] == 'C')
                        {
                            //Make Booking
                            //Add to booking
                            book.BookingID = function.GenerateRandomBookingID();
                            HttpCookie bookingTime = Request.Cookies["BookTime"];
                            book.SlotNo = bookingTime["TimeSlot"];
                            book.Date = calBooking.SelectedDate;
                            book.CustomerID = cookie["ID"];
                            book.StylistID = rblPickAStylist.SelectedValue;
                            book.Available = "N";
                            book.primaryBookingID = book.BookingID;
                            handler.BLL_AddBooking(book);
                            bookService = new BookingService();
                            foreach (string id in pickedServiceID)
                            {
                                bookService.BookingID = book.BookingID;
                                bookService.ServiceID = id;
                                handler.BLL_AddToBookingService(bookService);
                            }
                        }
                        #endregion

                        #region internal Booking (Receptionist)
                        if (Authcookie["UT"].ToString()[0] == 'R')
                        {
                            //Make Booking
                            //Add to booking
                            book.BookingID = function.GenerateRandomBookingID();
                            HttpCookie bookingTime = Request.Cookies["BookTime"];
                            book.SlotNo = bookingTime["TimeSlot"];
                            book.Date = calBooking.SelectedDate;
                            //load customer IDs 
                            loadCustomerID();
                            //add customer id to booking
                            book.CustomerID = CustomerIDs[lbCustomers.SelectedIndex];
                            book.StylistID = rblPickAStylist.SelectedValue;
                            book.Available = "N";
                            book.primaryBookingID = book.BookingID;
                            handler.BLL_AddBooking(book);
                            bookService = new BookingService();
                            foreach (string id in pickedServiceID)
                            {
                                bookService.BookingID = book.BookingID;
                                bookService.ServiceID = id;
                                handler.BLL_AddToBookingService(bookService);
                            }
                        }
                        #endregion

                        length = CalculateSlotLength(sender, e);
                        
                        if (length > 1)
                        {
                            string primaryBookingID = book.BookingID;
                            int bookedSlotIndex = 0;
                            int slotIndex = 0;
                                foreach(SP_GetSlotTimes slot in slotList)
                                {
                                    if(slot.SlotNo == book.SlotNo)
                                    {
                                        bookedSlotIndex = slotIndex;
                                    }
                                    slotIndex++;
                                }

                            for(int i = 1; i < length; i++)
                            {
                                book.BookingID = function.GenerateRandomBookingID();
                                HttpCookie bookingTime = Request.Cookies["BookTime"];
                                bookedSlotIndex++;
                                book.SlotNo = slotList[bookedSlotIndex].SlotNo;
                                book.Date = calBooking.SelectedDate;
                                if (Authcookie["UT"].ToString()[0] == 'C')
                                {
                                    book.CustomerID = cookie["ID"];
                                }
                                else if (Authcookie["UT"].ToString()[0] == 'R')
                                {
                                    book.CustomerID = CustomerIDs[lbCustomers.SelectedIndex];
                                }
                                book.StylistID = rblPickAStylist.SelectedValue;
                                book.Available = "N";
                                book.primaryBookingID = primaryBookingID;
                                handler.BLL_AddBooking(book);

                                foreach (string id in pickedServiceID)
                                {
                                    bookService.BookingID = book.BookingID;
                                    bookService.ServiceID = id;
                                    handler.BLL_AddToBookingService(bookService);
                                }
                            }
                        }

                        #region Email Notification
                        USER user = handler.GetUserDetails(cookie["ID"]);
                        //send an email notification
                        var body = new System.Text.StringBuilder();
                        body.AppendFormat("Hello " + user.FirstName.ToString() + ",");
                        body.AppendLine(@"");
                        body.AppendLine(@"");
                        body.AppendLine(@"Your booking is with " + rblPickAStylist.SelectedItem.Text.ToString() + " on " + calBooking.SelectedDate.ToString("dd MMM yyyy") + " at " + Convert.ToDateTime(bookingTime["Time"]).ToString("HH:mm") + ".");
                        body.AppendLine(@"Your booking is for " + lblServices.Text.ToString() + ".");
                        body.AppendLine(@"");
                        body.AppendLine(@"View or change your booking details here: http://sict-iis.nmmu.ac.za/beauxdebut/ViewBooking.aspx?BookingID=" + book.BookingID.ToString().Replace(" ", string.Empty));
                        body.AppendLine(@"");
                        body.AppendLine(@"Regards,");
                        body.AppendLine(@"");
                        body.AppendLine(@"The Cheveux Team");
                        function.sendEmailAlert(user.Email, user.FirstName + " " + user.LastName,
                            "Booking Confirmation",
                            body.ToString(),
                            "Bookings Cheveux");
                        #endregion

                        #region Redirect
                        //load booking type 
                        bookingType = Request.QueryString["Type"];
                        if (bookingType == null)
                        {
                            //if external booking
                            //redirect and confirm booking
                            Response.Redirect("Default.aspx?BS=True&Sty=" + book.StylistID + "&D=" + book.Date + "&T=" + book.SlotNo);
                        }
                        else
                        {
                            //if internal booking
                            //redirect to booking summary
                            Response.Redirect("ViewBooking.aspx?BookingID=" + book.BookingID.ToString().Replace(" ", string.Empty));
                        }
                        #endregion
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/Authentication/Accounts.aspx?PreviousPage=MakeABooking','_newtab');", true);
                    }
                }
                catch (Exception err)
                {
                    function.logAnError("Error Making abooking "+err.ToString());
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
                divSelectUser.Visible = false;
                divDateTime.Visible = true;
                btnPrevious.Visible = true;
                btnPrevious.Text = "Choose Hairstylist";
                //load booking type 
                bookingType = Request.QueryString["Type"];
                if (bookingType == "Internal")
                {
                    //if internal booking
                    btnNext.Text = "Select Customer";
                }
                else
                {
                    //if external booking
                    btnNext.Text = "Booking Summary";
                }
            }
            #region Internal Booking
            else if (btnPrevious.Text == "Select Customer")
            {
                loadCustomerList();
                //if the booking is being made internaly i.e: by Receptionist
                #region set the buttons
                btnPrevious.Text = "Choose Date & Time";
                btnNext.Text = "Booking Summary";
                divSelectUser.Visible = true;
                btnPrevious.Visible = true;
                #endregion
            }
            #endregion
        }
        #endregion

        #region Date
        protected void calBooking_SelectionChanged(object sender, EventArgs e)
        {
            //get slot length
            int i = CalculateSlotLength(sender, e);
            //slotList Index
            int slotIndex = 0;
            int morningButtonCount = 1;
            int afternoonButtonCount = 11;
            bookedList = handler.BLL_GetBookedStylistTimes(rblPickAStylist.SelectedValue.ToString(), calBooking.SelectedDate);
            HideButtons();
            foreach (SP_GetSlotTimes times in slotList)
            {
                if (bookedList.Count != 0)
                {
                    int checkslotIndex = slotIndex;
                    bool add = true;
                    for (int b = 0; b < i; b++)
                    {
                        if (checkslotIndex < 20)
                        {
                            foreach (SP_GetBookedTimes checkbooked in bookedList)
                            {
                                if (checkbooked.SlotNo == slotList[checkslotIndex].SlotNo
                                    || (calBooking.SelectedDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") &&
                                        times.Time.TimeOfDay < DateTime.Now.TimeOfDay))
                                {
                                    add = false;
                                }
                            }
                        }
                        else
                        {
                            add = false;
                        }
                        checkslotIndex++;
                    }

                    if (add == true)
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
                else
                {
                    int checkslotIndex = slotIndex;
                    bool add = true;
                    for (int b = 0; b < i; b++)
                    {
                        if (checkslotIndex + b >= 20
                            || (calBooking.SelectedDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") &&
                                times.Time.TimeOfDay < DateTime.Now.TimeOfDay))
                        {
                            add = false;
                        }
                        else
                        {
                            add = true;
                        }
                    }

                    if (add == true)
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
                slotIndex++;
            }
        }

        protected void calBooking_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date.CompareTo(DateTime.Today) < 0)
            {
                e.Day.IsSelectable = false;
            }
        }
        #endregion

        #region Time Buttons
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
        #endregion

        #region Services
        protected void rblPickAServiceA_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rblPickAServiceA.SelectedValue != "0")
            {
                lblChoose.Text = "";

                if (pickedServiceID == null)
                {
                    if (pickedServiceName == null)
                    {
                        pickedServiceName = new List<string>();
                        pickedServiceID = new List<string>();
                        pickedServiceID.Add(rblPickAServiceA.SelectedValue);
                        pickedServiceName.Add(rblPickAServiceA.SelectedItem.Text);
                        count++;
                    }
                    
                }
                else
                {
                    pickedServiceID.Add(rblPickAServiceA.SelectedValue);
                    pickedServiceName.Add(rblPickAServiceA.SelectedItem.Text);
                    count++;
                }
                lblServiceLabel.Text = "You have chosen: ";
                if(lblServices.Text == "")
                {
                    lblServices.Text = rblPickAServiceA.SelectedItem.Text;
                }
                else
                {
                    lblServices.Text += ", " + rblPickAServiceA.SelectedItem.Text;
                }
                
                rblPickAServiceB.Enabled = false;

            }
            else
            {
                rblPickAServiceB.Enabled = true;
            }
            
        }

        protected void rblPickAServiceB_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblChoose.Text = "";
            if (rblPickAServiceB.SelectedValue != "0")
            {
                if (pickedServiceID == null)
                {
                    if (pickedServiceName == null)
                    {
                        pickedServiceID = new List<string>();
                        pickedServiceName = new List<string>();
                        pickedServiceID.Add(rblPickAServiceB.SelectedValue);
                        pickedServiceName.Add(rblPickAServiceB.SelectedItem.Text);
                        count++;
                    }
                   
                }
                else
                {
                    pickedServiceID.Add(rblPickAServiceB.SelectedValue);
                    pickedServiceName.Add(rblPickAServiceB.SelectedItem.Text);
                    count++;
                }
                lblServiceLabel.Text = "You have chosen: ";
                if (lblServices.Text == "")
                {
                    lblServices.Text = rblPickAServiceB.SelectedItem.Text;
                }
                else
                {
                    lblServices.Text += ", " + rblPickAServiceB.SelectedItem.Text;
                }
                rblPickAServiceA.Enabled = false;
                
            }
            else
            {
                rblPickAServiceA.Enabled = true;
            }
            
        }

        protected void cblPickAServiceN_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblChoose.Text = "";
            foreach (ListItem item in cblPickAServiceN.Items)
            {
                if (item.Selected)
                 {
                    lblServiceLabel.Text = "You have chosen: ";
                    if (lblServices.Text == "")
                    {
                        lblServices.Text = item.Text;
                    }
                    else
                    {
                        lblServices.Text = ", " + item.Text;
                    }
                    if (pickedServiceID == null)
                    {
                        if (pickedServiceName == null)
                        {
                            pickedServiceName = new List<string>();
                            pickedServiceID = new List<string>();
                            pickedServiceID.Add(item.Value);
                            pickedServiceName.Add(item.Text);
                            count++;
                        }
                      
                    }
                    else
                    {
                        pickedServiceID.Add(item.Value);
                        pickedServiceName.Add(item.Text);
                        count++;
                    }


                }

            }
        }
        #endregion

        #region Internal Bookings
        #region Customer list box & IDs
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
                function.logAnError("Error Loading Customer on MakeABooking | Error: " + err);
            }
        }

        //customerIDS
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

        //update customer for search criteria
        protected void txtCustomerSearch_DataBinding(object sender, EventArgs e)
        {
            loadCustomerList();
        }
        #endregion

        #region Register New Customer
        protected void btnNewCust_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/Authentication/NewAccount.aspx?Type=NewCust&PreviousPage=MakeABooking','_newtab');", true);
        }
        #endregion
        #endregion

        private int CalculateSlotLength(object sender, EventArgs e)
        {
            if (pickedServiceID != null)
            {
                pickedServiceID.Clear();
            }
            service = new SERVICE();
            rblPickAServiceA_SelectedIndexChanged(sender, e);
            rblPickAServiceB_SelectedIndexChanged(sender, e);
            cblPickAServiceN_SelectedIndexChanged(sender, e);
            int slotLength = 0;
            foreach (string id in pickedServiceID)
            {
                slotLength += Convert.ToInt32(handler.BLL_GetSlotLength(id).NoOfSlots);

            }
            return slotLength;
        }
    }
}
