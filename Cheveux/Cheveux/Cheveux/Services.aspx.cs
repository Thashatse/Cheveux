using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;
using System.Text;
using FusionCharts.Charts;

namespace Cheveux.Cheveux
{
    public partial class Services : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        SP_GetService service = null;
        SP_GetBraidService bservice = null;
        List<SP_GetServices> allservices = null;
        HttpCookie cookie = null;

        protected void Page_PreInit(Object sender, EventArgs e)
        {
            //check the cheveux user id cookie for the user
            HttpCookie cookie = Request.Cookies["CheveuxUserID"];
            char userType;
            //check if the cookie is empty or not
            if (cookie != null)
            {
                //store the user Type in a variable and display the appropriate master page for the user
                userType = cookie["UT"].ToString()[0];
                //if customer
                if (userType == 'C')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/Cheveux.Master";
                }
                //if receptionist
                else if (userType == 'R')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/CheveuxReceptionist.Master";
                }
                //if stylist
                else if (userType == 'S')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/CheveuxStylist.Master";
                }
                //if Manager
                else if (userType == 'M')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/CheveuxManager.Master";
                }
                //default
                else
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/Cheveux.Master";
                }
            }
            //set the default master page if the cookie is empty
            else
            {
                //set the master page
                this.MasterPageFile = "~/MasterPages/Cheveux.Master";
            }
        }

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
                lblViewService.Text = service.ServiceName;
                lblNoOfSlots.Text = Convert.ToString(service.NoOfSlots*30)+" Mins";
                lblPrice.Text = "R " + string.Format("{0:#.00}", service.Price).ToString();
                lblDescription.Text = service.Description;
                phProductImage.Controls.Add(new LiteralControl
                    ("<img width='400' height='400' src='http://sict-iis.nmmu.ac.za/beauxdebut/Theam/img/portfolio/thumbnails/fullsize/"+serviceID+".jpg'/>"));
                //check if the user is logged in
                cookie = Request.Cookies["CheveuxUserID"];
                if (cookie != null)
                {
                    if (cookie["UT"] == "M")
                    {
                        btnCancel.Visible = true;
                        btnUpdate.Visible = true;
                    }
                }


                #region Gauge
                        #region Value
                if (cookie != null)
                {
                    if (cookie["UT"] == "M" || cookie["UT"] == "R")
                    {
                        List<productSalesReport> salesGaugeData = handler.getSalesGauge(serviceID);
                        if (salesGaugeData.Count == 2)
                        {
                            int lowStockTier = (Convert.ToInt16((salesGaugeData[1].value / 2) / 2));
                            int middelTier = Convert.ToInt16((salesGaugeData[1].value / 2));
                            int UpperlTier = Convert.ToInt16(salesGaugeData[1].value);

                            Dictionary<string, string> chartConfig = new Dictionary<string, string>();
                            chartConfig.Add("caption", "Gross (Past 30 Days)");
                            chartConfig.Add("subCaption", "Compared to best grossing service");
                            chartConfig.Add("lowerLimit", "0");
                            chartConfig.Add("upperLimit", UpperlTier.ToString());
                            chartConfig.Add("showValue", "1");
                            chartConfig.Add("numberSuffix", "ZAR");
                            chartConfig.Add("theme", "fusion");
                            chartConfig.Add("showToolTip", "0");

                            List<ColorRange> color = new List<ColorRange>();
                            color.Add(new ColorRange(0, lowStockTier, "#F2726F"));
                            color.Add(new ColorRange(lowStockTier, middelTier, "#FFC533"));
                            color.Add(new ColorRange(middelTier, UpperlTier, "#62B58F"));

                            //store dial configuration

                            List<KeyValuePair<string, string>> dial = new List<KeyValuePair<string, string>>();
                            dial.Add(new KeyValuePair<string, string>("value", salesGaugeData[0].value.ToString()));

                            // json data to use as chart data source
                            StringBuilder jsonData = new StringBuilder();
                            //build chart config object
                            jsonData.Append("{'chart':{");
                            foreach (var config in chartConfig)
                            {
                                jsonData.AppendFormat("'{0}':'{1}',", config.Key, config.Value);
                            }
                            jsonData.Replace(",", "},", jsonData.Length - 1, 1);

                            StringBuilder range = new StringBuilder();
                            //build colorRange object
                            range.Append("'colorRange':{");
                            range.Append("'color':[");
                            foreach (ColorRange clr in color)
                            {
                                range.AppendFormat("{{'minValue':'{0}','maxValue':'{1}','code':'{2}'}},", clr.Min, clr.Max, clr.ColorCode);
                            }
                            range.Replace(",", "]},", range.Length - 1, 1);
                            //build dials object
                            StringBuilder dials = new StringBuilder();
                            dials.Append("'dials':{");
                            dials.Append("'dial':[");
                            foreach (var dialCnf in dial)
                            {
                                dials.AppendFormat("{{'{0}':'{1}'}},", dialCnf.Key, dialCnf.Value);
                            }
                            dials.Replace(",", "]}", dials.Length - 1, 1);

                            jsonData.Append(range.ToString());
                            jsonData.Append(dials.ToString());
                            jsonData.Append("}");

                            //Create gauge instance
                            // charttype, chartID, width, height, data format, data

                            Chart My2ndGauge = new Chart("angulargauge", "first_gauge", "400", "250", "json", jsonData.ToString());
                            //render gauge
                            Literal1.Text = My2ndGauge.Render();
                        }
                    }
                }
                #endregion
                        #endregion
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
                tblServicesTable.Rows.Clear();
                //load a list of all products
                allservices = handler.BLL_GetAllServices();
                allservices = allservices.OrderBy(o => o.Name).ToList();
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
                    //create a header row and set cell withs
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Name: ";
                    newHeaderCell.Width = 300;
                    tblServicesTable.Rows[count].Cells.Add(newHeaderCell);
                    //create a header row and set cell withs
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Description: ";
                    newHeaderCell.Width = 500;
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
                        if ((function.compareToSearchTerm(serv.Name, txtProductSearchTerm.Text)== true
                            || function.compareToSearchTerm(serv.Description, txtProductSearchTerm.Text) == true))
                        {
                            //diplay the product details
                            //add a new row to the table
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblServicesTable.Rows.Add(newRow);

                            //Name
                            TableCell newCell = new TableCell();
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

        #region Gauge
        class ColorRange
        {
            public int Min
            {
                get;
                set;
            }
            public int Max
            {
                get;
                set;
            }
            public string ColorCode
            {
                get;
                set;
            }

            public ColorRange(int min, int max, string code)
            {
                Min = min;
                Max = max;
                ColorCode = code;
            }
        }
        #endregion
    }
}