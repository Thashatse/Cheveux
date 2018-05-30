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
            ProductResultsLable.Visible = false;
            StylistResultsLable.Visible = false;
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
                try
                {
                    //run the search function of the BLL to get the results and diplay them 
                    Tuple<List<SP_ProductSearchByTerm>, List<SP_SearchStylistsBySearchTerm>> results = handler.UniversalSearch(searchTerm);
                    //check if there are product result or not
                    if (results.Item1.Count != 0)
                    {
                        //set the product search results heading
                        ProductResultsLable.Visible = true;
                        ProductResultsLable.Text = "<h2> Product Search Results For '" + searchTerm + "' </h2>";
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
                        newHeaderCell.Width = 100;
                        ProductSearchResults.Rows[0].Cells.Add(newHeaderCell);
                        newHeaderCell = new TableHeaderCell();
                        newHeaderCell.Text = "Product Type";
                        newHeaderCell.Width = 200;
                        ProductSearchResults.Rows[0].Cells.Add(newHeaderCell);
                        newHeaderCell = new TableHeaderCell();
                        newHeaderCell.Width = 150;
                        ProductSearchResults.Rows[0].Cells.Add(newHeaderCell);

                        //create a loop to display each result
                        //creat a counter to keep track of the current row
                        int rowCount = 1;
                        foreach(SP_ProductSearchByTerm result in results.Item1)
                        {
                            // create a new row in the results table and set the height
                            newRow = new TableRow();
                            newRow.Height = 50;
                            ProductSearchResults.Rows.Add(newRow);
                            //fill the row with the data from the product results object
                            TableCell newCell = new TableCell();
                            newCell.Text = result.Name.ToString();
                            ProductSearchResults.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = result.ProductDescription.ToString();
                            ProductSearchResults.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = result.Price;
                            ProductSearchResults.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = function.GetFullProductTypeText(result.ProductType);
                            ProductSearchResults.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text =
                                "<button type = 'button' class='btn btn-default'>" +
                                "<a href = 'ViewProduct.aspx?ProductID=" + result.ProductID.ToString().Replace(" ", string.Empty) +
                                "&PreviousPage=Search.aspx?ST=" + searchTerm + "''>View Product</a></button>";
                            ProductSearchResults.Rows[rowCount].Cells.Add(newCell);
                            rowCount++;
                        }
                    }
                    else
                    {
                        //let the user know there are no Product Search results
                        ProductResultsLable.Visible = true;
                        ProductResultsLable.Text = "<h2> No Product Search Results For '" + searchTerm + "' </h2>";
                    }


                    //check if the are Stylist Results or not
                    if (results.Item2.Count != 0)
                    {
                        //set the stylist search results heading
                        StylistResultsLable.Visible = true;
                        StylistResultsLable.Text = "<h2> Stylist Search Results For '" + searchTerm + "' </h2>";
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
                        //creat a counter to keep track of the current row
                        int rowCount = 1;
                        foreach (SP_SearchStylistsBySearchTerm result in results.Item2)
                        {
                            // create a new row in the results table and set the height
                            newRow = new TableRow();
                            newRow.Height = 50;
                            StylistSearchResults.Rows.Add(newRow);
                            //fill the row with the data from the results object
                            TableCell newCell = new TableCell();
                            newCell.Text = "<img src=" + result.StylistImage 
                                + " alt='" + result.StylistFName + " " + result.StylistLName +
                                " Profile Image' width='75' height='75'/>";
                            StylistSearchResults.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = result.StylistFName + " "+result.StylistLName;
                            StylistSearchResults.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text =
                                "<button type = 'button' class='btn btn-default'>" +
                                "<a href = 'ViewStylist.aspx?StylistID=" + result.StylistID.ToString().Replace(" ", string.Empty) +
                                "&PreviousPage=Search.aspx?ST="+searchTerm+"'>View Stylist Profile</a></button>";
                            StylistSearchResults.Rows[rowCount].Cells.Add(newCell);
                            rowCount++;
                        }
                    }
                    else
                    {
                        //let the user know there are no Stylist Search results
                        StylistResultsLable.Visible = true;
                        StylistResultsLable.Visible = true;
                        StylistResultsLable.Text = "<h2> No Stylist Search Results For '" + searchTerm + "' </h2>";
                    }
                }
                catch(ApplicationException Err)
                {
                    function.logAnError(Err.ToString());
                    Response.Redirect("Error.aspx?Error=An Error Occurred Getting Search Results From The Server, Try Again Later");
                }
            }
        }

        
    }
}