<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="ViewBooking.aspx.cs" Inherits="Cheveux.ViewBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function goBack() {
            window.history.back()
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" runat="server" id="Bookings">
        <!-- if the user is loged In -->
        <!-- Display the Booking for the client -->
        <div class="container" runat="server" id="LogedIn" visible="false">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label runat="server" ID="BookingLable"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <asp:Table ID="BookingTable" runat="server"></asp:Table>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <!-- Display the Booking for the client -->
                    <asp:Label ID="BackButton" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <!-- if the user is loged Out -->
        <div class="container" runat="server" id="LogedOut">
            <div class="jumbotron">
                <p>Please log-in</p>
                <button type="button" class="btn btn-default"><a href="Accounts.aspx?PreviousPage=Bookings.aspx" id="LogedOutButton">Login / Sign Up</a></button>
            </div>
        </div>
    </div>
</asp:Content>
