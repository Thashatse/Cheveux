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
            ListItem braid = new ListItem("Braid", "B");
            ListItem application = new ListItem("Application", "A");
            ListItem natural = new ListItem("Natural", "N");
            drpType.Items.Add(braid);
            drpType.Items.Add(application);
            drpType.Items.Add(natural);

            styleList = handler.BLL_GetStyles();
            widthList = handler.BLL_GetWidths();
            lengthList = handler.BLL_GetLengths();
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(drpType.SelectedValue.ToString() == "B")
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
        }
    }
}