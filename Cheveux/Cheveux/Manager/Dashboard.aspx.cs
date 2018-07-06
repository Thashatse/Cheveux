using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;

namespace Cheveux.Manager
{
    public partial class Manager : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;
        Tuple<List<SP_GetAllAccessories>, List<SP_GetAllTreatments>> products = null;
        int alertCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is loged out
            cookie = Request.Cookies["CheveuxUserID"];

            if (cookie == null)
            {
                //if the user is not loged in as a manager do not display Bussines setting
            }else if(cookie["UT"] != "M")
            {
                Response.Redirect("../Default.aspx");
            }
            else if (cookie["UT"] == "M")
            {
                //if the user is loged in as a manager display the dashboard
                LogedIn.Visible = true;
                LogedOut.Visible = false;
                //display the headings
                string wB = Request.QueryString["WB"];
                if (wB == "True")
                {
                    Welcome.Text = "Welcome Back " + handler.GetUserDetails(cookie["ID"]).FirstName;
                }
                lJumbotronDate.Text = DateTime.Today.ToString("D");
                //check for any alerts
                //low stock alert
                checkForLowStock();
            }  
        }

        private void checkForLowStock()
        {
            //load a list of all products
            products = handler.getAllProductsAndDetails();
            //sort the products by stock count
            products = Tuple.Create(products.Item1.OrderBy(o => o.Qty).ToList(), 
                products.Item2.OrderBy(o => o.Qty).ToList());;

            //check for out of stock products
            //check out of stock treatments
            foreach (SP_GetAllTreatments treat in products.Item2)
            {
                if (treat.Qty <= 0)
                {
                    //if the accessory is low and stock add an alert to the alert table
                    addAlertToTable("&#10071;", "Out Of Stock",
                        "<a href = '#?ProductID="
                        + treat.ProductID.ToString().Replace(" ", string.Empty) +
                        "&PreviousPage=../Manager/Dashboard.aspx'>" +
                        "The Treatment '" + treat.Name + "' is currently out off stock</a>");
                }
            }
            //check out of stock accessories
            foreach (SP_GetAllAccessories Access in products.Item1)
            {
                if (Access.Qty <= 0)
                {
                    //if the accessory is low and stock add an alert to the alert table
                    addAlertToTable("&#10071;", "Out Of Stock",
                        "<a href = '#?ProductID="
                        + Access.ProductID.ToString().Replace(" ", string.Empty) +
                        "&PreviousPage=../Manager/Dashboard.aspx'>" +
                        "The Accessory '" + Access.Name + "' is currently out of stock</a>");
                }
            }
            //check for low stock
            //check low stock treatments
            foreach (SP_GetAllTreatments treat in products.Item2)
            {
                if (treat.Qty < 10 && treat.Qty > 0)
                {
                    //if the accessory is low and stock add an alert to the alert table
                    addAlertToTable("&#9888;", "Low Stock",
                        " <a href = '#?ProductID="
                        + treat.ProductID.ToString().Replace(" ", string.Empty) +
                        "&PreviousPage=../Manager/Dashboard.aspx'>" +
                        "The Treatment '" + treat.Name + "' is currently runing low on stock with "
                        + treat.Qty + " Left in stock </a>");
                }
            }
            //check low stock accessories
            foreach (SP_GetAllAccessories Access in products.Item1)
            {
                if (Access.Qty < 10 && Access.Qty > 0)
                {
                    //if the accessory is low and stock add an alert to the alert table
                    addAlertToTable("&#9888;", "Low Stock",
                        "<a href = '#?ProductID="
                        + Access.ProductID.ToString().Replace(" ", string.Empty) +
                        "&PreviousPage=../Manager/Dashboard.aspx'>" +
                        "The Accessory '" + Access.Name + "' is currently runing low on stock with "
                        + Access.Qty + " Left in stock</a>");
                }
            }
        }

        private void addAlertToTable(string alertIcon, string alertType, string alertDescription)
        {
            alertsContainer.Visible = true;
            if (alertType != null
                && alertDescription != null)
            {
                TableRow newRow;
                //disply the table headers
                if (alertCount == 0)
                {
                    //create a new row in the table and set the height
                    newRow = new TableRow();
                    newRow.Height = 50;
                    tblAlerts.Rows.Add(newRow);
                    //create a header row and set cell withs
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 50;
                    tblAlerts.Rows[alertCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Type: ";
                    newHeaderCell.Width = 200;
                    tblAlerts.Rows[alertCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Description: ";
                    newHeaderCell.Width = 750;
                    tblAlerts.Rows[alertCount].Cells.Add(newHeaderCell);

                    //increment rowcounter
                    alertCount++;
                }

                //add the alert to the table
                //create a new row in the table and set the height
                newRow = new TableRow();
                newRow.Height = 50;
                tblAlerts.Rows.Add(newRow);
                //add the alert to the table
                TableCell newCell = new TableCell();
                newCell.Text = alertIcon;
                tblAlerts.Rows[alertCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = alertType;
                tblAlerts.Rows[alertCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = alertDescription;
                tblAlerts.Rows[alertCount].Cells.Add(newCell);

                //increment rowcounter
                alertCount++;
            }
        }
    }
}