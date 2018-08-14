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
                ListItem braid = new ListItem("Braid", "1");
                ListItem application = new ListItem("Application", "2");
                ListItem natural = new ListItem("Natural", "3");
                drpType.Items.Add(select);
                drpType.Items.Add(braid);
                drpType.Items.Add(application);
                drpType.Items.Add(natural);
            }
           

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
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(drpType.SelectedValue == "0")
            {
                divBraidDetails.Visible = false;
            }
            else if(drpType.SelectedValue == "1")
            {
                divBraidDetails.Visible = true;
            }
            else if(drpType.SelectedValue == "2")
            {
                divBraidDetails.Visible = false;
            }
            else if (drpType.SelectedValue == "3")
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

            //product.ProductID =;
            product.Name = txtName.Text;
            product.ProductDescription = txtDescription.Text;
            product.Price = Convert.ToDecimal(txtPrice.Text);
            product.ProductType = "S";

            service.NoOfSlots = int.Parse(txtNoOfSlots.Text);
            service.Type = drpType.SelectedValue.ToString();

            bservice.StyleID = rblStyle.SelectedValue.ToString();
            bservice.LengthID = rblLength.SelectedValue.ToString();
            bservice.WidthID = rblWidth.SelectedValue.ToString();

            handler.BLL_AddService(product, service, bservice);
        }
    }
}