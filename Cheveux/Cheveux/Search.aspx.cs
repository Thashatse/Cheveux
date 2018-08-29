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
    public partial class Search : System.Web.UI.Page
    {
        IDBHandler handler = new DBHandler();
        Functions function = new Functions();
        HttpCookie cookie;
        String searchTerm;
        int bookingCount;

        //set the master page based on the user type
        protected void Page_PreInit(Object sender, EventArgs e)
        {
            //check the cheveux user id cookie for the user
            cookie = Request.Cookies["CheveuxUserID"];
            char userType;
            //check if the cookie is empty or not
            if (cookie != null)
            {
                //store the user Type in a variable and display the appropriate master page for the user
                userType = cookie["UT"].ToString()[0];
                //if customer
                if(userType == 'C')
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

        //display the search results from the search term in the query string on statup
        protected void Page_Load(object sender, EventArgs e)
        {
            //get the search term form the querystring
            searchTerm = Request.QueryString["ST"];
            //check if the search term is empty
            if (searchTerm == null || searchTerm == "")
            {
                JumbotronSearchBox.ForeColor = System.Drawing.Color.Red;
                JumbotronSearchBox.Text = "Enter A Search Term";
            }
            else
            {
                //creat a counter to keep track of the current row and result count
                int productCount = 0;
                int serviceCount = 0;
                int stylistRowCount = 0;
                bookingCount = 0;
                try
                {
                    //run the search function of the BLL to get the results and diplay them 
                    Tuple<List<SP_ProductSearchByTerm>, List<SP_SearchStylistsBySearchTerm>> results = handler.UniversalSearch(searchTerm);
                    #region Products
                    //check if there are product result or not
                    if (results.Item1.Count != 0)
                    {
                        TableRow newRow = new TableRow();
                        TableHeaderCell newHeaderCell = new TableHeaderCell();

                        //create a loop to display each result

                        foreach (SP_ProductSearchByTerm result in results.Item1)
                        {
                            //check if it is a service or product
                            //service (Applecation / Service)
                            if (result.ProductType == 'S' || result.ProductType == 'A')
                            {
                                //Service
                                serviceCount++;
                                //check if it the first service and create a table header if it  is
                                if (serviceCount == 1)
                                {
                                    createServiceTableHeader();
                                }
                                // create a new row in the results table and set the height
                                newRow = new TableRow();
                                newRow.Height = 50;
                                serviceSearchResults.Rows.Add(newRow);
                                //fill the row with the data from the product results object
                                TableCell newCell = new TableCell();
                                newCell.Text = "<a class='btn btn-default' href='ViewProduct.aspx?ProductID=" + result.ProductID.ToString().Replace(" ", string.Empty) + "'>" +
                                    result.Name.ToString()+"</a>";
                                serviceSearchResults.Rows[serviceCount].Cells.Add(newCell);
                                newCell = new TableCell();
                                newCell.Text = "<a class='btn btn-default' href='ViewProduct.aspx?ProductID=" + result.ProductID.ToString().Replace(" ", string.Empty) + "'>" +
                                    result.ProductDescription.ToString() + "</a>";
                                serviceSearchResults.Rows[serviceCount].Cells.Add(newCell);
                                newCell = new TableCell();
                                newCell.Text = "R " + result.Price;
                                serviceSearchResults.Rows[serviceCount].Cells.Add(newCell);
                            }
                            //products (Treatments)
                            else if (result.ProductType == 'T')
                            {
                                //Products
                                productCount++;
                                //check if it the first product and create a table header if it  is
                                if (productCount == 1)
                                {
                                    createProductTableHeader();
                                }
                                // create a new row in the results table and set the height
                                newRow = new TableRow();
                                newRow.Height = 50;
                                ProductSearchResults.Rows.Add(newRow);
                                //fill the row with the data from the product results object
                                TableCell newCell = new TableCell();
                                newCell.Text = "<a class='btn btn-default' href='ViewProduct.aspx?ProductID=" + result.ProductID.ToString().Replace(" ", string.Empty) + "'>" + 
                                    result.Name.ToString() + "</a>";
                                ProductSearchResults.Rows[productCount].Cells.Add(newCell);
                                newCell = new TableCell();
                                newCell.Text = "<a class='btn btn-default' href='ViewProduct.aspx?ProductID=" + result.ProductID.ToString().Replace(" ", string.Empty) + "'>" + 
                                    result.ProductDescription.ToString() + "</a>";
                                ProductSearchResults.Rows[productCount].Cells.Add(newCell);
                                newCell = new TableCell();
                                newCell.Text = "R "+result.Price;
                                ProductSearchResults.Rows[productCount].Cells.Add(newCell);
                            }
                            //error
                            else
                            {
                                //Error
                                function.logAnError("Unknown Product Type found in search results");
                            }
                        }
                    }
                    #endregion

                    #region Stylist
                    //check if the are Stylist Results or not
                    if (results.Item2.Count != 0)
                        {
                        //create a new row in the results table and set the height
                        TableRow newRow = new TableRow();
                            newRow.Height = 50;
                            StylistSearchResults.Rows.Add(newRow);
                        //create a header row and set cell withs
                        TableHeaderCell newHeaderCell = new TableHeaderCell();
                            newHeaderCell.Width = 150;
                            StylistSearchResults.Rows[0].Cells.Add(newHeaderCell);
                            newHeaderCell = new TableHeaderCell();
                            newHeaderCell.Text = "Stylist Name";
                            newHeaderCell.Width = 750;
                            StylistSearchResults.Rows[0].Cells.Add(newHeaderCell);
                        newHeaderCell = new TableHeaderCell();
                        newHeaderCell.Text = "Stylist Specialization";
                        newHeaderCell.Width = 750;
                        StylistSearchResults.Rows[0].Cells.Add(newHeaderCell); ;

                        //create a loop to display each result
                        foreach (SP_SearchStylistsBySearchTerm result in results.Item2)
                            {
                                stylistRowCount++;
                                // create a new row in the results table and set the height
                                newRow = new TableRow();
                                newRow.Height = 50;
                                StylistSearchResults.Rows.Add(newRow);
                                //fill the row with the data from the results object
                                TableCell newCell = new TableCell();
                                newCell.Text = "<img src=" + result.StylistImage
                                    + " alt='" + result.StylistFName + " " + result.StylistLName +
                                    " Profile Image' width='75' height='75'/>";
                                StylistSearchResults.Rows[stylistRowCount].Cells.Add(newCell);
                                newCell = new TableCell();
                                newCell.Text = "<a class='btn btn-default' href='Profile.aspx?Action=View&empID=" + result.StylistID.ToString().Replace(" ", string.Empty) + "'>"+ 
                                    result.StylistFName + " " + result.StylistLName+ "</a>";
                                StylistSearchResults.Rows[stylistRowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = "<a class='btn btn-default' href='ViewProduct.aspx?ProductID=" + handler.viewStylistSpecialisationAndBio(result.StylistID.ToString()).serviceID.ToString().Replace(" ", string.Empty) + "'>" +
                                handler.viewStylistSpecialisationAndBio(result.StylistID.ToString()).serviceName.ToString() + "</a>";
                            StylistSearchResults.Rows[stylistRowCount].Cells.Add(newCell);
                        }
                        }
                    #endregion

                    #region Bookings
                    searchBookings(sender, e);
                    #endregion

                    #region count headings
                    //set the headings based on the search results
                    //products heading
                    if (bookingCount != 0)
                    {
                        //set the product search results heading
                        bookingResultLable.Text = "<h2> " + bookingCount + " Booking Search Results For '" + searchTerm + "' </h2>";
                    }
                    //products heading
                    if (productCount != 0)
                {
                    //set the product search results heading
                    ProductResultsLable.Text = "<h2> " + productCount + " Product Search Results For '" + searchTerm + "' </h2>";
                }
                //service heading
                if (serviceCount != 0)
                {
                    //set the product search results heading
                    serviceResultsLable.Text = "<h2> " + serviceCount + " Service Search Results For '" + searchTerm + "' </h2>";
                }
                //Stylist Heading
                if (stylistRowCount != 0)
                {
                    //set the stylist search results heading
                    StylistResultsLable.Text = "<h2> " + stylistRowCount + " Stylist Search Results For '" + searchTerm + "' </h2>";
                }
                //no results
                if(stylistRowCount == 0 && serviceCount == 0 && productCount == 0 && bookingCount == 0)
                {
                    bookingResultLable.Text = "<h2> 0 Search Results For '" + searchTerm + "' </h2>";
                }
                }
                #endregion

                catch (ApplicationException Err)
                {
                    function.logAnError(Err.ToString());
                    serviceResultsLable.Text = "An Error Occurred Getting Search Results From The Server, Try Again Later";
                }


            }
        }

        #region Table Headers
        //create the product table header
        public void createProductTableHeader()
        {
            //Products
            //create a new row in the results table and set the height
            TableRow newRow = new TableRow();
            newRow.Height = 50;
            ProductSearchResults.Rows.Add(newRow);
            //create a header row and set cell withs
            TableHeaderCell newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Product Name";
            newHeaderCell.Width = 600;
            ProductSearchResults.Rows[0].Cells.Add(newHeaderCell);
            newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Product Description";
            newHeaderCell.Width = 750;
            ProductSearchResults.Rows[0].Cells.Add(newHeaderCell);
            newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Price";
            newHeaderCell.Width = 300;
            ProductSearchResults.Rows[0].Cells.Add(newHeaderCell);
            newHeaderCell = new TableHeaderCell();
        }

        //create the service table header
        public void createServiceTableHeader()
        {
            //Services
            //create a new row in the results table and set the height
            TableRow newRow = new TableRow();
            newRow.Height = 50;
            serviceSearchResults.Rows.Add(newRow);
            //create a header row and set cell withs
            TableHeaderCell newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Service Name";
            newHeaderCell.Width = 600;
            serviceSearchResults.Rows[0].Cells.Add(newHeaderCell);
            newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Service Description";
            newHeaderCell.Width = 750;
            serviceSearchResults.Rows[0].Cells.Add(newHeaderCell);
            newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Price";
            newHeaderCell.Width = 300;
            serviceSearchResults.Rows[0].Cells.Add(newHeaderCell);
        }

        //create the Booking table header
        public void createBookingTableHeader()
        {
            //Bookings
            //create a new row in the results table and set the height
            TableRow newRow = new TableRow();
            newRow.Height = 50;
            bookingSearchResults.Rows.Add(newRow);
            //create a header row and set cell withs
            TableHeaderCell newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Date";
            newHeaderCell.Width = 300;
            bookingSearchResults.Rows[0].Cells.Add(newHeaderCell);
            newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Service";
            newHeaderCell.Width = 600;
            bookingSearchResults.Rows[0].Cells.Add(newHeaderCell);
            
            //if customer show stylist
            if (cookie["UT"].ToString()[0] == 'C')
            {
                newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Stylist";
            newHeaderCell.Width = 750;
            bookingSearchResults.Rows[0].Cells.Add(newHeaderCell);
            }
            //if stylist show customer
            else if (cookie["UT"].ToString()[0] == 'S')
            {
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Customer";
                newHeaderCell.Width = 750;
                bookingSearchResults.Rows[0].Cells.Add(newHeaderCell);
            }
            //if receptionist or manager customer and stylist
            else if (cookie["UT"].ToString()[0] == 'R'
                || cookie["UT"].ToString()[0] == 'M')
            {
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Customer";
                newHeaderCell.Width = 750;
                bookingSearchResults.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Stylist";
                newHeaderCell.Width = 750;
                bookingSearchResults.Rows[0].Cells.Add(newHeaderCell);
            }
            newHeaderCell = new TableHeaderCell();
            newHeaderCell.Width = 300;
            bookingSearchResults.Rows[0].Cells.Add(newHeaderCell);
        }
        #endregion

        #region search bookings
        public void searchBookings(object sender, EventArgs e)
        {
            List<SP_GetCustomerBooking> bookingsList = new List<SP_GetCustomerBooking>();
            List<SP_GetBookingServices> bookingServiceList = null;
            bool searchBooking = false;
            //get the search term form the querystring
            searchTerm = Request.QueryString["ST"];

            if (cookie != null)
            {
                if (cookie["UT"].ToString()[0] == 'C' 
                    || cookie["UT"].ToString()[0] == 'R'
                    || cookie["UT"].ToString()[0] == 'S'
                    || cookie["UT"].ToString()[0] == 'M')
                {
                    searchBooking = true;
                    if (!Page.IsPostBack)
                    {
                        CalendarDateStrart.SelectedDate = Convert.ToDateTime("1/1/1753");
                        CalendarDateEnd.SelectedDate = Convert.ToDateTime("12/31/9999");
                    }
                }

                if (searchBooking == true)
                {
                    //add booking to list
                    foreach (SP_GetCustomerBooking book in handler.searchBookings(CalendarDateStrart.SelectedDate, CalendarDateEnd.SelectedDate))
                    {
                        bookingsList.Add(book);
                    }

                    //sort the list by date
                    bookingsList.Sort((x, y) => y.bookingDate.CompareTo(x.bookingDate));

                    if (bookingsList.Count != 0)
                    {
                        foreach (SP_GetCustomerBooking booking in bookingsList)
                        {
                            bool addBooking = false;

                            //if customer show only their bookings
                            if (cookie["UT"].ToString()[0] == 'C' 
                                && booking.CustomerID.ToString().Replace(" ", string.Empty) == cookie["ID"].ToString().Replace(" ", string.Empty))
                            {
                                if (function.compareToSearchTerm(booking.bookingDate.ToString(), searchTerm) == true ||
                                       function.compareToSearchTerm(booking.bookingStartTime.ToString(), searchTerm) == true ||
                                       function.compareToSearchTerm(booking.stylistFirstName.ToString(), searchTerm) == true ||
                                       function.compareToSearchTerm(function.GetFullArrivedStatus(booking.arrived), searchTerm) == true ||
                                       function.compareToSearchTerm(booking.BookingComment.ToString(), searchTerm) == true)
                                {
                                    addBooking = true;
                                }

                                bookingServiceList = handler.getBookingServices(booking.bookingID);
                                foreach (SP_GetBookingServices bookingService in bookingServiceList)
                                {
                                    if (function.compareToSearchTerm(bookingService.ServiceName.ToString(), searchTerm) == true ||
                                        function.compareToSearchTerm(bookingService.serviceDescripion.ToString(), searchTerm) == true)
                                    {
                                        addBooking = true;
                                    }
                                }
                            }
                            //if stylist show only their bookings
                            else if (cookie["UT"].ToString()[0] == 'S'
                                && booking.stylistEmployeeID.ToString().Replace(" ", string.Empty) == cookie["ID"].ToString().Replace(" ", string.Empty))
                            {
                                if (function.compareToSearchTerm(booking.bookingDate.ToString(), searchTerm) == true ||
                                       function.compareToSearchTerm(booking.bookingStartTime.ToString(), searchTerm) == true ||
                                       function.compareToSearchTerm(booking.CustFullName.ToString(), searchTerm) == true ||
                                       function.compareToSearchTerm(booking.BookingComment.ToString(), searchTerm) == true ||
                                       function.compareToSearchTerm(function.GetFullArrivedStatus(booking.arrived), searchTerm) == true)
                                {
                                    addBooking = true;
                                }

                                bookingServiceList = handler.getBookingServices(booking.bookingID);
                                foreach (SP_GetBookingServices bookingService in bookingServiceList)
                                {
                                    if (function.compareToSearchTerm(bookingService.ServiceName.ToString(), searchTerm) == true ||
                                        function.compareToSearchTerm(bookingService.serviceDescripion.ToString(), searchTerm) == true)
                                    {
                                        addBooking = true;
                                    }
                                }
                            }
                            //if receptionist or manager show only their bookings
                            else if (cookie["UT"].ToString()[0] == 'R'
                                || cookie["UT"].ToString()[0] == 'M')
                            {
                                if (function.compareToSearchTerm(booking.bookingDate.ToString(), searchTerm) == true ||
                                       function.compareToSearchTerm(booking.bookingStartTime.ToString(), searchTerm) == true ||
                                       function.compareToSearchTerm(booking.stylistFirstName.ToString(), searchTerm) == true ||
                                       function.compareToSearchTerm(booking.CustFullName.ToString(), searchTerm) == true ||
                                       function.compareToSearchTerm(booking.BookingComment.ToString(), searchTerm) == true ||
                                       function.compareToSearchTerm(function.GetFullArrivedStatus(booking.arrived), searchTerm) == true)
                                {
                                    addBooking = true;
                                }

                                bookingServiceList = handler.getBookingServices(booking.bookingID);
                                foreach (SP_GetBookingServices bookingService in bookingServiceList)
                                {
                                    if (function.compareToSearchTerm(bookingService.ServiceName.ToString(), searchTerm) == true ||
                                        function.compareToSearchTerm(bookingService.serviceDescripion.ToString(), searchTerm) == true)
                                    {
                                        addBooking = true;
                                    }
                                }
                            }

                            if (addBooking == true)
                            {
                                if (bookingCount == 0)
                                {
                                    createBookingTableHeader();
                                    FillterBookingResults.Visible = true;
                                }
                                bookingCount++;
                                // create a new row in the results table and set the height
                                TableRow newRow = new TableRow();
                                newRow.Height = 50;
                                bookingSearchResults.Rows.Add(newRow);
                                //fill the row with the data from the results object
                                //Date
                                TableCell newCell = new TableCell();
                                newCell.Text = booking.bookingDate.ToString("dd-MM-yyyy");
                                bookingSearchResults.Rows[bookingCount].Cells.Add(newCell);
                                //Services
                                newCell = new TableCell();
                                if (bookingServiceList.Count == 1)
                                {
                                    newCell.Text = "<a href='ViewProduct.aspx?ProductID=" + bookingServiceList[0].ServiceID.Replace(" ", string.Empty) + "'>"
                                    + bookingServiceList[0].ServiceName.ToString() + "</a>";
                                }
                                else if (bookingServiceList.Count == 2)
                                {
                                    newCell.Text = "<a href='../ViewBooking.aspx?BookingID=" + booking.bookingID.ToString().Replace(" ", string.Empty) +
                                        "'>" + bookingServiceList[0].ServiceName.ToString() +
                                        ", " + bookingServiceList[1].ServiceName.ToString() + "</a>";
                                }
                                else if (bookingServiceList.Count > 2)
                                {
                                    newCell.Text = "<a href='../ViewBooking.aspx?BookingID=" + booking.bookingID.ToString().Replace(" ", string.Empty) +
                                        "&BookingType=Past'> Multiple </a>";
                                }
                                bookingSearchResults.Rows[bookingCount].Cells.Add(newCell);
                                
                                //if customer show stylist
                                if (cookie["UT"].ToString()[0] == 'C')
                                {
                                    //Stylist
                                    newCell = new TableCell();
                                    newCell.Text = "<a href='Profile.aspx?Action=View" +
                                    "&empID=" + booking.stylistEmployeeID.ToString().Replace(" ", string.Empty) +
                                    "'>" + booking.stylistFirstName.ToString() + "</a>";
                                    bookingSearchResults.Rows[bookingCount].Cells.Add(newCell);
                                }
                                //if stylist show customer
                                else if (cookie["UT"].ToString()[0] == 'S')
                                {
                                    //Customer
                                    newCell = new TableCell();
                                    newCell.Text = "<a href='Profile.aspx?Action=View" +
                                    "&UserID=" + booking.CustomerID.ToString().Replace(" ", string.Empty) +
                                    "'>" + booking.CustFullName.ToString() + "</a>";
                                    bookingSearchResults.Rows[bookingCount].Cells.Add(newCell);
                                }
                                //if receptionist or manager customer and stylist
                                else if (cookie["UT"].ToString()[0] == 'R'
                                    || cookie["UT"].ToString()[0] == 'M')
                                {
                                    //Customer
                                    newCell = new TableCell();
                                    newCell.Text = "<a href='Profile.aspx?Action=View" +
                                    "&UserID=" + booking.CustomerID.ToString().Replace(" ", string.Empty) +
                                    "'>" + booking.CustFullName.ToString() + "</a>";
                                    bookingSearchResults.Rows[bookingCount].Cells.Add(newCell);

                                    //Stylist
                                    newCell = new TableCell();
                                    newCell.Text = "<a href='Profile.aspx?Action=View" +
                                    "&empID=" + booking.stylistEmployeeID.ToString().Replace(" ", string.Empty) +
                                    "'>" + booking.stylistFirstName.ToString() + "</a>";
                                    bookingSearchResults.Rows[bookingCount].Cells.Add(newCell);
                                }

                                //BTN
                                newCell = new TableCell();
                                newCell.Text = "<button type = 'button' class='btn btn-default'>" +
                                    "<a href = '../ViewBooking.aspx?BookingID=" + booking.bookingID.ToString().Replace(" ", string.Empty) +
                                    "&BookingType=Past'" +
                                    ">View Booking</a></button>";
                                bookingSearchResults.Rows[bookingCount].Cells.Add(newCell);
                            }
                        }
                    }
                }
            }
        }

        public void showFilterBooking(object sender, EventArgs e)
        {
            if (divBookingSearchFilter.Visible == true)
            {
                divBookingSearchFilter.Visible = false;
            }
            else
            {
                divBookingSearchFilter.Visible = true;
            }
        }
        #endregion
    }
}