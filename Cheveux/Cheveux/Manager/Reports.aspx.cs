using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;
using FusionCharts.Charts;
using System.Text;

namespace Cheveux.Manager
{
    public partial class Reports : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;

        #region Graphs
        StringBuilder jsonData = new StringBuilder();
        StringBuilder data = new StringBuilder();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            btnPrint.Visible = false;

            //check if the user is loged out
            cookie = Request.Cookies["CheveuxUserID"];

            if (cookie == null)
            {
                //if the user is not loged in as a manager do not display Bussines setting
            }
            else if (cookie["UT"] != "M")
            {
                Response.Redirect("../Default.aspx");
            }
            else if (cookie["UT"] == "M")
            {
                //if the user is loged in as a manager display Bussines setting
                drpReport_SelectedIndexChanged1(sender, e);
                LogedIn.Visible = true;
                LogedOut.Visible = false;
            }
        }
        
        #region BTN Functions
        protected void drpReport_SelectedIndexChanged1(object sender, EventArgs e)
        {
            lError.Visible = false;
            if (drpReport.SelectedIndex == 0)
            {
                reportByContainer.Visible = true;
                if (ddlReportFor.SelectedIndex == -1)
                {
                    try
                    {
                        List<SP_GetEmpNames> list = handler.BLL_GetEmpNames();
                        foreach (SP_GetEmpNames emps in list)
                        {
                            //Load employee names into dropdownlist
                            ddlReportFor.DataSource = list;
                            //set the coloumn that will be displayed to the user
                            ddlReportFor.DataTextField = "Name";
                            //set the coloumn that will be used for the valuefield
                            ddlReportFor.DataValueField = "EmployeeID";
                            //bind the data
                            ddlReportFor.DataBind();
                            /*set the default (text (dropdownlist index[0]) that will first be displayed to the user.
                             * in this case the "instruction" to select the employee
                            */
                            ddlReportFor.Items.Insert(0, new ListItem("Select Stylist", "-1"));
                        }
                    }
                    catch (Exception Err)
                    {
                        function.logAnError(Err.ToString());
                        reportLable.Text = ("An error occurred communicating with the database. Please Try Again Later");
                    }
                }

                if (ddlReportFor.SelectedValue != "-1"
                    && CalendarDateStrart.SelectedDate.ToString() == "0001/01/01 00:00:00"
                    && CalendarDateEnd.SelectedDate.ToString() == "0001/01/01 00:00:00")
                {
                    reportByContainer.Visible = true;
                    reportDateRangeContainer.Visible = true;
                    divReport.Visible = true;
                    salesPaymentType.Visible = true;
                    //display the sales report
                    getSalesReport(true);
                }
                else if (ddlReportFor.SelectedValue != "-1"
                    && CalendarDateStrart.SelectedDate.ToString() != "0001/01/01 00:00:00"
                    && CalendarDateEnd.SelectedDate.ToString() != "0001/01/01 00:00:00")
                {
                    reportDateRangeContainer.Visible = true;
                    reportByContainer.Visible = true;
                    divReport.Visible = true;
                    salesPaymentType.Visible = true;
                    //display the sales report
                    getSalesReport(false);
                }

            }
            else if (drpReport.SelectedIndex == 1)
            {
                Response.Redirect("../Receptionist/Appointments.aspx?Action=ViewAllSchedules");
            }
            else if (drpReport.SelectedIndex == 2)
            {
                reportByContainer.Visible = false;
                reportDateRangeContainer.Visible = true;
                if (CalendarDateStrart.SelectedDate.ToString() == "0001/01/01 00:00:00"
                    && CalendarDateEnd.SelectedDate.ToString() == "0001/01/01 00:00:00")
                {

                    divReport.Visible = true;
                    //display the top customer report
                    getTopCustomerReport(true);
                }
                else if (CalendarDateStrart.SelectedDate.ToString() != "0001/01/01 00:00:00"
                    && CalendarDateEnd.SelectedDate.ToString() != "0001/01/01 00:00:00")
                {
                    divReport.Visible = true;
                    //display the top customer report
                    getTopCustomerReport(false);
                }

            }
            else if (drpReport.SelectedIndex == 3)
            {
                reportByContainer.Visible = true;
                if (ddlReportFor.SelectedIndex == -1 || 
                    (ddlReportFor.SelectedItem.Text != "Value" &&
                    ddlReportFor.SelectedItem.Text != "Volume"))
                {
                    ddlReportFor.Items.Clear();
                    ddlReportFor.Items.Add("Value");
                    ddlReportFor.Items.Add("Volume");
                }

                if (ddlReportFor.SelectedValue != "-1"
                    && (ddlReportFor.SelectedItem.Text == "Value" ||
                        ddlReportFor.SelectedItem.Text == "Volume")
                    && CalendarDateStrart.SelectedDate.ToString() == "0001/01/01 00:00:00"
                    && CalendarDateEnd.SelectedDate.ToString() == "0001/01/01 00:00:00")
                {
                    reportByContainer.Visible = true;
                    reportDateRangeContainer.Visible = true;
                    divReport.Visible = true;
                    salesPaymentType.Visible = true;
                    //display the sales report
                    getProductSalesReport(true);
                }
                else if (ddlReportFor.SelectedValue != "-1"
                    && (ddlReportFor.SelectedItem.Text == "Value" ||
                        ddlReportFor.SelectedItem.Text == "Volume")
                    && CalendarDateStrart.SelectedDate.ToString() != "0001/01/01 00:00:00"
                    && CalendarDateEnd.SelectedDate.ToString() != "0001/01/01 00:00:00")
                {
                    reportDateRangeContainer.Visible = true;
                    reportByContainer.Visible = true;
                    divReport.Visible = true;
                    salesPaymentType.Visible = true;
                    //display the sales report
                    getProductSalesReport(false);
                }

            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            drpReport_SelectedIndexChanged1(sender, e);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            divPrintHeader.Visible = true;
            divReport.Visible = true;

            LogedIn.Visible = false;
            LogedOut.Visible = false;

            TableRow newRow = new TableRow();
            newRow.Height = 50;
            tblLogo.Rows.Add(newRow);
            TableCell newCell = new TableCell();
            newCell.Font.Bold = true;
            newCell.Font.Bold = true;
            newCell.Text = "<a class='navbar-brand js-scroll-trigger' href='#' onClick='window.print()'>Cheveux </a>";
            tblLogo.Rows[0].Cells.Add(newCell);

            //print the report
            ClientScript.RegisterStartupScript(typeof(Page), "key", "<script type='text/javascript'>window.print();;</script>");
            divPrintHeader.Visible = true;
        }

        protected void btnGraph_Click(object sender, EventArgs e)
        {
            divPrintHeader.Visible = true;
            divReport.Visible = false;
            
            divGraph.Visible = true;

            LogedIn.Visible = false;
            LogedOut.Visible = false;

            TableRow newRow = new TableRow();
            newRow.Height = 50;
            tblLogo.Rows.Add(newRow);
            TableCell newCell = new TableCell();
            newCell.Font.Bold = true;
            newCell.Font.Bold = true;
            newCell.Text = "<a class='navbar-brand js-scroll-trigger' href='#' onClick='window.print()'>Cheveux </a>";
            tblLogo.Rows[0].Cells.Add(newCell);

            divPrintHeader.Visible = true;
        }
        #endregion

        #region Load Reports
        private void getSalesReport(bool defaultDateRange)
        {
            #region Graph
            var dataValuePair = new List<KeyValuePair<string, double>>();
            #endregion

            //clear the table
            tblReport.Rows.Clear();

            reportLable.Text = "Sales Report";
            reportByLable.Text = "For: "+ ddlReportFor.SelectedItem.Text.ToString();
            reportGenerateDateLable.Text = "Generated: "+DateTime.Now.ToString("HH:mm dd MMM yyyy");
            try
            {
                List<SP_SaleOfHairstylist> report = null;
                if (defaultDateRange == true)
                {
                    reportDateRangeLable.Text = new DateTime(DateTime.Now.Year, 1, 1).ToString("dd MMM yyyy") + " - " +
                        DateTime.Today.ToString("dd MMM yyyy");
                    report = handler.getSaleOfHairstylist(ddlReportFor.SelectedValue, new DateTime(DateTime.Now.Year, 1, 1), DateTime.Today) ;
                }
                else if (defaultDateRange == false)
                {
                    reportDateRangeLable.Text = CalendarDateStrart.SelectedDate.ToString("dd MMM yyyy") + " - " +
                        CalendarDateEnd.SelectedDate.ToString("dd MMM yyyy");
                    report = handler.getSaleOfHairstylist(ddlReportFor.SelectedValue, CalendarDateStrart.SelectedDate, CalendarDateEnd.SelectedDate);
                }

                    //get the invoice dt lines
                    if (report.Count != 0)
                    {
                        //counter to keep track of rows in report
                    int reportRowCount = 0;

                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblReport.Rows.Add(newRow);
                    //set the report headers
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Date";
                    newHeaderCell.Width = 300;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Customer Name";
                    newHeaderCell.Width = 300;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    //empty cell
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 300;
                    newHeaderCell.Text = "Total spent";
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    reportRowCount++;

                   //display each record
                    foreach (SP_SaleOfHairstylist Sales in report)
                     {
                        if (drpPaymentType.SelectedItem.Text == "All"
                            || drpPaymentType.SelectedItem.Text == Sales.PaymentType.ToString().Replace(" ", string.Empty))
                        {
                            // create a new row in the results table and set the height
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblReport.Rows.Add(newRow);
                            //fill the row with the data from the product results object
                            TableCell newCell = new TableCell();
                            newCell.Text = Sales.date.ToString("dd MMM yyy");
                            tblReport.Rows[reportRowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = Sales.FullName.ToString();
                            tblReport.Rows[reportRowCount].Cells.Add(newCell);

                            #region Total
                            //get the invoice 
                            List<SP_getInvoiceDL> invoice = handler.getInvoiceDL(Sales.BookingID);
                            //calculate total price
                            double total = 0.0;

                            foreach (SP_getInvoiceDL item in invoice)
                            {
                                //increment final price
                                total = item.Qty * item.price;
                            }

                            //fill in total
                            newCell = new TableCell();
                            newCell.Text = "R " + string.Format("{0:#.00}", total).ToString();
                            tblReport.Rows[reportRowCount].Cells.Add(newCell);

                            reportRowCount++;
                            //empty row
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblReport.Rows.Add(newRow);
                            reportRowCount++;
                            #endregion

                            #region Graph
                            dataValuePair.Add(new KeyValuePair<string, double>(Sales.date.ToString("dd/MM/yy")+" "+ Sales.FullName.ToString(), total));
                            #endregion
                        }
                    }

                    #region Graph
                    //store chart config name - config value pair
                    Dictionary<string, string> chartConfig = new Dictionary<string, string>();
                    chartConfig.Add("caption", "Sales Report For: " + ddlReportFor.SelectedItem.Text.ToString()+". Date Range: "+ reportDateRangeLable.Text);
                    chartConfig.Add("subCaption", reportGenerateDateLable.Text);
                    chartConfig.Add("xAxisName", "Customer Visit");
                    chartConfig.Add("yAxisName", "Total Spent");
                    chartConfig.Add("numberSuffix", "ZAR");
                    chartConfig.Add("theme", "fusion");

                    // json data to use as chart data source
                    jsonData.Append("{'chart':{");
                    foreach (var config in chartConfig)
                    {
                        jsonData.AppendFormat("'{0}':'{1}',", config.Key, config.Value);
                    }
                    jsonData.Replace(",", "},", jsonData.Length - 1, 1);

                    // build  data object from label-value pair
                    data.Append("'data':[");

                    foreach (KeyValuePair<string, double> pair in dataValuePair)
                    {
                        data.AppendFormat("{{'label':'{0}','value':'{1}'}},", pair.Key, pair.Value);
                    }
                    data.Replace(",", "]", data.Length - 1, 1);

                    jsonData.Append(data.ToString());
                    jsonData.Append("}");
                    //Create chart instance
                    // charttype, chartID, width, height, data format, data

                    Chart MyFirstChart = new Chart("column2d", "first_chart", "800", "550", "json", jsonData.ToString());
                    // render chart
                    Literal1.Text = MyFirstChart.Render();
                    #endregion
                }
                btnPrint.Visible = true;
                btnGraph.Visible = true;
            }
            catch (Exception Err)
            {
                function.logAnError("Error getting Sales Report " + Err.ToString());
                divReport.Visible = false;
                lError.Visible = true;
                lError.Text = "An error occurred generating the report, Try Again Later";
            }
        }

        private void getTopCustomerReport(bool defaultDateRange)
        {
            #region Graph
            var dataValuePair = new List<KeyValuePair<string, double>>();
            #endregion

            //clear the table
            tblReport.Rows.Clear();

            reportLable.Text = "Top Customers";
            reportGenerateDateLable.Text = "Generated: " + DateTime.Now.ToString("HH:mm dd MMM yyyy");
            try
            {

                List<SP_GetTopCustomerbyBooking> report = null;
            
                if (defaultDateRange == true)
                {
                    report = handler.getTopCustomerByBookings(new DateTime(DateTime.Now.Year, 1, 1),
                        DateTime.Today);
                    reportDateRangeLable.Text = new DateTime(DateTime.Now.Year, 1, 1).ToString("dd MMM yyyy") + " - " +
                        DateTime.Today.ToString("dd MMM yyyy");
                   
                }
                else if (defaultDateRange == false)
                {
                    report = handler.getTopCustomerByBookings(CalendarDateStrart.SelectedDate,
                        CalendarDateEnd.SelectedDate);
                    reportDateRangeLable.Text = CalendarDateStrart.SelectedDate.ToString("dd MMM yyyy") + " - " +
                        CalendarDateEnd.SelectedDate.ToString("dd MMM yyyy");
                  
                }

                //get the invoice dt lines
                if (report.Count != 0)
                {
                    //counter to keep track of rows in report
                    int reportRowCount = 0;

                    //display each record
                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblReport.Rows.Add(newRow);
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Customer Name";
                    newHeaderCell.Width = 300;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    //empty cell
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 300;
                    newHeaderCell.Text = "Total Bookings";
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 300;
                    newHeaderCell.Text = "Total Spent";
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    reportRowCount++;


                    foreach (SP_GetTopCustomerbyBooking cust in report)
                    {
                            // create a new row in the results table and set the height
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblReport.Rows.Add(newRow);

                            //fill the row with the data from the product results object
                            TableCell newCell = new TableCell();
                           newCell.Text = cust.CustomerName;
                           tblReport.Rows[reportRowCount].Cells.Add(newCell);

                        newCell = new TableCell();
                        newCell.Text = cust.noOfBookings.ToString();
                        tblReport.Rows[reportRowCount].Cells.Add(newCell);
                        reportRowCount++;

                        #region empty row
                        newRow = new TableRow();
                            newRow.Height = 50;
                            tblReport.Rows.Add(newRow);
                            reportRowCount++;
                        #endregion

                        #region Graph
                        dataValuePair.Add(new KeyValuePair<string, double>(cust.CustomerName, Convert.ToDouble(cust.noOfBookings)));
                        #endregion
                    }

                    #region Graph
                    //store chart config name - config value pair
                    Dictionary<string, string> chartConfig = new Dictionary<string, string>();
                    chartConfig.Add("caption", reportLable.Text);
                    chartConfig.Add("subCaption", reportGenerateDateLable.Text);
                    chartConfig.Add("xAxisName", "Customer");
                    chartConfig.Add("yAxisName", "Visits");
                    chartConfig.Add("theme", "fusion");

                    // json data to use as chart data source
                    jsonData.Append("{'chart':{");
                    foreach (var config in chartConfig)
                    {
                        jsonData.AppendFormat("'{0}':'{1}',", config.Key, config.Value);
                    }
                    jsonData.Replace(",", "},", jsonData.Length - 1, 1);

                    // build  data object from label-value pair
                    data.Append("'data':[");

                    foreach (KeyValuePair<string, double> pair in dataValuePair)
                    {
                        data.AppendFormat("{{'label':'{0}','value':'{1}'}},", pair.Key, pair.Value);
                    }
                    data.Replace(",", "]", data.Length - 1, 1);

                    jsonData.Append(data.ToString());
                    jsonData.Append("}");
                    //Create chart instance
                    // charttype, chartID, width, height, data format, data

                    Chart MyFirstChart = new Chart("column2d", "first_chart", "800", "550", "json", jsonData.ToString());
                    // render chart
                    Literal1.Text = MyFirstChart.Render();
                    #endregion
                }
                btnPrint.Visible = true;
                btnGraph.Visible = true;
            }
            catch (Exception Err)
            {
                function.logAnError("Error getting Sales Report " + Err.ToString());
                divReport.Visible = false;
                lError.Visible = true;
                lError.Text = "An error occurred generating the report, Try Again Later";
            }
        }

        private void getProductSalesReport(bool defaultDateRange)
        {
            #region Graph
            var dataValuePair = new List<KeyValuePair<string, double>>();
            #endregion

            //clear the table
            tblReport.Rows.Clear();
            reportLable.Text = "Sales Report";
            reportByLable.Text = "By: " + ddlReportFor.SelectedItem.Text.ToString() + 
                " For "+drpPaymentType.SelectedItem.Text+" Payment Types";
            reportGenerateDateLable.Text = "Generated: " + DateTime.Now.ToString("HH:mm dd MMM yyyy");

            try
            {
                List<productSalesReport> report = null;

                if (defaultDateRange == true)
                {
                    reportDateRangeLable.Text = new DateTime(DateTime.Now.Year, 1, 1).ToString("dd MMM yyyy") + " - " +
                        DateTime.Today.ToString("dd MMM yyyy");

                    #region get the report from the db
                    if (drpPaymentType.SelectedItem.Text == "All")
                    {
                        if(ddlReportFor.SelectedIndex == 0)
                        {
                            report = handler.getProductSalesValueAll(new DateTime(DateTime.Now.Year, 1, 1),
                                 DateTime.Today);
                        }
                        else if (ddlReportFor.SelectedIndex == 1)
                        {
                            report = handler.getProductSalesVolumeAll(new DateTime(DateTime.Now.Year, 1, 1),
                                 DateTime.Today);
                        }
                    }
                    else if (drpPaymentType.SelectedItem.Text == "Cash")
                    {
                        if (ddlReportFor.SelectedIndex == 0)
                        {
                            report = handler.getProductSalesValueCash(new DateTime(DateTime.Now.Year, 1, 1),
                                 DateTime.Today);
                        }
                        else if (ddlReportFor.SelectedIndex == 1)
                        {
                            report = handler.getProductSalesVolumeCash(new DateTime(DateTime.Now.Year, 1, 1),
                                 DateTime.Today);
                        }
                    }
                    else if (drpPaymentType.SelectedItem.Text == "Credit")
                    {
                        if (ddlReportFor.SelectedIndex == 0)
                        {
                            report = handler.getProductSalesValueCredit(new DateTime(DateTime.Now.Year, 1, 1),
                                 DateTime.Today);
                        }
                        else if (ddlReportFor.SelectedIndex == 1)
                        {
                            report = handler.getProductSalesVolumeCredit(new DateTime(DateTime.Now.Year, 1, 1),
                                 DateTime.Today);
                        }
                    }
                    #endregion
                }
                else if (defaultDateRange == false)
                {
                    reportDateRangeLable.Text = CalendarDateStrart.SelectedDate.ToString("dd MMM yyyy") + " - " +
                        CalendarDateEnd.SelectedDate.ToString("dd MMM yyyy");

                    #region get the report from the db
                    if (drpPaymentType.SelectedItem.Text == "All")
                    {
                        if (ddlReportFor.SelectedIndex == 0)
                        {
                            report = handler.getProductSalesValueAll(CalendarDateStrart.SelectedDate,
                                 CalendarDateEnd.SelectedDate);
                        }
                        else if (ddlReportFor.SelectedIndex == 1)
                        {
                            report = handler.getProductSalesVolumeAll(CalendarDateStrart.SelectedDate,
                                 CalendarDateEnd.SelectedDate);
                        }
                    }
                    else if (drpPaymentType.SelectedItem.Text == "Cash")
                    {
                        if (ddlReportFor.SelectedIndex == 0)
                        {
                            report = handler.getProductSalesValueCash(CalendarDateStrart.SelectedDate,
                                 CalendarDateEnd.SelectedDate);
                        }
                        else if (ddlReportFor.SelectedIndex == 1)
                        {
                            report = handler.getProductSalesVolumeCash(CalendarDateStrart.SelectedDate,
                                 CalendarDateEnd.SelectedDate);
                        }
                    }
                    else if (drpPaymentType.SelectedItem.Text == "Credit")
                    {
                        if (ddlReportFor.SelectedIndex == 0)
                        {
                            report = handler.getProductSalesValueCredit(CalendarDateStrart.SelectedDate,
                                 CalendarDateEnd.SelectedDate);
                        }
                        else if (ddlReportFor.SelectedIndex == 1)
                        {
                            report = handler.getProductSalesVolumeCredit(CalendarDateStrart.SelectedDate,
                                 CalendarDateEnd.SelectedDate);
                        }
                    }
                    #endregion
                }

                //get the invoice dt lines
                if (report.Count != 0)
                {
                    //counter to keep track of rows in report
                    int reportRowCount = 0;

                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblReport.Rows.Add(newRow);

                    #region Header
                    //set the report headers
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Product";
                    newHeaderCell.Width = 300;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    if(ddlReportFor.SelectedIndex == 0)
                    {
                        newHeaderCell.Text = "Value";
                    }
                    else
                    {
                        newHeaderCell.Text = "Volume";
                    }
                    newHeaderCell.Width = 300;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    //empty cell
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 300;
                    if (ddlReportFor.SelectedIndex == 0)
                    {
                        newHeaderCell.Text = "Volume";
                    }
                    else
                    {
                        newHeaderCell.Text = "Value";
                    }
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    reportRowCount++;
                    #endregion

                    //display each record
                    foreach (productSalesReport prod in report)
                    {
                            // create a new row in the results table and set the height
                            newRow = new TableRow();
                            newRow.Height = 50;
                            tblReport.Rows.Add(newRow);
                            //fill the row with the data from the product results object
                            TableCell newCell = new TableCell();
                            newCell.Text = prod.product;
                            tblReport.Rows[reportRowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            if (ddlReportFor.SelectedIndex == 0)
                            {
                            newCell.Text = "R " + string.Format("{0:#.00}", prod.value).ToString();
                            }
                            else
                            {
                            newCell.Text = prod.volume.ToString();
                            }
                            tblReport.Rows[reportRowCount].Cells.Add(newCell);
                            
                            //fill in total
                            newCell = new TableCell();
                            if (ddlReportFor.SelectedIndex == 0)
                            {
                            newCell.Text = prod.volume.ToString();
                        }
                            else
                            {
                            newCell.Text = "R " + string.Format("{0:#.00}", prod.value).ToString();
                        }
                            tblReport.Rows[reportRowCount].Cells.Add(newCell);
                            reportRowCount++;

                        #region Graph
                        if (ddlReportFor.SelectedIndex == 1)
                        {
                            dataValuePair.Add(new KeyValuePair<string, double>(prod.product, prod.volume));
                        }
                        else
                        {
                            dataValuePair.Add(new KeyValuePair<string, double>(prod.product, prod.value));
                        }
                            #endregion
                        }

                    #region Graph
                    //store chart config name - config value pair
                    Dictionary<string, string> chartConfig = new Dictionary<string, string>();
                    chartConfig.Add("caption", "Top Products By " + reportByLable.Text );
                    chartConfig.Add("subCaption", "Date Range: " + reportDateRangeLable.Text);
                    chartConfig.Add("xAxisName", "Customer Visit");
                    if (ddlReportFor.SelectedIndex == 1)
                    {
                        chartConfig.Add("numberSuffix", "Qty");
                        chartConfig.Add("yAxisName", "Volume");
                    }
                    else
                    {
                        chartConfig.Add("yAxisName", "Value");
                        chartConfig.Add("numberSuffix", "ZAR");
                    }
                    chartConfig.Add("theme", "fusion");

                    // json data to use as chart data source
                    jsonData.Append("{'chart':{");
                    foreach (var config in chartConfig)
                    {
                        jsonData.AppendFormat("'{0}':'{1}',", config.Key, config.Value);
                    }
                    jsonData.Replace(",", "},", jsonData.Length - 1, 1);

                    // build  data object from label-value pair
                    data.Append("'data':[");

                    foreach (KeyValuePair<string, double> pair in dataValuePair)
                    {
                        data.AppendFormat("{{'label':'{0}','value':'{1}'}},", pair.Key, pair.Value);
                    }
                    data.Replace(",", "]", data.Length - 1, 1);

                    jsonData.Append(data.ToString());
                    jsonData.Append("}");
                    //Create chart instance
                    // charttype, chartID, width, height, data format, data

                    Chart MyFirstChart = new Chart("column2d", "first_chart", "800", "550", "json", jsonData.ToString());
                    // render chart
                    Literal1.Text = MyFirstChart.Render();
                    #endregion
                }
                btnPrint.Visible = true;
                btnGraph.Visible = true;
            }
            catch (Exception Err)
            {
                function.logAnError("Error getting Product Sales Report " + Err.ToString());
                divReport.Visible = false;
                lError.Visible = true;
                lError.Text = "An error occurred generating the report, Try Again Later";
            }
        }
        #endregion
    }
}
 