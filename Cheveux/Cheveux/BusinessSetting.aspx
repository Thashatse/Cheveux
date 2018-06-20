<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="BusinessSetting.aspx.cs" Inherits="Cheveux.BusinessSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Business Settings - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            margin-top: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" runat="server" id="BussinessSettings">
        <!-- if the user is loged In -->
        <div class="container" runat="server" id="LogedIn" visible="false">
            <div class="row">
                <div class="col-md-12">
                    <!--Heading-->
                    <asp:Label runat="server" ID="PageHeading"> <h2>Business Setting</h2> </asp:Label>
                    <!-- Line Breake --> 
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <form id="frmBussinessSettings" runat="server">
                        <asp:Table ID="tblBussinesSettings" runat="server">
                            <asp:TableRow>
                                <asp:TableHeaderCell>
                                <!-- Vat Rate -->
                                VAT Rate (%):
                                </asp:TableHeaderCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="vatRate" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Button ID="btnEnitvatRate" runat="server" Text="Edit" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableHeaderCell>
                                <!-- Vat Registration Number -->
                                VAT Reg No.:
                                </asp:TableHeaderCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="vatRegNo" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Button ID="btnEnitvatRegNo" runat="server" Text="Edit" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableHeaderCell>
                                <!-- Address -->
                                Address: 
                                </asp:TableHeaderCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="addLineOne" runat="server"></asp:TextBox>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableHeaderCell><!-- Address line 2 --></asp:TableHeaderCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="addLineTwo" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Button ID="btnEnitadd" runat="server" Text="Edit" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableHeaderCell>
                                <!-- Phone Number -->
                                Phone Number
                                </asp:TableHeaderCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="phoneNumber" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Button ID="btnEnitPhoneNum" runat="server" Text="Edit" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableHeaderCell>
                        <!-- Week Day Hours -->
                                Weekday Hours:
                                </asp:TableHeaderCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="wDStart" runat="server"></asp:TextBox>
                                    - 
                                <asp:TextBox ID="wDEnd" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Button ID="btnEnitWDHours" runat="server" Text="Edit" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableHeaderCell>
                                <!-- Weekend Hours -->
                                Weekend Hours:
                                </asp:TableHeaderCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="wEStart" runat="server"></asp:TextBox>
                                    - 
                                <asp:TextBox ID="wEEnd" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Button ID="btnEnitWEHours" runat="server" Text="Edit" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableHeaderCell>
                                <!-- Public Holiday Hours -->
                                Week Holiday Hours:
                                </asp:TableHeaderCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="pHStrat" runat="server"></asp:TextBox>
                                    - 
                                <asp:TextBox ID="pHEnd" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Button ID="btnEnitPHHours" runat="server" Text="Edit" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableHeaderCell>
                                <!-- Logo -->
                                Logo:
                                </asp:TableHeaderCell>
                                <asp:TableCell>
                                    <a href='/IMG_0715.png' target="_blank"><img src="/IMG_0715.png" alt="logo" width="300" height="300" /></a>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Button ID="Button1" runat="server" Text="Edit" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </form>
                </div>
            </div>
        </div>
        <!-- if the user is loged Out -->
        <div class="container" runat="server" id="LogedOut">
            <div class="jumbotron">
                <p>Please log-in</p>
                <button type="button" class="btn btn-default">
                    <a href="Accounts.aspx?PreviousPage=BusinessSetting.aspx" id="LogedOutButton">Login</a>
                </button>
            </div>
        </div>
    </div>
</asp:Content>
