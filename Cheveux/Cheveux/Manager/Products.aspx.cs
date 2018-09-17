using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;
using System.Collections;

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

        #region set the master page based on the user type
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
        #endregion

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
                
                if (Page.IsPostBack)
                {
                    tblProductTable.Rows.Clear();
                }

                if (!Page.IsPostBack)
                {
                    //check the action and set the view
                    string action = Request.QueryString["Action"];
                    if (action == "NewOrder")
                    {
                        btnViewNewOrder_Click(sender, e);
                    }
                    else
                    {
                        btnViewAllProducts_Click(sender, e);
                    }

                    drpProductType.Items.Add(new ListItem("All", "X"));
                    ddlOrdersProductType.Items.Add(new ListItem("All", "X"));
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
                                ddlOrdersProductType.Items.Add(new ListItem(
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
                    ddlOrdersProductType.Items.RemoveAt(0);
                    ddlOrdersProductType.SelectedIndex = 0;
                }

                //get the selected sort by and display the results
                loadProductList(drpProductType.SelectedValue.ToString()[0]);
            }
        }

        public bool compareToSearchTerm(string toBeCompared, bool newOrder)
        {
            bool result = false;
            if (txtProductSearchTerm.Text != null && newOrder == false)
            {
                toBeCompared = toBeCompared.ToLower();
                string searcTearm = txtProductSearchTerm.Text.ToLower();
                if (toBeCompared.Contains(searcTearm))
                {
                    result = true;
                }
            }
            else if (txtProductSearch.Text != null && newOrder == true)
            {
                toBeCompared = toBeCompared.ToLower();
                string searcTearm = txtProductSearch.Text.ToLower();
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

        #region View
        private void hideAllView()
        {
            ViewAllProducts.Visible = false;
            NewOrder.Visible = false;
            OutstandingOrders.Visible = false;
            PastOrders.Visible = false;
        }

        protected void btnViewAllProducts_Click(object sender, EventArgs e)
        {
            hideAllView();
            ViewAllProducts.Visible = true;
        }

        protected void btnViewNewOrder_Click(object sender, EventArgs e)
        {
            hideAllView();

            //load new order page
            loadSupplier();
            loadSupplierProducts();

            NewOrder.Visible = true;
        }

        protected void btnViewOutstandingOrders_Click(object sender, EventArgs e)
        {
            hideAllView();

            loadOutOrders();

            OutstandingOrders.Visible = true;
        }

        protected void btnViewPastOrders_Click(object sender, EventArgs e)
        {
            hideAllView();
            PastOrders.Visible = true;
        }
        #endregion

        #region Outstanding Orders
        private void loadOutOrders()
        {
            try
            {
                List<OrderViewModel> outOrders = handler.getOutStandingOrders();
                //check if there are outstanding orders
                if (outOrders.Count > 0)
                {
                    //if there are bookings desplay them
                    //create a new row in the uppcoming bookings table and set the height
                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblOutstandingOrders.Rows.Add(newRow);
                    //create a header row and set cell withs
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Date Orderd";
                    newHeaderCell.Width = 400;
                    tblOutstandingOrders.Rows[0].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Supplier";
                    newHeaderCell.Width = 1000;
                    tblOutstandingOrders.Rows[0].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Items Out Standing";
                    newHeaderCell.Width = 400;
                    tblOutstandingOrders.Rows[0].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 200;
                    tblOutstandingOrders.Rows[0].Cells.Add(newHeaderCell);

                    //create a loop to display each result
                    //creat a counter to keep track of the current row
                    int rowCount = 1;
                    foreach (OrderViewModel outOrder in outOrders)
                    {
                        List<OrderViewModel> outOrderProducts = handler.getProductOrderDL(outOrder.OrderID.ToString());

                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblOutstandingOrders.Rows.Add(newRow);
                        //fill the row with the data from the results object
                        TableCell newCell = new TableCell();
                        newCell.Text = outOrder.orderDate.ToString("dd MMM yyyy");
                        tblOutstandingOrders.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = "<a href='../Profile.aspx?Action=Viewsup" +
                                        "&supID=" + outOrder.supplierID.Replace(" ", string.Empty) +
                                        "'>" + outOrder.supplierName + "</a>";
                        tblOutstandingOrders.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        int outsandingItemCount = 0;
                        foreach (OrderViewModel outOrderDL in outOrderProducts)
                        {
                            outsandingItemCount += outOrderDL.Qty;
                        }
                        newCell.Text = outsandingItemCount.ToString();
                        tblOutstandingOrders.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = "<button type = 'button' class='btn btn-default'>" +
                            "<a href='ViewOrder.aspx?OrderID=" + outOrder.OrderID.ToString() +
                            "'>View</a></button>";
                        tblOutstandingOrders.Rows[rowCount].Cells.Add(newCell);
                        rowCount++;
                    }

                    if (rowCount == 1)
                    {
                        // if there aren't let the user know
                        outstandingOrdersLable.Text =
                            "<p> No Outstanding Orders </p>";
                        tblOutstandingOrders.Visible = false;
                    }
                    else
                    {
                        // set the booking copunt
                        outstandingOrdersLable.Text =
                            "<p> " + (rowCount - 1) + " Outstanding Orders </p>";
                    }
                }
                else
                {
                    // if there aren't let the user know
                    outstandingOrdersLable.Text =
                        "<p> No outstanding Orders </p>";
                }
            }
            catch(Exception err)
            {
                function.logAnError("Error loading outstanding product orders on internal product page | Error: " + err.ToString());
                outstandingOrdersLable.Visible = true;
                tblOutstandingOrders.Visible = false;
                outstandingOrdersLable.Text =
                        "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }
        } 
        #endregion

        #region view all Products
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
                            (compareToSearchTerm(Access.Name, false) == true ||
                            compareToSearchTerm(Access.ProductDescription, false) == true ||
                            compareToSearchTerm(Access.Brandname, false) == true ||
                            compareToSearchTerm(Access.brandType, false) == true ||
                            compareToSearchTerm(Access.Colour, false) == true) &&
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
                            newCell.Text = "<a class='btn btn-default' href = '../cheveux/products.aspx?ProductID="
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

                                #region Stock
                                newCell = new TableCell();
                                //new stock oder
                                newCell.Text = "<a class='btn  btn-secondary' href='?Action=NewOrder&" +
                                            "ProductID=" + Access.ProductID.ToString().Replace(" ", string.Empty) +
                                            "'>Make Order</a>";
                                tblProductTable.Rows[count].Cells.Add(newCell);

                                /*
                                if ()
                                {
                                    newCell = new TableCell();
                                    //recive oder link to be added by Sivu
                                    newCell.Text = "<a class='btn  btn-secondary' href='#?" +
                                                "ProductID=" + Access.ProductID.ToString().Replace(" ", string.Empty) +
                                                "'>Receive Order</a>";
                                    tblProductTable.Rows[count].Cells.Add(newCell);
                                }
                                */
                                #endregion
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
                            (compareToSearchTerm(treat.Name, false) == true ||
                            compareToSearchTerm(treat.ProductDescription, false) == true ||
                            compareToSearchTerm(treat.Brandname, false) == true ||
                            compareToSearchTerm(treat.TreatmentType, false) == true ||
                            compareToSearchTerm(treat.brandType, false) == true) &&
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
                            newCell.Text = "<a class='btn btn-default' href = '../cheveux/products.aspx?ProductID="
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

                                #region orders
                                newCell = new TableCell();
                            //Edit sok link to be added by Sivu
                            string cellText = "";
                            //add the add stock button only for Treatments and application services
                            //add stock link to be added by Sivu
                                cellText += "<a class='btn  btn-secondary' href='Products.aspx?Action=NewOrder" +
                                            "ProductID=" + treat.ProductID.ToString().Replace(" ", string.Empty) +
                                            "'>Make Order</a>";

                                /*
                                if ()
                                {
                                    newCell = new TableCell();
                                    //recive oder link to be added by Sivu
                                    newCell.Text = "<a class='btn  btn-secondary' href='#?" +
                                                "ProductID=" + Access.ProductID.ToString().Replace(" ", string.Empty) +
                                                "'>Receive Order</a>";
                                    tblProductTable.Rows[count].Cells.Add(newCell);
                                }
                                */
                                #endregion

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
        #endregion

        #region New Order
        List<string> productIDs = new List<string>();

        public void loadSupplier()
        {
            try
            {
                List<Supplier> suppliers = handler.getSuppliers();
                foreach (Supplier supplier in suppliers)
                {
                    //Load employee names into dropdownlist
                    ddlSupplier.DataSource = suppliers;
                    //set the coloumn that will be displayed to the user
                    ddlSupplier.DataTextField = "SupplierName";
                    //set the coloumn that will be used for the valuefield
                    ddlSupplier.DataValueField = "SupplierID";
                    //bind the data
                    ddlSupplier.DataBind();
                }
            }
            catch (Exception err)
            {
                function.logAnError("Error Loading Suppliers in new product order | Error: " + err);
                Response.Redirect("http://sict-iis.nmmu.ac.za/beauxdebut/error.aspx?Error=An%20error%20occurred%20loading%20suppliers");
            }
        }

        public void loadSupplierProducts()
        {
            try
            {
                //load a list of all products
                products = handler.getAllProductsAndDetails();

                ArrayList ListBoxArray = new ArrayList();
                char productType = ddlOrdersProductType.SelectedValue.ToString()[0];
                lbProducts.Items.Clear();

                foreach (SP_GetAllAccessories Access in products.Item1)
                {
                    //if the product maches the selected type
                    //if product matches the tearm
                    if ((Access.ProductType[0] == productType || productType == 'X') &&
                        (compareToSearchTerm(Access.Name, true) == true ||
                        compareToSearchTerm(Access.ProductDescription, true) == true ||
                        compareToSearchTerm(Access.Brandname, true) == true ||
                        compareToSearchTerm(Access.brandType, true) == true ||
                        compareToSearchTerm(Access.Colour, true) == true) &&
                        Access.ProductType[0] != 'S')
                    {
                        lbProducts.Items.Add(Access.Name);
                    }
                }

                foreach (SP_GetAllTreatments treat in products.Item2)
                {
                    //if the product maches the selected type
                    //if product matches the tearm
                    if ((treat.ProductType[0] == productType || productType == 'X') &&
                        (compareToSearchTerm(treat.Name, true) == true ||
                        compareToSearchTerm(treat.ProductDescription, true) == true ||
                        compareToSearchTerm(treat.Brandname, true) == true ||
                        compareToSearchTerm(treat.TreatmentType, true) == true ||
                        compareToSearchTerm(treat.brandType, true) == true) &&
                        treat.ProductType[0] != 'S')
                    {
                        lbProducts.Items.Add(treat.Name);
                    }
                }

                for (int i = 0; i < lbProducts.Items.Count; i++)
                {
                    ListBoxArray.Add(lbProducts.Items[i].Value);
                }

                ListBoxArray.Sort();
                lbProducts.Items.Clear();
                foreach(string item in ListBoxArray)
                {
                    lbProducts.Items.Add(item);
                }
            }
            catch (Exception err)
            {
                function.logAnError("Error Loading Suppliers in new product order | Error: " + err);
                Response.Redirect("http://sict-iis.nmmu.ac.za/beauxdebut/error.aspx?Error=An%20error%20occurred%20loading%20products");
            }
        }

        public void loadSupplierProductsID()
        {
            try
            {
                //load a list of all products IDS
                products = handler.getAllProductsAndDetails();

                ArrayList ListBoxArray = new ArrayList();
                char productType = ddlOrdersProductType.SelectedValue.ToString()[0];
                productIDs.Clear();

                foreach (SP_GetAllAccessories Access in products.Item1)
                {
                    //if the product maches the selected type
                    //if product matches the tearm
                    if ((Access.ProductType[0] == productType || productType == 'X') &&
                        (compareToSearchTerm(Access.Name, true) == true ||
                        compareToSearchTerm(Access.ProductDescription, true) == true ||
                        compareToSearchTerm(Access.Brandname, true) == true ||
                        compareToSearchTerm(Access.brandType, true) == true ||
                        compareToSearchTerm(Access.Colour, true) == true) &&
                        Access.ProductType[0] != 'S')
                    {
                        productIDs.Add(Access.ProductID);
                    }
                }

                foreach (SP_GetAllTreatments treat in products.Item2)
                {
                    //if the product maches the selected type
                    //if product matches the tearm
                    if ((treat.ProductType[0] == productType || productType == 'X') &&
                        (compareToSearchTerm(treat.Name, true) == true ||
                        compareToSearchTerm(treat.ProductDescription, true) == true ||
                        compareToSearchTerm(treat.Brandname, true) == true ||
                        compareToSearchTerm(treat.TreatmentType, true) == true ||
                        compareToSearchTerm(treat.brandType, true) == true) &&
                        treat.ProductType[0] != 'S')
                    {
                        productIDs.Add(treat.ProductID);
                    }
                }

                for (int i = 0; i < productIDs.Count; i++)
                {
                    ListBoxArray.Add(productIDs[i]);
                }

                ListBoxArray.Sort();
                productIDs.Clear();
                foreach (string item in ListBoxArray)
                {
                    productIDs.Add(item);
                }
            }
            catch (Exception err)
            {
                function.logAnError("Error Loading Suppliers in new product order | Error: " + err);
                Response.Redirect("http://sict-iis.nmmu.ac.za/beauxdebut/error.aspx?Error=An%20error%20occurred%20loading%20products");
            }
        }

        protected void ddlOrdersProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSupplierProducts();
        }

        protected void btnNewProd_Click(object sender, EventArgs e)
        {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('http://sict-iis.nmmu.ac.za/beauxdebut/Cheveux/Products.aspx?Action=Add','_newtab');", true);
        }
        #endregion
    }
}