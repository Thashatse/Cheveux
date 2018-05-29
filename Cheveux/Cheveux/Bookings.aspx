﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="Bookings.aspx.cs" Inherits="Cheveux.Bookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Bookings - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron">
        <h1>Bookings</h1>
        <!-- if the user is loged in -->
        <div class="container" runat="server" id="JumbotronLogedIn" visible="false">
        <button type="button" class="btn btn-default">Make A Booking</button>
            </div>
        <!-- if the user is loged Out -->
        <div class="container" runat="server" id="JumbotronLogedOut">
            <p>Please log-in</p>
        <button type="button" class="btn btn-default"><a href="Accounts.aspx?PreviousPage=Bookings.aspx" id="LogedOut">Login / Sign Up</a></button>
            </div>
    </div>
    <!--Tabs-->
    <div class="container" runat="server" id="Tabs">
        <div class="row">
            <div class="col-md-4">
                <!--Tabs-->
                <ul class="nav nav-tabs" role="tablist">
                    <li><a href="#Upcoming Service(s)" role="tab" data-toggle="tab">Upcoming Service(s)</a></li>
                    <li><a href="#Past Service(s)" role="tab" data-toggle="tab">Past Service(s)</a></li>
                </ul>

                <!--Tabs Content-->
                <div class="tab-content">
                    <div class="active tab-pane" id="Upcoming Service(s)">
                        <h2>Upcoming Service(s)</h2>
                        <!--Content-->
                        <!-- Display all upocoming services for the client -->

                    </div>

                    <div class="tab-pane" id="Past Service(s)">
                        <h2>Past Service(s)</h2>
                        <!--Content-->
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>