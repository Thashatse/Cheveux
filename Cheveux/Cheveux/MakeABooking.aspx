<%@ Page Title ="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="MakeABooking.aspx.cs" Inherits="Cheveux.MakeABooking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Profile - Cheveux
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
            <h1 style="text-align:center">Make A Booking</h1>
            <asp:Table ID="Service" runat="server">
                <asp:TableHeaderRow><asp:TableHeaderCell VerticalAlign="Middle" ColumnSpan="10" HorizontalAlign="Center"><asp:Label runat="server" Text="Services"></asp:Label></asp:TableHeaderCell></asp:TableHeaderRow> 
                <asp:TableRow><asp:TableCell VerticalAlign="Top" ColumnSpan="10"><asp:Label runat="server" Text="Pick a service:"></asp:Label> </asp:TableCell></asp:TableRow>
                <asp:TableRow><asp:TableCell VerticalAlign="Top" RowSpan ="6"><asp:CheckBoxList ID="Services" runat="server"></asp:CheckBoxList></asp:TableCell></asp:TableRow>
                <asp:TableFooterRow><asp:TableCell VerticalAlign="Middle" HorizontalAlign="Right" ColumnSpan="10"><asp:Button runat="server" ID="PickStylist" Text="Choose Hairstylist ->" OnClick="btnStylist"/></asp:TableCell></asp:TableFooterRow>
            </asp:Table>
            <asp:Table ID="Hairstylist" runat="server" Visible="false">
                <asp:TableHeaderRow><asp:TableHeaderCell VerticalAlign="Middle" ColumnSpan="10" HorizontalAlign="Center"><asp:Label runat="server" Text="Hairstylists"></asp:Label></asp:TableHeaderCell></asp:TableHeaderRow> 
                <asp:TableRow><asp:TableCell VerticalAlign="Top" ColumnSpan="10"><asp:Label runat="server" Text="Pick a hairstylist:"></asp:Label></asp:TableCell></asp:TableRow>
                <asp:TableRow><asp:TableCell VerticalAlign="Top" RowSpan ="6"><asp:RadiobuttonList ID="Hairstylists" runat="server"></asp:RadiobuttonList></asp:TableCell></asp:TableRow>
                <asp:TableFooterRow><asp:TableCell VerticalAlign="Middle" ColumnSpan="5"><asp:Button runat="server" ID="PickService" Text="<- Choose Service" OnClick="btnService" /></asp:TableCell><asp:TableCell VerticalAlign="Middle" ColumnSpan="5"><asp:Button runat="server" ID="PickDate" Text="Choose Date & Time ->" OnClick="btnDateTime" /></asp:TableCell></asp:TableFooterRow>
            </asp:Table>
             <asp:Table ID="Date" runat="server" Visible="false">
                <asp:TableHeaderRow><asp:TableHeaderCell VerticalAlign="Middle" ColumnSpan="10" HorizontalAlign="Center"><asp:Label runat="server" Text="Date & Time"></asp:Label></asp:TableHeaderCell></asp:TableHeaderRow> 
                <asp:TableRow><asp:TableCell VerticalAlign="Top" ColumnSpan="10"><asp:Label runat="server" Text="Pick Date & Time:"></asp:Label></asp:TableCell></asp:TableRow>
                <asp:TableRow><asp:TableCell VerticalAlign="Top" ColumnSpan="5"><asp:Calendar runat="server"></asp:Calendar></asp:TableCell><asp:TableCell VerticalAlign="Top" ColumnSpan="5"><asp:DropDownList runat="server" ID="Time" OnSelectedIndexChanged="Time_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></asp:TableCell></asp:TableRow>
                <asp:TableFooterRow><asp:TableCell VerticalAlign="Middle" ColumnSpan="5"><asp:Button runat="server" ID="ChooseStylist" Text="<- Choose Hairstylist" OnClick="btnHairStylist" /></asp:TableCell><asp:TableCell VerticalAlign="Middle" ColumnSpan="5"><asp:Button runat="server" ID="Confirm" Text="Confirm Booking ->" OnClick="btnConfirm" /></asp:TableCell></asp:TableFooterRow>
            </asp:Table>
            <asp:Table ID="Summary" runat="server" Visible="false">
                <asp:TableHeaderRow><asp:TableHeaderCell VerticalAlign="Middle" ColumnSpan="10" HorizontalAlign="Center"><asp:Label runat="server" Text="Booking Summary"></asp:Label></asp:TableHeaderCell></asp:TableHeaderRow>
                <asp:TableRow><asp:TableCell VerticalAlign="Top" ColumnSpan="5"><asp:Label runat="server" Text="Service:"></asp:Label></asp:TableCell><asp:TableCell VerticalAlign="Top" ColumnSpan="5"><asp:Placeholder ID="ServiceName" runat="server"></asp:Placeholder></asp:TableCell></asp:TableRow>
                <asp:TableRow><asp:TableCell VerticalAlign="Top" ColumnSpan="5"><asp:Label runat="server" Text="Stylist:"></asp:Label></asp:TableCell><asp:TableCell VerticalAlign="Top" ColumnSpan="5"><asp:Placeholder ID="StylistName" runat="server"></asp:Placeholder></asp:TableCell></asp:TableRow>
                <asp:TableRow><asp:TableCell VerticalAlign="Top" ColumnSpan="5"><asp:Label runat="server" Text="Date & Time:"></asp:Label></asp:TableCell><asp:TableCell VerticalAlign="Top" ColumnSpan="3"><asp:Placeholder ID="bDate" runat="server"></asp:Placeholder></asp:TableCell><asp:TableCell VerticalAlign="Top" ColumnSpan="2"><asp:Placeholder ID="bTime" runat="server"></asp:Placeholder></asp:TableCell></asp:TableRow>
                <asp:TableFooterRow><asp:TableCell VerticalAlign="Middle" ColumnSpan="5"><asp:Button runat="server" ID="PickTime" Text="<- Choose Date & Time" OnClick="btnDate" /></asp:TableCell><asp:TableCell VerticalAlign="Middle" ColumnSpan="5"><asp:Button runat="server" Text="Confirm Booking ->" OnClick="btnBConfirm" /></asp:TableCell></asp:TableFooterRow>
            </asp:Table>

        </div>
    </div>













    
</asp:Content>
