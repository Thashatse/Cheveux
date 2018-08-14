using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;
namespace Cheveux.Cheveux
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


        protected void Page_Load(object sender, EventArgs e)
        {
            loadProductList('X');
        }

        public void loadProductList(char productType)
        {
            try
            {
                //load a list of all products
                products = handler.getAllProductsAndDetails();
                //sort the products by stock count
                products = Tuple.Create(products.Item1.OrderBy(o => o.Name).ToList(),
                    products.Item2.OrderBy(o => o.Name).ToList());
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

                    //create a header row and set cell withs
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Name: ";
                    newHeaderCell.Width = 600;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                    //create a header row and set cell withs
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Product Description: ";
                    newHeaderCell.Width = 300;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Price: ";
                    newHeaderCell.Width = 100;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);

                    //increment rowcounter
                    count++;

                    //display accessories
                    foreach (SP_GetAllAccessories Access in products.Item1)
                    {
                        //if the product maches the selected type
                        //if product matches the tearm
                        if (Access.ProductType[0] == productType || productType == 'X')
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
                                newHeaderCell.Text = function.GetFullProductTypeText('A') + "'s:";
                                tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                                //increment rowcounter
                                count++;
                                //increment accesorie cout
                                accCount++;
                            }

if ((Access.ProductType == "A" || Access.ProductType == "T") &&
                                Access.Active[0] == 'Y')
                            {
                            //diplay the product details
                            //add a new row to the table
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblProductTable.Rows.Add(newRow);

                            //Name
                            TableCell newCell = new TableCell();
                            newCell.Text = "<a class='btn btn-default' href = '../ViewProduct.aspx?ProductID="
                                        + Access.ProductID.ToString().Replace(" ", string.Empty) +
                                        "'>" + Access.Name + "</a>";
                            tblProductTable.Rows[count].Cells.Add(newCell);

                            
                                //stock count
                                newCell = new TableCell();
                                newCell.Text = Access.ProductDescription.ToString();
                                tblProductTable.Rows[count].Cells.Add(newCell);

                                //PRICE
                                newCell = new TableCell();
                                newCell.Text = "R" + string.Format("{0:#.00}", Access.Price);
                                
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
                        if ((treat.ProductType[0] == productType || productType == 'X'))
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
                                newHeaderCell.Text = function.GetFullProductTypeText('T') + "'s:";
                                tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                                //increment rowcounter
                                count++;
                                //increment accesorie cout
                                treatCount++;
                            }
if ((treat.ProductType == "A" || treat.ProductType == "T") &&
                                treat.Active[0] == 'Y')
                            {
                            //diplay the product details
                            //add a new row to the table
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblProductTable.Rows.Add(newRow);

                            //Name
                            TableCell newCell = new TableCell();
                            newCell.Text = "<a class='btn btn-default' href = '../ViewProduct.aspx?ProductID="
                                        + treat.ProductID.ToString().Replace(" ", string.Empty) +
                                        "'>" + treat.Name + "</a>";
                            tblProductTable.Rows[count].Cells.Add(newCell);

                            
                                //stock count
                                newCell = new TableCell();
                                newCell.Text = treat.ProductDescription.ToString();
                                tblProductTable.Rows[count].Cells.Add(newCell);

                                //PRICE
                                newCell = new TableCell();
                                newCell.Text = "R" + string.Format("{0:#.00}", treat.Price);
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
                    productJumbotronLable.Text = count - 1 + " Products";
                }
                else
                {
                    productJumbotronLable.Text = count - 1 + " " + function.GetFullProductTypeText(productType);
                }
                if (count - 1 == 0)
                {
                    productJumbotronLable.ForeColor = System.Drawing.Color.Red;
                }
                else {

                    productJumbotronLable.ForeColor = System.Drawing.Color.Black;
                }
            }

            catch (Exception Err)
            {
                function.logAnError( " An error occurred retrieving list of products external products page. Error: " + Err);
                productJumbotronLable.Font.Size = 22;
                productJumbotronLable.Font.Bold = true;
                productJumbotronLable.Text = "An error occurred retrieving Product details";
            }
        }


    }
}

    