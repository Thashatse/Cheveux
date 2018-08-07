<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxReceptionist.Master" AutoEventWireup="true" CodeBehind="Receptionist.aspx.cs" Inherits="Cheveux.Receptionist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Receptionist Dashboard - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .inlineBlock { display: inline-block; }
        .floatLeft { float: left; }
    </style>

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
                <!--If the user is logged in-->
                <!--jumbotron page heading-->
                <div class="jumbotron bg-dark text-white" runat="server" id="LoggedIn" visible="false">
                    <!--Date-->
                    <asp:Label ID="lblDate" runat="server" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                    <br />
                    <!--Welcome-->
                    <asp:Label ID="Welcome" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
                    <!--Line Break-->
                    <div class="container">
                        <br />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-9 col-sm-12">

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
                            <asp:DropDownList ID="drpEmpNames" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            </asp:DropDownList>
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


                <div class="container" runat="server" id="LoggedOut" visible="true">
                    <div class="jumbotron bg-dark text-white">
                        <h1>Please Log-in to proceed</h1>
                        <button type="button" class="btn btn-default">
                            <a href="../Authentication/Accounts.aspx?PreviousPage=Receptionist.aspx" id="LogedOutButton">Login</a>
                        </button>
                    </div>
                </div>

                </div>
                    <div class="col-3" style="border:solid #F05F40 2px;" runat="server" id="alertsContainer" visible="false">
                <!--Alerts-->
                        <div class="container" >
                            <!-- Alert Table -->
                            <h1>Alerts </h1>
                            <asp:Table ID="tblAlerts" runat="server">
                            </asp:Table>
                        </div>
</div>
                    </div>
            </form>
        </div>
        <div class="col-md-1 col-sm-1"></div>
    </div>
</asp:Content>
