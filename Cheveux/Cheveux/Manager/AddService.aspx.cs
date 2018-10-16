using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;
using System.Drawing;
using System.IO;

namespace Cheveux.Manager
{
    public partial class AddService : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        List<SP_GetStyles> styleList = null;
        List<SP_GetWidth> widthList = null;
        List<SP_GetLength> lengthList = null;
        PRODUCT product = null;
        SERVICE service = null;
        BRAID_SERVICE bservice = null;
        List<ProductType> prodTypes = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Access Control
            HttpCookie cookie = Request.Cookies["CheveuxUserID"];
            if (cookie == null)
            {
                Response.Redirect("../Manager/Service.aspx");
            }
            else if (cookie["UT"] != "M")
            {
                Response.Redirect("../Default.aspx");
            }
            else if (cookie["UT"] == "M")
            {
                //manager is allowed access
            }
            #endregion

            if (!Page.IsPostBack)
            {
                drpType.Items.Clear();

                drpType.Items.Add(new ListItem(" --Select Type-- ", "0"));
                try
                {
                    prodTypes = handler.getProductTypes();
                    foreach (ProductType productType in prodTypes)
                    {
                        if (productType.ProductOrService == 'S' && productType.name.Replace(" ", string.Empty) != "Service" && productType.name.Replace(" ", string.Empty) != "Employee Leave".Replace(" ", string.Empty))
                        {
                            drpType.Items.Add(new ListItem(productType.name,
                                productType.typeID.Replace(" ", string.Empty)));
                        }
                    }
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "Unable to load drpType on Add Service Page");
                }

                drpType.SelectedIndex = 0;
            }
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(drpType.SelectedValue == "0")
            {
                divBraidDetails.Visible = false;
                lblTypeValidation.Visible = true;
                lblTypeValidation.ForeColor = Color.Red;
            }
            else if(drpType.SelectedValue == "B")
            {
                styleList = handler.BLL_GetStyles();
                widthList = handler.BLL_GetWidths();
                lengthList = handler.BLL_GetLengths();

                rblStyle.DataSource = styleList;
                rblStyle.DataTextField = "Description";
                rblStyle.DataValueField = "StyleID";
                rblStyle.DataBind();

                rblLength.DataSource = lengthList;
                rblLength.DataTextField = "Description";
                rblLength.DataValueField = "LengthID";
                rblLength.DataBind();

                rblWidth.DataSource = widthList;
                rblWidth.DataTextField = "Description";
                rblWidth.DataValueField = "WidthID";
                rblWidth.DataBind();
                divBraidDetails.Visible = true;
            }
            else if(drpType.SelectedValue == "A")
            {
                divBraidDetails.Visible = false;
            }
            else if (drpType.SelectedValue == "N")
            {
                divBraidDetails.Visible = false;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //redirect to previous page
            Response.Redirect("Service.aspx");

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string prodID = "";
            try
            {
                product = new PRODUCT();
            service = new SERVICE();
            bservice = new BRAID_SERVICE();

            string price = String.Format("{0:N2}", txtPrice.Text);
            product.ProductID = function.GenerateRandomProductID();
            product.Name = txtName.Text;
            product.ProductDescription = txtDescription.Text;
            product.Price = Convert.ToDecimal(txtPrice.Text);

            service.NoOfSlots = int.Parse(drpNoOfSlots.SelectedValue);
            service.Type = drpType.SelectedValue.ToString();

            bservice.ProductID = product.ProductID;
            bservice.StyleID = rblStyle.SelectedValue.ToString();
            bservice.LengthID = rblLength.SelectedValue.ToString();
            bservice.WidthID = rblWidth.SelectedValue.ToString();

            if (drpType.SelectedValue == "B")
            {
                handler.BLL_AddBraidService(bservice);
                handler.BLL_AddService(product, service);
            }
            else if (drpType.SelectedValue == "A")
            {
                handler.BLL_AddService(product, service);
            }
            else if (drpType.SelectedValue == "N")
            {
                handler.BLL_AddService(product, service);
            }

                Boolean fileOK = false;
                string path = Server.MapPath("~/Theam/img/");
                if (flUploadServiceimg.HasFile)
                {
                    String fileExtension =
                        System.IO.Path.GetExtension(flUploadServiceimg.FileName).ToLower();
                    String[] allowedExtensions =
                        {".gif", ".png", ".jpeg", ".jpg"};
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (fileExtension == allowedExtensions[i])
                        {
                            fileOK = true;
                        }
                    }
                }

                if (File.Exists(path + product.ProductID + ".jpg"))
                {
                    File.Delete(path + product.ProductID + ".jpg");
                }

                if (fileOK)
                {
                    flUploadServiceimg.PostedFile.SaveAs(path + product.ProductID+".jpg");
                }

                prodID = product.ProductID;
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString());
                Response.Redirect("http://sict-iis.nmmu.ac.za/beauxdebut/error.aspx?Error=An Error Occured Communicating With The Data Base, Try Again Later");
            }
            
            //redirect to previous page
            Response.Redirect("../Cheveux/Service.aspx?ProductID="+prodID);
        }
    }
}