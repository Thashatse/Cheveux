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

                        <!--Placeholder: Manager View Emps Schedule -->
                        <asp:PlaceHolder ID="managerview" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-8">
                                    <asp:Label ID="EmployeeHead" runat="server" Text="" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-6">
                                    <asp:Table ID="tblEmployee" runat="server"></asp:Table>
                                </div>
                            </div>
                        </asp:PlaceHolder>

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
                        <div class="col-xs-12 col-md-10">
                            <asp:DropDownList ID="drpViewAppt" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle">
                                <asp:ListItem Text="Upcoming Bookings" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Past Bookings" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-12 col-md-2">
                            <!-- Manage Schedule -->
                            <a class='btn btn-secondary' href='../Stylist/Schedule.aspx?Action=Manage'>Manage Schedule </a>
                        </div>
                    </div>
                    <br />
                    <asp:PlaceHolder ID="phCheckBox" runat="server">
                        <div class="container row">
                            <div class="col-sm-2">
                                <asp:CheckBox ID="bxAll" runat="server" Text="All" AutoPostBack="true" Checked="true" />
                                <p style="font-size: 12px;"><i>Note:Uncheck for date range</i></p>
                            </div>
                        </div>
                    </asp:PlaceHolder>

                    <!-- Calendar -->
                    <asp:PlaceHolder ID="phCal" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-lg-6 text-center">
                                <asp:Label ID="lblStartDate" runat="server" Text="Start Date" Visible="true" CssClass="inline"></asp:Label>
                            </div>
                            <div class="col-lg-6 text-center">
                                <asp:Label ID="lblEndDate" runat="server" Text="End Date" Visible="true" CssClass="inline"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:Calendar runat="server" ID="calStart" Width="350" Height="200" CssClass="inline" OnSelectionChanged="calStart_SelectionChanged" OnDayRender="cal_DayRender"></asp:Calendar>
                            </div>
                            <div class="col-lg-6">
                                <asp:Calendar runat="server" ID="calEnd" Width="350" Height="200" CssClass="inline" OnSelectionChanged="calEnd_SelectionChanged" OnDayRender="cal_DayRender"></asp:Calendar>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6 text-center">
                                <asp:Label ID="lblDate1" runat="server" Text="" Visible="true" CssClass="inline"></asp:Label>
                            </div>
                            <div class="col-lg-6 text-center">
                                <asp:Label ID="lblDate2" runat="server" Text="" Visible="true" CssClass="inline"></asp:Label>
                            </div>
                        </div>
                    </asp:PlaceHolder>

                    <br />

                    <!--Sorting-->
                    <asp:PlaceHolder ID="phSorting" runat="server">
                        <div class="row">
                            <div class="col-xs-12 col-md-4 col-lg-4">
                                <asp:DropDownList ID="drpSortDir" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle">
                                    <asp:ListItem Text="Ascending" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Descending" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-12 col-md-4 col-lg-4">
                                <asp:DropDownList ID="drpSortBy" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle">
                                    <asp:ListItem Text="Date" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Stylist" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </asp:PlaceHolder>

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
