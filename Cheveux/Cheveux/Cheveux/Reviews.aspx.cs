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
        List<SP_GetStylistBookings> customer = null;
        List<SP_GetBookingServices> bServices = null;
        REVIEW review = null;
        List<SP_ReturnStylistNamesForReview> empNames = null;
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

                    if (Page.IsPostBack)
                    {
                        displayPastBookings(cookie["ID"].ToString(), calDay.SelectedDate);
                    }

                    //Set the month dropdownlist to the current month by defualt on page load
                    int index = DateTime.Today.Month;
                    drpMonth.SelectedValue = index.ToString();
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
        public void displayPastBookings(string customerID,DateTime day)
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
                customer = handler.getCustomerPastBookingsForDate(customerID,day);
                int count = 1;
                foreach (SP_GetStylistBookings a in customer)
                {
                    TableRow newRow = new TableRow();
                    tblBookings.Rows.Add(newRow);

                    TableCell newCell = new TableCell();
                    tblBookings.Rows[count].Cells.Add(newCell);
                    newCell.Width = 50;
                    newCell.Text = a.StartTime.ToString("HH:mm");

                    //getServiceAndStylist(count, a);

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
                        services.Text = "<img src='https://cdn4.iconfinder.com/data/icons/smiley-vol-3-2/48/134-512.png' alt='Error' width='10' height='10'></img>";
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
                        lblBookingID.Text = a.PrimaryID.ToString();
                        lblCustID.Text = a.CustomerID.ToString();
                        lblStylistID.Text = a.StylistID.ToString();
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

        #region Events
        protected void calDay_SelectionChanged(object sender, EventArgs e)
        {
            theReview.Visible = false;
            cookie = Request.Cookies["CheveuxUserID"];
            lsBksHeader.InnerText = "Your Booking(s) on "
                                + calDay.SelectedDate.ToString("dd-MM-yyyy");
            displayPastBookings(cookie["ID"].ToString(),calDay.SelectedDate);
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
            cookie = Request.Cookies["CheveuxUserID"];

            if (drpReviewType.SelectedValue == "0")//review booking
            {
                datepick.Visible = true;
                dvStylistNames.Visible = false;
            }
            else if (drpReviewType.SelectedValue == "1")//review stylist
            {
                datepick.Visible = false;
                theReview.Visible = true;
                lblCustID.Text= cookie["ID"].ToString();
                dvStylistNames.Visible = true;
                try
                {
                    empNames = handler.returnStylistNamesForReview(cookie["ID"].ToString());
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
            try
            {
                review = new REVIEW();
                cookie = Request.Cookies["CheveuxUserID"];

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
                    Response.Redirect("../Cheveux/Reviews.aspx?Action=MakeAreview");
                }
                else
                {
                    //temporary
                    Response.Redirect("../Cheveux/Reviews.aspx?Action=ReadReviews");
                }
            }
            catch (Exception Err)
            {
                Response.Redirect("../Default.aspx");//temporary
                function.logAnError(Err.ToString());
            }
        }
        #endregion

    }
}