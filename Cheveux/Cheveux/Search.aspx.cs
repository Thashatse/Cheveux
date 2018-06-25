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

        //set the master page based on the user type
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
            String searchTerm = Request.QueryString["ST"];
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
                try
                {
                    //run the search function of the BLL to get the results and diplay them 
                    Tuple<List<SP_ProductSearchByTerm>, List<SP_SearchStylistsBySearchTerm>> results = handler.UniversalSearch(searchTerm);
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
                                newCell.Text = result.Name.ToString();
                                serviceSearchResults.Rows[serviceCount].Cells.Add(newCell);
                                newCell = new TableCell();
                                newCell.Text = result.ProductDescription.ToString();
                                serviceSearchResults.Rows[serviceCount].Cells.Add(newCell);
                                newCell = new TableCell();
                                newCell.Text = "R " + result.Price;
                                serviceSearchResults.Rows[serviceCount].Cells.Add(newCell);
                                newCell = new TableCell();
                                newCell.Text = function.GetFullProductTypeText(result.ProductType);
                                serviceSearchResults.Rows[serviceCount].Cells.Add(newCell);
                                newCell = new TableCell();
                                newCell.Text =
                                    "<button type = 'button' class='btn btn-default'>" +
                                    "<a href = 'ViewProduct.aspx?ProductID=" + result.ProductID.ToString().Replace(" ", string.Empty) +
                                    "&PreviousPage=Search.aspx?ST=" + searchTerm + "''>View Service</a></button>";
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
                                newCell.Text = result.Name.ToString();
                                ProductSearchResults.Rows[productCount].Cells.Add(newCell);
                                newCell = new TableCell();
                                newCell.Text = result.ProductDescription.ToString();
                                ProductSearchResults.Rows[productCount].Cells.Add(newCell);
                                newCell = new TableCell();
                                newCell.Text = "R "+result.Price;
                                ProductSearchResults.Rows[productCount].Cells.Add(newCell);
                                newCell = new TableCell();
                                newCell.Text =
                                    "<button type = 'button' class='btn btn-default'>" +
                                    "<a href = 'ViewProduct.aspx?ProductID=" + result.ProductID.ToString().Replace(" ", string.Empty) +
                                    "&PreviousPage=Search.aspx?ST=" + searchTerm + "''>View Product</a></button>";
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
                        
                        //check if the are Stylist Results or not
                        if (results.Item2.Count != 0)
                        {
                        //create a new row in the results table and set the height
                        TableRow newRow = new TableRow();
                            newRow.Height = 50;
                            StylistSearchResults.Rows.Add(newRow);
                        //create a header row and set cell withs
                        TableHeaderCell newHeaderCell = new TableHeaderCell();
                            newHeaderCell.Width = 400;
                            StylistSearchResults.Rows[0].Cells.Add(newHeaderCell);
                            newHeaderCell = new TableHeaderCell();
                            newHeaderCell.Text = "Stylist Name";
                            newHeaderCell.Width = 450;
                            StylistSearchResults.Rows[0].Cells.Add(newHeaderCell);
                            newHeaderCell = new TableHeaderCell();
                            newHeaderCell.Width = 150;
                            StylistSearchResults.Rows[0].Cells.Add(newHeaderCell);

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
                                newCell.Text = result.StylistFName + " " + result.StylistLName;
                                StylistSearchResults.Rows[stylistRowCount].Cells.Add(newCell);
                                newCell = new TableCell();
                                newCell.Text =
                                    "<button type = 'button' class='btn btn-default'>" +
                                    "<a href = 'Profile.aspx?Action=View" +
                                    "&empID=" + result.StylistID.ToString().Replace(" ", string.Empty) +
                                    "&PreviousPage=Search.aspx?ST=" + searchTerm + "'>View Stylist Profile</a></button>";
                                StylistSearchResults.Rows[stylistRowCount].Cells.Add(newCell);
                            }
                        }
                 
                }
                catch (ApplicationException Err)
                {
                    function.logAnError(Err.ToString());
                    Response.Redirect("Error.aspx?Error=An Error Occurred Getting Search Results From The Server, Try Again Later");
                }

                //set the headings based on the search results
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
            }
        }

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
            newHeaderCell.Width = 600;
            ProductSearchResults.Rows[0].Cells.Add(newHeaderCell);
            newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Price";
            newHeaderCell.Width = 300;
            ProductSearchResults.Rows[0].Cells.Add(newHeaderCell);
            newHeaderCell = new TableHeaderCell();
            newHeaderCell.Width = 150;
            ProductSearchResults.Rows[0].Cells.Add(newHeaderCell);
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
            newHeaderCell.Width = 600;
            serviceSearchResults.Rows[0].Cells.Add(newHeaderCell);
            newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Price";
            newHeaderCell.Width = 100;
            serviceSearchResults.Rows[0].Cells.Add(newHeaderCell);
            newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Service Type";
            newHeaderCell.Width = 200;
            serviceSearchResults.Rows[0].Cells.Add(newHeaderCell);
            newHeaderCell = new TableHeaderCell();
            newHeaderCell.Width = 150;
            serviceSearchResults.Rows[0].Cells.Add(newHeaderCell);
        }
    }
}