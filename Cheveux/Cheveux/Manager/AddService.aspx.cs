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
            ListItem select = new ListItem(" --Select Type-- ", "");
            select.Selected = true;
            ListItem braid = new ListItem("Braid", "B");
            ListItem application = new ListItem("Application", "A");
            ListItem natural = new ListItem("Natural", "N");
            drpType.Items.Add(select);
            drpType.Items.Add(braid);
            drpType.Items.Add(application);
            drpType.Items.Add(natural);

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
            if(drpType.SelectedValue.ToString() == "Braid")
            {
                divBraidDetails.Visible = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

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