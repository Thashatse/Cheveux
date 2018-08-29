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
    public partial class Services : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        SP_GetService service = null;
        SP_GetBraidService bservice = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string serviceID = Request.QueryString["ProductID"];
            if (serviceID == null)
            {
                divCustomerView.Visible = true;
            }
            else if(serviceID != null)
            {
                divViewService.Visible = true;
                divCustomerView.Visible = false;
                LoadService(serviceID);
            }
        }
        public void LoadService(string serviceID)
        {
            //BLL_GetServiceFromID & BLL_GetBraidServiceFromeID could not be found
            //service = handler.BLL_GetServiceFromID(serviceID);
            //bservice = handler.BLL_GetBraidServiceFromeID(serviceID);

            lblName.Text = service.ServiceName;
            lblNoOfSlots.Text = Convert.ToString(service.NoOfSlots);
            lblPrice.Text = Convert.ToString(service.Price);
            lblDescription.Text = service.Description;

            if(service.ServiceType == 'B')
            {
                lblStyle.Text = bservice.StyleDesc;
                lblLength.Text = bservice.LengthDesc;
                lblWidth.Text = bservice.WidthDesc;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Manager/UpdateService.aspx");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Manager/Service.aspx");
        }
    }
}