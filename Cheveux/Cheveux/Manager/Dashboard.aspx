<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Cheveux.Manager.Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Dashboard - Cheveux
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
            <div runat="server" id="managerDashboardPage">
                <!-- if the user is loged In -->
                <div runat="server" id="LogedIn" visible="false">
                    <div class="row">
                        <div class="col-md-12">

                            <!-- Jumbo Tron -->
                            <div class="jumbotron bg-dark text-white">
                                <!--Date-->
                                <asp:Label ID="lJumbotronDate" runat="server" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                                <!-- line Break -->
                                <br />
                                <!--Welcome-->
                                <asp:Label ID="Welcome" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-9 col-md-12">
                            <div runat="server" id="divStats">
                                <!--Stats-->
                                <h1>Stats </h1>
                                <div class="row">
                                    <div class="col-md-12 col-lg-3">
                                        <h5>Todays Sales </h5>
                                        <asp:Label ID="lStats1" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-12 col-lg-3">
                                        <h5>Upcoming Bookings </h5>
                                        <asp:Label ID="lStats2" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-12 col-lg-3">
                                        <h5>Total Bookings </h5>
                                        <asp:Label ID="lStats3" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-12 col-lg-3">
                                        <h5>Registered Customers </h5>
                                        <asp:Label ID="lStats4" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 text-center">
                                <!-- Line Break -->
                                <br />
                                <hr class="my-4">
                                <!-- Line Break -->
                                <br />
                            </div>

                            <p style="text-align: left; float: left;">
                                <!--Todays Bookings-->
                                <h1>Todays Bookings </h1>
                            </p>
                            <p style="text-align: right; float: right;">
                                <a href="../Receptionist/Appointments.aspx?Action=ViewAllSchedules">View Booking Schedules </a>
                            </p>
                            <!-- Line Break -->
                            <br />
                            <!-- Bookings Count lable -->
                            <asp:Label ID="bookingsLable" runat="server"></asp:Label>

                            <!-- Bookings Table -->
                            <asp:Table ID="tblBookings" runat="server">
                            </asp:Table>

                            <div class="col-12 text-center">
                                <!-- Line Break -->
                                <br />
                                <hr class="my-4">
                                <!-- Line Break -->
                                <br />
                            </div>

                            <div runat="server" id="divOrders">
                                <!--Orders-->
                                <h1>Outstanding Product Orders </h1>
                                <!-- Orders Table -->
                                <asp:Table ID="productOrders" runat="server">
                                </asp:Table>
                            </div>

                        </div>
                        <div class="col-lg-3 col-md-12">
                            <!--Alerts-->
                            <div class="container" runat="server" id="alertsContainer" visible="false" style="border: solid #F05F40 2px;">
                                <div class="container">
                                    <!-- Alert Table -->
                                    <h1>Alerts </h1>
                                    <asp:Table ID="tblAlerts" runat="server">
                                    </asp:Table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- if the user is loged Out -->
                <div class="container" runat="server" id="LogedOut">
                    <div class="jumbotron bg-dark text-white">
                        <div class="row">
                            <div class="col-10">
                                <h1>Dashboard</h1>
                                <p>Please log-in to view your Dashboard</p>
                            </div>
                            <div class="col-2">
                                <a class="btn btn-primary" href="../Authentication/Accounts.aspx?PreviousPage=Manager.aspx" id="LogedOutButton">Login</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-1"></div>
    </div>
</asp:Content>
