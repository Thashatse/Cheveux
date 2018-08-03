<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxStylist.Master" AutoEventWireup="true" CodeBehind="Schedule.aspx.cs" Inherits="Cheveux.Schedule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    View Schedule - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" id="frmDropdown">
    <div class="bg-secondary text-white" id="Div1">
        <!-- Top Margin & Nav Bar Back Color -->
        <br />
        <br />
    </div>
    <br />
    <div class="row">
        <div class="col-md-2 col-sm-1"></div>
        <div class="col-md-8 col-sm-10">
            <!-- If stylist is not logged in-->
            <asp:PlaceHolder ID="phLogIn" runat="server">
                <div class="container" runat="server" id="LoggedOut">
                    <div class="jumbotron bg-dark text-white">
                        <h1>Please Log-in to proceed</h1>
                        <button type="button" class="btn btn-default">
                            <a href="../Authentication/Accounts.aspx?PreviousPage=Schedule.aspx" id="LogedOutButton">Login</a>
                        </button>
                    </div>
                </div>
            </asp:PlaceHolder>

            <!-- If stylist is logged in-->
            <asp:PlaceHolder runat="server" ID="phMain">
                <!--jumbotron page heading-->
                <div class="jumbotron bg-dark text-white" runat="server" id="LoggedIn">
                    <asp:Label ID="Header" runat="server" Text="View Schedule" Font-Bold="true" Font-Size="Large"></asp:Label>
                    <!--Line Break-->
                    <div class="container">
                        <br />
                    </div>
                </div>

                <!-- Line Break -->
                <br />
            
                <!-- Past/Upcoming Dropdownlist -->
                <div id="viewDrpDown" class="container row">
                    <div class="col-xs-12 col-md-12">
                        <asp:DropDownList ID="drpViewAppt" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle">
                            <asp:ListItem Text="Past Bookings" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Upcoming Bookings" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <br />
                <!--Past Bookings -->
                <asp:PlaceHolder ID="phPast" runat="server" Visible="false">
                    <asp:Table ID="tblPast" runat="server"></asp:Table>
                </asp:PlaceHolder>

                <!--Upcoming Bookings -->
                <asp:PlaceHolder ID="phUpcoming" runat="server" Visible="false">
                    <asp:Table ID="tblUpcoming" runat="server"></asp:Table>
                </asp:PlaceHolder>

            </asp:PlaceHolder>

            <!--Error: If cant display the stylists schedule-->
            <asp:PlaceHolder ID="phScheduleErr" runat="server" Visible="false">
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
            <!--err-->
            <div class="container row" runat="server">
                <asp:PlaceHolder ID="noBookingsPH" runat="server" Visible="false">
                    <div class="col-sm-12 col-md-12 alert alert-primary alert-dismissible">
                        <asp:Label ID="lblNoBookings" runat="server" Text="Label"></asp:Label>
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    </div>
                </asp:PlaceHolder>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
