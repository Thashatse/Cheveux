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
        
        List<SP_GetStylistBookings> customer = null;
        List<SP_GetBookingServices> bServices = null;
        List<SP_ReturnStylistNamesForReview> empNames = null;
        List<SP_GetReviews> r = null;
        List<SP_AboutStylist> abS = null;
        SP_ViewEmployee viewEmp = null;
        SP_GetEmployee_S_ s = null;
        SP_GetCustomerBooking dtl = null;
        REVIEW review = null;
        REVIEW customerStylistReview = null;
        REVIEW customerBookingReview = null;


        string noti;
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

                    drpReadType.Visible = false;
                    drpRev.Visible = true;

                    drpReadType.SelectedValue = "0";

                    if (!IsPostBack)
                    {
                        if (drpReadType.SelectedValue == "0")
                        {
                            if (drpRev.SelectedValue == "0")//stylist reviews
                            {
                                stylistPanel.Visible = true;
                                bookingsPanel.Visible = false;

                                lblBookingReviewsHeader.Visible = false;
                                lblStylistReviewsHeader.Visible = true;
                                loadAllStylistReviews();
                            }
                            else if (drpRev.SelectedValue == "1")//bookings reviews
                            {
                                bookingsPanel.Visible = true;
                                stylistPanel.Visible = false;

                                lblStylistReviewsHeader.Visible = false;
                                lblBookingReviewsHeader.Visible = true;
                                loadAllBookingReviews();
                            }
                        }
                    }

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
                    btnRead.Visible = false;

                    drpReadType.Visible = true;
                    drpRev.Visible = true;

                    if (!IsPostBack)
                    {
                        if (drpReadType.SelectedValue == "0")
                        {
                            if (drpRev.SelectedValue == "0")
                            {
                                stylistPanel.Visible = true;
                                bookingsPanel.Visible = false;

                                lblBookingReviewsHeader.Visible = false;
                                lblStylistReviewsHeader.Visible = true;


                                loadAllStylistReviews();
                            }
                            else if (drpRev.SelectedValue == "1")
                            {
                                bookingsPanel.Visible = true;
                                stylistPanel.Visible = false;

                                lblStylistReviewsHeader.Visible = false;
                                lblBookingReviewsHeader.Visible = true;


                                loadAllBookingReviews();
                            }
                        }
                        else if (drpReadType.SelectedValue == "1")
                        {
                            loadMyReviews(cookie["ID"].ToString());
                            if (drpRev.SelectedValue == "0")
                            {
                                stylistPanel.Visible = true;
                                bookingsPanel.Visible = false;

                                lblBookingReviewsHeader.Visible = false;
                                lblStylistReviewsHeader.Visible = true;
                            }
                            else if (drpRev.SelectedValue == "1")
                            {
                                bookingsPanel.Visible = true;
                                stylistPanel.Visible = false;

                                lblStylistReviewsHeader.Visible = false;
                                lblBookingReviewsHeader.Visible = true;
                            }
                        }
                    }

                }
                else if (action == "MakeAreview")
                {
                    OpeningHeader.Text = "Write A Review";
                    readReviews.Visible = false;
                    btnRev.Visible = false;
                    makeAreview.Visible = true;
                    btnRead.Visible = true;

                    if (drpReviewType.SelectedValue == "0")//review booking
                    {
                        rvBoxHeader.InnerText = "Write a review and rate the service";
                    }
                    else if (drpReviewType.SelectedValue == "1")//review stylist
                    {
                        rvBoxHeader.InnerText = "Write a review and rate the stylist";
                    }

                    noti = Request.QueryString["noti"];
                    if(noti == "s")
                    {
                        Notification(noti);
                    }
                    else if(noti == "f")
                    {
                        Notification(noti);
                    }

                    if (Page.IsPostBack)
                    {
                        displayPastBookings(cookie["ID"].ToString(), calDay.SelectedDate);
                        hideNoti();
                    }

                    if (!Page.IsPostBack)
                    {
                        //Set the month dropdownlist to the current month by defualt on page load
                        int index = DateTime.Today.Month;
                        drpMonth.SelectedValue = index.ToString();
                    }

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

                    drpReadType.Visible = false;
                    drpRev.Visible = true;

                    drpReadType.SelectedValue = "0";

                    if (!IsPostBack)
                    {
                        if (drpReadType.SelectedValue == "0")
                        {
                            if (drpRev.SelectedValue == "0")//stylist reviews
                            {
                                stylistPanel.Visible = true;
                                bookingsPanel.Visible = false;

                                lblBookingReviewsHeader.Visible = false;
                                lblStylistReviewsHeader.Visible = true;
                                loadAllStylistReviews();
                            }
                            else if (drpRev.SelectedValue == "1")//bookings reviews
                            {
                                bookingsPanel.Visible = true;
                                stylistPanel.Visible = false;

                                lblStylistReviewsHeader.Visible = false;
                                lblBookingReviewsHeader.Visible = true;
                                loadAllBookingReviews();
                            }
                        }
                    }
                }
            }
        }
        public Tuple<string,string> getBookingDateTime(string bookingID)
        {
            
            try
            {
                dtl = handler.getCustomerPastBookingDetails(bookingID);
                
                string date = dtl.bookingDate.ToString("dd-MM-yyyy");
                string time = dtl.bookingStartTime.ToString("HH:mm");
                var tuple = new Tuple<string, string>(date,time);
                return tuple;
            }
            catch(Exception err)
            {
                function.logAnError(err.ToString());
                string date = "-";
                string time = "-";
                var tuple = new Tuple<string, string>(date, time);
                return  tuple;
            }
            
        }

        #region Write Reviews
        
        #region Methods
        public void viewEmployee(string empID)
        {
            tblBookings.Rows.Clear();

            TableRow row = new TableRow();
            tblBookings.Rows.Add(row);
            try
            {
                viewEmp = handler.viewEmployee(empID);
                TableCell userImage = new TableCell();
                userImage.Text = "<img src=" + viewEmp.empImage
                                  + " alt='" + viewEmp.firstName + " " + viewEmp.lastName +
                                 " Profile Image' width='150' height='170'/>";
                tblBookings.Rows[0].Cells.Add(userImage);

                try
                {
                    s = handler.getEmployee_S(empID);
                    TableCell newCell = new TableCell();
                    newCell.Text = "<h5>Specialisation: "
                                  + "<a href='ViewProduct.aspx?ProductID="
                                  + s.ServiceID.Replace(" ", string.Empty) + "' target='_blank'>"
                                  + s.Specialisation.ToString() + "</a></h5>";
                    newCell.Width = 600;
                    tblBookings.Rows[0].Cells.Add(newCell);

                }
                catch (Exception Err)
                {
                    TableCell errorCell = new TableCell();
                    errorCell.Text = " ";
                    tblBookings.Rows[0].Cells.Add(errorCell);

                    function.logAnError("Unable to retrievee specialisation review.aspx err:" + Err.ToString());
                }

                TableRow newRow = new TableRow();
                tblBookings.Rows.Add(newRow);
            }
            catch (Exception Err)
            {
                TableCell userImage = new TableCell();
                userImage.Text = "<img src='https://cdn1.iconfinder.com/data/icons/user-thinline-icons-set/144/User003_Error-512.png' alt='Error' width='50' height='50'></img>";
                tblBookings.Rows[0].Cells.Add(userImage);

                function.logAnError("Couldn't display user image in reviews.aspx err:" + Err.ToString());
            }
        }
        public void displayPastBookings(string customerID, DateTime day)
        {
            Button btn;
            tblBookings.Rows.Clear();
            choose.Visible = true;
            TableRow headerRow = new TableRow();
            tblBookings.Rows.Add(headerRow);

            TableCell rHeadings = new TableCell();
            rHeadings.Text = "Time";
            rHeadings.Width = 100;
            tblBookings.Rows[0].Cells.Add(rHeadings);

            rHeadings = new TableCell();
            rHeadings.Text = "Service";
            rHeadings.Width = 100;
            tblBookings.Rows[0].Cells.Add(rHeadings);

            rHeadings = new TableCell();
            rHeadings.Text = "Stylist";
            rHeadings.Width = 100;
            tblBookings.Rows[0].Cells.Add(rHeadings);
            try
            {
                customer = handler.getCustomerPastBookingsForDate(customerID, day);
                int count = 1;
                foreach (SP_GetStylistBookings a in customer)
                {
                    TableRow newRow = new TableRow();
                    tblBookings.Rows.Add(newRow);

                    TableCell newCell = new TableCell();
                    tblBookings.Rows[count].Cells.Add(newCell);
                    newCell.Width = 50;
                    newCell.Text = a.StartTime.ToString("HH:mm");

                    TableCell services = new TableCell();
                    services.Width = 100;
                    try
                    {
                        bServices = handler.getBookingServices(a.BookingID.ToString());
                        if (bServices.Count == 1)
                        {
                            services.Text = "<a href='ViewProduct.aspx?ProductID=" + bServices[0].ServiceID.Replace(" ", string.Empty) + "'>"
                            + bServices[0].ServiceName.ToString() + "</a>";
                        }
                        else if (bServices.Count == 2)
                        {
                            services.Text = "<a href='../ViewBooking.aspx?BookingID=" + a.BookingID.ToString().Replace(" ", string.Empty) +
                                "'>" + bServices[0].ServiceName.ToString() +
                                ", " + bServices[1].ServiceName.ToString() + "</a>";
                        }
                        else if (bServices.Count > 2)
                        {
                            string toolTip = "";
                            int toolTipCount = 0;
                            foreach (SP_GetBookingServices toolTipDTL in bServices)
                            {
                                if (toolTipCount == 0)
                                {
                                    toolTip = toolTipDTL.ServiceName;
                                    toolTipCount++;
                                }
                                else
                                {
                                    toolTip += ", " + toolTipDTL.ServiceName;
                                }
                            }
                            services.Text = "<a title='" + toolTip + "'" +
                                "href='../ViewBooking.aspx?BookingID=" + a.BookingID.ToString().Replace(" ", string.Empty) +
                                "'> Multiple Services </a>";
                        }
                        tblBookings.Rows[count].Cells.Add(services);

                    }
                    catch (Exception Err)
                    {
                        //if theres an error or cant retrieve the services from the database 
                        services.Text = "---";
                        tblBookings.Rows[count].Cells.Add(services);
                        function.logAnError("Couldn't get the services [reviews.aspx "
                            + "{getTimeCustomerServices?getServices} ] error:" + Err.ToString());
                    }

                    TableCell empCell = new TableCell();
                    empCell.Width = 150;
                    try
                    {
                        empCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + a.StylistID.ToString().Replace(" ", string.Empty) +
                                        "'>" + a.StylistName.ToString() + "</a>";
                        tblBookings.Rows[count].Cells.Add(empCell);
                    }
                    catch (Exception Err)
                    {
                        empCell.Text = "------";
                        tblBookings.Rows[count].Cells.Add(empCell);
                        function.logAnError("Couldnt get stylist name[reviews.aspx {getT/c/s method}]err:" + Err.ToString());
                    }

                    newCell = new TableCell();
                    newCell.Width = 50;
                    btn = new Button();
                    btn.Text = "Review";
                    btn.CssClass = "btn btn-primary";
                    newCell.Controls.Add(btn);
                    btn.Click += (ss, ee) => {
                        //display review box to review a booking

                        lblBookingID.Text = a.PrimaryID.ToString();
                        lblStylistID.Text = a.StylistID.ToString();

                        checkBooking(a);

                        theReview.Visible = true;
                    };
                    tblBookings.Rows[count].Cells.Add(newCell);
                    count++;
                }

            }
            catch (Exception Err)
            {
                TableRow newRow = new TableRow();
                tblBookings.Rows.Add(newRow);
                TableCell newCell = new TableCell();
                newCell.Text = "--";
                tblBookings.Rows[1].Cells.Add(newCell);
                function.logAnError(Err.ToString());
            }
        }
        public void Notification(string noti)
        {
            if (noti == "s")
            {
                erNoti.Visible = false;
                sucNoti.Visible = true;
                lblSucNoti.Text = "Your review has been successfully recieved." +
                    "Thank you for your feedback";
            }
            else if (noti == "f")
            {
                erNoti.Visible = true;
                sucNoti.Visible = false;
                lblErNoti.Text = "Error posting the review." +
                    "Please try again later.";
            }
        }
        public void loadStylistNames(string customerID)
        {
            try
            {
                empNames = handler.returnStylistNamesForReview(customerID);
                foreach (SP_ReturnStylistNamesForReview emp in empNames)
                {
                    drpStylistNames.DataSource = empNames;
                    drpStylistNames.DataTextField = "StylistName";
                    drpStylistNames.DataValueField = "StylistID";
                    drpStylistNames.DataBind();
                }
            }
            catch (Exception err)
            {
                drpStylistNames.Items.Add("Unable to retrieve names");
                function.logAnError("Error on review page" + err.ToString());
            }
        }
        public void loadBookingReview(string customerID, string bookingID)
        {
            try
            {
                customerBookingReview = handler.customersReviewForBooking(customerID, bookingID);

                if (IsPostBack)
                {
                    Rating1.CurrentRating = customerBookingReview.Rating;
                    if (customerBookingReview.Comment != string.Empty || customerBookingReview.Comment != null)
                    {
                        reviewComment.InnerText = customerBookingReview.Comment.ToString();
                    }
                    lblReviewID.Text = customerBookingReview.ReviewID;
                }


            }
            catch (Exception Err)
            {
                reviewComment.Attributes.Add("placeholder",
                                    "Booking has already been review.If you wish you may update it here.....");
                function.logAnError("(load booking method error). Error: " + Err.ToString());
            }
        }
        public void loadStylistReview(string customerID, string stylistID)
        {
            try
            {
                customerStylistReview = handler.customersReviewForStylist(customerID, stylistID);

                if (IsPostBack)
                {
                    Rating1.CurrentRating = customerStylistReview.Rating;
                    if (customerStylistReview.Comment != string.Empty || customerStylistReview.Comment != null)
                    {
                        reviewComment.InnerText = customerStylistReview.Comment.ToString();
                    }
                    lblReviewID.Text = customerStylistReview.ReviewID;
                }

            }
            catch (Exception Err)
            {
                Rating1.CurrentRating = 0;
                reviewComment.Attributes.Add("placeholder",
                                    "You have already reviewed the stylist.Update the review here....");
                function.logAnError("(load stylist method). Error: " + Err.ToString());
            }
        }
        public void checkStylist()
        {
            try
            {
                review = handler.customersReviewForStylist(cookie["ID"].ToString(), drpStylistNames.SelectedValue.ToString());
                if (review != null)
                {
                    loadStylistReview(cookie["ID"].ToString(), drpStylistNames.SelectedValue.ToString());
                    btnPostReview.Visible = false;
                    btnUpdateReview.Visible = true;
                }
                else
                {
                    btnPostReview.Visible = true;
                    btnUpdateReview.Visible = false;
                    clearAndAddPlaceholderText();
                }
            }
            catch (Exception ERR)
            {
                function.logAnError("checkStylist method. Error: " + ERR.ToString());
            }
        }
        public void checkBooking(SP_GetStylistBookings a)
        {
            try
            {
                //check if booking selected for review has been reviewed
                cookie = Request.Cookies["CheveuxUserID"];
                review = handler.customersReviewForBooking(cookie["ID"].ToString(), a.PrimaryID.ToString());
                //if its not null the boooking has been reviewed
                if (review != null)
                {
                    //load the review comment in the text area 
                    loadBookingReview(cookie["ID"].ToString(), a.PrimaryID.ToString());
                    btnPostReview.Visible = false;
                    btnUpdateReview.Visible = true;
                }
                else
                {
                    btnPostReview.Visible = true;
                    btnUpdateReview.Visible = false;
                    clearAndAddPlaceholderText();
                }
            }
            catch (Exception ERR)
            {
                function.logAnError("checkBooking method. Error: " + ERR.ToString());
            }
        }
        public void clearAndAddPlaceholderText()
        {
            reviewComment.InnerHtml = "Your review here....";
            Rating1.CurrentRating = 0;
        }
        public void hideNoti()
        {
            erReview.Visible = false;
            erNoti.Visible = false;
            sucNoti.Visible = false;
        }
        #endregion

        #region Events
        protected void calDay_SelectionChanged(object sender, EventArgs e)
        {
            theReview.Visible = false;
            cookie = Request.Cookies["CheveuxUserID"];
            lsBksHeader.InnerText = "Your Booking(s) on "
                                + calDay.SelectedDate.ToString("dd-MM-yyyy");
            displayPastBookings(cookie["ID"].ToString(), calDay.SelectedDate);
        }
        protected void drpReviewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cookie = Request.Cookies["CheveuxUserID"];
            if (drpReviewType.SelectedValue == "0")//review booking
            {
                lsBksHeader.Visible = true;
                datesPick.Visible = true;
                dvStylistNames.Visible = false;
                theReview.Visible = false;
                choose.Visible = true;
                tblBookings.Visible = true;
            }
            else if (drpReviewType.SelectedValue == "1")//review stylist
            {
                lsBksHeader.Visible = false;
                datesPick.Visible = false;
                theReview.Visible = true;
                choose.Visible = false;
                dvStylistNames.Visible = true;

                loadStylistNames(cookie["ID"].ToString());
                viewEmployee(drpStylistNames.SelectedValue.ToString());
                checkStylist();
            }
        }
        protected void drpStylistNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            datesPick.Visible = false;
            theReview.Visible = true;
            choose.Visible = false;
            checkStylist();
            viewEmployee(drpStylistNames.SelectedValue.ToString());
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
        protected void btnPostReview_Click(object sender, EventArgs e)
        {
            cookie = Request.Cookies["CheveuxUserID"];
            if (drpReviewType.SelectedValue == "0")
            {
                //review booking
                try
                {
                    review = new REVIEW();

                    string rID = function.GenerateRandomReviewID();

                    review.ReviewID = rID;
                    review.CustomerID = cookie["ID"].ToString();
                    review.EmployeeID = lblStylistID.Text.ToString();
                    review.PrimaryBookingID = lblBookingID.Text.ToString();
                    review.Date = DateTime.Today;
                    review.Time = Convert.ToDateTime(DateTime.Now.ToString("h:mm:ss tt"));
                    review.Rating = Rating1.CurrentRating;
                    review.Comment = reviewComment.InnerText.ToString();

                    if (handler.reviewBooking(review))
                    {
                        Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview&noti=s");
                    }
                    else
                    {
                        Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview&noti=f");
                    }
                }
                catch (Exception Err)
                {
                    erReview.Visible = true;
                    lblErReview.Text = "Error posting review";
                    function.logAnError(Err.ToString());
                }
            }
            else if (drpReviewType.SelectedValue == "1")
            {
                //review stylist
                try
                {
                    review = new REVIEW();

                    string rID = function.GenerateRandomReviewID();

                    review.ReviewID = rID;
                    review.CustomerID = cookie["ID"].ToString();
                    review.EmployeeID = drpStylistNames.SelectedValue.ToString();
                    review.Date = DateTime.Today;
                    review.Time = Convert.ToDateTime(DateTime.Now.ToString("h:mm:ss tt"));
                    review.Rating = Rating1.CurrentRating;
                    review.Comment = reviewComment.InnerText.ToString();

                    if (handler.reviewStylist(review))
                    {
                        Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview&noti=s");
                    }
                    else
                    {
                        Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview&noti=f");
                    }
                }
                catch (Exception Err)
                {
                    erReview.Visible = true;
                    lblErReview.Text = "Error posting review";
                    function.logAnError(Err.ToString());
                }

            }

        }
        protected void btnUpdateReview_Click(object sender, EventArgs e)
        {
            cookie = Request.Cookies["CheveuxUserID"];
            if (drpReviewType.SelectedValue == "0")
            {
                //reviewing a booking
                try
                {
                    review = new REVIEW();

                    review.ReviewID = lblReviewID.Text.ToString();
                    review.PrimaryBookingID = lblBookingID.Text.ToString();
                    review.Date = DateTime.Today;
                    review.Time = Convert.ToDateTime(DateTime.Now.ToString("h:mm:ss tt"));
                    review.Rating = Rating1.CurrentRating;
                    review.Comment = reviewComment.InnerText.ToString();

                    if (handler.updateBookingReview(review))
                    {
                        Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview&noti=s");
                    }
                    else
                    {
                        Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview&noti=f");
                    }
                }
                catch (Exception err)
                {
                    erReview.Visible = true;
                    lblErReview.Text = "Error";
                    function.logAnError("Error updating booking review. Error: " + err.ToString());
                }
            }
            else if (drpReviewType.SelectedValue == "1")
            {
                //reviewing a stylist
                try
                {
                    review = new REVIEW();

                    review.ReviewID = lblReviewID.Text.ToString();
                    review.EmployeeID = drpStylistNames.SelectedValue.ToString();
                    review.Date = DateTime.Today;
                    review.Time = Convert.ToDateTime(DateTime.Now.ToString("h:mm:ss tt"));
                    review.Rating = Rating1.CurrentRating;
                    review.Comment = reviewComment.InnerText.ToString();

                    if (handler.updateStylistReview(review))
                    {
                        Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview&noti=s");
                    }
                    else
                    {
                        Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview&noti=f");
                    }
                }
                catch (Exception err)
                {
                    erReview.Visible = true;
                    lblErReview.Text = "Error";
                    function.logAnError("Error updating stylist review. Error: " + err.ToString());
                }
            }

        }
        #endregion

        #endregion

        #region Read Reviews

        #region Methods

        #region myReviews
        public void loadMyReviews(string customerID)
        {
            int numofStylistReviews = 0;
            int numofBookingReviews = 0;
            
            try
            {
                r = handler.getCustomersReviews(customerID);
                //int sCount = 0;
                
                foreach (SP_GetReviews a in r)
                {
                    if(string.IsNullOrEmpty(a.PrimaryBookingID))//stylist reviews
                    {
                        Table tblStylistReviews = new Table();
                        tblStylistReviews.CssClass = "table-bordered";

                        TableRow newRow = new TableRow();
                        tblStylistReviews.Rows.Add(newRow);

                        TableCell newCell = new TableCell();

                        newCell.Text = "<img src='" + a.StylistImage +
                                    "' alt='Stylist Image' width='200' height='160'/><br/>"
                                    + "Review Date:" + a.Date.ToString("dd-MM-yyyy");
                        newCell.Font.Bold = true;
                        tblStylistReviews.Rows[0].Cells.Add(newCell);

                        newCell = new TableCell();

                        //create placeholder control

                        //stylist name placeholder
                        PlaceHolder ph = new PlaceHolder();
                        ph.Controls.Add(new LiteralControl(
                                        a.StylistName.ToString() + "<br/>"));

                        //add the placeholder to the cell
                        newCell.Controls.Add(ph);

                        //star rating placeholder
                        ph = new PlaceHolder();
                        //create rating control
                        AjaxControlToolkit.Rating theStars = new AjaxControlToolkit.Rating();
                        theStars.CurrentRating = a.Rating;
                        theStars.ID = "rt" + a.EmployeeID.ToString();
                        theStars.StarCssClass = "starRating";
                        theStars.WaitingStarCssClass = "waitingStar";
                        theStars.FilledStarCssClass = "filledStar";
                        theStars.EmptyStarCssClass = "emptyStar";
                        theStars.CssClass = "img-fluid";
                        theStars.ReadOnly = true;

                        //add control to the placeholder
                        ph.Controls.Add(theStars);

                        //add the placeholder to the cell
                        newCell.Controls.Add(ph);

                        //Customer review of the stylist
                        ph = new PlaceHolder();
                        ph.Controls.Add(new LiteralControl(
                                        a.Comment.ToString() +"<br>"));
                        newCell.Controls.Add(ph);


                        tblStylistReviews.Rows[0].Cells.Add(newCell);
                        stylistPanel.Controls.Add(tblStylistReviews);
                        numofStylistReviews++;
                    }
                    else if(!string.IsNullOrEmpty(a.PrimaryBookingID))//booking reviews
                    {
                        int bCount = 0;
                        Table tblBookings = new Table();
                        tblBookings.CssClass = "table-bordered";

                        TableRow newRow = new TableRow();
                        tblBookings.Rows.Add(newRow);

                        TableCell newCell = new TableCell();

                        PlaceHolder ph;
                        try
                        {
                            bServices = handler.getBookingServices(a.PrimaryBookingID.ToString());
                            var dt = getBookingDateTime(a.PrimaryBookingID.ToString());
                            if (bServices.Count == 1)
                            {
                                ph = new PlaceHolder();
                                ph.Controls.Add(new LiteralControl(
                                "<a href='ViewProduct.aspx?ProductID=" + bServices[0].ServiceID.Replace(" ", string.Empty) + "'>"
                                + bServices[0].ServiceName.ToString() + "</a>"
                                + " with "
                                + a.StylistName.ToString()
                                + " on " + dt.Item1
                                + " at " + dt.Item2+ "<br><br>"));
                                newCell.Controls.Add(ph);
                            }
                            else if (bServices.Count == 2)
                            {
                                ph = new PlaceHolder();
                                ph.Controls.Add(new LiteralControl(
                                "<a href='../ViewBooking.aspx?BookingID=" + a.PrimaryBookingID.ToString().Replace(" ", string.Empty) +
                                "'>" + bServices[0].ServiceName.ToString() +
                                ", " + bServices[1].ServiceName.ToString() + "</a>"
                                + " with "
                                + a.StylistName.ToString()
                                + " on " + dt.Item1
                                + " at " + dt.Item2+ "<br><br>"));
                                newCell.Controls.Add(ph);
                            }
                            else if (bServices.Count > 2)
                            {
                                string toolTip = "";
                                int toolTipCount = 0;
                                foreach (SP_GetBookingServices toolTipDTL in bServices)
                                {
                                    if (toolTipCount == 0)
                                    {
                                        toolTip = toolTipDTL.ServiceName;
                                        toolTipCount++;
                                    }
                                    else
                                    {
                                        toolTip += ", " + toolTipDTL.ServiceName;
                                    }
                                }
                                ph = new PlaceHolder();
                                ph.Controls.Add(new LiteralControl(
                                "<a title='" + toolTip + "'" +
                                "href='../ViewBooking.aspx?BookingID=" + a.PrimaryBookingID.ToString().Replace(" ", string.Empty) +
                                "'> Multiple Services </a>"
                                + a.StylistName.ToString()
                                + " on " + dt.Item1
                                + " at " + dt.Item2+ "<br><br>"));
                                newCell.Controls.Add(ph);
                            }

                        }
                        catch (Exception Err)
                        {
                            PlaceHolder phE = new PlaceHolder();
                            phE.Controls.Add(new LiteralControl("Booking details currently unavailable."));
                            newCell.Controls.Add(phE);

                            function.logAnError("Error getting booking services for reading booking reviews(my)."
                                    +"Error : "+ Err.ToString());
                        }  
                        ph = new PlaceHolder();
                        ph.Controls.Add(new LiteralControl(
                                        "<b>Rating given:</b><br>"));
                        newCell.Controls.Add(ph);

                        ph = new PlaceHolder();
                        AjaxControlToolkit.Rating theStars = new AjaxControlToolkit.Rating();
                        theStars.CurrentRating = a.Rating;
                        theStars.ID = "rt" + a.PrimaryBookingID.ToString();
                        theStars.StarCssClass = "starRating";
                        theStars.WaitingStarCssClass = "waitingStar";
                        theStars.FilledStarCssClass = "filledStar";
                        theStars.EmptyStarCssClass = "emptyStar";
                        theStars.ReadOnly = true;
                        ph.Controls.Add(theStars);
                        newCell.Controls.Add(ph);

                        ph = new PlaceHolder();
                        ph.Controls.Add(new LiteralControl("<br><br><br>"));
                        newCell.Controls.Add(ph);

                        ph = new PlaceHolder();
                        ph.Controls.Add(new LiteralControl("<b>Comment: </b><br>"));
                        newCell.Controls.Add(ph);

                        ph = new PlaceHolder();
                        ph.Controls.Add(new LiteralControl(a.Comment.ToString() + "<br><br>"));
                        newCell.Controls.Add(ph);

                        tblBookings.Rows[bCount].Cells.Add(newCell);
                        bCount++;

                        bookingsPanel.Controls.Add(tblBookings);
                        numofBookingReviews++;
                    }
                    
                }
                lblStylistReviewsHeader.Text = "Your stylist reviews ("+ numofStylistReviews.ToString() + ")";
                lblBookingReviewsHeader.Text = "Your Booking reviews ("+numofBookingReviews.ToString() +")";
            }
            catch (Exception err)
            {
                Table srError = new Table();
                TableRow newRow = new TableRow();
                srError.Rows.Add(newRow);
                TableCell newCell = new TableCell();
                newCell.Text = "Error retrieving data from the database.";
                srError.Rows[0].Cells.Add(newCell);
                function.logAnError("Load Stylist Reviews method error. Error: " + err.ToString());
            }
        }
        public void loadAllStylistReviews()
        {
            
            int totStylistReviews = 0;
            try
            {
                abS = handler.aboutStylist();

                foreach(SP_AboutStylist stylist in abS)
                {

                    Table tblForStylist = new Table();

                    TableRow newRow = new TableRow();
                    tblForStylist.Rows.Add(newRow);

                    TableCell newCell = new TableCell();

                    PlaceHolder ph = new PlaceHolder();

                    ph.Controls.Add(new LiteralControl(
                                "<img src='" + stylist.UserImage +
                                "' alt='Stylist Image' width='200' height='160'/><br/>"
                                + stylist.StylistName + "<br/><br/>"));
                    newCell.Controls.Add(ph);

                    ph = new PlaceHolder();
                    ph.Controls.Add(new LiteralControl("<span style='text-decoration:underline;'>Average Rating</span>:<br/>"));
                    newCell.Controls.Add(ph);

                    ph = new PlaceHolder();
                    try
                    {
                        review = handler.getStylistRating(stylist.EmployeeID.ToString());

                        AjaxControlToolkit.Rating theStars = new AjaxControlToolkit.Rating();
                        theStars.CurrentRating = review.Rating;
                        theStars.ID = "rt" + stylist.EmployeeID.ToString();
                        theStars.StarCssClass = "starRating";
                        theStars.WaitingStarCssClass = "waitingStar";
                        theStars.FilledStarCssClass = "filledStar";
                        theStars.EmptyStarCssClass = "emptyStar";
                        theStars.CssClass = "img-fluid";
                        theStars.ReadOnly = true;

                        ph.Controls.Add(theStars);

                    }
                    catch(Exception err)
                    {
                        ph.Controls.Add(new LiteralControl("-"));
                        function.logAnError("Error getting stylist rating. Error:"+err.ToString());
                    }
                    newCell.Controls.Add(ph);

                    ph = new PlaceHolder();
                    ph.Controls.Add(new LiteralControl("<a class='btn btn-primary' "+
                                        "style='text-decoration:none !important;'"+
                                        "href='../Cheveux/Reviews.aspx?Action=MakeAreview'"+
                                        ">Review</a>"));
                    newCell.Controls.Add(ph);

                    newCell.Font.Bold = true;
                    tblForStylist.Rows[0].Cells.Add(newCell);

                    newCell = new TableCell();

                    ph = new PlaceHolder();
                    Panel newPanel = new Panel();
                    newPanel.ScrollBars = ScrollBars.Vertical;
                    newPanel.Height = 400;
                    newPanel.Width = 600;
                    newPanel.BorderColor = System.Drawing.Color.DarkGray;
                    newPanel.BorderStyle = BorderStyle.Solid;

                    PlaceHolder innerPh = new PlaceHolder();
                    Label lblrev = new Label();
                    lblrev.Text = "Reviews:";
                    lblrev.Font.Bold = true;
                    lblrev.Attributes.Add("style", "text-decoration:underline;");
                    innerPh.Controls.Add(lblrev);
                    newPanel.Controls.Add(innerPh);

                    innerPh = new PlaceHolder();
                    Table innerTable = new Table();
                    innerTable.CssClass = "table-bordered";
                    try
                    {
                        int counter = 0;
                        r = handler.getReviewsOfStylist(stylist.EmployeeID.ToString());
                        foreach(SP_GetReviews stylistReviews in r)
                        {
                            
                            TableRow innerRow = new TableRow();
                            innerTable.Rows.Add(innerRow);
                            
                            
                            TableCell innerCell = new TableCell();
                            PlaceHolder innerPhForTableCells = new PlaceHolder();
                            innerPhForTableCells.Controls.Add(new LiteralControl("<b>Review by:</b><br>"+
                                                   stylistReviews.CustomerName));
                            innerCell.Controls.Add(innerPhForTableCells);

                            innerPhForTableCells = new PlaceHolder();
                            innerPhForTableCells.Controls.Add(new LiteralControl("<br><b>Rating given:</b><br>"));
                            innerCell.Controls.Add(innerPhForTableCells);

                            innerPhForTableCells = new PlaceHolder();
                            AjaxControlToolkit.Rating theStars = new AjaxControlToolkit.Rating();
                            theStars.CurrentRating = stylistReviews.Rating;
                            theStars.ID = "rt" + stylistReviews.ReviewID.ToString();
                            theStars.StarCssClass = "starRating";
                            theStars.WaitingStarCssClass = "waitingStar";
                            theStars.FilledStarCssClass = "filledStar";
                            theStars.EmptyStarCssClass = "emptyStar";
                            theStars.ReadOnly = true;
                            innerPhForTableCells.Controls.Add(theStars);
                            innerCell.Controls.Add(innerPhForTableCells);

                            innerPhForTableCells = new PlaceHolder();
                            innerPhForTableCells.Controls.Add(new LiteralControl(
                                                    "<br><b>Comment:</b><br>"+
                                                stylistReviews.Comment + "<br><br>"));
                            innerCell.Controls.Add(innerPhForTableCells);

                            innerTable.Rows[counter].Cells.Add(innerCell);

                            counter++;
                            totStylistReviews++;
                        }
                    }
                    catch (Exception err)
                    {
                        TableRow innerRow = new TableRow();
                        innerTable.Rows.Add(innerRow);

                        TableCell innerCell = new TableCell();
                        innerCell.Text = "Reviews unavailable.Please try again later.";

                        innerTable.Rows[0].Cells.Add(innerCell);

                        function.logAnError("GetReviewsOfStylist method error. Error:" + err.ToString());
                    }
                    innerPh.Controls.Add(innerTable);
                    newPanel.Controls.Add(innerPh);


                    ph.Controls.Add(newPanel);
                    newCell.Controls.Add(ph);

                    tblForStylist.Rows[0].Cells.Add(newCell);
                    stylistPanel.Controls.Add(tblForStylist);
                    lblStylistReviewsHeader.Text = "Total number of stylist reviews ("+totStylistReviews+")";
                }
            }
            catch(Exception err)
            {
                Table erTbl = new Table();
                TableRow erRow = new TableRow();
                erTbl.Rows.Add(erRow);
                TableCell erCell = new TableCell();
                erCell.Text = "<img src='https://cdn1.iconfinder.com/data/icons/user-thinline-icons-set/144/User003_Error-512.png' alt='Error' width='100' height='100'></img>"
                    +
                    "<br/>Stylist Reviews are currently unavailable.Please try again later.";
                erTbl.Rows[0].Cells.Add(erCell);
                function.logAnError("error loading stylist reviews. Error: "+err.ToString());
            }
        }
        public void loadAllBookingReviews()
        {
            
            int totBookingReviews = 0;
            try
            {
                r = handler.getAllBookingReviews();
                Table bTable = new Table();
                bTable.CssClass = "table-bordered";
                int count = 0;
                foreach(SP_GetReviews br in r)
                {
                    TableRow newRow = new TableRow();
                    bTable.Rows.Add(newRow);

                    TableCell newCell = new TableCell();

                    PlaceHolder newPH = new PlaceHolder();

                    newPH.Controls.Add(new LiteralControl(
                    "<img src='" + br.CustomerImage +
                    "' alt='Stylist Image' width='200' height='160'/><br/><b>"
                    + br.CustomerName + "</b><br/><span style='text-decoration:underline;'><b>Review Date:<br></span>" 
                    + br.Date.ToString("dd-MM-yyyy")+ "</b><br><br>"));
                    newCell.Controls.Add(newPH);

                    try
                    {
                        bServices = handler.getBookingServices(br.PrimaryBookingID.ToString());
                        var dt = getBookingDateTime(br.PrimaryBookingID.ToString());
                        if (bServices.Count == 1)
                        {
                            newPH = new PlaceHolder();
                            newPH.Controls.Add(new LiteralControl("<span style='text-decoration:underline;'><b>Booking Details:</b></span><br>"
                            + "<a href='ViewProduct.aspx?ProductID=" + bServices[0].ServiceID.Replace(" ", string.Empty) + "'>"
                            + bServices[0].ServiceName.ToString() + "</a>"
                            + " with "
                            + br.StylistName.ToString()
                            + "<br> on " + dt.Item1
                            + " at " + dt.Item2 + "<br><br>"));
                            newCell.Controls.Add(newPH);
                        }
                        else if (bServices.Count == 2)
                        {
                            newPH = new PlaceHolder();
                            newPH.Controls.Add(new LiteralControl("<span style='text-decoration:underline;'><b>Booking Details:</b></span><br>"
                                +"<a href='../ViewBooking.aspx?BookingID=" + br.PrimaryBookingID.ToString().Replace(" ", string.Empty) +
                                "'>" + bServices[0].ServiceName.ToString() +
                                ", " + bServices[1].ServiceName.ToString() + "</a>"
                                + " with "
                                + br.StylistName.ToString()
                                + "<br> on " + dt.Item1
                                + " at " + dt.Item2 + "<br><br>"));
                            newCell.Controls.Add(newPH);
                        }
                        else if (bServices.Count > 2)
                        {
                            string toolTip = "";
                            int toolTipCount = 0;
                            foreach (SP_GetBookingServices toolTipDTL in bServices)
                            {
                                if (toolTipCount == 0)
                                {
                                    toolTip = toolTipDTL.ServiceName;
                                    toolTipCount++;
                                }
                                else
                                {
                                    toolTip += ", " + toolTipDTL.ServiceName;
                                }
                            }
                            newPH = new PlaceHolder();
                            newPH.Controls.Add(new LiteralControl("<span style='text-decoration:underline;'><b>Booking Details:</b></span><br>"
                                +"<a title='" + toolTip + "'" +
                                "href='../ViewBooking.aspx?BookingID=" + br.PrimaryBookingID.ToString().Replace(" ", string.Empty) +
                                "'> Multiple Services </a>"
                                + " with "
                                + br.StylistName.ToString()
                                + "<br> on " + dt.Item1
                                + " at " + dt.Item2 + "<br><br>"));
                            newCell.Controls.Add(newPH);
                        }

                    }
                    catch (Exception Err)
                    {
                        PlaceHolder phE = new PlaceHolder();
                        phE.Controls.Add(new LiteralControl("Booking details currently unavailable."));
                        newCell.Controls.Add(phE);

                        function.logAnError("Error getting booking services for reading booking reviews(my)."
                                + "Error : " + Err.ToString());
                    }

                    bTable.Rows[count].Cells.Add(newCell);

                    newCell = new TableCell();
                    newPH = new PlaceHolder();
                    newPH.Controls.Add(new LiteralControl("<b>Rating given:</b><br>"));
                    newCell.Controls.Add(newPH);

                    newPH = new PlaceHolder();
                    AjaxControlToolkit.Rating theStars = new AjaxControlToolkit.Rating();
                    theStars.CurrentRating = br.Rating;
                    theStars.ID = "rt" + br.ReviewID.ToString();
                    theStars.StarCssClass = "starRating";
                    theStars.WaitingStarCssClass = "waitingStar";
                    theStars.FilledStarCssClass = "filledStar";
                    theStars.EmptyStarCssClass = "emptyStar";
                    theStars.CssClass = "img-fluid";
                    theStars.ReadOnly = true;
                    newPH.Controls.Add(theStars);
                    newCell.Controls.Add(newPH);

                    newPH = new PlaceHolder();
                    newPH.Controls.Add(new LiteralControl("<b>Comment:</b><br>"));
                    newCell.Controls.Add(newPH);

                    newPH = new PlaceHolder();
                    newPH.Controls.Add(new LiteralControl(br.Comment + "<br><br>"));
                    newCell.Controls.Add(newPH);

                    bTable.Rows[count].Cells.Add(newCell);
                    count++;
                    totBookingReviews++;
                }

                lblBookingReviewsHeader.Text="Total number of booking reviews ("+totBookingReviews+")";
                bookingsPanel.Controls.Add(bTable);
            }
            catch(Exception err)
            {
                bookingsPanel.Controls.Add(new LiteralControl("Booking reviews currently unavailable. Please try again later."));
                function.logAnError("Error with loadAllBookingReviews method. Error: " + err.ToString());
            }
        }

        #endregion

        #endregion

        #region Events
        protected void drpReadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cookie = Request.Cookies["CheveuxUserID"];
            if (drpReadType.SelectedValue == "0")
            {
                if (drpRev.SelectedValue == "0")
                {
                    stylistPanel.Visible = true;
                    bookingsPanel.Visible = false;

                    lblBookingReviewsHeader.Visible = false;
                    lblStylistReviewsHeader.Visible = true;
                    loadAllStylistReviews();
                }
                else if (drpRev.SelectedValue == "1")
                {
                    bookingsPanel.Visible = true;
                    stylistPanel.Visible = false;

                    lblStylistReviewsHeader.Visible = false;
                    lblBookingReviewsHeader.Visible = true;
                    loadAllBookingReviews();
                }
            }
            else if (drpReadType.SelectedValue == "1")//view my reviews
            {
                if (cookie == null)
                {
                    phLogin.Visible = true;
                    phMain.Visible = false;
                }
                else if (cookie["UT"] == "C")
                {
                    loadMyReviews(cookie["ID"].ToString());
                    if (drpRev.SelectedValue == "0")
                    {
                        stylistPanel.Visible = true;
                        bookingsPanel.Visible = false;

                        lblBookingReviewsHeader.Visible = false;
                        lblStylistReviewsHeader.Visible = true;
                    }
                    else if (drpRev.SelectedValue == "1")
                    {
                        bookingsPanel.Visible = true;
                        stylistPanel.Visible = false;

                        lblStylistReviewsHeader.Visible = false;
                        lblBookingReviewsHeader.Visible = true;
                    }
                }
                
            }
        }
        protected void drpRev_SelectedIndexChanged(object sender, EventArgs e)
        {
            cookie = Request.Cookies["CheveuxUserID"];
            if (drpReadType.SelectedValue == "0")
            {

                if (drpRev.SelectedValue == "0")//stylist reviews
                {
                    stylistPanel.Visible = true;
                    bookingsPanel.Visible = false;

                    lblBookingReviewsHeader.Visible = false;
                    lblStylistReviewsHeader.Visible = true;
                    loadAllStylistReviews();
                }
                else if (drpRev.SelectedValue == "1")//bookings reviews
                {
                    bookingsPanel.Visible = true;
                    stylistPanel.Visible = false;

                    lblStylistReviewsHeader.Visible = false;
                    lblBookingReviewsHeader.Visible = true;
                    loadAllBookingReviews();
                }
            }
            else if (drpReadType.SelectedValue == "1")
            {
                loadMyReviews(cookie["ID"].ToString());
                if (drpRev.SelectedValue == "0")//stylist reviews
                {
                    stylistPanel.Visible = true;
                    bookingsPanel.Visible = false;

                    lblBookingReviewsHeader.Visible = false;
                    lblStylistReviewsHeader.Visible = true;
                }
                else if (drpRev.SelectedValue == "1")//bookings reviews
                {
                    bookingsPanel.Visible = true;
                    stylistPanel.Visible = false;

                    lblStylistReviewsHeader.Visible = false;
                    lblBookingReviewsHeader.Visible = true;
                }
            }

        }
        #endregion

        #endregion

    }
}