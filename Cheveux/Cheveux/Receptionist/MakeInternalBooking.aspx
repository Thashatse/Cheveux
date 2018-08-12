<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxReceptionist.Master" AutoEventWireup="true" CodeBehind="MakeInternalBooking.aspx.cs" Inherits="Cheveux.MakeInternalBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Make A Booking - Cheveux
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
        <div class="col-md-1 col-sm-1"></div>
        <div class="col-md-10 col-sm-10">
            <form runat="server">
            <div class="jumbotron  bg-dark text-white">
                <h1>Make A Booking</h1>
                <br />
                <asp:Label runat="server" ID="BookingSummary"></asp:Label>
            </div>
            <div style="border: solid #F05F40 2px;" runat="server" id="divSelectStyle">
                <!--Style-->
                <div class="container">
                    <h3>Select Style</h3>
                    <!--Line Break-->
                        <br />
                    <asp:Button ID="btnSelectCustomer" runat="server" Text="Select Customer" CssClass="btn btn-primary" OnClick="btnSelectCustomer_Click" />
                </div>
            </div>
            <div style="border: solid #F05F40 2px;" runat="server" id="divSelectUser" visible="false">
                <!--User-->
                <div class="container">
                    <h3>Select Customer</h3>
                    <!--Line Break-->
                        <br />
                    <asp:Button ID="btnComfirmation" runat="server" Text="Summary" CssClass="btn btn-primary" OnClick="btnComfirmation_Click" />
                </div>
            </div>
            <div style="border: solid #F05F40 2px;" runat="server" id="divSummary" visible="false">
                <!--User-->
                <div class="container">
                    <h3>Summary</h3>
                    <!--Line Break-->
                        <br />
                    <asp:Button ID="btnMakeBooking" runat="server" Text="Make Booking" CssClass="btn btn-primary" OnClick="btnMakeBooking_Click" />
                </div>
            </div>
        </form>
        </div>
        <div class="col-md-1 col-sm-1"></div>
    </div>
</asp:Content>
