<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Cheveux.Manager.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Manage Products - Cheveux
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
            <form runat="server">
                <!-- if the user is loged In -->
                <div runat="server" id="LogedIn" visible="false">


                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12">
                            <div id="divTabs" runat="server">
                                <!--Tabs-->
                                <ul class="nav nav-tabs" role="tablist">
                                    <li><a class="btn" href="#ViewAllProducts" role="tab" data-toggle="tab" ac>Products&#09;</a></li>
                                    <li><a class="btn" href="#OutstandingOrders" role="tab" data-toggle="tab">Outstanding Orders&#09;</a></li>
                                    <li><a class="btn" href="#PastOrders" role="tab" data-toggle="tab">Past Orders</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <!-- Line Break -->
                    <br />

                    <div class="tab-content">
                        <div class="active tab-pane" id="ViewAllProducts">

                            <!-- Jumbo Tron -->
                            <div class="jumbotron bg-dark text-white">

                                <h1>Manage Products</h1>
                                <!-- line Break -->
                                <br />


                                <!-- View By Selector -->
                                <p>View Products By: </p>
                                <asp:DropDownList ID="drpProductType" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>

                                <!-- line Break -->
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-12">
                                        <!-- View By search tearm -->
                                        <p>View Products By Search Term: </p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-5">
                                        <asp:TextBox class="form-control" ID="txtProductSearchTerm" runat="server" AutoPostBack="true"
                                            OnTextChanged="Page_Load"></asp:TextBox>
                                    </div>
                                    <div class="col-2">
                                        <asp:Button CssClass="btn btn-primary" ID="btnProductSearch" runat="server" Text="Search" />
                                    </div>
                                </div>
                            </div>



                            <div class="row">
                                <div class="col-md-10">
                                    <asp:Label ID="productJumbotronLable" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <!--add product btn -->
                                    <a class='btn btn-primary' href='../Cheveux/Products.aspx?Action=Add'>New Product </a>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <!-- List Product Table -->
                                    <asp:Table ID="tblProductTable" runat="server">
                                    </asp:Table>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" id="OutstandingOrders">
                            outst
                        </div>
                        <div class="tab-pane" id="PastOrders">
                            past
                        </div>
                    </div>
                </div>

                <!-- if the user is loged Out -->
                <div runat="server" id="LogedOut">
                    <div class="bg-dark text-white jumbotron">
                        <div class="row">
                            <div class="col-10">
                                <h1>Manage Products</h1>
                                <p>Please log-in to Manage Products</p>
                            </div>
                            <div class="col-2">
                                <a class="btn btn-primary" href="../Authentication/Accounts.aspx?PreviousPage=Products.aspx" id="LogedOutButton">Login</a>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Order -->
                <div runat="server" id="divNewOrder" visible="false">
                    <asp:Table runat="server" ID="NewOrder">
                    </asp:Table>
                </div>
            </form>
        </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
