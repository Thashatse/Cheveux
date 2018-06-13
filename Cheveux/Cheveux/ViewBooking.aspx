<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="ViewBooking.aspx.cs" Inherits="Cheveux.ViewBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function goBack() {
            window.history.back()
        }
    </script>

        <style>
        body{
            margin-top:150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" runat="server" id="Bookings">
        <!-- if the user is loged In -->
        <!-- Display the Booking for the client -->
        <div class="container" runat="server" id="LogedIn" visible="false">
            <div class="row">
                <div class="col-md-12">
                    <!--Bookings Heading-->
                    <asp:Label runat="server" ID="BookingLable"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <!--Booking Table-->
                    <asp:Table ID="BookingTable" runat="server"></asp:Table>
                    <!--Edit Booking Table -->
                    <div class="container" runat="server" id="Edit" visible="false">
                        <asp:Table ID="editBookingTable" runat="server">
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell>
                                    <!--Service Name-->
                                    <asp:DropDownList ID="DDLService" runat="server"></asp:DropDownList>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell>
                                    <!--Service Description-->
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell>
                                    <!--Service Price-->
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell>
                                    <!--Service Stylist-->
                                    <asp:DropDownList ID="DDLStylist" runat="server"></asp:DropDownList>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell>
                                    <!--Booking Date-->

                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell>
                                    <!--Booking Time-->

                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell>
                                    <!--Cancel Updates-->
                                    <a href = 'javascript:history.back()'>Cancel   </a>
                                    <!--Save Updates-->
                                    <asp:Button ID="Save" runat="server" Text="Save" class="btn btn-default" OnClick="Save_Click"/></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
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

    <div class="container" runat="server" id="confirm" visible="false">
        <div class="jumbotron">

            <asp:Label ID="confirmHeaderPlaceHolder" runat="server"></asp:Label>
            <asp:Label ID="confirmPlaceHolder" runat="server"></asp:Label>
            <!--Line Break-->
            <br />
            <br />
            <!-- Edit -->
            <asp:Button ID="no" runat="server" Text="No" class="btn btn-default" OnClick="showEdit" />
            <asp:Button ID="yes" runat="server" Text="Yes" class="btn btn-default" OnClick="commitEdit" />
            <asp:Button ID="OK" runat="server" Text="OK" class="btn btn-default" OnClick="OK_Click" Visible="false" />
        </div>
    </div>
</asp:Content>
