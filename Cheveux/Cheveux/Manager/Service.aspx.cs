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
                LogedIn.Visible = false;
                LogedOut.Visible = true;
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

                if (!Page.IsPostBack)
                {
                    //check the action and set the view
                    string action = Request.QueryString["Action"];
                    if (action == "NewType")
                    {
                        hideAll();
                        divNewType.Visible = true;
                    }
                    else if(action == "EditType")
                    {
                        hideAll();
                        loadEdit();
                        divNewType.Visible = true;
                    }
                    else
                    {
                        //check if a view has been requested
                        string view = Request.QueryString["View"];
                        if (view == "Service")
                        {
                            btnViewAllServices_Click(sender, e);
                        }
                        else if (view == "Type")
                        {
                            btnViewServiceTypes_Click(sender, e);
                        }
                        else
                        {
                            btnViewAllServices_Click(sender, e);
                        }
                    }
                }
            }
        }

        #region View
        private void hideAll()
        {
            divAllServices.Visible = false;
            divNewType.Visible = false;
            divServiceTypes.Visible = false;
        }

        protected void btnViewAllServices_Click(object sender, EventArgs e)
        {
            hideAll();

            //get the selected sort by and display the results
            loadProductList();

            divAllServices.Visible = true;
        }

        protected void btnViewServiceTypes_Click(object sender, EventArgs e)
        {
            hideAll();

            loadSerivceTypes();

            divServiceTypes.Visible = true;
        }
        #endregion

        #region services
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
                    newHeaderCell.Width = 300;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                    //create a header row and set cell withs
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Description: ";
                    newHeaderCell.Width = 500;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 100;
                    tblProductTable.Rows[count].Cells.Add(newHeaderCell);

                    //increment rowcounter
                    count++;

                    foreach (PRODUCT prod in products)
                    {
                        //if the product maches the selected type
                        //if product matches the tearm
                        if ((function.compareToSearchTerm(prod.Name, txtProductSearchTerm.Text) == true ||
                            function.compareToSearchTerm(prod.ProductDescription, txtProductSearchTerm.Text) == true) &&
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

                            //Edit service link to be added by Sivu
                            //view service link to be added by Lachea

                            //Name
                            newCell = new TableCell();
                            newCell.Text = "<a class='btn btn-default' href ='../cheveux/services.aspx?ProductID="
                                        + prod.ProductID.ToString().Replace(" ", string.Empty) + "'>" + prod.Name + "</a>";
                            tblProductTable.Rows[count].Cells.Add(newCell);

                            //Description
                            newCell = new TableCell();
                            newCell.Text = "<a class='btn btn-default' href ='../cheveux/services.aspx?ProductID="
                                        + prod.ProductID.ToString().Replace(" ", string.Empty) + "'>" + prod.ProductDescription + "</a> ";
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
                    + " An error occurred retrieving list of Services with tearm " + txtProductSearchTerm.Text
                    + " in loadEmployeeList() method on Manager/Employee page");
                productJumbotronLable.Font.Size = 22;
                productJumbotronLable.Font.Bold = true;
                productJumbotronLable.Text = "An error occurred retrieving employee details";
            }
        }
        #endregion

        #region Types
        public void loadSerivceTypes()
        {
            try
            {
                List<ProductType> types = handler.getProductTypes();
                types = types.OrderBy(o => o.name).ToList();
                //check if there are outstanding orders
                if (types.Count > 0)
                {
                    //if there are bookings desplay them
                    //create a new row in the uppcoming bookings table and set the height
                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblServiceTypes.Rows.Add(newRow);
                    //create a header row and set cell withs
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Name";
                    newHeaderCell.Width = 1000;
                    tblServiceTypes.Rows[0].Cells.Add(newHeaderCell);

                    //create a loop to display each result
                    //creat a counter to keep track of the current row
                    int rowCount = 1;
                    foreach (ProductType type in types)
                    {
                        if(type.ProductOrService == 'S' && type.name.Replace(" ", string.Empty) != "Service")
                        {
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblServiceTypes.Rows.Add(newRow);
                            //fill the row with the data from the results object
                            TableCell newCell = new TableCell();
                            newCell.Text = "<a href='/Manager/Service.aspx?Action=EditType" +
                                            "&typeID=" + type.typeID.Replace(" ", string.Empty) +
                                            "'>" + type.name + "</a>";
                            tblServiceTypes.Rows[rowCount].Cells.Add(newCell);
                            rowCount++;
                        }
                    }

                    if (rowCount == 1)
                    {
                        // if there aren't let the user know
                        lblServicetypes.Text =
                            "<p> No Suppliers </p>";
                        tblServiceTypes.Visible = false;
                    }
                    else
                    {
                        // set the booking copunt
                        lblServicetypes.Text =
                            "<p> " + (rowCount - 1) + " Supliers </p>";
                    }
                }
                else
                {
                    // if there aren't let the user know
                    lblServicetypes.Text =
                        "<p> No Suppliers </p>";
                }
            }
            catch (Exception err)
            {
                function.logAnError("Error loading Service Types on internal service page | Error: " + err.ToString());
                lblServicetypes.Visible = true;
                tblServiceTypes.Visible = false;
                lblServicetypes.Text =
                        "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }
        }
        #endregion

        #region New/Edit Type
        protected void btnAddType_Click(object sender, EventArgs e)
        {

        }

        private void loadEdit()
        {
            string typeID = Request.QueryString["typeID"];
            if (typeID != null && typeID != "")
            {
                List<ProductType> types = handler.getProductTypes();
                foreach (ProductType type in types)
                {
                    if (type.typeID.Replace(" ", string.Empty) == typeID)
                    {
                        txtTypeName.Text = type.name;
                    }
                }
                lblNewTypeHeader.Text = "Edit Type";
            }
        }
        #endregion
    }
}