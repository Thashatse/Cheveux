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
        <div class="col-md-2 col-sm-1"></div>
        <div class="col-md-8 col-sm-10">
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
                        <button type='button' class='btn btn-primary'><a class='btn btn-primary' href='#'>New Service </a></button>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <!-- List Service Table -->
                        <asp:Table ID="tblProductTable" runat="server">
                        </asp:Table>
                    </div>
                </div>

                <!-- if the user is loged Out -->
                <div runat="server" id="LogedOut">
                    <div class="jumbotron  bg-dark text-white">
                        <h1>Manage Services</h1>
                        <p>Please log-in to Manage Services</p>
                        <button type="button" class="btn btn-default">
                            <a href="../Authentication/Accounts.aspx?PreviousPage=Service.aspx" id="LogedOutButton">Login</a>
                        </button>
                    </div>
                </div>
            </div>
            <div class="col-md-2 col-sm-1"></div>
        </div>
</asp:Content>
