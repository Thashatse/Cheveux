﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxStylist.Master" AutoEventWireup="true" CodeBehind="CustomerVisit.aspx.cs" Inherits="Cheveux.CustomerVisit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Customer Visit - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <style>
        #box {
            border: 1px solid #ddd;
            border-radius: 0;
            padding: 5px;
            height: auto;
            overflow-y: scroll;
            width: 30%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div class="bg-secondary text-white" id="Div1">
            <!-- Top Margin & Nav Bar Back Color -->
            <br />
            <br />
        </div>
        <br />

        <div class="row">
            <div class="col-1"></div>
            <div class="col-10">
                <div class="jumbotron  bg-dark text-white" id="LoggedIn" runat="server">
                    <asp:Label ID="theVisit" runat="server" Font-Bold="true" Font-Size="X-Large">
                        <h1 runat="server" id="jheader"></h1>
                    </asp:Label>
                </div>
                <div class="container">
                    <br />
                </div>

                <!--Customer visit Error Message-->
                <div class="container row" runat="server">
                    <asp:PlaceHolder ID="phVisitErr" runat="server" Visible="false">
                        <div class="col-sm-12 col-md-12 alert alert-danger alert-dismissible">
                            <asp:Label ID="lblVisitErr" runat="server" Text="Label"></asp:Label>
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        </div>
                    </asp:PlaceHolder>
                </div>

                <!--All Details of booking -->
                <div id="CustomerBookingDTLs">
                    <div class="row">
                        <div class="col-xs-12 col-md-12">
                            <asp:PlaceHolder ID="phBookingDetails" runat="server">
                                <asp:Table ID="allBookingTable" runat="server">
                                </asp:Table>
                                <br />
                            </asp:PlaceHolder>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-md-12">
                            <asp:PlaceHolder ID="phOther" runat="server">
                                <asp:Table runat="server" ID="tblOther">
                                </asp:Table>
                            </asp:PlaceHolder>
                        </div>
                    </div>
                </div>

                <!--Error: If details cant be displayed-->
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
                                    <br />
                                    <a href="../Stylist/Stylist.aspx">Dashboard</a>
                                    <a href="Help/CheveuxHelpCenter.aspx" target="_blank">Help Center</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:PlaceHolder>


                <!--Login requeset-->
                <div class="container" runat="server" id="LoggedOut" visible="true">
                    <div class="jumbotron bg-dark text-white">
                        <h1>Please Log-in to proceed</h1>
                        <button type="button" class="btn btn-primary">
                            <a href="../Authentication/Accounts.aspx?PreviousPage=Stylist.aspx" id="LogedOutButton">Login</a>
                        </button>
                    </div>
                </div>

                <!--Line Break-->
                <div class="container">
                    <br />
                </div>
            </div>
            <div class="col-1"></div>
        </div>
    </form>
</asp:Content>
