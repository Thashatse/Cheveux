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
    </div>
    <br />
    <div class="row">
        <div class="col-1"></div>
        <div class="col-10">

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


                                        <asp:ListItem Text="Sales" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Bookings" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Top Customer" Value="2"></asp:ListItem>

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

                                    <div class="container" runat="server" id="salesPaymentType" visible="False">
                                        Payment Type:
                                        <asp:DropDownList ID="drpPaymentType" runat="server" CssClass="btn btn-secondary dropdown-toggle" AutoPostBack="True">
                                            <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Credit" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <!-- line Break  -->
                                    <br />
                                    <br />

                                    <div class="row" runat="server" id="reportDateRangeContainer" visible="false">
                                        <div class="col-lg-5">
                                            <p>Start Date: </p>
                                            <asp:Calendar CssClass="bg-secondary text-primary" ID="CalendarDateStrart" runat="server" OnSelectionChanged="btnRefresh_Click"></asp:Calendar>
                                        </div>
                                        <div class="col-lg-5">
                                            <p>End Date: </p>
                                            <asp:Calendar CssClass="bg-secondary text-primary" ID="CalendarDateEnd" runat="server" OnSelectionChanged="btnRefresh_Click"></asp:Calendar>
                                        </div>
                                    </div>

                                    <!-- line Break  -->
                                    <br />
                                    <br />

                                    <div class="row">
                                        <div class="col-8"></div>
                                        <div class="col-4">
                                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CssClass="btn btn-secondary" />
                                            <asp:Button ID="btnPrint" runat="server" Text="Print Friendly Version" OnClick="btnPrint_Click" Visible="false" CssClass="btn btn-secondary" />
                                        </div>
                                    </div>
                                </form>
                                <!-- line Break -->
                                <br />
                                <asp:Label ID="lError" runat="server" Text="Label" Visible="false"></asp:Label>
                            </div>
                        </div>
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

                <div runat="server" id="divReport" visible="false">
                    <div class="row">
                        <div class="col-md-12">
                            <!--Logo-->
                            <!-- <img src="../IMG_0715.png" alt="logo" width="300" height="300" class="img-fluid" />-->
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8">
                            <asp:Label ID="reportLable" runat="server" Font-Bold Font-Size="XX-Large"></asp:Label>

                        </div>
                        <div class="col-4">
                            <asp:Label ID="reportGenerateDateLable" runat="server" Font-Bold></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8">
                            <asp:Label ID="reportByLable" runat="server" Font-Bold Font-Size="X-Large"></asp:Label>
                        </div>
                        <div class="col-4"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-8">
                            <asp:Label ID="reportDateRangeLable" runat="server" Font-Bold Font-Size="X-Large"></asp:Label>
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
                            <a class="btn btn-primary" href="../Authentication/Accounts.aspx?PreviousPage=Reports.aspx" id="LogedOutButton">Login</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-1"></div>
    </div>
</asp:Content>
