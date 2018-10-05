<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Cheveux.Profile2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Profile - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <!--CSS-->
    <link rel="stylesheet" type="text/css" href="/CSS/Cheveux.css">

    <style>
        .starRating {
            width: 50px;
            height: 50px;
            cursor: pointer;
            background-repeat: no-repeat;
            display: block;
        }

        .filledStar {
            background-image: url("../Images/filledStar.gif");
        }

        .waitingStar {
            background-image: url("../Images/waitingStar.gif");
        }

        .emptyStar {
            background-image: url("../Images/star.gif");
        }
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
        <div class="col-1"></div>
        <div class="col-10">
            <form id="formProfile" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row">
                    <div class="col-lg-9 col-md-12 col-sm-12">
                        <div id="divTabs" runat="server" visible="false">
                            <!--Tabs-->
                            <ul class="nav nav-tabs" role="tablist">
                                <li>
                                    <asp:Button runat="server" Text="Upcoming Booking(s)" ID="btnViewUpBook" OnClick="btnViewUpBook_Click" class="btn btn-light" />
                                </li>
                                <li>
                                    <asp:Button runat="server" Text="Past Booking(s)" ID="btnViewPasBook" OnClick="btnViewPastBook_Click" class="btn btn-light" />
                                </li>
                                <li>
                                    <asp:Button runat="server" Text="Profile" ID="btnViewPrfile" OnClick="btnViewProfile_Click" class="btn btn-light" />
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-3">
                        <div runat="server" id="LogOutBTN" visible="false">
                            <a class="btn btn-secondary nav-link" href="/Authentication/Accounts.aspx?action=Logout" style="text-decoration: none;">Log Out</a>
                        </div>
                    </div>
                </div>

                <!-- Line Break -->
                <br />

                <div runat="server" visible="false" id="UpcomingService">
                    <div class="jumbotron bg-dark text-white">
                        <div class="row">
                            <div class="col-lg-9 col-md-12 col-sm-12">
                                <h1>Upcoming Bookings</h1>
                            </div>
                            <div class="col-lg-3 col-md-2 col-sm-2">
                                <a class="btn btn-primary js-scroll-trigger" href="MakeABooking.aspx">Make a booking</a>
                            </div>
                        </div>
                    </div>
                    <!-- Line Break -->
                    <br />
                    <!-- Display all upocoming Bookings for the client -->
                    <div class="row">
                        <div class="col-xs-12 col-md-12">
                            <asp:Label runat="server" ID="upcomingBookingsLable"></asp:Label>
                            <!-- Line Break -->
                            <br />
                            <asp:Table ID="upcomingBookings" runat="server"></asp:Table>
                        </div>
                    </div>
                </div>

                <div runat="server" visible="false" id="PastService">
                    <div class="jumbotron bg-dark text-white">
                        <div class="row">
                            <div class="col-lg-9 col-md-12 col-sm-12">
                                <h1>Past Bookings</h1>
                            </div>
                            <div class="col-lg-3 col-md-2 col-sm-2">
                                <a class="btn btn-primary js-scroll-trigger" href="MakeABooking.aspx">Make a booking</a>
                            </div>
                        </div>
                    </div>
                    <br />
                    <!-- Display all Past Bookings for the client -->
                    <div class="row">
                        <div class="col-xs-12 col-md-12">
                            <asp:Label runat="server" ID="pastBookingsLable"></asp:Label>
                            <!-- Line Break -->
                            <br />
                            <asp:Table ID="pastBookings" runat="server"></asp:Table>
                        </div>
                    </div>
                </div>

                <div runat="server" visible="false" id="divProfile">
                    <!-- if the user is loged in -->
                    <div class="jumbotron bg-dark text-white" id="divProfileHeader" runat="server">
                        <asp:Image ID="profileImage" runat="server" Height="100" Width="100" />
                        &nbsp;
                        <asp:Label ID="profileLable" runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                        <br />
                        <br />
                        <asp:Panel ID="ratingsPanel" runat="server" Visible="false">
                        </asp:Panel>
                    </div>


                    <asp:Table ID="profileTable" runat="server"></asp:Table>
                    <div runat="server" id="Edit" visible="false">

                        <asp:Table ID="editGoogleProfileTable" runat="server" Visible="false">
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow Height="50">
                                <asp:TableCell ColumnSpan="2"></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell>
                                    <!--Username-->
                                    <asp:TextBox ID="userName" runat="server" placeholder="Placecholder" OnTextChanged="userName_TextChanged"> </asp:TextBox>
                                    <!--Help-->
                                    <a href="/Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank"
                                        title="This is used to identify you on the Cheveux platform">
                                        <span class="glyphicon">&#63;</span>
                                    </a>
                                    <!--userName Validation-->
                                    <asp:Label ID="LUserNameExistsGoogleAccount" runat="server" Text="Label" ForeColor="Red" Visible="false"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell>
                                    <!--Phone Number-->
                                    <asp:TextBox ID="contactNumber" runat="server" placeholder="041 243 8389"></asp:TextBox>
                                    <!--Help-->
                                    <a href="/Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank"
                                        title="10-digit RSA Cellphone number">
                                        <span class="glyphicon">&#63;</span>
                                    </a>
                                    <!--contactNumber Validation-->
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ControlToValidate="contactNumber" ErrorMessage="Please enter a valid 10-digit RSA Cellphone number" ForeColor="Red" ValidationExpression="^0[0-9]{9}$"></asp:RegularExpressionValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell ColumnSpan="2"></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell>
                                    <!--Bio-->
                                    <textarea runat="server" id="txtBio" cols="45" rows="5" placeholder="Placecholder"></textarea>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow Height="50">
                                <asp:TableCell>
                                    <a href = '?Action=Delete'>Delete Profile   </a>
                                </asp:TableCell>
                                <asp:TableCell HorizontalAlign="Right">
                                    <a runat="server" id="aGoogleBioHelp" href='Profile.aspx'>Cancel   </a>

                                    <asp:Button ID="btnSaveGoogle" runat="server" Text="Save" class="btn btn-primary" OnClick="commitEdit" UseSubmitBehavior="false" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>

                        <asp:Table ID="editEmailProfileTable" runat="server" Visible="false">
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell>
                                    <!--Name-->
                                    <asp:TextBox ID="txtName" runat="server" placeholder="First Name"></asp:TextBox>
                                    &ensp;&ensp;<asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name"></asp:TextBox>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell>
                                    <!--Email-->
                                    <asp:TextBox ID="txtEmailAddress" runat="server" placeholder="Email" OnTextChanged="txtEmailAddress_TextChanged"></asp:TextBox>
                                    <!--Email Validation-->
                                    <asp:Label ID="LEmailExists" runat="server" Text="Label" ForeColor="Red" Visible="false"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell>
                                    <!--Username-->
                                    <asp:TextBox ID="txtUsername" runat="server" placeholder="Placecholder" OnTextChanged="userName_TextChanged"></asp:TextBox>
                                    <!--Help-->
                                    <a href="/Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank"
                                        title="This is used to identify you on the Cheveux platform">
                                        <span class="glyphicon">&#63;</span>
                                    </a>
                                    <!--userName Validation-->
                                    <asp:Label ID="LUsernmaeExistsEmailAccount" runat="server" Text="Label" ForeColor="Red" Visible="false"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell>
                                    <!--Phone Number-->
                                    <asp:TextBox ID="txtContactNumber" runat="server" placeholder="041 243 8389"></asp:TextBox>
                                    <!--Help-->
                                    <a href="/Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank"
                                        title="10-digit RSA Cellphone number">
                                        <span class="glyphicon">&#63; (Optional)</span>
                                    </a>
                                    <!--contactNumber Validation-->
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatortxtContactNumber" runat="server" ControlToValidate="txtContactNumber" ErrorMessage="Please enter a valid 10-digit RSA Cellphone number" ForeColor="Red" ValidationExpression="^0[0-9]{9}$"></asp:RegularExpressionValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell ColumnSpan="2"></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="50">
                                <asp:TableCell Font-Bold="true"></asp:TableCell>
                                <asp:TableCell>
                                    <!--Bio-->
                                    <textarea runat="server" id="txtABioEmail" cols="45" rows="5" placeholder="Placecholder"></textarea>
                                    <!--Help-->
                                    <a runat="server" id="aEmailBioHelp" href="/Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank"
                                        title="Stylist Bio">
                                        <span class="glyphicon">&#63;</span>
                                    </a>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow Height="50">
                                <asp:TableCell>
                                    <a href = '?Action=Delete'>Delete Profile   </a>
                                </asp:TableCell>
                                <asp:TableCell HorizontalAlign="Right">
                                    <a href='Profile.aspx?View=Profile'>Cancel   </a>

                                    <asp:Button ID="btnSaveEmail" runat="server" Text="Save" class="btn btn-primary" OnClick="commitEdit" UseSubmitBehavior="false" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>

                    </div>

                    <div runat="server" id="divConfirm" visible="false">
                        <div class="jumbotron bg-dark text-white">
                            <asp:Label ID="confirmHeaderPlaceHolder" runat="server" Font-Size="X-Large"></asp:Label>
                            <!--Line Break-->
                            <br />
                            <asp:Label ID="confirmPlaceHolder" runat="server"></asp:Label>
                            <!--Line Break-->
                            <br />
                            <br />
                            <!-- Edit -->
                            <asp:Button ID="no" runat="server" Text="No" class="btn btn-default" OnClick="no_Click" />
                            <asp:Button ID="yes" runat="server" Text="Yes" class="btn btn-danger" OnClick="OK_Click1" />
                            <asp:Button ID="OK" runat="server" Text="OK" class="btn btn-primary" OnClick="OK_Click1" Visible="false" />
                        </div>
                    </div>
                </div>

                <!-- if the user is loged Out -->
                <div runat="server" id="JumbotronLogedOut">
                    <div class="jumbotron bg-dark text-white">
                        <div class="row">
                            <div class="col-10">
                                <h1>Profile</h1>
                                <p>Please login</p>
                            </div>
                            <div class="col-2">
                                <a class="btn btn-primary" href="Authentication/Accounts.aspx?PreviousPage=Profile.aspx" id="LogedOut">Login</a>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
        <div class="col-1"></div>
    </div>
</asp:Content>
