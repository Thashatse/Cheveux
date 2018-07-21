<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Cheveux.Manager.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Reports - Cheveux
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg-secondary text-white" id="Div1">
        <!-- Top Margin & Nav Bar Back Color -->
        <br />
        <br />
        <br />
    </div>
    <br />
    <div class="row">
        <div class="col-md-2 col-sm-1"></div>
        <div class="col-md-8 col-sm-10">

            <div runat="server" id="ReportsPage">
                <!-- if the user is loged In -->
                <div runat="server" id="LogedIn" visible="false">
                    <div class="row">
                        <div class="col-md-12">
                            <!-- Jumbo Tron -->
                            <div class="jumbotron bg-dark text-white">
                                <h1>Reports</h1>
                                <!-- line Break -->
                                <br />
                                <form runat="server">

                                    <!-- Report Selector -->
                                    <p>Select A Report: </p>
                                    <asp:DropDownList ID="drpReport" runat="server" AutoPostBack="True" class="dropdown"
                                        CssClass="btn btn-primary dropdown-toggle" OnSelectedIndexChanged="drpReport_SelectedIndexChanged1"
                                        OnTextChanged="drpReport_SelectedIndexChanged1">

                                        <asp:ListItem Text="Select A Report" Value="0"></asp:ListItem>

                                        <asp:ListItem Text="Sales" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Bookings By Hairstylist" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Booking By Hairstylist For Date Range" Value="3"></asp:ListItem>

                                    </asp:DropDownList>

                                    <!-- line Break -->
                                    <br />
                                    <br />

                                    <div class="container" runat="server" id="reportByContainer" visible="False">
                                        <p>Report By: </p>
                                        <asp:DropDownList ID="ddlReportFor" runat="server" AutoPostBack="True" CssClass="btn btn-secondary dropdown-toggle">
                                        </asp:DropDownList>
                                    </div>

                                    <!-- line Break  -->
                                    <br />
                                    <br />

                                    <div class="container" runat="server" id="reportDateRangeContainer" visible="false">
                                        <p>Report Date Range: </p>
                                        <asp:Calendar ID="CalendarDateRage" runat="server"></asp:Calendar>
                                    </div>
                                </form>
                                <!-- line Break -->
                                <br />
                                <!--Help-->
                                <a href="../Help/CheveuxHelpCenter.aspx#Reports" target="_blank" title="How To Generate Reports">
                                    <span class="glyphicon">&#63; Help</span></a>
                            </div>
                        </div>
                    </div>
                </div>

                <div runat="server" id="divReport" visible="false">
                    <div class="row">
                        <div class="col-md-8">
                            Title:
                                <asp:Label ID="reportLable" runat="server"></asp:Label>

                        </div>
                        <div class="col-4">
                            Generated:
                                <asp:Label ID="reportGenerateDateLable" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8">
                            For:
                                <asp:Label ID="reportByLable" runat="server"></asp:Label>
                        </div>
                        <div class="col-4"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-8">
                            Date Range:
                                <asp:Label ID="reportDateRangeLable" runat="server"></asp:Label>
                        </div>
                        <div class="col-4"></div>
                    </div>

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
                            <button type="button" class="btn btn-default">
                                <a href="../Authentication/Accounts.aspx?PreviousPage=Reports.aspx" id="LogedOutButton">Login</a>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
