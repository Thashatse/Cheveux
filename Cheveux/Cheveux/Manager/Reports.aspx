<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Cheveux.Manager.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Reports - Cheveux
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src=" http://cdn.fusioncharts.com/fusioncharts/latest/fusioncharts.js"></script>
    <script type="text/javascript" src=" http://cdn.fusioncharts.com/fusioncharts/latest/themes/fusioncharts.theme.fusion.js"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg-secondary text-white" id="Div1">
        <!-- Top Margin & Nav Bar Back Color -->
        <br />
        <br />
    </div>
    <br />
    <form runat="server">
        <div class="row">
            <div class="col-1"></div>
            <div class="col-10">
                <div runat="server" id="LogedIn" visible="false">
                    <div runat="server" id="ReportsPage">
                        <!-- if the user is loged In -->

                        <!-- Jumbo Tron -->
                        <div class="jumbotron bg-dark text-white">
                            <h1>Reports</h1>

                            
                                <div class="row">
                            <div runat="server" id="SelectReport" class="col-9">
                                <div class="row">
                                    <div class="col-3">
                                        <!-- Report Selector -->
                                        <p>Select A Report: </p>
                                        <asp:DropDownList ID="drpReport" runat="server" AutoPostBack="True" class="dropdown"
                                            CssClass="btn btn-primary dropdown-toggle" OnSelectedIndexChanged="drpReport_SelectedIndexChanged1"
                                            OnTextChanged="drpReport_SelectedIndexChanged1">
                                            <asp:ListItem Text="Top Services" Value="4" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Top Products" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Top Customer" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Bookings" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Bookings Gross" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Bookings Missed" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Stylist Popularity" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="Customer Satisfaction" Value="7"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-3" runat="server" id="reportByContainer" visible="False">
                                        <p>Report By: </p>
                                        <asp:DropDownList ID="ddlReportFor" runat="server" AutoPostBack="True" CssClass="btn btn-secondary dropdown-toggle">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-3" runat="server" id="salesPaymentType" visible="False">
                                        <p>Payment Type:</p>
                                        <asp:DropDownList ID="drpPaymentType" runat="server" CssClass="btn btn-secondary dropdown-toggle" AutoPostBack="True">
                                            <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Credit" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                                    

                                    <div class="col-3" runat="server" id="hideFilters" visible="false">
                                        <asp:Button ID="btnHideFilters" runat="server" Text="Hide Filters" OnClick="btnHideFilters_Click" CssClass="btn btn-secondary" />
                                    </div>

                                </div>

                            <!-- line Break  -->
                            <br />
                            <asp:Label ID="lError" runat="server" Text="Label" Visible="false"></asp:Label>
                        </div>
                        <div class="row" runat="server" id="reportDateRangeContainer" visible="false">
                            <div class="col-lg-4">
                                <p>Start Date: </p>
                                <asp:DropDownList ID="drpStartMonth" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle"
                                    OnSelectedIndexChanged="drpStartMonth_SelectedIndexChanged">
                                    <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Febuary" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <br />
                                <asp:Calendar ID="CalendarDateStart" runat="server" Height="100" Width="200" OnSelectionChanged="btnRefresh_Click" OnDayRender="CalendarDateStart_DayRender"></asp:Calendar>
                            </div>
                            <div class="col-lg-4">
                                <p>End Date: </p>
                                <asp:DropDownList ID="drpEndMonth" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle"
                                    OnSelectedIndexChanged="drpEndMonth_SelectedIndexChanged">
                                    <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Febuary" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <br />
                                <asp:Calendar ID="CalendarDateEnd" runat="server" Height="100" Width="200" OnSelectionChanged="btnRefresh_Click" OnDayRender="CalendarDateStart_DayRender"></asp:Calendar>
                            </div>
                        </div>
                    </div>

                    <!-- line Break  -->
                    <br />

                    <div class="row" id="btnControlls" runat="server" visible="false">
                        <div class="col-3">
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CssClass="btn btn-secondary" />
                        </div>
                        <div class="col-3 text-center">
                            <!--Tabs-->
                            <ul class="nav" role="tablist">
                                <li>
                                    <asp:Button ID="btnViewText" runat="server" Text="Text" class="btn btn-primary" OnClick="btnViewText_Click" /></li>
                                <li>
                                    <asp:Button ID="btnViewGraph" runat="server" Text="Graph" class="btn btn-light" OnClick="btnViewGraph_Click" /></li>
                            </ul>
                        </div>
                        <div class="col-3 text-center" runat="server" id="divGraphType">
                            <ul class="nav" role="tablist">
                                <li>
                                    <asp:Button ID="btnShowBarGraph" runat="server" Text="Bar" class="btn btn-primary" OnClick="btnShowBarGraph_Click" /></li>
                                <li>
                                    <asp:Button ID="btnShowPieGraph" runat="server" Text="Pie" class="btn btn-light" OnClick="btnShowPieGraph_Click" /></li>
                            </ul>
                        </div>
                        <div class="col-3">
                            <asp:Button ID="btnPrint" runat="server" Text="Print Friendly Version" OnClick="btnPrint_Click" CssClass="btn btn-secondary" />
                        </div>

                        <div class="col-12">
                            <hr class="my-4">
                        </div>
                    </div>

                    <div runat="server" id="divPrintHeader" visible="false">
                        <div class="row">
                            <div class="col-md-12">
                                <!-- Logo -->
                                <asp:Table ID="tblLogo" runat="server"></asp:Table>
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divReportHeader" visible="false">
                        <div class="row">
                            <div class="col-md-8">
                                <asp:Label ID="reportLable" runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                            </div>
                            <div class="col-4">
                                <asp:Label ID="reportGenerateDateLable" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8">
                                <asp:Label ID="reportByLable" runat="server" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                            </div>
                            <div class="col-4"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-8">
                                <asp:Label ID="reportDateRangeLable" runat="server" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                            </div>
                            <div class="col-4"></div>
                        </div>
                    </div>

                    <div runat="server" id="divReport" visible="false">
                        <div class="row">
                            <div class="col-md-12">
                                <!-- Line Break -->
                                <br />
                                <br />
                                <!-- Report Table -->
                                <asp:Table ID="tblReport" runat="server">
                                </asp:Table>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-4"></div>
                            <div class="col-4" align="center">
                                <!-- Line Break -->
                                <br />
                                <br />
                                <h3>--- End Of Report ---</h3>
                            </div>
                            <div class="col-4"></div>
                        </div>
                    </div>

                    <div id="divGraph" runat="server" visible="false" style="text-align: center">
                        <!-- line Break  -->
                        <br />
                        <asp:Literal ID="graphBar" runat="server" Visible="true"></asp:Literal>
                        <asp:Literal ID="graphPie" runat="server" Visible="false"></asp:Literal>
                    </div>
                </div>

                <!-- if the user is loged Out -->
                <div runat="server" id="LogedOut">
                    <div class="jumbotron bg-dark text-white">
                        <div class="row">
                            <div class="col-10">
                                <h1>Reports</h1>
                                <p>Please log-in to view Reports</p>
                            </div>
                            <div class="col-2">
                                <a class="btn btn-primary" href="../Authentication/Accounts.aspx?PreviousPage=Reports.aspx" id="LogedOutButton">Login</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-1"></div>
        </div>
        <!-- line Break -->
        <br />
        <br />
    </form>
</asp:Content>
