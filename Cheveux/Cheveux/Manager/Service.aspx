<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="Service.aspx.cs" Inherits="Cheveux.Manager.Service" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Manage Services - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container" runat="server" id="ReportsPage">
        <!-- if the user is loged In -->
        <div class="container" runat="server" id="LogedIn" visible="false">
            <div class="row">
                <div class="col-md-12">

                    <!-- Jumbo Tron -->
                    <div class="jumbotron">
                        <h1>Manage Services</h1>
                        <!-- line Break -->
                        <br />
                        <form runat="server">

                            <!-- View By search tearm -->
                            <p>View Products By Search Term: </p>
                            <asp:TextBox ID="txtProductSearchTerm" runat="server"></asp:TextBox>
                            <asp:Button ID="btnProductSearch" runat="server" Text="Search" />

                        </form>
                        <!-- line Break -->
                        <br />
                        <!--Help-->
                        <a href="../Help/CheveuxHelpCenter.aspx#ManageServices" target="_blank" title="How To Manage Products">
                            <span class="glyphicon">&#63; Help</span></a>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-10">
                    <asp:Label ID="productJumbotronLable" runat="server"></asp:Label>
                </div>
                <div class="col-md-2">
                    <!--add Service btn -->
                    <button type='button' class='btn btn-default'><a href='#'>New Service </a></button>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <!-- List Service Table -->
                    <asp:Table ID="tblProductTable" runat="server">
                    </asp:Table>
                </div>
            </div>
        </div>
        <!-- if the user is loged Out -->
        <div class="container" runat="server" id="LogedOut">
            <div class="jumbotron">
                <h1>Manage Services</h1>
                <p>Please log-in to Manage Services</p>
                <button type="button" class="btn btn-default">
                    <a href="../Authentication/Accounts.aspx?PreviousPage=Service.aspx" id="LogedOutButton">Login</a>
                </button>
            </div>
        </div>
    </div>
</asp:Content>
