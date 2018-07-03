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
        List<PRODUCT> products = null;
        List<SP_GetProductTypes> productTypes = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is loged out
            cookie = Request.Cookies["CheveuxUserID"];

            if (cookie == null)
            {
                //if the user is not loged in as a manager do not display Products
            }
            else if (cookie["UT"] != "M")
            {
                Response.Redirect("../Default.aspx");
            }
            else if (cookie["UT"] == "M")
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
                products = handler.getAllProducts();
                //track row count & number of products cound
                int count = 0;

                if (products.Count > 0)
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
                    newHeaderCell.Width = 300;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                    //create a header row and set cell withs
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Product Type: ";
                    newHeaderCell.Width = 200;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                    //create a header row and set cell withs
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Stock Count: ";
                    newHeaderCell.Width = 100;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 125;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 250;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);

                    //increment rowcounter
                    count++;

                    foreach (PRODUCT prod in products)
                    {
                        //if the product maches the selected type
                        //if product matches the tearm
                        if ((prod.ProductType[0] == productType || productType == 'X') &&
                            (compareToSearchTerm(prod.Name) == true ||
                            compareToSearchTerm(prod.ProductDescription) == true) &&
                            prod.ProductType[0] != 'S')
                        {
                            //diplay the product details
                            //add a new row to the table
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblProductTable.Rows.Add(newRow);

                            //image
                            TableCell newCell = new TableCell();
                            //image display to be added here
                            tblProductTable.Rows[count].Cells.Add(newCell);

                            //Name
                            newCell = new TableCell();
                            newCell.Text = prod.Name;
                            tblProductTable.Rows[count].Cells.Add(newCell);
                            
                            //Product Type
                            newCell = new TableCell();
                            newCell.Text = function.GetFullProductTypeText(prod.ProductType[0]);
                            tblProductTable.Rows[count].Cells.Add(newCell);

if ((prod.ProductType == "A" || prod.ProductType == "T") && prod.Active[0] == 'Y')
                            {
                                //stock count
                                newCell = new TableCell();
                                newCell.Text = "Count";
                                tblProductTable.Rows[count].Cells.Add(newCell);

                                //add stock button
                                newCell = new TableCell();
                            //Edit sok link to be added by Sivu
                            string cellText = "";
                            //add the add stock button only for Treatments and application services
                            //add stock link to be added by Sivu
                                cellText +=
                                "<button type = 'button' class='btn btn-default'>" +
                                "<a href = '#?" +
                                        "ProductID=" + prod.ProductID.ToString().Replace(" ", string.Empty) +
                                        "&PreviousPage=../Manager/Products.aspx'>Manage Stock  </a></button>            ";
                            
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

                            //view & edit
                            newCell = new TableCell();
                            //Edit product link to be added by Lachea
                            //view Product link to be added by Lachea
                            newCell.Text =
                                "<button type = 'button' class='btn btn-default'>" +
                                "<a href = '#?" +
                                        "ProductID=" + prod.ProductID.ToString().Replace(" ", string.Empty) +
                                        "&PreviousPage=../Manager/Products.aspx'>Edit  </a></button>          " +

                                        "<button type = 'button' class='btn btn-default'>" +
                                        "<a href = '../'ViewProduct.aspx?ProductID=" 
                                        + prod.ProductID.ToString().Replace(" ", string.Empty) +
                                        "&PreviousPage=../Manager/Products.aspx'>View   </a></button>";
                            tblProductTable.Rows[count].Cells.Add(newCell);

                            //increment counter
                            count++;
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
                }else
                {
                    productJumbotronLable.ForeColor = System.Drawing.Color.Black;
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving list of emplyees for employee type: "
                    + productType + " - " + function.GetFullEmployeeTypeText(productType)
                    +  " with tearm: " + txtProductSearchTerm.Text
                    + " in loadEmployeeList(char empType) method on Manager/Employee page");
                productJumbotronLable.Font.Size = 22;
                productJumbotronLable.Font.Bold = true;
                productJumbotronLable.Text = "An error occurred retrieving employee details";
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