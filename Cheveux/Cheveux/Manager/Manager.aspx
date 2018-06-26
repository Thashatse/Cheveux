<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="Manager.aspx.cs" Inherits="Cheveux.Manager.Manager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Dashboard - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container" runat="server" id="managerDashboardPage">
        <!-- if the user is loged In -->
        <div class="container" runat="server" id="LogedIn" visible="false">
            Test
        </div>
        <!-- if the user is loged Out -->
        <div class="container" runat="server" id="LogedOut">
            <div class="jumbotron">
                <h1>Dashboard</h1>
                <p>Please log-in to view your Dashboard</p>
                <button type="button" class="btn btn-default">
                    <a href="../Authentication/Accounts.aspx?PreviousPage=Manager.aspx" id="LogedOutButton">Login</a>
                </button>
            </div>
        </div>
    </div>
</asp:Content>
