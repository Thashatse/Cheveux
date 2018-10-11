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
        
        List<SP_GetCustomerBooking> bks = null;
        List<SP_GetBookingServices> bServices = null;
        List<SP_ReturnStylistNamesForReview> empNames = null;
        List<SP_GetReviews> r = null;
        List<SP_GetEmpNames> n = null;
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
                    //btnRev.Visible = true;
                    readReviews.Visible = true;
                    makeAreview.Visible = false;

                    drpReadType.Visible = false;
                    drpRev.Visible = true;

                    drpReadType.SelectedValue = "0";

                    if (!Page.IsPostBack)
                    {
                        if (drpReadType.SelectedValue == "0")
                        {
                            loadNames();
                            if (drpRev.SelectedValue == "0")//stylist reviews
                            {
                                stylistPanel.Visible = true;
                                bookingsPanel.Visible = false;
                                names.Visible = true;

                                loadAllStylistReviews(drpsNames.SelectedValue.ToString());
                            }
                            else if (drpRev.SelectedValue == "1")//bookings reviews
                            {
                                bookingsPanel.Visible = true;
                                stylistPanel.Visible = false;
                                names.Visible = false;

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

                    readReviews.Visible = true;
                    makeAreview.Visible = false;

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
                                names.Visible = true;
                                loadNames();
                               
                                loadAllStylistReviews(drpsNames.SelectedValue.ToString());
                            }
                            else if (drpRev.SelectedValue == "1")
                            {
                                bookingsPanel.Visible = true;
                                stylistPanel.Visible = false;

                                loadAllBookingReviews();
                            }
                        }
                        else if (drpReadType.SelectedValue == "1")
                        {
                            loadMyReviews(cookie["ID"].ToString());

                            if (drpRev.SelectedValue == "0")
                            {
                                names.Visible = true;
                                stylistPanel.Visible = true;
                                bookingsPanel.Visible = false;
                            }
                            else if (drpRev.SelectedValue == "1")
                            {
                                names.Visible = false;
                                bookingsPanel.Visible = true;
                                stylistPanel.Visible = false;
                            }
                        }
                    }

                }
                else if (action == "MakeAreview")
                {
                    OpeningHeader.Text = "Write A Review";
                    readReviews.Visible = false;
                    makeAreview.Visible = true;

                    if (drpReviewType.SelectedValue == "0")//review booking
                    {
                        rvBoxHeader.InnerText = "Write a review and rate the service";
                        displayPastBookings(cookie["ID"].ToString());
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
                        hideNoti();
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
                    //btnRev.Visible = false;
                    makeAreview.Visible = false;

                    drpReadType.Visible = false;
                    drpRev.Visible = true;

                    drpReadType.SelectedValue = "0";

                    if (!Page.IsPostBack)
                    {
                        if (drpReadType.SelectedValue == "0")
                        {
                            if (drpRev.SelectedValue == "0")//stylist reviews
                            {
                                stylistPanel.Visible = true;
                                bookingsPanel.Visible = false;
                                names.Visible = true;

                                loadNames();
                                loadAllStylistReviews(drpsNames.SelectedValue.ToString());
                            }
                            else if (drpRev.SelectedValue == "1")//bookings reviews
                            {
                                bookingsPanel.Visible = true;
                                stylistPanel.Visible = false;
                                names.Visible = false;

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
        public void displayPastBookings(string customerID)
        {
            Button btn;
            tblBookings.Rows.Clear();
            choose.Visible = true;
            
            try
            {
                TableRow headerRow = new TableRow();
                tblBookings.Rows.Add(headerRow);
                tblBookings.CssClass = "table table-bordered table-hover";
                TableCell rHeadings = new TableCell();
                rHeadings.Text = "Date";
                rHeadings.Width = 100;
                tblBookings.Rows[0].Cells.Add(rHeadings);

                rHeadings = new TableCell();
                rHeadings.Text = "Time";
                rHeadings.Width = 70;
                tblBookings.Rows[0].Cells.Add(rHeadings);

                rHeadings = new TableCell();
                rHeadings.Text = "Service";
                rHeadings.Width = 120;
                tblBookings.Rows[0].Cells.Add(rHeadings);

                rHeadings = new TableCell();
                rHeadings.Text = "Stylist";
                rHeadings.Width = 100;
                tblBookings.Rows[0].Cells.Add(rHeadings);

                bks = handler.getCustRecentBookings(customerID);
                int count = 1;
                foreach (SP_GetCustomerBooking a in bks)
                {
                    TableRow newRow = new TableRow();
                    tblBookings.Rows.Add(newRow);

                    TableCell newCell = new TableCell();
                    tblBookings.Rows[count].Cells.Add(newCell);
                    newCell.Width = 35;
                    newCell.Text = a.bookingDate.ToString("d MMM");

                    newCell = new TableCell();
                    tblBookings.Rows[count].Cells.Add(newCell);
                    newCell.Width = 35;
                    newCell.Text = a.bookingStartTime.ToString("HH:mm");

                    TableCell services = new TableCell();
                    services.Width = 120;
                    try
                    {
                        bServices = handler.getBookingServices(a.bookingID.ToString());
                        if (bServices.Count == 1)
                        {
                            services.Text = "<a href='../cheveux/services.aspx?ProductID=" + bServices[0].ServiceID.Replace(" ", string.Empty) + "'>"
                            + bServices[0].ServiceName.ToString() + "</a>";
                        }
                        else if(bServices.Count > 1)
                        {
                            string dropDown = "<li style='list-style: none;' class='dropdown'>" +
                                        "<a class='dropdown-toggle' data-toggle='dropdown' href='#'>";
                            if (bServices.Count == 2)
                            {
                                dropDown += bServices[0].ServiceName.ToString() +
                                ", " + bServices[1].ServiceName.ToString();
                            }
                            else if (bServices.Count > 2)
                            {
                                dropDown += " Multiple ";
                            }

                            dropDown += "<span class='caret'></span></a>" +
                                "<ul class='dropdown-menu bg-dark text-white'>";
                            
                            foreach (SP_GetBookingServices service in bServices)
                            {
                                dropDown += "<li>&nbsp;<a href='/cheveux/services.aspx?ProductID=" + service.ServiceID.Replace(" ", string.Empty) + "'>" +
                                " " + service.ServiceName.ToString() + " </a>&nbsp;</li>";
                            }
                            dropDown += "</ul></li>";

                            services.Text = dropDown;
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
                    empCell.Width = 70;
                    try
                    {
                        empCell.Text = "<a href = '../Profile.aspx?Action=View&UserID=" + a.stylistEmployeeID.ToString().Replace(" ", string.Empty) +
                                        "'>" + a.stylistFirstName.ToString() + "</a>";
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

                        lblBookingID.Text = a.bookingID.ToString();
                        lblStylistID.Text = a.stylistEmployeeID.ToString();

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
                newCell.Text = "Your past bookings are currently unavailable.Please try again later.";
                tblBookings.Rows[0].Cells.Add(newCell);
                function.logAnError("Error displaying past bookings method. Error: "+Err.ToString());
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
        public void checkBooking(SP_GetCustomerBooking a)
        {
            try
            {
                //check if booking selected for review has been reviewed
                cookie = Request.Cookies["CheveuxUserID"];
                review = handler.customersReviewForBooking(cookie["ID"].ToString(), a.bookingID.ToString());
                //if its not null the boooking has been reviewed
                if (review != null)
                {
                    //load the review comment in the text area 
                    loadBookingReview(cookie["ID"].ToString(), a.bookingID.ToString());
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
            //reviewComment.InnerHtml = "Your review here....";
            reviewComment.InnerText = string.Empty;

            reviewComment.Attributes.Add("placeholder", "Tell us about your experience...");
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
        protected void drpReviewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cookie = Request.Cookies["CheveuxUserID"];
            if (drpReviewType.SelectedValue == "0")//review booking
            {
                dvStylistNames.Visible = false;
                theReview.Visible = false;
                choose.Visible = true;
                tblBookings.Visible = true;
            }
            else if (drpReviewType.SelectedValue == "1")//review stylist
            {
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
            theReview.Visible = true;
            choose.Visible = false;
            checkStylist();
            viewEmployee(drpStylistNames.SelectedValue.ToString());
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

                    if (Rating1.CurrentRating == 0 && (reviewComment.InnerText == string.Empty || reviewComment.InnerText == "Tell us about your experience..."))
                    {
                        valRating.Visible = true;
                        valComment.Visible = true;
                    }
                    else if (Rating1.CurrentRating == 0 && (reviewComment.InnerText != string.Empty || reviewComment.InnerText != "Tell us about your experience..."))
                    {
                        valRating.Visible = true;
                        valComment.Visible = false;
                    }
                    else if (reviewComment.InnerText == string.Empty || reviewComment.InnerText == "Tell us about your experience..." && Rating1.CurrentRating > 0)
                    {
                        valComment.Visible = true;
                        valRating.Visible = false;
                    }
                    else
                    {
                        if (handler.reviewBooking(review))
                        {
                            valRating.Visible = false;
                            valComment.Visible = false;
                            Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview&noti=s");
                        }
                        else
                        {
                            valRating.Visible = false;
                            valComment.Visible = false;
                            Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview&noti=f");
                        }
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

                    if (Rating1.CurrentRating == 0 && (reviewComment.InnerText == string.Empty || reviewComment.InnerText == "Tell us about your experience..."))
                    {
                        valRating.Visible = true;
                        valComment.Visible = true;
                    }
                    else if (Rating1.CurrentRating == 0 && (reviewComment.InnerText != string.Empty || reviewComment.InnerText != "Tell us about your experience..."))
                    {
                        valRating.Visible = true;
                        valComment.Visible = false;
                    }
                    else if (reviewComment.InnerText == string.Empty || reviewComment.InnerText == "Tell us about your experience..." && Rating1.CurrentRating > 0)
                    {
                        valComment.Visible = true;
                        valRating.Visible = false;
                    }
                    else
                    {
                        if (handler.reviewStylist(review))
                        {
                            valRating.Visible = false;
                            valComment.Visible = false;
                            Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview&noti=s");
                        }
                        else
                        {
                            valRating.Visible = false;
                            valComment.Visible = false;
                            Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview&noti=f");
                        }
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


                    if (Rating1.CurrentRating == 0 && (reviewComment.InnerText == string.Empty || reviewComment.InnerText == "Tell us about your experience..."))
                    {
                        valRating.Visible = true;
                        valComment.Visible = true;
                    }
                    else if (Rating1.CurrentRating == 0 && (reviewComment.InnerText != string.Empty || reviewComment.InnerText != "Tell us about your experience..."))
                    {
                        valRating.Visible = true;
                        valComment.Visible = false;
                    }
                    else if (reviewComment.InnerText == string.Empty || reviewComment.InnerText == "Tell us about your experience..." && Rating1.CurrentRating > 0)
                    {
                        valComment.Visible = true;
                        valRating.Visible = false;
                    }
                    else
                    {
                        if (handler.updateBookingReview(review))
                        {
                            valRating.Visible = false;
                            valComment.Visible = false;
                            Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview&noti=s");
                        }
                        else
                        {
                            valRating.Visible = false;
                            valComment.Visible = false;
                            Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview&noti=f");
                        }
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

                    if (Rating1.CurrentRating == 0 && (reviewComment.InnerText == string.Empty || reviewComment.InnerText == "Tell us about your experience..."))
                    {
                        valRating.Visible = true;
                        valComment.Visible = true;
                    }
                    else if(Rating1.CurrentRating== 0 && (reviewComment.InnerText != string.Empty || reviewComment.InnerText != "Tell us about your experience..."))
                    {
                        valRating.Visible = true;
                        valComment.Visible = false;
                    }
                    else if (reviewComment.InnerText == string.Empty || reviewComment.InnerText == "Tell us about your experience..." && Rating1.CurrentRating>0)
                    {
                        valComment.Visible = true;
                        valRating.Visible = false;
                    }
                    else
                    {
                        if (handler.updateStylistReview(review))
                        {
                            valRating.Visible = false;
                            valComment.Visible = false;
                            Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview&noti=s");
                        }
                        else
                        {
                            valRating.Visible = false;
                            valComment.Visible = false;
                            Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview&noti=f");
                        }
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
            try
            {
                r = handler.getCustomersReviews(customerID);
                foreach (SP_GetReviews a in r)
                {
                    if (string.IsNullOrEmpty(a.PrimaryBookingID))//stylist reviews
                    {
                        Table tblStylistReviews = new Table();
                        tblStylistReviews.CssClass = "table-bordered";

                        TableRow newRow = new TableRow();
                        tblStylistReviews.Rows.Add(newRow);

                        TableCell newCell = new TableCell();
                        newCell.Width = 150;

                        newCell.Text = "<img src='" + a.StylistImage +
                                    "' alt='Stylist Image' width='200' height='160'/><br/>"
                                    + "Review Date:" + a.Date.ToString("dd-MM-yyyy");
                        newCell.Font.Bold = true;
                        tblStylistReviews.Rows[0].Cells.Add(newCell);

                        newCell = new TableCell();
                        newCell.Width = 600;

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

                        ph = new PlaceHolder();
                        ph.Controls.Add(new LiteralControl("<br>"));
                        newCell.Controls.Add(ph);

                        //Customer review of the stylist
                        ph = new PlaceHolder();
                        ph.Controls.Add(new LiteralControl(
                                "<b>Comment:</b><br>" +
                            a.Comment + "<br><br>"));
                        newCell.Controls.Add(ph);

                        tblStylistReviews.Rows[0].Cells.Add(newCell);
                        stylistPanel.Controls.Add(tblStylistReviews);
                    }
                    else if (!string.IsNullOrEmpty(a.PrimaryBookingID))//booking reviews
                    {
                        int bCount = 0;

                        //create table
                        Table tblBookings = new Table();
                        tblBookings.CssClass = "table-bordered";

                        TableRow newRow = new TableRow();
                        tblBookings.Rows.Add(newRow);

                        TableCell newCell = new TableCell();
                        newCell.Width = 600;

                        PlaceHolder ph;

                        //get services
                        try
                        {
                            bServices = handler.getBookingServices(a.PrimaryBookingID.ToString());
                            var dt = getBookingDateTime(a.PrimaryBookingID.ToString());
                            if (bServices.Count == 1)
                            {
                                ph = new PlaceHolder();
                                ph.Controls.Add(new LiteralControl(
                                    "<span style='text-decoration:underline;'><b>Booking Details:</b></span><br>" +
                                "<a href='../cheveux/services.aspx?ProductID=" + bServices[0].ServiceID.Replace(" ", string.Empty) + "'>"
                                + bServices[0].ServiceName.ToString() + "</a>"
                                + " with "
                                + a.StylistName.ToString()
                                + "<br>on " + dt.Item1
                                + " at " + dt.Item2 + "<br><br>"));
                                newCell.Controls.Add(ph);
                            }
                            else if (bServices.Count > 1)
                            {
                                string dropDown = "<li style='list-style: none;' class='dropdown'>" +
                                        "<a class='dropdown-toggle' data-toggle='dropdown' href='#'>";

                                if (bServices.Count == 2)
                                {
                                    dropDown +=
                                            bServices[0].ServiceName.ToString() +
                                            ", " + bServices[1].ServiceName.ToString();
                                    dropDown += "<span class='caret'></span></a>" +
                                    "<ul class='dropdown-menu bg-dark text-white'>";
                                    foreach (SP_GetBookingServices service in bServices)
                                    {
                                        dropDown += "<li>&nbsp;<a href='/cheveux/services.aspx?ProductID=" + service.ServiceID.Replace(" ", string.Empty) + "'>" +
                                        " " + service.ServiceName.ToString() + " </a>&nbsp;</li>";
                                    }
                                    dropDown += "</ul></li>";

                                    ph = new PlaceHolder();
                                    ph.Controls.Add(new LiteralControl(
                                        "<span style='text-decoration:underline;'><b>Booking Details:</b></span><br>" +
                                                        dropDown
                                                        + " with "
                                            + a.StylistName.ToString()
                                            + "<br>on " + dt.Item1
                                            + " at " + dt.Item2 + "<br><br>"));
                                    newCell.Controls.Add(ph);
                                }
                                else if (bServices.Count > 2)
                                {
                                    dropDown += " Multiple Services";

                                    dropDown += "<span class='caret'></span></a>" +
                                        "<ul class='dropdown-menu bg-dark text-white'>";
                                    ph = new PlaceHolder();
                                    foreach (SP_GetBookingServices service in bServices)
                                    {
                                        dropDown += "<li>&nbsp;<a href='/cheveux/services.aspx?ProductID=" + service.ServiceID.Replace(" ", string.Empty) + "'>" +
                                        " " + service.ServiceName.ToString() + " </a>&nbsp;</li>";
                                    }
                                    dropDown += "</ul></li>";
                                    ph.Controls.Add(new LiteralControl("<span style='text-decoration:underline;'><b>Booking Details:</b></span><br>" +
                                                    dropDown
                                                    + " with "
                                                    + a.StylistName.ToString()
                                                    + "<br>on " + dt.Item1
                                                    + " at " + dt.Item2 + "<br><br>"));
                                    newCell.Controls.Add(ph);
                                }
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

                        //rating given heaing 
                        ph = new PlaceHolder();
                        ph.Controls.Add(new LiteralControl(
                                        "<b>Rating given:</b><br>"));
                        newCell.Controls.Add(ph);

                        //add rating given (stars)
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

                        //line break for spacing
                        ph = new PlaceHolder();
                        ph.Controls.Add(new LiteralControl("<br><br><br>"));
                        newCell.Controls.Add(ph);


                        //comment header 
                        ph = new PlaceHolder();
                        ph.Controls.Add(new LiteralControl("<b>Comment: </b><br>"));
                        newCell.Controls.Add(ph);

                        //add comment to placeholder
                        ph = new PlaceHolder();
                        ph.Controls.Add(new LiteralControl(a.Comment.ToString() + "<br><br>"));
                        newCell.Controls.Add(ph);

                        //add cell to table row 
                        tblBookings.Rows[bCount].Cells.Add(newCell);
                        bCount++;

                        //add table to panel
                        bookingsPanel.Controls.Add(tblBookings);
                    }
                }
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
        #endregion

        #region General Reviews
        public void loadAllStylistReviews(string stylistID)
        {
            tblViewEmployee.Rows.Clear();
            tblStylistReviews.Rows.Clear();

            //load employee
            try
            {
                viewEmp = handler.viewEmployee(stylistID);

                TableRow newRow = new TableRow();
                tblViewEmployee.Rows.Add(newRow);

                TableCell newCell = new TableCell();
                PlaceHolder ph = new PlaceHolder();
                ph.Controls.Add(new LiteralControl(
                    "<img src=" + viewEmp.empImage + " alt='Stylist Image'"
                    + "width='120' height='120/><br>"));
                newCell.Controls.Add(ph);
                tblViewEmployee.Rows[0].Cells.Add(newCell);

                try
                {
                    newRow = new TableRow();
                    tblViewEmployee.Rows.Add(newRow);
                    newCell = new TableCell();
                    review = handler.getStylistRating(stylistID);
                    ph = new PlaceHolder();
                    ph.Controls.Add(new LiteralControl("Stylist's average Rating:"));
                    newCell.Controls.Add(ph);
                    tblViewEmployee.Rows[1].Cells.Add(newCell);

                    newRow = new TableRow();
                    tblViewEmployee.Rows.Add(newRow);
                    newCell = new TableCell();
                    ph = new PlaceHolder();
                    AjaxControlToolkit.Rating theStars = new AjaxControlToolkit.Rating();
                    theStars.CurrentRating = review.Rating;
                    theStars.ID = "rt" + drpsNames.SelectedValue;
                    theStars.StarCssClass = "starRating";
                    theStars.WaitingStarCssClass = "waitingStar";
                    theStars.FilledStarCssClass = "filledStar";
                    theStars.EmptyStarCssClass = "emptyStar";
                    theStars.CssClass = "img-fluid";
                    theStars.ReadOnly = true;
                    ph.Controls.Add(theStars);
                    newCell.Controls.Add(ph);
                    tblViewEmployee.Rows[2].Cells.Add(newCell);
                }
                catch (Exception err)
                {
                    newRow = new TableRow();
                    tblViewEmployee.Rows.Add(newRow);
                    newCell = new TableCell();
                    ph = new PlaceHolder();
                    ph.Controls.Add(new LiteralControl("Rating unavailable"));
                    newCell.Controls.Add(ph);
                    tblViewEmployee.Rows[1].Cells.Add(newCell);
                    function.logAnError("Error getting stylist rating. Error:" + err.ToString());
                }


            }
            catch (Exception err)
            {
                TableRow newRow = new TableRow();
                tblViewEmployee.Rows.Add(newRow);

                TableCell newCell = new TableCell();
                newCell.Text = "<img src='https://cdn1.iconfinder.com/data/icons/user-thinline-icons-set/144/User003_Error-512.png' alt='Error' width='50' height='50'></img>";

                tblViewEmployee.Rows[0].Cells.Add(newCell);

                function.logAnError("Error getting stylist.Error: " + err.ToString());
            }

            //load reviews of employee
            tblStylistReviews.CssClass = "table-bordered";
            
            int count = 0;
            try
            {
                r = handler.getReviewsOfStylist(stylistID);


                    foreach (SP_GetReviews stylistReviews in r)
                    {
                        TableRow revRow = new TableRow();
                        tblStylistReviews.Rows.Add(revRow);

                        TableCell newCell = new TableCell();
                        newCell.Text = "<img src=" + stylistReviews.CustomerImage
                                      + " alt='" + stylistReviews.CustomerName +
                                     " Profile Image' width='80' height='80'/>&nbsp;&nbsp;&nbsp;<br>";
                        tblStylistReviews.Rows[count].Cells.Add(newCell);

                        newCell = new TableCell();
                        newCell.Width = 600;
                        PlaceHolder ph = new PlaceHolder();
                        ph.Controls.Add(new LiteralControl(
                            "<b>Review by:</b><br>" +
                            stylistReviews.CustomerName+ "<br>"));
                        newCell.Controls.Add(ph);

                        ph = new PlaceHolder();
                        ph.Controls.Add(new LiteralControl("<br><b>Rating given:</b><br>"));
                        newCell.Controls.Add(ph);

                        ph = new PlaceHolder();
                        AjaxControlToolkit.Rating theStars = new AjaxControlToolkit.Rating();
                        theStars.CurrentRating = stylistReviews.Rating;
                        theStars.ID = "rtM" + stylistReviews.ReviewID.ToString();
                        theStars.StarCssClass = "starRating";
                        theStars.WaitingStarCssClass = "waitingStar";
                        theStars.FilledStarCssClass = "filledStar";
                        theStars.EmptyStarCssClass = "emptyStar";
                        theStars.ReadOnly = true;
                        theStars.CssClass = "img-fluid";
                        ph.Controls.Add(theStars);
                        newCell.Controls.Add(ph);

                        ph = new PlaceHolder();
                        ph.Controls.Add(new LiteralControl("<br>"));
                        newCell.Controls.Add(ph);

                        ph = new PlaceHolder();
                        ph.Controls.Add(new LiteralControl(
                                "<b>Comment:</b><br>" +
                            stylistReviews.Comment + "<br><br>"));
                        newCell.Controls.Add(ph);

                        tblStylistReviews.Rows[count].Cells.Add(newCell);

                        count++;
                    }  
                }
            catch(Exception err)
            {
                TableRow erRow = new TableRow();
                tblStylistReviews.Rows.Add(erRow);
                TableCell erCell = new TableCell();
                erCell.Text = "Stylist Reviews are currently unavailable.Please try again later.";
                tblStylistReviews.Rows[0].Cells.Add(erCell);
                stylistPanel.Controls.Add(tblStylistReviews);
                function.logAnError("error loading stylist reviews. Error: " + err.ToString());
            }
        }
        public void loadAllBookingReviews()
        {
            try
            {
                r = handler.getAllBookingReviews();
                    Table bTable = new Table();
                    bTable.CssClass = "table-bordered";
                    int count = 0;
                    foreach (SP_GetReviews br in r)
                    {
                        TableRow newRow = new TableRow();
                        bTable.Rows.Add(newRow);

                        TableCell newCell = new TableCell();
                        newCell.Width = 150;
                        PlaceHolder newPH = new PlaceHolder();

                        newPH.Controls.Add(new LiteralControl(
                        "<img src='" + br.CustomerImage +
                        "' alt='Stylist Image' width='80' height='80'/>&nbsp;&nbsp;&nbsp;<br/><br/><b>"
                        + br.CustomerName + "</b><br/><br/> Review Date:<br>"
                        + br.Date.ToString("dd-MM-yyyy") + "</b><br><br>"));
                        newCell.Controls.Add(newPH);
                        bTable.Rows[count].Cells.Add(newCell);

                        newCell = new TableCell();
                        newCell.Width = 600;
                        try
                        {
                            bServices = handler.getBookingServices(br.PrimaryBookingID.ToString());
                            var dt = getBookingDateTime(br.PrimaryBookingID.ToString());
                            if (bServices.Count == 1)
                            {
                                newPH = new PlaceHolder();
                                newPH.Controls.Add(new LiteralControl(
                                    "<span style='text-decoration:underline;'><b>Booking Details:</b></span><br>" +
                                "<a href='../cheveux/services.aspx?ProductID=" + bServices[0].ServiceID.Replace(" ", string.Empty) + "'>"
                                + bServices[0].ServiceName.ToString() + "</a>"
                                + " with "
                                + br.StylistName.ToString()
                                + "<br>on " + dt.Item1
                                + " at " + dt.Item2 + "<br><br>"));
                                newCell.Controls.Add(newPH);
                            }
                            else if (bServices.Count > 1)
                            {
                                string dropDown = "<li style='list-style: none;' class='dropdown'>" +
                                        "<a class='dropdown-toggle' data-toggle='dropdown' href='#'>";

                                if (bServices.Count == 2)
                                {
                                    dropDown +=
                                            bServices[0].ServiceName.ToString() +
                                            ", " + bServices[1].ServiceName.ToString();
                                    dropDown += "<span class='caret'></span></a>" +
                                    "<ul class='dropdown-menu bg-dark text-white'>";
                                    foreach (SP_GetBookingServices service in bServices)
                                    {
                                        dropDown += "<li>&nbsp;<a href='/cheveux/services.aspx?ProductID=" + service.ServiceID.Replace(" ", string.Empty) + "'>" +
                                        " " + service.ServiceName.ToString() + " </a>&nbsp;</li>";
                                    }
                                    dropDown += "</ul></li>";

                                    newPH = new PlaceHolder();
                                    newPH.Controls.Add(new LiteralControl(
                                        "<span style='text-decoration:underline;'><b>Booking Details:</b></span><br>" +
                                                        dropDown
                                                        + " with "
                                            + br.StylistName.ToString()
                                            + "<br>on " + dt.Item1
                                            + " at " + dt.Item2 + "<br><br>"));
                                    newCell.Controls.Add(newPH);
                                }
                                else if (bServices.Count > 2)
                                {
                                    dropDown += " Multiple Services";

                                    dropDown += "<span class='caret'></span></a>" +
                                        "<ul class='dropdown-menu bg-dark text-white'>";
                                    newPH = new PlaceHolder();
                                    foreach (SP_GetBookingServices service in bServices)
                                    {
                                        dropDown += "<li>&nbsp;<a href='/cheveux/services.aspx?ProductID=" + service.ServiceID.Replace(" ", string.Empty) + "'>" +
                                        " " + service.ServiceName.ToString() + " </a>&nbsp;</li>";
                                    }
                                    dropDown += "</ul></li>";
                                    newPH.Controls.Add(new LiteralControl("<span style='text-decoration:underline;'><b>Booking Details:</b></span><br>" +
                                                    dropDown
                                                    + " with "
                                                    + br.StylistName.ToString()
                                                    + "<br>on " + dt.Item1
                                                    + " at " + dt.Item2 + "<br><br>"));
                                    newCell.Controls.Add(newPH);
                                }
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
                        newPH.Controls.Add(new LiteralControl("<br>"));
                        newCell.Controls.Add(newPH);

                        newPH = new PlaceHolder();
                        newPH.Controls.Add(new LiteralControl("<b>Comment:</b><br>"));
                        newCell.Controls.Add(newPH);

                        newPH = new PlaceHolder();
                        newPH.Controls.Add(new LiteralControl(br.Comment + "<br><br>"));
                        newCell.Controls.Add(newPH);

                        bTable.Rows[count].Cells.Add(newCell);
                        count++;

                    }

                    bookingsPanel.Controls.Add(bTable);
                
            }
            catch (Exception err)
            {
                bookingsPanel.Controls.Add(new LiteralControl("Booking reviews currently unavailable. Please try again later."));
                function.logAnError("Error with loadAllBookingReviews method. Error: " + err.ToString());
            }
        }
        #endregion

        public void loadNames()
        {
            try
            {
                n = handler.BLL_GetEmpNames();
                foreach (SP_GetEmpNames emp in n)
                {
                    drpsNames.DataSource = n;
                    drpsNames.DataTextField = "Name";
                    drpsNames.DataValueField = "EmployeeID";
                    drpsNames.DataBind();
                }
            }
            catch (Exception err)
            {
                drpsNames.Items.Add("Unable to retrieve names");
                function.logAnError("Error on read reviews page.loadNames method. Error: "
                    + err.ToString());
            }
        }

        #endregion

        #region Events
        protected void drpReadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cookie = Request.Cookies["CheveuxUserID"];
            if (drpReadType.SelectedValue == "0")
            {
                names.Visible = true;
                if (drpRev.SelectedValue == "0")
                {
                    stylistPanel.Visible = true;
                    bookingsPanel.Visible = false;

                    loadAllStylistReviews(drpsNames.SelectedValue.ToString());
                }
                else if (drpRev.SelectedValue == "1")
                {
                    bookingsPanel.Visible = true;
                    stylistPanel.Visible = false;

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
                    names.Visible = false;

                    loadMyReviews(cookie["ID"].ToString());
                    if (drpRev.SelectedValue == "0")
                    {
                        stylistPanel.Visible = true;
                        bookingsPanel.Visible = false;
                        

                    }
                    else if (drpRev.SelectedValue == "1")
                    {
                        bookingsPanel.Visible = true;
                        stylistPanel.Visible = false;
                        
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
                    names.Visible = true;

                    loadNames();
                    loadAllStylistReviews(drpsNames.SelectedValue.ToString());
                }
                else if (drpRev.SelectedValue == "1")//bookings reviews
                {
                    bookingsPanel.Visible = true;
                    stylistPanel.Visible = false;
                    names.Visible = false;

                    loadAllBookingReviews();
                }
            }
            else if (drpReadType.SelectedValue == "1")
            {
                loadMyReviews(cookie["ID"].ToString());

                if (drpRev.SelectedValue == "0")//stylist reviews
                {
                    if(cookie["UT"] == "C")
                    {
                        stylistPanel.Visible = true;
                        bookingsPanel.Visible = false;
                        names.Visible = false;
                    }
                    else
                    {
                        stylistPanel.Visible = true;
                        bookingsPanel.Visible = false;
                        names.Visible = true;
                    } 
                }
                else if (drpRev.SelectedValue == "1")//bookings reviews
                {
                    bookingsPanel.Visible = true;
                    stylistPanel.Visible = false;
                    names.Visible = false;
                }
            }
        }
            
        protected void drpsNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadAllStylistReviews(drpsNames.SelectedValue.ToString());
        }
        #endregion

        #endregion


    }
}