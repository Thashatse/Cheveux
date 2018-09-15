<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="Service.aspx.cs" Inherits="Cheveux.Manager.Service" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Manage Services - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
            <!-- if the user is loged In -->
            <div runat="server" id="LogedIn" visible="false">
                <div class="row">
                    <div class="col-md-12">

                        <!-- Jumbo Tron -->
                        <div class="jumbotron  bg-dark text-white">
                            <h1>Manage Services</h1>
                            <!-- line Break -->
                            <br />
                            <form runat="server">
                                <div class="row">
                                    <div class="col-12">
                                        <!-- View By search tearm -->
                                        <p>View Products By Search Term: </p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-5">
                                        <asp:TextBox ID="txtProductSearchTerm" runat="server" class="form-control" AutoPostBack="true"
                                            OnTextChanged="Page_Load"></asp:TextBox>
                                    </div>
                                    <div class="col-2">
                                        <asp:Button ID="btnProductSearch" runat="server" Text="Search" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-10">
                        <asp:Label ID="productJumbotronLable" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <!--add Service btn -->
                        <a class='btn btn-primary' href='../Manager/AddService.aspx'>New Service </a>
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
            <div runat="server" id="LogedOut">
                <div class="jumbotron  bg-dark text-white">
                    <div class="row">
                        <div class="col-10">
                            <h1>Manage Services</h1>
                            <p>Please log-in to Manage Services</p>
                        </div>
                        <div class="col-2">
                            <a class="btn btn-primary" href="../Authentication/Accounts.aspx?PreviousPage=Service.aspx" id="LogedOutButton">Login</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-1"></div>
        </div>
</asp:Content>
