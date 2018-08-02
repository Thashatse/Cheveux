using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;

namespace Cheveux.Manager
{
    public partial class Products : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;
        Tuple<List<SP_GetAllAccessories>, List<SP_GetAllTreatments>> products = null;
        List<SP_GetProductTypes> productTypes = null;
        int treatCount = 0;
        int accCount = 0;

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
                this.MasterPageFile = "~/MasterPages/CheveuxManager.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is loged out
            cookie = Request.Cookies["CheveuxUserID"];

            if (cookie == null)
            {
                //if the user is not loged in as a manager or receptionist do not display Products
            }
            else if (cookie["UT"] != "M" && cookie["UT"] != "R")
            {
                Response.Redirect("../Default.aspx");
            }
            else if (cookie["UT"] == "M" || cookie["UT"] == "R")
            {
                //if the user is loged in as a manager display Products
                LogedIn.Visible = true;
                LogedOut.Visible = false;

                if (!Page.IsPostBack)
                {
                    drpProductType.Items.Add(new ListItem("All", "X"));
                    try
                    {
                        productTypes = handler.getProductTypes();
                        foreach (SP_GetProductTypes productType in productTypes)
                        {
                            if (productType.type != 'S')
                            {
                                drpProductType.Items.Add(new ListItem(
                                    function.GetFullProductTypeText(productType.type.ToString()[0]),
                                    productType.type.ToString()));
                            }
                        }
                    }
                    catch (Exception Err)
                    {
                        function.logAnError(Err.ToString() + "Unable to load drpEmpTyp on emplyee Page");
                    }
                    drpProductType.Items.RemoveAt(0);
                    drpProductType.SelectedIndex = 0;
                }
                //get the selected sort by and display the results
                loadProductList(drpProductType.SelectedValue.ToString()[0]);
            }
        }

        public void loadProductList(char productType)
        {
           try
            {
                //load a list of all products
                products = handler.getAllProductsAndDetails();
                //sort the products by stock count
                products = Tuple.Create(products.Item1.OrderBy(o => o.Qty).ToList(),
                    products.Item2.OrderBy(o => o.Qty).ToList());
                //track row count & number of products cound
                int count = 0;

                if (products.Item1.Count != 0 && products.Item2.Count != 0)
                {
                    //disply the table headers
                    //create a new row in the table and set the height
                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblProductTable.Rows.Add(newRow);
                    //create a header row and set cell withs
                    //Product image row
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 100;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                    //create a header row and set cell withs
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Name: ";
                    newHeaderCell.Width = 700;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                    //create a header row and set cell withs
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Stock Count: ";
                    newHeaderCell.Width = 200;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 125;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);

                    //increment rowcounter
                    count++;
                    
                    //display accessories
                    foreach (SP_GetAllAccessories Access in products.Item1)
                    {
                        //if the product maches the selected type
                        //if product matches the tearm
                        if ((Access.ProductType[0] == productType || productType == 'X') &&
                            (compareToSearchTerm(Access.Name) == true ||
                            compareToSearchTerm(Access.ProductDescription) == true ||
                            compareToSearchTerm(Access.Brandname) == true ||
                            compareToSearchTerm(Access.brandType) == true ||
                            compareToSearchTerm(Access.Colour) == true) &&
                            Access.ProductType[0] != 'S')
                        {
                            //add header only before the first accessorie
                            if (accCount == 0)
                            {
                                //add a new row to the table
                                newRow = new TableRow();
                                newRow.Height = 50;
                                tblProductTable.Rows.Add(newRow);
                                //Product Type
                                newHeaderCell = new TableHeaderCell();
                                tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                                newHeaderCell = new TableHeaderCell();
                                newHeaderCell.Text = function.GetFullProductTypeText('A') + "'s:";
                                tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                                //increment rowcounter
                                count++;
                                //increment accesorie cout
                                accCount++;
                            }

                            //diplay the product details
                            //add a new row to the table
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblProductTable.Rows.Add(newRow);

                            //image
                            TableCell newCell = new TableCell();
                            if (Access.Qty <= 0)
                            {
                                newCell.Text = "&#10071;";
                            }
                            else if (Access.Qty < 10 && Access.Qty > 0)
                            {
                                newCell.Text = "&#9888;";
                            }
                            else
                            {
                                //image display to be added here
                            }
                            tblProductTable.Rows[count].Cells.Add(newCell);

                            //Name
                            //Edit product link to be added by Lachea
                            //view Product link to be added by Lachea
                            newCell = new TableCell();
                            newCell.Text = "<a class='btn btn-default' href = '../ViewProduct.aspx?ProductID="
                                        + Access.ProductID.ToString().Replace(" ", string.Empty) +
                                        "'>"+ Access.Name + "</a>";
                            tblProductTable.Rows[count].Cells.Add(newCell);

                            if ((Access.ProductType == "A" || Access.ProductType == "T") &&
                                Access.Active[0] == 'Y')
                            {
                                //stock count
                                newCell = new TableCell();
                                newCell.Text = Access.Qty.ToString();
                                tblProductTable.Rows[count].Cells.Add(newCell);

                                //Stock
                                newCell = new TableCell();
                                //add stock link to be added by Sivu
                                newCell.Text = "<a class='btn  btn-secondary' href='#?" +
                                            "ProductID=" + Access.ProductID.ToString().Replace(" ", string.Empty) +
                                            "'>Manage Stock</a>";
                                tblProductTable.Rows[count].Cells.Add(newCell);
                            }
                            else
                            {
                                newCell = new TableCell();
                                newCell.Text = "Inactive Product";
                                tblProductTable.Rows[count].Cells.Add(newCell);

                                newCell = new TableCell();
                                tblProductTable.Rows[count].Cells.Add(newCell);
                            }

                            //increment counter
                            accCount++;
                            count++;
                        }
                    }
                    
                    //display treatments
                    foreach (SP_GetAllTreatments treat in products.Item2)
                    {
                        //if the product maches the selected type
                        //if product matches the tearm
                        if ((treat.ProductType[0] == productType || productType == 'X') &&
                            (compareToSearchTerm(treat.Name) == true ||
                            compareToSearchTerm(treat.ProductDescription) == true ||
                            compareToSearchTerm(treat.Brandname) == true ||
                            compareToSearchTerm(treat.TreatmentType) == true ||
                            compareToSearchTerm(treat.brandType) == true) &&
                            treat.ProductType[0] != 'S')
                        {
                            //add header only before the first product
                            if (treatCount == 0)
                            {
                                //add a new row to the table
                                newRow = new TableRow();
                                newRow.Height = 50;
                                tblProductTable.Rows.Add(newRow);
                                //Product Type
                                newHeaderCell = new TableHeaderCell();
                                tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                                newHeaderCell = new TableHeaderCell();
                                newHeaderCell.Text = function.GetFullProductTypeText('T') + "'s:";
                                tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                                //increment rowcounter
                                count++;
                                //increment accesorie cout
                                treatCount++;
                            }

                            //diplay the product details
                            //add a new row to the table
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblProductTable.Rows.Add(newRow);

                            //image
                            TableCell newCell = new TableCell();
                            if (treat.Qty <= 0)
                            {
                                newCell.Text = "&#10071;";
                            }
                            else if (treat.Qty < 10 && treat.Qty > 0)
                            {
                                newCell.Text = "&#9888;";
                            }
                            else
                            {
                                //image display to be added here
                            }
                            tblProductTable.Rows[count].Cells.Add(newCell);

                            //Name
                            newCell = new TableCell();
                            newCell.Text = "<a class='btn btn-default' href = '../ViewProduct.aspx?ProductID="
                                        + treat.ProductID.ToString().Replace(" ", string.Empty) +
                                        "'>" + treat.Name + "</a>";
                            tblProductTable.Rows[count].Cells.Add(newCell);

                            if ((treat.ProductType == "A" || treat.ProductType == "T") && 
                                treat.Active[0] == 'Y')
                            {
                                //stock count
                                newCell = new TableCell();
                                newCell.Text = treat.Qty.ToString();
                                tblProductTable.Rows[count].Cells.Add(newCell);

                                //add stock button
                                newCell = new TableCell();
                            //Edit sok link to be added by Sivu
                            string cellText = "";
                            //add the add stock button only for Treatments and application services
                            //add stock link to be added by Sivu
                                cellText += "<a class='btn  btn-secondary' href='#?" +
                                        "ProductID=" + treat.ProductID.ToString().Replace(" ", string.Empty) +
                                        "'>Manage Stock</a>";
                            
                            newCell.Text = cellText;
                            tblProductTable.Rows[count].Cells.Add(newCell);
                            }else
                            {
                                newCell = new TableCell();
                                newCell.Text = "Inactive Product";
                                tblProductTable.Rows[count].Cells.Add(newCell);

                                newCell = new TableCell();
                                tblProductTable.Rows[count].Cells.Add(newCell);
                            }

                            //increment counter
                            count++;
                            treatCount++;
                        }
                    }
                }

                //result count
                if (productType == 'X')
                {
                    productJumbotronLable.Text = count-1 + " Products";
                }
                else
                {
                    productJumbotronLable.Text = count-1 + " " + function.GetFullProductTypeText(productType);
                }
                if(count-1 == 0)
                {
                    productJumbotronLable.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    productJumbotronLable.ForeColor = System.Drawing.Color.Black;
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving list of products for product type: "
                    + productType + " - " + function.GetFullProductTypeText(productType)
                    +  " with tearm: " + txtProductSearchTerm.Text
                    + " in loadProductList(char productType) method on Manager/Product page");
                productJumbotronLable.Font.Size = 22;
                productJumbotronLable.Font.Bold = true;
                productJumbotronLable.Text = "An error occurred retrieving Product details";
            }
        }

        public bool compareToSearchTerm(string toBeCompared)
        {
            bool result = false;
            if (txtProductSearchTerm.Text != null)
            {
                toBeCompared = toBeCompared.ToLower();
                string searcTearm = txtProductSearchTerm.Text.ToLower();
                if (toBeCompared.Contains(searcTearm))
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }
            return result;
        }
    }
}