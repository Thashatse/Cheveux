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
    public partial class Service : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;
        List<PRODUCT> products = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is loged out
            cookie = Request.Cookies["CheveuxUserID"];

            if (cookie == null)
            {
                //if the user is not loged in as a manager do not display Bussines setting
            }
            else if (cookie["UT"] != "M")
            {
                Response.Redirect("../Default.aspx");
            }
            else if (cookie["UT"] == "M")
            {
                //if the user is loged in as a manager display Services
                LogedIn.Visible = true;
                LogedOut.Visible = false;

                //get the selected sort by and display the results
                loadProductList();
            }
        }

        public void loadProductList()
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
                    newHeaderCell.Width = 500;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                    //create a header row and set cell withs
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 100;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 300;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);

                    //increment rowcounter
                    count++;

                    foreach (PRODUCT prod in products)
                    {
                        //if the product maches the selected type
                        //if product matches the tearm
                        if ((compareToSearchTerm(prod.Name) == true ||
                            compareToSearchTerm(prod.ProductDescription) == true) &&
                            prod.ProductType[0] == 'S')
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

                            if (prod.Active[0] == 'Y')
                            {
                                newCell = new TableCell();
                                tblProductTable.Rows[count].Cells.Add(newCell);
                            }
                            else
                            {
                                newCell = new TableCell();
                                newCell.Text = "Inactive Service";
                                tblProductTable.Rows[count].Cells.Add(newCell);
                            }

                            //view & edit
                            newCell = new TableCell();
                            //Edit service link to be added by Sivu
                            //view service link to be added by Lachea
                            newCell.Text =
                                "<button type = 'button' class='btn btn-default'>" +
                                "<a href = '#?" +
                                        "ProductID=" + prod.ProductID.ToString().Replace(" ", string.Empty) +
                                        "&PreviousPage=../Manager/Service.aspx'>Edit  </a></button>          " +

                                        "<button type = 'button' class='btn btn-default'>" +
                                        "<a href = '../'ViewProduct.aspx?ProductID="
                                        + prod.ProductID.ToString().Replace(" ", string.Empty) +
                                        "&PreviousPage=../Manager/Service.aspx'>View   </a></button>";
                            tblProductTable.Rows[count].Cells.Add(newCell);

                            //increment counter
                            count++;
                        }
                    }
                }

                //result count
                    productJumbotronLable.Text = count - 1 + " Services";
                if (count - 1 == 0)
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
                    + " An error occurred retrieving list of Services with tearm "+ txtProductSearchTerm.Text
                    + " in loadEmployeeList() method on Manager/Employee page");
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