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
    public partial class UpdateService : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        SP_GetService service = null;
        SP_GetBraidService bservice = null;
        PRODUCT product = null;
        SERVICE services = null;
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
            string serviceID = Request.QueryString["ServiceID"];
            service = handler.BLL_GetServiceFromID(serviceID);
            bservice = handler.BLL_GetBraidServiceFromID(serviceID);
            if (!Page.IsPostBack)
            {
                if (service.ServiceType == 'B')
                {
                    divBraidDetails.Visible = true;
                    lblStyle.Text = bservice.StyleDesc;
                    lblLength.Text = bservice.LengthDesc;
                    lblWidth.Text = bservice.WidthDesc;
                }
                lblName.Text = service.ServiceName;
                drpNoOfSlots.SelectedItem.Text = Convert.ToString(service.NoOfSlots);
                txtPrice.Text = string.Format("{0:#.00}", service.Price).ToString();
                lblDescription.Text = service.Description;
            }

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string serviceID = Request.QueryString["ServiceID"];
            product = new PRODUCT();
            services = new SERVICE();

            product.ProductID = serviceID;
            product.Price = Convert.ToDecimal(txtPrice.Text);
            services.NoOfSlots = Convert.ToInt32(drpNoOfSlots.SelectedItem.Text);

            handler.updateService(product, services);
            Response.Redirect("../Cheveux/Services.aspx?ProductID="+serviceID);

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Manager/Service.aspx");
        }
    }
}