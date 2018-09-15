<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxReceptionist.Master" AutoEventWireup="true" CodeBehind="Appointments.aspx.cs" Inherits="Cheveux.Appointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    View Schedules - Cheveux
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
            <div class="col-1"></div>
            <div class="col-10">
                <!-- If user is not logged in-->
                <asp:PlaceHolder ID="phLogin" runat="server">
                    <div class="jumbotron bg-dark text-white" runat="server" id="LoggedOut">
                        <div class="row">
                            <div class="col-lg-9 col-md-12 col-sm-12">
                                <h1>View Schedules</h1>
                                <p>Please Login</p>
                            </div>
                            <div class="col-lg-3 col-md-2 col-sm-2">
                                <a class="btn btn-primary" href="../Authentication/Accounts.aspx?PreviousPage=Diary.aspx" id="LogedOutButton">Login</a>
                            </div>
                        </div>
                    </div>
                </asp:PlaceHolder>

                <!-- If user is logged in -->
                <asp:PlaceHolder ID="phMain" runat="server">
                    <div runat="server" id="dontPrint">
                        <!--jumbotron page heading-->
                        <div class="jumbotron bg-dark text-white" runat="server" id="LoggedIn">
                            <asp:Label ID="Header" runat="server" Text="View Schedule" Font-Bold="true" Font-Size="XX-Large"></asp:Label>

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
                                    <asp:DropDownList ID="drpViewAppt" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle"
                                        OnSelectedIndexChanged="drpViewAppt_SelectedIndexChanged">
                                        <asp:ListItem Text="Upcoming Bookings" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Past Bookings" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </asp:PlaceHolder>

                            <!-- For Stylist/For All Stylist Dropdown -->
                            <asp:PlaceHolder ID="phStylists" runat="server">
                                <div class="col-xs-12 col-md-4">
                                    <asp:DropDownList ID="empSelectionType" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle"
                                        OnSelectedIndexChanged="empSelectionType_Changed">
                                        <asp:ListItem Text="All Stylists" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Specific Stylist" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </asp:PlaceHolder>

                            <!--Stylists Names -->
                            <asp:PlaceHolder ID="phNames" runat="server" Visible="false">
                                <div class="col-xs-12 col-md-4">
                                    <asp:DropDownList ID="drpStylistNames" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle">
                                    </asp:DropDownList>
                                </div>
                            </asp:PlaceHolder>
                        </div>

                        <br />

                        <!-- Calendars -->
                        <asp:PlaceHolder ID="phCalendars" runat="server" Visible="false">

                            <br />

                            <div class="row">
                                <div class="col-xs-12 col-md-6">
                                    <asp:RadioButtonList ID="rdoDate" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" RepeatLayout="flow">
                                        <asp:ListItem Text="All &nbsp;" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Today &nbsp;" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Specific Day &nbsp;" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Date Range &nbsp;" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>

                            <!--for general errors or telling user what must happen next-->
                            <div class="container row" runat="server">
                                <asp:PlaceHolder ID="phBookingsErr" runat="server" Visible="false">
                                    <div class="col-sm-12 col-md-12 alert alert-primary alert-dismissible">
                                        <asp:Label ID="lblBookingsErr" runat="server" Text="Label"></asp:Label>
                                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                    </div>
                                </asp:PlaceHolder>
                            </div>
                            <br />

                            <!--Dropdown to pick year/month-->
                            <asp:PlaceHolder ID="phMY" runat="server">
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <asp:Label ID="lblStartM" runat="server" Text="Start Month" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="drpStartMonth" Visible="false" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle"
                                            OnSelectedIndexChanged="drpStartMonth_SelectedIndexChanged">
                                            <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Febuary" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-12 col-md-4 col-lg-4">
                                        <asp:Label ID="lblEndM" runat="server" Text="End Month" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="drpEndMonth" runat="server" Visible="false" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle"
                                            OnSelectedIndexChanged="drpEndMonth_SelectedIndexChanged">
                                            <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Febuary" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </asp:PlaceHolder>

                            <br />

                            <!-- Specific Day -->
                            <asp:PlaceHolder ID="phDay" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-xs-12 col-md-4 col-lg-4">
                                        <asp:Calendar ID="calDay" runat="server" Height="100" Width="200" OnSelectionChanged="calDay_SelectionChanged" OnDayRender="scheduleCalender_DayRender"></asp:Calendar>
                                        <asp:Label ID="lblDay" runat="server" Text="Label1" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </asp:PlaceHolder>

                            <!--Date Range -->
                            <asp:PlaceHolder ID="phDateRange" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-xs-12 col-md-4 col-lg-4">
                                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date" Visible="true"></asp:Label>
                                    </div>
                                    <div class="col-xs-12 col-md-4 col-lg-4">
                                        <asp:Label ID="lblEndDate" runat="server" Text="End Date" Visible="true"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12 col-md-4 col-lg-4">
                                        <asp:Calendar ID="calStart" runat="server" Height="100" Width="200" OnSelectionChanged="calStart_SelectionChanged" OnDayRender="scheduleCalender_DayRender"></asp:Calendar>
                                    </div>
                                    <div class="col-xs-12 col-md-4 col-lg-4">
                                        <asp:Calendar ID="calEnd" runat="server" Height="100" Width="200" OnSelectionChanged="calEnd_SelectionChanged" OnDayRender="scheduleCalender_DayRender"></asp:Calendar>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12 col-md-4 col-lg-4">
                                        <asp:Label ID="lblStart" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-xs-12 col-md-4 col-lg-4">
                                        <asp:Label ID="lblEnd" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-md-4 col-lg-6">
                                        <asp:Label ID="valDate" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </asp:PlaceHolder>
                        </asp:PlaceHolder>

                        <br />

                        <!--Sorting-->
                        <asp:PlaceHolder ID="phSorting" runat="server">
                            <div class="row">
                                <div class="col-xs-12 col-md-4 col-lg-4">
                                    Direction : 
                                <asp:DropDownList ID="drpSortDir" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle">
                                    <asp:ListItem Text="Descending" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Ascending" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                                </div>
                                <div class="col-xs-12 col-md-4 col-lg-4">
                                    Sort By : 
                                <asp:DropDownList ID="drpSortBy" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle">
                                    <asp:ListItem Text="Date" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Stylist" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                                </div>
                                <div class="col-xs-12 col-md-4 col-lg-4">
                                    <asp:Button ID="btnPrint" runat="server" Text="Print Friendly Version" OnClick="btnPrint_Click" Visible="false" CssClass="btn btn-secondary" />
                                </div>
                            </div>
                        </asp:PlaceHolder>

                        <br />

                    </div>

                    <div runat="server" id="divPrintHeader" visible="false">
                        <div class="row">
                            <div class="col-md-12">
                                <!-- Logo -->
                                <asp:Table ID="tblLogo" runat="server"></asp:Table>
                                <!-- Report Summary -->
                                <asp:Table ID="tblSum" runat="server"></asp:Table>
                            </div>
                        </div>
                    </div>

                    <!-- Table displaying stylist bookings -->
                    <asp:PlaceHolder ID="phTable" runat="server">
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
            </div>
            <div class="col-1"></div>
        </div>
    </form>
</asp:Content>
