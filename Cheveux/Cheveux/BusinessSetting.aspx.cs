using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;
using BLL;

namespace Cheveux
{
    public partial class BusinessSetting : System.Web.UI.Page
    {

        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;
        BUSINESS BusinessDetails = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is loged out
            cookie = Request.Cookies["CheveuxUserID"];

            if (cookie == null || cookie["UT"] != "M")
            {
                //if the user is not loged in as a manager do not display Bussines setting
            }
            else if (cookie["UT"] == "M")
            {
                //if the user is loged in as a manager display Bussines setting
                LogedIn.Visible = true;
                LogedOut.Visible = false;

                try
                {
                    //get the curent bussines details
                    BusinessDetails = handler.getBusinessTable();

                    //fill the table with the current setings
                    fillTable();
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n getting business data from the db Page_Load on bussines settings");
                    Response.Redirect("Error.aspx?Error='An error occurred when communicating with the Cheveux server'");
                }
            }
        }

        public void fillTable()
        {
            try
            {
                //vat rate
                tblBussinesSettings.Rows[0].Cells[1].Text = BusinessDetails.Vat + "%";
                //vat Reg No
                tblBussinesSettings.Rows[1].Cells[1].Text = BusinessDetails.VatRegNo;
                //address
                //address line 1
                tblBussinesSettings.Rows[2].Cells[1].Text = BusinessDetails.AddressLine1;
                //ddress line 2
                tblBussinesSettings.Rows[3].Cells[1].Text = BusinessDetails.AddressLine2;
                //Phone Number
                tblBussinesSettings.Rows[4].Cells[1].Text = BusinessDetails.Phone;
                //Weekend Hours
                tblBussinesSettings.Rows[5].Cells[1].Text = BusinessDetails.WeekdayStart.ToString("HH:mm")
                +" - "+BusinessDetails.WeekdayEnd.ToString("HH:mm");
                //Weekend Hours
                tblBussinesSettings.Rows[6].Cells[1].Text = BusinessDetails.WeekendStart.ToString("HH:mm")
                + " - " + BusinessDetails.WeekendEnd.ToString("HH:mm");
                //public holiday Hours
                tblBussinesSettings.Rows[7].Cells[1].Text = BusinessDetails.PublicHolStart.ToString("HH:mm")
                + " - " + BusinessDetails.PublicHolEnd.ToString("HH:mm");
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString() + "\n filling the tblBussinesSettings on the business setings page");
                Response.Redirect("Error.aspx?Error='An error displaying the page'");
            }
        }
    }
}