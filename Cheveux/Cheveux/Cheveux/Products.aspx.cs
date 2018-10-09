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
        string productID;
        int accCount = 0;
        
        //Creating the variable//
        SP_GetAllAccessories Accessory = null;
        SP_GetAllTreatments Treatment = null;
        List<SP_GetBrandsForProductType> brandList = null;

        #region Master Page
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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            //check query string
            productID = Request.QueryString["ProductID"];
            string action = Request.QueryString["Action"];

            //access Contorl
            cookie = Request.Cookies["CheveuxUserID"];

            if (cookie != null) {
                if (action == null && productID == null && (cookie["UT"] == "M" || cookie["UT"] == "R"))
                {
                    Response.Redirect("../Manager/Products.aspx");
                }
        }

            //load all products
            if (action == null)
            {
                if (productID == null)
                {
                    phProducts.Visible = true;
                    addandedit.Visible = false;
                    phSpecProduct.Visible = false;
                    lblHeadera.Text= "Products";

                    loadProductList('X');

                }
                else if (productID != null) //load specific product
                {
                    lblHeadera.Text = "Product"; //later make this display the specific products name or image
                    phProducts.Visible = false;
                    addandedit.Visible = false;
                    phSpecProduct.Visible = true;
                    DisplayProduct.Visible = true;
                    LoadProduct(productID);
                }
            }
            else if (cookie != null)
            {
                if (action == "Add" && (cookie["UT"] == "M" || cookie["UT"] == "R"))
                {
                    phProducts.Visible = false;
                    addandedit.Visible = true;
                    phSpecProduct.Visible = false;

                    if (!IsPostBack)
                    {
                        productTypes = handler.getProductTypes();
                        foreach (SP_GetProductTypes pType in productTypes)
                        {
                            if (pType.type != 'S')
                            {
                                drpProductType.Items.Add(new ListItem(
                                function.GetFullProductTypeText(pType.type.ToString()[0]),
                                pType.type.ToString()));
                            }
                        }
                        brandList = handler.getBrandsForProductType(drpProductType.SelectedItem.Text.ToCharArray()[0]);
                        foreach (SP_GetBrandsForProductType brand in brandList)
                        {
                            drpBrandList.DataSource = brandList;
                            drpBrandList.DataTextField = "Name";
                            drpBrandList.DataValueField = "BrandID";
                            drpBrandList.DataBind();
                        }
                        loadSupplier();

                    }
                }
                else if (action == "EditProd" && (cookie["UT"] == "M" || cookie["UT"] == "R"))
                {
                    phProducts.Visible = false;
                    addandedit.Visible = true;
                    phSpecProduct.Visible = false;

                    if (!IsPostBack)
                    {
                        productTypes = handler.getProductTypes();
                        foreach (SP_GetProductTypes pType in productTypes)
                        {
                            if (pType.type != 'S')
                            {
                                drpProductType.Items.Add(new ListItem(
                                function.GetFullProductTypeText(pType.type.ToString()[0]),
                                pType.type.ToString()));
                            }
                        }
                        brandList = handler.getBrandsForProductType(drpProductType.SelectedItem.Text.ToCharArray()[0]);
                        foreach (SP_GetBrandsForProductType brand in brandList)
                        {
                            drpBrandList.DataSource = brandList;
                            drpBrandList.DataTextField = "Name";
                            drpBrandList.DataValueField = "BrandID";
                            drpBrandList.DataBind();
                        }
                        loadSupplier();
                    lblHeadera.Text = "Edit Product";
                    btnAddProduct.Text = "Save";
                    Accessory = handler.selectAccessory(productID);
                    Treatment = handler.selectTreatment(productID);

                    //track row count & number of products count
                    if (Accessory != null)
                    {
                        txtName.Text = Accessory.Name.ToString();
                        txtProductDescription.Text = Accessory.ProductDescription.ToString();
                        txtPrice.Text= Accessory.Price.ToString();
                        drpProductType.SelectedIndex = 0;
                        drpProductType_Change(sender, e);
                        drpBrandList.SelectedValue = Accessory.BrandID;
                        drpListSupplier.SelectedValue= Accessory.supplierID;
                        txtcolour.Text = Accessory.Colour.ToString();
                        }
                    //display accessories
                    else if (Treatment != null)
                    {
                        txtName.Text = Treatment.Name.ToString();
                        txtProductDescription.Text = Treatment.ProductDescription.ToString();
                        txtPrice.Text = Treatment.Price.ToString();
                        drpProductType.SelectedIndex = 1;
                        drpProductType_Change(sender, e);
                        drpBrandList.SelectedValue = Treatment.BrandID;
                        drpListSupplier.SelectedValue = Treatment.supplierID;
                        txtcolour.Text = Treatment.TreatmentType;
                        }

                    }
                }
                    }
                }
        
        public void loadSupplier()
        {
            if (!Page.IsPostBack)
            {
                drpListSupplier.Items.Clear();
                try
                {
                    List<Supplier> suppliers = handler.getSuppliers();
                    foreach (Supplier supplier in suppliers)
                    {

                        drpListSupplier.DataSource = suppliers;
                        //set the coloumn that will be displayed to the user
                        drpListSupplier.DataTextField = "SupplierName";
                        //set the coloumn that will be used for the valuefield
                       drpListSupplier.DataValueField = "SupplierID";
                        //bind the data
                        drpListSupplier.DataBind();
                    }
                }
                catch (Exception err)
                {
                    function.logAnError("Error Loading Suppliers in new product order | Error: " + err);
                    Response.Redirect("http://sict-iis.nmmu.ac.za/beauxdebut/error.aspx?Error=An%20error%20occurred%20loading%20suppliers");
                }
            }
        }
        
        protected void drpProductType_Change(object sender, EventArgs e)
        {
            try
            {
                brandList = handler.getBrandsForProductType(drpProductType.SelectedItem.Text.ToCharArray()[0]);
            }
            catch(Exception err)
            {
                drpBrandList.Text = "-------";
                function.logAnError("Error getting product type and brand [drpProductType_change]"+err.ToString());
            }
            foreach (SP_GetBrandsForProductType brand in brandList)
            {
                drpBrandList.DataSource = brandList;
                drpBrandList.DataTextField = "Name";
                drpBrandList.DataValueField = "BrandID";
                drpBrandList.DataBind();
            }

            if (drpProductType.SelectedItem.Text == "Application Service")
            {
                productLabel.Text = "Colour";        
            }
            else if (drpProductType.SelectedItem.Text == "Treatment")
            {
                productLabel.Text = "Treatment Type";
            }



        }

        public void loadProductList(char productType)
        {





            /*add an edit button for the products
             * set button action to edit
             * 
             */ 


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
                    newHeaderCell = new TableHeaderCell();
                 



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
                                newCell.Text = "<a class='btn btn-default' href = '../Cheveux/Products.aspx?ProductID="
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
                                newCell.Text = "<a class='btn btn-default' href = '../Cheveux/Products.aspx?ProductID="
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
                function.logAnError(" An error occurred retrieving list of products external products page. Error: " + Err);
                productJumbotronLable.Font.Size = 22;
                productJumbotronLable.Font.Bold = true;
                productJumbotronLable.Text = "An error occurred retrieving Product details";
            }
        }

        public void LoadProduct(string productID)
        {
            /*Lachea To-do:
             * 
             * - Make sure the user can see the image of the product as well as the product details.
             * - Make it so that instead of the jumbotron displaying a heading it should display the product 
             *   instead. (lblHeader.Visible=false; and then add the productImage in that area)
             */

            tblProducts.Visible = true;

            int count = 0;
            //displaythe table headers
            //create a new row in the table and set the height
            TableRow newRow = new TableRow();
            newRow.Height = 50;
            tblProducts.Rows.Add(newRow);
            //create a header row and set cell widths

            //create a header row and set cell widths
            TableHeaderCell newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Name: ";
            newHeaderCell.Width = 300;
            tblProducts.Rows[count].Cells.Add(newHeaderCell);
            count++;

            newRow = new TableRow();
            tblProducts.Rows.Add(newRow);
            //create a header row and set cell widths
            newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Product Description: ";
            newHeaderCell.Width = 300;
            tblProducts.Rows[count].Cells.Add(newHeaderCell);

            count++;

            newRow = new TableRow();
            tblProducts.Rows.Add(newRow);
            newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Price: ";
            newHeaderCell.Width = 100;
            tblProducts.Rows[count].Cells.Add(newHeaderCell);

            count++;

            if (cookie != null)
            {
                if (cookie["UT"] == "M" || cookie["UT"] == "R")
                {
                    newRow = new TableRow();
                    tblProducts.Rows.Add(newRow);
                    //create a header row and set cell widths
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "QTY on Hand: ";
                    newHeaderCell.Width = 300;
                    tblProducts.Rows[count].Cells.Add(newHeaderCell);
                    count++;

                    newRow = new TableRow();
                    tblProducts.Rows.Add(newRow);
                    //create a header row and set cell widths
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Brand: ";
                    newHeaderCell.Width = 300;
                    tblProducts.Rows[count].Cells.Add(newHeaderCell);

                    count++;

                    newRow = new TableRow();
                    tblProducts.Rows.Add(newRow);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Supplier: ";
                    newHeaderCell.Width = 100;
                    tblProducts.Rows[count].Cells.Add(newHeaderCell);

                    count++;

                    //edit BTN Row
                    newRow = new TableRow();
                    tblProducts.Rows.Add(newRow);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 100;
                    tblProducts.Rows[count].Cells.Add(newHeaderCell);

                    count++;




                }
            }
            //Display specific product
            try
            {
                Accessory = handler.selectAccessory(productID);
                Treatment = handler.selectTreatment(productID);

                //track row count & number of products count
                if (Accessory != null)
                {
                    count = 0;
                    TableCell cell = new TableCell();
                    cell.Text = Accessory.Name.ToString();
                    tblProducts.Rows[
            count].Cells.Add(cell);

                    count++; 

                    cell = new TableCell();
                    cell.Text = Accessory.ProductDescription.ToString();
                    tblProducts.Rows[
            count].Cells.Add(cell);

                    count++; 

                    cell = new TableCell();
                    cell.Text = "R" + string.Format("{0:#.00}", Accessory.Price);
                    tblProducts.Rows[
            count].Cells.Add(cell);
                    count++;

                    if (cookie != null)
                    {
                        if (cookie["UT"] == "M" || cookie["UT"] == "R")
                        {
                            cell = new TableCell();
                            cell.Text = Accessory.Qty.ToString();
                            tblProducts.Rows[
                    count].Cells.Add(cell);
                            count++;

                            cell = new TableCell();
                            cell.Text = Accessory.Brandname.ToString();
                            tblProducts.Rows[
                    count].Cells.Add(cell);
                            count++;

                            cell = new TableCell();
                            cell.Text = Accessory.supplierName.ToString();
                            tblProducts.Rows[count].Cells.Add(cell);
                            count++;

                            cell = new TableCell();
                            cell.Text = "<a class= 'btn btn-primary' href = '?Action=EditProd&" +
                                        "ProductID=" + productID.ToString().Replace(" ", string.Empty) +
                                        "' >Edit Product</a>";
                            tblProducts.Rows[count].Cells.Add(cell);
                            count++;
                        }
                    }
                }
                //display accessories
                else if (Treatment != null)
                {
                    count = 0;
                    TableCell cell = new TableCell();
                    cell.Text = Treatment.Name.ToString();
                    tblProducts.Rows[
            count].Cells.Add(cell);
                    count++;

                    cell = new TableCell();
                    cell.Text = Treatment.ProductDescription.ToString();
                    tblProducts.Rows[count].Cells.Add(cell);
                    count++;

                    cell = new TableCell();
                    cell.Text = "R" + string.Format("{0:#.00}", Treatment.Price);
                    tblProducts.Rows[count].Cells.Add(cell);
                    count++;

                    if (cookie != null)
                    {
                        if (cookie["UT"] == "M" || cookie["UT"] == "R")
                        {
                            cell = new TableCell();
                            cell.Text = Treatment.Qty.ToString();
                            tblProducts.Rows[
                    count].Cells.Add(cell);
                            count++;

                            cell = new TableCell();
                            cell.Text = Treatment.Brandname.ToString();
                            tblProducts.Rows[
                    count].Cells.Add(cell);
                            count++;

                            cell = new TableCell();
                            cell.Text = Treatment.supplierName.ToString();
                            tblProducts.Rows[count].Cells.Add(cell);
                            count++;

                            cell = new TableCell();
                            cell.Text = "<a class= 'btn btn-primary' href = '?Action=EditProd&" +
                                        "ProductID=" + productID.ToString().Replace(" ", string.Empty) +
                                        "' >Edit Product</a>";
                            tblProducts.Rows[count].Cells.Add(cell);
                            count++;
                        }
                    }
                }

            }
            catch (Exception Err)
            {
                function.logAnError(" An error occurred retrieving list of products external products page. Error: " + Err);
            }

            
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
          //create a product object 
            PRODUCT newProduct = new PRODUCT();
          
            newProduct.Name = txtName.Text;
            newProduct.ProductDescription = txtPrice.Text;


            if (drpProductType.SelectedIndex == 0)
            {
                PRODUCT p = new PRODUCT();
                ACCESSORY a = new ACCESSORY();


                if (btnAddProduct.Text != "Save")
                {

                    string ID = function.GenerateRandomProductID();
                    a.TreatmentID = ID;

                    a.Qty = 0;
                }
                else if (btnAddProduct.Text == "Save")
                {

                    a.TreatmentID = productID;
                }
                a.Colour = txtcolour.Text;
                a.BrandID = drpBrandList.SelectedValue.ToString();
                p.Name = txtName.Text;
                p.ProductDescription = txtProductDescription.Text;
                p.Price = Convert.ToDecimal(txtPrice.Text);
                p.ProductType = drpProductType.SelectedValue.ToString();
                a.supplierID = drpListSupplier.SelectedValue.ToString();

                bool result = false;
                if (btnAddProduct.Text != "Save")
                {
                    result = handler.addAccessories(a, p);
                }
                else if (btnAddProduct.Text == "Save")
                {
                    //run updated product BLL method for accessory
                    //result = handler.updateAccessories(a, p);
                }


                if (result == true)
                {
                    if (btnAddProduct.Text != "Save")
                    {
                        Response.Redirect("../Manager/Products.aspx");
                    }
                    else if (btnAddProduct.Text == "Save")
                    {
                        //redirct to product page
                        Response.Redirect("../cheveux/Products.aspx?ProductID="+productID);
                        
                    }
                }
                else
                {
                   Response.Redirect("Error.aspx");
                }
                
            }
            else if (drpProductType.SelectedIndex == 1)
            {
                PRODUCT p = new PRODUCT();
                TREATMENT t = new TREATMENT();


                if (btnAddProduct.Text != "Save")
                {

                    string prodID = function.GenerateRandomProductID();

                    t.TreatmentID = prodID;

                    t.Qty = 0;
                }
                else if (btnAddProduct.Text == "Save")
                {

                    t.TreatmentID = productID;
                }
                t.BrandID = drpBrandList.SelectedValue.ToString();
                p.Name = txtName.Text;
                p.ProductDescription = txtProductDescription.Text;
                p.Price = Convert.ToDecimal(txtPrice.Text);
                p.ProductType = drpProductType.SelectedValue.ToString();
                t.supplierID = drpListSupplier.SelectedValue.ToString();
                t.TreatmentType = txtcolour.Text;
                
                bool result = false;
                if (btnAddProduct.Text != "Save")
                {
                    result = handler.addTreatments(t, p);
                }
                else if (btnAddProduct.Text == "Save")
                {
                    //run updated product BLL method fot treatment
                    //result = handler.updateTreatment(t, p);
                }

                if (result == true)
                {
                    if (btnAddProduct.Text != "Save")
                    {
                        Response.Redirect("../Manager/Products.aspx");
                    }
                    else if (btnAddProduct.Text == "Save")
                    {
                        //redirct to product page
                        Response.Redirect("../cheveux/Products.aspx?ProductID=" + productID);
                    }
                }
                else
                {
                    Response.Redirect("Error.aspx");
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Manager/Products.aspx");
        }
    }
} 