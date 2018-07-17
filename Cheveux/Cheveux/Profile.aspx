<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Cheveux.Profile2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Profile - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <!--CSS-->
    <link rel="stylesheet" type="text/css" href="/CSS/Cheveux.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- if the user is loged Out -->
    <div class="container" runat="server" id="JumbotronLogedOut">
        <div class="jumbotron">
            <h1>Profile</h1>
            <p>Please log-in</p>
            <button type="button" class="btn btn-default"><a href="Authentication/Accounts.aspx?PreviousPage=Profile.aspx" id="LogedOut">Login / Sign Up</a></button>
        </div>
    </div>
    <!-- if the user is loged in -->
    <div class="container" runat="server" id="JumbotronLogedIn" visible="false">
        <!--Tabs-->
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="jumbotron">
                        <asp:Image ID="profileImage" runat="server" />
                        &nbsp;
                        <asp:Label ID="profileLable" runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                    </div>
                    <asp:Table ID="profileTable" runat="server"></asp:Table>
                    <div class="container" runat="server" id="Edit" visible="false">
                        <form id="formEditProfile" runat="server">
                            <asp:Table ID="editGoogleProfileTable" runat="server" Visible ="false">
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
                                        <asp:Label ID="LUserNameExistsGoogleAccount" runat="server" Text="Label" ForeColor="Red" Visible ="false"></asp:Label></asp:TableCell>
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
                                    <a href = '#'>Delete Profile   </a>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Right">
                                        <a href='Profile.aspx'>Cancel   </a>

                                        <asp:Button ID="btnSaveGoogle" runat="server" Text="Save" class="btn btn-default" OnClick="btnSaveGoogle_Click" />
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
                                        <asp:Label ID="LEmailExists" runat="server" Text="Label" ForeColor="Red" Visible ="false"></asp:Label>
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
                                        <asp:Label ID="LUsernmaeExistsEmailAccount" runat="server" Text="Label" ForeColor="Red" Visible ="false"></asp:Label>
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
                                    <a href = '#'>Delete Profile   </a>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Right">
                                        <a href='Profile.aspx'>Cancel   </a>

                                        <asp:Button ID="btnSaveEmail" runat="server" Text="Save" class="btn btn-default" OnClick="commitEdit"  />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </form>
                    </div>
                </div>
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
            <asp:Button ID="no" runat="server" Text="No" class="btn btn-default" OnClick="no_Click"/>
            <asp:Button ID="yes" runat="server" Text="Yes" class="btn btn-default" OnClick="commitEdit" />
            <asp:Button ID="OK" runat="server" Text="OK" class="btn btn-default" OnClick="OK_Click" Visible="false" />
        </div>
    </div>
</asp:Content>
