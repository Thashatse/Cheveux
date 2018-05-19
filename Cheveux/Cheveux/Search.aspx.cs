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
            ResultsLable.Visible = false;
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
                    List<SP_ProductSearchByTerm> results = handler.UniversalSearch(searchTerm);
                    //check if there are result or not
                    if (results.Count != 0)
                    {
                        ResultsLable.Visible = true;
                        ResultsLable.Text = "<h2> Search Results For '" + searchTerm + "' </h2>";
                        //create a new row in the results table and set the height
                        TableRow newRow = new TableRow();
                        newRow.Height = 50;
                        SearchResults.Rows.Add(newRow);
                        //create a header row and set cell withs
                        TableHeaderCell newHeaderCell = new TableHeaderCell();
                        newHeaderCell.Text = "Product Name";
                        newHeaderCell.Width = 600;
                        SearchResults.Rows[0].Cells.Add(newHeaderCell);
                        newHeaderCell = new TableHeaderCell();
                        newHeaderCell.Text = "Product Description";
                        newHeaderCell.Width = 600;
                        SearchResults.Rows[0].Cells.Add(newHeaderCell);
                        newHeaderCell = new TableHeaderCell();
                        newHeaderCell.Text = "Price";
                        newHeaderCell.Width = 100;
                        SearchResults.Rows[0].Cells.Add(newHeaderCell);
                        newHeaderCell = new TableHeaderCell();
                        newHeaderCell.Text = "Product Type";
                        newHeaderCell.Width = 200;
                        SearchResults.Rows[0].Cells.Add(newHeaderCell);

                        //create a loop to display each result
                        //creat a counter to keep track of the current row
                        int rowCount = 1;
                        foreach(SP_ProductSearchByTerm result in results)
                        {
                            // create a new row in the results table and set the height
                            newRow = new TableRow();
                            newRow.Height = 50;
                            SearchResults.Rows.Add(newRow);
                            //fill the row with the data from the results object
                            TableCell newCell = new TableCell();
                            newCell.Text = result.Name.ToString();
                            SearchResults.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = result.ProductDescription.ToString();
                            SearchResults.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = result.Price;
                            SearchResults.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = function.GetFullProductTypeText(result.ProductType);
                            SearchResults.Rows[rowCount].Cells.Add(newCell);
                            rowCount++;
                        }
                    }
                    else
                    {
                        //let the user know there are no results
                        ResultsLable.Visible = true;
                        ResultsLable.Text = "<h2> No Results For '" + searchTerm + "' </h2>";
                    }
                }
                catch(ApplicationException)
                {
                        Response.Redirect("Error.aspx?Error=An Error Occurred Getting Search Results From The Server");
                }
            }
        }

        
    }
}