using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;
using BLL;
using System.Collections;

namespace Cheveux
{
    public partial class BusinessSetting : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;
        BUSINESS BusinessDetails = null;
        List<HomePageFeatures> features = null;
        int listBoxSelectedIndex;
        //create bools to track which setings are to be edited
        bool editVatRate = false;
        bool editVatRegNo = false;
        bool editAddress = false;
        bool editPhoneNumber = false;
        bool editWeekDayHours = false;
        bool editWeekEndHours = false;
        bool editPublicHolHours = false;
        bool editLogo = false;

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
                #region load Featured items in to edit selector
                features = null;
                try
                {
                    //get the home page featurs
                    features = handler.GetHomePageFeatures();
                }
                catch (Exception err)
                {
                    function.logAnError("unable to load featurd items from the DB on BusinessSetting.aspx: " +
                        err);
                }
                #endregion
                
                #region view
                //if the user is loged in as a manager display Bussines setting
                LogedIn.Visible = true;
                LogedOut.Visible = false;

                //check if a view has been requested
                string view = Request.QueryString["View"];
                if (view == "BS")
                {
                    btnViewBS_Click(sender, e);
                }
                else if (view == "FI")
                {
                    btnViewFeaturedItems_Click(sender, e);
                }
                else if (view == "SK")
                {
                    btnViewSK_Click(sender, e);
                }
                else
                {
                    btnViewFeaturedItems_Click(sender, e);
                }
                #endregion

                try
                {
                    #region featuredItems
                    #region Featured hairstyles
                    try
                    {
                        lblFeaturedService1.Text = "<a href='?EditFeatureID=" +
                            features[3].FeatureID.ToString()
                            + "'>" + features[3].Name.ToString() + "</a>";

                        lblFeaturedService2.Text = "<a href='?EditFeatureID=" +
                            features[4].FeatureID.ToString()
                            + "'>" + features[4].Name.ToString() + "</a>";

                        lblFeaturedService3.Text = "<a href='?EditFeatureID=" +
                            features[5].FeatureID.ToString()
                            + "'>" + features[5].Name.ToString() + "</a>";

                        lblFeaturedService4.Text = "<a href='?EditFeatureID=" +
                            features[6].FeatureID.ToString()
                            + "'>" + features[6].Name.ToString() + "</a>";
                    }
                    catch (Exception err)
                    {
                        function.logAnError("unable to display featured hairstyls on BusinessSetting.aspx: " +
                            err);
                    }
                    #endregion

                    #region Featured Products
                    //Load Products
                    try
                    {
                        lblFeaturedProduct1.Text = "<a href='?EditFeatureID=" +
                            features[0].FeatureID.ToString()
                            + "'>" + features[0].Name.ToString() + "</a>";

                        lblFeaturedProduct2.Text = "<a href='?EditFeatureID=" +
                            features[1].FeatureID.ToString()
                            + "'>" + features[1].Name.ToString() + "</a>";

                        lblFeaturedProduct3.Text = "<a href='?EditFeatureID=" +
                            features[2].FeatureID.ToString()
                            + "'>" + features[2].Name.ToString() + "</a>";
                    }
                    catch (Exception err)
                    {
                        function.logAnError("unable to display featured products on default.aspx: " +
                            err);
                    }
                    #endregion

                    #region Featured Stylists
                    //Load stylist
                    try
                    {
                        lblFeaturedStylist1.Text = "<a href='?EditFeatureID=" +
                            features[7].FeatureID.ToString()
                            + "'>" + features[7].firstName.ToString() + "</a>";

                        lblFeaturedStylist2.Text = "<a href='?EditFeatureID=" +
                            features[8].FeatureID.ToString()
                            + "'>" + features[8].firstName.ToString() + "</a>";

                        lblFeaturedStylist3.Text = "<a href='?EditFeatureID=" +
                            features[9].FeatureID.ToString()
                            + "'>" + features[9].firstName.ToString() + "</a>";
                    }
                    catch (Exception err)
                    {
                        function.logAnError("unable to display featured stylist on default.aspx: " +
                            err);
                    }
                    #endregion

                    #region Contact Us
                    try
                    {
                        lblFeaturedEmail.Text = "<a href='?EditFeatureID=" +
                            features[11].FeatureID.ToString()
                            + "'>" + features[11].email.ToString() + "</a>";

                        lblFeaturedPhone.Text = "<a href='?EditFeatureID=" +
                            features[10].FeatureID.ToString()
                            + "'>" + features[10].contactNo.ToString() + "</a>";
                    }
                    catch (Exception err)
                    {
                        function.logAnError("unable to display featured stylist on default.aspx: " +
                            err);
                    }
                    #endregion
                    #endregion

                    #region Business Settings
                    //get the curent bussines details
                    BusinessDetails = handler.getBusinessTable();

                    //check if an edit has been requested
                    string edit = Request.QueryString["EditType"];
                    if (edit == "EVR")
                    {
                        btnViewBS_Click(sender, e);
                        //edit vat rate
                        btnEditvatRate.Text = "Save";
                        editVatRate = true;
                        //hide all other edit butons
                        hideEditBTNs(0);
                    }
                    else if (edit == "EVRN")
                    {
                        btnViewBS_Click(sender, e);
                        //edit vat reg num
                        btnEditvatRegNo.Text = "Save";
                        editVatRegNo = true;
                        //hide all other edit butons
                        hideEditBTNs(1);
                    }
                    else if (edit == "ADD")
                    {
                        btnViewBS_Click(sender, e);
                        //edit address
                        btnEditadd.Text = "Save";
                        editAddress = true;
                        //hide all other edit butons
                        hideEditBTNs(2);
                    }
                    else if (edit == "PN")
                    {
                        btnViewBS_Click(sender, e);
                        //edit phone number
                        btnEditPhoneNum.Text = "Save";
                        editPhoneNumber = true;
                        //hide all other edit butons
                        hideEditBTNs(3);
                    }
                    else if (edit == "WDH")
                    {
                        btnViewBS_Click(sender, e);
                        //edit weekday hours
                        btnEditWDHours.Text = "Save";
                        editWeekDayHours = true;
                        //hide all other edit butons
                        hideEditBTNs(4);
                    }
                    else if (edit == "WEH")
                    {
                        btnViewBS_Click(sender, e);
                        //edit Weekend hours
                        btnEditWEHours.Text = "Save";
                        editWeekEndHours = true;
                        //hide all other edit butons
                        hideEditBTNs(5);
                    }
                    else if (edit == "PHH")
                    {
                        btnViewBS_Click(sender, e);
                        //edit public holiday hours
                        btnEditPHHours.Text = "Save";
                        editPublicHolHours = true;
                        //hide all other edit butons
                        hideEditBTNs(6);
                    }
                    else if (edit == "LOGO")
                    {
                        btnViewBS_Click(sender, e);
                        //edit Logo
                        btnEditLogo.Text = "Save";
                        editLogo = true;
                        //hide all other edit butons
                        hideEditBTNs(7);
                    }

                    //fill the table with the current setings
                    fillTable();
                    #endregion
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n getting business data from the db Page_Load on bussines settings");
                    Response.Redirect("Error.aspx?Error='An error occurred when communicating with the Cheveux server'");
                }

                listBoxSelectedIndex = lblFeatuerdItems.SelectedIndex;

                #region display edit for feature
                if (!Page.IsPostBack)
                {
                    string editFeature = Request.QueryString["EditFeatureID"];
                    if (editFeature != null)
                    {
                        hideALL();
                        DivEditFeaturedItem.Visible = true;
                        if (editFeature == "Ser01" || editFeature == "Ser02" || editFeature == "Ser03" || editFeature == "Ser04")
                        {
                            lblListBoxHeader.Text = "Services";
                            if (editFeature == "Ser01")
                            {
                                LblFeatureEditHeading.Text = "Edit Featured Service 1";
                                loadServiceListBox(sender, e, 3);
                            }
                            else if (editFeature == "Ser02")
                            {
                                LblFeatureEditHeading.Text = "Edit Featured Service 2";
                                loadServiceListBox(sender, e, 4);
                            }
                            else if (editFeature == "Ser03")
                            {
                                LblFeatureEditHeading.Text = "Edit Featured Service 3";
                                loadServiceListBox(sender, e, 5);
                            }
                            else if (editFeature == "Ser04")
                            {
                                loadServiceListBox(sender, e, 6);
                                LblFeatureEditHeading.Text = "Edit Featured Service 4";
                            }
                        }
                        else if (editFeature == "Pro01" || editFeature == "Pro02" || editFeature == "Pro03")
                        {
                            TxtSearchProductForAutoLowStock.Text = "";
                            lblListBoxHeader.Text = "Products";
                            if (editFeature == "Pro01")
                            {
                                LblFeatureEditHeading.Text = "Edit Featured Product 1";
                                loadProductListBoxs(sender, e, 0);
                            }
                            else if (editFeature == "Pro02")
                            {
                                LblFeatureEditHeading.Text = "Edit Featured Product 2";
                                loadProductListBoxs(sender, e, 1);
                            }
                            else if (editFeature == "Pro03")
                            {
                                LblFeatureEditHeading.Text = "Edit Featured Product 3";
                                loadProductListBoxs(sender, e, 2);
                            }
                        }
                        else if (editFeature == "Sty01" || editFeature == "Sty02" || editFeature == "Sty03")
                        {
                            lblListBoxHeader.Text = "Stylists";
                            if (editFeature == "Sty01")
                            {
                                loadStylistListBox(sender, e, 7);
                                LblFeatureEditHeading.Text = "Edit Featured Stylist 1";
                            }
                            else if (editFeature == "Sty02")
                            {
                                loadStylistListBox(sender, e, 8);
                                LblFeatureEditHeading.Text = "Edit Featured Stylist 2";
                            }
                            else if (editFeature == "Sty03")
                            {
                                loadStylistListBox(sender, e, 9);
                                LblFeatureEditHeading.Text = "Edit Featured Stylist 3";
                            }
                        }
                        else if (editFeature == "CwuPno" || editFeature == "CwuEma")
                        {
                            lblListBoxHeader.Text = "Employee";
                            if (editFeature == "CwuPno")
                            {
                                LblFeatureEditHeading.Text = "Edit Contact Phone";
                                loadEmpyeeListBox(sender, e, 11);
                            }
                            else if (editFeature == "CwuEma")
                            {
                                LblFeatureEditHeading.Text = "Edit Contact Email";
                                loadEmpyeeListBox(sender, e, 10);
                            }
                        }
                    }
                    
            }
                #endregion
            }
        }
        
        #region View
        protected void btnViewFeaturedItems_Click(object sender, EventArgs e)
        {
            hideALL();
            FI.Visible = true;
        }

        protected void btnViewBS_Click(object sender, EventArgs e)
        {
            hideALL();
            BS.Visible = true;
        }
        
        protected void btnViewSK_Click(object sender, EventArgs e)
        {
            hideALL();

            txtSearchItems.Text = "";
            if (!Page.IsPostBack)
            {
                loadProductListBoxs(sender, e, 0);
            }
            loadStockManagement(sender, e);

            SK.Visible = true;
        }

        private void hideALL()
        {
            BS.Visible = false;
            FI.Visible = false;
            SK.Visible = false;
            DivEditFeaturedItem.Visible = false;
        }

        protected void btnViewHint_Click(object sender, EventArgs e)
        {
            if (btnViewHint.Text == "Hint")
            {
                SettingsHint.Visible = true;
                btnViewHint.Text = "Hide Hint";
            }
            else if (btnViewHint.Text == "Hide Hint")
            {
                SettingsHint.Visible = false;
                btnViewHint.Text = "Hint";
            }
        }

        protected void btnViewSK_Click1(object sender, EventArgs e)
        {
            Response.Redirect("../Manager/BusinessSetting.aspx?View=SK");
        }

        protected void rblAutoStockOrderProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblAutoStockOrderProducts.SelectedIndex == 0)
            {
                divAutoStockOrderProducts.Visible = false;
            }
            else
            {
                divAutoStockOrderProducts.Visible = true;
            }
        }

        protected void cbAutoLowStockOnOff_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoLowStockOnOff.Checked)
            {
                divAutoStockOrder.Visible = true;
                AutoLowInfo.Visible = false;
            }
            else
            {
                divAutoStockOrder.Visible = false;
                AutoLowInfo.Visible = true;
            }
        }

        protected void showSaveStockSetting(object sender, EventArgs e)
        {
            cbAutoLowStockOnOff_CheckedChanged(sender, e);
            rblAutoStockOrderProducts_SelectedIndexChanged(sender, e);
            btnSave.Visible = true;
        }
        #endregion
        
        #region Product ListBoxes
        protected void loadProductListBoxs(object sender, EventArgs e, int featureIndex)
        {
            //add Products to the list
            lblFeatuerdItems.Items.Clear();
            lblProductsForAutoOrder.Items.Clear();
            try
            {
                //load a list of all products
                products = handler.getAllProductsAndDetails();
                if (products.Item1.Count != 0 && products.Item2.Count != 0)
                {

                    //sort the products by alphabetical oder
                    products = Tuple.Create(products.Item1.OrderBy(o => o.Name).ToList(),
                        products.Item2.OrderBy(o => o.Name).ToList());

                    int prodCount = 0;
                    ArrayList ListBoxArray = new ArrayList();
                    lblFeatuerdItems.Items.Clear();

                    //add treatments
                    foreach (SP_GetAllTreatments treat in products.Item2)
                    {
                        //make sure there is stock
                        if (treat.Qty > 0
                            && (function.compareToSearchTerm(treat.Name, txtSearchItems.Text) == true
                            || function.compareToSearchTerm(treat.ProductDescription, txtSearchItems.Text) == true
                            || function.compareToSearchTerm(treat.Brandname, txtSearchItems.Text) == true)
                            && (function.compareToSearchTerm(treat.Name, TxtSearchProductForAutoLowStock.Text) == true
                            || function.compareToSearchTerm(treat.ProductDescription, TxtSearchProductForAutoLowStock.Text) == true
                            || function.compareToSearchTerm(treat.Brandname, TxtSearchProductForAutoLowStock.Text) == true))
                        {
                            lblFeatuerdItems.Items.Add(treat.Name.ToString());
                            prodCount++;
                        }
                    }

                    //add accessories
                    foreach (SP_GetAllAccessories Access in products.Item1)
                    {
                        //make sure there is stock
                        if (Access.Qty > 0
                            && (function.compareToSearchTerm(Access.Name, txtSearchItems.Text) == true
                            || function.compareToSearchTerm(Access.ProductDescription, txtSearchItems.Text) == true
                            || function.compareToSearchTerm(Access.Brandname, txtSearchItems.Text) == true)
                            && (function.compareToSearchTerm(Access.Name, TxtSearchProductForAutoLowStock.Text) == true
                            || function.compareToSearchTerm(Access.ProductDescription, TxtSearchProductForAutoLowStock.Text) == true
                            || function.compareToSearchTerm(Access.Brandname, TxtSearchProductForAutoLowStock.Text) == true))
                        {
                            lblFeatuerdItems.Items.Add(Access.Name.ToString());
                            prodCount++;
                        }
                    }

                    //if no products found matching the criteria
                    if (prodCount == 0)
                    {
                        lblFeatuerdItems.Items.Add("No Products Found");
                        lblProductsForAutoOrder.Items.Add("No Products Found");
                    }
                    else
                    {
                        for (int i = 0; i < lblFeatuerdItems.Items.Count; i++)
                        {
                            ListBoxArray.Add(lblFeatuerdItems.Items[i].Value);
                        }

                        int x = 0;
                        ListBoxArray.Sort();
                        lblFeatuerdItems.Items.Clear();
                        lblProductsForAutoOrder.Items.Clear();
                        foreach (string item in ListBoxArray)
                        {
                            lblFeatuerdItems.Items.Add(item);
                            lblProductsForAutoOrder.Items.Add(item);
                            if (item == features[featureIndex].Name)
                            {
                                lblFeatuerdItems.SelectedIndex = x;
                            }
                            x++;
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert('An error occoured loading products, Please try again later.');location.reload(true);</script>");
                    btnViewFeaturedItems_Click(sender, e);
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving list of products"
                    + " in loadProductListBox method on Settings");
                Response.Write("<script>alert('An error occoured loading products, Please try again later.');location.reload(true);</script>");
                btnViewFeaturedItems_Click(sender, e);
            }
        }

        List<string> productIDs = new List<string>();

        protected void loadproductIDs()
        {
            productIDs.Clear();
            productIDs.Add("");
            productIDs.Add("");
            productIDs.Add("");

            //load the product ids
            products = handler.getAllProductsAndDetails();
            if (products.Item1.Count != 0 && products.Item2.Count != 0)
            {
                //add treatments
                foreach (SP_GetAllTreatments treat in products.Item2)
                {
                    if (treat.Name == lblFeatuerdItems.SelectedItem.Text)
                    {
                        productIDs[0] = treat.ProductID.ToString();
                    }

                    if(lblProductsForAutoOrder.SelectedIndex != -1)
                    {
                        if (treat.Name == lblProductsForAutoOrder.SelectedItem.Text)
                        {
                            productIDs[1] = treat.ProductID.ToString();
                        }
                    }

                    if (lblProductsOnAutoOrder.SelectedIndex != -1)
                    {
                        string[] array = lblProductsOnAutoOrder.SelectedItem.Text.Split('*');
                        array[1] = array[1].Substring(1);
                        if (treat.Name == array[1])
                        {
                            productIDs[2] = treat.ProductID.ToString();
                        }
                    }
                }

                //add accessories
                foreach (SP_GetAllAccessories Access in products.Item1)
                {
                    if (Access.Name == lblFeatuerdItems.SelectedItem.Text)
                    {
                        productIDs[0] = Access.ProductID.ToString();
                    }

                    if (lblProductsForAutoOrder.SelectedIndex != -1)
                    {
                        if (Access.Name == lblProductsForAutoOrder.SelectedItem.Text)
                        {
                            productIDs[1] = Access.ProductID.ToString();
                        }
                    }

                    if (lblProductsOnAutoOrder.SelectedIndex != -1)
                    {
                        string[] array = lblProductsOnAutoOrder.SelectedItem.Text.Split('*');
                        array[1] = array[1].Substring(1);
                        if (Access.Name == array[1])
                        {
                            productIDs[2] = Access.ProductID.ToString();
                        }
                    }
                }
            }
        }
        #endregion

        #region Featured Items
        int selectedIndex = 0;

        #region Service
        List<PRODUCT> services = null;
        List<string> serviceIDs = new List<string>();

        protected void loadServiceListBox(object sender, EventArgs e, int featureIndex)
        {
            //add Products to the list
            lblFeatuerdItems.Items.Clear();
            selectedIndex = 0;
            try
            {
                //load a list of all products
                services = handler.getAllProducts();
                if (services.Count != 0)
                {
                    int serviceCount = 0;
                    ArrayList ListBoxArray = new ArrayList();
                    lblFeatuerdItems.Items.Clear();

                    //add treatments
                    foreach (PRODUCT service in services)
                    {
                        //make sure there is stock
                        if (service.ProductType[0] == 'S' &&
                            (function.compareToSearchTerm(service.Name, txtSearchItems.Text) == true
                            || function.compareToSearchTerm(service.ProductDescription, txtSearchItems.Text) == true))
                        {
                            lblFeatuerdItems.Items.Add(service.Name.ToString());
                            serviceCount++;
                        }
                    }

                    //if no products found matching the criteria
                    if (serviceCount == 0)
                    {
                        lblFeatuerdItems.Items.Add("No Services Found");
                    }
                    else
                    {
                        for (int i = 0; i < lblFeatuerdItems.Items.Count; i++)
                        {
                            ListBoxArray.Add(lblFeatuerdItems.Items[i].Value);
                        }

                        int x = 0;
                        ListBoxArray.Sort();
                        lblFeatuerdItems.Items.Clear();
                        foreach (string item in ListBoxArray)
                        {
                            lblFeatuerdItems.Items.Add(item);
                            if (item == features[featureIndex].Name)
                            {
                                selectedIndex = x;
                            }
                            x++;
                        }

                        lblFeatuerdItems.SelectedIndex = selectedIndex;
                    }
                }
                else
                {
                    Response.Write("<script>alert('An error occoured loading services, Please try again later.');location.reload(true);</script>");
                    btnViewFeaturedItems_Click(sender, e);
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving list of service"
                    + " in loadServiceListBox method on Settings");
                Response.Write("<script>alert('An error occoured loading services, Please try again later.');location.reload(true);</script>");
                btnViewFeaturedItems_Click(sender, e);
            }
        }

        protected void loadServiceIDs(object sender, EventArgs e)
        {
            //add Products to the list
            serviceIDs.Clear();
            try
            {
                services = handler.getAllProducts();
                if (services.Count != 0)
                {
                    foreach (PRODUCT service in services)
                    {
                        //make sure there is stock
                        if (service.ProductType[0] == 'S' &&
                            (service.Name == lblFeatuerdItems.Items[listBoxSelectedIndex].Text))
                        {
                            serviceIDs.Add(service.ProductID.ToString());
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert('An error occoured loading service, Please try again later.');location.reload(true);</script>");
                    btnViewFeaturedItems_Click(sender, e);
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving list of service"
                    + " in loadServiceIDs method on Settings");
                Response.Write("<script>alert('An error occoured loading service, Please try again later.');location.reload(true);</script>");
                btnViewFeaturedItems_Click(sender, e);
            }
        }
        #endregion

        #region Product
        Tuple<List<SP_GetAllAccessories>, List<SP_GetAllTreatments>> products = null;
        #endregion

        List<SP_ViewEmployee> employees = null;
        
        #region Stylist
        List<string> stylistIDs = new List<string>();

        protected void loadStylistListBox(object sender, EventArgs e, int featureIndex)
        {
            if (!Page.IsPostBack)
            {
                //add Products to the list
                lblFeatuerdItems.Items.Clear();
                selectedIndex = 0;
                try
                {
                    //load a list of all products
                    employees = handler.viewAllEmployees();
                    if (employees.Count != 0)
                    {
                        int empCount = 0;
                        ArrayList ListBoxArray = new ArrayList();
                        lblFeatuerdItems.Items.Clear();

                        //add treatments
                        foreach (SP_ViewEmployee stylist in employees)
                        {
                            //make sure there is stock
                            if (stylist.employeeType[0] == 'S' &&
                                (function.compareToSearchTerm(stylist.firstName, txtSearchItems.Text) == true
                                || function.compareToSearchTerm(stylist.lastName, txtSearchItems.Text) == true
                                || function.compareToSearchTerm(stylist.email, txtSearchItems.Text) == true
                                || function.compareToSearchTerm(stylist.phoneNumber, txtSearchItems.Text) == true))
                            {
                                lblFeatuerdItems.Items.Add(stylist.firstName.ToString());
                                empCount++;
                            }
                        }

                        //if no products found matching the criteria
                        if (empCount == 0)
                        {
                            lblFeatuerdItems.Items.Add("No Stylist Found");
                        }
                        else
                        {
                            for (int i = 0; i < lblFeatuerdItems.Items.Count; i++)
                            {
                                ListBoxArray.Add(lblFeatuerdItems.Items[i].Value);
                            }

                            int x = 0;
                            ListBoxArray.Sort();
                            lblFeatuerdItems.Items.Clear();
                            foreach (string item in ListBoxArray)
                            {
                                lblFeatuerdItems.Items.Add(item);
                                if (item == features[featureIndex].firstName)
                                {
                                    selectedIndex = x;
                                }
                                x++;
                            }

                            lblFeatuerdItems.SelectedIndex = selectedIndex;
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('An error occoured loading stylists, Please try again later.');location.reload(true);</script>");
                        btnViewFeaturedItems_Click(sender, e);
                    }
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString()
                        + " An error occurred retrieving list of stylist"
                        + " in loadStylistListBox method on Settings");
                    Response.Write("<script>alert('An error occoured loading stylist, Please try again later.');location.reload(true);</script>");
                    btnViewFeaturedItems_Click(sender, e);
                }
            }
        }

        protected void loadStylistIDs(object sender, EventArgs e)
        {
            //add Products to the list
            stylistIDs.Clear();
            selectedIndex = 0;
            try
            {
                //load a list of all products
                employees = handler.viewAllEmployees();
                if (employees.Count != 0)
                {
                    foreach (SP_ViewEmployee stylist in employees)
                    {
                        
                        if (stylist.firstName == lblFeatuerdItems.Items[listBoxSelectedIndex].Text)
                        {
                            stylistIDs.Add(stylist.UserID.ToString());
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert('An error occoured loading stylists, Please try again later.');location.reload(true);</script>");
                    btnViewFeaturedItems_Click(sender, e);
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving list of stylist"
                    + " in loadStylistIDs method on Settings");
                Response.Write("<script>alert('An error occoured loading stylist, Please try again later.');location.reload(true);</script>");
                btnViewFeaturedItems_Click(sender, e);
            }
        }
        #endregion

        #region Contact Us
        List<string> employeeIDs = new List<string>();

        protected void loadEmpyeeListBox(object sender, EventArgs e, int featureIndex)
        {
            //add Products to the list
            lblFeatuerdItems.Items.Clear();
            selectedIndex = 0;
            try
            {
                //load a list of all products
                employees = handler.viewAllEmployees();
                if (employees.Count != 0)
                {
                    int empCount = 0;
                    ArrayList ListBoxArray = new ArrayList();
                    lblFeatuerdItems.Items.Clear();

                    //add treatments
                    foreach (SP_ViewEmployee stylist in employees)
                    {
                        //make sure there is stock
                        if (stylist.employeeType[0] != 'S' &&
                            (function.compareToSearchTerm(stylist.firstName, txtSearchItems.Text) == true
                            || function.compareToSearchTerm(stylist.lastName, txtSearchItems.Text) == true
                            || function.compareToSearchTerm(stylist.email, txtSearchItems.Text) == true
                            || function.compareToSearchTerm(stylist.phoneNumber, txtSearchItems.Text) == true))
                        {
                            lblFeatuerdItems.Items.Add(stylist.firstName.ToString());
                            empCount++;
                        }
                    }

                    //if no products found matching the criteria
                    if (empCount == 0)
                    {
                        lblFeatuerdItems.Items.Add("No Employees Found");
                    }
                    else
                    {
                        for (int i = 0; i < lblFeatuerdItems.Items.Count; i++)
                        {
                            ListBoxArray.Add(lblFeatuerdItems.Items[i].Value);
                        }

                        int x = 0;
                        ListBoxArray.Sort();
                        lblFeatuerdItems.Items.Clear();
                        foreach (string item in ListBoxArray)
                        {
                            lblFeatuerdItems.Items.Add(item);
                            if (item == features[featureIndex].firstName)
                            {
                                selectedIndex = x;
                            }
                            x++;
                        }

                        lblFeatuerdItems.SelectedIndex = selectedIndex;
                    }
                }
                else
                {
                    Response.Write("<script>alert('An error occoured loading employee, Please try again later.');location.reload(true);</script>");
                    btnViewFeaturedItems_Click(sender, e);
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving list of stylist"
                    + " in loadEmpyeeListBox method on Settings");
                Response.Write("<script>alert('An error occoured employee stylist, Please try again later.');location.reload(true);</script>");
                btnViewFeaturedItems_Click(sender, e);
            }
        }

        protected void loadEmaloyeeIDs(object sender, EventArgs e)
        {
            //add Products to the list
            employeeIDs.Clear();
            try
            {
                //load a list of all products
                employees = handler.viewAllEmployees();
                if (employees.Count != 0)
                {
                    foreach (SP_ViewEmployee stylist in employees)
                    {
                        if (stylist.firstName == lblFeatuerdItems.Items[listBoxSelectedIndex].Text)
                        {
                            employeeIDs.Add(stylist.UserID.ToString());
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert('An error occoured loading employee, Please try again later.');location.reload(true);</script>");
                    btnViewFeaturedItems_Click(sender, e);
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving list of stylist"
                    + " in loadEmaloyeeIDs method on Settings");
                Response.Write("<script>alert('An error occoured loading employee, Please try again later.');location.reload(true);</script>");
                btnViewFeaturedItems_Click(sender, e);
            }
        }
        #endregion

        protected void btnDoneFatureEdit_Click(object sender, EventArgs e)
        {
            lblErrorFeaturedItem.Visible = false;

            bool result = false;

            try
            {
                if (lblFeatuerdItems.SelectedItem != null && lblFeatuerdItems.SelectedItem.Text != "" && lblFeatuerdItems.SelectedIndex >= 0)
                {
                    string editFeature = Request.QueryString["EditFeatureID"];
                    if (editFeature != null)
                    {
                        Home_Page updatedFeture = new Home_Page();
                        updatedFeture.FeatureID = editFeature;

                        if (editFeature == "Ser01" || editFeature == "Ser02" || editFeature == "Ser03" || editFeature == "Ser04")
                        {
                            loadServiceIDs(sender, e);
                            updatedFeture.ItemID = serviceIDs[0];
                        }
                        else if (editFeature == "Pro01" || editFeature == "Pro02" || editFeature == "Pro03")
                        {
                            loadproductIDs();
                            updatedFeture.ItemID = productIDs[0];
                        }
                        else if (editFeature == "Sty01" || editFeature == "Sty02" || editFeature == "Sty03")
                        {
                            loadStylistIDs(sender, e);
                            updatedFeture.ItemID = stylistIDs[0];
                        }
                        else if (editFeature == "CwuPno" || editFeature == "CwuEma")
                        {
                            loadEmaloyeeIDs(sender, e);
                            updatedFeture.ItemID = employeeIDs[0];
                        }

                        result = handler.UpdatedHomePageFeatures(updatedFeture);
                    }
                }
                else
                {
                    lblErrorFeaturedItem.Visible = true;
                }
            }
            catch (Exception Err)
            {
                function.logAnError("Error Updating homepaeferes - business setings page. | Err: "+Err);
                Response.Write("<script>alert('An error occurred updating the database try again later');</script>");
                Response.Redirect("BusinessSetting.aspx?View=FI");
            }

            if (result == true)
            {
                Response.Redirect("BusinessSetting.aspx?View=FI");
            }
            else if(lblFeatuerdItems.SelectedIndex < 0)
            {
                lblErrorFeaturedItem.Visible = true;
            }
            else
            {
                Response.Write("<script>alert('An error occurred updating the database try again later');</script>");
                Response.Redirect("BusinessSetting.aspx?View=FI");
            }
        }

        protected void btnCancelFatureEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("BusinessSetting.aspx?View=FI");
        }
        #endregion

        #region Business Settings
        public void hideEditBTNs(int index)
        {
            //give a row index it hides all edit butons exept the one in the given index
            if (index != 0)
            {
                //hide edit btn for vat rate
                btnEditvatRate.Visible = false;

            }
            if (index != 1)
            {
                //hide edit btn for vat reg num
                btnEditvatRegNo.Visible = false;
            }
            if (index != 2)
            {
                //hide edit btn for address
                btnEditadd.Visible = false;
            }
            if (index != 3)
            {
                //hide edit btn for phone number
                btnEditPhoneNum.Visible = false;
            }
            if (index != 4)
            {
                //hide edit btn for weekday hours
                btnEditWDHours.Visible = false;
            }
            if (index != 5)
            {
                //hide edit btn for Weekend hours
                btnEditWEHours.Visible = false;
            }
            if (index != 6)
            {
                //hide edit btn for public holiday hours
                btnEditPHHours.Visible = false;
            }
            if (index != 7)
            {
                //hide edit btn for Logo
                btnEditLogo.Visible = false;
            }
        }

        public void fillTable()
        {
            try
            {
                //check wheather an edit has been requested before displaying the currant value as ethier a placeholder or text
                //vat rate
                if (editVatRate == false)
                {
                    tblBussinesSettings.Rows[0].Cells[1].Text = BusinessDetails.Vat + "%";
                }
                else
                {
                    vatRate.Attributes.Add("placeholder", BusinessDetails.Vat + "%");
                    // add a Cancel button
                    TableCell cancelLinkCell = new TableCell();
                    cancelLinkCell.Text= "<a href ='BusinessSetting.aspx?View=BS'> Cancel </a>";
                    tblBussinesSettings.Rows[0].Cells.Add(cancelLinkCell);
                }

                //vat Reg No
                if (editVatRegNo == false)
                {
                    tblBussinesSettings.Rows[1].Cells[1].Text = BusinessDetails.VatRegNo;
                }
                else
                {
                    vatRegNo.Attributes.Add("placeholder", BusinessDetails.VatRegNo);
                    // add a Cancel button
                    TableCell cancelLinkCell = new TableCell();
                    cancelLinkCell.Text = "<a href ='BusinessSetting.aspx?View=BS'> Cancel </a>";
                    tblBussinesSettings.Rows[1].Cells.Add(cancelLinkCell);
                }

                //address
                if (editAddress == false)
                {
                    //address line 1
                    tblBussinesSettings.Rows[2].Cells[1].Text = BusinessDetails.AddressLine1;
                    //ddress line 2
                    tblBussinesSettings.Rows[3].Cells[1].Text = BusinessDetails.AddressLine2;
                }
                else
                {
                    addLineOne.Attributes.Add("placeholder", BusinessDetails.AddressLine1);
                    addLineTwo.Attributes.Add("placeholder", BusinessDetails.AddressLine2);
                    // add a Cancel button
                    TableCell cancelLinkCell = new TableCell();
                    cancelLinkCell.Text = "<a href ='BusinessSetting.aspx?View=BS'> Cancel </a>";
                    tblBussinesSettings.Rows[3].Cells.Add(cancelLinkCell);
                }

                //Phone Number
                if (editPhoneNumber == false)
                {
                    tblBussinesSettings.Rows[4].Cells[1].Text = BusinessDetails.Phone;
                }
                else
                {
                    phoneNumber.Attributes.Add("placeholder", BusinessDetails.Phone);
                    // add a Cancel button
                    TableCell cancelLinkCell = new TableCell();
                    cancelLinkCell.Text = "<a href ='BusinessSetting.aspx?View=BS'> Cancel </a>";
                    tblBussinesSettings.Rows[4].Cells.Add(cancelLinkCell);
                }

                //Weekend Hours
                if (editWeekDayHours == false)
                {
                    tblBussinesSettings.Rows[5].Cells[1].Text = BusinessDetails.WeekdayStart.ToString("HH:mm")
                    + " - " + BusinessDetails.WeekdayEnd.ToString("HH:mm");
                }
                else
                {
                    wDStart.Attributes.Add("placeholder", BusinessDetails.WeekdayStart.ToString("HH:mm"));
                    wDEnd.Attributes.Add("placeholder", BusinessDetails.WeekdayEnd.ToString("HH:mm"));
                    // add a Cancel button
                    TableCell cancelLinkCell = new TableCell();
                    cancelLinkCell.Text = "<a href ='BusinessSetting.aspx?View=BS'> Cancel </a>";
                    tblBussinesSettings.Rows[5].Cells.Add(cancelLinkCell);
                }

                //Weekend Hours
                if (editWeekEndHours == false)
                {
                    tblBussinesSettings.Rows[6].Cells[1].Text = BusinessDetails.WeekendStart.ToString("HH:mm")
                    + " - " + BusinessDetails.WeekendEnd.ToString("HH:mm");
                }
                else
                {
                    wEStart.Attributes.Add("placeholder", BusinessDetails.WeekendStart.ToString("HH:mm"));
                    wEEnd.Attributes.Add("placeholder", BusinessDetails.WeekendEnd.ToString("HH:mm"));
                    // add a Cancel button
                    TableCell cancelLinkCell = new TableCell();
                    cancelLinkCell.Text = "<a href ='BusinessSetting.aspx?View=BS'> Cancel </a>";
                    tblBussinesSettings.Rows[6].Cells.Add(cancelLinkCell);
                }

                //public holiday Hours
                if (editPublicHolHours == false)
                {
                    tblBussinesSettings.Rows[7].Cells[1].Text = BusinessDetails.PublicHolStart.ToString("HH:mm")
                    + " - " + BusinessDetails.PublicHolEnd.ToString("HH:mm");
                }
                else
                {
                    pHStart.Attributes.Add("placeholder", BusinessDetails.PublicHolStart.ToString("HH:mm"));
                    pHEnd.Attributes.Add("placeholder", BusinessDetails.PublicHolEnd.ToString("HH:mm"));
                    // add a Cancel button
                    TableCell cancelLinkCell = new TableCell();
                    cancelLinkCell.Text = "<a href ='BusinessSetting.aspx?View=BS'> Cancel </a>";
                    tblBussinesSettings.Rows[7].Cells.Add(cancelLinkCell);
                }

                //logo
                if(editLogo == false)
                {
                    
                }
                else
                {
                    // add a Cancel button
                    TableCell cancelLinkCell = new TableCell();
                    cancelLinkCell.Text = "<a href ='BusinessSetting.aspx?View=BS'> Cancel </a>";
                    tblBussinesSettings.Rows[8].Cells.Add(cancelLinkCell);
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString() + "\n filling the tblBussinesSettings on the business setings page");
                Response.Redirect("Error.aspx?Error='An error displaying the page'");
            }
        }

        protected void btnEnitvatRate_Click(object sender, EventArgs e)
        {
            //check if the the edit is beening requested or if the edit should be saved
            if(editVatRate == false)
            {
                //displaye the text box
                Response.Redirect("BusinessSetting.aspx?EditType=EVR");
            }
            else if(editVatRate == true)
            {
                //save the edit
                try
                {
                    //update the database
                    handler.updateVatRate(BusinessDetails.BusinessID, int.Parse(vatRate.Text.Substring(0, 2)));
                    //refresh the page to show the updates
                    Response.Redirect("BusinessSetting.aspx?View=BS");
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n Updating Vat Rate business setings page");
                    Response.Write("<script>alert('An error occurred updating the database try again later');</script>");
                    Response.Redirect("BusinessSetting.aspx?View=BS");
                }
            }
        }

        protected void btnEnitvatRegNo_Click(object sender, EventArgs e)
        {
            //check if the the edit is beening requested or if the edit should be saved
            if (editVatRegNo == false)
            {
                //displaye the text box
                Response.Redirect("BusinessSetting.aspx?EditType=EVRN");
            }
            else if (editVatRegNo == true)
            {
                //save the edit
                try
                {
                    //update the database
                    handler.updateVatRegNo(BusinessDetails.BusinessID, vatRegNo.Text);
                    //refresh the page to show the updates
                    Response.Redirect("BusinessSetting.aspx?View=BS");
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n Updating Vat Reg no business setings page");
                    Response.Write("<script>alert('An error occurred updating the database try again later');</script>");
                    Response.Redirect("BusinessSetting.aspx?View=BS");
                }
            }
        }

        protected void btnEditadd_Click(object sender, EventArgs e)
        {
            //check if the the edit is beening requested or if the edit should be saved
            if (editAddress == false)
            {
                //displaye the text box
                Response.Redirect("BusinessSetting.aspx?EditType=ADD");
            }
            else if (editAddress == true)
            {
                //save the edit
                try
                {
                    //update the database
                    handler.updateAddress(BusinessDetails.BusinessID, addLineOne.Text, addLineTwo.Text);
                    //refresh the page to show the updates
                    Response.Redirect("BusinessSetting.aspx?View=BS");
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n Updating address - business setings page");
                    Response.Write("<script>alert('An error occurred updating the database try again later');</script>");
                    Response.Redirect("BusinessSetting.aspx?View=BS");
                }
            }
        }

        protected void btnEditPhoneNum_Click(object sender, EventArgs e)
        {
            //check if the the edit is beening requested or if the edit should be saved
            if (editPhoneNumber == false)
            {
                //displaye the text box
                Response.Redirect("BusinessSetting.aspx?EditType=PN");
            }
            else if (editPhoneNumber == true)
            {
                //save the edit
                try
                {
                    //update the database
                    handler.updatePhoneNumber(BusinessDetails.BusinessID, phoneNumber.Text);
                    //refresh the page to show the updates
                    Response.Redirect("BusinessSetting.aspx?View=BS");
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n Updating phone number - business setings page");
                    Response.Write("<script>alert('An error occurred updating the database try again later');</script>");
                    Response.Redirect("BusinessSetting.aspx?View=BS");
                }
            }
        }

        protected void btnEditWDHours_Click(object sender, EventArgs e)
        {
            //check if the the edit is beening requested or if the edit should be saved
            if (editWeekDayHours == false)
            {
                //displaye the text box
                Response.Redirect("BusinessSetting.aspx?EditType=WDH");
            }
            else if (editWeekDayHours == true)
            {
                //save the edit
                try
                {
                    //update the database
                    handler.updateWeekdayHours(BusinessDetails.BusinessID, Convert.ToDateTime(wDStart.Text), Convert.ToDateTime(wDEnd.Text));
                    //refresh the page to show the updates
                    Response.Redirect("BusinessSetting.aspx?View=BS");
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n Updating weekday hours - business setings page");
                    Response.Write("<script>alert('An error occurred updating the database try again later');</script>");
                    Response.Redirect("BusinessSetting.aspx?View=BS");
                }
            }
        }

        protected void btnEditWEHours_Click(object sender, EventArgs e)
        {
            //check if the the edit is beening requested or if the edit should be saved
            if (editWeekEndHours == false)
            {
                //displaye the text box
                Response.Redirect("BusinessSetting.aspx?EditType=WEH");
            }
            else if (editWeekEndHours == true)
            {
                //save the edit
                try
                {
                    //update the database
                    handler.updateWeekendHours(BusinessDetails.BusinessID, Convert.ToDateTime(wEStart.Text), Convert.ToDateTime(wEEnd.Text));
                    //refresh the page to show the updates
                    Response.Redirect("BusinessSetting.aspx?View=BS");
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n Updating weekday hours - business setings page");
                    Response.Write("<script>alert('An error occurred updating the database try again later');</script>");
                    Response.Redirect("BusinessSetting.aspx?View=BS");
                }
            }
        }

        protected void btnEditPHHours_Click(object sender, EventArgs e)
        {
            //check if the the edit is beening requested or if the edit should be saved
            if (editPublicHolHours == false)
            {
                //displaye the text box
                Response.Redirect("BusinessSetting.aspx?EditType=PHH");
            }
            else if (editPublicHolHours == true)
            {
                //save the edit
                try
                {
                    //update the database
                    handler.updatePublicHolidayHours(BusinessDetails.BusinessID, Convert.ToDateTime(pHStart.Text), Convert.ToDateTime(pHEnd.Text));
                    //refresh the page to show the updates
                    Response.Redirect("BusinessSetting.aspx?View=BS");
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n Updating weekday hours - business setings page");
                    Response.Write("<script>alert('An error occurred updating the database try again later');</script>");
                    Response.Redirect("BusinessSetting.aspx?View=BS");
                }
            }
        }

        protected void btnEditLogo_Click(object sender, EventArgs e)
        {
            //check if the the edit is beening requested or if the edit should be saved
            if (editLogo == false)
            {
                //displaye the text box
                Response.Redirect("BusinessSetting.aspx?EditType=LOGO");
            }
            else if (editLogo == true)
            {
                //save the edit
            }
        }
        #endregion

        #region Stock Managment Settings
        Stock_Management stockSettings = null;
        Auto_Purchase_Products AutoPurchProd = null;
        List<SP_GetAuto_Purchase_Products> AutoPurchProds = null;

        public void loadStockManagement(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    stockSettings = handler.getStockSettings();
                    txtLowStock.Text = stockSettings.LowStock.ToString();
                    txtPurchaseQty.Text = stockSettings.PurchaseQty.ToString();
                    ddlQty.SelectedValue = stockSettings.PurchaseQty.ToString();
                    lblNextAutoOrderDate.Text = stockSettings.NxtOrderdDate.ToString("dd MMM yyyy");
                    if (stockSettings.AutoPurchase == true)
                    {
                        cbAutoLowStockOnOff.Checked = true;
                        AutoLowInfo.Visible = false;
                        divAutoStockOrder.Visible = true;
                    }
                    else
                    {
                        cbAutoLowStockOnOff.Checked = false;
                        AutoLowInfo.Visible = true;
                        divAutoStockOrder.Visible = false;
                    }
                    ddlAutoStockOrderFrequency.SelectedValue = stockSettings.AutoPurchaseFrequency;
                    if (stockSettings.AutoPurchaseProducts == true)
                    {
                        rblAutoStockOrderProducts.SelectedValue = "True";
                        divAutoStockOrderProducts.Visible = true;
                    }
                    else
                    {
                        rblAutoStockOrderProducts.SelectedValue = "False";
                        divAutoStockOrderProducts.Visible = false;
                    }
                    loadAutoPurchaseProducts();
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString()
                        + " An error occurred Stock Management Settings"
                        + " in loadStockManagement method on Settings");
                    Response.Write("<script>alert('An error occoured loading settings, Please try again later.');location.reload(true);</script>");
                    btnViewFeaturedItems_Click(sender, e);
                }

                if (stockSettings.NxtOrderdDate < DateTime.Now)
                {
                    stockSettings.NxtOrderdDate = function.updateAutoOrderDate(ddlAutoStockOrderFrequency.SelectedValue.ToString());
                    updateStockManagement(sender, e);
                }
            }
        }
        
        public void updateStockManagement(object sender, EventArgs e)
        {
            try
            {
                if(stockSettings == null)
                {
                    stockSettings = handler.getStockSettings();
                }

                stockSettings.LowStock = int.Parse(txtLowStock.Text);
                stockSettings.PurchaseQty = int.Parse(txtPurchaseQty.Text);
                if (cbAutoLowStockOnOff.Checked == true)
                {
                    stockSettings.AutoPurchase = true;
                }
                else
                {
                    stockSettings.AutoPurchase = false;
                }
                stockSettings.AutoPurchaseFrequency = ddlAutoStockOrderFrequency.SelectedValue;
                if (rblAutoStockOrderProducts.SelectedValue == "True")
                {
                    stockSettings.AutoPurchaseProducts = true;
                }
                else
                {
                    stockSettings.AutoPurchaseProducts = false;
                }
                stockSettings.NxtOrderdDate = function.updateAutoOrderDate(ddlAutoStockOrderFrequency.SelectedValue.ToString());
                handler.updateStockSettings(stockSettings);
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred updating Stock Management Settings"
                    + " in updateStockManagement method on Settings");
                Response.Write("<script>alert('An error occoured updating settings, Please try again later.');location.reload(true);</script>");
                Response.Redirect("http://sict-iis.nmmu.ac.za/beauxdebut/error.aspx?Error=An error occoured updating settings, Please try again later.");
            }
            Response.Redirect("BusinessSetting.aspx?View=SK");
        }
        
        public void loadAutoPurchaseProducts()
        {
            lblProductsOnAutoOrder.Items.Clear();
                try
                {
                //load a list of products
                AutoPurchProds = handler.getAutoPurchOrdProds();
                if (AutoPurchProds.Count != 0)
                {
                    foreach (SP_GetAuto_Purchase_Products Prod in AutoPurchProds)
                    {
                        lblProductsOnAutoOrder.Items.Add(Prod.Qty + "* " + Prod.Name);
                        for (int i = 0; i < lblProductsForAutoOrder.Items.Count; i++)
                        {
                            if (lblProductsForAutoOrder.Items[i].Text == Prod.Name)
                            {
                                lblProductsForAutoOrder.Items.Remove(lblProductsForAutoOrder.Items[i]);
                            }
                        }
                    }
                }
                    else
                    {
                        Response.Write("<script>alert('An error occoured loading products, Please try again later.');location.reload(true);</script>");
                    Response.Redirect("http://sict-iis.nmmu.ac.za/beauxdebut/error.aspx?Error=An error occoured loading products, Please try again later.");
                }
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString()
                        + " An error occurred retrieving list of products on auto order"
                        + " in loadAutoPurchaseProducts method on Settings");
                    Response.Write("<script>alert('An error occoured loading products, Please try again later.');location.reload(true);</script>");
                Response.Redirect("http://sict-iis.nmmu.ac.za/beauxdebut/error.aspx?Error=An error occoured loading products, Please try again later.");
            }
        }

        protected void btnAddProductToAutoOrder_Click(object sender, EventArgs e)
        {
            bool success = false;

            try
            {
                loadproductIDs();
                if (productIDs[1] != "")
                {
                    AutoPurchProd = new Auto_Purchase_Products();
                    AutoPurchProd.ProductID = productIDs[1];
                    AutoPurchProd.Qty = int.Parse(ddlQty.SelectedValue.ToString());
                    success = handler.newAutoPurchProd(AutoPurchProd);
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred adding product to auto order"
                    + " in btnAddProductToAutoOrder_Click method on Settings");
                Response.Redirect("http://sict-iis.nmmu.ac.za/beauxdebut/error.aspx?Error=An error occoured adding the product, Please try again later.");
            }

            if (success == true)
            {
                Response.Redirect("BusinessSetting.aspx?View=SK");
            }
            else
            {
                Response.Redirect("http://sict-iis.nmmu.ac.za/beauxdebut/error.aspx?Error=An error occoured adding the product, Please try again later.");
            }
        }

        protected void btnAddRemoveToAutoOrder_Click(object sender, EventArgs e)
        {
            bool success = false;

            try
            {
                loadproductIDs();
                if (productIDs[2] != "")
                {
                    AutoPurchProd = new Auto_Purchase_Products();
                    AutoPurchProd.ProductID = productIDs[2];
                    success = handler.deleteAutoPurchProd(AutoPurchProd);
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred removing product from auto order"
                    + " in btnAddRemoveToAutoOrder_Click method on Settings");
                Response.Redirect("http://sict-iis.nmmu.ac.za/beauxdebut/error.aspx?Error=An error occoured removing the product, Please try again later.");
            }

            if (success == true)
            {
                Response.Redirect("BusinessSetting.aspx?View=SK");
            }
            else
            {
                Response.Redirect("http://sict-iis.nmmu.ac.za/beauxdebut/error.aspx?Error=An error occoured removing the product, Please try again later.");
            }
        }
        #endregion
    }
}