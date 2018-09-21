﻿using System;
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
                this.MasterPageFile = "~/MasterPages/Cheveux.Master";
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is loged out
            cookie = Request.Cookies["CheveuxUserID"];

            if (cookie == null)
            {
                string action = Request.QueryString["Action"];
                if (action == "ViewOrder")
                {
                    LogedIn.Visible = true;
                    LogedOut.Visible = false;
                    divTabs.Visible = false;
                    hideAllView();
                    divViewOrder.Visible = true;
                    viewOrder(sender, e, true);
                }
                
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
                    //check the action and set the view
                    string action = Request.QueryString["Action"];
                    if (action == "NewOrder")
                    {
                        btnViewNewOrder_Click(sender, e);
                    }
                    else if (action == "ViewOrder")
                    {
                        hideAllView();
                        divViewOrder.Visible = true;
                        viewOrder(sender, e, false);
                    }
                    else if (action == "Viewsup")
                    {
                        hideAllView();
                        divviewSupplier.Visible = true;
                        viewSupplier(sender, e);
                    }
                    else if (action == "AcceptOrder")
                    {
                        hideAllView();
                        divAcceptOrder.Visible = true;
                        acceptOrder(sender, e);
                    }
                    else
                    {
                        //check if a vie has been requested
                        string view = Request.QueryString["View"];
                        if (view == "OutOrders")
                        {
                            btnViewOutstandingOrders_Click(sender, e);
                        }
                        else if (view == "PastOrders")
                        {
                            btnViewPastOrders_Click(sender, e);
                        }
                        else
                        {
                            btnViewAllProducts_Click(sender, e);
                        }
                    }
                }
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
            divAcceptOrder.Visible = false;
            divViewOrder.Visible = false;
            divviewSupplier.Visible = false;
            Suppliers.Visible = false;
        }

        protected void btnViewAllProducts_Click(object sender, EventArgs e)
        {
            hideAllView();

            loadProductTypeDropDowns();
            loadSupplier();

            //get the selected sort by and display the results
            loadProductList(drpProductType.SelectedValue.ToString()[0]);

            ViewAllProducts.Visible = true;
        }

        protected void btnViewNewOrder_Click(object sender, EventArgs e)
        {
            hideAllView();
            
            loadProductTypeDropDowns();
            loadSupplier();

            //load new order page
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

            loadPastOrders();

            PastOrders.Visible = true;
        }

        protected void btnViewSuppliers_Click(object sender, EventArgs e)
        {
            hideAllView();

            loadSuppliers();

            Suppliers.Visible = true;
        }

        protected void btnViewFillterAllProducts_Click(object sender, EventArgs e)
        {
            loadProductList(drpProductType.SelectedValue.ToString()[0]);
            if (btnViewFillterAllProducts.Text == "Fillter")
            {
                btnViewFillterAllProducts.Text = "Hide Fillters";
                divAllProductsFilter.Visible = true;
            }
            else if (btnViewFillterAllProducts.Text == "Hide Fillters")
            {
                btnViewFillterAllProducts.Text = "Fillter";
                divAllProductsFilter.Visible = false;
            }
        }

        List<SP_GetProductTypes> productTypes = null;

        private void loadProductTypeDropDowns()
        {
            if (!Page.IsPostBack)
            {
                drpProductType.Items.Clear();
                ddlOrdersProductType.Items.Clear();

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

                drpProductType.SelectedIndex = 0;
                ddlOrdersProductType.SelectedIndex = 0;
            }
        }

        public void loadSupplier()
        {
            if (!Page.IsPostBack)
            {
                ddlSupplier.Items.Clear();
                ddlAllProdsSuppliers.Items.Clear();
                try
                {
                    List<Supplier> suppliers = handler.getSuppliers();
                    foreach (Supplier supplier in suppliers)
                    {
                        ddlSupplier.DataSource = suppliers;
                        ddlSupplier.DataTextField = "SupplierName";
                        ddlSupplier.DataValueField = "SupplierID";
                        //bind the data
                        ddlSupplier.DataBind();

                        ddlAllProdsSuppliers.DataSource = suppliers;
                        //set the coloumn that will be displayed to the user
                        ddlAllProdsSuppliers.DataTextField = "SupplierName";
                        //set the coloumn that will be used for the valuefield
                        ddlAllProdsSuppliers.DataValueField = "SupplierID";
                        //bind the data
                        ddlAllProdsSuppliers.DataBind();
                    }
                    ddlAllProdsSuppliers.Items.Add(new ListItem("All", "X"));
                    ddlAllProdsSuppliers.SelectedValue = "X";
                }
                catch (Exception err)
                {
                    function.logAnError("Error Loading Suppliers in new product order | Error: " + err);
                    Response.Redirect("http://sict-iis.nmmu.ac.za/beauxdebut/error.aspx?Error=An%20error%20occurred%20loading%20suppliers");
                }
            }
        }
        #endregion

        #region Product Orders
        private void viewOrder(object sender, EventArgs e, bool External)
        {
            //check for the product ID and display the details
            string orderID = Request.QueryString["OrderID"];
            if(orderID != null && orderID != "")
            {
                try
                {
                    //get order
                    OrderViewModel order = handler.getOrder(orderID);
                    //get order detail lines
                    List<OrderViewModel> orderProducts = handler.getProductOrderDL(order.OrderID.ToString());
                    int rowCount = 0;
                    int DLCout = 0;

                    #region Heaing 
                    if (order.Received == true && External == false)
                    {
                        lblOrder.Text = "Product Order from " + order.supplierName;
                    }
                    else if (order.Received == false && External == false)
                    {
                        lblOrder.Text = "Outstanding Product Order from " + order.supplierName;
                    }
                    if (order.Received == true && External == true)
                    {
                        lblOrder.Text = "Fulfilled Product Order Request from Cheveux";
                    }
                    else if (order.Received == false && External == true)
                    {
                        lblOrder.Text = "Product Order Request from Cheveux";
                    }
                    #endregion

                    #region Order Deatails
                    #region order Date
                    //create a new row in the uppcoming bookings table and set the height
                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblViewOrder.Rows.Add(newRow);
                    TableCell newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Width = 150;
                    newCell.Text = "Ordered:";
                    tblViewOrder.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = order.orderDate.ToString("HH:mm dd MMM yyyy");
                    tblViewOrder.Rows[rowCount].Cells.Add(newCell);
                    rowCount++;
                    #endregion

                    //if order has been recevied
                    if (order.Received == true)
                    {
                        #region date Recived
                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblViewOrder.Rows.Add(newRow);
                        newCell = new TableCell();
                        newCell.Font.Bold = true;
                        newCell.Text = "Received:";
                        tblViewOrder.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = order.dateReceived.ToString("dd MMM yyyy");
                        tblViewOrder.Rows[rowCount].Cells.Add(newCell);
                        rowCount++;
                        #endregion
                    }

                    #region Supplier
                    newRow = new TableRow();
                    newRow.Height = 50;
                    tblViewOrder.Rows.Add(newRow);
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Supplier:";
                    tblViewOrder.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    if (External != true)
                    {
                        newCell.Text = "<a href='/Manager/Products.aspx?Action=Viewsup" +
                                        "&supID=" + order.supplierID.Replace(" ", string.Empty) +
                                        "'>" + order.supplierName + "</a>";
                    }
                    else
                    {
                        newCell.Text = order.supplierName;
                    }
                    tblViewOrder.Rows[rowCount].Cells.Add(newCell);
                    rowCount++;

                    //supplier Contact
                    newRow = new TableRow();
                    newRow.Height = 50;
                    tblViewOrder.Rows.Add(newRow);
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Supplier Contact:";
                    tblViewOrder.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = order.contactName;
                    tblViewOrder.Rows[rowCount].Cells.Add(newCell);
                    if(External != true)
                    {
                        newCell = new TableCell();
                    newCell.Text = "<button type = 'button' class='btn btn-default'>" +
                                "<a href = 'tel:" + order.contactNo.ToString() +
                                "'>Phone    </a></button>          " +
                                "<button type = 'button' class='btn btn-default'>" +
                                "<a href = 'mailto:" + order.contactEmail.ToString() +
                                "'>Email    </a></button>";
                    tblViewOrder.Rows[rowCount].Cells.Add(newCell);
                    }
                    rowCount++;
                    #endregion
                    #endregion

                    #region products
                    foreach (OrderViewModel orderDL in orderProducts)
                    {
                        if (DLCout == 0)
                        {
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblViewOrder.Rows.Add(newRow);
                            newCell = new TableCell();
                            newCell.Font.Bold = true;
                            newCell.Text = "Products:";
                            tblViewOrder.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = orderDL.Qty + "x <a href='/cheveux/products.aspx?ProductID=" + orderDL.ProductID.Replace(" ", string.Empty) + "'> " +
                                    orderDL.Name.ToString() + " </a>";
                            tblViewOrder.Rows[rowCount].Cells.Add(newCell);
                            rowCount++;
                            DLCout++;
                        }
                        else
                        {
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblViewOrder.Rows.Add(newRow);
                            //empty cell
                            newCell = new TableCell();
                            tblViewOrder.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = orderDL.Qty + "x <a href='/cheveux/products.aspx?ProductID=" + orderDL.ProductID.Replace(" ", string.Empty) + "'> " +
                                    orderDL.Name.ToString() + " </a>";
                            tblViewOrder.Rows[rowCount].Cells.Add(newCell);
                            rowCount++;
                        }
                    }
                    #endregion

                    #region Navigation
                    if (External != true)
                    {
                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblViewOrder.Rows.Add(newRow);
                        //empty cell
                        newCell = new TableCell();
                        tblViewOrder.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();


                        if (order.Received == false)
                        {
                            newCell.Text = "<br/><br/> " +
                                "<button type = 'button' class='btn btn-default'> <a href='Products.aspx?View=OutOrders'>All Outstanding Orders</a></button>" +
                                "&nbsp; <button type = 'button' class='btn btn-primary'> <a class='btn-primary' href='?Action=AcceptOrder&OrderID=" + order.OrderID.ToString() +
                                "'> Accept Order </a></button>";
                        }
                        else
                        {
                            newCell.Text = "<br/><br/> <button type = 'button' class='btn btn-default'> <a href='Products.aspx?View=PastOrders'>All Past Orders</a></button>";
                        }

                        tblViewOrder.Rows[rowCount].Cells.Add(newCell);
                        rowCount++;
                    }
                    #endregion
                }
                catch (Exception err)
                {
                    function.logAnError("Error loading order details on internal product page orderID = "+ orderID +" | Error: " + err.ToString());
                    tblViewOrder.Visible = false;
                    lblOrder.Text =
                            "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
                }
            }
            else if(External != true)
            {
                //if there is no order ID
                btnViewOutstandingOrders_Click(sender, e);
            }
            else
            {
                Response.Redirect("http://sict-iis.nmmu.ac.za/beauxdebut/error.aspx?Error=An%20error%20occurred%20loading%20product%20order");
            }
        }
        
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
                    newHeaderCell.Width = 800;
                    tblOutstandingOrders.Rows[0].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Items Out Standing";
                    newHeaderCell.Width = 400;
                    tblOutstandingOrders.Rows[0].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 400;
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
                        newCell.Text = "<a href='/Manager/Products.aspx?Action=Viewsup" +
                                        "&supID=" + outOrder.supplierID.Replace(" ", string.Empty) +
                                        "'>" + outOrder.supplierName + "</a>";
                        tblOutstandingOrders.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        int outsandingItemCount = 0;
                        foreach (OrderViewModel outOrderDL in outOrderProducts)
                        {
                            outsandingItemCount += outOrderDL.Qty;
                        }
                        string dropDown = "<li style='list-style: none;' class='dropdown'>" +
                                "<a class='dropdown-toggle' data-toggle='dropdown' href='#'>";
                        dropDown += outsandingItemCount;
                        dropDown += "<span class='caret'></span></a>" +
                                            "<ul class='dropdown-menu bg-dark text-white'>";
                        foreach (OrderViewModel outOrderDL in outOrderProducts)
                        {
                            dropDown += "<li>&nbsp;<a href='/cheveux/products.aspx?ProductID=" + outOrderDL.ProductID.Replace(" ", string.Empty) + "'>" +
                                    " " + outOrderDL.Name.ToString() + " </a>&nbsp;</li>";
                        }
                        dropDown += "</ul></li>";
                        newCell.Text = dropDown;
                        tblOutstandingOrders.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = "<a class='btn btn-default' href='?Action=ViewOrder&OrderID=" + outOrder.OrderID.ToString() +
                            "'> View </a> &nbsp;&nbsp; " +
                            "<a class='btn btn-primary' href='?Action=AcceptOrder&OrderID=" + outOrder.OrderID.ToString() +
                            "'> Accept Order </a>";
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
        
        private void loadPastOrders()
        {
            try
            {
                List<OrderViewModel> pastOrders = handler.getPastOrders();
                //check if there are outstanding orders
                if (pastOrders.Count > 0)
                {
                    //if there are bookings desplay them
                    //create a new row in the uppcoming bookings table and set the height
                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblPastOrders.Rows.Add(newRow);
                    //create a header row and set cell withs
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Date Received";
                    newHeaderCell.Width = 400;
                    tblPastOrders.Rows[0].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Supplier";
                    newHeaderCell.Width = 800;
                    tblPastOrders.Rows[0].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Items Received";
                    newHeaderCell.Width = 400;
                    tblPastOrders.Rows[0].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 400;
                    tblPastOrders.Rows[0].Cells.Add(newHeaderCell);

                    //create a loop to display each result
                    //creat a counter to keep track of the current row
                    int rowCount = 1;
                    foreach (OrderViewModel pastOrder in pastOrders)
                    {
                        List<OrderViewModel> pastOrderProducts = handler.getProductOrderDL(pastOrder.OrderID.ToString());

                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblPastOrders.Rows.Add(newRow);
                        //fill the row with the data from the results object
                        TableCell newCell = new TableCell();
                        newCell.Text = pastOrder.dateReceived.ToString("dd MMM yyyy");
                        tblPastOrders.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = "<a href='/Manager/Products.aspx?Action=Viewsup" +
                                        "&supID=" + pastOrder.supplierID.Replace(" ", string.Empty) +
                                        "'>" + pastOrder.supplierName + "</a>";
                        tblPastOrders.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        int receivedItemCount = 0;
                        foreach (OrderViewModel outOrderDL in pastOrderProducts)
                        {
                            receivedItemCount += outOrderDL.Qty;
                        }
                        string dropDown = "<li style='list-style: none;' class='dropdown'>" +
                                "<a class='dropdown-toggle' data-toggle='dropdown' href='#'>";
                        dropDown += receivedItemCount;
                        dropDown += "<span class='caret'></span></a>" +
                                            "<ul class='dropdown-menu bg-dark text-white'>";
                        foreach (OrderViewModel outOrderDL in pastOrderProducts)
                        {
                            dropDown += "<li>&nbsp;<a href='/cheveux/products.aspx?ProductID=" + outOrderDL.ProductID.Replace(" ", string.Empty) + "'>" +
                                    " " + outOrderDL.Name.ToString() + " </a>&nbsp;</li>";
                        }
                        dropDown += "</ul></li>";
                        newCell.Text = dropDown;
                        tblPastOrders.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = "<a class='btn btn-default' href='?Action=ViewOrder&OrderID=" + pastOrder.OrderID.ToString() +
                            "'> View </a>";
                        tblPastOrders.Rows[rowCount].Cells.Add(newCell);
                        rowCount++;
                    }

                    if (rowCount == 1)
                    {
                        // if there aren't let the user know
                        lblPastOrder.Text =
                            "<p> No past Orders </p>";
                        tblPastOrders.Visible = false;
                    }
                    else
                    {
                        // set the booking copunt
                        lblPastOrder.Text =
                            "<p> " + (rowCount - 1) + " Past Orders </p>";
                    }
                }
                else
                {
                    // if there aren't let the user know
                    lblPastOrder.Text =
                        "<p> No past orders </p>";
                }
            }
            catch (Exception err)
            {
                function.logAnError("Error loading outstanding product orders on internal product page | Error: " + err.ToString());
                lblPastOrder.Visible = true;
                tblOutstandingOrders.Visible = false;
                lblPastOrder.Text =
                        "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }
        }
        
        #region New Order
        List<string> productIDs = new List<string>();
        
        public void loadSupplierProducts() /** For Lachea **/
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
                        (Access.supplierID == ddlSupplier.SelectedValue.ToString() || ddlSupplier.SelectedValue.ToString()[0] == 'X') &&/** For Lachea **/
                        (compareToSearchTerm(Access.Name, true) == true ||
                        compareToSearchTerm(Access.ProductDescription, true) == true ||
                        compareToSearchTerm(Access.Brandname, true) == true ||
                        compareToSearchTerm(Access.brandType, true) == true ||
                        compareToSearchTerm(Access.Colour, true) == true) &&
                        Access.ProductType[0] != 'S'/** For Lachea **/)
                    {
                        lbProducts.Items.Add(Access.Name);
                    }
                }

                foreach (SP_GetAllTreatments treat in products.Item2)
                {
                    //if the product maches the selected type
                    //if product matches the tearm
                    if ((treat.ProductType[0] == productType || productType == 'X') &&
                        (treat.supplierID == ddlSupplier.SelectedValue.ToString() || ddlSupplier.SelectedValue.ToString()[0] == 'X') &&/** For Lachea **/
                        (compareToSearchTerm(treat.Name, true) == true ||
                        compareToSearchTerm(treat.ProductDescription, true) == true ||
                        compareToSearchTerm(treat.Brandname, true) == true ||
                        compareToSearchTerm(treat.TreatmentType, true) == true ||
                        compareToSearchTerm(treat.brandType, true) == true) &&
                        treat.ProductType[0] != 'S'/** For Lachea **/)
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
        
        protected void btnAddProductToOrder_Click(object sender, EventArgs e)
        {
            if(lbProducts.SelectedItem != null)
            {
                lProductsOnOrder.Items.Add(Qty.SelectedItem.Text +" - "+ lbProducts.SelectedItem.Text);
                NoProductSelectedOnOrder.Visible = false;
            }
        }
        
        protected void btnRemoveProductFromOrder_Click(object sender, EventArgs e)
        {
            if (lProductsOnOrder.SelectedItem != null)
            {
                lProductsOnOrder.Items.Remove(lProductsOnOrder.SelectedItem);
            }
        }

        protected void btnSaveOrder_Click(object sender, EventArgs e)
        {
            if (lProductsOnOrder.Items.Count > 0)
            {
                bool success = false;
                string orderID = "";

                try
                {
                    Order newOrder = new Order();
                    newOrder.OrderID = function.GenerateRandomOrderID();
                    newOrder.supplierID = ddlSupplier.SelectedValue;
                    success = handler.newProductOrder(newOrder);

                    if (success != false)
                    {
                        for (int i = 0; i < lProductsOnOrder.Items.Count; i++)
                        {
                            Order_DTL newOrderDL = new Order_DTL();
                            newOrderDL.OrderID = newOrder.OrderID;

                            string[] array = lProductsOnOrder.Items[i].Text.Split('-');
                            array[1] = array[1].Substring(1);
                            string prodID = "";
                            txtProductSearch.Text = "";
                            ddlOrdersProductType.SelectedIndex = 0;
                            loadSupplierProducts();
                            loadSupplierProductsID();

                            for (int index = 0; i < lbProducts.Items.Count; i++)
                            {
                                if (array[1] == lbProducts.Items[index].Text)
                                {
                                    prodID = productIDs[index];
                                }
                                index++;
                            }

                            newOrderDL.ProductID = prodID;
                            newOrderDL.Qty = Convert.ToInt32(array[0]);
                            success = handler.newProductOrderDL(newOrderDL);
                        }
                    }

                    orderID = newOrder.OrderID;
                }
                catch (Exception err)
                {
                    function.logAnError("Error making new product order | Error: " + err);
                    Response.Redirect("http://sict-iis.nmmu.ac.za/beauxdebut/error.aspx?Error=An%20error%20occurred%20creating%20product%20order");
                }

                if (success == true)
                {
                    //email to supplier
                    Supplier supp = handler.getSupplier(ddlSupplier.SelectedValue); 
                    //send an email notification
                    var body = new System.Text.StringBuilder();
                    body.AppendFormat("Hello " + supp.contactName.ToString() + ",");
                    body.AppendLine(@"");
                    body.AppendLine(@"");
                    body.AppendLine(@"Please review the stock oder request at the link below");
                    body.AppendLine(@"");
                    body.AppendLine(@"http://sict-iis.nmmu.ac.za/beauxdebut/Manager/Products.aspx?Action=ViewOrder&OrderID=" + orderID);
                    body.AppendLine(@"");
                    body.AppendLine(@"Regards,");
                    body.AppendLine(@"");
                    body.AppendLine(@"The Cheveux Team");
                    function.sendEmailAlert(supp.contactEmail, supp.contactName,
                        "Product Stock Order Request",
                        body.ToString(),
                        "Cheveux");

                    //show order details to user
                    Response.Redirect("Products.aspx?Action=ViewOrder&OrderID=" + orderID);
                }
                else if (success == false)
                {
                    function.logAnError("Error making new product order");
                    Response.Redirect("http://sict-iis.nmmu.ac.za/beauxdebut/error.aspx?Error=An%20error%20occurred%20creating%20product%20order");
                }
            }
            else
            {
                NoProductSelectedOnOrder.Visible = true;
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

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProductSearch.Text = "";
            ddlOrdersProductType.SelectedIndex = 'X';
            lProductsOnOrder.Items.Clear();
            loadSupplierProducts();
            loadSupplierProductsID();
        }
        #endregion

        #region Accept Order
        //Sivu's Code Here

        private void acceptOrder(object sender, EventArgs e)
        {
            //check for the product ID and display the accept form
            string orderID = Request.QueryString["OrderID"];
            if (orderID != null && orderID != "")
            {
                //Sivu's Code Here
            }
            else
            {
                //if there is no order ID redirect to outstanding orders
                btnViewOutstandingOrders_Click(sender, e);
            }
        }
        #endregion
        #endregion

        #region Products
        int treatCount = 0;
        int accCount = 0;
        
        public void loadProductList(char productType)
        {
           try
            {
                #region set the supplierID
                if (!Page.IsPostBack)
                {
                    string view = Request.QueryString["View"];
                    if (view == "ViewProds")
                    {
                        string suppID = Request.QueryString["SuppID"];
                        if (suppID != "" || suppID != null)
                        {
                            ddlAllProdsSuppliers.SelectedValue = suppID;
                            lblViewAllProductsHeading.Text = "<h1>"+ ddlAllProdsSuppliers.SelectedItem.Text + " Products</h1>";
                        }
                    }
                }
                #endregion
                tblProductTable.Rows.Clear();
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
                            (Access.supplierID == ddlAllProdsSuppliers.SelectedValue.ToString() || ddlAllProdsSuppliers.SelectedValue.ToString()[0] == 'X') &&
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
                            (treat.supplierID == ddlAllProdsSuppliers.SelectedValue.ToString() || ddlAllProdsSuppliers.SelectedValue.ToString()[0] == 'X') &&
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
        
        protected void ddlAllProdsSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadProductList(drpProductType.SelectedValue.ToString()[0]);
            if (ddlAllProdsSuppliers.SelectedValue[0] != 'X')
            {
                lblViewAllProductsHeading.Text = "<h1>" + ddlAllProdsSuppliers.SelectedItem.Text + " Products</h1>";
            }
            else
            {
                lblViewAllProductsHeading.Text = "<h1>Manage Products</h1>";
            }     
        }
        #endregion

        #region Supplier
        private void loadSuppliers()
        {
            try
            {
                List<Supplier> suppliers = handler.getSuppliers();
                //check if there are outstanding orders
                if (suppliers.Count > 0)
                {
                    //if there are bookings desplay them
                    //create a new row in the uppcoming bookings table and set the height
                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblSuppliers.Rows.Add(newRow);
                    //create a header row and set cell withs
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Name";
                    newHeaderCell.Width = 800;
                    tblSuppliers.Rows[0].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Contact";
                    newHeaderCell.Width = 800;
                    tblSuppliers.Rows[0].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 400;
                    tblSuppliers.Rows[0].Cells.Add(newHeaderCell);

                    //create a loop to display each result
                    //creat a counter to keep track of the current row
                    int rowCount = 1;
                    foreach (Supplier supplier in suppliers)
                    {
                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblSuppliers.Rows.Add(newRow);
                        //fill the row with the data from the results object
                        TableCell newCell = new TableCell();
                        newCell.Text = "<a href='/Manager/Products.aspx?Action=Viewsup" +
                                        "&supID=" + supplier.supplierID.Replace(" ", string.Empty) +
                                        "'>" + supplier.supplierName + "</a>";
                        tblSuppliers.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = supplier.contactName;
                        tblSuppliers.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = "<button type = 'button' class='btn btn-default'>" +
                                "<a href = 'tel:" + supplier.contactNo.ToString() +
                                "'>Phone    </a></button>          " +
                                "<button type = 'button' class='btn btn-default'>" +
                                "<a href = 'mailto:" + supplier.contactEmail.ToString() +
                                "'>Email    </a></button>";
                        tblSuppliers.Rows[rowCount].Cells.Add(newCell);
                        rowCount++;
                    }

                    if (rowCount == 1)
                    {
                        // if there aren't let the user know
                        lblSuppliers.Text =
                            "<p> No Suppliers </p>";
                        tblSuppliers.Visible = false;
                    }
                    else
                    {
                        // set the booking copunt
                        lblSuppliers.Text =
                            "<p> " + (rowCount - 1) + " Supliers </p>";
                    }
                }
                else
                {
                    // if there aren't let the user know
                    lblSuppliers.Text =
                        "<p> No Suppliers </p>";
                }
            }
            catch (Exception err)
            {
                function.logAnError("Error loading Suppliers on internal product page | Error: " + err.ToString());
                lblSuppliers.Visible = true;
                tblOutstandingOrders.Visible = false;
                lblSuppliers.Text =
                        "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }
        }

        private void viewSupplier(object sender, EventArgs e)
        {
            //check for the product ID and display the details
            string suppID = Request.QueryString["supID"];
            if (suppID != null && suppID != "")
            {
                try
                {
                    //get Supplier Deatils
                    Supplier supp = handler.getSupplier(suppID);
                    int rowCount = 0;

                    #region Heaing 
                    lblSupplier.Text = supp.supplierName;
                    #endregion

                    #region Supplier Deatails
                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblSupplier.Rows.Add(newRow);
                    TableCell newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Contact Name:";
                    newCell.Width = 150;
                    tblSupplier.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Width = 300;
                    newCell.Text = supp.contactName;
                    tblSupplier.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "<button type = 'button' class='btn btn-default'>" +
                                "<a href = 'tel:" + supp.contactNo.ToString() +
                                "'>Phone    </a></button>          " +
                                "<button type = 'button' class='btn btn-default'>" +
                                "<a href = 'mailto:" + supp.contactEmail.ToString() +
                                "'>Email    </a></button>";
                    tblSupplier.Rows[rowCount].Cells.Add(newCell);
                    rowCount++;

                    newRow = new TableRow();
                    newRow.Height = 50;
                    tblSupplier.Rows.Add(newRow);
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Address:";
                    tblSupplier.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = supp.AddressLine1;
                    tblSupplier.Rows[rowCount].Cells.Add(newCell);
                    rowCount++;

                    if (supp.AddressLine2 != null && supp.AddressLine2 != "")
                    {
                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblSupplier.Rows.Add(newRow);
                        newCell = new TableCell();
                        tblSupplier.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = supp.AddressLine2;
                        tblSupplier.Rows[rowCount].Cells.Add(newCell);
                        rowCount++;
                    }

                    if (supp.Suburb != null && supp.Suburb != "")
                    {
                        newRow = new TableRow();
                        newRow.Height = 50;
                        tblSupplier.Rows.Add(newRow);
                        newCell = new TableCell();
                        tblSupplier.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = supp.Suburb;
                        tblSupplier.Rows[rowCount].Cells.Add(newCell);
                        rowCount++;
                    }

                    newRow = new TableRow();
                    newRow.Height = 50; 
                    tblSupplier.Rows.Add(newRow);
                    newCell = new TableCell();
                    tblSupplier.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = supp.City;
                    tblSupplier.Rows[rowCount].Cells.Add(newCell);
                    rowCount++;
                    #endregion

                    #region Navigation
                    newRow = new TableRow();
                    newRow.Height = 50;
                    tblSupplier.Rows.Add(newRow);
                    newCell = new TableCell();
                    tblSupplier.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "<button type = 'button' class='btn btn-default'>" + 
                        "<a href='?SuppID=" + supp.supplierID + "&View=ViewProds" +
                            "'> View Supplier Products </a></button>";
                    tblSupplier.Rows[rowCount].Cells.Add(newCell);
                    rowCount++;
                    #endregion
                }
                catch (Exception err)
                {
                    function.logAnError("Error loading Supplier details on internal product page suppID = " + suppID + " | Error: " + err.ToString());
                    tblSupplier.Visible = false;
                    lblSupplier.Text =
                            "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
                }
            }
            else
            {
                //if there is no order ID
                btnViewOutstandingOrders_Click(sender, e);
            }
        }
        #endregion
    }
}