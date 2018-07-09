<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Cheveux.Manager.Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Dashboard - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" runat="server" id="managerDashboardPage">
        <!-- if the user is loged In -->
        <div class="container" runat="server" id="LogedIn" visible="false">
            <div class="row">
                <div class="col-md-12">

                    <!-- Jumbo Tron -->
                    <div class="jumbotron">
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
                <div class="col-md-12">

                    <!--Alerts-->
                    <div class="container" runat="server" id="alertsContainer" visible="false">
                        <!-- Alert Table -->
                        <h1>Alerts </h1>
                        <asp:Table ID="tblAlerts" runat="server">
                        </asp:Table>
                    </div>

                </div>
            </div>
            <!-- line Break -->
                        <br />
            <div class="row">
                <div class="col-md-12">

                    <!--Todays Bookings-->
                    <h1>Todays Bookings </h1>
                    <!-- Bookings Count lable -->
                    <asp:Label ID="bookingsLable" runat="server"></asp:Label>

                    <!-- Bookings Table -->
                    <asp:Table ID="tblBookings" runat="server">
                    </asp:Table>

                </div>
            </div>
        </div>
        <!-- if the user is loged Out -->
        <div class="container" runat="server" id="LogedOut">
            <div class="jumbotron">
                <h1>Dashboard</h1>
                <p>Please log-in to view your Dashboard</p>
                <button type="button" class="btn btn-default">
                    <a href="../Authentication/Accounts.aspx?PreviousPage=Manager.aspx" id="LogedOutButton">Login</a>
                </button>
            </div>
        </div>
    </div>
</asp:Content>
