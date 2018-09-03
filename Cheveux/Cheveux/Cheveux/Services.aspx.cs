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
        List<SP_GetServices> allservices = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string serviceID = Request.QueryString["ProductID"];
            if (serviceID == null)
            {
                divCustomerView.Visible = true;
                LoadAllServices();
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
            service = handler.BLL_GetServiceFromID(serviceID);
            bservice = handler.BLL_GetBraidServiceFromID(serviceID);
            if (service != null)
            {

                if (service.ServiceType == 'B')
                {
                    divBraidDetails.Visible = true;

                    lblStyle.Text = bservice.StyleDesc;
                    lblLength.Text = bservice.LengthDesc;
                    lblWidth.Text = bservice.WidthDesc;
                }
                lblName.Text = service.ServiceName;
                lblNoOfSlots.Text = Convert.ToString(service.NoOfSlots);
                lblPrice.Text = "R " + string.Format("{0:#.00}", service.Price).ToString();
                lblDescription.Text = service.Description;

            }
            else
            {
                Response.Redirect("Services.aspx");
            }

        }
        public void LoadAllServices()
        {
            try
            {
                //load a list of all products
                allservices = handler.BLL_GetAllServices();
                //track row count & number of products cound
                int count = 0;

                if (allservices.Count > 0)
                {
                    //disply the table headers
                    //create a new row in the table and set the height
                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblServicesTable.Rows.Add(newRow);
                    //create a header row and set cell withs
                    //Product image row
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 100;
                    tblServicesTable.Rows[count].Cells.Add(newHeaderCell);
                    //create a header row and set cell withs
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Name: ";
                    newHeaderCell.Width = 300;
                    tblServicesTable.Rows[count].Cells.Add(newHeaderCell);
                    //create a header row and set cell withs
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Description: ";
                    newHeaderCell.Width = 500;
                    tblServicesTable.Rows[count].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 100;
                    tblServicesTable.Rows[count].Cells.Add(newHeaderCell);
                    //create a header row and set cell withs
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Price: ";
                    newHeaderCell.Width = 300;
                    tblServicesTable.Rows[count].Cells.Add(newHeaderCell);
                    //increment rowcounter
                    count++;

                    foreach (SP_GetServices serv in allservices)
                    {
                        //if the product maches the selected type
                        //if product matches the tearm
                        if (function.compareToSearchTerm(txtProductSearchTerm.Text,serv.Name) || function.compareToSearchTerm(txtProductSearchTerm.Text, serv.Description))
                        {
                            //diplay the product details
                            //add a new row to the table
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblServicesTable.Rows.Add(newRow);

                            //image
                            TableCell newCell = new TableCell();
                            //image display to be added here
                            tblServicesTable.Rows[count].Cells.Add(newCell);

                            //Edit service link to be added by Sivu
                            //view service link to be added by Lachea

                            //Name
                            newCell = new TableCell();
                            newCell.Text = "<a class='btn btn-default' href ='../cheveux/services.aspx?ProductID="
                                        + serv.ServiceID.ToString().Replace(" ", string.Empty) + "'>" + serv.Name + "</a>";
                            tblServicesTable.Rows[count].Cells.Add(newCell);

                            //Description
                            newCell = new TableCell();
                            newCell.Text = serv.Description;
                            tblServicesTable.Rows[count].Cells.Add(newCell);


                            //Price
                            newCell = new TableCell();
                            newCell.Text = "R" + string.Format("{0:#.00}", serv.Price);
                            tblServicesTable.Rows[count].Cells.Add(newCell);
                            newCell.HorizontalAlign = HorizontalAlign.Right;


                            if (serv.Active == 'Y')
                            {
                                newCell = new TableCell();
                                tblServicesTable.Rows[count].Cells.Add(newCell);
                            }
                            else
                            {
                                newCell = new TableCell();
                                newCell.Text = "Inactive Service";
                                tblServicesTable.Rows[count].Cells.Add(newCell);
                            }

                            //increment counter
                            count++;
                        }
                        else
                        {
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblServicesTable.Rows.Add(newRow);

                            //image
                            TableCell newCell = new TableCell();
                            //image display to be added here
                            tblServicesTable.Rows[count].Cells.Add(newCell);

                            //Edit service link to be added by Sivu
                            //view service link to be added by Lachea

                            //Name
                            newCell = new TableCell();
                            newCell.Text = "<a class='btn btn-default' href ='../cheveux/services.aspx?ProductID="
                                        + serv.ServiceID.ToString().Replace(" ", string.Empty) + "'>" + serv.Name + "</a>";
                            tblServicesTable.Rows[count].Cells.Add(newCell);

                            //Description
                            newCell = new TableCell();
                            newCell.Text = serv.Description;
                            tblServicesTable.Rows[count].Cells.Add(newCell);

                            //Price
                            newCell = new TableCell();
                            newCell.Text = "R" + string.Format("{0:#.00}", serv.Price);
                            newCell.HorizontalAlign = HorizontalAlign.Right;
                            tblServicesTable.Rows[count].Cells.Add(newCell);


                            if (serv.Active == 'Y')
                            {
                                newCell = new TableCell();
                                tblServicesTable.Rows[count].Cells.Add(newCell);
                            }
                            else
                            {
                                newCell = new TableCell();
                                newCell.Text = "Inactive Service";
                                tblServicesTable.Rows[count].Cells.Add(newCell);
                            }

                            //increment counter
                            count++;
                        }
                    }
                }

                //result count
                lblErrorSummary.Text = count - 1 + " Services";
                if (count - 1 == 0)
                {
                     lblErrorSummary.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                     lblErrorSummary.ForeColor = System.Drawing.Color.Black;
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving list of Services with term " + txtProductSearchTerm.Text);
                 lblErrorSummary.Font.Size = 22;
                 lblErrorSummary.Font.Bold = true;
                 lblErrorSummary.Text = "An error occurred retrieving searched service details";
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