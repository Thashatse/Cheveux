<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxStylist.Master" AutoEventWireup="true" CodeBehind="CustomerVisit.aspx.cs" Inherits="Cheveux.CustomerVisit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Customer Visit - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
        <style>
        body{
            margin-top:150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="jumbotron">
            <asp:Label ID="theVisit" runat="server" Font-Bold="true" Font-Size="X-Large">
                <h1>Customer Visit</h1>
                <h2 id="theDate" runat="server"></h2>
            </asp:Label>
        </div>
        <div class="container">
            <br />
        </div>

        <!--All Details of booking -->
        <div id="CustomerBookingDTLs">
            <div class="row">
                <h1 runat="server" id="lblBookingDetailsHeading">Customer Booking Details</h1>
            </div>
            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <asp:PlaceHolder ID="phBookingDetails" runat="server">
                        <asp:Table ID="allBookingTable" runat="server">
                        </asp:Table>
                        <br />
                    </asp:PlaceHolder>
                </div>
            </div>
        </div>

        <!--Edit Service Details-->
        <div id="BookingServiceDTLs">
            <div class="row">
                <h1 runat="server" id="lblServiceHeading">Edit Customer Visit Record</h1>
            </div>
            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <asp:PlaceHolder ID="phServiceDetails" runat="server" Visible="False">
                        <asp:Table ID="serviceDetailsTable" runat="server">
                        </asp:Table>
                        <br />
                    </asp:PlaceHolder>
                </div>
            </div>
        </div>

        <!--Confirm Visit-->
        <!--
        <div id="ConfirmVisitRecord">
            <div class="row">
                <h1 runat="server" id="lblConfirmUpdateHeading">Confirm Update</h1>
            </div>
            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <asp:PlaceHolder ID="phConfirmVisit" runat="server" Visible="False">
                        <asp:Table ID="confirmVisitTable" runat="server"></asp:Table>
                        <br />
                    </asp:PlaceHolder>
                </div>
            </div>
        </div>
        -->

    </div>
</asp:Content>
