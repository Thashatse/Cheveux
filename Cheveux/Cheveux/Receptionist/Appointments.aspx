<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxReceptionist.Master" AutoEventWireup="true" CodeBehind="Appointments.aspx.cs" Inherits="Cheveux.Appointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Appointments - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="bg-secondary text-white" id="Div1">
            <!-- Top Margin & Nav Bar Back Color -->
            <br />
            <br />
        </div>
        <br />

        <div class="row">
            <div class="col-md-1 col-sm-1"></div>
            <div class="col-md-10 col-sm-10">
                <!-- If stylist is not logged in-->
                <asp:PlaceHolder ID="phLogin" runat="server">
                    <div class="container" runat="server" id="LoggedOut">
                        <div class="jumbotron bg-dark text-white">
                            <h1>Please Log-in to proceed</h1>
                            <button type="button" class="btn btn-default">
                                <a href="../Authentication/Accounts.aspx?PreviousPage=Diary.aspx" id="LogedOutButton">Login</a>
                            </button>
                        </div>
                    </div>
                </asp:PlaceHolder>

                <!-- If stylist is logged in -->
                <asp:PlaceHolder ID="phMain" runat="server">

                    <!--jumbotron page heading-->
                    <div class="jumbotron bg-dark text-white" runat="server" id="LoggedIn">
                        <asp:Label ID="Header" runat="server" Text="View Schedule" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                        <!--Line Break-->
                        <div class="container">
                            <br />
                        </div>
                    </div>

                    <!-- Line Break -->
                    <br />

                    <div id="dropdownSelections" class="container row">
                        <!-- Past/Upcoming Dropdownlist -->
                        <asp:PlaceHolder ID="phWhen" runat="server">
                            <div class="col-xs-12 col-md-4">
                                <asp:DropDownList ID="drpViewAppt" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle">
                                    <asp:ListItem Text="---Select Period---" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Upcoming Bookings" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Past Bookings" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </asp:PlaceHolder>

                        <!-- For Stylist/For All Stylist Dropdown -->
                        <asp:PlaceHolder ID="phStylists" runat="server">
                            <div class="col-xs-12 col-md-4">
                                <asp:DropDownList ID="empSelectionType" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle">
                                    <asp:ListItem Text="---Group By---" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="All" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Stylist" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </asp:PlaceHolder>

                        <!--Stylists Names -->
                        <asp:PlaceHolder ID="phNames" runat="server" Visible="false">
                            <div class="col-xs-12 col-md-4">
                                <asp:DropDownList ID="drpStylistNames" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle">
                                    <asp:ListItem Text="---Choose Stylist---" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </asp:PlaceHolder>
                    </div>

                    <br />

                    <!-- Calendars -->
                    <asp:PlaceHolder ID="phCalendars" runat="server" Visible="false">

                        <br />

                        <div class="row">
                            <div class="col-xs-12 col-md-4">
                                <asp:RadioButtonList ID="rdoDate" runat="server" AutoPostBack="true" >
                                    <asp:ListItem Text="Today" Value="0" ></asp:ListItem>
                                    <asp:ListItem Text="Specific Day" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Date Range" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>

                        <br />

                        <!-- Specific Day -->
                        <asp:PlaceHolder ID="phDay" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-xs-12 col-md-4 col-lg-4">
                                    <asp:Calendar ID="calDay" runat="server" Height="100" Width="200" ></asp:Calendar>
                                </div>
                            </div>
                        </asp:PlaceHolder>

                        <br />

                        <!--Date Range -->
                        <asp:PlaceHolder ID="phDateRange" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-xs-12 col-md-4 col-lg-4">
                                    <asp:Label ID="lblStartDate" runat="server" Text="Start Date" Visible="true" ></asp:Label>
                                </div>
                                <div class="col-xs-12 col-md-4 col-lg-4">
                                    <asp:Label ID="lblEndDate" runat="server" Text="End Date" Visible="true"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-md-4 col-lg-4">
                                    <asp:Calendar ID="calStart" runat="server" Height="100" Width="200" ></asp:Calendar>
                                </div>
                                <div class="col-xs-12 col-md-4 col-lg-4">
                                    <asp:Calendar ID="calEnd" runat="server" Height="100" Width="200" ></asp:Calendar>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-md-4 col-lg-4">
                                    <asp:Label ID="lblDate1" runat="server" Text="" Visible="true"></asp:Label>
                                </div>
                                <div class="col-xs-12 col-md-4 col-lg-4">
                                    <asp:Label ID="lblDate2" runat="server" Text="" Visible="true"></asp:Label>
                                </div>
                            </div>
                        </asp:PlaceHolder>
                    </asp:PlaceHolder>

                    <br />

                    <!-- Table displaying stylist bookings -->
                    <asp:PlaceHolder ID="phTable" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-sm-12 col-md-10 col-lg-10">
                                <asp:Table ID="tblSchedule" runat="server"></asp:Table>
                            </div>
                        </div>
                    </asp:PlaceHolder>

                    <br />
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
                    <asp:PlaceHolder ID="phBookingsErr" runat="server" Visible="false">
                        <div class="col-sm-12 col-md-12 alert alert-primary alert-dismissible">
                            <asp:Label ID="lblBookingsErr" runat="server" Text="Label"></asp:Label>
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        </div>
                    </asp:PlaceHolder>
                </div>


            </div>
        </div>
    </form>
</asp:Content>
