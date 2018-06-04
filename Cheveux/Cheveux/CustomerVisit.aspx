<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxStylist.Master" AutoEventWireup="true" CodeBehind="CustomerVisit.aspx.cs" Inherits="Cheveux.CustomerVisit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
Customer Visit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <div class="jumbotron">
            <asp:Label ID="Visit" runat="server" Font-Bold="true" Font-Size="X-Large">
                <h1>Customer Visit</h1>
                <h2 id="theDate" runat="server"></h2>
            </asp:Label>
        </div>
        <div class="container">
                    <br />
                </div>

        <!--All Details of booking --> 
        <asp:PlaceHolder ID="cBookingDetails" runat="server">
            <h1>Customer Booking Details</h1>
        </asp:PlaceHolder>


        <!--Edit Service Details-->
        <asp:PlaceHolder ID="eServiceDetails" runat="server" Visible="False">
            <h1>Edit Service</h1>
        </asp:PlaceHolder>


        <!--Confirm Visit-->
        <asp:PlaceHolder ID="ConfirmVisit" runat="server" Visible="False">
            <h1>Confirm Update</h1>
        </asp:PlaceHolder>




    </div>


</asp:Content>
