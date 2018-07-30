<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxStylist.Master" AutoEventWireup="true" CodeBehind="Stylist.aspx.cs" Inherits="Cheveux.Stylist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Stylist Dashboard - Cheveux
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

            <!--jumbotron page heading-->
            <div class="jumbotron bg-dark text-white" runat="server" id="LoggedIn" visible="false">
                <!--Date-->
                <asp:Label ID="lblDate" runat="server" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                <br />
                <!--Welcome-->
                <asp:Label ID="lblWelcome" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>

                <!--Line Break-->
                <div class="container">
                    <br />
                </div>
            </div>

            <div id="Agenda">
                <div class="row">
                    <div class="col-xs-12 col-md-12">
                        <asp:Table runat="server" ID="AgendaTable" Visible="false"></asp:Table>
                    </div>
                </div>
            </div>

            
            <div class="container row" runat="server">
                <asp:PlaceHolder ID="noBookingsPH" runat="server" Visible="false">
                        <div class="col-sm-12 col-md-12 alert alert-primary alert-dismissible">
                            <asp:Label ID="lblNoBookings" runat="server" Text="Label"></asp:Label>
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        </div>
                </asp:PlaceHolder>
            </div>

            <div class="container" runat="server" id="LoggedOut" visible="true">
                    <div class="jumbotron bg-dark text-white">
                        <h1>Please Log-in to proceed</h1>
                        <button type="button" class="btn btn-default">
                            <a href="../Authentication/Accounts.aspx?PreviousPage=Stylist.aspx" id="LogedOutButton">Login</a>
                        </button>
                    </div>
                </div>

            <!--Line Break-->
            <div class="container">
                <br />
            </div>

        </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
