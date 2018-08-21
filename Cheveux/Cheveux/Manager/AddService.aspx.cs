using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;

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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ListItem select = new ListItem(" --Select Type-- ", "0");
                ListItem braid = new ListItem("Braid", "B");
                ListItem application = new ListItem("Application", "A");
                ListItem natural = new ListItem("Natural", "N");
                drpType.Items.Add(select);
                drpType.Items.Add(braid);
                drpType.Items.Add(application);
                drpType.Items.Add(natural);
            }
           

            
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(drpType.SelectedValue == "0")
            {
                divBraidDetails.Visible = false;
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
            product = new PRODUCT();
            service = new SERVICE();
            bservice = new BRAID_SERVICE();

            product.ProductID = function.GenerateRandomProductID();
            product.Name = txtName.Text;
            product.ProductDescription = txtDescription.Text;
            product.Price = Convert.ToDecimal(txtPrice.Text);
            product.ProductType = "S";

            service.NoOfSlots = int.Parse(txtNoOfSlots.Text);
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
            else if(drpType.SelectedValue == "A")
            {
                handler.BLL_AddService(product, service);
            }
            else if (drpType.SelectedValue == "N")
            {
                handler.BLL_AddService(product, service);
            }


        }
    }
}