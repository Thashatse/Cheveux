<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="Service.aspx.cs" Inherits="Cheveux.Manager.Service" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Manage Services - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
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
                        <div class="col-lg-12 col-md-12 col-sm-12">
                            <div id="divTabs" runat="server">
                                <!--Tabs-->
                                <ul class="nav nav-tabs" role="tablist">
                                    <li>
                                        <asp:Button ID="btnViewAllServices" runat="server" Text="Sercvices" class="btn btn-light" OnClick="btnViewAllServices_Click" /></li>
                                    <li>
                                        <asp:Button ID="btnViewServiceTypes" runat="server" Text="Service Types" class="btn btn-light" OnClick="btnViewServiceTypes_Click" /></li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <!-- line Break -->
                    <br />

                    <div runat="server" id="divAllServices">
                        <div class="row">
                            <div class="col-md-12">
                                <!-- Jumbo Tron -->
                                <div class="jumbotron  bg-dark text-white">
                                    <h1>Manage Services</h1>
                                    <!-- line Break -->
                                    <br />
                                    <div class="row">
                                        <div class="col-12">
                                            <!-- View By search tearm -->
                                            <p>View Products By Search Term: </p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-5">
                                            <asp:TextBox ID="txtProductSearchTerm" runat="server" class="form-control" AutoPostBack="true"
                                                OnTextChanged="btnViewAllServices_Click"></asp:TextBox>
                                        </div>
                                        <div class="col-2">
                                            <asp:Button ID="btnProductSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnViewAllServices_Click" />
                                        </div>
                                    </div>
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

                    <div runat="server" id="divServiceTypes">
                        <div class="jumbotron bg-dark text-white">
                            <h1>Service Types</h1>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="lblServicetypes"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <!--add product btn -->
                                <a class='btn btn-primary' href='?Action=NewType'>New Type </a>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12 col-md-12">
                                <!-- Line Break -->
                                <br />
                                <asp:Table ID="tblServiceTypes" runat="server"></asp:Table>
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divNewType">
                        <div class="jumbotron bg-dark text-white">
                            <h1>
                                <asp:Label runat="server" ID="lblNewTypeHeader" Text="New Type"></asp:Label></h1>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-2">
                                Name:
                            </div>
                            <div class="col-6">
                                <asp:TextBox ID="txtTypeName" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-4"></div>
                        </div>
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-6">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtTypeName" runat="server"
                                    ErrorMessage="*Name is required" ControlToValidate="txtTypeName"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-4"></div>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-2">
                                <asp:Button Style="float: left;" ID="btnCancelAddType" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancelAddType_Click" />
                            </div>
                            <div class="col-6">
                                <asp:Button Style="float: right;" ID="btnAddType" runat="server" Text="Create Type" CssClass="btn btn-primary" OnClick="btnAddType_Click" />
                            </div>
                            <div class="col-4"></div>
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
        </div>
        <br />
        <br />
    </form>
</asp:Content>
