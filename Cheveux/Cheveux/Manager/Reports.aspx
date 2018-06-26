<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Cheveux.Manager.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Reports - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" runat="server" id="ReportsPage">
        <!-- if the user is loged In -->
        <div class="container" runat="server" id="LogedIn" visible="false">
            Test
        </div>
        <!-- if the user is loged Out -->
        <div class="container" runat="server" id="LogedOut">
            <div class="jumbotron">
                <h1>Reports</h1>
                <p>Please log-in to view Reports</p>
                <button type="button" class="btn btn-default">
                    <a href="../Authentication/Accounts.aspx?PreviousPage=Reports.aspx" id="LogedOutButton">Login</a>
                </button>
            </div>
        </div>
    </div>
</asp:Content>
