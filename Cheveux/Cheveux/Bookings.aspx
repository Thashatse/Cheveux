<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="Bookings.aspx.cs" Inherits="Cheveux.Bookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Bookings - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg-secondary text-white" id="Div1">
        <!-- Top Margin & Nav Bar Back Color -->
        <br />
        <br />
    </div>
    <!-- Top Margin -->
    <br />
    <div class="row">
        <div class="col-md-2 col-sm-1"></div>
        <div class="col-md-8 col-sm-10">
            <div class="jumbotron bg-dark text-white">
                <!--Line Break-->
                <br />
                <br />
                <!-- if the user is loged in -->
                <div class="container" runat="server" id="JumbotronLogedIn" visible="false">
                    <div class="row">
                        <div class="col-10">
                            <h1>Bookings</h1>
                        </div>
                        <div class="col-2">
                            <a class="btn btn-primary js-scroll-trigger" href="MakeABooking.aspx">Make a booking</a>
                        </div>
                    </div>
                </div>
                <!-- if the user is loged Out -->
                <div class="container" runat="server" id="JumbotronLogedOut">
                    <div class="row">
                        <div class="col-10">
                            <h1>Bookings</h1>
                            <p>Please log-in to view bookings</p>
                        </div>
                        <div class="col-2">
                            <button type="button" class="btn btn-default"><a href="../Authentication/Accounts.aspx?PreviousPage=Bookings.aspx" id="LogedOut">Login</a></button>
                        </div>
                    </div>
                </div>
            </div>

        <!--Tabs-->
        <div runat="server" id="Tabs">
            <div class="row">
                <div class="col-md-12">
                    <!--Tabs-->
                    <ul class="nav nav-tabs" role="tablist">
                        <li><a class="btn" href="#UpcomingService" role="tab" data-toggle="tab">Upcoming Booking(s)&#09;</a></li>
                        <li><a class="btn" href="#PastService" role="tab" data-toggle="tab">Past Booking(s)</a></li>
                    </ul>

                    <!--Tabs Content-->
                    <div class="tab-content">
                        <div class="active tab-pane" id="UpcomingService">
                            <h2>Upcoming Bookings(s)</h2>
                            <!--Content-->
                            <!-- Display all upocoming Bookings for the client -->
                            <div class="row">
                                <div class="col-xs-12 col-md-12">
                                    <asp:Label runat="server" ID="upcomingBookingsLable"></asp:Label>
                                    <asp:Table ID="upcomingBookings" runat="server"></asp:Table>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane" id="PastService">
                            <h2>Past Bookings(s)</h2>
                            <!--Content-->
                            <!-- Display all Past Bookings for the client -->
                            <div class="row">
                                <div class="col-xs-12 col-md-12">
                                    <asp:Label runat="server" ID="pastBookingsLable"></asp:Label>
                                    <asp:Table ID="pastBookings" runat="server"></asp:Table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
