<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Cheveux.Profile2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Profile - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <!--CSS-->
    <link rel="stylesheet" type="text/css" href="/CSS/Cheveux.css">
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


            <div class="row">
                <div class="col-lg-9 col-md-12 col-sm-12">
                    <div id="divTabs" runat="server" visible="false">
                        <!--Tabs-->
                        <ul class="nav nav-tabs" role="tablist">
                            <li><a class="btn" href="#UpcomingService" role="tab" data-toggle="tab">Upcoming Booking(s)&#09;</a></li>
                            <li><a class="btn" href="#PastService" role="tab" data-toggle="tab">Past Booking(s)&#09;</a></li>
                            <li><a class="btn" href="#Profile" role="tab" data-toggle="tab">Profile</a></li>
                        </ul>
                    </div>
                </div>
                <div class="col-3">
                    <div runat="server" id="LogOutBTN" visible="false">
                        <a class="btn btn-secondary nav-link" href="../Authentication/Accounts.aspx?action=Logout" style="text-decoration: none;">Log Out</a>
                    </div>
                </div>
            </div>

            <!-- Line Break -->
            <br />

            <!--Tabs Content-->
            <div class="tab-content">

                <div class="tab-pane" id="UpcomingService">
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
                            <asp:Table ID="upcomingBookings" runat="server"></asp:Table>
                        </div>
                    </div>
                </div>

                <div class="tab-pane" id="PastService">

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
                            <asp:Table ID="pastBookings" runat="server"></asp:Table>
                        </div>
                    </div>
                </div>

                <div class="active tab-pane" id="Profile">
                    <!-- if the user is loged Out -->
                    <div runat="server" id="JumbotronLogedOut">
                        <div class="jumbotron bg-dark text-white">
                            <h1>Profile</h1>
                            <p>Please log-in</p>
                            <a class="btn btn-primary" href="Authentication/Accounts.aspx?PreviousPage=Profile.aspx" id="LogedOut">Login</a>
                        </div>
                    </div>
                    <!-- if the user is loged in -->
                    <div runat="server" id="JumbotronLogedIn" visible="false">

                        <div class="jumbotron bg-dark text-white" id="divProfileHeader" runat="server">
                            <asp:Image ID="profileImage" runat="server" Height="100" Width="100" />
                            &nbsp;
                        <asp:Label ID="profileLable" runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                        </div>

                        <form id="formEditProfile" runat="server">
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
                                        <asp:TableCell>
                                    <a href = '?Action=Delete'>Delete Profile   </a>
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Right">
                                            <a href='Profile.aspx'>Cancel   </a>

                                            <asp:Button ID="btnSaveGoogle" runat="server" Text="Save" class="btn btn-primary" OnClick="btnSaveGoogle_Click" />
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
                                        <asp:TableCell>
                                    <a href = '?Action=Delete'>Delete Profile   </a>
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Right">
                                            <a href='Profile.aspx'>Cancel   </a>

                                            <asp:Button ID="btnSaveEmail" runat="server" Text="Save" class="btn btn-primary" OnClick="commitEdit" />
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

                        </form>

                    </div>
                </div>

            </div>
        </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
