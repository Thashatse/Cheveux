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
        
        protected void drpReport_SelectedIndexChanged1(object sender, EventArgs e)
        {
            lError.Visible = false;
            if (drpReport.SelectedIndex == 0)
            {
                reportByContainer.Visible = true;
                if (ddlReportFor.SelectedIndex == -1
                    || (ddlReportFor.SelectedItem.Text == "Value" ||
                        ddlReportFor.SelectedItem.Text == "Volume"))
                {
                    divReport.Visible = false;
                    divReportHeader.Visible = false;
                    divGraph.Visible = false;
                    try
                    {
                        ddlReportFor.Items.Clear();
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
                    salesPaymentType.Visible = true;
                    //display the sales report
                    getSalesReport(sender, e, true);
                }
                else if (ddlReportFor.SelectedValue != "-1"
                    && CalendarDateStrart.SelectedDate.ToString() != "0001/01/01 00:00:00"
                    && CalendarDateEnd.SelectedDate.ToString() != "0001/01/01 00:00:00")
                {
                    reportDateRangeContainer.Visible = true;
                    reportByContainer.Visible = true;
                    salesPaymentType.Visible = true;
                    //display the sales report
                    getSalesReport(sender, e, false);
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
                    //display the top customer report
                    getTopCustomerReport(sender, e, true);
                }
                else if (CalendarDateStrart.SelectedDate.ToString() != "0001/01/01 00:00:00"
                    && CalendarDateEnd.SelectedDate.ToString() != "0001/01/01 00:00:00")
                {
                    //display the top customer report
                    getTopCustomerReport(sender, e, false);
                }

            }
            else if (drpReport.SelectedIndex == 3)
            {
                reportByContainer.Visible = true;
                if (ddlReportFor.SelectedIndex == -1 || 
                    (ddlReportFor.SelectedItem.Text != "Value" &&
                    ddlReportFor.SelectedItem.Text != "Volume"))
                {
                    salesPaymentType.Visible = false;
                    divReport.Visible = false;
                    divReportHeader.Visible = false;
                    divGraph.Visible = false;
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
                    salesPaymentType.Visible = true;
                    //display the sales report
                    getProductSalesReport(sender, e, true);
                }
                else if (ddlReportFor.SelectedValue != "-1"
                    && (ddlReportFor.SelectedItem.Text == "Value" ||
                        ddlReportFor.SelectedItem.Text == "Volume")
                    && CalendarDateStrart.SelectedDate.ToString() != "0001/01/01 00:00:00"
                    && CalendarDateEnd.SelectedDate.ToString() != "0001/01/01 00:00:00")
                {
                    salesPaymentType.Visible = true;
                    //display the sales report
                    getProductSalesReport(sender, e, false);
                }

            }
            else if (drpReport.SelectedIndex == 4)
            {
                reportByContainer.Visible = true;
                if (ddlReportFor.SelectedIndex == -1 ||
                    (ddlReportFor.SelectedItem.Text != "Value" &&
                    ddlReportFor.SelectedItem.Text != "Count"))
                {
                    salesPaymentType.Visible = false;
                    divReport.Visible = false;
                    divReportHeader.Visible = false;
                    divGraph.Visible = false;
                    ddlReportFor.Items.Clear();
                    ddlReportFor.Items.Add("Value");
                    ddlReportFor.Items.Add("Count");
                }

                if (ddlReportFor.SelectedValue != "-1"
                    && (ddlReportFor.SelectedItem.Text == "Value" ||
                        ddlReportFor.SelectedItem.Text == "Count")
                    && CalendarDateStrart.SelectedDate.ToString() == "0001/01/01 00:00:00"
                    && CalendarDateEnd.SelectedDate.ToString() == "0001/01/01 00:00:00")
                {
                    salesPaymentType.Visible = true;
                    reportDateRangeContainer.Visible = true;
                    //display the sales report
                    getServiceSalesReport(sender, e, true);
                }
                else if (ddlReportFor.SelectedValue != "-1"
                    && (ddlReportFor.SelectedItem.Text == "Value" ||
                        ddlReportFor.SelectedItem.Text == "Count")
                    && CalendarDateStrart.SelectedDate.ToString() != "0001/01/01 00:00:00"
                    && CalendarDateEnd.SelectedDate.ToString() != "0001/01/01 00:00:00")
                {
                    salesPaymentType.Visible = true;
                    reportDateRangeContainer.Visible = true;
                    //display the sales report
                    getServiceSalesReport(sender, e, false);
                }

            }
        }
        
        #region BTN Functions
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            drpReport_SelectedIndexChanged1(sender, e);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            divPrintHeader.Visible = true;
            
            LogedOut.Visible = false;
            btnControlls.Visible = false;
            ReportsPage.Visible = false;

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
        
        protected void btnViewText_Click(object sender, EventArgs e)
        {
            divGraph.Visible = false;
            divReport.Visible = true;
            divGraphType.Visible = false;
            btnViewText.CssClass = "btn btn-primary";
            btnViewGraph.CssClass = "btn btn-light";
        }

        protected void btnViewGraph_Click(object sender, EventArgs e)
        {
            divGraph.Visible = true;
            divReport.Visible = false;
            divGraphType.Visible = true;
            btnViewText.CssClass = "btn btn-light";
            btnViewGraph.CssClass = "btn btn-primary";
        }
        
        protected void btnShowPieGraph_Click(object sender, EventArgs e)
        {
            graphBar.Visible = false;
            graphPie.Visible = true;
            btnShowBarGraph.CssClass = "btn btn-light";
            btnShowPieGraph.CssClass = "btn btn-primary";
        }

        protected void btnShowBarGraph_Click(object sender, EventArgs e)
        {
            graphBar.Visible = true;
            graphPie.Visible = false;
            btnShowBarGraph.CssClass = "btn btn-primary";
            btnShowPieGraph.CssClass= "btn btn-light";
        }
        #endregion

        #region Load Reports
        private void getSalesReport(object sender, EventArgs e, bool defaultDateRange)
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
                    Chart pieChart = new Chart("pie2D", "first_chart", "1700", "750", "json", jsonData.ToString());
                    Chart barChart = new Chart("bar2d", "first_chart", "1700", "750", "json", jsonData.ToString());
                    // render chart
                    graphBar.Text = barChart.Render();
                    graphPie.Text = pieChart.Render();
                    #endregion
                }

                if (divGraph.Visible != true && divReport.Visible != true)
                {
                    btnControlls.Visible = true;
                    divReportHeader.Visible = true;
                    divReport.Visible = true;
                    btnViewText_Click(sender, e);
                }
            }
            catch (Exception Err)
            {
                function.logAnError("Error getting Sales Report " + Err.ToString());
                divReport.Visible = false;
                lError.Visible = true;
                lError.Text = "An error occurred generating the report, Try Again Later";
            }
        }

        private void getTopCustomerReport(object sender, EventArgs e, bool defaultDateRange)
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

                    Chart pieChart = new Chart("pie2D", "first_chart", "1700", "750", "json", jsonData.ToString());
                    Chart barChart = new Chart("column2d", "first_chart", "1700", "750", "json", jsonData.ToString());
                    // render chart
                    graphBar.Text = barChart.Render();
                    graphPie.Text = pieChart.Render();
                    #endregion
                }

                if (divGraph.Visible != true && divReport.Visible != true)
                {
                    btnControlls.Visible = true;
                    divReportHeader.Visible = true;
                    divReport.Visible = true;
                    btnViewText_Click(sender, e);
                }
            }
            catch (Exception Err)
            {
                function.logAnError("Error getting Sales Report " + Err.ToString());
                divReport.Visible = false;
                lError.Visible = true;
                lError.Text = "An error occurred generating the report, Try Again Later";
            }
        }

        private void getProductSalesReport(object sender, EventArgs e, bool defaultDateRange)
        {
            #region Graph
            var dataValuePair = new List<KeyValuePair<string, double>>();
            #endregion

            //clear the table
            tblReport.Rows.Clear();
            reportLable.Text = "Products Sales Report";
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
                    int totalUnitCount = 0;
                    double totalValueCount = 0;

                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblReport.Rows.Add(newRow);

                    #region Header
                    //set the report headers
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Product";
                    newHeaderCell.Width = 1000;
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
                    newHeaderCell.Width = 200;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    //empty cell
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 200;
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

                        totalUnitCount += prod.volume;
                        totalValueCount += prod.value;

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

                    #region Totals
                    newRow = new TableRow();
                    newRow.Height = 50;
                    tblReport.Rows.Add(newRow);
                    //fill the row with the data from the product results object
                    TableCell newCell2 = new TableCell();
                    newCell2.Text = "Total:";
                    newCell2.Font.Bold = true;
                    tblReport.Rows[reportRowCount].Cells.Add(newCell2);

                    newCell2 = new TableCell();
                    if (ddlReportFor.SelectedIndex == 0)
                    {
                        newCell2.Text = "R " + string.Format("{0:#.00}", totalValueCount).ToString();
                    }
                    else
                    {
                        newCell2.Text = totalUnitCount.ToString();
                    }
                    newCell2.Font.Bold = true;
                    tblReport.Rows[reportRowCount].Cells.Add(newCell2);

                    //fill in total
                    newCell2 = new TableCell();
                    if (ddlReportFor.SelectedIndex == 0)
                    {
                        newCell2.Text = totalUnitCount.ToString();
                    }
                    else
                    {
                        newCell2.Text = "R " + string.Format("{0:#.00}", totalValueCount).ToString();
                    }
                    newCell2.Font.Bold = true;
                    tblReport.Rows[reportRowCount].Cells.Add(newCell2);
                    reportRowCount++;
                    #endregion

                    #region Graph
                    //store chart config name - config value pair
                    Dictionary<string, string> chartConfig = new Dictionary<string, string>();
                    chartConfig.Add("xAxisName", "Product");
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

                    Chart pieChart = new Chart("pie2D", "first_chart", "1700", "750", "json", jsonData.ToString());
                    Chart barChart = new Chart("column2d", "first_chart", "1700", "750", "json", jsonData.ToString());
                    // render chart
                    graphBar.Text = barChart.Render();
                    graphPie.Text = pieChart.Render();
                    #endregion
                }

                if (divGraph.Visible != true && divReport.Visible != true)
                {
                    btnControlls.Visible = true;
                    divReportHeader.Visible = true;
                    divReport.Visible = true;
                    btnViewText_Click(sender, e);
                }
            }
            catch (Exception Err)
            {
                function.logAnError("Error getting Product Sales Report " + Err.ToString());
                divReport.Visible = false;
                divReportHeader.Visible = false;
                lError.Visible = true;
                lError.Text = "An error occurred generating the report, Try Again Later";
            }
        }

        private void getServiceSalesReport(object sender, EventArgs e, bool defaultDateRange)
        {
            #region Graph
            var dataValuePair = new List<KeyValuePair<string, double>>();
            #endregion

            //clear the table
            tblReport.Rows.Clear();
            reportLable.Text = "Service Sales Report";
            reportByLable.Text = "By: " + ddlReportFor.SelectedItem.Text.ToString() +
                " For " + drpPaymentType.SelectedItem.Text + " Payment Types";
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
                        if (ddlReportFor.SelectedIndex == 0)
                        {
                            report = handler.getServiceSalesValueAll(new DateTime(DateTime.Now.Year, 1, 1),
                                 DateTime.Today);
                        }
                        else if (ddlReportFor.SelectedIndex == 1)
                        {
                            report = handler.getServiceSalesVolumeAll(new DateTime(DateTime.Now.Year, 1, 1),
                                 DateTime.Today);
                        }
                    }
                    else if (drpPaymentType.SelectedItem.Text == "Cash")
                    {
                        if (ddlReportFor.SelectedIndex == 0)
                        {
                            report = handler.getServiceSalesValueCash(new DateTime(DateTime.Now.Year, 1, 1),
                                 DateTime.Today);
                        }
                        else if (ddlReportFor.SelectedIndex == 1)
                        {
                            report = handler.getServiceSalesVolumeCash(new DateTime(DateTime.Now.Year, 1, 1),
                                 DateTime.Today);
                        }
                    }
                    else if (drpPaymentType.SelectedItem.Text == "Credit")
                    {
                        if (ddlReportFor.SelectedIndex == 0)
                        {
                            report = handler.getServiceSalesValueCredit(new DateTime(DateTime.Now.Year, 1, 1),
                                 DateTime.Today);
                        }
                        else if (ddlReportFor.SelectedIndex == 1)
                        {
                            report = handler.getServiceSalesVolumeCredit(new DateTime(DateTime.Now.Year, 1, 1),
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
                            report = handler.getServiceSalesValueAll(CalendarDateStrart.SelectedDate,
                                 CalendarDateEnd.SelectedDate);
                        }
                        else if (ddlReportFor.SelectedIndex == 1)
                        {
                            report = handler.getServiceSalesVolumeAll(CalendarDateStrart.SelectedDate,
                                 CalendarDateEnd.SelectedDate);
                        }
                    }
                    else if (drpPaymentType.SelectedItem.Text == "Cash")
                    {
                        if (ddlReportFor.SelectedIndex == 0)
                        {
                            report = handler.getServiceSalesValueCash(CalendarDateStrart.SelectedDate,
                                 CalendarDateEnd.SelectedDate);
                        }
                        else if (ddlReportFor.SelectedIndex == 1)
                        {
                            report = handler.getServiceSalesVolumeCash(CalendarDateStrart.SelectedDate,
                                 CalendarDateEnd.SelectedDate);
                        }
                    }
                    else if (drpPaymentType.SelectedItem.Text == "Credit")
                    {
                        if (ddlReportFor.SelectedIndex == 0)
                        {
                            report = handler.getServiceSalesValueCredit(CalendarDateStrart.SelectedDate,
                                 CalendarDateEnd.SelectedDate);
                        }
                        else if (ddlReportFor.SelectedIndex == 1)
                        {
                            report = handler.getServiceSalesVolumeCredit(CalendarDateStrart.SelectedDate,
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
                    int totalUnitCount = 0;
                    double totalValueCount = 0;

                    TableRow newRow = new TableRow();
                    newRow.Height = 50;
                    tblReport.Rows.Add(newRow);

                    #region Header
                    //set the report headers
                    TableHeaderCell newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Text = "Service";
                    newHeaderCell.Width = 1000;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    newHeaderCell = new TableHeaderCell();
                    if (ddlReportFor.SelectedIndex == 0)
                    {
                        newHeaderCell.Text = "Value";
                    }
                    else
                    {
                        newHeaderCell.Text = "Count";
                    }
                    newHeaderCell.Width = 200;
                    tblReport.Rows[reportRowCount].Cells.Add(newHeaderCell);
                    //empty cell
                    newHeaderCell = new TableHeaderCell();
                    newHeaderCell.Width = 200;
                    if (ddlReportFor.SelectedIndex == 0)
                    {
                        newHeaderCell.Text = "Count";
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

                        totalUnitCount += prod.volume;
                        totalValueCount += prod.value;

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

                    #region Totals
                    newRow = new TableRow();
                    newRow.Height = 50;
                    tblReport.Rows.Add(newRow);
                    //fill the row with the data from the product results object
                    TableCell newCell2 = new TableCell();
                    newCell2.Text = "Total:";
                    newCell2.Font.Bold = true;
                    tblReport.Rows[reportRowCount].Cells.Add(newCell2);

                    newCell2 = new TableCell();
                    if (ddlReportFor.SelectedIndex == 0)
                    {
                        newCell2.Text = "R " + string.Format("{0:#.00}", totalValueCount).ToString();
                    }
                    else
                    {
                        newCell2.Text = totalUnitCount.ToString();
                    }
                    newCell2.Font.Bold = true;
                    tblReport.Rows[reportRowCount].Cells.Add(newCell2);

                    //fill in total
                    newCell2 = new TableCell();
                    if (ddlReportFor.SelectedIndex == 0)
                    {
                        newCell2.Text = totalUnitCount.ToString();
                    }
                    else
                    {
                        newCell2.Text = "R " + string.Format("{0:#.00}", totalValueCount).ToString();
                    }
                    newCell2.Font.Bold = true;
                    tblReport.Rows[reportRowCount].Cells.Add(newCell2);
                    reportRowCount++;
                    #endregion

                    #region Graph
                    //store chart config name - config value pair
                    Dictionary<string, string> chartConfig = new Dictionary<string, string>();
                    chartConfig.Add("xAxisName", "Product");
                    if (ddlReportFor.SelectedIndex == 1)
                    {
                        chartConfig.Add("numberSuffix", "No.");
                        chartConfig.Add("yAxisName", "Count");
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

                    Chart pieChart = new Chart("pie2D", "first_chart", "1700", "750", "json", jsonData.ToString());
                    Chart barChart = new Chart("column2d", "first_chart", "1700", "750", "json", jsonData.ToString());
                    // render chart
                    graphBar.Text = barChart.Render();
                    graphPie.Text = pieChart.Render();
                    #endregion
                }

                if (divGraph.Visible != true && divReport.Visible != true)
                {
                    btnControlls.Visible = true;
                    divReportHeader.Visible = true;
                    divReport.Visible = true;
                    btnViewText_Click(sender, e);
                }
            }
            catch (Exception Err)
            {
                function.logAnError("Error getting Service Sales Report " + Err.ToString());
                divReport.Visible = false;
                divReportHeader.Visible = false;
                lError.Visible = true;
                lError.Text = "An error occurred generating the report, Try Again Later";
            }
        }
        #endregion
    }
}
 