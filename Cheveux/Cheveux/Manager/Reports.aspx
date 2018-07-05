<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Cheveux.Manager.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Reports - Cheveux
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" runat="server" id="ReportsPage">
        <!-- if the user is loged In -->
        <div class="container" runat="server" id="LogedIn" visible="false">
            <div class="row">
                <div class="col-md-12">

                    <!-- Jumbo Tron -->
                    <div class="jumbotron">
                        <h1>Reports</h1>
                        <!-- line Break -->
                        <br />
                        <form runat="server">

                            <!-- Report Selector -->
                            <p>Select A Report: </p>
                            <asp:DropDownList ID="drpReport" runat="server" AutoPostBack="True">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            </asp:DropDownList>

                            <!-- line Break -->
                        <br />
                            <br />

                            <div class="container" runat="server" id="reportByContainer" visible="false">
                                <p>Report For: </p>
                                <asp:DropDownList ID="ddlReportFor" runat="server" AutoPostBack="True">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                                </div>

                            <!-- line Break  -->
                        <br />
                            <br />

                            <div class="container" runat="server" id="reportDateRangeContainer" visible="false" >
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

            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="reportLable" runat="server"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <!-- Report Table -->
                    <asp:Table ID="tblReport" runat="server">
                    </asp:Table>
                </div>
            </div>
        </div>
        <!-- if the user is loged Out -->
        <div class="container" runat="server" id="LogedOut">
            <div class="jumbotron">
                <h1>Reports</h1>
                <p>Please log-in to view Reports</p>
                <button type="button" class="btn btn-default">
                    <a href="../Authentication/Accounts.aspx?PreviousPage=Reports.aspx" id="LogedOutButton">Login</a>
                </button>
            </div>
        </div>
    </div>
</asp:Content>