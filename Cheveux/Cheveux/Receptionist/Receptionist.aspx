﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxReceptionist.Master" AutoEventWireup="true" CodeBehind="Receptionist.aspx.cs" Inherits="Cheveux.Receptionist" %>

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
        <div class="col-md-2 col-sm-1"></div>
        <div class="col-md-8 col-sm-10">
            <form runat="server">
                <!--If the user is logged in-->
                <!--jumbotron page heading-->
                <div class="jumbotron bg-dark text-white" runat="server" id="LoggedIn" visible="false">
                    <!--Date-->
                    <asp:Label ID="lblDate" runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                    <br />
                    <!--Welcome-->
                    <asp:Label ID="Welcome" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
                    <!--Line Break-->
                    <div class="container">
                        <br />
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-9 col-md-12">

                        <!--Check-In Success Message-->
                        <div class="container row" runat="server">
                            <asp:PlaceHolder ID="phCheckInSuccess" runat="server" Visible="false">
                                <div class="col-sm-12 col-md-12 alert alert-success alert-dismissible">
                                    <asp:Label ID="lblSuccess" runat="server" Text="Label"></asp:Label>
                                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                </div>
                            </asp:PlaceHolder>
                        </div>

                        <!--Dropdown with stylists names-->
                        <div id="viewAgenda" runat="server">
                            <div class="row">
                                <div class="col-xs-12 col-md-12">
                                    <h3 id="viewStylistHeader" runat="server">View Stylist Schedule
                                    </h3>
                                </div>
                            </div>
                            <div id="empDropdown" class="row">
                                <div class="col-xs-12 col-md-12">
                                    <asp:DropDownList ID="drpEmpNames" runat="server" AutoPostBack="True" 
                                        CssClass="btn btn-primary dropdown-toggle" OnSelectedIndexChanged="drpEmpNames_Changed">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp&nbsp<i class="fa fa-refresh" onClick="history.go(0)"></i>
                                </div>
                            </div>
                        </div>

                        <!--Line Break-->
                        <div class="container">
                            <br />
                        </div>

                        <!--Check-In Error Message-->
                        <div class="container row" runat="server">
                            <asp:PlaceHolder ID="phCheckInErr" runat="server" Visible="false">
                                <div class="col-sm-12 col-md-12 alert alert-danger alert-dismissible">
                                    <asp:Label ID="lblCheckinErr" runat="server" Text="Label"></asp:Label>
                                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                </div>
                            </asp:PlaceHolder>
                        </div>

                        <!--Bookings with the stylist on that day-->
                        <div id="Agenda">
                            <div class="row">
                                <div class="col-xs-12 col-md-12">
                                    <asp:Table runat="server" ID="AgendaTable"></asp:Table>
                                </div>
                            </div>
                        </div>

                        <!--If stylist doesn't have bookings on that day-->
                        <div class="container row" runat="server">
                            <asp:PlaceHolder ID="noBookingsPH" runat="server" Visible="false">
                                <div class="col-sm-12 col-md-12 alert alert-primary alert-dismissible">
                                    <asp:Label ID="lblNoBookings" runat="server" Text="Label"></asp:Label>
                                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                </div>
                            </asp:PlaceHolder>
                        </div>

                        <!--Error: If cant display the stylists bookings-->
                        <asp:PlaceHolder ID="phBookingsErr" runat="server" Visible="false">
                            <div class="container">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="jumbotron">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <!-- Error Image-->
                                                    <img src="https://cdn4.iconfinder.com/data/icons/smiley-vol-3-2/48/134-512.png" alt="Error" width="100" height="100"></img>
                                                </div>
                                                <div class="col-md-10">
                                                    <!--Error details placehoders-->
                                                    <asp:Label ID="errorHeader" runat="server"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="errorMessage" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:PlaceHolder>
                    </div>

                    <div class="col-lg-3 col-md-12" runat="server" id="sidebar" visible="false">
                        <div style="border: solid #F05F40 2px;" runat="server" id="alertsContainer">
                            <!--Alerts-->
                            <div class="container">
                                <!-- Alert Table -->
                                <h1>Alerts </h1>
                                <asp:Table ID="tblAlerts" runat="server">
                                </asp:Table>
                            </div>
                        </div>
                        <!--Line Break-->
                            <br />
                        <div style="border: solid #F05F40 2px;" runat="server" id="makeABookingContainer">
                            <!--Bookings-->
                            <div class="container text-center">
                                <br />
                                <asp:Button ID="btnMakeInternalBooking" runat="server" OnClick="btnMakeInternalBooking_Click" 
                                    Text="Make a booking" CssClass="btn btn-secondary btn-lg"/>
                                <br />
                                <br />
                            </div>
                        </div>
                        <!--Line Break-->
                            <br />
                        <div style="border: solid #F05F40 2px;" runat="server" id="DivNewCustomer">
                            <!--Bookings-->
                            <div class="container text-center">
                                <br />
                                <asp:Button ID="btnNewCust" runat="server" OnClick="btnNewCust_Click" 
                                    Text="New Customer" CssClass="btn btn-secondary btn-lg"/>
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="jumbotron bg-dark text-white" runat="server" id="LoggedOut" visible="true">
                        <div class="row">
                            <div class="col-lg-9 col-md-12 col-sm-12">
                                <h1>Dashboard</h1>
                                <p>Please Login</p>
                            </div>
                            <div class="col-lg-3 col-md-2 col-sm-2">
                                <a class="btn btn-primary" href="../Authentication/Accounts.aspx?PreviousPage=Receptionist.aspx" id="LogedOutButton">Login</a>
                            </div>
                        </div>
                    </div>
            </form>
        </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
