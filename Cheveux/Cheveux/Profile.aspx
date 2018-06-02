<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Cheveux.Profile2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Profile - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- if the user is loged Out -->
    <div class="container" runat="server" id="JumbotronLogedOut">
        <div class="jumbotron">
            <h1>Profile</h1>
            <p>Please log-in</p>
            <button type="button" class="btn btn-default"><a href="Accounts.aspx?PreviousPage=Profile.aspx" id="LogedOut">Login / Sign Up</a></button>
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
                        </div>
            </div>
        </div>
    </div>
</asp:Content>
