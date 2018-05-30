using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;


namespace Cheveux
{
    public partial class Receptionist : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        String test = DateTime.Now.ToString("dd.MM.yyy");
        List<SP_GetEmpNames> list = null;
        List<SP_GetEmpAgenda> agenda = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            theDate.InnerHtml = test;


            list = handler.BLL_GetEmpNames();
            if (!IsPostBack)
            {

                foreach(SP_GetEmpNames emps in list)
                {
                    drpEmpNames.DataSource = list;
                    drpEmpNames.DataTextField = "Name";
                    drpEmpNames.DataValueField = "EmployeeID";
                    drpEmpNames.DataBind();
                    drpEmpNames.Items.Insert(0,new ListItem("Select Employee", "-1"));
                }

            }

        }

        protected void drpEmpNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            agenda = handler.BLL_GetEmpAgenda(drpEmpNames.SelectedValue);
            try
            {
                if (drpEmpNames.SelectedValue != "-1")
                {
                    grdAgenda.DataSource = agenda;
                    grdAgenda.DataBind();

                }
            }
            catch(ApplicationException Err)
            {
               throw new Exception(Err.ToString());
            }
            
        }

        protected void grdAgenda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void CheckIn(Object sender, EventArgs e)
        {
        }

        protected void grdAgenda_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void grdAgenda_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            grdAgenda.PageIndex = e.NewPageIndex;
            grdAgenda.DataBind();
            agenda = handler.BLL_GetEmpAgenda(drpEmpNames.SelectedValue);
            if (drpEmpNames.SelectedValue != "-1")
            {
                grdAgenda.DataSource = agenda;
                grdAgenda.DataBind(); 
            }
        }
    }
}