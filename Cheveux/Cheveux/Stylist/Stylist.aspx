<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxStylist.Master" AutoEventWireup="true" CodeBehind="Stylist.aspx.cs" Inherits="Cheveux.Stylist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Stylist Dashboard - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
            <div class="col-md-2"></div>
            <div class="col-md-8 col-sm-12">

                <!--jumbotron page heading-->
                <div class="bg-dark text-white jumbotron" runat="server" id="LoggedIn" visible="false">
                    <!--Date-->
                    <asp:Label ID="lblDate" runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                </div>

                <!--Create Visit Record Success Message-->
                <div class="container row" runat="server">
                    <asp:PlaceHolder ID="phVisitSuccess" runat="server" Visible="false">
                        <div class="col-sm-12 col-md-12 alert alert-success alert-dismissible">
                            <asp:Label ID="lblSuccess" runat="server" Text="Label"></asp:Label>
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        </div>
                    </asp:PlaceHolder>
                </div>

                <!--Create Visit Record Error Message-->
                <div class="container row" runat="server">
                    <asp:PlaceHolder ID="phRecordErr" runat="server" Visible="false">
                        <div class="col-sm-12 col-md-12 alert alert-danger alert-dismissible">
                            <asp:Label ID="lblRecordErr" runat="server" Text="Label"></asp:Label>
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        </div>
                    </asp:PlaceHolder>
                </div>

                <div id="Agenda">
                    <div class="row">
                        <div class="col-xs-12 col-md-12">
                            <asp:Table runat="server" ID="AgendaTable" Visible="false"></asp:Table>
                        </div>
                    </div>
                </div>

                <!--Create Customer Visit Error Message-->
                <div class="container row" runat="server">
                    <asp:PlaceHolder ID="phVisitErr" runat="server" Visible="false">
                        <div class="col-sm-12 col-md-12 alert alert-danger alert-dismissible">
                            <asp:Label ID="lblVisitErr" runat="server" Text="Label"></asp:Label>
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
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:PlaceHolder>


                <div class="container row" runat="server">
                    <asp:PlaceHolder ID="noBookingsPH" runat="server" Visible="false">
                        <div class="col-sm-12 col-md-12 alert alert-primary alert-dismissible">
                            <asp:Label ID="lblNoBookings" runat="server" Text="Label"></asp:Label>
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        </div>
                    </asp:PlaceHolder>
                </div>

                <div class="jumbotron bg-dark text-white" runat="server" id="LoggedOut" visible="true">
                    <div class="row">
                        <div class="col-lg-9 col-md-12 col-sm-12">
                            <h1>Dashboard</h1>
                            <p>Please Login</p>
                        </div>
                        <div class="col-lg-3 col-md-2 col-sm-2">
                            <a class="btn btn-primary" href="../Authentication/Accounts.aspx?PreviousPage=Stylist.aspx" id="LogedOutButton">Login</a>
                        </div>
                    </div>
                </div>
                <!--Line Break-->
                <div class="container">
                    <br />
                </div>

            </div>
            <div class="col-md-2"></div>
        </div>
    </form>
</asp:Content>
